using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Services;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ProviderInfoModel;
using ProviderInfoService.SFEntService;
using enterprise = ProviderInfoService.SFEntService;




namespace ProviderInfoService.Respository
{
    public class SFDocumentsRepository
    {
        private  SoapClient login; // for login endpoint
        private  SoapClient client; // for API endpoint
        private  SessionHeader header;
        private  EndpointAddress endpoint;
        private string sessionId;
        private string serverUrl;
        private LimitInfo[] liHeader;
        private SFDocument sfDocument;
        private const string token = "FvB0EAGuhj82hYkQIQE09LWtQ";
        private LoginResult lr;

        public SFDocumentsRepository(SFDocument document)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client = new SoapClient("Soap");
            sfDocument = document;
            try
            {
                lr = client.login(null, document.User.Username, document.User.Password+token);
                
                sessionId = lr.sessionId;
                serverUrl = lr.serverUrl;
                header = new SessionHeader();
                header.sessionId = lr.sessionId;

            }
            catch (Exception e)
            {
                //we can either log it or deal with the error here and retry
            }
        }

        public bool UploadDocument()
        {






            endpoint = new EndpointAddress(serverUrl);
            header.sessionId = sessionId;
            bool success;
            using (client = new SoapClient("Soap", endpoint))
            {

                QueryResult qr = null;
                QueryOptions qo = new QueryOptions();
                qo.batchSize = 250;
                qo.batchSizeSpecified = true;

                Folder rF = null;

                client.query(header, qo, null, null,
                    "SELECT Id FROM Folder WHERE Name='BDDocuments' AND Type = 'Document'",
                    out qr);


                if (qr.size > 0)
                {
                    
                    rF = (Folder)qr.records[0];
                }
                UserAppInfo userAppInfo = new UserAppInfo();
                
                Document doc = new Document();
                doc.FolderId = lr.userInfo.userId;
                doc.ContentType = ".pdf";
                doc.IsPublic = false;
                doc.DeveloperName = "BDDocuments";
                doc.Name = sfDocument.FileName;//check for null reference here.
                doc.Type = "pdf";

                doc.Body = sfDocument.Document;

                sObject[] docs = new Document[1];
                docs[0] = doc;


                SaveResult[] sResults;

                client.create(header,null,null,null,null,null,null,null,null,null,null,null,docs, out liHeader,out sResults);

                if (sResults[0].success) // we can also loop through results if we have multiple documents created.
                {
                    success = true;
                }
                else
                {
                    success = false;
                    //log the reason why it was not successful which can then be retreived with another service call
                }

            }
            //logout();
            return success;
        }


        private void logout()
        {
            client.logout(header);
        }


    }
}
