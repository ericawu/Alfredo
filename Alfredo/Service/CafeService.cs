﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using Alfredo.Domain;
using Alfredo.Resource;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;

namespace Alfredo.Service
{
    public class CafeService
    {
        public static string[] Cafes =
        {
            "Café 4",
            "Café 9",
            "Café 16",
            "Café 25",
            "Café 26",
            "Café 31",
            "Café 34",
            "Café 36",
            "Café 37",
            "Café 40/41",
            "Café 43",
            "Café 50",
            "Café 83",
            "Café 86",
            "Café 92",
            "Café 99",
            "Café 109",
            "Café 112",
            "Café 121",
            "Café Advanta A",
            "Café Bravern 1",
            "Café Bravern 2",
            "Café City Center Plaza",
            "Café Lincoln Square",
            "Café Millennium E",
            "Café Redw-F",
            "Café RTC-5",
            "Café Samm-C",
            "Café Studio H",
            "Café Studio X",
            "Café Willows"
        };

        public static Restaurant GetRestaurant(DateTime day, string cafeName)
        {
            var webUri = new Uri("https://microsoft.sharepoint.com/sites/refweb/");
            const string userName = Constants.Username;
            const string password = Constants.Password;
            var securePassword = new SecureString();

            foreach (var c in password)
            {
                securePassword.AppendChar(c);
            }

            var credentials = new SharePointOnlineCredentials(userName, securePassword);

            var list = GetList(webUri, credentials, "DiningMenus", day, cafeName);

            var menu = list.GroupBy(f => f.DayOfWeekID, f => f,
                (key, g) => new
                {
                    DayOfWeekId = (Day)int.Parse(key),
                    Food = g.Select(f => new Food
                    {
                        Name = f.MenuItem,
                        Description = f.MenuDescription,
                        Price = f.MenuPrice
                    }).ToList()
                })
                .ToDictionary(k => k.DayOfWeekId, v => v.Food);

            return new Restaurant
            {
                Menu = menu
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webUri"></param>
        /// <param name="credentials"></param>
        /// <param name="listTitle"></param>
        /// <param name="day"></param>
        /// <param name="cafeName">eg: Café RTC-5</param>
        /// <returns></returns>
        private static List<MenuFoodItem> GetList(Uri webUri, ICredentials credentials, string listTitle, DateTime day,
            string cafeName)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
                client.Credentials = credentials;
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json;odata=verbose");
                client.Headers.Add(HttpRequestHeader.Accept, "application/json;odata=verbose");

                client.QueryString.Add("$select",
                    "CafeName,StationName,DayOfWeekID,WeekDate,MenuItem,MenuDescription,MenuPrice");
                client.QueryString.Add("$filter", $"CafeName eq '{cafeName}' and WeekDate eq '06/27/2016'");
                client.QueryString.Add("$orderby", "DayOfWeekID,StationName");
                client.QueryString.Add("$top", "700");

                var endpointUri = new Uri(
                    $"https://microsoft.sharepoint.com/sites/refweb/_api/web/lists/GetByTitle('{listTitle}')/items");
                var result = client.DownloadString(endpointUri);

                var jsonResp = JsonConvert.DeserializeObject<Result>(result);
                return jsonResp.D.Results;
            }
        }
    }

    internal class Result
    {
        public Menu D { get; set; }
    }

    internal class Menu
    {
        public List<MenuFoodItem> Results { get; set; }
    }

    internal class MenuFoodItem
    {
        public string CafeName { get; set; }
        public string StationName { get; set; }
        public string MenuItem { get; set; }
        public string MenuDescription { get; set; }
        public string MenuPrice { get; set; }
        public string WeekDate { get; set; }
        public string DayOfWeekID { get; set; }
    }
}