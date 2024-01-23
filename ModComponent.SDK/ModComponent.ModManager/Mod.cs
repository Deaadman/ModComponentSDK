using UnityEngine;

namespace ModComponent.ModManager
{
    public class Mod : ScriptableObject
    {
        public string Name;
        public string Author;
        public string Version;
        public string[] RequiredMods;
        public bool RequiresDLC;
        public GameObject[] Items = new GameObject[0];
        public Texture2D[] Icons = new Texture2D[0];

        public static Mod CreateMod(string name, string author)
        {
            Mod mod = CreateInstance<Mod>();
            mod.Name = name;
            mod.Author = author;
            return mod;
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