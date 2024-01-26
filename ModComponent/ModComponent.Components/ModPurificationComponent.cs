using ModComponent.Components;
using ModComponent.SDK.Components;
using UnityEngine;

[HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Purification-Component-Documentation.md")]
public class ModPurificationComponent : ModGenericComponent
{
    public float LitersPurify;
    public float ProgressBarDurationSeconds;
    public string ProgressBarLocalizationID;
    public DataSoundAsset PurifyAudio;
}