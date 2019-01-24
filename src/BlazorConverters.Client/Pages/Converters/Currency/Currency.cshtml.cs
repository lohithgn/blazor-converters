using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorCalculator.Services;
using BlazorConverters.Data;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorConverters.Client.Pages
{
    public class CurrencyModel : BlazorComponent
    {
        [Inject] CurrencyService CurrencyService { get; set; }

        protected IEnumerable<Currency> Currencies { get; set; } = null;
        protected Currency SourceCurrency { get; set; } = null;
        protected double SourceCurrencyInput { get; set; } = 1;
        protected string SelectedSourceCurrency { get; set; }
        protected Currency TargetCurrency { get; set; } = null;
        protected double TargetCurrencyInput { get; set; } = 1;
        protected string SelectedTargetCurrency { get; set; }
        protected double Rate { get; set; }
        protected override async Task OnInitAsync()
        {
            Currencies = CurrencyService.GetCurrencies();
            SourceCurrency = Currencies.First();
            SelectedSourceCurrency = SourceCurrency.Code;
            TargetCurrency = Currencies.First();
            SelectedTargetCurrency = TargetCurrency.Code;
            await CalculateRate();
        }

        private async Task CalculateRate()
        {
            Rate = await CurrencyService.CalculateConversionRate(SelectedSourceCurrency, SelectedTargetCurrency);
            TargetCurrencyInput = Rate * SourceCurrencyInput;
        }

        protected async Task OnSourceCurrencyChanged(UIChangeEventArgs e)
        {
            SelectedSourceCurrency = e.Value.ToString();
            SourceCurrency = Currencies.First(c => c.Code == SelectedSourceCurrency);
            await CalculateRate();
            StateHasChanged();
        }

        protected async Task OnTargetCurrencyChanged(UIChangeEventArgs e)
        {
            SelectedTargetCurrency = e.Value.ToString();
            TargetCurrency = Currencies.First(c => c.Code == SelectedTargetCurrency);
            await CalculateRate();
            StateHasChanged();
        }

        protected async Task OnKeyInput(string key)
        {
            var currString = SourceCurrencyInput.ToString();
            switch (key.ToLower())
            {
                case "ce":
                    currString = "0";
                    break;
                case "backspace":
                    currString = currString.Substring(0, currString.Length - 1);
                    break;
                default:
                    currString = (currString.Length == 1 && currString == "0") 
                                          ? key : $"{currString}{key}";
                    break;
            }
            SourceCurrencyInput = double.Parse(currString);
            await CalculateRate();
            StateHasChanged();
        }
    }
}