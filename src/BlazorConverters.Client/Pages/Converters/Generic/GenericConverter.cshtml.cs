using System.Threading.Tasks;
using BlazorConverters.Client.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace BlazorConverters.Client.Pages
{
    public class GenericConverterModel : BlazorComponent
    {
        [Parameter] protected UnitCategory CurrentUnitCategory { get; set; }

        [Inject] private IUriHelper UriHelper { get; set; }
        [Inject] private UnitsService UnitsService { get; set; }

        protected string[] Units { get; set; }
        protected string SourceUnit { get; set; }
        protected int SourceUnitInput { get; set; } = 1;
        protected string TargetUnit { get; set; }
        protected double TargetUnitInput { get; set; } = 1;

        protected override void OnParametersSet()
        {
            Units = UnitsService.GetUnits(CurrentUnitCategory);
            SourceUnit = Units[0];
            TargetUnit = Units[0];
        }

        public void OnSourceUnitChanged(UIChangeEventArgs args)
        {
            SourceUnit = args.Value.ToString();
            PerformConversion();
        }

        private void PerformConversion()
        {
            TargetUnitInput = UnitsService.Convert(SourceUnitInput, CurrentUnitCategory, SourceUnit, TargetUnit);
            StateHasChanged();
        }

        protected void OnTargetUnitChanged(UIChangeEventArgs args)
        {
            TargetUnit = args.Value.ToString();
            PerformConversion();
        }

        protected async Task OnKeyInput(string key)
        {
            await Task.Run(() =>
            {
                var unitString = SourceUnitInput.ToString();
                switch (key.ToLower())
                {
                    case "ce":
                        unitString = "0";
                        break;
                    case "backspace":
                        unitString = unitString.Substring(0, unitString.Length - 1);
                        if(string.IsNullOrEmpty(unitString))
                        {
                            unitString = "0";
                        }
                        break;
                    default:
                        unitString = (unitString.Length == 1 && unitString == "0")
                                              ? key : $"{unitString}{key}";
                        break;
                }
                SourceUnitInput = int.Parse(unitString);
                PerformConversion();
            });
        }
    }
}