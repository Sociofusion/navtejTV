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
using System.Web;

namespace navtezcms.DAL
{
    public class DBHelper
    {
        public enum EStatus
        {
            Draft = 1,
            Approved = 2,
            Pending = 3
        }


        public static SqlConnection GetConnectionDest => new SqlConnection(ConfigurationManager.ConnectionStrings["ConStrDest"].ConnectionString);
        public static SqlConnection GetConnectionSource => new SqlConnection(ConfigurationManager.ConnectionStrings["ConStrSource"].ConnectionString);
        public static long GetNextTextContentID()
        {
            long nextID = 1;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PBusinessBase pBusinessBase = new PBusinessBase();
                pBusinessBase.CreatedOn = DateTime.Now.ToString("yyyy-MM-dd");
                nextID = conn.Insert<PBusinessBase>(pBusinessBase);
            }

            return nextID;
        }

        public static long GetNextBusinessID()
        {
            long nextID = 1;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PBusinessBase pBusinessBase = new PBusinessBase();
                pBusinessBase.CreatedOn = DateTime.Now.ToString("yyyy-MM-dd");
                nextID = conn.Insert<PBusinessBase>(pBusinessBase);
            }

            return nextID;
        }

        public static List<PPostType> GetPostTypes()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return conn.GetAll<PPostType>().ToList();
            }
        }

        #region Import Data
        public static void ImportCategries()
        {
            using (var connDest = GetConnectionDest)
            {
                connDest.Open();

                using (var transaction = connDest.BeginTransaction())
                {
                    using (var connSource = GetConnectionSource)
                    {
                        connSource.Open();

                        List<PLanguage> pLanguages = GetAllLanguages();

                        IDataReader dr = connSource.ExecuteReader("Select * from [webcomcl_news].[categories] ");
                        while (dr.Read())
                        {
                            int language_id = dr["language_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["language_id"]);
                            int id = dr["id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["id"]);
                            string title = dr["title"] == DBNull.Value ? "" : Convert.ToString(dr["title"]);
                            string slug = dr["slug"] == DBNull.Value ? "" : Convert.ToString(dr["slug"]);
                            int parent_id = dr["parent_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["parent_id"]);
                            string color = dr["color"] == DBNull.Value ? "" : Convert.ToString(dr["color"]);
                            int category_order = dr["category_order"] == DBNull.Value ? 0 : Convert.ToInt32(dr["category_order"]);
                            int show_at_homepage = dr["show_at_homepage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["show_at_homepage"]);
                            int show_on_menu = dr["show_on_menu"] == DBNull.Value ? 0 : Convert.ToInt32(dr["show_on_menu"]);

                            long titleContentID = GetNextTextContentID();

                            PCategory pCategory = new PCategory();
                            pCategory.CanShowAtHome = show_at_homepage;
                            pCategory.CanShowOnMenu = show_on_menu;
                            pCategory.CategoryOrder = category_order;
                            pCategory.Color = color;
                            pCategory.ISActive = 1;
                            pCategory.ParentCategoryID = parent_id;
                            pCategory.Slug = slug.Replace(" ","-").Replace("'","");
                            pCategory.TitleContentID = titleContentID;
                            pCategory.ID = id;

                            connDest.Insert<PCategory>(pCategory, transaction);

                            foreach (var item in pLanguages)
                            {
                                PTranslation pTranslation = new PTranslation();
                                pTranslation.LanguageID = item.ID;
                                pTranslation.TextContentID = titleContentID;
                                pTranslation.Translation = title;
                                pTranslation.RecordID = id;

                                connDest.Insert<PTranslation>(pTranslation, transaction);

                            }
                        }
                    }

                    transaction.Commit();
                }
            }
        }
        public static void ImportPost()
        {
            List<PPostType> pPostTypes = GetPostTypes();
            using (var connDest = GetConnectionDest)
            {
                connDest.Open();

                using (var transaction = connDest.BeginTransaction())
                {
                    using (var connSource = GetConnectionSource)
                    {
                        connSource.Open();
                        List<PLanguage> pLanguages = GetAllLanguages();

                        IDataReader dr = connSource.ExecuteReader("Select * from  [webcomcl_news].[posts] ");
                        while (dr.Read())
                        {
                            int language_id = dr["language_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["language_id"]);
                            int id = dr["id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["id"]);
                            string title = dr["title"] == DBNull.Value ? "" : Convert.ToString(dr["title"]);
                            string slug = dr["slug"] == DBNull.Value ? "" : Convert.ToString(dr["slug"]);
                            string post_type = dr["post_type"] == DBNull.Value ? "article" : Convert.ToString(dr["post_type"]);
                            string meta_tag = dr["meta_tag"] == DBNull.Value ? "" : Convert.ToString(dr["meta_tag"]);
                            int show_right_column = dr["show_right_column"] == DBNull.Value ? 0 : Convert.ToInt32(dr["show_right_column"]);
                            int is_feature = dr["is_feature"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_feature"]);
                            int is_slider = dr["is_slider"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_slider"]);
                            int slider_left = dr["slider_left"] == DBNull.Value ? 0 : Convert.ToInt32(dr["slider_left"]);
                            int slider_right = dr["slider_right"] == DBNull.Value ? 0 : Convert.ToInt32(dr["slider_right"]);
                            int is_trending = dr["is_trending"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_trending"]);
                            int is_videoGallery = dr["is_videoGallery"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_videoGallery"]);
                            string tags = dr["tags"] == DBNull.Value ? "" : Convert.ToString(dr["tags"]);
                            string description = dr["description"] == DBNull.Value ? "" : Convert.ToString(dr["description"]);
                            string image_big = dr["image_big"] == DBNull.Value ? "" : Convert.ToString(dr["image_big"]);
                            string rss_image = dr["rss_image"] == DBNull.Value ? "" : Convert.ToString(dr["rss_image"]);
                            string image_small = dr["image_small"] == DBNull.Value ? "" : Convert.ToString(dr["image_small"]);
                            string video = dr["video"] == DBNull.Value ? "" : Convert.ToString(dr["video"]);
                            string embed_video = dr["embed_video"] == DBNull.Value ? "" : Convert.ToString(dr["embed_video"]);
                            string audio = dr["audio"] == DBNull.Value ? "" : Convert.ToString(dr["audio"]);
                            int category_id = dr["category_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["category_id"]);
                            int subcategories_id = dr["subcategories_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["subcategories_id"]);
                            int schedule_post = dr["schedule_post"] == DBNull.Value ? 0 : Convert.ToInt32(dr["schedule_post"]);
                            DateTime? schedule_post_date = dr["schedule_post_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["schedule_post_date"]);
                            int createdby = dr["admin_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["admin_id"]);
                            int isactive = 1;
                            DateTime? CreatedOn = dr["created_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["created_at"]);
                            DateTime? UpdatedOn = dr["updated_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["updated_at"]);
                            PPostType postType = pPostTypes.Where(i => i.PostType.ToLower() == post_type).FirstOrDefault();

                            int is_pending = dr["is_pending"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_pending"]);
                            int is_draft = dr["is_draft"] == DBNull.Value ? 0 : Convert.ToInt32(dr["is_draft"]);

                            //check category exists

                            int categoryid = category_id;
                            if (subcategories_id > 0)
                                categoryid = subcategories_id;

                            int categoryCount = connDest.ExecuteScalar<int>("select count(*) from Category where ID= " + categoryid + "",null, transaction);


                            if (categoryCount>0)
                            {
                                int posttypeid = postType.ID;

                                long titleContentID = GetNextTextContentID();
                                long metatagcontentid = GetNextTextContentID();
                                long descriptionContentId = GetNextTextContentID();


                                PPost pPost = new PPost();
                                pPost.Audio = audio;
                                pPost.CategoryID = category_id;
                                pPost.CreatedBy = createdby;
                                pPost.CreatedOn = CreatedOn;
                                pPost.DescriptionContentID = descriptionContentId;
                                pPost.EmbedVideo = embed_video;
                                pPost.ID = id;
                                pPost.ImageBig = image_big;
                                pPost.ImageSmall = image_small;
                                pPost.ISActive = isactive;
                                pPost.ISFeature = (short)is_feature;
                                pPost.ISSchedulePost = (short)schedule_post;
                                pPost.ISShowRightColumn = (short)show_right_column;
                                pPost.ISSlider = (short)is_slider;
                                pPost.ISSliderLeft = (short)slider_left;
                                pPost.ISSliderRight = (short)slider_right;
                                pPost.ISTrending = (short)is_trending;
                                pPost.ISVideoGallery = (short)is_videoGallery;
                                pPost.MetaTagContentID = metatagcontentid;
                                pPost.PostTypeID = posttypeid;
                                pPost.RssImage = rss_image;
                                pPost.SchedulePostDate = schedule_post_date == DateTime.MinValue ? null : schedule_post_date;
                                pPost.Slug = slug.Replace(" ", "-").Replace("'", "");
                                if (is_pending == 1)
                                    pPost.StatusID = (int)EStatus.Pending;
                                if (is_draft == 1)
                                    pPost.StatusID = (int)EStatus.Draft;
                                if (is_pending != 1 && is_draft != 1)
                                    pPost.StatusID = (int)EStatus.Approved;

                                if (subcategories_id > 0)
                                {
                                    pPost.CategoryID = subcategories_id;
                                }

                                //pPost.SubCategoryID = subcategories_id;
                                pPost.Tags = tags;
                                pPost.TitleContentID = titleContentID;
                                pPost.UpdatedOn = UpdatedOn == DateTime.MinValue ? null : UpdatedOn;
                                pPost.Video = video;

                                connDest.Insert<PPost>(pPost, transaction);


                                foreach (var item in pLanguages)
                                {
                                    PTranslation pTranslation = new PTranslation();
                                    pTranslation.LanguageID = item.ID;
                                    pTranslation.TextContentID = titleContentID;
                                    pTranslation.Translation = title;
                                    pTranslation.RecordID = id;

                                    connDest.Insert<PTranslation>(pTranslation, transaction);


                                    PTranslation pTranslation2 = new PTranslation();
                                    pTranslation2.LanguageID = item.ID;
                                    pTranslation2.TextContentID = metatagcontentid;
                                    pTranslation2.Translation = meta_tag;
                                    pTranslation2.RecordID = id;

                                    connDest.Insert<PTranslation>(pTranslation2, transaction);


                                    PTranslation pTranslation3 = new PTranslation();
                                    pTranslation3.LanguageID = item.ID;
                                    pTranslation3.TextContentID = descriptionContentId;
                                    pTranslation3.Translation = description;
                                    pTranslation3.RecordID = id;

                                    connDest.Insert<PTranslation>(pTranslation3, transaction);
                                }


                                string fileName = "";
                                if (!string.IsNullOrWhiteSpace(video))
                                {
                                    fileName = video;
                                }
                                else if (!string.IsNullOrWhiteSpace(audio))
                                {
                                    fileName = audio;
                                }
                                else
                                {
                                    fileName = image_big;
                                }

                                PAsset pAsset = new PAsset();
                                pAsset.ActualName = fileName;
                                pAsset.ContentType = MimeMapping.GetMimeMapping(fileName);
                                pAsset.CreatedBy = createdby;
                                pAsset.CreatedOn = CreatedOn.Value;
                                pAsset.ID = GetNextBusinessID();
                                pAsset.ISActive = 1;
                                pAsset.ParentID = id;
                                pAsset.ParentTypeID = (int)APIDBHelper.ParentType.Post;
                                pAsset.UniqueName = fileName;

                                connDest.Insert<PAsset>(pAsset,transaction);
                            }
                        }
                    }

                    transaction.Commit();
                }
            }



        }
        #endregion


        public static long InsertUpdateAsset(PAsset pAsset)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                if (pAsset.ID == 0)
                {
                    pAsset.ID = DBHelper.GetNextTextContentID();
                    conn.Insert<PAsset>(pAsset);
                }
                else
                {
                    conn.Update<PAsset>(pAsset);
                }
            }
            return pAsset.ID;
        }


        #region Advertisements

        // Advertisement


        public static long InsertUpdateAdvertisment(PAdvertisment pAdvertisment)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                if (pAdvertisment.ISNew == 1)
                {
                    pAdvertisment.ID = DBHelper.GetNextTextContentID();
                    conn.Insert<PAdvertisment>(pAdvertisment);
                }
                else
                {
                    conn.Update<PAdvertisment>(pAdvertisment);
                }
            }

            return pAdvertisment.ID;
        }

        public static List<PAdvertisment> GetAllAdvertisements()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return conn.GetAll<PAdvertisment>().ToList();
            }
        }

        public static PAdvertisment GetAdvertisementByPlacement(string placementName)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select top 1  Advertisment.ID from Advertisment inner join AdvertismentPlacement on PlacementAreaID = AdvertismentPlacement.ID where AdvertismentPlacement.PlacementAreaName = '" + placementName + "'  and Advertisment.IsActive=1";
                int advertismentID = Convert.ToInt32(conn.ExecuteScalar(query));
                return GetAdvertisementByAdvertismentID(advertismentID);
            }
        }

        public static PAdvertisment GetAdvertisementByAdvertismentID(int advertismentID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return conn.Get<PAdvertisment>(advertismentID);
            }
        }

        public static PAdvertisment GetAdvertisementByPlacementID(int placementId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select top 1  Advertisment.ID from Advertisment where Advertisment.PlacementAreaID = " + placementId + " and Advertisment.IsActive=1";
                int advertismentID = Convert.ToInt32(conn.ExecuteScalar(query));
                return GetAdvertisementByAdvertismentID(advertismentID);
            }
        }

        public static bool DeleteAdvertisment(int advertismentId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PAdvertisment pAdvertisment = GetAdvertisementByAdvertismentID(advertismentId);
                pAdvertisment.ISActive = 0;
                return conn.Update<PAdvertisment>(pAdvertisment);
            }
        }
        #endregion


        // Post Start

        #region Posts

        public static List<PPost> GetActivePosts()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PPost> lst = conn.GetAll<PPost>().ToList();
                lst = lst.Where(i => i.ISActive == 1).ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PPost pPost = item;
                    pPost = SetTranlationPostForBackend(pPost);
                }
                return lst;
            }
        }

        public static List<PPost> GetAllPosts()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PPost> lst = conn.GetAll<PPost>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PPost pPost = item;
                    pPost = SetTranlationPostForBackend(pPost);
                }

                return lst;
            }
        }

        public static List<PPost> GetAllPostsByLanguageID(int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PPost> lst = conn.GetAll<PPost>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }
        }

        public static PPost GetPostById(long id, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                PPost pPost = conn.Get<PPost>(id);
                List<PTranslation> TranslationData = GetPTranslations(pPost.ID, languageId);

                // Title
                pPost.TitleData = TranslationData.Where(i => i.TextContentID == pPost.TitleContentID).ToList();

                if (languageId == 0)
                {
                    pPost.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pPost.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pPost.TitleData != null && pPost.TitleData.Count > 0)
                {
                    pPost.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pPost.TitleContentID).FirstOrDefault().Translation;
                }

                // Description
                pPost.DescriptionData = TranslationData.Where(i => i.TextContentID == pPost.DescriptionContentID).ToList();

                if (languageId == 0)
                {
                    pPost.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pPost.DescriptionContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pPost.DescriptionData != null && pPost.DescriptionData.Count > 0)
                {
                    pPost.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pPost.DescriptionContentID).FirstOrDefault().Translation;
                }


                return pPost;
            }
        }

        public static int CanAddPost(string slug, long PostID = 0)
        {
            int canAdd = 1;
            //search for existing title urls
            using (var conn = GetConnectionDest)
            {
                if (PostID == 0)
                {
                    string query = "select ID from Post where rtrim(ltrim(Slug)) =  '" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingPostId = conn.ExecuteScalar<long>(query);
                    if (exsitingPostId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from Post where rtrim(ltrim(Slug)) =  '" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingPostId = conn.ExecuteScalar<long>(query);
                    if (exsitingPostId > 0 && exsitingPostId != PostID)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertPost(PPost pPost, List<PTranslation> titles, List<PTranslation> descriptions, List<PTranslation> metatags)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                long postID = GetNextTextContentID();
                pPost.ID = postID;

                long titleContentId = GetNextTextContentID();
                pPost.TitleContentID = titleContentId;

                long descriptionContentId = GetNextTextContentID();
                pPost.DescriptionContentID = descriptionContentId;

                long metaTagsContentId = GetNextTextContentID();
                pPost.MetaTagContentID = metaTagsContentId;

                conn.Insert<PPost>(pPost);


                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pPost.ID;

                    conn.Insert<PTranslation>(pTranslation);

                    // description
                    PTranslation pTranslationDesc = descriptions.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationDesc == null)
                        pTranslationDesc = new PTranslation();

                    pTranslationDesc.TextContentID = descriptionContentId;
                    pTranslationDesc.LanguageID = item.ID;
                    pTranslationDesc.RecordID = pPost.ID;

                    conn.Insert<PTranslation>(pTranslationDesc);

                    // metatags
                    PTranslation pTranslationMetaTags = metatags.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationMetaTags == null)
                        pTranslationMetaTags = new PTranslation();

                    pTranslationMetaTags.TextContentID = metaTagsContentId;
                    pTranslationMetaTags.LanguageID = item.ID;
                    pTranslationMetaTags.RecordID = pPost.ID;

                    conn.Insert<PTranslation>(pTranslationMetaTags);
                }

                return pPost.ID;
            }
        }

        public static bool UpdatePost(PPost pPost)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PPost>(pPost);

                foreach (var item in pPost.TitleData)
                {
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pPost.DescriptionData)
                {
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool DeletePost(long PostId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PPost Post = GetPostById(PostId);
                Post.ISActive = 0;
                return conn.Update<PPost>(Post);
            }
        }


        public static List<PPost> GetPostsForBackEnd(long userId = 0, long category = 0, long subCategory = 0,
            int status = 0, int isFeature = 0, int isSlider = 0, int isSliderLeft = 0, int isSliderRight = 0, int isTrending = 0, int ISSchedulePost = 0, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = " select * from [dbo].[Post] where 1=1 ";
                if (userId > 0)
                    query = query + " and CreatedBy = " + userId + " ";
                if (category > 0)
                    query = query + " and CategoryID = " + category + " ";
                if (subCategory > 0)
                    query = query + " and SubCategoryID = " + subCategory + " ";
                if (status > 0)
                    query = query + " and StatusID = " + status + " ";
                if (isFeature > 0)
                    query = query + " and ISFeature = " + isFeature + " ";
                if (isSlider > 0)
                    query = query + " and ISSlider = " + isSlider + " ";
                if (isSliderLeft > 0)
                    query = query + " and ISSliderLeft = " + isSliderLeft + " ";
                if (isSliderRight > 0)
                    query = query + " and ISSliderRight = " + isSliderRight + " ";
                if (isTrending > 0)
                    query = query + " and ISTrending = " + isTrending + " ";
                if (ISSchedulePost > 0)
                    query = query + " and ISSchedulePost = " + ISSchedulePost + " ";

                List<PPost> lst = conn.Query<PPost>(query).ToList();

                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();
                    item.DescriptionData = TranslationData.Where(i => i.TextContentID == item.DescriptionContentID).ToList();
                    item.MetaTagData = TranslationData.Where(i => i.TextContentID == item.MetaTagContentID).ToList();

                    string categoryName = GetCategoryById(item.CategoryID, languageId).DefaultTitleToDisplay;
                    item.CategoryName = categoryName;

                    item.PostType = conn.ExecuteScalar<string>("select PostType from PostType where ID = " + item.PostTypeID + "");

                    PPost post = item;
                    post = SetTranlationPostForBackend(post);

                }

                return lst;
            }
        }

        public static PPost SetTranlationPostForBackend(PPost item)
        {

            int languageId = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
            // Title
            PTranslation pTranslation = item.TitleData.Where(i => i.LanguageID == languageId && i.TextContentID == item.TitleContentID).FirstOrDefault();
            if (pTranslation != null && !string.IsNullOrEmpty(pTranslation.Translation))
            {
                item.DefaultTitleToDisplay = pTranslation.Translation;
                item.Language = pTranslation.Language;
            }
            else
            {
                PTranslation pTranslation1 = item.TitleData.Where(i => !string.IsNullOrEmpty(i.Translation) && i.TextContentID == item.TitleContentID).FirstOrDefault();
                if (pTranslation1 != null && !string.IsNullOrEmpty(pTranslation1.Translation))
                {
                    item.DefaultTitleToDisplay = pTranslation1.Translation;
                    item.Language = pTranslation1.Language;
                }
            }

            // Description
            PTranslation pTranslation_Desc = item.DescriptionData.Where(i => i.LanguageID == languageId && i.TextContentID == item.DescriptionContentID).FirstOrDefault();
            if (pTranslation_Desc != null && !string.IsNullOrEmpty(pTranslation_Desc.Translation))
            {
                item.DefaultDescriptionToDisplay = pTranslation_Desc.Translation;
            }
            else
            {
                PTranslation pTranslation_Desc1 = item.DescriptionData.Where(i => !string.IsNullOrEmpty(i.Translation) && i.TextContentID == item.DescriptionContentID).FirstOrDefault();
                if (pTranslation_Desc1 != null && !string.IsNullOrEmpty(pTranslation_Desc1.Translation))
                {
                    item.DefaultDescriptionToDisplay = pTranslation_Desc1.Translation;
                }
            }

            // MetaTag
            PTranslation pTranslation_Meta = item.MetaTagData.Where(i => i.LanguageID == languageId && i.TextContentID == item.MetaTagContentID).FirstOrDefault();
            if (pTranslation_Meta != null && !string.IsNullOrEmpty(pTranslation_Meta.Translation))
            {
                item.DefaultMetaTagToDisplay = pTranslation_Meta.Translation;
            }
            else
            {
                PTranslation pTranslation_Meta1 = item.MetaTagData.Where(i => !string.IsNullOrEmpty(i.Translation) && i.TextContentID == item.MetaTagContentID).FirstOrDefault();
                if (pTranslation_Meta1 != null && !string.IsNullOrEmpty(pTranslation_Meta1.Translation))
                {
                    item.DefaultMetaTagToDisplay = pTranslation_Meta1.Translation;
                }
            }

            return item;
        }

        public static PPost GetPostDetailById(int postId, int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PPost post = conn.Get<PPost>(postId);
                ////post.pTranslations = GetPTranslations(postId, languageId);
                return post;
            }
        }

        public static PPost GetPostById(int postId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return conn.Get<PPost>(postId);
            }
        }

        public static List<PTranslation> GetPTranslations(long recordId, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select TextContentID, LanguageID, Translation, RecordID,Name as Language " +
                    " from Translation inner join Language on Language.ID = Translation.LanguageID where RecordID = " + recordId + " and 1=1 ";

                if (languageId > 0)
                    query = query + " and Translation.LanguageID = " + languageId + " ";
                return conn.Query<PTranslation>(query).ToList();
            }
        }

        public static string GetPTranslationByLangId(long recordId, int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select TextContentID, LanguageID, Translation, RecordID,Name as Language " +
                    " from Translation inner join Language on Language.ID = Translation.LanguageID where RecordID = " + recordId + " and 1=1 ";

                if (languageId > 0)
                    query = query + " and Translation.LanguageID = " + languageId + " ";
                List<PTranslation> pTranslations = conn.Query<PTranslation>(query).ToList();
                if (pTranslations.Count > 0)
                    return pTranslations[0].Translation;
                else
                    return "";
            }
        }

        // Post End 
        #endregion



        // Category

        #region Categories

        public static List<PCategory> GetAllCategories()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PCategory> lst = conn.GetAll<PCategory>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PCategory pCategory = item;
                    pCategory = SetTranlationCategoryForBackend(pCategory);
                }

                return lst;
            }
        }

        public static List<PCategory> GetAllCategoriesByLanguageID(int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PCategory> lst = conn.GetAll<PCategory>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }
        }

        public static PCategory SetTranlationCategoryForBackend(PCategory item)
        {

            int languageId = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
            // Title
            PTranslation pTranslation = item.TitleData.Where(i => i.LanguageID == languageId && i.TextContentID == item.TitleContentID).FirstOrDefault();
            if (pTranslation != null && !string.IsNullOrEmpty(pTranslation.Translation))
            {
                item.DefaultTitleToDisplay = pTranslation.Translation;
                item.Language = pTranslation.Language;
            }
            else
            {
                PTranslation pTranslation1 = item.TitleData.Where(i => !string.IsNullOrEmpty(i.Translation) && i.TextContentID == item.TitleContentID).FirstOrDefault();
                if (pTranslation1 != null && !string.IsNullOrEmpty(pTranslation1.Translation))
                {
                    item.DefaultTitleToDisplay = pTranslation1.Translation;
                    item.Language = pTranslation1.Language;
                }
            }
            return item;
        }

        public static string GetExistingCategoryOrders()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return string.Join(",", conn.GetAll<PCategory>().ToList().OrderBy(i => i.CategoryOrder).Select(i => i.CategoryOrder.ToString()).ToList());
            }
        }

        public static PCategory GetCategoryById(long id, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                PCategory pCategory = conn.Get<PCategory>(id);
                if (pCategory != null)
                {
                    List<PTranslation> TranslationData = GetPTranslations(pCategory.ID, languageId);
                    pCategory.TitleData = TranslationData.Where(i => i.TextContentID == pCategory.TitleContentID).ToList();

                    if (languageId == 0)
                    {
                        pCategory.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pCategory.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                    }
                    else if (pCategory.TitleData != null && pCategory.TitleData.Count > 0)
                    {
                        pCategory.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pCategory.TitleContentID).FirstOrDefault().Translation;
                    }
                }
                return pCategory;
            }
        }

        public static List<PCategory> GetSubCategories(long categoryId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select * from Category where ParentCategoryID = " + categoryId + "";
                return conn.Query<PCategory>(query).ToList();
            }
        }

        public static int CanAddCategory(string slug, long categoryID = 0)
        {
            int canAdd = 1;
            //search for existing title urls
            using (var conn = GetConnectionDest)
            {
                if (categoryID == 0)
                {
                    string query = "select ID from Category where rtrim(ltrim(Slug)) =  '" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingCategoryId = conn.ExecuteScalar<long>(query);
                    if (exsitingCategoryId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from Category where rtrim(ltrim(Slug)) =  '" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingCategoryId = conn.ExecuteScalar<long>(query);
                    if (exsitingCategoryId > 0 && exsitingCategoryId != categoryID)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertCategory(PCategory pCategory, List<PTranslation> titles, List<PTranslation> slugs)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                long titleContentId = GetNextTextContentID();
                pCategory.TitleContentID = titleContentId;

                long categoryId = conn.Insert<PCategory>(pCategory);


                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = categoryId;

                    conn.Insert<PTranslation>(pTranslation);
                }

                return categoryId;
            }
        }

        public static bool UpdateCategory(PCategory pCategory)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PCategory>(pCategory);

                foreach (var item in pCategory.TitleData)
                {
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool DeleteCategory(long categoryId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PCategory category = GetCategoryById(categoryId);
                category.ISActive = 0;
                return conn.Update<PCategory>(category);
            }
        }

        #endregion


        // CustomPage

        #region CustomPages

        public static List<PCustomPage> GetActiveCustomePages()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PCustomPage> lst = conn.GetAll<PCustomPage>().ToList();
                lst = lst.Where(i => i.ISActive == 1).ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PCustomPage pCustomPage = item;
                    pCustomPage = SetTranlationCustomPageForBackend(pCustomPage);
                }
                return lst;
            }
        }

        public static List<PCustomPage> GetAllCustomePages()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PCustomPage> lst = conn.GetAll<PCustomPage>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PCustomPage pCustomPage = item;
                    pCustomPage = SetTranlationCustomPageForBackend(pCustomPage);
                }

                return lst;
            }
        }

        public static List<PCustomPage> GetAllCustomPagesByLanguageID(int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PCustomPage> lst = conn.GetAll<PCustomPage>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }
        }

        public static PCustomPage SetTranlationCustomPageForBackend(PCustomPage item)
        {

            int languageId = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
            // Title
            PTranslation pTranslation = item.TitleData.Where(i => i.LanguageID == languageId && i.TextContentID == item.TitleContentID).FirstOrDefault();
            if (pTranslation != null && !string.IsNullOrEmpty(pTranslation.Translation))
            {
                item.DefaultTitleToDisplay = pTranslation.Translation;
                item.Language = pTranslation.Language;
            }
            else
            {
                PTranslation pTranslation1 = item.TitleData.Where(i => !string.IsNullOrEmpty(i.Translation) && i.TextContentID == item.TitleContentID).FirstOrDefault();
                if (pTranslation1 != null && !string.IsNullOrEmpty(pTranslation1.Translation))
                {
                    item.DefaultTitleToDisplay = pTranslation1.Translation;
                    item.Language = pTranslation1.Language;
                }
            }
            return item;
        }

        public static PCustomPage GetCustomPageById(long id, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                PCustomPage pCustomPage = conn.Get<PCustomPage>(id);
                List<PTranslation> TranslationData = GetPTranslations(pCustomPage.ID, languageId);

                // Title
                pCustomPage.TitleData = TranslationData.Where(i => i.TextContentID == pCustomPage.TitleContentID).ToList();

                if (languageId == 0)
                {
                    pCustomPage.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pCustomPage.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pCustomPage.TitleData != null && pCustomPage.TitleData.Count > 0)
                {
                    pCustomPage.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pCustomPage.TitleContentID).FirstOrDefault().Translation;
                }

                // Description
                pCustomPage.DescriptionData = TranslationData.Where(i => i.TextContentID == pCustomPage.DescriptionContentID).ToList();

                if (languageId == 0)
                {
                    pCustomPage.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pCustomPage.DescriptionContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pCustomPage.DescriptionData != null && pCustomPage.DescriptionData.Count > 0)
                {
                    pCustomPage.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pCustomPage.DescriptionContentID).FirstOrDefault().Translation;
                }


                return pCustomPage;
            }
        }



        public static int CanAddCustomPage(string slug, long CustomPageID = 0)
        {
            int canAdd = 1;
            //search for existing title urls
            using (var conn = GetConnectionDest)
            {
                if (CustomPageID == 0)
                {
                    string query = "select ID from CustomPage where rtrim(ltrim(Slug)) =  '" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingCustomPageId = conn.ExecuteScalar<long>(query);
                    if (exsitingCustomPageId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from CustomPage where rtrim(ltrim(Slug)) =  '" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingCustomPageId = conn.ExecuteScalar<long>(query);
                    if (exsitingCustomPageId > 0 && exsitingCustomPageId != CustomPageID)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertCustomPage(PCustomPage pCustomPage, List<PTranslation> titles, List<PTranslation> descriptions)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                long titleContentId = GetNextTextContentID();
                pCustomPage.TitleContentID = titleContentId;


                long descriptionContentId = GetNextTextContentID();
                pCustomPage.DescriptionContentID = descriptionContentId;

                long CustomPageId = conn.Insert<PCustomPage>(pCustomPage);


                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = CustomPageId;

                    conn.Insert<PTranslation>(pTranslation);

                    // description
                    PTranslation pTranslationDesc = descriptions.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationDesc == null)
                        pTranslationDesc = new PTranslation();

                    pTranslationDesc.TextContentID = descriptionContentId;
                    pTranslationDesc.LanguageID = item.ID;
                    pTranslationDesc.RecordID = CustomPageId;

                    conn.Insert<PTranslation>(pTranslationDesc);
                }

                return CustomPageId;
            }
        }

        public static bool UpdateCustomPage(PCustomPage pCustomPage)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PCustomPage>(pCustomPage);

                foreach (var item in pCustomPage.TitleData)
                {
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pCustomPage.DescriptionData)
                {
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool DeleteCustomPage(long CustomPageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PCustomPage CustomPage = GetCustomPageById(CustomPageId);
                CustomPage.ISActive = 0;
                return conn.Update<PCustomPage>(CustomPage);
            }
        }

        #endregion

        // CustomPage End 


        #region Advertisement Placement
        public static PAdvertismentPlacement GetPlacementID(long id)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PAdvertismentPlacement pAdvertismentPlacement = conn.Get<PAdvertismentPlacement>(id);
                return pAdvertismentPlacement;
            }
        }

        public static int CanAddPlacement(string areaname, long id = 0)
        {
            int canAdd = 1;
            //search for existing title urls
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                if (id == 0)
                {
                    string query = "select ID from AdvertismentPlacement where rtrim(ltrim(PlacementAreaName)) =  '" + (areaname == null ? "" : areaname).Trim() + "'";
                    long exsitingPlacementId = conn.ExecuteScalar<long>(query);
                    if (exsitingPlacementId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from AdvertismentPlacement where rtrim(ltrim(PlacementAreaName)) =  '" + (areaname == null ? "" : areaname).Trim() + "'";
                    long exsitingPlacementId = conn.ExecuteScalar<long>(query);
                    if (exsitingPlacementId > 0 && exsitingPlacementId != id)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertUpdateAdvertismentPlacement(PAdvertismentPlacement pAdvertismentPlacement)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                if (pAdvertismentPlacement.ID == 0)
                {
                    pAdvertismentPlacement.ID = GetNextBusinessID();
                    return conn.Insert<PAdvertismentPlacement>(pAdvertismentPlacement);
                }
                else
                {
                    conn.Update<PAdvertismentPlacement>(pAdvertismentPlacement);
                    return pAdvertismentPlacement.ID;
                }
            }
        }

        public static List<PAdvertismentPlacement> GetAllAdvertisePlacements()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return conn.GetAll<PAdvertismentPlacement>().ToList();
            }
        }

        public bool DeletePlacement(int placementId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PAdvertisment pAdvertisment = GetAdvertisementByPlacementID(placementId);
                if (pAdvertisment != null && pAdvertisment.ISActive == 1)
                {
                    throw new Exception("Placement can not be delete. As it has active advertisment");
                }
                else
                {
                    return conn.Update<PAdvertisment>(pAdvertisment);
                }
            }
        }

        #endregion


        // Employee


        public static List<PEmployee> GetEmployeeByEmailPassword(string email, string password)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "SELECT * FROM [dbo].[Employee] where Email = '" + email + "' and Password = '" + password + "'";
                return conn.Query<PEmployee>(query).ToList();
            }
        }

        public static DataSet GetDashboardData()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("exec [Proc_ManageDashboard] 'GetDashboard'", conn);
                dataAdapter.Fill(ds);
                return ds;
            }

        }

        public static List<PLanguage> GetAllLanguages()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return conn.GetAll<PLanguage>().ToList();
            }
        }

    }
}



