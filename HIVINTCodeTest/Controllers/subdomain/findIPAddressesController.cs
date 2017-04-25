using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HIVINTCodeTest.Models;
using System.Threading.Tasks;


/// <summary>
/// 1.2	Endpoint 2 
//•	Url: /subdomain/findIPAddresses
//•	Request body: array of subdomains
//•	Response body: array of subdomains with the associated IP addresses
//•	Requirement: Take an array of subdomains from the request body and find the IP addresses associated with each of the subdomain(use System.Net.Dns.GetHostAddresses(string hostNameorAddress) method).

/// </summary>
namespace HIVINTCodeTest.Controllers.subdomain
{
    [RoutePrefix("subdomain/findIPAddresses")]
    public class findIPAddressesController : ApiController
    {
        [Route("")]
        public  async Task<HttpResponseMessage>  put([FromBody]IEnumerable<SubDomain> subdomains)
        {
            try
            {
                foreach (var subdomain in subdomains)
                {

                    IPAddress[] ipaddr = await Dns.GetHostAddressesAsync(subdomain.hostName);
                    string ip = null;

                    foreach (var item in ipaddr)
                    {
                        ip += item.ToString();
                        ip += ",";
                    }

                    ip = ip.Remove(ip.Length -1);

                    subdomain.ipaddress = ip;
                }


                return Request.CreateResponse(HttpStatusCode.OK, subdomains);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
            
        }
    }
}
