using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool roll;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        [Header("Keybinds")]
        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode rollKey = KeyCode.LeftAlt;

        //#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        //		public void OnMove(InputValue value)
        //		{
        //			MoveInput(value.Get<Vector2>());
        //		}

        //		public void OnLook(InputValue value)
        //		{
        //			if(cursorInputForLook)
        //			{
        //				LookInput(value.Get<Vector2>());
        //			}
        //		}

        //		public void OnJump(InputValue value)
        //		{
        //			JumpInput(value.isPressed);
        //		}

        //		public void OnSprint(InputValue value)
        //		{
        //			SprintInput(value.isPressed);
        //		}
        //#endif

        private void Update()
        {
            var hor = Input.GetAxisRaw("Horizontal");
            var ver = Input.GetAxisRaw("Vertical");
            var direction = new Vector2(hor, ver).normalized;
            MoveInput(direction);


            JumpInput(Input.GetKey(jumpKey));
            RollInput(Input.GetKeyDown(rollKey));
            SprintInput(Input.GetKey(sprintKey));

            //if (Input.GetKey(jumpKey)) { JumpInput(true); }
            //if (Input.GetKey(rollKey)) { RollInput(true); }
            //if (Input.GetKey(sprintKey)) { SprintInput(true); }
        }


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

        public void RollInput(bool newRollState)
        {
            roll = newRollState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}