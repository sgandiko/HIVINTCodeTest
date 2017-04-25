Create a single page application that meets the following requirements:
1.	Create 2 API endpoints
1.1	Endpoint 1 
    Requirement: Take a domain name (e.g. yahoo.com) from the URL and respond with all the possible subdomains which contain only third -level domains and no more than 2 characters in the third-level domain name (e.g. a.yahoo.com, but not a.a.yahoo.com or abc.yahoo.com)

1.2	Endpoint 2 
    Requirement: Take an array of subdomains from the request body and find the IP addresses associated with each of the subdomain (use System.Net.Dns.GetHostAddresses(string hostNameorAddress) method).
Code must be written in C# and using one of the ASP.NET libraries, preferably WEB API 2 or MVC Core.
