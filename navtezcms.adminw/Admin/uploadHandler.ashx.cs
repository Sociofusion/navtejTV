using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using navtezcms.DAL;
using navtezcms.BO;
using System.Web.SessionState;

namespace navtezcms.adminw.Admin
{
    /// <summary>
    /// Summary description for uploadHandler
    /// </summary>
    public class uploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection SelectedFiles = context.Request.Files;
                    int employeeID = Convert.ToInt32(context.Request.QueryString["employeeID"]);

                    string newFileName = "";
                    string newFileNameForMultiple = "";
                    for (int i = 0; i < SelectedFiles.Count; i++)
                    {
                        HttpPostedFile PostedFile = SelectedFiles[i];

                        // upload Gallery Images
                        string fileContentType = "";
                        AssetUploader assetUploader = new AssetUploader();
                        string uniqueName = assetUploader.UploadAsset(PostedFile.FileName, PostedFile, "post", out fileContentType);
                        if (string.IsNullOrEmpty(uniqueName))
                            throw new Exception("File is not uploaded");

                        PAsset pAsset = new PAsset();
                        pAsset.UniqueName = uniqueName;
                        pAsset.ActualName = System.IO.Path.GetFileName(PostedFile.FileName);
                        pAsset.ContentType = fileContentType;
                        pAsset.CreatedBy = employeeID;
                        pAsset.ParentTypeID = (int)Common.ParentType.Post;
                        pAsset.CreatedBy = employeeID;
                        pAsset.ParentID = 0;
                        pAsset.ISActive = 1;
                        pAsset.ISGallery = 1;

                        DBHelper.InsertUpdateAsset(pAsset);
                        newFileName = string.Join("", uniqueName.Split(Path.GetInvalidFileNameChars()));
                        newFileNameForMultiple += newFileName + "|";
                    }

                    if (SelectedFiles.Count > 1)
                    {
                        context.Response.Write(newFileNameForMultiple);
                    }
                    else
                    {
                        context.Response.Write(newFileName);
                    }
                    context.Response.ContentType = "text/html";
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Please Select Files");
                }
            }
            catch(Exception ex)
            {
                context.Response.Write(ex);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}