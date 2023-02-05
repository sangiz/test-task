using UnityEngine.UI;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class LoadingDialog : Dialog
    {
        [SF] private Slider progressBar;

        private void Update()
        {
            // Update used because UI process not happening on level loading
            if(progressBar != null)
                progressBar.value = App.Scene.LoadingProgress;
        }
    }
}
