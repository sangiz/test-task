using UnityEngine.UI;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class PauseDialog : Dialog
    {
        // Serialized Fields

        [SF] private UIButton resumeButton;
        [SF] private UIButton optionsButton;
        [SF] private Button   exitToMenuButton;

        // PauseDialog

        protected override void OnInit()
        {
            resumeButton.onClick.AddListener(ResumeClick);
            optionsButton.onClick.AddListener(OptionsClick);
            exitToMenuButton.onClick.AddListener(ExitToMenuClick);
        }

        private void ResumeClick()
        {
            Close();
        }

        protected override void OnClose()
        {
            App.ResumeGame();

            App.UIManager.CloseDialog<NavigationDialog>();
        }

        private void OptionsClick()
        {
            App.UIManager.ForwardDialog<MainOptionsDialog>(this);
        }

        private void ExitToMenuClick()
        {
            var dialog = App.UIManager.GetDialog<AreYouSureDialog>();
            dialog.Ask(onYes, question: "EXIT TO MAIN MENU?");

            void onYes() 
            {
                Close();
                App.Scene.LoadScene(Constants.MAIN_SCENE);
            }
        }

        public override void OnCancel()
        {
        }
    }
}
