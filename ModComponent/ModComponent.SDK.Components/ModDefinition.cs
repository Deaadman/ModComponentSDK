using UnityEngine;

namespace ModComponent.SDK.Components
{
    public class ModDefinition : ScriptableObject
    {
        public string Name;
        public string Author;
        public string Version;

        public string[] RequiredMods;
        public bool RequiresDLC;

        public ModLocalization modLocalization;
        public ModGearSpawns modGearSpawns;

        public GameObject[] Items = new GameObject[0];
        public Texture2D[] Icons = new Texture2D[0];

        public static ModDefinition CreateModDefinition(string name, string author)
        {
            ModDefinition modDefinition = CreateInstance<ModDefinition>();
            modDefinition.Name = name;
            modDefinition.Author = author;
            return modDefinition;
        }

        internal struct BuildInfo
        {
            public string Name;
            public string Author;
            public string Version;
            public string[] Requires;
            public bool RequiresDLC;
        }
    }
}