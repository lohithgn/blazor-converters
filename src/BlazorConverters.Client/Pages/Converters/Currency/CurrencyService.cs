using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorConverters.Data;
using BlazorConverters.Shared;

namespace BlazorCalculator.Services
{
    public class CurrencyService
    {
        private readonly ForexApiClient _apiClient;

        private Quotes forexRates;
        private DateTime? lastLoadTime;

        public CurrencyService(ForexApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return new List<Currency>
            {
                new Currency{ Name="Albanian lek",Code="ALL", Symbol="Lek" },
                new Currency{ Name="Afghanistan Afghani",Code="AFN", Symbol="؋" },
                new Currency{ Name="Argentina Peso",Code="ARS", Symbol="$" },
                new Currency{ Name="Aruba Guilder",Code="AWS", Symbol="ƒ" },
                new Currency{ Name="Australia Dollar",Code="AUD", Symbol="$" },
                new Currency{ Name="Azerbaijan Manat",Code="AZN", Symbol="₼" },
                new Currency{ Name="Bahamas Dollar",Code="BSD", Symbol="$" },
                new Currency{ Name="Barbados Dollar",Code="BBD", Symbol="$" },
                new Currency{ Name="Belarus Ruble",Code="BYN", Symbol="Br" },
                new Currency{ Name="Belize Dollar",Code="BZD", Symbol="BZ$" },
                new Currency{ Name="Bermuda Dollar",Code="BMD", Symbol="$" },
                new Currency{ Name="Bolivia Bolíviano",Code="BOB", Symbol="$b" },
                new Currency{ Name="Bosnia and Herzegovina Convertible Marka",Code="BAM", Symbol="KM" },
                new Currency{ Name="Botswana Pula",Code="BWP", Symbol="P" },
                new Currency{ Name="Bulgaria Lev",Code="BGN", Symbol="лв" },
                new Currency{ Name="Brazil Real",Code="BRL", Symbol="R$" },
                new Currency{ Name="Brunei Darussalam Dollar",Code="BND", Symbol="$" },
                new Currency{ Name="Cambodia Riel",Code="KHR", Symbol="៛" },
                new Currency{ Name="Canada Dollar",Code="CAD", Symbol="$" },
                new Currency{ Name="Cayman Islands Dollar",Code="KYD", Symbol="$" },
                new Currency{ Name="Chile Peso",Code="CLP", Symbol="$" },
                new Currency{ Name="China Yuan Renminbi",Code="CNY", Symbol="¥" },
                new Currency{ Name="Colombia Peso",Code="COP", Symbol="$" },
                new Currency{ Name="Costa Rica Colon",Code="CRC", Symbol="₡" },
                new Currency{ Name="Croatia Kuna",Code="HRK", Symbol="kn" },
                new Currency{ Name="Cuba Peso",Code="CUP", Symbol="₱" },
                new Currency{ Name="Czech Republic Koruna",Code="CZK", Symbol="Kč" },
                new Currency{ Name="Denmark Krone",Code="DKK", Symbol="kr" },
                new Currency{ Name="Dominican Republic Peso",Code="DOP", Symbol="RD$" },
                new Currency{ Name="East Caribbean Dollar",Code="XCD", Symbol="$" },
                new Currency{ Name="Egypt Pound",Code="EGP", Symbol="£" },
                new Currency{ Name="El Salvador Colon",Code="SVC", Symbol="$" },
                new Currency{ Name="Euro Member Countries",Code="EUR", Symbol="€" },
                new Currency{ Name="Falkland Islands (Malvinas) Pound",Code="FKP", Symbol="£" },
                new Currency{ Name="Fiji Dollar",Code="FJD", Symbol="$" },
                new Currency{ Name="Ghana Cedi",Code="GHS", Symbol="¢" },
                new Currency{ Name="Gibraltar Pound",Code="GIP", Symbol="£" },
                new Currency{ Name="Guatemala Quetzal",Code="GTQ", Symbol="Q" },
                new Currency{ Name="Guernsey Pound",Code="GGP", Symbol="£" },
                new Currency{ Name="Guyana Dollar",Code="GYD", Symbol="$" },
                new Currency{ Name="Honduras Lempira",Code="HNL", Symbol="L" },
                new Currency{ Name="Hong Kong Dollar",Code="HKD", Symbol="$" },
                new Currency{ Name="Hungary Forint",Code="HUF", Symbol="Ft" },
                new Currency{ Name="Iceland Krona",Code="ISK", Symbol="kr" },
                new Currency{ Name="India Rupee",Code="INR", Symbol="₹" },
                new Currency{ Name="Indonesia Rupiah",Code="IDR", Symbol="Rp" },
                new Currency{ Name="Iran Rial",Code="IRR", Symbol="﷼" },
                new Currency{ Name="Isle of Man Pound",Code="IMP", Symbol="£" },
                new Currency{ Name="Israel Shekel",Code="ILS", Symbol="₪" },
                new Currency{ Name="Jamaica Dollar",Code="JMD", Symbol="J$" },
                new Currency{ Name="Japan Yen",Code="JPY", Symbol="¥" },
                new Currency{ Name="Jersey Pound",Code="JEP", Symbol="£" },
                new Currency{ Name="Kazakhstan Tenge",Code="KZT", Symbol="лв" },
                new Currency{ Name="Korea (North) Won",Code="KPW", Symbol="₩" },
                new Currency{ Name="Korea (South) Won",Code="KRW", Symbol="₩" },
                new Currency{ Name="Kyrgyzstan Som",Code="KGS", Symbol="лв" },
                new Currency{ Name="Laos Kip",Code="LAK", Symbol="₭" },
                new Currency{ Name="Lebanon Pound",Code="LAK", Symbol="£" },
                new Currency{ Name="Liberia Dollar",Code="LRD", Symbol="$" },
                new Currency{ Name="Macedonia Denar",Code="MKD", Symbol="ден" },
                new Currency{ Name="Malaysia Ringgit",Code="MYR", Symbol="RM" },
                new Currency{ Name="Mauritius Rupee",Code="MUR", Symbol="₨" },
                new Currency{ Name="Mexico Peso",Code="MXN", Symbol="$" },
                new Currency{ Name="Mongolia Tughrik",Code="MNT", Symbol="₮" },
                new Currency{ Name="Mozambique Metical",Code="MZN", Symbol="MT" },
                new Currency{ Name="Namibia Dollar",Code="NAD", Symbol="$" },
                new Currency{ Name="Nepal Rupee",Code="NPR", Symbol="₨" },
                new Currency{ Name="Netherlands Antilles Guilder",Code="ANG", Symbol="ƒ" },
                new Currency{ Name="New Zealand Dollar",Code="NZD", Symbol="$" },
                new Currency{ Name="Nicaragua Cordoba",Code="NIO", Symbol="C$" },
                new Currency{ Name="Nigeria Naira",Code="NGN", Symbol="₦" },
                new Currency{ Name="Norway Krone",Code="NOK", Symbol="kr" },
                new Currency{ Name="Oman Rial",Code="OMR", Symbol="﷼" },
                new Currency{ Name="Pakistan Rupee",Code="PKR", Symbol="₨" },
                new Currency{ Name="Panama Balboa",Code="PAB", Symbol="B/." },
                new Currency{ Name="Paraguay Guarani",Code="PYG", Symbol="Gs" },
                new Currency{ Name="Peru Sol",Code="PEN", Symbol="S/." },
                new Currency{ Name="Philippines Peso",Code="PHP", Symbol="₱" },
                new Currency{ Name="Poland Zloty",Code="PLN", Symbol="zł" },
                new Currency{ Name="Qatar Riyal",Code="QAR", Symbol="﷼" },
                new Currency{ Name="Romania Leu",Code="RON", Symbol="lei" },
                new Currency{ Name="Russia Ruble",Code="RUB", Symbol="₽" },
                new Currency{ Name="Saint Helena Pound",Code="SHP", Symbol="£" },
                new Currency{ Name="Saudi Arabia Riyal",Code="SAR", Symbol="﷼" },
                new Currency{ Name="Serbia Dinar",Code="RSD", Symbol="Дин." },
                new Currency{ Name="Seychelles Rupee",Code="SCR", Symbol="Rs" },
                new Currency{ Name="Singapore Dollar",Code="SGD", Symbol="$" },
                new Currency{ Name="Solomon Islands Dollar",Code="SBD", Symbol="$" },
                new Currency{ Name="Somalia Shilling",Code="SOS", Symbol="S" },
                new Currency{ Name="South Africa Rand",Code="ZAR", Symbol="R" },
                new Currency{ Name="Sri Lanka Rupee",Code="LKR", Symbol="Rs" },
                new Currency{ Name="Sweden Krona",Code="SEK", Symbol="kr" },
                new Currency{ Name="Switzerland Franc",Code="CHF", Symbol="CHF" },
                new Currency{ Name="Suriname Dollar",Code="SRD", Symbol="$" },
                new Currency{ Name="Syria Pound",Code="SYP", Symbol="£" },
                new Currency{ Name="Taiwan New Dollar",Code="TWD", Symbol="NT$" },
                new Currency{ Name="Thailand Baht",Code="THB", Symbol="NT$" },
                new Currency{ Name="Trinidad and Tobago Dollar",Code="TTD", Symbol="TT$" },
                new Currency{ Name="Turkey Lira",Code="TRY", Symbol="₺" },
                new Currency{ Name="Tuvalu Dollar",Code="TVD", Symbol="$" },
                new Currency{ Name="Ukraine Hryvnia",Code="UAH", Symbol="₴" },
                new Currency{ Name="United Kingdom Pound",Code="GBP", Symbol="£" },
                new Currency{ Name="United States Dollar",Code="USD", Symbol="$" },
                new Currency{ Name="Uruguay Peso",Code="UYU", Symbol="$U" },
                new Currency{ Name="Uzbekistan Som",Code="UZS", Symbol="лв" },
                new Currency{ Name="Venezuela Bolívar",Code="VEF", Symbol="Bs" },
                new Currency{ Name="Viet Nam Dong",Code="VND", Symbol="₫" },
                new Currency{ Name="Yemen Rial",Code="YER", Symbol="﷼" },
                new Currency{ Name="Zimbabwe Dollar",Code="ZWD", Symbol="Z$" }
            }.AsEnumerable();
        }

        public async Task<double> CalculateConversionRate(string sourceCurrency, string targetCurrency)
        {
            var rates = await GetLatestRates();
            var sourceRate = double.Parse(rates.GetType().GetProperty($"USD{sourceCurrency}").GetValue(rates, null).ToString());
            var targetRate = double.Parse(rates.GetType().GetProperty($"USD{targetCurrency}").GetValue(rates, null).ToString());
            return targetRate/sourceRate;
        }

        private async Task<Quotes> GetLatestRates()
        {
            if (NeedsRefresh())
            {
                var latest = await _apiClient.LatestRates();
                forexRates = latest.quotes;
                lastLoadTime = DateTime.UtcNow;
            }
            return forexRates;
        }

        private bool NeedsRefresh()
        {
            if (forexRates == null)
            {
                return true;
            }
            var hourElapsed = DateTime.UtcNow.Ticks - lastLoadTime.Value.Ticks >= TimeSpan.FromHours(1).Ticks;
            return lastLoadTime.HasValue && hourElapsed;
        }
    }
}
