using UnityEngine;

namespace ET
{
    public class MovementStep
    {
        public string SceneName;
        public int Hour;
        public int Minute;
        public int Second;
        public int GridX;
        public int GridY;

        public Vector2 Position => new Vector2(this.GridX, this.GridY);
    }
}