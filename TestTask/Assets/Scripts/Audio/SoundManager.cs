using UnityEngine;
using HI = UnityEngine.HideInInspector;

namespace IgnSDK
{
    public class SoundManager : MonoBehaviour
    {
        public bool enableDebugMessages;

        [HI] public FMOD.Studio.Bus masterBus;
        [HI] public FMOD.Studio.Bus uiBus;
        [HI] public FMOD.Studio.Bus effectsBus;
        [HI] public FMOD.Studio.Bus musicBus;

        // SoundManager

        public void Init() 
        {
            //? Different bus allow to control each sound group separately Ex. change volume, pause

            // Get bus ref
            masterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
            uiBus = FMODUnity.RuntimeManager.GetBus("Bus:/UI_SFX_Bus");
            effectsBus = FMODUnity.RuntimeManager.GetBus("Bus:/GAMEPLAY_SFX_Bus");
            musicBus = FMODUnity.RuntimeManager.GetBus("Bus:/MUSIC_Bus");

            // Assign bus volume from savedata
            App.SoundManager.masterBus.setVolume(App.settingsSaveData.masterVolume);
            App.SoundManager.effectsBus.setVolume(App.settingsSaveData.effectsVolume);
            App.SoundManager.musicBus.setVolume(App.settingsSaveData.musicVolume);
            App.SoundManager.uiBus.setVolume(App.settingsSaveData.uiVolume);
        }

        public void StopAll()
        {
            masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        public void SetPaused(bool paused) 
        {
            effectsBus.setPaused(paused);
            musicBus.setPaused(paused);
        }
    }

   
}
