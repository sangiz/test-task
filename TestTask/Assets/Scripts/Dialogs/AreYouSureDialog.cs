using System;
using TMPro;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class AreYouSureDialog : Dialog
    {
        // Serialized Fields

        [SF] private UIButton yesButton;
        [SF] private UIButton noButton;
        [SF] private TMP_Text questionText;

        // Events

        private event Action OnYesAnswer;

        // AreYouSureDialog

        protected override void OnInit()
        {
            yesButton.onClick.AddListener(YesClick);
            noButton.onClick.AddListener(NoClick);
        }

        public void Ask(Action onYes = null, string question = "REALLY DO THIS") 
        {
            Open();

            OnYesAnswer = onYes;

            questionText.text = question.ToUpper();
        }

        private void YesClick()
        {  
            OnYesAnswer?.Invoke();

            Close();
        }

        private void NoClick()
        {
            OnCancel();
        }
    }
}
