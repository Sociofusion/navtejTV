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

        public enum ERole
        {
            admin = 1,
            customer = 2,
            moderator = 3,
            reporter = 5,
            superadmin = 6
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
                            pCategory.Slug = slug.Replace(" ", "-").Replace("'", "");
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

                            int categoryCount = connDest.ExecuteScalar<int>("select count(*) from Category where ID= " + categoryid + "", null, transaction);


                            if (categoryCount > 0)
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

                                connDest.Insert<PAsset>(pAsset, transaction);
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

        public static long DeleteAssetByParentID(long ParentID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "delete from Asset where ParentID = '" + ParentID + "'";
                Convert.ToInt32(conn.ExecuteScalar(query));
            }
            return ParentID;
        }

        public static long DeleteAssetGalleryByParentID(long ParentID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "delete from Asset where ParentID = '" + ParentID + "' and ISGallery = 1";
                Convert.ToInt32(conn.ExecuteScalar(query));
            }
            return ParentID;
        }


        public static long DeleteAssetByAssetID(long AssetID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "delete from Asset where ID = '" + AssetID + "'";
                Convert.ToInt32(conn.ExecuteScalar(query));
            }
            return AssetID;
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
            List<PAdvertismentPlacement> pAdvertismentPlacements = GetAllAdvertisePlacements();
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PAdvertisment> pAdvertisments = conn.GetAll<PAdvertisment>().ToList();
                foreach (var item in pAdvertisments)
                {
                    item.PlacementAreaName = pAdvertismentPlacements.Where(i => i.ID == item.PlacementAreaID).FirstOrDefault().PlacementAreaName;
                    if (item.AdType == "AdFromAsset")
                    {
                        List<PAsset> pAssets = GetAssets(item.ID, (int)APIDBHelper.ParentType.Advertisement);
                        if (pAssets != null && pAssets.Count > 0)
                        {
                            item.pAsset = pAssets[0];
                            item.AssetFullUrl = item.pAsset.AssetLiveUrl;
                        }
                    }
                }

                return pAdvertisments;
            }
        }

        public static PAdvertisment GetAdvertisementByPlacement(string placementName)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select top 1  Advertisment.ID from Advertisment inner join AdvertismentPlacement on PlacementAreaID = AdvertismentPlacement.ID where AdvertismentPlacement.PlacementAreaName = '" + placementName + "' and Advertisment.IsActive=1";
                int advertismentID = Convert.ToInt32(conn.ExecuteScalar(query));
                PAdvertisment pAdvertisment = GetAdvertisementByAdvertismentID(advertismentID);
                if (pAdvertisment != null && pAdvertisment.AdType == "AdFromAsset")
                {
                    List<PAsset> pAssets = GetAssets(pAdvertisment.ID, (int)APIDBHelper.ParentType.Advertisement);
                    if (pAssets != null && pAssets.Count > 0)
                    {
                        pAdvertisment.pAsset = pAssets[0];
                        pAdvertisment.AssetFullUrl = pAdvertisment.pAsset.AssetLiveUrl;
                    }
                }

                return pAdvertisment;
            }
        }

        public static PAdvertisment GetAdvertisementByAdvertismentID(int advertismentID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                PAdvertisment pAdvertisment = conn.Get<PAdvertisment>(advertismentID);
                if (pAdvertisment != null && pAdvertisment.AdType == "AdFromAsset")
                {
                    List<PAsset> pAssets = GetAssets(pAdvertisment.ID, (int)APIDBHelper.ParentType.Advertisement);
                    if (pAssets != null && pAssets.Count > 0)
                    {
                        pAdvertisment.pAsset = pAssets[0];
                        pAdvertisment.AssetFullUrl = pAdvertisment.pAsset.AssetLiveUrl;
                    }
                }

                return pAdvertisment;
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

        public static bool ActiveDeActiveAdvertisment(int advertismentId)
        {
            using (var conn = GetConnectionDest)
            {
                bool isUpdate = false;
                conn.Open();
                string query = "update [dbo].[Advertisment] set IsActive = case when IsActive= 0 then 1 else 0 end where ID = '" + advertismentId + "'";
                isUpdate = Convert.ToBoolean(conn.ExecuteScalar(query));
                return isUpdate;
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
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Post);
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
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Post);
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
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Post, languageId);
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
                List<PTranslation> TranslationData = GetPTranslations(pPost.ID, (int)APIDBHelper.ParentType.Post, languageId);

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

                // Meta Tags
                pPost.MetaTagData = TranslationData.Where(i => i.TextContentID == pPost.MetaTagContentID).ToList();

                if (languageId == 0)
                {
                    pPost.DefaultMetaTagToDisplay = TranslationData.Where(i => i.TextContentID == pPost.MetaTagContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pPost.MetaTagData != null && pPost.MetaTagData.Count > 0)
                {
                    pPost.DefaultMetaTagToDisplay = TranslationData.Where(i => i.TextContentID == pPost.MetaTagContentID).FirstOrDefault().Translation;
                }
                // Assets Images
                List<PAsset> pAssets = new List<PAsset>();
                if (pPost.ImageBigAssetID > 0)
                {
                    pAssets = GetAssetByID(pPost.ImageBigAssetID, (int)APIDBHelper.ParentType.Post);
                    if (pAssets != null && pAssets.Count > 0)
                    {
                        pPost.pAsset = pAssets[0];
                        pPost.AssetFullUrl = pPost.pAsset.AssetLiveUrl;
                    }
                }
                else
                {
                    pAssets = GetAssets(pPost.ID, (int)APIDBHelper.ParentType.Post);
                    if (pAssets != null && pAssets.Count > 0)
                    {
                        pPost.pAsset = pAssets[0];
                        pPost.AssetFullUrl = pPost.pAsset.AssetLiveUrl;
                    }
                }

                // Assets Gallery Images

                List<PAsset> pAssetsGallery = GetAssetsGallery(pPost.ID, (int)APIDBHelper.ParentType.Post);
                if (pAssetsGallery != null && pAssetsGallery.Count > 0)
                {
                    pPost.pAssetGallery = pAssetsGallery;
                }

                // CategoryName
                PCategory pCategory = GetCategoryById(pPost.CategoryID, languageId);
                string categoryName = "";
                if (pCategory != null)
                {
                    categoryName = GetCategoryById(pPost.CategoryID, languageId).DefaultTitleToDisplay;
                }
                pPost.CategoryName = categoryName;
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
                    string query = "select ID from Post where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingPostId = conn.ExecuteScalar<long>(query);
                    if (exsitingPostId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from Post where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
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
                    //title
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pPost.ID;
                    pTranslation.ParentTypeID = (int)APIDBHelper.ParentType.Post;

                    conn.Insert<PTranslation>(pTranslation);

                    // description
                    PTranslation pTranslationDesc = descriptions.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationDesc == null)
                        pTranslationDesc = new PTranslation();

                    pTranslationDesc.TextContentID = descriptionContentId;
                    pTranslationDesc.LanguageID = item.ID;
                    pTranslationDesc.RecordID = pPost.ID;
                    pTranslationDesc.ParentTypeID = (int)APIDBHelper.ParentType.Post;

                    conn.Insert<PTranslation>(pTranslationDesc);

                    // metatags
                    PTranslation pTranslationMetaTags = metatags.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationMetaTags == null)
                        pTranslationMetaTags = new PTranslation();

                    pTranslationMetaTags.TextContentID = metaTagsContentId;
                    pTranslationMetaTags.LanguageID = item.ID;
                    pTranslationMetaTags.RecordID = pPost.ID;
                    pTranslationMetaTags.ParentTypeID = (int)APIDBHelper.ParentType.Post;

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
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Post;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pPost.DescriptionData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Post;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pPost.MetaTagData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Post;
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool SetStatusPost(int PostID, int StatusID)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                string query = "Update Post Set StatusID = " + StatusID + " where ID = " + PostID + "";
                isUpdate = Convert.ToBoolean(conn.ExecuteScalar(query));
                return isUpdate;
            }
        }

        public static bool ActiveDeActivePost(int postID)
        {
            using (var conn = GetConnectionDest)
            {
                bool isUpdate = false;
                conn.Open();
                string query = "update [dbo].[Post] set IsActive = case when IsActive= 0 then 1 else 0 end where ID = '" + postID + "'";
                isUpdate = Convert.ToBoolean(conn.ExecuteScalar(query));
                return isUpdate;
            }
        }

        public static bool DeletePost(long PostId)
        {
            using (var conn = GetConnectionDest)
            {
                bool isUpdate = false;
                conn.Open();
                string query = @"delete from Translation where RecordID = " + PostId + "; delete from Asset where ParentID = " + PostId + "; delete from post where ID = " + PostId + " ";
                isUpdate = Convert.ToBoolean(conn.ExecuteScalar(query));
                return isUpdate;
            }
        }

        public static List<PPost> GetPostsForBackEnd(string startDate, string endDate, long userId = 0, long category = 0, long subCategory = 0,
            int status = 0, int isFeature = 0, int isSlider = 0, int isSliderLeft = 0, int isSliderRight = 0,
            int isTrending = 0, int ISSchedulePost = 0, int languageId = 0, bool ISShowAll = false)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = @" select P.*, (Select Name from Employee where ID = P.CreatedBy) as Author
                              from[dbo].[Post] as P  where CreatedOn >= '" + startDate + " 00:00:00" + "' " +
                              "AND CreatedOn <= '" + endDate + " 23:59:59" + "'";


                if (userId > 0 && ISShowAll == false)
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

                query = query + " ORDER BY CreatedON Desc";

                List<PPost> lst = conn.Query<PPost>(query).ToList();

                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Post, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();
                    item.DescriptionData = TranslationData.Where(i => i.TextContentID == item.DescriptionContentID).ToList();
                    item.MetaTagData = TranslationData.Where(i => i.TextContentID == item.MetaTagContentID).ToList();
                    if (item.CategoryID > 0)
                    {
                        string categoryName = GetCategoryById(item.CategoryID, languageId).DefaultTitleToDisplay;
                        item.CategoryName = categoryName;
                    }
                    else
                    {
                        item.CategoryName = "";
                    }
                    

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

        public static List<PTranslation> GetPTranslations(long recordId, long parentType, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select TextContentID, LanguageID, Translation, RecordID,Name as Language " +
                    " from Translation inner join Language on Language.ID = Translation.LanguageID where ParentTypeID=" + parentType + " and  RecordID = " + recordId + " and 1=1 ";

                if (languageId > 0)
                    query = query + " and Translation.LanguageID = " + languageId + " ";
                return conn.Query<PTranslation>(query).ToList();
            }
        }

        public static string GetPTranslationByLangId(long recordId, int parentType, int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select TextContentID, LanguageID, Translation, RecordID,Name as Language " +
                    " from Translation inner join Language on Language.ID = Translation.LanguageID where ParentTypeID = " + parentType + " and  RecordID = " + recordId + " and 1=1 ";

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

        public static List<PCategory> GetParentCategories(int languageID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PCategory> lst = conn.Query<PCategory>("select * from Category where ParentCategoryID=0 and ISActive =1 and CanShowAtHome= 1 order by CategoryOrder").ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Category, languageID);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }

        }

        public static List<PCategory> GetAllCategories()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PCategory> lst = conn.GetAll<PCategory>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Category);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PCategory pCategory = item;
                    pCategory = SetTranlationCategoryForBackend(pCategory);
                }

                return lst;
            }
        }

        public static List<PCategory> GetAllParentCategories()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select * from Category where ISActive = 1 and ParentCategoryID = 0 and CanShowAtHome = 1 ORDER BY CategoryOrder ASC";
                List<PCategory> lst = conn.Query<PCategory>(query).ToList();
                lst = lst.OrderBy(x => x.CategoryOrder).ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Category);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PCategory pCategory = item;
                    pCategory = SetTranlationCategoryForBackend(pCategory);
                }

                return lst;
            }
        }

        public static List<PCategory> GetCategoryByRoleID(int RoleID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = @"select * from Category where ID in 
                                (SELECT data FROM dbo.Function_Split1((SELECT CategoryIds FROM Role WHERE id = " + RoleID + "), ','))";
                List<PCategory> lst = conn.Query<PCategory>(query).ToList();
                int countParent = lst.Where(i => i.ParentCategoryID == 0).Count();
                if (countParent > 0)
                    lst = lst.Where(i => i.ParentCategoryID == 0).ToList();


                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Category);
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
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Category, languageId);
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
                    List<PTranslation> TranslationData = GetPTranslations(pCategory.ID, (int)APIDBHelper.ParentType.Category, languageId);
                    pCategory.TitleData = TranslationData.Where(i => i.TextContentID == pCategory.TitleContentID).ToList();

                    if (languageId == 0)
                    {
                        pCategory.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pCategory.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                    }
                    else if (pCategory.TitleData != null && pCategory.TitleData.Count > 0)
                    {
                        pCategory.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pCategory.TitleContentID).FirstOrDefault().Translation;
                    }

                    pCategory.DefaultLanguageID = defaultLangID;
                }

                return pCategory;
            }
        }

        public static List<PCategory> GetSubCategories(long categoryId, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select * from Category where ParentCategoryID = " + categoryId + "";
                List<PCategory> lst = conn.Query<PCategory>(query).ToList();
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Category, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (languageId == 0)
                    {
                        item.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == item.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                    }
                    else if (item.TitleData != null && item.TitleData.Count > 0)
                    {
                        item.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == item.TitleContentID).FirstOrDefault().Translation;
                    }

                }

                return lst;
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
                    string query = "select ID from Category where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingCategoryId = conn.ExecuteScalar<long>(query);
                    if (exsitingCategoryId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from Category where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingCategoryId = conn.ExecuteScalar<long>(query);
                    if (exsitingCategoryId > 0 && exsitingCategoryId != categoryID)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertCategory(PCategory pCategory, List<PTranslation> titles)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                long categoryID = GetNextTextContentID();
                pCategory.ID = categoryID;

                long titleContentId = GetNextTextContentID();
                pCategory.TitleContentID = titleContentId;

                conn.Insert<PCategory>(pCategory);

                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pCategory.ID;
                    pTranslation.ParentTypeID = (int)APIDBHelper.ParentType.Category;
                    conn.Insert<PTranslation>(pTranslation);
                }

                return pCategory.ID;
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
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Category;
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool UpdateCategoryOrder(PCategory pCategory)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                string query = "Update Category Set CategoryOrder = " + pCategory.CategoryOrder + " where ID = " + pCategory.ID + "";
                isUpdate = Convert.ToBoolean(conn.ExecuteScalar(query));
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
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.CustomPage);
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
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.CustomPage);
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
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.CustomPage, languageId);
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
                List<PTranslation> TranslationData = GetPTranslations(pCustomPage.ID, (int)APIDBHelper.ParentType.CustomPage, languageId);

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

                pCustomPage.DefaultLanguageID = defaultLangID;

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
                    string query = "select ID from CustomPage where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingCustomPageId = conn.ExecuteScalar<long>(query);
                    if (exsitingCustomPageId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from CustomPage where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
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

                long customPageId = GetNextTextContentID();
                pCustomPage.ID = customPageId;

                long titleContentId = GetNextTextContentID();
                pCustomPage.TitleContentID = titleContentId;


                long descriptionContentId = GetNextTextContentID();
                pCustomPage.DescriptionContentID = descriptionContentId;

                conn.Insert<PCustomPage>(pCustomPage);


                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pCustomPage.ID;
                    pTranslation.ParentTypeID = (int)APIDBHelper.ParentType.CustomPage;
                    conn.Insert<PTranslation>(pTranslation);

                    // description
                    PTranslation pTranslationDesc = descriptions.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationDesc == null)
                        pTranslationDesc = new PTranslation();

                    pTranslationDesc.TextContentID = descriptionContentId;
                    pTranslationDesc.LanguageID = item.ID;
                    pTranslationDesc.RecordID = pCustomPage.ID;
                    pTranslationDesc.ParentTypeID = (int)APIDBHelper.ParentType.CustomPage;
                    conn.Insert<PTranslation>(pTranslationDesc);
                }

                return pCustomPage.ID;
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
                    item.ParentTypeID = (int)APIDBHelper.ParentType.CustomPage;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pCustomPage.DescriptionData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.CustomPage;
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

        public static bool DeletePlacement(int placementId)
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
                    PAdvertismentPlacement pAdvertismentPlacement = GetPlacementID(placementId);
                    pAdvertismentPlacement.ISActive = 0;
                    return conn.Update<PAdvertismentPlacement>(pAdvertismentPlacement);
                }
            }
        }

        #endregion


        // Employee

        #region Employee
        public static List<PEmployee> GetEmployeeByEmailPassword(string email, string password)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "SELECT E.*,(Select RoleName from Role where ID = E.RoleID) as RoleName FROM [dbo].[Employee] as E where E.Email = '" + email + "' and E.Password = '" + password + "'";
                return conn.Query<PEmployee>(query).ToList();
            }
        }

        public static List<PEmployee> GetEmployee()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "SELECT E.*,(Select RoleName from Role where ID = E.RoleID) as RoleName FROM [dbo].[Employee] as E";
                List<PEmployee> lst = conn.Query<PEmployee>(query).ToList();
                return lst;
            }
        }

        public static PEmployee GetEmployeeById(long id)
        {
            using (var conn = GetConnectionDest)
            {
                PEmployee pEmployee = conn.Get<PEmployee>(id);
                return pEmployee;
            }
        }

        public static long InsertEmployee(PEmployee pEmployee)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                conn.Insert<PEmployee>(pEmployee);
                return pEmployee.ID;
            }
        }

        public static bool UpdateEmployee(PEmployee pEmployee)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PEmployee>(pEmployee);
                return isUpdate;
            }
        }

        public static bool DeleteEmployee(long EmployeeId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PEmployee Employee = GetEmployeeById(EmployeeId);
                Employee.ISActive = 0;
                return conn.Update<PEmployee>(Employee);
            }
        }

        #endregion

        // Employee

        // Event

        #region Events

        public static List<PEvent> GetActiveEvent()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PEvent> lst = conn.GetAll<PEvent>().ToList();
                lst = lst.Where(i => i.ISActive == 1).ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Event);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PEvent pEvent = item;
                    pEvent = SetTranlationEventForBackend(pEvent);


                    List<PAsset> pAssets = GetAssets(item.ID, (int)APIDBHelper.ParentType.Event);
                    if (pAssets != null && pAssets.Count > 0)
                    {
                        item.pAsset = pAssets[0];
                        item.AssetFullUrl = item.pAsset.AssetLiveUrl;
                    }

                }
                return lst;
            }
        }

        public static List<PEvent> GetAllEvent()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PEvent> lst = conn.GetAll<PEvent>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Event);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PEvent pEvent = item;
                    pEvent = SetTranlationEventForBackend(pEvent);


                    List<PAsset> pAssets = GetAssets(item.ID, (int)APIDBHelper.ParentType.Event);
                    if (pAssets != null && pAssets.Count > 0)
                    {
                        item.pAsset = pAssets[0];
                        item.AssetFullUrl = item.pAsset.AssetLiveUrl;
                    }

                }

                return lst;
            }
        }

        public static List<PEvent> GetAllEventsByLanguageID(int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PEvent> lst = conn.GetAll<PEvent>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Event, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }
        }

        public static PEvent SetTranlationEventForBackend(PEvent item)
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

        public static PEvent GetEventById(long id, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                PEvent pEvent = conn.Get<PEvent>(id);
                List<PTranslation> TranslationData = GetPTranslations(pEvent.ID, (int)APIDBHelper.ParentType.Event, languageId);

                // Title
                pEvent.TitleData = TranslationData.Where(i => i.TextContentID == pEvent.TitleContentID).ToList();

                if (languageId == 0)
                {
                    pEvent.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pEvent.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pEvent.TitleData != null && pEvent.TitleData.Count > 0)
                {
                    pEvent.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pEvent.TitleContentID).FirstOrDefault().Translation;
                }

                // Description
                pEvent.DescriptionData = TranslationData.Where(i => i.TextContentID == pEvent.DescriptionContentID).ToList();

                if (languageId == 0)
                {
                    pEvent.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pEvent.DescriptionContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pEvent.DescriptionData != null && pEvent.DescriptionData.Count > 0)
                {
                    pEvent.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pEvent.DescriptionContentID).FirstOrDefault().Translation;
                }

                // Event Address
                pEvent.EventAddressData = TranslationData.Where(i => i.TextContentID == pEvent.EventAddressContentID).ToList();

                if (languageId == 0)
                {
                    pEvent.DefaultEventAddressToDisplay = TranslationData.Where(i => i.TextContentID == pEvent.EventAddressContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pEvent.EventAddressData != null && pEvent.EventAddressData.Count > 0)
                {
                    pEvent.DefaultEventAddressToDisplay = TranslationData.Where(i => i.TextContentID == pEvent.EventAddressContentID).FirstOrDefault().Translation;
                }

                // ContactPName
                pEvent.ContactPNameData = TranslationData.Where(i => i.TextContentID == pEvent.ContactPNameContentID).ToList();

                if (languageId == 0)
                {
                    pEvent.DefaultContactPNameToDisplay = TranslationData.Where(i => i.TextContentID == pEvent.ContactPNameContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pEvent.ContactPNameData != null && pEvent.ContactPNameData.Count > 0)
                {
                    pEvent.DefaultContactPNameToDisplay = TranslationData.Where(i => i.TextContentID == pEvent.ContactPNameContentID).FirstOrDefault().Translation;
                }

                List<PAsset> pAssets = GetAssets(pEvent.ID, (int)APIDBHelper.ParentType.Event);
                if (pAssets != null && pAssets.Count > 0)
                {
                    pEvent.pAsset = pAssets[0];
                    pEvent.AssetFullUrl = pEvent.pAsset.AssetLiveUrl;
                }

                pEvent.DefaultLanguageID = defaultLangID;
                return pEvent;
            }
        }

        public static int CanAddEvent(string slug, long EventID = 0)
        {
            int canAdd = 1;
            //search for existing title urls
            using (var conn = GetConnectionDest)
            {
                if (EventID == 0)
                {
                    string query = "select ID from Event where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingEventId = conn.ExecuteScalar<long>(query);
                    if (exsitingEventId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from Event where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingEventId = conn.ExecuteScalar<long>(query);
                    if (exsitingEventId > 0 && exsitingEventId != EventID)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertEvent(PEvent pEvent, List<PTranslation> titles, List<PTranslation> descriptions, List<PTranslation> eventAddresses, List<PTranslation> contactPNames)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                long titleContentId = GetNextTextContentID();
                pEvent.TitleContentID = titleContentId;


                long descriptionContentId = GetNextTextContentID();
                pEvent.DescriptionContentID = descriptionContentId;

                long eventAddresContentId = GetNextTextContentID();
                pEvent.EventAddressContentID = eventAddresContentId;

                long contactPNameContentId = GetNextTextContentID();
                pEvent.ContactPNameContentID = contactPNameContentId;

                conn.Insert<PEvent>(pEvent);


                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    // title
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pEvent.ID;
                    pTranslation.ParentTypeID = (int)APIDBHelper.ParentType.Event;
                    conn.Insert<PTranslation>(pTranslation);

                    // description
                    PTranslation pTranslationDesc = descriptions.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationDesc == null)
                        pTranslationDesc = new PTranslation();

                    pTranslationDesc.TextContentID = descriptionContentId;
                    pTranslationDesc.LanguageID = item.ID;
                    pTranslationDesc.RecordID = pEvent.ID;
                    pTranslationDesc.ParentTypeID = (int)APIDBHelper.ParentType.Event;

                    conn.Insert<PTranslation>(pTranslationDesc);

                    // eventAddress
                    PTranslation pTranslationEventAddress = eventAddresses.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationEventAddress == null)
                        pTranslationEventAddress = new PTranslation();

                    pTranslationEventAddress.TextContentID = eventAddresContentId;
                    pTranslationEventAddress.LanguageID = item.ID;
                    pTranslationEventAddress.RecordID = pEvent.ID;
                    pTranslationEventAddress.ParentTypeID = (int)APIDBHelper.ParentType.Event;

                    conn.Insert<PTranslation>(pTranslationEventAddress);

                    // contact PName
                    PTranslation pTranslationContactPName = contactPNames.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationContactPName == null)
                        pTranslationContactPName = new PTranslation();

                    pTranslationContactPName.TextContentID = contactPNameContentId;
                    pTranslationContactPName.LanguageID = item.ID;
                    pTranslationContactPName.RecordID = pEvent.ID;
                    pTranslationContactPName.ParentTypeID = (int)APIDBHelper.ParentType.Event;

                    conn.Insert<PTranslation>(pTranslationContactPName);


                }

                return pEvent.ID;
            }
        }

        public static bool UpdateEvent(PEvent pEvent)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PEvent>(pEvent);

                foreach (var item in pEvent.TitleData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Event;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pEvent.DescriptionData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Event;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pEvent.EventAddressData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Event;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pEvent.ContactPNameData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Event;
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool DeleteEvent(long EventId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PEvent Event = GetEventById(EventId);
                Event.ISActive = 0;
                return conn.Update<PEvent>(Event);
            }
        }

        #endregion

        // Event End 

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

        // Language

        #region Language

        public static List<PLanguage> GetAllLanguages()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return conn.GetAll<PLanguage>().ToList();
            }
        }

        public static PLanguage GetLanguageById(long id)
        {
            using (var conn = GetConnectionDest)
            {
                PLanguage pLanguage = conn.Get<PLanguage>(id);
                return pLanguage;
            }
        }

        public static int CanAddLanguage(string name, long LanguageID = 0)
        {
            int canAdd = 1;
            //search for existing title urls
            using (var conn = GetConnectionDest)
            {
                if (LanguageID == 0)
                {
                    string query = "select ID from Language where rtrim(ltrim(Name)) =  N'" + (name == null ? "" : name).Trim() + "'";
                    long exsitingLanguageId = conn.ExecuteScalar<long>(query);
                    if (exsitingLanguageId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from Language where rtrim(ltrim(Name)) =  N'" + (name == null ? "" : name).Trim() + "'";
                    long exsitingLanguageId = conn.ExecuteScalar<long>(query);
                    if (exsitingLanguageId > 0 && exsitingLanguageId != LanguageID)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertLanguage(PLanguage pLanguage)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                conn.Insert<PLanguage>(pLanguage);
                return pLanguage.ID;
            }
        }

        public static bool UpdateLanguage(PLanguage pLanguage)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PLanguage>(pLanguage);

                return isUpdate;
            }
        }

        public static bool DeleteLanguage(long LanguageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PLanguage Language = GetLanguageById(LanguageId);
                Language.ISActive = 0;
                return conn.Update<PLanguage>(Language);
            }
        }

        #endregion


        #region Assets 

        public static List<PAsset> GetAssets(long recordid, int parentType)
        {
            var enumDisplayStatus = (APIDBHelper.ParentType)parentType;
            string stringValue = enumDisplayStatus.ToString().ToLower();
            using (var conn = GetConnectionDest)
            {
                string query = "select * from Asset where ParentTypeID =" + parentType + " and ParentID = " + recordid + "";
                List<PAsset> pAssets = conn.Query<PAsset>(query).ToList();
                foreach (var item in pAssets)
                {
                    item.ParentType = stringValue;
                }

                return pAssets;
            }
        }

        public static List<PAsset> GetAssetsGallery(long recordid, int parentType)
        {
            var enumDisplayStatus = (APIDBHelper.ParentType)parentType;
            string stringValue = enumDisplayStatus.ToString().ToLower();
            using (var conn = GetConnectionDest)
            {
                string query = "select * from Asset where ParentTypeID =" + parentType + " and ParentID = " + recordid + " and ISGallery = 1";
                List<PAsset> pAssets = conn.Query<PAsset>(query).ToList();
                foreach (var item in pAssets)
                {
                    item.ParentType = stringValue;
                }

                return pAssets;
            }
        }

        public static List<PAsset> GetAssetByID(long id, int parentType)
        {
            var enumDisplayStatus = (APIDBHelper.ParentType)parentType;
            string stringValue = enumDisplayStatus.ToString().ToLower();
            using (var conn = GetConnectionDest)
            {
                string query = "select * from Asset where ParentTypeID =" + parentType + " and ID = " + id + "";
                List<PAsset> pAssets = conn.Query<PAsset>(query).ToList();
                foreach (var item in pAssets)
                {
                    item.ParentType = stringValue;
                }

                return pAssets;
            }
        }


        #endregion



        // Donation

        #region Donation

        public static List<PDonation> GetAllDonation()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PDonation> lst = conn.GetAll<PDonation>().ToList();
                return lst;
            }
        }

        #endregion



        // Menu


        #region Manu

        public static List<PMenu> GetAllMenu()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                string query = "select M.*, (select ParentType from ParentType where ID = M.ParentTypeID) as ParentType from Menu as M where M.IsFooter = 0";
                List<PMenu> lst = conn.Query<PMenu>(query).ToList();

                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Menu);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.MenuTextContentID).ToList();

                    PMenu pMenu = item;
                    pMenu = SetTranlationMenuForBackend(pMenu);
                }

                return lst;
            }
        }

        public static List<PMenu> GetAllFooterMenu()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                string query = "select M.*, (select ParentType from ParentType where ID = M.ParentTypeID) as ParentType from Menu as M where IsFooter = 1 ";
                List<PMenu> lst = conn.Query<PMenu>(query).ToList();

                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Menu);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.MenuTextContentID).ToList();

                    PMenu pMenu = item;
                    pMenu = SetTranlationMenuForBackend(pMenu);
                }

                return lst;
            }
        }

        public static List<PMenu> GetAllMenuByLanguageID(int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PMenu> lst = conn.GetAll<PMenu>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Menu, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.MenuTextContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }
        }

        public static PMenu SetTranlationMenuForBackend(PMenu item)
        {

            int languageId = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
            // Title
            PTranslation pTranslation = item.TitleData.Where(i => i.LanguageID == languageId && i.TextContentID == item.MenuTextContentID).FirstOrDefault();
            if (pTranslation != null && !string.IsNullOrEmpty(pTranslation.Translation))
            {
                item.DefaultTitleToDisplay = pTranslation.Translation;
                item.Language = pTranslation.Language;
            }
            else
            {
                PTranslation pTranslation1 = item.TitleData.Where(i => !string.IsNullOrEmpty(i.Translation) && i.TextContentID == item.MenuTextContentID).FirstOrDefault();
                if (pTranslation1 != null && !string.IsNullOrEmpty(pTranslation1.Translation))
                {
                    item.DefaultTitleToDisplay = pTranslation1.Translation;
                    item.Language = pTranslation1.Language;
                }
            }
            return item;
        }

        public static string GetExistingMenuOrders()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return string.Join(",", conn.GetAll<PMenu>().ToList().OrderBy(i => i.MenuOrder).Select(i => i.MenuOrder.ToString()).ToList());
            }
        }

        public static PMenu GetMenuById(long id, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                PMenu pMenu = conn.Get<PMenu>(id);
                if (pMenu != null)
                {
                    List<PTranslation> TranslationData = GetPTranslations(pMenu.ID, (int)APIDBHelper.ParentType.Menu, languageId);
                    pMenu.TitleData = TranslationData.Where(i => i.TextContentID == pMenu.MenuTextContentID).ToList();

                    if (languageId == 0)
                    {
                        pMenu.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pMenu.MenuTextContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                    }
                    else if (pMenu.TitleData != null && pMenu.TitleData.Count > 0)
                    {
                        pMenu.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pMenu.MenuTextContentID).FirstOrDefault().Translation;
                    }
                    pMenu.DefaultLanguageID = defaultLangID;
                }

                return pMenu;
            }
        }

        public static long InsertMenu(PMenu pMenu, List<PTranslation> titles)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                long MenuID = GetNextTextContentID();
                pMenu.ID = MenuID;

                long titleContentId = GetNextTextContentID();
                pMenu.MenuTextContentID = titleContentId;

                conn.Insert<PMenu>(pMenu);

                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pMenu.ID;
                    pTranslation.ParentTypeID = (int)APIDBHelper.ParentType.Menu;
                    conn.Insert<PTranslation>(pTranslation);
                }

                return pMenu.ID;
            }
        }

        public static bool UpdateMenu(PMenu pMenu)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PMenu>(pMenu);

                foreach (var item in pMenu.TitleData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Menu;
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool DeleteMenu(long MenuId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PMenu Menu = GetMenuById(MenuId);
                Menu.ISActive = 0;
                return conn.Update<PMenu>(Menu);
            }
        }

        public static List<FrontHeaderMenu> setMenuJson(int langId)
        {
            List<FrontHeaderMenu> klst = new List<FrontHeaderMenu>();
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PMenu> pMenus = conn.Query<PMenu>("select * from Menu where ISFooter=0 and ISActive=1 and ParentMenuId=0 ").ToList();
                FrontHeaderMenu frontHeaderMenu = null;

                foreach (var item in pMenus)
                {
                    if (item.ParentTypeID == (int)APIDBHelper.ParentType.Category || item.ParentTypeID == (int)APIDBHelper.ParentType.CustomPage)
                    {
                        //load category
                        if (item.ParentTypeID == (int)APIDBHelper.ParentType.Category)
                            klst.AddRange(APIDBHelper.GetCategories(langId, 0, item.MenuOrder));
                    }
                    else
                    {
                        frontHeaderMenu = new FrontHeaderMenu();
                        frontHeaderMenu.MenuOrder = item.MenuOrder;
                        frontHeaderMenu.ID = item.ID;
                        frontHeaderMenu.ParentMenuID = item.ParentMenuID;
                        frontHeaderMenu.ParentTypeID = item.ParentTypeID;
                        frontHeaderMenu.MenuTitle = DBHelper.GetPTranslationByLangId(item.ID, (int)APIDBHelper.ParentType.Menu, langId);

                        if (frontHeaderMenu.ParentTypeID == (int)APIDBHelper.ParentType.Home)
                            frontHeaderMenu.Slug = "home";
                        else if (frontHeaderMenu.ParentTypeID == (int)APIDBHelper.ParentType.About)
                            frontHeaderMenu.Slug = "about";
                        else if (frontHeaderMenu.ParentTypeID == (int)APIDBHelper.ParentType.Contact)
                            frontHeaderMenu.Slug = "contact";
                        else if (frontHeaderMenu.ParentTypeID == (int)APIDBHelper.ParentType.News)
                            frontHeaderMenu.Slug = "news";
                        else if (frontHeaderMenu.ParentTypeID == (int)APIDBHelper.ParentType.Support)
                            frontHeaderMenu.Slug = "support";
                        else if (frontHeaderMenu.ParentTypeID == (int)APIDBHelper.ParentType.Features)
                            frontHeaderMenu.Slug = "features";
                        else if (frontHeaderMenu.ParentTypeID == (int)APIDBHelper.ParentType.Advertise)
                            frontHeaderMenu.Slug = "advertise";

                        klst.Add(frontHeaderMenu);
                    }
                }

            }

            return klst.OrderBy(i => i.MenuOrder).ToList();
        }

        #endregion


        // AdminMenu


        #region AdminManu

        public static List<PAdminMenu> GetAllAdminMenu()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                string query = "Exec [dbo].[Proc_ManageMenu] 'GetAll', 0";
                List<PAdminMenu> lst = conn.Query<PAdminMenu>(query).ToList();

                return lst;
            }
        }

        public static List<PAdminMenu> GetAdminMenuByMenuLevel()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                string query = "Exec [dbo].[Proc_ManageMenu] 'GetByMenuLevel', 0";
                List<PAdminMenu> lst = conn.Query<PAdminMenu>(query).ToList();

                return lst;
            }
        }

        public static List<PAdminMenu> GetAdminMenuByRoleID(int RoleID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                string query = "Exec [dbo].[Proc_ManageMenu] 'GetMenuByRoleID', '" + RoleID + "'";
                List<PAdminMenu> lst = conn.Query<PAdminMenu>(query).ToList();

                return lst;
            }
        }

        public static string GetExistingAdminMenuOrders()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return string.Join(",", conn.GetAll<PAdminMenu>().ToList().OrderBy(i => i.Position).Select(i => i.Position.ToString()).ToList());
            }
        }

        public static PAdminMenu GetAdminMenuById(long id)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PAdminMenu pAdminMenu = conn.Get<PAdminMenu>(id);
                return pAdminMenu;
            }
        }

        public static long InsertAdminMenu(PAdminMenu pAdminMenu)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                conn.Insert<PAdminMenu>(pAdminMenu);
                return pAdminMenu.ID;
            }
        }

        public static bool UpdateAdminMenu(PAdminMenu pAdminMenu)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PAdminMenu>(pAdminMenu);

                return isUpdate;
            }
        }

        public static bool DeleteAdminMenu(PAdminMenu pAdminMenu)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Delete<PAdminMenu>(pAdminMenu);

                return isUpdate;
            }
        }

        public static long IsActiveDeActiveAdminMenu(long AdminMenuId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "Exec [dbo].[Proc_ManageMenu] 'IsActive', '" + AdminMenuId + "'";
                Convert.ToInt32(conn.ExecuteScalar(query));
            }
            return AdminMenuId;
        }


        #endregion


        // Role

        #region Role

        public static List<PRole> GetRoles()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return conn.GetAll<PRole>().ToList();
            }
        }

        public static List<PRole> GetRolesById(int ID)
        {
            using (var conn = GetConnectionDest)
            {
                string query = "SELECT * FROM [dbo].[Role] where ID = '" + ID + "'";
                List<PRole> lst = conn.Query<PRole>(query).ToList();
                return lst;
            }
        }

        public static List<PCategory> GetRoleRightsCategory(string str, int ID)
        {
            using (var conn = GetConnectionDest)
            {
                string query = "exec [Proc_ManageRoleCategory] " + str + "," + ID;
                List<PCategory> lst = conn.Query<PCategory>(query).ToList();
                return lst;
            }
        }

        public static List<PAdminMenu> GetRoleRightsMenu(string str, int ID)
        {
            using (var conn = GetConnectionDest)
            {
                string query = "exec [Proc_ManageRoleMenu] " + str + "," + ID;
                List<PAdminMenu> lst = conn.Query<PAdminMenu>(query).ToList();
                return lst;
            }
        }

        public static int InsertRoles(PRole pRole)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                conn.Insert<PRole>(pRole);
                return pRole.ID;
            }
        }
        public static bool UpdateRoles(PRole pRole)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PRole>(pRole);
                return isUpdate;
            }
        }
        public static bool DeleteRoles(PRole pRole)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Delete<PRole>(pRole);
                return isUpdate;
            }
        }

        public static int IsActiveDeActiveRoles(int RoleID)
        {

            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "update [dbo].[Role] set IsActive = case when IsActive= 0 then 1 else 0 end where ID = '" + RoleID + "'";
                Convert.ToInt32(conn.ExecuteScalar(query));
            }
            return RoleID;
        }

        #endregion


        // Role


        // DonationEvent

        #region DonationEvents

        public static List<PDonationEvent> GetActiveDonationEvent()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PDonationEvent> lst = conn.GetAll<PDonationEvent>().ToList();
                lst = lst.Where(i => i.ISActive == 1).ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Donations);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentId).ToList();

                    PDonationEvent pDonationEvent = item;
                    pDonationEvent = SetTranlationDonationEventForBackend(pDonationEvent);
                }
                return lst;
            }
        }

        public static List<PDonationEvent> GetAllDonationEvent()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PDonationEvent> lst = conn.GetAll<PDonationEvent>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Donations);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentId).ToList();

                    PDonationEvent pDonationEvent = item;
                    pDonationEvent = SetTranlationDonationEventForBackend(pDonationEvent);
                }

                return lst;
            }
        }

        public static List<PDonationEvent> GetAllDonationEventsByLanguageID(int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PDonationEvent> lst = conn.GetAll<PDonationEvent>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Donations, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentId).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }
        }

        public static PDonationEvent SetTranlationDonationEventForBackend(PDonationEvent item)
        {

            int languageId = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
            // Title
            PTranslation pTranslation = item.TitleData.Where(i => i.LanguageID == languageId && i.TextContentID == item.TitleContentId).FirstOrDefault();
            if (pTranslation != null && !string.IsNullOrEmpty(pTranslation.Translation))
            {
                item.DefaultTitleToDisplay = pTranslation.Translation;
                item.Language = pTranslation.Language;
            }
            else
            {
                PTranslation pTranslation1 = item.TitleData.Where(i => !string.IsNullOrEmpty(i.Translation) && i.TextContentID == item.TitleContentId).FirstOrDefault();
                if (pTranslation1 != null && !string.IsNullOrEmpty(pTranslation1.Translation))
                {
                    item.DefaultTitleToDisplay = pTranslation1.Translation;
                    item.Language = pTranslation1.Language;
                }
            }
            return item;
        }

        public static PDonationEvent GetDonationEventById(long id, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                PDonationEvent pDonationEvent = conn.Get<PDonationEvent>(id);

                List<PTranslation> TranslationData = GetPTranslations(pDonationEvent.ID, (int)APIDBHelper.ParentType.Donations, languageId);

                // Title
                pDonationEvent.TitleData = TranslationData.Where(i => i.TextContentID == pDonationEvent.TitleContentId).ToList();

                if (languageId == 0)
                {
                    pDonationEvent.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pDonationEvent.TitleContentId && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pDonationEvent.TitleData != null && pDonationEvent.TitleData.Count > 0)
                {
                    pDonationEvent.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pDonationEvent.TitleContentId).FirstOrDefault().Translation;
                }

                // Description
                pDonationEvent.DescriptionData = TranslationData.Where(i => i.TextContentID == pDonationEvent.DescriptionContentID).ToList();

                if (languageId == 0)
                {
                    pDonationEvent.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pDonationEvent.DescriptionContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pDonationEvent.DescriptionData != null && pDonationEvent.DescriptionData.Count > 0)
                {
                    pDonationEvent.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pDonationEvent.DescriptionContentID).FirstOrDefault().Translation;
                }

                pDonationEvent.DefaultLanguageID = defaultLangID;
                return pDonationEvent;
            }
        }

        public static long InsertDonationEvent(PDonationEvent pDonationEvent, List<PTranslation> titles, List<PTranslation> descriptions)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                long titleContentId = GetNextTextContentID();
                pDonationEvent.TitleContentId = titleContentId;


                long descriptionContentId = GetNextTextContentID();
                pDonationEvent.DescriptionContentID = descriptionContentId;

                conn.Insert<PDonationEvent>(pDonationEvent);


                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pDonationEvent.ID;
                    pTranslation.ParentTypeID = (int)APIDBHelper.ParentType.Donations;
                    conn.Insert<PTranslation>(pTranslation);

                    // description
                    PTranslation pTranslationDesc = descriptions.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationDesc == null)
                        pTranslationDesc = new PTranslation();

                    pTranslationDesc.TextContentID = descriptionContentId;
                    pTranslationDesc.LanguageID = item.ID;
                    pTranslationDesc.RecordID = pDonationEvent.ID;
                    pTranslationDesc.ParentTypeID = (int)APIDBHelper.ParentType.Donations;
                    conn.Insert<PTranslation>(pTranslationDesc);
                }

                return pDonationEvent.ID;
            }
        }

        public static bool UpdateDonationEvent(PDonationEvent pDonationEvent)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PDonationEvent>(pDonationEvent);

                foreach (var item in pDonationEvent.TitleData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Donations;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pDonationEvent.DescriptionData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Donations;
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool DeleteDonationEvent(long DonationEventId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PDonationEvent DonationEvent = GetDonationEventById(DonationEventId);
                DonationEvent.ISActive = 0;
                return conn.Update<PDonationEvent>(DonationEvent);
            }
        }

        #endregion

        // DonationEvent End 




        // ParentType
        public static List<PParentType> GetParentType()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PParentType> lst = conn.GetAll<PParentType>().ToList();
                return lst;
            }
        }


        // Post Stored Procedure
        public static List<PPost> GetPostFilter()
        {
            using (var conn = GetConnectionDest)
            {
                string query = "exec [Proc_GetPost] '30'";
                List<PPost> lst = conn.Query<PPost>(query).ToList();
                return lst;
            }
        }




        // ProductCategory

        #region ProductProductCategories

        public static List<PProductCategory> GetParentProductCategories(int languageID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PProductCategory> lst = conn.Query<PProductCategory>("select * from ProductCategory where ParentCategoryID=0 and ISActive =1 and CanShowAtHome= 1 order by CategoryOrder").ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.ProductCategory, languageID);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }

        }

        public static List<PProductCategory> GetAllProductCategories()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PProductCategory> lst = conn.GetAll<PProductCategory>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.ProductCategory);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PProductCategory pProductCategory = item;
                    pProductCategory = SetTranlationProductCategoryForBackend(pProductCategory);
                }

                return lst;
            }
        }

        public static List<PProductCategory> GetAllParentProductCategories()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select * from ProductCategory ORDER BY CategoryOrder ASC";
                List<PProductCategory> lst = conn.Query<PProductCategory>(query).ToList();
                lst = lst.Where(i => i.ParentCategoryID == 0).ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.ProductCategory);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PProductCategory pProductCategory = item;
                    pProductCategory = SetTranlationProductCategoryForBackend(pProductCategory);
                }

                return lst;
            }
        }

        public static List<PProductCategory> GetProductCategoryByRoleID(int RoleID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = @"select * from ProductCategory where ID in 
                                (SELECT data FROM dbo.Function_Split1((SELECT ProductCategoryIds FROM Role WHERE id = " + RoleID + "), ','))";
                List<PProductCategory> lst = conn.Query<PProductCategory>(query).ToList();
                int countParent = lst.Where(i => i.ParentCategoryID == 0).Count();
                if (countParent > 0)
                    lst = lst.Where(i => i.ParentCategoryID == 0).ToList();


                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.ProductCategory);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PProductCategory pProductCategory = item;
                    pProductCategory = SetTranlationProductCategoryForBackend(pProductCategory);
                }

                return lst;
            }
        }

        public static List<PProductCategory> GetAllProductCategoriesByLanguageID(int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PProductCategory> lst = conn.GetAll<PProductCategory>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.ProductCategory, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }
        }

        public static PProductCategory SetTranlationProductCategoryForBackend(PProductCategory item)
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

        public static string GetExistingProductCategoryOrders()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                return string.Join(",", conn.GetAll<PProductCategory>().ToList().OrderBy(i => i.CategoryOrder).Select(i => i.CategoryOrder.ToString()).ToList());
            }
        }

        public static PProductCategory GetProductCategoryById(long id, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                PProductCategory pProductCategory = conn.Get<PProductCategory>(id);
                if (pProductCategory != null)
                {
                    List<PTranslation> TranslationData = GetPTranslations(pProductCategory.ID, (int)APIDBHelper.ParentType.ProductCategory, languageId);
                    pProductCategory.TitleData = TranslationData.Where(i => i.TextContentID == pProductCategory.TitleContentID).ToList();

                    if (languageId == 0)
                    {
                        pProductCategory.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pProductCategory.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                    }
                    else if (pProductCategory.TitleData != null && pProductCategory.TitleData.Count > 0)
                    {
                        pProductCategory.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pProductCategory.TitleContentID).FirstOrDefault().Translation;
                    }

                    pProductCategory.DefaultLanguageID = defaultLangID;
                }

                return pProductCategory;
            }
        }

        public static List<PProductCategory> GetSubProductCategories(long categoryId, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "select * from ProductCategory where ParentCategoryID = " + categoryId + "";
                List<PProductCategory> lst = conn.Query<PProductCategory>(query).ToList();
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.ProductCategory, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (languageId == 0)
                    {
                        item.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == item.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                    }
                    else if (item.TitleData != null && item.TitleData.Count > 0)
                    {
                        item.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == item.TitleContentID).FirstOrDefault().Translation;
                    }

                }

                return lst;
            }
        }

        public static int CanAddProductCategory(string slug, long categoryID = 0)
        {
            int canAdd = 1;
            //search for existing title urls
            using (var conn = GetConnectionDest)
            {
                if (categoryID == 0)
                {
                    string query = "select ID from ProductCategory where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingProductCategoryId = conn.ExecuteScalar<long>(query);
                    if (exsitingProductCategoryId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from ProductCategory where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingProductCategoryId = conn.ExecuteScalar<long>(query);
                    if (exsitingProductCategoryId > 0 && exsitingProductCategoryId != categoryID)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertProductCategory(PProductCategory pProductCategory, List<PTranslation> titles)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                long categoryID = GetNextTextContentID();
                pProductCategory.ID = categoryID;

                long titleContentId = GetNextTextContentID();
                pProductCategory.TitleContentID = titleContentId;

                conn.Insert<PProductCategory>(pProductCategory);

                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pProductCategory.ID;
                    pTranslation.ParentTypeID = (int)APIDBHelper.ParentType.ProductCategory;
                    conn.Insert<PTranslation>(pTranslation);
                }

                return pProductCategory.ID;
            }
        }

        public static bool UpdateProductCategory(PProductCategory pProductCategory)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PProductCategory>(pProductCategory);

                foreach (var item in pProductCategory.TitleData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.ProductCategory;
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool UpdateProductCategoryOrder(PProductCategory pProductCategory)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                string query = "Update ProductCategory Set CategoryOrder = " + pProductCategory.CategoryOrder + " where ID = " + pProductCategory.ID + "";
                isUpdate = Convert.ToBoolean(conn.ExecuteScalar(query));
                return isUpdate;
            }
        }

        public static bool DeleteProductCategory(long categoryId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PProductCategory category = GetProductCategoryById(categoryId);
                category.ISActive = 0;
                return conn.Update<PProductCategory>(category);
            }
        }

        #endregion

        // End ProductCategory

        public static void GetPostFilterDS()
        {
            using (var conn = GetConnectionDest)
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "exec [Proc_GetPost] '30'";
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataTable post = ds.Tables[0];
                    DataTable count = ds.Tables[1];
                    conn.Close();
                }
            }
        }

        public static List<PAsset> GetAssetsForMultipleRecords(string records, int parentType)
        {
            var enumDisplayStatus = (APIDBHelper.ParentType)parentType;
            string stringValue = enumDisplayStatus.ToString().ToLower();
            using (var conn = GetConnectionDest)
            {
                string query = "select * from Asset where ParentTypeID =" + parentType + " and ParentID in ( " + records + ") ";
                List<PAsset> pAssets = conn.Query<PAsset>(query).ToList();
                foreach (var item in pAssets)
                {
                    item.ParentType = stringValue;
                }

                return pAssets;
            }
        }

        public static long GetCategoryIDFromSlug(string categorySlug)
        {
            string query = "select ID from Category where rtrim(ltrim(Slug)) = N'" + categorySlug + "'";
            using (var conn = GetConnectionDest)
            {
                return conn.ExecuteScalar<long>(query);
            }
        }


        // Product Start

        #region Products

        public static List<PProduct> GetActiveProducts()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PProduct> lst = conn.GetAll<PProduct>().ToList();
                lst = lst.Where(i => i.ISActive == 1).ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Shop);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PProduct pProduct = item;
                    pProduct = SetTranlationProductForBackend(pProduct);
                }
                return lst;
            }
        }

        public static List<PProduct> GetAllProducts()
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PProduct> lst = conn.GetAll<PProduct>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Shop);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    PProduct pProduct = item;
                    pProduct = SetTranlationProductForBackend(pProduct);
                }

                return lst;
            }
        }

        public static List<PProduct> GetAllProductsByLanguageID(int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                List<PProduct> lst = conn.GetAll<PProduct>().ToList();
                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Shop, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();

                    if (item.TitleData != null && item.TitleData.Count > 0)
                        item.DefaultTitleToDisplay = item.TitleData[0].Translation;
                    else
                        item.DefaultTitleToDisplay = "";

                }

                return lst;
            }
        }

        public static PProduct GetProductById(long id, int languageId = 0)
        {
            using (var conn = GetConnectionDest)
            {
                int defaultLangID = GetAllLanguages().Where(i => i.ISDefault == 1).FirstOrDefault().ID;
                PProduct pProduct = conn.Get<PProduct>(id);
                List<PTranslation> TranslationData = GetPTranslations(pProduct.ID, (int)APIDBHelper.ParentType.Shop, languageId);

                // Title
                pProduct.TitleData = TranslationData.Where(i => i.TextContentID == pProduct.TitleContentID).ToList();

                if (languageId == 0)
                {
                    pProduct.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pProduct.TitleContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pProduct.TitleData != null && pProduct.TitleData.Count > 0)
                {
                    pProduct.DefaultTitleToDisplay = TranslationData.Where(i => i.TextContentID == pProduct.TitleContentID).FirstOrDefault().Translation;
                }

                // Description
                pProduct.DescriptionData = TranslationData.Where(i => i.TextContentID == pProduct.DescriptionContentID).ToList();

                if (languageId == 0)
                {
                    pProduct.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pProduct.DescriptionContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pProduct.DescriptionData != null && pProduct.DescriptionData.Count > 0)
                {
                    pProduct.DefaultDescriptionToDisplay = TranslationData.Where(i => i.TextContentID == pProduct.DescriptionContentID).FirstOrDefault().Translation;
                }

                // Return Policy
                pProduct.ReturnPolicyData = TranslationData.Where(i => i.TextContentID == pProduct.ReturnPolicyContentID).ToList();

                if (languageId == 0)
                {
                    pProduct.DefaultReturnPolicyToDisplay = TranslationData.Where(i => i.TextContentID == pProduct.ReturnPolicyContentID && i.LanguageID == defaultLangID).FirstOrDefault().Translation;
                }
                else if (pProduct.ReturnPolicyData != null && pProduct.ReturnPolicyData.Count > 0)
                {
                    pProduct.DefaultReturnPolicyToDisplay = TranslationData.Where(i => i.TextContentID == pProduct.ReturnPolicyContentID).FirstOrDefault().Translation;
                }

                // Assets Images

                List<PAsset> pAssets = GetAssets(pProduct.ID, (int)APIDBHelper.ParentType.Shop);
                if (pAssets != null && pAssets.Count > 0)
                {
                    pProduct.pAsset = pAssets[0];
                    pProduct.AssetFullUrl = pProduct.pAsset.AssetLiveUrl;
                }

                // Assets Gallery Images

                List<PAsset> pAssetsGallery = GetAssetsGallery(pProduct.ID, (int)APIDBHelper.ParentType.Shop);
                if (pAssetsGallery != null && pAssetsGallery.Count > 0)
                {
                    pProduct.pAssetGallery = pAssetsGallery;
                }

                // CategoryName
                string categoryName = GetProductCategoryById(pProduct.CategoryID, languageId).DefaultTitleToDisplay;
                pProduct.CategoryName = categoryName;

                return pProduct;
            }
        }

        public static int CanAddProduct(string slug, long ProductID = 0)
        {
            int canAdd = 1;
            //search for existing title urls
            using (var conn = GetConnectionDest)
            {
                if (ProductID == 0)
                {
                    string query = "select ID from Product where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingProductId = conn.ExecuteScalar<long>(query);
                    if (exsitingProductId > 0)
                        canAdd = 0;
                }
                else
                {
                    string query = "select ID from Product where rtrim(ltrim(Slug)) =  N'" + (slug == null ? "" : slug).Trim() + "'";
                    long exsitingProductId = conn.ExecuteScalar<long>(query);
                    if (exsitingProductId > 0 && exsitingProductId != ProductID)
                        canAdd = 0;
                }
            }

            return canAdd;
        }

        public static long InsertProduct(PProduct pProduct, List<PTranslation> titles, List<PTranslation> descriptions, List<PTranslation> returnpolicies)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();

                long titleContentId = GetNextTextContentID();
                pProduct.TitleContentID = titleContentId;

                long descriptionContentId = GetNextTextContentID();
                pProduct.DescriptionContentID = descriptionContentId;

                long returnPolicyContentId = GetNextTextContentID();
                pProduct.ReturnPolicyContentID = returnPolicyContentId;

                conn.Insert<PProduct>(pProduct);

                List<PLanguage> languages = GetAllLanguages();
                foreach (var item in languages)
                {
                    //title
                    PTranslation pTranslation = titles.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslation == null)
                        pTranslation = new PTranslation();

                    pTranslation.TextContentID = titleContentId;
                    pTranslation.LanguageID = item.ID;
                    pTranslation.RecordID = pProduct.ID;
                    pTranslation.ParentTypeID = (int)APIDBHelper.ParentType.Shop;

                    conn.Insert<PTranslation>(pTranslation);

                    // description
                    PTranslation pTranslationDesc = descriptions.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationDesc == null)
                        pTranslationDesc = new PTranslation();

                    pTranslationDesc.TextContentID = descriptionContentId;
                    pTranslationDesc.LanguageID = item.ID;
                    pTranslationDesc.RecordID = pProduct.ID;
                    pTranslationDesc.ParentTypeID = (int)APIDBHelper.ParentType.Shop;

                    conn.Insert<PTranslation>(pTranslationDesc);

                    // returnPolicy
                    PTranslation pTranslationReturnPolicy = returnpolicies.Where(i => i.LanguageID == item.ID).FirstOrDefault();
                    if (pTranslationReturnPolicy == null)
                        pTranslationReturnPolicy = new PTranslation();

                    pTranslationReturnPolicy.TextContentID = returnPolicyContentId;
                    pTranslationReturnPolicy.LanguageID = item.ID;
                    pTranslationReturnPolicy.RecordID = pProduct.ID;
                    pTranslationReturnPolicy.ParentTypeID = (int)APIDBHelper.ParentType.Shop;

                    conn.Insert<PTranslation>(pTranslationReturnPolicy);
                }

                return pProduct.ID;
            }
        }

        public static bool UpdateProduct(PProduct pProduct)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PProduct>(pProduct);

                foreach (var item in pProduct.TitleData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Shop;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pProduct.DescriptionData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Shop;
                    conn.Update<PTranslation>(item);
                }

                foreach (var item in pProduct.ReturnPolicyData)
                {
                    item.ParentTypeID = (int)APIDBHelper.ParentType.Shop;
                    conn.Update<PTranslation>(item);
                }

                return isUpdate;
            }
        }

        public static bool DeleteProduct(long ProductId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PProduct Product = GetProductById(ProductId);
                Product.ISActive = 0;
                return conn.Update<PProduct>(Product);
            }
        }


        public static List<PProduct> GetProductsForBackEnd(DateTime startDate, DateTime endDate, long userId = 0, long category = 0,
            int isActive = 0, int isCOD = 0, int languageId = 0, bool ISShowAll = false)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = @" select P.*, (Select Name from Employee where ID = P.CreatedBy) as Author
                              from[dbo].[Product] as P  where CreatedDate >= '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00" + "' " +
                              "AND CreatedDate <= '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59" + "'";


                if (userId > 0 && ISShowAll == false)
                    query = query + " and CreatedBy = " + userId + " ";
                if (category > 0)
                    query = query + " and CategoryID = " + category + " ";
                if (isActive > 0)
                    query = query + " and ISActive = " + isActive + " ";
                if (isCOD > 0)
                    query = query + " and ISCOD = " + isCOD + " ";

                query = query + " ORDER BY CreatedDate Desc";

                List<PProduct> lst = conn.Query<PProduct>(query).ToList();

                foreach (var item in lst)
                {
                    List<PTranslation> TranslationData = GetPTranslations(item.ID, (int)APIDBHelper.ParentType.Shop, languageId);
                    item.TitleData = TranslationData.Where(i => i.TextContentID == item.TitleContentID).ToList();
                    item.DescriptionData = TranslationData.Where(i => i.TextContentID == item.DescriptionContentID).ToList();

                    if (item.CategoryID > 0)
                    {
                        string categoryName = GetProductCategoryById(item.CategoryID, languageId).DefaultTitleToDisplay;
                        item.CategoryName = categoryName;
                    }
                    else
                    {
                        item.CategoryName = "";
                    }

                    PProduct post = item;
                    post = SetTranlationProductForBackend(post);

                }

                return lst;
            }
        }

        public static PProduct SetTranlationProductForBackend(PProduct item)
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


            return item;
        }

        public static PProduct GetProductDetailById(int postId, int languageId)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                PProduct post = conn.Get<PProduct>(postId);
                return post;
            }
        }

        // Product End 
        #endregion



        #region ------------------------------- Setting -----------------------------------

        public static List<PSetting> GetSetting()
        {
            using (var conn = GetConnectionDest)
            {
                List<PSetting> lst = conn.GetAll<PSetting>().ToList();
                return lst;
            }
        }


        public static bool UpdateSetting(PSetting pSetting)
        {
            bool isUpdate = false;
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                isUpdate = conn.Update<PSetting>(pSetting);
                return isUpdate;
            }
        }

        #endregion Setting


        #region ------------------------------- Json Menu -----------------------------------
        public static int InsertUpdateMenuJson(string json, int LanguageID)
        {
            using (var conn = GetConnectionDest)
            {
                conn.Open();
                string query = "EXEC [Proc_AddEditMenuJson] N'" + json + "', '" + LanguageID + "'";
                Convert.ToInt32(conn.ExecuteScalar(query));
            }
            return LanguageID;
        }
        #endregion Json Menu
    }
}



