using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class MainOptionsDialog : OptionsDialog
    {
        [SF] private UIButton videoButton;
        [SF] private UIButton graphicsButton;
        [SF] private UIButton audioButton;
        [SF] private UIButton controlsButton;
        [SF] private UIButton languageButton;

        // MainOptionsDialog

        protected override void OnInit()
        {
            base.OnInit();

            videoButton.onClick.AddListener(VideoClick);
            graphicsButton.onClick.AddListener(GraphicsClick);
            languageButton.onClick.AddListener(LanguageClick);
            controlsButton.onClick.AddListener(ControlsClick);
            audioButton.onClick.AddListener(AudioClick);
        }

        private void VideoClick() 
        {
            App.UIManager.ForwardDialog<VideoOptionsDialog>(this);
        }

        private void GraphicsClick()
        {
            App.UIManager.ForwardDialog<GraphicsOptionsDialog>(this);
        }

        private void AudioClick()
        {
            App.UIManager.ForwardDialog<AudioOptionsDialog>(this);
        }

        private void ControlsClick()
        {
            App.UIManager.ForwardDialog<ControlsOptionsDialog>(this);
        }

        private void LanguageClick()
        {
            App.UIManager.ForwardDialog<LanguageOptionsDialog>(this);
        }

        public override void OnCancel()
        {
            base.OnCancel();

            DataManager.SaveData(App.settingsSaveData);
        }
    }
}
