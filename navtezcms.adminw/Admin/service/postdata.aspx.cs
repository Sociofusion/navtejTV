using navtezcms.BO;
using navtezcms.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace navtezcms.adminw.Admin.service
{

    public partial class postdata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string requestType = Request.QueryString["requesttype"];

                HttpContext context = HttpContext.Current;
                context.Response.ContentType = "text/plain";
                string result = "";

                if (requestType.ToLower() == "getcategorybyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["catid"]);
                    result = GetCategoryById(id);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getblankobjforcategoryinsert")
                {
                    result = GetBlankObjectForCategoryInsert();
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "canaddcategory")
                {
                    long id = Convert.ToInt64(Request.Form["catid"]);
                    string titleUrlEnglish = Convert.ToString(Request.Form["titleurl"]);
                    result = CanAddCategory(id, titleUrlEnglish);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getexistingcategoryorders")
                {
                    result = GetExistingCategoryOrders();
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "canaddadverplacement")
                {
                    long id = 0;
                    if (!string.IsNullOrWhiteSpace(Request.Form["id"]))
                        id = Convert.ToInt64(Request.Form["id"]);
                    string areaname = Convert.ToString(Request.Form["areaname"]);
                    result = CanAddAdvertismentPlacement(id, areaname);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getplacementbyid")
                {
                    long id = 0;
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
                        id = Convert.ToInt64(Request.QueryString["id"]);
                    result = GetAdvertismentPlacement(id);
                    Response.Write(result);
                }
                // Custom page
                else if (requestType.ToLower() == "canaddcustompage")
                {
                    long id = Convert.ToInt64(Request.Form["custompageid"]);
                    string slug = Convert.ToString(Request.Form["slug"]);
                    result = CanAddCustomPage(id, slug);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getcustompagebyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["custompageid"]);
                    result = GetCustomPageByID(id);
                    Response.Write(result);
                }
                // Post
                else if (requestType.ToLower() == "canaddpost")
                {
                    long id = Convert.ToInt64(Request.Form["postid"]);
                    string slug = Convert.ToString(Request.Form["slug"]);
                    result = CanAddPost(id, slug);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getpostbyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["postid"]);
                    result = GetPostByID(id);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getsubcategorybyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["catid"]);
                    result = GetSubCategoryById(id);
                    Response.Write(result);
                }
                // Event
                else if (requestType.ToLower() == "geteventbyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["eventid"]);
                    result = GetEventByID(id);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "canaddevent")
                {
                    long id = Convert.ToInt64(Request.Form["eventid"]);
                    string slug = Convert.ToString(Request.Form["slug"]);
                    result = CanAddEvent(id, slug);
                    Response.Write(result);
                }
                //Menu
                else if (requestType.ToLower() == "getexistingmenuorders")
                {
                    result = GetExistingMenuOrders();
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getmenubyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["menuid"]);
                    result = GetMenuByID(id);
                    Response.Write(result);
                }
                // Language
                else if (requestType.ToLower() == "canaddlanguage")
                {
                    long id = Convert.ToInt64(Request.Form["languageid"]);
                    string name = Convert.ToString(Request.Form["name"]);
                    result = CanAddLanguage(id, name);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getlanguagebyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["languageid"]);
                    result = GetLanguageByID(id);
                    Response.Write(result);
                }
                // Employee
                else if (requestType.ToLower() == "getemployeebyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["empid"]);
                    result = GetEmployeeByID(id);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "updateparentorder")
                {
                    long id = Convert.ToInt64(Request.QueryString["empid"]);
                    result = GetEmployeeByID(id);
                    Response.Write(result);
                }
                // Donation
                else if (requestType.ToLower() == "getdonationeventbyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["donationeventid"]);
                    result = GetDonationEventByID(id);
                    Response.Write(result);
                }
                // Product category
                if (requestType.ToLower() == "getproductcategorybyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["catid"]);
                    result = GetProductCategoryById(id);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "canaddproductcategory")
                {
                    long id = Convert.ToInt64(Request.Form["catid"]);
                    string titleUrlEnglish = Convert.ToString(Request.Form["titleurl"]);
                    result = CanAddProductCategory(id, titleUrlEnglish);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getexistingproductcategoryorders")
                {
                    result = GetExistingProductCategoryOrders();
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "canaddproduct")
                {
                    long id = Convert.ToInt64(Request.Form["productid"]);
                    string slug = Convert.ToString(Request.Form["slug"]);
                    result = CanAddProduct(id, slug);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getproductbyid")
                {
                    long id = Convert.ToInt64(Request.QueryString["productid"]);
                    result = GetProductByID(id);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getadvertisement")
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    result = GetAdvertisementByID(id);
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "getassetsforgalleryonly")
                {
                    result = GetAssetsForGalleryOnly();
                    Response.Write(result);
                }
                else if (requestType.ToLower() == "deleteasset")
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    result = DeleteAsset(id);
                    Response.Write(result);
                }
            }
        }

        public string GetAdvertismentPlacement(long advPlacementId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetPlacementID(advPlacementId));
        }

        
        // Category
        public string GetCategoryById(long categoryId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetCategoryById(categoryId));
        }

        public string CanAddCategory(long categoryID, string titleForUrlEnglish)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.CanAddCategory(titleForUrlEnglish, categoryID));
        }

        public string GetExistingCategoryOrders()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetExistingCategoryOrders());
        }

        public string GetBlankObjectForCategoryInsert()
        {
            PCategory pCategory = new PCategory();
            List<PLanguage> languages = DBHelper.GetAllLanguages();
            pCategory.TitleData = new List<PTranslation>();
            PTranslation titleTrans = null;
            foreach (var item in languages)
            {
                titleTrans = new PTranslation();
                titleTrans.LanguageID = item.ID;
                pCategory.TitleData.Add(titleTrans);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(pCategory);
        }
        
        public string CanAddAdvertismentPlacement(long id, string area)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.CanAddPlacement(area, id));
        }

        // CustomPage

        public string CanAddCustomPage(long customPageID, string slug)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.CanAddCustomPage(slug, customPageID));
        }

        public string GetCustomPageByID(long customPageId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetCustomPageById(customPageId));
        }
        // Post
        public string CanAddPost(long postID, string slug)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.CanAddPost(slug, postID));
        }

        public string GetPostByID(long postID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetPostById(postID));
        }

        public string GetSubCategoryById(long categoryId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetSubCategories(categoryId));
        }

        // Event

        public string CanAddEvent(long eventID, string slug)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.CanAddEvent(slug, eventID));
        }

        public string GetEventByID(long eventID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetEventById(eventID));
        }

        // Menu
        public string GetExistingMenuOrders()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetExistingMenuOrders());
        }

        public string GetMenuByID(long menuID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetMenuById(menuID));
        }
        // Language
        public string CanAddLanguage(long languageID, string name)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.CanAddLanguage(name, languageID));
        }
        public string GetLanguageByID(long languageID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetLanguageById(languageID));
        }
        // Employee
        public string GetEmployeeByID(long employeeID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetEmployeeById(employeeID));
        }
        // Donation
        public string GetDonationEventByID(long donationEventID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetDonationEventById(donationEventID));
        }

        // Product Category
        public string GetProductCategoryById(long productCategoryId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetProductCategoryById(productCategoryId));
        }
        public string CanAddProductCategory(long categoryID, string titleForUrlEnglish)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.CanAddProductCategory(titleForUrlEnglish, categoryID));
        }
        public string GetExistingProductCategoryOrders()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetExistingProductCategoryOrders());
        }
        // Product
        public string CanAddProduct(long postID, string slug)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.CanAddProduct(slug, postID));
        }

        public string GetProductByID(long postID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetProductById(postID));
        }
        public string GetAdvertisementByID(int ID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetAdvertisementByAdvertismentID(ID));
        }
        public string GetAssetsForGalleryOnly()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.GetAssetsGallery(0, (int)Common.ParentType.Post));
        }
        public string DeleteAsset(int ID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.DeleteAssetByAssetID(ID));
        }
    } 
}
