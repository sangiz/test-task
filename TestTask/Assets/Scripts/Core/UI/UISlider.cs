using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IgnSDK
{
    public class UISlider : Slider
    {
        public override void OnMove(AxisEventData eventData)
        {
            base.OnMove(eventData);

            App.Database.audioDB.sliderMove.Play2D();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
        }
    }
}
