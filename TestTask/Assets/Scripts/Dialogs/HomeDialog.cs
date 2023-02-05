using UnityEngine;
using UnityEngine.EventSystems;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class HomeDialog : Dialog
    {
        // Serialized Fields

        [SF] private UIButton playButton;
        [SF] private UIButton optionsButton;
        [SF] private UIButton quitButton;

        // HomeDialog

        protected override void OnInit()
        {
            playButton.onClick.AddListener(PlayClick);
            optionsButton.onClick.AddListener(OptionsClick);
            quitButton.onClick.AddListener(QuitClick);

            EventSystem.current.SetSelectedGameObject(defaultSelection.gameObject);
        }

        private void PlayClick() 
        {
            Close();
            App.UIManager.CloseDialog<NavigationDialog>();

            App.Scene.LoadScene(Constants.GAME_SCENE);
        }

        private void OptionsClick()
        {
            App.UIManager.ForwardDialog<MainOptionsDialog>(this);
        }

        private void QuitClick() 
        {
            var dialog = App.UIManager.GetDialog<AreYouSureDialog>();
            dialog.Ask(onYes, question: "QUIT GAME?");

            void onYes()
            {
                Application.Quit();
            }
                 
        }

        public override void OnCancel()
        {
            
        }
    }
}
