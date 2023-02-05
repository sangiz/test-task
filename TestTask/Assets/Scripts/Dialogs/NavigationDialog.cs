using SF = UnityEngine.SerializeField;
using UnityEngine;
using UnityEngine.UI;

namespace IgnSDK
{
    public class NavigationDialog : Dialog
    {
        private RectTransform[] rebuildRects;

        protected override void OnInit()
        {
            base.OnInit();

            rebuildRects = GetComponentsInChildren<RectTransform>(true);
        }

        public void RebuildLayout()
        {
            Async.SkipFrame(() => 
            {
                foreach (RectTransform rect in rebuildRects)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
                }
            });  
        }

        public override void OnCancel()
        {
            //! Have to be empty because navigation always on
        }
    }
}
