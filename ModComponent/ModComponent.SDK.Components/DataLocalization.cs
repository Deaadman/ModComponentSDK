using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.SDK.Components
{
    public class DataLocalization : ScriptableObject
    {
        [Serializable]
        public class LocalizationEntry
        {
            public string localizationKey;
            public Languages languages = new();
        }

        [Serializable]
        public class Languages
        {
            public string English = "";
            public string German = "";
            public string Russian = "";
            public string French = "";
            public string Japanese = "";
            public string Korean = "";
            public string SimplifiedChinese = "";
            public string Swedish = "";
            public string TraditionalChinese = "";
            public string Turkish = "";
            public string Norwegian = "";
            public string Spanish = "";
            public string PortuguesePortugal = "";
            public string PortugueseBrazil = "";
            public string Dutch = "";
            public string Finnish = "";
            public string Italian = "";
            public string Polish = "";
            public string Ukrainian = "";
        }

        public List<LocalizationEntry> localizationEntries = new();
    }
}