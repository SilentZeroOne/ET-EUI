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
            public bool CreateView;
        }

        public struct AfterUnitRemove
        {
            public Unit Unit;
        }
        
        public struct MoveStart
        {
            public Unit Unit;
        }

        public struct MoveStop
        {
            public Unit Unit;
        }

        public struct UnitReady
        {
            public Scene ZoneScene;
            public int UnitIndex;
            public int Ready;
        }
        
        public struct AfterCardCreate
        {
            public Scene ZoneScene;
            public Card Card;
            public bool CreateView;
        }
        
        public struct AfterCardCreateEnd
        {
            public Scene ZoneScene;
        }
        
        public struct AfterLordCardCreate
        {
            public Scene ZoneScene;
            public Card Card;
        }
        
        public struct StartPlayCard
        {
            public Scene ZoneScene;
            public bool PlayingCard;
            public bool IsSelf;
        }

        public struct RobLandLord
        {
            public Scene ZoneScene;
            public bool Rob;
            public int UnitIndex;
        }
        
        public struct SetMutiples
        {
            public Scene ZoneScene;
            public int Mutiples;
        }

        public struct CardSelected
        {
            public Scene ZoneScene;
            public bool IsSelected;
            public long CardId;
        }
    }
}