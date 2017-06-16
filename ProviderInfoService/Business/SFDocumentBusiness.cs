using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProviderInfoModel;
using ProviderInfoService.Respository;

namespace ProviderInfoService.Business
{
    public class SFDocumentBusiness
    {

        //we can use an interface here in order to extract the repository implementation and instead inject it in the constructor
        private SFDocumentsRepository context;
        private SFDocument document;

        //code below is self explanatory. instanciate the repository and pass in the model object as a parameter
        //one function is available to process the uploaded files
        /// <summary>
        /// Instance with a model class.
        /// </summary>
        /// <param name="model">sf document model</param>
        public SFDocumentBusiness(SFDocument model)
        {
            document = model;
            context = new SFDocumentsRepository(document);
        }
        /// <summary>
        /// Upload Documents
        /// </summary>
        /// <returns>boolean if success or fail</returns>
        public bool UploadDocument()
        {
            try
            {
                return context.upload();
            }
            catch (Exception e)
            {
                //we can log or handle the exception here
                throw e;
            }
        }

    }
}
