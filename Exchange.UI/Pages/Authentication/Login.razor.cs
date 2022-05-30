
using Exchange.UI.Helpers;
using Exchange.UI.Models;
using Exchange.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Exchange.UI.Pages.Authentication
{
    public partial class Login
    {
        private readonly AuthenticationDTO UserForAuthentication = new AuthenticationDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowAuthenticationErrors { get; set; }
        public string Errors { get; set; } = String.Empty;
        public string ReturnUrl { get; set; } = String.Empty;
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public IJSRuntime JsRuntime { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private async Task LoginUser()
        {
            await JsRuntime.ToastrSuccess("Working on it");
            ShowAuthenticationErrors = false;
            IsProcessing = true;
            try
            {


                var result = await AuthenticationService.Login(UserForAuthentication);
                if (result.IsAuthSuccessful)
                {
                    await JsRuntime.ToastrSuccess("DONE");
                    IsProcessing = false;

                    var absoluteUri = new Uri(NavigationManager.Uri);
                    var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                    ReturnUrl = queryParam["returnUrl"];

                    if (string.IsNullOrEmpty(ReturnUrl))
                    {
                        NavigationManager.NavigateTo("/converter");
                    }
                    else
                    {
                        NavigationManager.NavigateTo("/" + ReturnUrl);
                    }
                }
                else
                {
                    await JsRuntime.ToastrError("Auth Failed");
                    await JsRuntime.ToastrError(result.ErrorMessage);
                    IsProcessing = false;
                    Errors = result.ErrorMessage;
                    ShowAuthenticationErrors = true;
                }
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                await JsRuntime.ToastrError("Auth Failed");
                await JsRuntime.ToastrError(ex.Message);
            }
        }
    }
}
