using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IgnSDK
{
    public class UIButton : Button
    {
        public override void OnSubmit(BaseEventData eventData) 
        {
            base.OnSubmit(eventData);

            App.Database.audioDB.buttonClick.Play2D();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            App.Database.audioDB.buttonClick.Play2D();
        }

        public override void OnSelect(BaseEventData eventData) 
        {
            base.OnSelect(eventData);

            App.Database.audioDB.buttonSelect.Play2D();
        }
    }
}
