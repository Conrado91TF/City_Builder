using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationLocal : MonoBehaviour
{
    private bool active = false;

    public void SetLocaleID(int localeID)
    {
        if(active ==true)
            return;
        StartCoroutine(Setlocale(localeID));

    }


    IEnumerator Setlocale(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        LocalizationSettings.SelectedLocale = selectedLocale;
        active = false;
    }
}