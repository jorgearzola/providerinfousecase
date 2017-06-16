using System;
using System.Net;
using System.ServiceModel;
using ProviderInfoModel;
using ProviderInfoService.SFEntService;




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

        /// <summary>
        /// Constructor will initialize a session that will be used to upload files in the Sf Document object
        /// </summary>
        /// <param name="document"></param>
        public SFDocumentsRepository(SFDocument document)
        {
            //set TLS version 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //create a soap client passing in the configuration name
            client = new SoapClient("Soap");
            sfDocument = document;
            try
            {

                //create a login session
                lr = client.login(null, sfDocument.User.Username, sfDocument.User.Password+token);

                sessionId = lr.sessionId;
                serverUrl = lr.serverUrl;

                //set endpoint url
                endpoint = new EndpointAddress(serverUrl);


                //reassign the client with the endpoint url.
                client = new SoapClient("Soap", endpoint);

                //create a session
                header = new SessionHeader();
                header.sessionId = sessionId;

            }
            catch (Exception e)
            {
                //we can either log it or deal with the error here and retry
            }
        }

        /// <summary>
        /// Query options
        /// </summary>
        /// <param name="qo">out queryoptions object</param>
        private void queryOptionsObject(out QueryOptions qo)
        {
            //separate this concern and set query options separately
            qo = new QueryOptions();
            qo.batchSize = 250;
            qo.batchSizeSpecified = true;
        }

        /// <summary>
        /// Get a contact object by passing in the Contact first name
        /// </summary>
        /// <param name="fname">Contact first name</param>
        /// <returns></returns>
        private Contact getContact(string fname)
        {
            QueryResult qr;
            QueryOptions qo;
            queryOptionsObject(out qo);
            Contact contact = null;

            //query contacts and return a result collection
            client.query(header, qo, null, null,
                "SELECT Id FROM Contact WHERE FirstName='" + fname + "'", out qr);

            if (qr.size > 0)
            {
                //retrieve contact from record collection
                contact = (Contact)qr.records[0];
            }
            return contact;
        }

        /// <summary>
        /// Create the attachment object
        /// </summary>
        /// <param name="contact">Contact object to attach to as parent object</param>
        /// <returns>Attachment</returns>
        private Attachment getAttachmentObject(Contact contact)
        {
            Attachment attach = new Attachment();

            attach.Body = sfDocument.Document;
            attach.Name = sfDocument.FileName;
            attach.IsPrivate = false;
            attach.ParentId = contact != null ? contact.Id : "";

            return attach;
        }

        public bool upload()
        {

            bool success;
            Contact userContact = getContact("Jon");

            //all attachments must be assigned to an object
            if (userContact == null || string.IsNullOrEmpty(userContact.Id))
            {
                throw new Exception("Contact not found");
            }

            Attachment attach = getAttachmentObject(userContact);

            sObject[] docs = new Attachment[1];
            docs[0] = attach;
            SaveResult[] sResults;
            
            client.create(header, null, null, null, null, null, null, null, null, null, null, null, docs, out liHeader, out sResults);
            if (sResults[0].success) // we can also loop through results if we have multiple documents created.
            {
                success = true;
            }
            else
            {
                success = false;
                //log the reason why it was not successful which can then be retreived with another service call
            }
            logout();
            return success;

        }

        private void logout()
        {
            client.logout(header);
        }
    }
}
