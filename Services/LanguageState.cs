using Microsoft.JSInterop;

namespace Piratenet.Services;

public sealed class LanguageState
{
    public const string StorageKey = "piratenet.settings.language";

    public string CurrentLanguage { get; private set; } = "es";

    public async Task LoadAsync(IJSRuntime js)
    {
        try
        {
            var storedLanguage = await js.InvokeAsync<string>("localStorage.getItem", StorageKey);
            if (storedLanguage is "es" or "en")
            {
                CurrentLanguage = storedLanguage;
            }
        }
        catch
        {
            // localStorage puede no estar disponible en algunos contextos.
        }
    }

    public async Task SetAsync(IJSRuntime js, string language)
    {
        if (language is not ("es" or "en"))
        {
            return;
        }

        CurrentLanguage = language;
        try
        {
            await js.InvokeVoidAsync("localStorage.setItem", StorageKey, language);
        }
        catch
        {
            // localStorage puede no estar disponible en algunos contextos.
        }
    }
}
