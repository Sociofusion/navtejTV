using navtezcms.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using navtezcms.BO;
using System.Web.Http.Cors;

namespace navtezcms.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class NavtejController : ApiController
    {

        [System.Web.Http.Route("api/Navtej/getSettings")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        public response getSettings()
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.GetSettings());
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/getLanguages")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        public response getLanguages()
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.GetAllLanguages());
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/getHeaderMenu")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getHeaderMenu(int languageId)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = APIDBHelper.GetFrontHeaderMenu(languageId);
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/getAdvertisment")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getAdvertisment(string placementname)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.GetAdvertismentByPlacementAreaName(placementname));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/GetFrontFooterMenu")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getFrontFooterMenu(int languageId)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.GetFrontFooterMenu(languageId));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/getPostById")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getPostById(long postId, int languageId, string slug)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.GetDetailByPostId(postId, languageId, slug));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/getPostBySlug")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getPostBySlug(int languageId, string slug)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.GetDetailByPostSlug(languageId, slug));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/getPosts")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getPosts(string categorySlug, int languageId, int offset, int itemcount, long? categoryId = null, int? isFeature = 0,
            int? isSlider = 0,
            int? isSliderLeft = 0,
            int? isSliderRight = 0,
            int? isTrending = 0)
        {
            response objResponse = new response();
            try
            {
                int totalRecordCount = 0;
                List<PPost> pPosts = APIDBHelper.GetPostsForFrontEnd(out totalRecordCount, categoryId.Value, isFeature.Value,
                    isSlider.Value, isSliderLeft.Value, isSliderRight.Value, isTrending.Value, languageId, offset, itemcount, categorySlug);
                objResponse.LastCounter = offset + pPosts.Count;
                objResponse.TotalRecords = totalRecordCount;
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(pPosts);
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }


        [System.Web.Http.Route("api/Navtej/GetSearchContent")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response GetSearchContent(int languageId, int offset, int itemcount, string searchKeyword)
        {
            response objResponse = new response();
            try
            {
                int totalRecordCount = 0;
                List<PPost> pPosts = APIDBHelper.GetSearchContent(out totalRecordCount, languageId, offset, itemcount, searchKeyword);
                objResponse.LastCounter = offset + pPosts.Count;
                objResponse.TotalRecords = totalRecordCount;
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(pPosts);
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }


        [System.Web.Http.Route("api/Navtej/getParentCategories")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getParentCategories(int languageId)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.GetAllParentCategories(languageId));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/insertNewsletterEmail")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        [ApiAuthorize]
        public response insertNewsletterEmail([FromBody] PNewsletterSub newletterdata)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.InsertIntoNewsLetter(newletterdata));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/getRelatedNews")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getPosts(int languageId, int offset, int itemcount, long postId)
        {
            response objResponse = new response();
            try
            {
                int totalRecordCount = 0;
                List<PPost> pPosts = APIDBHelper.GetRelatedNews(postId, out totalRecordCount, languageId, offset, itemcount);
                objResponse.LastCounter = offset + pPosts.Count;
                objResponse.TotalRecords = totalRecordCount;
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(pPosts);
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/insertContactUs")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        [ApiAuthorize]
        public response insertContactUs([FromBody] PContactUs contactUs)
        {
            response objResponse = new response();
            try
            {
                if (!string.IsNullOrWhiteSpace(contactUs.Email) && !string.IsNullOrWhiteSpace(contactUs.Mobile))
                {
                    objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.InsertIntoContactUs(contactUs));
                }
                else
                {
                    objResponse.success = false;
                    objResponse.messages.Add("Email and Mobile are required field");
                }            
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Navtej/getCustomPage")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ApiAuthorize]
        public response getCustomPage(int languageId, string slug)
        {
            response objResponse = new response();
            try
            {
                PCustomPage pCustomPage = APIDBHelper.GetCustomPageBySlug(slug, languageId);
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(pCustomPage);
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }
    }
}