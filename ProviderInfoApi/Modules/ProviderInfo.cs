using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using ProviderInfoModel;
using ProviderInfoService.Business;
using ProviderInfoService.Respository;

namespace ProviderInfoApi.Modules
{
    public class ProviderInfo : NancyModule
    {
        public ProviderInfo()
        {
            Get["/get/"] = _ =>
            {
                return Response.AsJson("{'msg':'this end point is working'}");
            };

            Post["/getprovider/"] = parameters =>
            {
                try
                {
                    var providerInfo = this.Bind<ProviderInfoModel.ProviderInfo>();// it binds/maps the parameters to the model
                    ProviderInfoBusiness context = new ProviderInfoBusiness(providerInfo);
                    var responseList = context.GetProviderInfo();
                    var response = new ProviderInfoResponse(responseList);
                    return Response.AsJson(response);
                }
                catch (Exception e)
                {
                    //Add logging
                    var response = new ProviderInfoResponse(new List<ProviderInfoModel.ProviderInfo>());
                    response.Message = e.Message;
                    return Response.AsJson(response);
                }
            };

        }
    }
}