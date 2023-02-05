using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class AudioOptionsDialog : OptionsDialog
    {
        // Serialized Fields

        [SF] private UISlider masterVolumeSlider;
        [SF] private UISlider effectsVolumeSlider;
        [SF] private UISlider musicVolumeSlider;
        [SF] private UISlider uiVolumeSlider;

        // AudioOptionsDialog

        protected override void OnInit()
        {
            base.OnInit();

            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            uiVolumeSlider.onValueChanged.AddListener(OnUiVolumeChanged);
            effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        protected override void OnOpen()
        {
            base.OnOpen();

            App.SoundManager.masterBus.getVolume(out var masterVolume);
            masterVolumeSlider.value = masterVolume;

            App.SoundManager.effectsBus.getVolume(out var effectsVolume);
            effectsVolumeSlider.value = effectsVolume;

            App.SoundManager.musicBus.getVolume(out var musicVolume);
            musicVolumeSlider.value = musicVolume;

            App.SoundManager.uiBus.getVolume(out var uiVolume);
            uiVolumeSlider.value = uiVolume;
        }

        private void OnMasterVolumeChanged(float volume)
        {
            App.settingsSaveData.masterVolume = volume;
            App.SoundManager.masterBus.setVolume(volume);
        }

        private void OnUiVolumeChanged(float volume) 
        {
            App.settingsSaveData.uiVolume = volume;
            App.SoundManager.uiBus.setVolume(volume);
        }

        private void OnEffectsVolumeChanged(float volume)
        {
            App.settingsSaveData.effectsVolume = volume;
            App.SoundManager.effectsBus.setVolume(volume);
        }

        private void OnMusicVolumeChanged(float volume)
        {
            App.settingsSaveData.musicVolume = volume;
            App.SoundManager.musicBus.setVolume(volume);
        }
    }
}
