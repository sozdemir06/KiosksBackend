using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Business.Abstract;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ExchangeRateManager : IExchangeRateService
    {
        private readonly ICurrencyDal currencyDal;
        private readonly IMapper mapper;
        public ExchangeRateManager(ICurrencyDal currencyDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.currencyDal = currencyDal;

        }
        public async Task<List<ExchangeRate>> GetExChangeRateAsync()
        {
            List<ExchangeRate> exchangeRateList = new List<ExchangeRate>();
            try
            {
                XDocument xDoc = XDocument.Load("https://www.tcmb.gov.tr/kurlar/today.xml");
                var selectedCurrency=await currencyDal.GetListAsync(x=>x.Selected==true);
             

                foreach (var item in selectedCurrency)
                {
                        var getCurrency = xDoc.Descendants("Currency").Where(x => (string)x.Attribute("Kod") == item.ShorName)
                        .Select(o => new ExchangeRate
                        {
                            Name = (string)o.Element("Isim"),
                            ForexBuying = (string)o.Element("ForexBuying"),
                            ForexSelling = (string)o.Element("ForexSelling"),
                            Symbol=item.Symbol
                        })
                        .ToList();  
                    exchangeRateList.AddRange(getCurrency);
                }
              
                return await Task.FromResult(exchangeRateList);
            }
            catch (Exception)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = "Döviz kuru alınamadı" });
            }
        }
    }
}