using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using navtezcms.BO;
using navtezcms.DAL;


namespace navtezcms.adminw.Admin
{
    public partial class menubuilder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoryParent();
            }
        }


        public void BindCategoryParent()
        {
            List<PCategory> lst = DBHelper.GetAllParentCategories();
            string CategoryIDStr = String.Join(",", lst.Select(x => x.ID).ToArray());
            hdnCategoryIDStr.Value = CategoryIDStr;

            string OrderStr = String.Join(",", lst.Select(x => x.CategoryOrder).ToArray());
            hdnOrderStr.Value = OrderStr;

            rptCategory.DataSource = lst;
            rptCategory.DataBind();
        }

        protected void btnSaveBuilder_Click(object sender, EventArgs e)
        {
            string CategoryStr = hdnCategoryIDStr.Value;
            string OrderStr = hdnOrderStr.Value;

            int[] arr_CategoryStr = CategoryStr.Split(',').Select(int.Parse).ToArray();
            int[] arr_OrderStr = OrderStr.Split(',').Select(int.Parse).ToArray();

            Array.Sort(arr_OrderStr);

            for (int i = 0; i < arr_CategoryStr.Length; i++)
            {
                PCategory pCategory = new PCategory();
                pCategory.ID = arr_CategoryStr[i];
                pCategory.CategoryOrder = arr_OrderStr[i];
                DBHelper.UpdateCategoryOrder(pCategory);
            }
            BindCategoryParent();
            setMenuJson();
        }

        public void setMenuJson()
        {
            List<PLanguage> lstLanguage = DBHelper.GetAllLanguages();
            foreach (var objLanguage in lstLanguage)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(DBHelper.setMenuJson(objLanguage.ID));
                DBHelper.InsertUpdateMenuJson(json, objLanguage.ID);
            }
        }
    }
}