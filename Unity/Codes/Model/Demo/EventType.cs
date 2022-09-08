using UnityEngine;

namespace ET
{
    namespace EventType
    {
        public struct AppStart
        {
        }

        public struct SceneChangeStart
        {
            public Scene ZoneScene;
            public string PreviousSceneName;
        }
        
        
        public struct SceneChangeFinish
        {
            public Scene ZoneScene;
            public Scene CurrentScene;
        }

        public class ChangePosition: DisposeObject
        {
            public static readonly ChangePosition Instance = new ChangePosition();

            public Unit Unit;
            public WrapVector3 OldPos = new WrapVector3();

            // 因为是重复利用的，所以用完PublishClass会调用Dispose
            public override void Dispose()
            {
                this.Unit = null;
            }
        }
    

        public class ChangeRotation: DisposeObject
        {
            public static readonly ChangeRotation Instance = new ChangeRotation();
                   
            public Unit Unit;
                   
            // 因为是重复利用的，所以用完PublishClass会调用Dispose
            public override void Dispose()
            {
                this.Unit = null;
            }
        }

        public struct PingChange
        {
            public Scene ZoneScene;
            public long Ping;
        }
        
        public struct AfterCreateZoneScene
        {
            public Scene ZoneScene;
        }
        
        public struct AfterCreateCurrentScene
        {
            public Scene CurrentScene;
        }
        
        public struct AfterCreateLoginScene
        {
            public Scene LoginScene;
        }

        public struct AppStartInitFinish
        {
            public Scene ZoneScene;
        }

        public struct LoginFinish
        {
            public Scene ZoneScene;
        }

        public struct LoadingBegin
        {
            public Scene Scene;
        }

        public struct LoadingFinish
        {
            public Scene Scene;
        }

        public struct EnterMapFinish
        {
            public Scene ZoneScene;
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }

        public struct AfterItemCreate
        {
            public Item Item;
            public bool UsePos;
            public bool SaveInScene;
            public float X;
            public float Y;
        }
        
        public struct MoveStart
        {
            public Unit Unit;
        }

        public struct MoveStop
        {
            public Unit Unit;
        }
        
        public struct RefreshInventory
        {
            public Scene ZoneScene;
        }

        public struct OnItemSelected
        {
            public Scene ZoneScene;
            public Item Item;
            public bool Carried;
        }
        
        public struct UpdateGameSecond
        {
            public Scene ZoneScene;
            public GameTimeComponent Time;
        }
        
        public struct UpdateGameSeason
        {
            public Scene ZoneScene;
            public GameTimeComponent Time;
        }
        
        public struct UpdateGameMinute
        {
            public Scene ZoneScene;
            public GameTimeComponent Time;
        }
        
        public struct UpdateGameHour
        {
            public Scene ZoneScene;
            public GameTimeComponent Time;
        }
        
        public struct UpdateGameDay
        {
            public Scene ZoneScene;
            public GameTimeComponent Time;
        }
    }
}