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

namespace navtezcms.api.Controllers
{
    public class ParamdhamController : ApiController
    {
        [System.Web.Http.Route("api/Paramdham/getLanguages")]
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

        [System.Web.Http.Route("api/Paramdham/getHeaderMenu")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ParamApiAuthorize]
        public response getHeaderMenu(int languageId)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(APIDBHelper.GetFrontHeaderMenu(languageId));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Paramdham/getAdvertisment")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ParamApiAuthorize]
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

        [System.Web.Http.Route("api/Paramdham/GetFrontFooterMenu")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ParamApiAuthorize]
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

        [System.Web.Http.Route("api/Paramdham/getPostById")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ParamApiAuthorize]
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

        [System.Web.Http.Route("api/Paramdham/getPosts")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ParamApiAuthorize]
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


        [System.Web.Http.Route("api/Paramdham/GetSearchContent")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ParamApiAuthorize]
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


        [System.Web.Http.Route("api/Paramdham/getParentCategories")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ParamApiAuthorize]
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

        [System.Web.Http.Route("api/Paramdham/insertNewsletterEmail")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        [ParamApiAuthorize]
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

        [System.Web.Http.Route("api/Paramdham/getRelatedNews")]
        [System.Web.Mvc.HttpGet]
        [JsonOutput]
        [ParamApiAuthorize]
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


        [System.Web.Http.Route("api/Paramdham/CanUseCustomerEmail")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        public response CanUseCustomerEmail(string emailId, int customerId)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(ParamAPIDBHelper.CanUseCustomerEmail(emailId, customerId));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }


        [System.Web.Http.Route("api/Paramdham/InsertCustomer")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        public response InsertCustomer([FromBody] PCustomer customer)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(ParamAPIDBHelper.InsertUpdateCustomer(customer));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Paramdham/UpdateCustomer")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        public response UpdateCustomer([FromBody] PCustomer customer)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(ParamAPIDBHelper.InsertUpdateCustomer(customer));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Paramdham/initLogin")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        public response initLogin(string email, string password)
        {
            response objResponse = new response();
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    throw new Exception("Parameters are missing or blank");
                }

                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(ParamAPIDBHelper.AuthenticateCustomer(email, password, HttpContext.Current.Request.UserHostAddress, ""));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }


        [System.Web.Http.Route("api/Paramdham/GetEvents")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        public response GetEvents(int languageId, int offset, int itemcount)
        {
            response objResponse = new response();
            int totalRecordCount = 0;
            try
            {
                List<PEvent> events = ParamAPIDBHelper.GetEvents(out totalRecordCount, languageId, offset, itemcount);
                objResponse.LastCounter = offset + events.Count;
                objResponse.TotalRecords = totalRecordCount;
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(events);
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Paramdham/GetCustomerById")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        [ParamApiAuthorize]
        public response GetCustomerById(int id)
        {
            response objResponse = new response();
            try
            {
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(ParamAPIDBHelper.GetCustomerById(id));
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }


        [System.Web.Http.Route("api/Paramdham/GetCustomerOrders")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        [ParamApiAuthorize]
        public response GetCustomerOrders(int customerId, int languageId, int offset, int itemcount)
        {
            response objResponse = new response();
            try
            {
                int totalRecordCount = 0;
                List<POrder> orders = ParamAPIDBHelper.GetCustomerOrders(out totalRecordCount, customerId, languageId, offset, itemcount);
                objResponse.LastCounter = offset + orders.Count;
                objResponse.TotalRecords = totalRecordCount;
                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(orders);
            }
            catch (Exception ex)
            {
                objResponse.success = false;
                objResponse.messages.Add(ex.Message);
            }
            return objResponse;
        }

        [System.Web.Http.Route("api/Paramdham/UpdateCustomerPassword")]
        [System.Web.Mvc.HttpPost]
        [JsonOutput]
        public response UpdateCustomerPassword(int customerId, string password)
        {
            response objResponse = new response();
            try
            {

                objResponse.payload = Newtonsoft.Json.JsonConvert.SerializeObject(ParamAPIDBHelper.UpdateCustomerPassword(customerId, password));

                return objResponse;
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