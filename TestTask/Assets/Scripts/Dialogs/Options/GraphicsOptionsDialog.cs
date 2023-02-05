using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class GraphicsOptionsDialog : OptionsDialog
    {
        // Serialized Fields

        [SF] private UIDropdown textureQualityDropdown;
        [SF] private UIDropdown antiAliasingDropdown;

        // GraphicsOptionsDialog

        protected override void OnInit() 
        {
            base.OnInit();
        }    
    }
}
