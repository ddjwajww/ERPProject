using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.Code;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Xml;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class DailyExchangeRateController : Controller
    {
        [HttpGet]
        public async Task<ResponseData> Run()
        {
            ResponseData result = new ResponseData();
            using HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://www.tcmb.gov.tr/kurlar/today.xml");

                if (response.IsSuccessStatusCode)
                {
                    result.Rate = new List<ResponseDataRate>();
                    string xmlString = await response.Content.ReadAsStringAsync();
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(xmlString);
                    foreach (XmlNode node in document.SelectNodes("Tarih_Date")[0].ChildNodes)
                    {
                        ResponseDataRate rate = new ResponseDataRate();
                        rate.Code = node.Attributes["Kod"].Value;
                        rate.Name = node["Isim"].InnerText;
                        rate.Currency = int.Parse(node["Unit"].InnerText);
                        rate.BuyingRate = Convert.ToDecimal("0" + node["ForexBuying"].InnerText.Replace(".", ","));
                        rate.SellingRate = Convert.ToDecimal("0" + node["ForexSelling"].InnerText.Replace(".", ","));
                        rate.EffectiveBuyingRate = Convert.ToDecimal("0" + node["BanknoteBuying"].InnerText.Replace(".", ","));
                        rate.EffectiveSellingRate = Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".", ","));
                        if (rate.Code == "EUR")
                        {
                            Repo.Exchange.Euro = (rate.BuyingRate).ToString();
                            result.Rate.Add(rate);

                        }

                        else if (rate.Code == "USD")
                        {
                            Repo.Exchange.Dolar = (rate.BuyingRate).ToString();
                            result.Rate.Add(rate);


                        }

                        else if (rate.Code == "GBP")
                        {
                            Repo.Exchange.Sterlin = (rate.BuyingRate).ToString();
                            result.Rate.Add(rate);

                        }
                    }
                }
                else
                {
                    result.Error = "Exchange Rate Not Found";
                    return result;
                }
            }
            catch (HttpRequestException ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}