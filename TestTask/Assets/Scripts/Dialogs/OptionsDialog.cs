using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class OptionsDialog : Dialog
    {
        [SF] private UIButton backButton;

        protected override void OnInit() 
        {
            backButton.onClick.AddListener(OnCancel);
        }
    }
}
