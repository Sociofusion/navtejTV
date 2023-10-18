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
    public class APIDBHelper
    {

        public enum ParentType
        {
            Event = 1,
            Home = 2,
            Contact = 3,
            Video = 5,
            Shop = 6,
            Donations = 7,
            CustomPage = 8,
            Advertisement = 9,
            About = 10,
            Post = 11,
            Category = 12,
            News = 13,
            Advertise = 14,
            Support = 15,
            Features = 16,
            Menu = 17,
            ProductCategory = 1012,
            Setting = 1013,
            Gallery = 1014
        }

        public static SqlConnection GetConnectionDest => new SqlConnection(ConfigurationManager.ConnectionStrings["ConStrDest"].ConnectionString);


        public static PCustomPage GetCustomPageBySlug(string slug, int languageid)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                if (!string.IsNullOrWhiteSpace(slug))
                {
                    string query = "select ID from CustomPage where Slug = '" + slug + "' and ISActive=1";
                    int id = Convert.ToInt32(conn.ExecuteScalar<int>(query));
                    if (id > 0)
                    {
                        return DBHelper.GetCustomPageById(id, languageid);
                    }
                    else
                    {
                        return null;
                    }
                }
                else { return null; }
            }
        }

        public static string GetFrontHeaderMenu(int langId)
        {
            string jsonString = "";
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select JsonString from MenuJson where LanguageID = '" + langId + "'";
                IDataReader dr = conn.ExecuteReader(query);
                while (dr.Read())
                {
                    jsonString = Convert.ToString(dr["JsonString"]);
                }
            }

            return jsonString;
        }

        public static List<FrontHeaderMenu> GetCategories(int langId, int category, int categoryMenuOrder)
        {
            List<PCategory> allCategories = DBHelper.GetAllCategoriesByLanguageID(langId);
            List<PCategory> pCategories = allCategories.Where(i => i.ISActive == 1 && i.ParentCategoryID == category && i.CanShowOnMenu == 1).ToList();

            List<FrontHeaderMenu> klst = new List<FrontHeaderMenu>();
            FrontHeaderMenu frontHeaderMenu = null;
            foreach (var item in pCategories)
            {
                frontHeaderMenu = new FrontHeaderMenu();
                frontHeaderMenu.MenuOrder = item.CategoryOrder + categoryMenuOrder;
                frontHeaderMenu.ID = item.ID;
                frontHeaderMenu.ParentMenuID = item.ParentCategoryID;
                frontHeaderMenu.ParentTypeID = (int)ParentType.Category;
                frontHeaderMenu.MenuTitle = item.DefaultTitleToDisplay;
                frontHeaderMenu.Slug = item.Slug;
                frontHeaderMenu.GotoStatePage = item.GotoStatePage;
                frontHeaderMenu.childs.AddRange(GetCategoriesRecursive(allCategories, item.ID));
                klst.Add(frontHeaderMenu);
            }

            return klst;
        }

        public static List<FrontHeaderMenu> GetCategoriesRecursive(List<PCategory> allCategories, long categoryId)
        {
            List<PCategory> pCategories = allCategories.Where(i => i.ISActive == 1 && i.ParentCategoryID == categoryId && i.CanShowOnMenu == 1).ToList();
            List<FrontHeaderMenu> klst = new List<FrontHeaderMenu>();
            FrontHeaderMenu frontHeaderMenu = null;
            foreach (var item in pCategories)
            {
                frontHeaderMenu = new FrontHeaderMenu();
                frontHeaderMenu.MenuOrder = item.CategoryOrder;
                frontHeaderMenu.ID = item.ID;
                frontHeaderMenu.ParentMenuID = item.ParentCategoryID;
                frontHeaderMenu.ParentTypeID = (int)ParentType.Category;
                frontHeaderMenu.MenuTitle = item.DefaultTitleToDisplay;
                frontHeaderMenu.GotoStatePage = item.GotoStatePage;
                frontHeaderMenu.Slug = item.Slug;

                frontHeaderMenu.childs.AddRange(GetCategoriesRecursive(allCategories, item.ID));

                klst.Add(frontHeaderMenu);
            }

            return klst;
        }

        public static List<PSetting> GetSettings()
        {
            return DBHelper.GetSetting().ToList();
        }

        public static List<PLanguage> GetAllLanguages()
        {
            return DBHelper.GetAllLanguages().Where(i => i.ISActive == 1).ToList();
        }

        public static PAdvertisment GetAdvertismentByPlacementAreaName(string placementAreaName)
        {
            return DBHelper.GetAdvertisementByPlacement(placementAreaName);
        }

        public static List<FrontHeaderMenu> GetFrontFooterMenu(int langId)
        {
            List<FrontHeaderMenu> klst = new List<FrontHeaderMenu>();

            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PMenu> pMenus = conn.Query<PMenu>("select * from Menu where ISFooter=1 and ISActive=1 and ParentMenuId=0 ").ToList();
                FrontHeaderMenu frontHeaderMenu = null;

                foreach (var item in pMenus)
                {
                    if (item.ParentTypeID == (int)ParentType.Category || item.ParentTypeID == (int)ParentType.CustomPage)
                    {
                        //load category
                        klst.AddRange(GetCategories(langId, 0, item.MenuOrder));
                    }
                    else
                    {
                        frontHeaderMenu = new FrontHeaderMenu();
                        frontHeaderMenu.MenuOrder = item.MenuOrder;
                        frontHeaderMenu.ID = item.ID;
                        frontHeaderMenu.ParentMenuID = item.ParentMenuID;
                        frontHeaderMenu.ParentTypeID = item.ParentTypeID;
                        frontHeaderMenu.MenuTitle = DBHelper.GetPTranslationByLangId(item.ID, (int)APIDBHelper.ParentType.Menu, langId);



                        if (frontHeaderMenu.ParentTypeID == (int)ParentType.Home)
                            frontHeaderMenu.Slug = "home";
                        else if (frontHeaderMenu.ParentTypeID == (int)ParentType.About)
                            frontHeaderMenu.Slug = "about";
                        else if (frontHeaderMenu.ParentTypeID == (int)ParentType.Contact)
                            frontHeaderMenu.Slug = "contact";
                        else if (frontHeaderMenu.ParentTypeID == (int)ParentType.News)
                            frontHeaderMenu.Slug = "news";
                        else if (frontHeaderMenu.ParentTypeID == (int)ParentType.Support)
                            frontHeaderMenu.Slug = "support";
                        else if (frontHeaderMenu.ParentTypeID == (int)ParentType.Features)
                            frontHeaderMenu.Slug = "features";
                        else if (frontHeaderMenu.ParentTypeID == (int)ParentType.Advertise)
                            frontHeaderMenu.Slug = "advertise";
                        else
                            frontHeaderMenu.Slug = item.FooterSlug;

                        klst.Add(frontHeaderMenu);
                    }
                }

            }

            return klst.OrderBy(i => i.MenuOrder).ToList();
        }

        public static PPost GetDetailByPostId(long postId, int languageId, string slug)
        {
            PPost pPost = null;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select * from Post where ID = " + postId + "";
                if (postId == 0 && !string.IsNullOrWhiteSpace(slug))
                {
                    query = "select * from Post where rtrim(ltrim(Slug)) = '" + slug + "' ";
                }

                List<PPost> posts = conn.Query<PPost>(query).ToList();
                if (posts.Count > 0)
                {
                    pPost = posts[0];
                    List<PTranslation> TranslationData = DBHelper.GetPTranslations(pPost.ID, (int)APIDBHelper.ParentType.Post, languageId);
                    pPost.TitleData = TranslationData.Where(i => i.TextContentID == pPost.TitleContentID).ToList();
                    pPost.DescriptionData = TranslationData.Where(i => i.TextContentID == pPost.DescriptionContentID).ToList();
                    pPost.MetaTagData = TranslationData.Where(i => i.TextContentID == pPost.MetaTagContentID).ToList();

                    PCategory pCategory = DBHelper.GetCategoryById(pPost.CategoryID, languageId);
                    pPost.CategoryName = pCategory.DefaultTitleToDisplay;
                    pPost.CategoryColor = pCategory.Color;

                    pPost.PostType = conn.ExecuteScalar<string>("select PostType from PostType where ID = " + pPost.PostTypeID + "");

                    if (pPost.ImageBigAssetID > 0)
                    {
                        pPost.PostFiles = DBHelper.GetAssetByID(pPost.ImageBigAssetID, (int)ParentType.Post);
                    }
                    else
                    {
                        pPost.PostFiles = DBHelper.GetAssets(pPost.ID, (int)ParentType.Post);
                    }


                }
                else
                {
                    throw new Exception("No post found for this ID");
                }
            }

            return pPost;
        }


        public static PPostSEO GetDetailByPostSlug(int languageId, string slug)
        {
            PPostSEO pPost = null;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "";
                if (!string.IsNullOrWhiteSpace(slug))
                {
                    query = "Select ID, Slug, TitleContentID, MetaTagContentID, Tags, ImageBig  from Post where rtrim(ltrim(Slug)) = '" + slug + "' ";
                }

                List<PPostSEO> posts = conn.Query<PPostSEO>(query).ToList();
                if (posts.Count > 0)
                {
                    pPost = posts[0];
                    List<PTranslation> TranslationData = DBHelper.GetPTranslations(pPost.ID, (int)APIDBHelper.ParentType.Post, languageId);
                    pPost.TitleData = TranslationData.Where(i => i.TextContentID == pPost.TitleContentID).ToList();
                    pPost.MetaTagData = TranslationData.Where(i => i.TextContentID == pPost.MetaTagContentID).ToList();
                    
                }
                else
                {
                    throw new Exception("No post found for this Slug");
                }
            }

            return pPost;
        }

        public static List<PPost> GetPostsForFrontEnd(
            out int totalRecordCount,
            long category = 0,
            int isFeature = 0,
            int isSlider = 0,
            int isSliderLeft = 0,
            int isSliderRight = 0,
            int isTrending = 0,
            int languageId = 0, int offset = 0, int itemcount = 10, string categorySlug = "")
        {
            List<PPost> pPosts = new List<PPost>();

            using (var conn = GetConnectionDest)
            {
                if (category == 0 && !string.IsNullOrWhiteSpace(categorySlug))
                    category = DBHelper.GetCategoryIDFromSlug(categorySlug);

                string categories = "";
                if (category > 0)
                    categories = GetAllChildCategories(category);
                if (string.IsNullOrWhiteSpace(categories))
                    categories = category.ToString();
                categories = categories.TrimStart(new char[] { ',' });
                totalRecordCount = 0;
                conn.Open();
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    if (category > 0)
                        cmd.CommandText = "exec [Proc_GetPost] '" + categories + "'," + isFeature + " , " + isSlider + ", " + isSliderLeft + ", " + isSliderRight + ", " + isTrending + " , " + offset + ", " + itemcount + ", " + languageId + "";
                    else
                        cmd.CommandText = "exec [Proc_GetPost] ''," + isFeature + " , " + isSlider + ", " + isSliderLeft + ", " + isSliderRight + ", " + isTrending + " , " + offset + ", " + itemcount + ", " + languageId + "";
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataTable dtPost = ds.Tables[0];
                    DataTable dtCount = ds.Tables[1];
                    if (dtCount != null && dtCount.Rows.Count > 0)
                    {
                        totalRecordCount = Convert.ToInt32(dtCount.Rows[0][0]);
                    }

                    PPost pPost = null;
                    if (dtPost != null && dtPost.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtPost.Rows)
                        {
                            pPost = new PPost();
                            pPost.CategoryID = Convert.ToInt32(row["CategoryID"]);
                            pPost.CategoryName = Convert.ToString(row["CategoryName"]);
                            pPost.CategoryColor = Convert.ToString(row["CategoryColor"]);
                            pPost.ID = Convert.ToInt64(row["ID"]);
                            pPost.Slug = Convert.ToString(row["Slug"]);
                            pPost.PostType = Convert.ToString(row["PostType"]);
                            pPost.DefaultTitleToDisplay = Convert.ToString(row["DefaultTitleToDisplay"]);
                            pPost.DefaultMetaTagToDisplay = Convert.ToString(row["DefaultMetaTagToDisplay"]);
                            pPost.DefaultDescriptionToDisplay = Convert.ToString(row["DefaultDescriptionToDisplay"]);

                            //pPost.AssetUniqueName = Convert.ToString(row["AssetUniqueName"]);
                            //pPost.AssetActualName = Convert.ToString(row["AssetActualName"]);
                            pPost.TitleData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultTitleToDisplay } };
                            pPost.DescriptionData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultDescriptionToDisplay } };
                            pPost.MetaTagData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultMetaTagToDisplay } };
                            pPost.ImageBigAssetID = Convert.ToInt32(row["ImageBigAssetID"]);
                            if (pPost.ImageBigAssetID > 0)
                            {
                                pPost.PostFiles = DBHelper.GetAssetByID(pPost.ImageBigAssetID, (int)ParentType.Post);
                            }
                            else
                            {
                                pPost.PostFiles = DBHelper.GetAssets(pPost.ID, (int)ParentType.Post);
                            }
                            pPosts.Add(pPost);
                        }
                    }
                }
            }
            return pPosts;

        }



        public static string GetAllChildCategories(long categoryId)
        {
            string query = @"WITH CatCTE (cat_id) AS        (                SELECT t.ID       FROM Category t    WHERE t.ID = " + categoryId + "      UNION ALL  SELECT P.ID as cat_id   FROM CatCTE AS m  JOIN Category AS P on m.cat_id = P.ParentCategoryID  )   SELECT cat_id FROM CatCTE";

            string result = "";
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                IDataReader dr = conn.ExecuteReader(query);
                while (dr.Read())
                {
                    result = result + "," + Convert.ToString(dr["cat_id"]);
                }
            }

            return result;
        }

        public static List<PCategory> GetAllParentCategories(int languageId)
        {
            return DBHelper.GetParentCategories(languageId);
        }

        public static bool InsertIntoNewsLetter(PNewsletterSub pNewsletterSub)
        {
            using (var conn = GetConnectionDest)
            {
                return conn.Insert<PNewsletterSub>(pNewsletterSub) > 0;
            }
        }

        public static bool InsertIntoContactUs(PContactUs pContact)
        {
            using (var conn = GetConnectionDest)
            {
                return conn.Insert<PContactUs>(pContact) > 0;
            }
        }


        public static List<PPost> GetSearchContent(
           out int totalRecordCount,
           int languageId = 0, int offset = 0, int itemcount = 10, string searchKeyword = "")
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PPost> pPosts = new List<PPost>();

                totalRecordCount = 0;
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    if (searchKeyword.Contains("'"))
                        searchKeyword = searchKeyword.Replace("'", "''");

                    cmd.CommandText = "exec [Proc_SearchPosts] N'" + searchKeyword + "', " + offset + ", " + itemcount + ", " + languageId + "";
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataTable dtPost = ds.Tables[0];
                    DataTable dtCount = ds.Tables[1];
                    if (dtCount != null && dtCount.Rows.Count > 0)
                    {
                        totalRecordCount = Convert.ToInt32(dtCount.Rows[0][0]);
                    }

                    PPost pPost = null;
                    if (dtPost != null && dtPost.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtPost.Rows)
                        {
                            pPost = new PPost();
                            pPost.CategoryID = Convert.ToInt32(row["CategoryID"]);
                            pPost.CategoryName = Convert.ToString(row["CategoryName"]);
                            pPost.ID = Convert.ToInt64(row["ID"]);
                            pPost.Slug = Convert.ToString(row["Slug"]);
                            pPost.PostType = Convert.ToString(row["PostType"]);
                            pPost.DefaultTitleToDisplay = Convert.ToString(row["DefaultTitleToDisplay"]);
                            pPost.DefaultMetaTagToDisplay = Convert.ToString(row["DefaultMetaTagToDisplay"]);
                            pPost.DefaultDescriptionToDisplay = Convert.ToString(row["DefaultDescriptionToDisplay"]);
                            //pPost.AssetUniqueName = Convert.ToString(row["AssetUniqueName"]);
                            //pPost.AssetActualName = Convert.ToString(row["AssetActualName"]);
                            pPost.TitleData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultTitleToDisplay } };
                            pPost.DescriptionData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultDescriptionToDisplay } };
                            pPost.MetaTagData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultMetaTagToDisplay } };
                            pPost.ImageBigAssetID = Convert.ToInt32(row["ImageBigAssetID"]);
                            if (pPost.ImageBigAssetID > 0)
                            {
                                pPost.PostFiles = DBHelper.GetAssetByID(pPost.ImageBigAssetID, (int)ParentType.Post);
                            }
                            else
                            {
                                pPost.PostFiles = DBHelper.GetAssets(pPost.ID, (int)ParentType.Post);
                            }
                            pPosts.Add(pPost);
                        }
                    }
                }

                return pPosts;

            }
        }



        public static List<PPost> GetRelatedNews(long postId,
          out int totalRecordCount, int languageId = 0, int offset = 0, int itemcount = 10)
        {
            List<PPost> pPosts = new List<PPost>();

            using (var conn = GetConnectionDest)
            {
                string tags = conn.ExecuteScalar<string>("select tags from Post where ID='" + postId + "'");
                List<string> stringList = tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                string query = "";
                string finalTags = "";
                foreach (string item in stringList)
                {
                    string tag = " Tags Like ''%" + item + "%'' OR ";
                    finalTags = finalTags + tag;
                }



                if (finalTags.Trim().EndsWith("OR"))
                    finalTags = finalTags.Trim().Remove(finalTags.Trim().Length - 2, 2);

                query = query + " " + finalTags;

                totalRecordCount = 0;
                conn.Open();
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "exec [Proc_GetRelatedPosts] '" + query + "', " + offset + ", " + itemcount + ", " + languageId + "";
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataTable dtPost = ds.Tables[0];
                    DataTable dtCount = ds.Tables[1];
                    if (dtCount != null && dtCount.Rows.Count > 0)
                    {
                        totalRecordCount = Convert.ToInt32(dtCount.Rows[0][0]);
                    }

                    PPost pPost = null;
                    if (dtPost != null && dtPost.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtPost.Rows)
                        {
                            pPost = new PPost();
                            pPost.CategoryID = Convert.ToInt32(row["CategoryID"]);
                            pPost.CategoryName = Convert.ToString(row["CategoryName"]);
                            pPost.CategoryColor = Convert.ToString(row["CategoryColor"]);
                            pPost.ID = Convert.ToInt64(row["ID"]);
                            pPost.Slug = Convert.ToString(row["Slug"]);
                            pPost.PostType = Convert.ToString(row["PostType"]);
                            pPost.DefaultTitleToDisplay = Convert.ToString(row["DefaultTitleToDisplay"]);
                            pPost.DefaultMetaTagToDisplay = Convert.ToString(row["DefaultMetaTagToDisplay"]);
                            pPost.DefaultDescriptionToDisplay = Convert.ToString(row["DefaultDescriptionToDisplay"]);
                            //pPost.AssetUniqueName = Convert.ToString(row["AssetUniqueName"]);
                            //pPost.AssetActualName = Convert.ToString(row["AssetActualName"]);
                            pPost.TitleData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultTitleToDisplay } };
                            pPost.DescriptionData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultDescriptionToDisplay } };
                            pPost.MetaTagData = new List<PTranslation> { new PTranslation { Translation = pPost.DefaultMetaTagToDisplay } };
                            pPost.ImageBigAssetID = Convert.ToInt32(row["ImageBigAssetID"]);
                            if (pPost.ImageBigAssetID > 0)
                            {
                                pPost.PostFiles = DBHelper.GetAssetByID(pPost.ImageBigAssetID, (int)ParentType.Post);
                            }
                            else
                            {
                                pPost.PostFiles = DBHelper.GetAssets(pPost.ID, (int)ParentType.Post);
                            }
                            pPosts.Add(pPost);
                        }
                    }
                }
            }
            return pPosts;

        }
    }
}
