using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using navtezcms.DAL;
using navtezcms.BO;



namespace navtezcms.adminw.Admin
{
    public static class Helper
    {
        public static string GetindiaDateTime()
        {
            TimeZoneInfo India_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, India_Standard_Time).ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        public static string GetindiaDateTime_dd_MMM_yyyy()
        {
            TimeZoneInfo India_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, India_Standard_Time).ToString("dd-MMM-yyyy");
        }
        public static void DeleteCokkies(string name)
        {
            HttpContext.Current.Response.Cookies[name].Value = "";
            HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
        }

        public static void CreateCokkies(string name, string value, int days)
        {
            HttpContext.Current.Response.Cookies[name].Value = value;
            HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(days);
        }

        public static string ReadCookiee(string name)
        {
            string value = string.Empty;
            try
            {
                if (HttpContext.Current.Request.Cookies[name] != null)
                {
                    value = HttpContext.Current.Request.Cookies[name].Value.ToString();
                }
            }
            catch
            {
                throw;
            }
            return value;
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = System.Configuration.ConfigurationManager.AppSettings["EncKey"];
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = System.Configuration.ConfigurationManager.AppSettings["EncKey"];
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static List<T> DataTableToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
        public static void CreatAdminLoginCookies(AdminSession AdminSession)
        {
            try
            {
                var str = Encrypt(JsonConvert.SerializeObject(AdminSession));
                CreateCokkies("_Admin_SessionId_Navtej", str, 365);
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        public static void ReLoginAdmin()
        {
            string str = string.Empty;
            DataTable dt = new DataTable();
            AdminSession adminSession = new AdminSession();
            try
            {
                if (HttpContext.Current.Session["LoggedInPerson"] == null)
                {
                    str = ReadCookiee("_Admin_SessionId_Navtej");
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = Decrypt(str);
                        if (!string.IsNullOrEmpty(str))
                        {
                            adminSession = JsonConvert.DeserializeObject<AdminSession>(str);
                            if (adminSession != null)
                            {

                                List<PEmployee> lstUser = DBHelper.GetEmployeeByEmailPassword(adminSession.Email, adminSession.Password);
                                if (lstUser.Count > 0)
                                {
                                    HttpContext.Current.Session["LoggedInPerson"] = lstUser;

                                    List<PAdminMenu> lstAdminMenu = new List<PAdminMenu>();
                                    lstAdminMenu = DBHelper.GetAdminMenuByRoleID(lstUser[0].RoleID);
                                    DataTable dtMenu = navtezcms.BO.Common.ToDataTable(lstAdminMenu);
                                    HttpContext.Current.Session["dtMenu"] = dtMenu;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw;
            }

        }
    }
}
public class AdminSession
{
    public string Email { get; set; }
    public string Password { get; set; }
}