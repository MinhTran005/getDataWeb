using ConsoleApp22.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xNet;

namespace ConsoleApp22
{
   public class Getdata
    {
        
        public  void CreateDatabase()
        {
            try
            {
                using var dbcontext = new GetdataDbContext();
                string dbname = dbcontext.Database.GetDbConnection().Database;
                dbcontext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                var c = ex;
            }
        }

        public void AddCookie(HttpRequest http, string cookie)
        {
            var temp = cookie.Split(';');
            foreach (var item in temp)
            {
                var temp2 = item.Split('=');
                if (temp2.Count() > 1)
                {
                    http.Cookies.Add(temp2[0], temp2[1]);
                }
            }
        }
        public string PostData(HttpRequest http, string url, string data = null, string contentType = null, string userArgent = "", string cookie = null)
        {
            if (http == null)
            {
                http = new HttpRequest();
                http.Cookies = new CookieDictionary();
            }

            if (!string.IsNullOrEmpty(cookie))
            {
                AddCookie(http, cookie);
            }

            if (!string.IsNullOrEmpty(userArgent))
            {
                http.UserAgent = userArgent;
            }

            string html = http.Post(url, data, contentType).ToString();
            return html;
        }

        public string GetData(string url, HttpRequest http = null, string userArgent = "", string cookie = null)
        {
            if (http == null)
            {
                http = new HttpRequest();
                http.Cookies = new CookieDictionary();
            }

            if (!string.IsNullOrEmpty(cookie))
            {
                AddCookie(http, cookie);
            }

            if (!string.IsNullOrEmpty(userArgent))
            {
                http.UserAgent = userArgent;
            }
            string html = http.Get(url).ToString();
            return html;
        }

        public string GetLoginDataToken(string html)
        {
            string token = "";

            var res = Regex.Matches(html, @"(?<=__RequestVerificationToken"" type=""hidden"" value="").*?(?="")", RegexOptions.Singleline);

            if (res != null && res.Count > 0)
            {
                token = res[0].ToString();
            }

            return token;
        }
        public void saveDatabase()
        {
            
            HttpRequest http = new HttpRequest();
            http.Cookies = new CookieDictionary();
            string userArgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

            var html = GetData("http://192.168.68.72:8888/", http, userArgent);


            string token = GetLoginDataToken(html);
            string userName = "admin";
            string password = "123";


            string data = "__RequestVerificationToken=" + token + "&Username=" + WebUtility.UrlEncode(userName) + "&Password=" + WebUtility.UrlEncode(password);
            html = PostData(http, "http://192.168.68.72:8888/xac-thuc/dang-nhap", data, "application/x-www-form-urlencoded").ToString();
            html = GetData("http://192.168.68.72:8888/", http, userArgent);
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var GiaTriNode = document.DocumentNode.QuerySelectorAll(".m-widget1__item").ToList();

            //foreach (var gt in GiaTriNode)
            //{
            //    Console.WriteLine(gt.InnerText);
            //}
            //Console.ReadLine();

            var donvi = "";
            var hientai = "";
            var congsuat = "";
            var thietke = "";
            var sanluongngay = "";
            foreach (var giatricon in GiaTriNode)
            {
                var colNodes = giatricon.Descendants("h3");
                if (colNodes.Count() != 5)
                {
                    throw new InvalidOperationException("The website modify the format result results");
                }
                var colList = colNodes.ToList();
                donvi = colList[0].InnerText;
                donvi = donvi.Replace("\r","");
                donvi = donvi.Replace("\n", "");
                donvi = donvi.Trim();
                hientai = colList[1].InnerText;
                
                var hientai1 = Regex.Match(hientai, @"\d+").Value;
                congsuat = colList[2].InnerText;
                var CongSuatLonNhat1 = Regex.Match(congsuat, @"\d+").Value;
                thietke = colList[3].InnerText;
                var ThietKe1 = Regex.Match(thietke, @"\d+").Value;
                sanluongngay = colList[4].InnerText;
                var SanLuongNgay1 = Regex.Match(sanluongngay, @"\d+").Value;
                int Hientai = int.Parse(hientai1);
                int CongSuatLonNhat = int.Parse(CongSuatLonNhat1);
                int ThietKe = int.Parse(ThietKe1);
                int SanLuongNgay = int.Parse(SanLuongNgay1);
                using (var dbcontext = new GetdataDbContext())
                {
                    var datas = new bangdulieu
                    {
                        DonVi = donvi.ToString(),
                        HienTai = Hientai,
                        CongSuatMax = CongSuatLonNhat,
                        ThietKe = ThietKe,
                        SanLuongNgay = SanLuongNgay,                        
                    };
                    dbcontext.Add<bangdulieu>(datas);                     
                    dbcontext.SaveChanges();                                                               
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                string file = path + DateTime.Now.ToString("dd-MM-yyyyy") + ".txt";
                Console.WriteLine("service start at" + DateTime.Now);
                if (!System.IO.File.Exists(file))
                {
                    using (StreamWriter sw = File.CreateText(file))
                    {
                        sw.WriteLine("Lấy dữ liệu từ web");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(file))
                    {
                        sw.WriteLine("service start at" + DateTime.Now);
                        sw.WriteLine($" {donvi,50}"+ "   " + Hientai + "   " + CongSuatLonNhat + "   " + ThietKe + "   " + SanLuongNgay);
                       
                    }

                }
            }
            //
        }
    }
}
