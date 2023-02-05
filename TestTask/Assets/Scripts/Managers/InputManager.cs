using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IgnSDK
{
    public class InputManager : MonoBehaviour
    {
        public PlayerInput InputActions;

        // Events

        public Action<Vector2> OnNavigateAction;
        public Action<bool> OnJumpAction;
        public Action OnCancelAction;
        public Action OnMenuAction;
        public Action OnSelectAction;
        public Action OnClickAction;
        public Action OnRightClickAction;
        public Action OnSubmitAction;
        public Action OnPointAction;
        public Action OnScrollAction;

        // InputManager

        public void OnMove(InputAction.CallbackContext context)
        {
            var inputVector = context.ReadValue<Vector2>();

            OnNavigateAction?.Invoke(inputVector);
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            var inputVector = context.ReadValue<Vector2>();

            OnNavigateAction?.Invoke(inputVector);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            OnJumpAction(context.started);  
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnCancelAction?.Invoke();
            }
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnClickAction?.Invoke();
            }
        }

        public void OnScroll(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnScrollAction?.Invoke();
            }
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnPointAction?.Invoke();
            }
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnRightClickAction?.Invoke();
            }
        }

        public void OnMenu(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnMenuAction?.Invoke();
            }
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnSubmitAction?.Invoke();
            }
        }

        public void SetActionMap(InputActionMap actionMap)
        {
            InputActions.SwitchCurrentActionMap(actionMap.ToString());
        }
    }

    public enum InputActionMap
    {
        // Case sensitive, should match PlayerInput action maps.

        UI,
        Gameplay 
    }
}
