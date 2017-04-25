Create a single page application to find the subdomains of a given domain name.

I have used google custom search api free keys to search the url with the given domain name. 

Endpoint 1:

Code implementation is done in the below mentioned file. 

..\HIVINTCodeTest\HIVINTCodeTest\Controllers\subdomain\enumerate\somedomainController.cs

Details: I will pass the query string("eg: Site:*.yahoo.com") to the CustomsearchSerive request. It will return the list of query results like google search. I will iterate each page contains 10 results and parse each result url. 

Model code : 

public class SubDomain
    {
        public string hostName { get; set; }

        public string ipaddress;
    }
    
    After finding the subdomain of the result url, create new SubDomain Object with hostname add to the list. After completing all iterations, I will return the SubDomain List in the response body.


Endpoint 2:

Code implementation is done in the below mentioned file.


..\HIVINTCodeTest\HIVINTCodeTest\Controllers\subdomain\findIPAddressesController.cs

Details: The response which I have got from the above endpoint, I will pass it to the PUT request. Iterate each SubDomain object in the list and find the respective IPAddress of the hostname and update the list. Add the updated list to the response.

Limitations:

1) Since, I have used free key, I can read only upto 10 pages from the search result. It has daily limit of the 20 transactions.