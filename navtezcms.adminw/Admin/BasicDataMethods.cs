using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Globalization;
namespace navtezcms.adminw.App_Code
{
    public partial class BasicDataMethods
    {
        public static List<cls_Employee> GetEmployeeByEmailPassword(string Email, string Password)
        {
            string query = "SELECT * FROM [dbo].[Employee] where Email = '" + Email + "' and Password = '" + Password + "'";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Employee> lstEmployee = new List<cls_Employee>().FromDataReader(dr).ToList<cls_Employee>();
            return lstEmployee;
        }

        // Banner
        public static List<cls_Banner> GetBanner()
        {
            string query = "SELECT B.*, (select FirstName + ' ' + LastName from Employee where ID = B.CreatedBy) as EmployeeName FROM [dbo].[Banner] as B";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Banner> lstBanner = new List<cls_Banner>().FromDataReader(dr).ToList<cls_Banner>();
            return lstBanner;
        }
        public static List<cls_Banner> GetBannerById(string ID)
        {
            string query = "SELECT * FROM [dbo].[Banner] where ID = '" + ID + "'";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Banner> lstDept = new List<cls_Banner>().FromDataReader(dr).ToList<cls_Banner>();
            return lstDept;
        }

        public static int InsertBanner(cls_Banner objBanner)
        {
            string query = "INSERT INTO [dbo].[Banner]([BannerImageName],[BannerText],[LinkToUrl],[CreatedBy]) VALUES ('" + objBanner.BannerImageName + "','" + objBanner.BannerText + "','" + objBanner.LinkToUrl + "'," + objBanner.CreatedBy + ")";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int UpdateBanner(cls_Banner objBanner)
        {
            string query = "Update [dbo].[Banner] set [BannerImageName] = '" + objBanner.BannerImageName + "',[BannerText] = '" + objBanner.BannerText + "',[LinkToUrl] = '" + objBanner.LinkToUrl + "',[CreatedBy] = '" + objBanner.CreatedBy + "' where [ID] = '" + objBanner.ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int DeleteBanner(string ID)
        {
            string query = "Delete from [dbo].[Banner] where [ID] = '" + ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        // ParentType
        public static List<cls_ParentType> GetParentType()
        {
            string query = "SELECT * FROM [dbo].[ParentType]";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_ParentType> lstParentType = new List<cls_ParentType>().FromDataReader(dr).ToList<cls_ParentType>();
            return lstParentType;
        }
        public static List<cls_ParentType> GetParentTypeById(string ID)
        {
            string query = "SELECT * FROM [dbo].[ParentType] where ID = '" + ID + "'";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_ParentType> lstDept = new List<cls_ParentType>().FromDataReader(dr).ToList<cls_ParentType>();
            return lstDept;
        }

        public static int InsertParentType(cls_ParentType objParentType)
        {
            string query = "INSERT INTO [dbo].[ParentType]([ParentType]) VALUES ('" + objParentType.ParentType + "')";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int UpdateParentType(cls_ParentType objParentType)
        {
            string query = "Update [dbo].[ParentType] set [ParentType] = '" + objParentType.ParentType + "' where [ID] = '" + objParentType.ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int DeleteParentType(string ID)
        {
            string query = "Delete from [dbo].[ParentType] where [ID] = '" + ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        // Language
        public static List<cls_Language> GetLanguage()
        {
            string query = "SELECT * FROM [dbo].[Language]";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Language> lstLanguage = new List<cls_Language>().FromDataReader(dr).ToList<cls_Language>();
            return lstLanguage;
        }
        public static List<cls_Language> GetLanguageById(string ID)
        {
            string query = "SELECT * FROM [dbo].[Language] where ID = '" + ID + "'";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Language> lstDept = new List<cls_Language>().FromDataReader(dr).ToList<cls_Language>();
            return lstDept;
        }

        public static int InsertLanguage(cls_Language objLanguage)
        {
            string query = "INSERT INTO [dbo].[Language]([Name]) VALUES ('" + objLanguage.Name + "')";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int UpdateLanguage(cls_Language objLanguage)
        {
            string query = "Update [dbo].[Language] set [Name] = '" + objLanguage.Name + "' where [ID] = '" + objLanguage.ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int DeleteLanguage(string ID)
        {
            string query = "Delete from [dbo].[Language] where [ID] = '" + ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }

        // Translation
        public static List<cls_Translation> GetTranslation()
        {
            string query = "SELECT * FROM [dbo].[Translation]";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Translation> lstTranslation = new List<cls_Translation>().FromDataReader(dr).ToList<cls_Translation>();
            return lstTranslation;
        }
        public static List<cls_Translation> GetTranslationById(string ID)
        {
            string query = "SELECT * FROM [dbo].[Translation] where TextContentID = '" + ID + "'";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Translation> lstDept = new List<cls_Translation>().FromDataReader(dr).ToList<cls_Translation>();
            return lstDept;
        }

        public static int InsertTranslation(cls_Translation objTranslation)
        {
            string query = "INSERT INTO [dbo].[Translation]([LanguageId],[Translation]) VALUES ('" + objTranslation.LanguageId + "', N'" + objTranslation.Translation + "'); SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];";
            int id = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, query, null));
            return id;
        }
        public static int UpdateTranslation(cls_Translation objTranslation)
        {
            string query = "Update [dbo].[Translation] set [Translation] = N'" + objTranslation.Translation + "' where [LanguageId] = '" + objTranslation.LanguageId + "' and [TextContentID] = '" + objTranslation.TextContentID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int DeleteTranslation(string ID)
        {
            string query = "Delete from [dbo].[Translation] where [TextContentID] = '" + ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }

        // Event
        public static List<cls_Event> GetEvent()
        {
            string query = @"select E.*,
                        (select Translation from Translation where TextContentID = E.TitleContentID) as Title,
                        (select Translation from Translation where TextContentID = E.ContactPNameContentID) as ContactPName,
                        (select Translation from Translation where TextContentID = E.DescriptionContentID) as Description,
                        (select Translation from Translation where TextContentID = E.EventAddressContentID) as EventAddress
                        from[Event] as E";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Event> lstEvent = new List<cls_Event>().FromDataReader(dr).ToList<cls_Event>();
            return lstEvent;
        }
        public static List<cls_Event> GetEventById(string ID)
        {
            string query = @"select E.*,
                        (select Translation from Translation where TextContentID = E.TitleContentID) as Title,
                        (select Translation from Translation where TextContentID = E.ContactPNameContentID) as ContactPName,
                        (select Translation from Translation where TextContentID = E.DescriptionContentID) as Description,
                        (select Translation from Translation where TextContentID = E.EventAddressContentID) as EventAddress
                        from[Event] as E where E.ID = '" + ID + "'";
            System.Data.SqlClient.SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, query);
            List<cls_Event> lstDept = new List<cls_Event>().FromDataReader(dr).ToList<cls_Event>();
            return lstDept;
        }

        public static int InsertEvent(cls_Event objEvent)
        {
            string query = "INSERT INTO [dbo].[Event]([TitleContentID],[DescriptionContentID],[EventDate],[ISFree],[Price],[GST]," +
            "[RegistrationOpenTill],[ISOnlineEvent],[EventAddressContentID],[GoogleMapUrl],[ContactPNameContentID],[ContactPPhone]," +
            "[ContactPEmail],[AttachmentLink]) VALUES ('" + objEvent.TitleContentID + "','" + objEvent.DescriptionContentID + "','" + objEvent.EventDate.ToString("yyyy-MM-dd") + "'," + objEvent.ISFree + "," +
            "'" + objEvent.Price + "','" + objEvent.GST + "','" + objEvent.RegistrationOpenTill.ToString("yyyy-MM-dd") + "'," + objEvent.ISOnlineEvent + "," +
            "'" + objEvent.EventAddressContentID + "','" + objEvent.GoogleMapUrl + "','" + objEvent.ContactPNameContentID + "','" + objEvent.ContactPPhone + "'," +
            "'" + objEvent.ContactPEmail + "','" + objEvent.AttachmentLink + "')";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int UpdateEvent(cls_Event objEvent)
        {
            string query = "Update [dbo].[Event] set [TitleContentID] = '" + objEvent.TitleContentID + "',[DescriptionContentID] = '" + objEvent.DescriptionContentID + "',[EventDate] = '" + objEvent.EventDate + "',[ISFree] = '" + objEvent.ISFree + "'," +
                "[Price] = '" + objEvent.Price + "',[GST] = '" + objEvent.GST + "',[RegistrationOpenTill] = '" + objEvent.RegistrationOpenTill + "',[ISOnlineEvent] = '" + objEvent.ISOnlineEvent + "'," +
                "[EventAddressContentID] = '" + objEvent.EventAddressContentID + "',[GoogleMapUrl] = '" + objEvent.GoogleMapUrl + "',[ContactPNameContentID] = '" + objEvent.ContactPNameContentID + "',[ContactPPhone] = '" + objEvent.ContactPPhone + "'" +
                " where [ID] = '" + objEvent.ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }
        public static int DeleteEvent(string ID)
        {
            string query = "Delete from [dbo].[Event] where [ID] = '" + ID + "'";
            int id = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, query, null));
            return id;
        }

        public static string SqlClean(string value)
        {
            if (value == "")
            {
                return value;
            }
            else
            {
                value = value.Replace("'", "''");
                return value;
            }
        }
    }
}