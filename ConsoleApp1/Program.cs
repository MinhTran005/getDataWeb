using HtmlAgilityPack;
using System;
using System.Linq;
using xNet;

namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            /*
            var html2 = new HtmlWeb();
            var document = html2.Load("");
            var node = document.DocumentNode.SelectSingleNode("");
            Console.WriteLine(node.InnerText);
            Console.ReadLine();
            */
            string baseUrl = "http://192.168.68.72:8888/;
                     string loginUrl = "http://192.168.68.72:8888/xac-thuc/dang-nhap"; 
                     string sessionUrl = "/Data";

                     var uri = new Uri(baseUrl);

                     CookieContainer cookies = new CookieContainer();
                     HttpClientHandler handler = new HttpClientHandler();
                     handler.CookieContainer = cookies;

                     using (var client = new HttpClient(handler))
                     {
                           client.BaseAddress = uri;

                           var request = new { userName = "you", password = "pwd" };
                           var resLogin = client.PostAsJsonAsync(loginUrl,request).Result;
                           if (resLogin.StatusCode != HttpStatusCode.OK)
                                Console.WriteLine("Could not login -> StatusCode = " + resLogin.StatusCode);

                           // see what cookies are returned   
                          IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
                          foreach (Cookie cookie in responseCookies)
                                Console.WriteLine(cookie.Name + ": " + cookie.Value);

                          var resData = client.GetAsync(dataUrl).Result;
                          if(resSession.StatusCode != HttpStatusCode.OK)
                                Console.WriteLine("Could not get data html -> StatusCode = " + resSession.StatusCode);

                           var html = resSession.Content.ReadAsStringAsync().Result;

                           var doc = new HtmlDocument();
                           doc.LoadHtml(html);
                     }
        }
        
    }
}
