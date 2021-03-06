using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace DSCC._7417.Interface
{
    // this class is implemented to apply SRP and DRY
    // the method is used by product and category controller

    public class HttpHelper
    {
        // constant variable is created to make sure that the base url cannot be changed by others
        private const string _baseUrl = "http://ec2-18-225-34-12.us-east-2.compute.amazonaws.com/";

        public static HttpClient HttpClientHelper = new HttpClient()
        {
            BaseAddress = new Uri(_baseUrl)
        };
    }
}