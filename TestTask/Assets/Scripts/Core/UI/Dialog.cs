using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class Dialog : MonoBehaviour
    {
        // Serialized Fields
        [SF] private bool gamePausedWhileOpen;
        [SF] protected Selectable defaultSelection;
        
        // Private Fields

        private Dialog exitToDialog = null;
        private Selectable lastSelected = null;

        // Properties

        public bool GamePausedWhileOpen => gamePausedWhileOpen;

        // Dialog

        public void Init()
        {
            App.Scene.InputManager.OnCancelAction += OnCancel;

            OnInit();
        }



        public virtual void Process(float delta)
        {

        }

        protected virtual void OnInit()
        {
        }

        public virtual void Forward(Dialog exitToDialog, Action callback = null)
        {
            exitToDialog.gameObject.SetActive(false);
            gameObject.SetActive(true);

            if (gamePausedWhileOpen) 
            {
                App.PauseGame();
            }

            this.exitToDialog = exitToDialog;

            exitToDialog.lastSelected = EventSystem.current.currentSelectedGameObject.GetComponent<UIButton>();
            SelectButton();
            OnOpen();

            callback?.Invoke();
        }

        public void Open(Action callback = null)
        {
            gameObject.SetActive(true);

            if (gamePausedWhileOpen)
            {
                App.PauseGame();
            }

            SelectButton();

            OnOpen();

            callback?.Invoke();
        }

        protected virtual void OnOpen() 
        {
        }

        private void SelectButton()
        {
            if (lastSelected != null)
            {
                lastSelected.Select();
                lastSelected = null;
            }
            else
            {
                defaultSelection?.Select();
            }
        }

        protected virtual void OnClose()
        {
        }

        public void Close(Action callback = null)
        {
            if (gamePausedWhileOpen)
            {
                App.ResumeGame();
            }

            OnClose();

            gameObject.SetActive(false);
            callback?.Invoke();
        }

        public virtual void OnCancel()
        {
            if (!gameObject.activeInHierarchy)
                return;

            Close();

            Async.SkipFrame(() =>
            { 
                if (exitToDialog)
                {
                    exitToDialog.Open();
                }
            });
        }
    }
}
