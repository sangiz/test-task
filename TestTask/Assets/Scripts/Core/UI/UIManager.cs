using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IgnSDK
{
    public class UIManager : MonoBehaviour
    {
        private readonly Dictionary<Type, Dialog> dialogs = new Dictionary<Type, Dialog>();

        // UIManager

        public T GetDialog<T>() where T: Dialog
        {
            if (dialogs.TryGetValue(typeof(T), out Dialog dialog))
            {
                return dialog as T;
            }

            Debug.LogError($"Dialog { typeof(T) } not found");
            
            return default;
        }

        public void ForwardDialog<T>(Dialog exitToDialog, Action callBack = null) where T : Dialog
        {
            GetDialog<T>().Forward(exitToDialog, callBack);
        }

        public void OpenDialog<T>(Action callBack = null) where T : Dialog
        {
            GetDialog<T>().Open(callBack);
        }
        
        public void CloseDialog<T>(Action callBack = null) where T : Dialog
        {
            GetDialog<T>().Close(callBack);
        }

        public void Init()
        {
            var d = GetComponentsInChildren<Dialog>(true);

            foreach (var dialog in d)
            {
                dialogs.Add(dialog.GetType(), dialog);
            }

            foreach (var dialog in dialogs)
            {
                dialog.Value.Init();
            }
        }

        public void Process(float delta)
        {
            foreach (var dialog in dialogs)
            {
                if (dialog.Value.gameObject.activeInHierarchy)
                    dialog.Value.Process(delta);
            }
        }

        public bool IsMouseOverUi()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
