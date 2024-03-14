using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ChangeLanguageUIDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    private AsyncOperationHandle _initializeOperation;

    private void Awake() {
        dropdown.onValueChanged.AddListener(OnSelectionChanged);
        dropdown.ClearOptions();
        dropdown.options.Add(new TMP_Dropdown.OptionData("Loading..."));
        dropdown.interactable = false;
        
        _initializeOperation = LocalizationSettings.InitializationOperation;
        if (_initializeOperation.IsDone) {
            InitializeCompleted(_initializeOperation);
        }
        else {
            _initializeOperation.Completed += InitializeCompleted;
        }
    }

    void InitializeCompleted(AsyncOperationHandle obj)
    {
        // Create an option in the dropdown for each Locale
        var options = new List<string>();
        int selectedOption = 0;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        for (int i = 0; i < locales.Count; ++i)
        {
            var locale = locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selectedOption = i;

            var displayName = locales[i].Identifier.CultureInfo != null ? locales[i].Identifier.CultureInfo.NativeName : locales[i].ToString();
            options.Add(displayName);
        }

        // If we have no Locales then something may have gone wrong.
        if (options.Count == 0)
        {
            options.Add("No Locales Available");
            dropdown.interactable = false;

        }
        else
        {
            dropdown.interactable = true;
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        dropdown.SetValueWithoutNotify(selectedOption);

        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void LocalizationSettings_SelectedLocaleChanged(Locale obj) {
        dropdown.SetValueWithoutNotify(LocalizationSettings.AvailableLocales.Locales.IndexOf(obj));
    }

    private void OnSelectionChanged(int index)
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }
}
