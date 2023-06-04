using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

using Neoner.Mechanics;
using Neoner.Objects;

namespace Neoner.InputSystem
{
    public class PlayerInputs : MonoBehaviour
    {
        [Header("UI")]
        public GameObject NeonIndicator;

        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnToggleNeonRed(InputValue value)
        {
            if (value.isPressed)
            {
                GameObject.Find("GameMaster").GetComponent<GameMaster>().CurrentColor = NeonColor.Red;
                GameObject.Find("GameMaster").GetComponent<GameMaster>().ToggleNeon();
                NeonIndicator.GetComponent<Neoner.UI.Neon>().Toggle(NeonColor.Red);
            }
        }
        public void OnToggleNeonGreen(InputValue value)
        {
            if (value.isPressed)
            {
                GameObject.Find("GameMaster").GetComponent<GameMaster>().CurrentColor = NeonColor.Green;
                GameObject.Find("GameMaster").GetComponent<GameMaster>().ToggleNeon();
                NeonIndicator.GetComponent<Neoner.UI.Neon>().Toggle(NeonColor.Green);
            }
        }
        public void OnToggleNeonLightBlue(InputValue value)
        {
            if (value.isPressed)
            {
                GameObject.Find("GameMaster").GetComponent<GameMaster>().CurrentColor = NeonColor.LightBlue;
                GameObject.Find("GameMaster").GetComponent<GameMaster>().ToggleNeon();
                NeonIndicator.GetComponent<Neoner.UI.Neon>().Toggle(NeonColor.LightBlue);
            }
        }
#else
        // old input sys if we do decide to have it (most likely wont)...
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

#if !UNITY_IOS || !UNITY_ANDROID

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

#endif

    }

}