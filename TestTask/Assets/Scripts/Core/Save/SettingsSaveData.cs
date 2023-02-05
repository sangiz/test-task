[System.Serializable]
public class SettingsSaveData 
{
    //? All game settings data like graphics, visual quality, audio preferences etc.

    // Video options

    public int resolutionIndex = 0;
    public int screenIndex = 0;
    public int vsyncIndex = 0;
    public int showFps = 0;

    // Audio

    public float masterVolume = 1f;
    public float effectsVolume = 1f;
    public float musicVolume = 1f;
    public float uiVolume = 1f;

    // Other

    public string language = "en-US";
}
