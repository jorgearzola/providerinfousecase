using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using ProviderInfoModel;
using ProviderInfoService.Business;

namespace ProviderInfoApi.Modules
{
    public class SFDocuments : NancyModule
    {
        public SFDocuments()
        {
            Get["/get/"] = _ =>
            {
                return Response.AsJson("{'msg':'this end point is working'}");
            };

            Post["/upload/"] = parameters =>
            {
                try
                {
                    

                    //retrieve all the files uploaded.  NancyFx specific
                    var data = Request.Files.FirstOrDefault();

                    //we can add code here to validate the contents of the file to make sure that it is a valid file type or size
                    var document = new SFDocument();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        data.Value.CopyTo(ms);
                        document.Document = ms.ToArray();
                    }

                    //use the Model object to transport data between layers
                    document.FileName = data.Name;
                    document.Stamp = DateTime.Now;
                    document.Type = data.ContentType;
                    document.User = new SFUser() {Username = "jarzola@gmail.com", Password = "Wu?ewr5Teb" }; //user/pass can also be passed in with the session and authenticated as such
                    //we can implement authentication / authorization token check here.
                    /*
                        if(!auth())
                        {
                            var response = new SfDocumentResponse(e.Message);
                            return Response.AsJson(response);
                        } 

                     */

                    //create the business object that will handle the processing of the file.
                    SFDocumentBusiness context = new SFDocumentBusiness(document);
                    var uploaded = context.UploadDocument();
                    var response = new SfDocumentResponse(uploaded ? "File Uploaded Successfully." : "File Upload Failed.");
                    return Response.AsJson(response);
                }
                catch (Exception e)
                {
                    //Add logging
                    var response = new SfDocumentResponse(e.Message);
                    return Response.AsJson(response);
                }
            };
        }
    }
}