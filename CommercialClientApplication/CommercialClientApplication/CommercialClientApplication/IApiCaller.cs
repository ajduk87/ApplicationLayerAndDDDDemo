using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication
{
    public interface IApiCaller
    {
        IRestResponse Post<T>(string url, T parameter);
        IRestResponse Put<T>(string url, T parameter);
        IRestResponse Delete<T>(string url, T parameter);
        string Get(string url, params object[] parameters);
    }
}
