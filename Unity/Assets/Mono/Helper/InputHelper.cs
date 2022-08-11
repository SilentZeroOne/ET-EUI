using UnityEngine;

namespace ET
{
    public static class InputHelper
    {
        public static bool GetKeyDown(int code)
        {
            return Input.GetKeyDown((KeyCode) code);
        }

        public static Vector2 GetMousePosition()
        {
            return Input.mousePosition;
        }
        
        public static bool GetKeyDown(KeyCode code)
        {
            return Input.GetKeyDown(code);
        }

        public static float GetAxisRaw(string axis)
        {
            return Input.GetAxisRaw(axis);
        }
        
        public static float GetXAxisRaw()
        {
            return Input.GetAxisRaw("Horizontal");
        }
        
        public static float GetYAxisRaw()
        {
            return Input.GetAxisRaw("Vertical");
        }
        
        public static float GetAxis(string axis)
        {
            return Input.GetAxis(axis);
        }

        public static bool GetMouseButtonDown(int code)
        {
            return Input.GetMouseButtonDown(code);
        }
    }
}