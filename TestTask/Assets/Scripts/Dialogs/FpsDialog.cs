using TMPro;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class FpsDialog : Dialog
    {
        [SF] private TMP_Text fpsText;

        
        private int fpsAccumulator = 0;
        private float fpsNextPeriod = 0;
        private int currentFps;

        const float fpsMeasurePeriod = 0.2f;

        // FpsDialog

        public override void Process(float delta)
        {
            // measure average frames per second
            fpsAccumulator++;

            if (Time.realtimeSinceStartup > fpsNextPeriod) // TODO will it work with timeScale 0
            {
                currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
                fpsAccumulator = 0;
                fpsNextPeriod += fpsMeasurePeriod;
                fpsText.text = $"FPS: {currentFps}";
            }
        }

        protected override void OnInit()
        {
            fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

            gameObject.SetActive(App.settingsSaveData.showFps == 1);
        }

        public override void OnCancel()
        {
            // Have to be empty. We manauly turn this on/off through settings
        }
    }
}
