using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Ngsystem.FrontDentis.Utility;
using Ngsystem.Infrastructure.Dtos;
using Ngsystem.Infrastructure.Infrastructure.Http;

namespace Ngsystem.FrontDentis.Layout;
public class LoginBase : ComponentBase
{
    [Inject] ILogin _ilogin { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
    [Inject] CustomAuthStateProvider customAuthStateProvider { get; set; } = null;


    public UsuarioLoginDto usuarioLogin = new UsuarioLoginDto();
    public string myImageClass { get; set; } = "d-none";
    public string myAlert { get; set; } = "d-none";
    public bool disableButton { get; set; } = false;

    public bool PasswordVisibility;
    public InputType PasswordInput = InputType.Password;
    public string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;


    public void TogglePasswordVisibility()
    {
        if (PasswordVisibility)
        {
            PasswordVisibility = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            PasswordVisibility = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }

    public async Task OnValidSubmit()
    {
        myImageClass = "d-block";
        disableButton = true;
        var result = await _ilogin.Authentication(usuarioLogin);

        if (result.Status)
        {
            await localStorage.SetItemAsync("authToken", result.Registro.Token);
            CustomAuthStateProvider.IsAuthenticated = true;
            customAuthStateProvider.NotifyAuthenticationStateChanged();
            _navigationManager.NavigateTo("/page/dashboard");
        }
        else
        {
            myImageClass = "d-none";
            disableButton = false;
            myAlert = "d-block";
        }
    }
}
