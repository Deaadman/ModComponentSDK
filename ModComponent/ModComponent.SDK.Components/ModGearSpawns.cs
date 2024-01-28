using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.SDK.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Gear-Spawns.md")]
    public class ModGearSpawns : ScriptableObject
    {
        [Serializable]
        public class SceneSpawnEntry
        {
            [Tooltip("All following item spawn definitions will use that scene, until another scene is defined. E.g. FarmhouseA.")]
            public DataSceneAsset sceneName;
            public List<SceneItemSpawn> sceneItemSpawns = new();
        }

        [Serializable]
        public class LoottableSpawnEntry
        {
            [Tooltip("All following loot table entry definitions will use that loot table, until another loot table is defined. E.g. LootTableVehicleGloveBox.")]
            public DataLootTableAsset lootTableName;
            public List<LootTableSpawn> lootTableSpawns = new();
        }

        [Serializable]
        public class SceneItemSpawn 
        {
            [Tooltip("The name of the item being spawned. E.g. GEAR_Knife.")]
            public DataGearAsset itemName;

            [Tooltip("The position is a Vector with 3 mandatory components separated by comma.")]
            public Vector3 position;

            [Tooltip("The rotation is a Vector with 3 mandatory components, representing the euler angles of the corresponding Quaternion.")]
            public Vector3 rotation;

            [Tooltip("Spawn Chance is a numeric value representing the probability in per cent that the item actually spawns, so '100' or above means \"will definitely spawn\" and '0' or below means \"will definitely not spawn\".")]
            [Range(0, 100)]
            public int spawnChance;
        }

        [Serializable]
        public class LootTableSpawn
        {
            [Tooltip("The name of the item being spawned. E.g. GEAR_Knife.")]
            public DataGearAsset itemName;

            [Tooltip("The weight represents the \"size\" of the item in the loot table. \"Bigger\" items are more likely to be selected, \"smaller\" items are less likely to be selected. 0 means the item cannot be selected. There is no upper limit.")]
            public int weight;
        }

        public List<SceneSpawnEntry> sceneGearSpawnEntries = new();
        public List<LoottableSpawnEntry> lootTableGearSpawnEntries = new();
    }
}