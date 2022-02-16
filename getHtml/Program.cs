using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text;
using System.Net.Http;

namespace getHtml
{
     class Program 
    {
        static void Main(string[] args)
        {
            var html = new HtmlWeb();
            var document = html.Load("");
            var node = document.DocumentNode.SelectSingleNode("");
            Console.WriteLine(node.InnerText);
            Console.ReadLine();
                  
            
        }
        public void getHtml()
        {
            HttpRequest http = new HttpRequest();
        }
}
