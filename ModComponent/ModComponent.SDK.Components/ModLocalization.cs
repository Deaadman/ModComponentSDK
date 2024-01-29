using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.SDK.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Localizations.md")]
    public class ModLocalization : ScriptableObject
    {
        [Serializable]
        public class LocalizationEntry
        {
            [Tooltip("The localization key for this entry.")]
            public string localizationKey;

            [Tooltip("Languages for this entry.")]
            public Languages languages = new();
        }

        [Serializable]
        public class Languages
        {
            [Tooltip("English language localization.")]
            public string English = "";

            [Tooltip("German language localization.")]
            public string German = "";

            [Tooltip("Russian language localization.")]
            public string Russian = "";

            [Tooltip("French language localization.")]
            public string French = "";

            [Tooltip("Japanese language localization.")]
            public string Japanese = "";

            [Tooltip("Korean language localization.")]
            public string Korean = "";

            [Tooltip("Simplified Chinese language localization.")]
            public string SimplifiedChinese = "";

            [Tooltip("Swedish language localization.")]
            public string Swedish = "";

            [Tooltip("Traditional Chinese language localization.")]
            public string TraditionalChinese = "";

            [Tooltip("Turkish language localization.")]
            public string Turkish = "";

            [Tooltip("Norwegian language localization.")]
            public string Norwegian = "";

            [Tooltip("Spanish language localization.")]
            public string Spanish = "";

            [Tooltip("Portuguese (Portugal) language localization.")]
            public string PortuguesePortugal = "";

            [Tooltip("Portuguese (Brazil) language localization.")]
            public string PortugueseBrazil = "";

            [Tooltip("Dutch language localization.")]
            public string Dutch = "";

            [Tooltip("Finnish language localization.")]
            public string Finnish = "";

            [Tooltip("Italian language localization.")]
            public string Italian = "";

            [Tooltip("Polish language localization.")]
            public string Polish = "";

            [Tooltip("Ukrainian language localization.")]
            public string Ukrainian = "";
        }

        [Tooltip("List of localization entries.")]
        public List<LocalizationEntry> localizationEntries = new();
    }
}