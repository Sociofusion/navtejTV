using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using navtezcms.BO;
using Dapper;
using System.Data;

namespace navtezcms.DAL
{
    public class ParamAPIDBHelper
    {
        public static SqlConnection GetConnectionDest => new SqlConnection(ConfigurationManager.ConnectionStrings["ConStrDest"].ConnectionString);

        public static bool ISSessionTokenValid(string sessionToken)
        {
            string query = "select top 1 * from APISession where APISession_Token = @APISession_Token ";
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PAPISession> pAPISessions = conn.Query<PAPISession>(query, new { APISession_Token = sessionToken }).ToList();
                if (pAPISessions.Count > 0)  // && (DateTime.Now - pAPISessions[0].APISession_LastUsed).TotalHours < Common.MajikServiceTimeout(removed this date check)
                {
                    //update last session time
                    RefreshSessionTokenTime(sessionToken);
                    return true;
                }
                else
                {
                    throw new Exception("Session Token has expired, Please login again to get new token");
                }
            }
        }

        public static void RefreshSessionTokenTime(string sessionToken)
        {
            string query = "update APISession set APISession_LastUsed = GETDATE()  where APISession_Token = @APISession_Token ";
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                conn.Execute(query, new { APISession_Token = sessionToken });
            }
        }


        public static bool CanUseCustomerEmail(string emailId, int customerId)
        {

            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select ID from Customer where Email = @Email ";
                int id = conn.ExecuteScalar<int>(query, new { Email = emailId });

                if (id == 0)
                    return true;
                else
                {
                    if (id == customerId)
                        return true;
                    else
                        return false;
                }
            }
        }

        public static PAPISession AuthenticateCustomer(string emailId, string password, string hostAddress, string deviceName)
        {
            PAPISession pAPISession = new PAPISession();
            string encryptPwd = new CryptLib().EnryptwithAES(password);

            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select * from Customer where Email = @Email ";
                List<PCustomer> lst = conn.Query<PCustomer>(query, new { Email = emailId }).ToList();
                if (lst.Count > 0)
                {
                    if (lst[0].Password == encryptPwd)
                    {
                        string sessionToken = Guid.NewGuid().ToString();
                        string lastToken = GetLastSession(lst[0].ID, sessionToken, hostAddress, deviceName);

                        if (!string.IsNullOrEmpty(lastToken))
                        {
                            pAPISession.APISession_Token = lastToken;
                            pAPISession.APISession_CustomerID = lst[0].ID.ToString();
                            return pAPISession;
                        }
                        else
                        {
                            throw new Exception("Not able to create session, Please contact to Majik Administrator");
                        }
                    }
                    else
                        throw new Exception("Invalid Password");
                }
                else
                {
                    throw new Exception("The user id does not exist. Please try to Sign up instead.");
                }

            }
        }

        private static string GetLastSession(int customerId, string sessionToken, string hostAddress = "", string deviceName = "")
        {
            string query = "select top 1 * from APISession where APISession_CustomerID = @APISession_CustomerID order by APISession_LastUsed desc ";
            using (var conn = GetConnectionDest)
            {
                List<PAPISession> pAPISessions = conn.Query<PAPISession>(query, new { APISession_CustomerID = customerId }).ToList();

                if (pAPISessions.Count > 0 && (DateTime.Now - pAPISessions[0].APISession_LastUsed).TotalHours < Common.ParamServiceTimeout)
                {
                    //update last session time
                    RefreshSessionTokenTime(pAPISessions[0].APISession_Token);
                    return pAPISessions[0].APISession_Token;
                }
                else
                {
                    if (CreateSessionRequest(customerId, sessionToken, hostAddress, deviceName) > 0)
                    {
                        return sessionToken;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

        }

        private static long CreateSessionRequest(int customerId, string sessionToken, string hostAddress = "", string deviceName = "")
        {
            PAPISession pAPISession = new PAPISession();
            pAPISession.APISession_CreatedOn = DateTime.Now;
            pAPISession.APISession_CustomerID = customerId.ToString();
            pAPISession.APISession_DeviceName = deviceName;
            pAPISession.APISession_LastUsed = DateTime.Now;
            pAPISession.APISession_Token = sessionToken;
            pAPISession.APISession_UserHostAddress = hostAddress;


            using (var conn = GetConnectionDest)
            {
                return conn.Insert<PAPISession>(pAPISession);
            }
        }



        public static bool InsertUpdateCustomer(PCustomer pCustomer)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                if (pCustomer.ID > 0)
                {
                    pCustomer.Password = new CryptLib().EnryptwithAES(pCustomer.Password);
                    return conn.Insert<PCustomer>(pCustomer) > 0;
                }
                else
                    return conn.Update<PCustomer>(pCustomer);
            }
        }


        public static List<PEvent> GetEvents(out int totalRecordCount,
         int languageId = 0, int offset = 0, int itemcount = 10)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select  ID, TitleContentID, EventDate,StartTime,EndTime, EventAddressContentID from [dbo].[Event] where EventDate >= GETDATE() and ISActive=1 order by StartTime desc  OFFSET " + offset + " ROWS FETCH NEXT " + itemcount + " ROWS ONLY";
                List<PEvent> events = conn.Query<PEvent>(query).ToList();


                string countQuery = "select count(*) from Event  where ISActive=1 ";
                totalRecordCount = conn.ExecuteScalar<int>(countQuery);

                return events;
            }
        }


        public static PCustomer GetCustomerById(int customerId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = " select * from customer where ID = @ID ";
                List<PCustomer> customers = conn.Query<PCustomer>(query, new { ID = customerId }).ToList();
                if (customers.Count > 0)
                { return customers[0]; }
                else
                { throw new Exception("Customer did not found"); }
            }
        }

        public static List<POrder> GetCustomerOrders(
         out int totalRecordCount, int customerId,
         int languageId = 0, int offset = 0, int itemcount = 10)
        {
            List<POrder> orders = new List<POrder>();

            using (var conn = GetConnectionDest)
            {
                totalRecordCount = 0;
                conn.Open();

                string query = "select * from [Order] where ISActive=1 and CustomerID = @CustomerID order by AddDate desc OFFSET " + offset + " ROWS FETCH NEXT " + itemcount + " ROWS ONLY";
                orders = conn.Query<POrder>(query, new { CustomerID = customerId }).ToList();
                foreach (var item in orders)
                {
                    string query1 = "select top 1 *,(select [Translation]  from  dbo.Translation where TextContentID = Product.ID) as ProdTitle from OrderDetail inner join Product on Product.ID = OrderDetail.OrderDetailID where OrderDetail.ISActive = 1 and ISCancel = 0 and OrderDetail.OrderID = @orderid ";
                    item.LineItem = conn.Query<POrderDetail>(query1, new { OrderID = item.ID }).ToList();
                }

                string countQuery = "select count(*) from [Order] where ISActive=1 and CustomerID = @CustomerID ";
                totalRecordCount = conn.ExecuteScalar<int>(countQuery, new { CustomerID = customerId });
            }
            return orders;
        }

        public static bool UpdateCustomerPassword(int customerId, string password)
        {
            using (var conn = GetConnectionDest)
            {
                string newpassword = new CryptLib().EnryptwithAES(password);
                string query = "update customer set Password=@password where ID=@customerId";
                return conn.Execute(query, new { password = password, customerId = customerId }) > 0;
            }
        }
    }

}
