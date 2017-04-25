using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Web.Http;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using HIVINTCodeTest.Models;
using System.Threading.Tasks;


///// <summary>
///// 1.1	Endpoint 1 
//•	Url:  /subdomain/enumerate/somedomain
//•	Response body: array of subdomains
//•	Requirement: Take a domain name(e.g.yahoo.com) from the URL and respond with all the possible subdomains which contain only third -level domains and no more than 2 characters in the third-level domain name(e.g.a.yahoo.com, but not a.a.yahoo.com or abc.yahoo.com)

///// </summary>

namespace HIVINTCodeTest.Controllers.subdomain.enumerate
{
    [RoutePrefix("subdomain/enumerate/somedomain")]
    public class somedomainController : ApiController
    {
        // Google API  free key
        // Restricted to read only 10 pages data search
        private const string apiKey = "AIzaSyDZMV0S3bBV4LvywH6SmT6uMRnVV2q83Ck";
        private const string searchEngineId = "016957202509644026363:w58kpytyx8c";

        //SubDomain is the model class contains Host Name and IP Address
        private static List<SubDomain> subdomains = new List<SubDomain>();
        private static string query;
        //method to get the validate the subdomain of the host
        private static bool getSubDomain(string host)
        {
            // trimming the  "www." string fromt the host.
            int index = host.IndexOf("www.");
            string trimhost = (index < 0) ? host : host.Remove(index, 4);

            // splitting the host name with '.'
            string[] str = trimhost.Split('.');

            // find the 3 tier domain
            if (str.Length == 3)
            {
                if (str[0].Length <=2)
                    return true;
            }

            return false;

        }




        private static async Task<int> readPages(CseResource.ListRequest listRequest)
        {
            var results = await listRequest.ExecuteAsync();
            if (results.Items != null)
            {
                foreach (var item in results.Items)
                {
                    if ((item.Title != null) && (item.Link != null))
                    {
                        Uri myUri = new Uri(item.Link);

                        if (getSubDomain(myUri.Host))
                            subdomains.Add(new SubDomain() { hostName = myUri.Host });
                        
                    }

                }

                return results.Items.Count;
            }
            else
            {
                return 0;
            }

        }




        private static async Task<int> SearchSite()
        {
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = searchEngineId;

            var count = 0;
            List<Task<int>> paging = new List<Task<int>>();
            while (count < 10)
            {
                Console.WriteLine($"Page {count}");
                listRequest.Start = count * 10 + 1;

                paging.Add(readPages(listRequest));

                while (paging.Count > 0)
                {
                    Task<int> finishedTask = await Task.WhenAny(paging.ToArray());

                    paging.Remove(finishedTask);
                    Console.WriteLine(finishedTask.Result);
                }


                count++;
            }

            return subdomains.Count;
        }



        [Route("")]
        public HttpResponseMessage Get([FromUri]string domain)
        {
            
            try
            {
                // google api custom search code
                query = "site:*" + domain;
                var task = Task.Run(SearchSite);
                Console.WriteLine(task.Result);
                return Request.CreateResponse(HttpStatusCode.OK, subdomains);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("exception ++ {0}", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

    
    }
}
