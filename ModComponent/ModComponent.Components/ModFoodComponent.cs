using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Food-Component-Documentation.md")]
    public class ModFoodComponent : ModCookableComponent
    {
        [Tooltip("Days to decay when stored outdoors. 0 means 'Never'. This overrides the basic property 'DaysToDecay'.")]
        public int DaysToDecayOutdoors;

        [Tooltip("Days to decay when stored indoors. 0 means 'Never'. This overrides the basic property 'DaysToDecay'.")]
        public int DaysToDecayIndoors;

        [Tooltip("Calories for one complete item with all servings. Calories remaining will scale with weight.")]
        public int Calories;

        [Range(0, 1)]
        [Tooltip("The number of servings contained in this item. Each consumption will be limited to one serving. 1 means 'Consume completely' - the way all pre-existing food items work.")]
        public int Servings;

        [Tooltip("Real-time seconds it takes to eat one complete serving.")]
        public int EatingTime;

        [Tooltip("Sound to use when the item is either unpackaged or already open.")]
        public DataSoundAsset EatingAudio;

        [Tooltip("Sound to use when the item is still packaged and unopened. Leave empty for unpackaged food.")]
        public DataSoundAsset EatingPackagedAudio;

        [Tooltip("How does this affect your thirst? Represents change in percentage points. Negative values increase thirst, positive values reduce thirst.")]
        public int ThirstEffect;

        [Tooltip("Chance in percent to contract food poisoning from an item above 20% condition.")]
        public int FoodPoisoning;

        [Tooltip("Chance in percent to contract food poisoning from an item below 20% condition.")]
        public int FoodPoisoningLowCondition;

        [Tooltip("Parasite Risk increments in percent for each unit eaten. Leave empty for no parasite risk.")]
        public float[] ParasiteRiskIncrements;

        [Tooltip("Is the food item naturally occurring meat or plant?")]
        public bool Natural;

        [Tooltip("Is the food item raw or cooked?")]
        public bool Raw;

        [Tooltip("Is the food item something to drink? (This mainly affects the names of actions and position in the radial menu)")]
        public bool Drink;

        [Tooltip("Is the food item meat directly from an animal? (E.g. wolf steak, but not beef jerky - mainly for statistics)")]
        public bool Meat;

        [Tooltip("Is the food item fish directly from an animal? (E.g. salmon, but not canned sardines - mainly for statistics)")]
        public bool Fish;

        [Tooltip("Is the food item canned? Canned items will yield a 'Recycled Can' when opened properly.")]
        public bool Canned;

        [Tooltip("Does this item require a tool for opening it? If not enabled, the other settings in this section will be ignored.")]
        public bool Opening;

        [Tooltip("Can it be opened with a can opener?")]
        public bool OpeningWithCanOpener;

        [Tooltip("Can it be opened with a knife?")]
        public bool OpeningWithKnife;

        [Tooltip("Can it be opened with a hatchet?")]
        public bool OpeningWithHatchet;

        [Tooltip("Can it be opened by smashing?")]
        public bool OpeningWithSmashing;

        [Tooltip("Does this item affect 'Condition' while sleeping? If not enabled, the other settings in this section will be ignored.")]
        public bool AffectCondition;

        [Tooltip("How much additional condition is restored per hour?")]
        public float ConditionRestBonus;

        [Tooltip("Amount of in-game minutes the 'ConditionRestBonus' will be applied.")]
        public float ConditionRestMinutes;

        [Tooltip("Does this item affect 'Rest'? If not enabled, the other settings in this section will be ignored.")]
        public bool AffectRest;

        [Tooltip("How much 'Rest' is restored/drained immediately after consuming the item. Represents change in percentage points. Negative values drain rest, positive values restore rest.")]
        public float InstantRestChange;

        [Tooltip("Amount of in-game minutes the 'RestFactor' will be applied.")]
        public int RestFactorMinutes;

        [Tooltip("Does this item affect 'Cold'? If not enabled, the other settings in this section will be ignored.")]
        public bool AffectCold;

        [Tooltip("How much 'Cold' is restored/drained immediately after consuming the item. Represents change in percentage points. Negative values make it feel colder, positive values make it feel warmer.")]
        public float InstantColdChange;

        [Tooltip("Amount of in-game minutes the 'ColdFactor' will be applied.")]
        public int ColdFactorMinutes;

        [Tooltip("Does this item contain Alcohol? If not enabled, the other settings in this section will be ignored. Currently disabled, but you can set a value for when it's re-enabled.")]
        public bool ContainsAlcohol;

        [Tooltip("How much of the item's weight is alcohol?")]
        public float AlcoholPercentage;

        [Tooltip("How many in-game minutes does it take for the alcohol to be fully absorbed? This is scaled by current hunger level (the hungrier the faster). The simulated blood alcohol level will slowly raise over this time. Real-life value is around 45 mins for liquids.")]
        public float AlcoholUptakeMinutes;

        [Tooltip("How much Vitamin C will be added to the player once consumed?")]
        public int VitaminC;
    }
}