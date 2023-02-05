using System.Collections.Generic;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class VideoOptionsDialog : OptionsDialog
    {
        // Serialized Fields

        [SF] private UIDropdown resolutionDropdown;
        [SF] private UIDropdown screenDropdown;
        [SF] private UIDropdown vsyncDropdown;
        [SF] private UIDropdown fpsDropdown;

        // Private Fields

        private Resolution[] resolutions;
        private List<Resolution> filteredResolutions;

        // VideoOptionsDialog

        protected override void OnInit()
        {
            base.OnInit();

            SetupResolutionDropdown();
            SetupScreenDropdown();
            SetupVSync();
            SetupFps();
        }

        // Resolution

        private void SetupResolutionDropdown()
        {
            resolutions = Screen.resolutions;
            filteredResolutions = new List<Resolution>();

            resolutionDropdown.ClearOptions();
            var refreshRate = Screen.currentResolution.refreshRate; // TODO load from save

            for (int i = resolutions.Length - 1; i > 0; i--)
            {
                if (resolutions[i].refreshRate == refreshRate)
                {
                    filteredResolutions.Add(resolutions[i]);
                }
            }

            List<string> options = new List<string>();

            for (int i = 0; i < filteredResolutions.Count; i++)
            {
                var resolutionsOption = $"{filteredResolutions[i].width}x{filteredResolutions[i].height}";
                options.Add(resolutionsOption);
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = App.settingsSaveData.resolutionIndex;
            resolutionDropdown.RefreshShownValue();

            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }

        private void SetResolution(int index)
        {
            App.settingsSaveData.resolutionIndex = index;

            var resolution = filteredResolutions[index];
            var fullscreen = App.settingsSaveData.screenIndex == 0;

            Screen.SetResolution(resolution.width, resolution.height, fullscreen);
        }

        // Screen

        private void SetupScreenDropdown()
        {
            List<string> options = new List<string>()
            {
                "FULLSCREEN",
                "WINDOWED"
            };

            screenDropdown.AddOptions(options);
            screenDropdown.value = App.settingsSaveData.screenIndex;
            screenDropdown.RefreshShownValue();

            screenDropdown.onValueChanged.AddListener(SetScreen);
        }

        private void SetScreen(int index)
        {
            App.settingsSaveData.screenIndex = index;

            switch (index)
            {
                case 0:
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;
                case 1:
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            }
        }

        // Vsync

        private void SetupVSync()
        {
            List<string> options = new List<string>()
            {
                "OFF",
                "ON"
            };

            vsyncDropdown.AddOptions(options);
            vsyncDropdown.value = App.settingsSaveData.vsyncIndex;
            vsyncDropdown.RefreshShownValue();

            vsyncDropdown.onValueChanged.AddListener(SetVsync);
        }

        private void SetVsync(int index)
        {
            App.settingsSaveData.screenIndex = index;

            switch (index)
            {
                case 0:
                    QualitySettings.vSyncCount = 1;
                    break;
                case 1:
                    QualitySettings.vSyncCount = 0;
                    break;
            }
        }

        // Fps

        private void SetupFps()
        {
            List<string> options = new List<string>()
            {
                "OFF",
                "ON"                
            };

            fpsDropdown.AddOptions(options);
            fpsDropdown.value = App.settingsSaveData.showFps;
            fpsDropdown.RefreshShownValue();

            fpsDropdown.onValueChanged.AddListener(SetFps);
        }

        private void SetFps(int index)
        {
            App.settingsSaveData.showFps = index;

            if (App.settingsSaveData.showFps == 1)
            {
                App.UIManager.GetDialog<FpsDialog>().Open();
            }
            else 
            {
                App.UIManager.GetDialog<FpsDialog>().Close();
            }
        }
    }
}
