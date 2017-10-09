//Author: Richard Pieterse
//Date: 16 May 2013
//Email: Merrik44@live.com

using UnityEngine;
using System.Collections;

namespace GamepadInput {

    public static class GamePad {

        public enum Button { A, B, Y, X, RightShoulder, LeftShoulder, RightStick, LeftStick, Back, Start }
        public enum Trigger { LeftTrigger, RightTrigger }
        public enum Axis { LeftStick, RightStick, Dpad }
        public enum OS { Windows, Mac }

        private static OS currentOS = OS.Mac;

        public static bool GetButtonDown(Button button) {
            KeyCode code = GetKeycode(button);
            return Input.GetKeyDown(code);
        }

        public static bool GetButtonUp(Button button)
        {
            KeyCode code = GetKeycode(button);
            return Input.GetKeyUp(code);
        }

        public static bool GetButton(Button button)
        {
            KeyCode code = GetKeycode(button);
            return Input.GetKey(code);
        }

        /// <summary>
        /// returns a specified axis
        /// </summary>
        /// <param name="axis">One of the analogue sticks, or the dpad</param>
        /// <param name="controlIndex">The controller number</param>
        /// <param name="raw">if raw is false then the controlIndex will be returned with a deadspot</param>
        /// <returns></returns>
        public static Vector2 GetAxis(Axis axis, bool raw = false) {
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
                currentOS = OS.Windows;

            string xName = "", yName = "";

            switch (axis) {
                case Axis.LeftStick:
                    xName = "L_XAxis";
                    yName = "L_YAxis";
                    break;
                case Axis.RightStick:
                    xName = "R_XAxis" + currentOS.ToString();
                    yName = "R_YAxis" + currentOS.ToString();
                    break;
            }

            Vector2 axisXY = Vector3.zero;

            try {
                if (raw == false) {
                    axisXY.x = Input.GetAxis(xName);
                    axisXY.y = -Input.GetAxis(yName);
                } else {
                    axisXY.x = Input.GetAxisRaw(xName);
                    axisXY.y = -Input.GetAxisRaw(yName);
                }
            } catch (System.Exception e) {
                Debug.LogError(e);
                Debug.LogWarning("Have you set up all axes correctly? \nThe easiest solution is to replace the InputManager.asset with version located in the GamepadInput package. \nWarning: do so will overwrite any existing input");
            }
            return axisXY;
        }

        public static float GetTrigger(Trigger trigger, bool raw = false) {
            //
            string name = "";
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
                currentOS = OS.Windows;

            if (trigger == Trigger.LeftTrigger)
                name = "TriggersL" + currentOS.ToString();
            else if (trigger == Trigger.RightTrigger)
                name = "TriggersR" + currentOS.ToString();

            //
            float axis = 0;
            try {
                if (raw == false)
                    axis = Input.GetAxis(name);
                else
                    axis = Input.GetAxisRaw(name);
            } catch (System.Exception e) {
                Debug.LogError(e);
                Debug.LogWarning("Have you set up all axes correctly? \nThe easiest solution is to replace the InputManager.asset with version located in the GamepadInput package. \nWarning: do so will overwrite any existing input");
            }
            return axis;
        }


        static KeyCode GetKeycode(Button button) {
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
                currentOS = OS.Windows;

            switch (currentOS) {
                case OS.Windows:
                    switch (button) {
                        case Button.A: return KeyCode.JoystickButton0;
                        case Button.B: return KeyCode.JoystickButton1;
                        case Button.X: return KeyCode.JoystickButton2;
                        case Button.Y: return KeyCode.JoystickButton3;
                        case Button.RightShoulder: return KeyCode.JoystickButton5;
                        case Button.LeftShoulder: return KeyCode.JoystickButton4;
                        case Button.Back: return KeyCode.JoystickButton6;
                        case Button.Start: return KeyCode.JoystickButton7;
                        case Button.LeftStick: return KeyCode.JoystickButton8;
                        case Button.RightStick: return KeyCode.JoystickButton9;
                    }
                    break;
                case OS.Mac:
                    switch (button) {
                        case Button.A: return KeyCode.JoystickButton16;
                        case Button.B: return KeyCode.JoystickButton17;
                        case Button.X: return KeyCode.JoystickButton18;
                        case Button.Y: return KeyCode.JoystickButton19;
                        case Button.RightShoulder: return KeyCode.JoystickButton14;
                        case Button.LeftShoulder: return KeyCode.JoystickButton13;
                        case Button.Back: return KeyCode.JoystickButton10;
                        case Button.Start: return KeyCode.JoystickButton9;
                        case Button.LeftStick: return KeyCode.JoystickButton11;
                        case Button.RightStick: return KeyCode.JoystickButton12;
                    }
                    break;
                }

            return KeyCode.None;
        }

        public static GamepadState GetState(bool raw = false) {
            GamepadState state = new GamepadState();

            state.A = GetButton(Button.A);
            state.B = GetButton(Button.B);
            state.Y = GetButton(Button.Y);
            state.X = GetButton(Button.X);

            state.RightShoulder = GetButton(Button.RightShoulder);
            state.LeftShoulder = GetButton(Button.LeftShoulder);
            state.RightStick = GetButton(Button.RightStick);
            state.LeftStick = GetButton(Button.LeftStick);

            state.Start = GetButton(Button.Start);
            state.Back = GetButton(Button.Back);

            state.LeftStickAxis = GetAxis(Axis.LeftStick, raw);
            state.rightStickAxis = GetAxis(Axis.RightStick, raw);
            state.dPadAxis = GetAxis(Axis.Dpad, raw);

            state.Left = (state.dPadAxis.x < 0);
            state.Right = (state.dPadAxis.x > 0);
            state.Up = (state.dPadAxis.y > 0);
            state.Down = (state.dPadAxis.y < 0);

            state.LeftTrigger = GetTrigger(Trigger.LeftTrigger, raw);
            state.RightTrigger = GetTrigger(Trigger.RightTrigger, raw);

            return state;
        }

    }

    public class GamepadState {
        public bool A = false;
        public bool B = false;
        public bool X = false;
        public bool Y = false;
        public bool Start = false;
        public bool Back = false;
        public bool Left = false;
        public bool Right = false;
        public bool Up = false;
        public bool Down = false;
        public bool LeftStick = false;
        public bool RightStick = false;
        public bool RightShoulder = false;
        public bool LeftShoulder = false;

        public Vector2 LeftStickAxis = Vector2.zero;
        public Vector2 rightStickAxis = Vector2.zero;
        public Vector2 dPadAxis = Vector2.zero;

        public float LeftTrigger = 0;
        public float RightTrigger = 0;

    }

}