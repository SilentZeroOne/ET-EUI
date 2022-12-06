using System;

namespace ET
{
    [Timer(TimerType.SaveChangeDB)]
    public class SaveChangeDBTimer: ATimer<UnitDBSaveComponent>
    {
        public override void Run(UnitDBSaveComponent self)
        {
            try
            {
                if (self.Parent == null || self.IsDisposed)
                {
                    return;
                }

                if (self.DomainScene() == null)
                {
                    return;
                }
                
                self?.SaveChange();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
    
    public class UnitDBSaveComponentAwakeSystem: AwakeSystem<UnitDBSaveComponent>
    {
        public override void Awake(UnitDBSaveComponent self)
        {
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(10000, TimerType.SaveChangeDB, this);
        }
    }

    public class UnitDBSaveComponentDestroySystem: DestroySystem<UnitDBSaveComponent>
    {
        public override void Destroy(UnitDBSaveComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    public class UnitAddComponentSystem: AddComponentSystem<Unit>
    {
        public override void AddComponent(Unit self, Entity component)
        {
            Type type = component.GetType();
            if (!typeof (IUnitCache).IsAssignableFrom(type))
            {
                return;
            }
            
            self.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        }
    }
    
    public class UnitGetComponentSystem : GetComponentSystem<Unit>
    {
        public override void GetComponent(Unit self, Entity component)
        {
            Type type = component.GetType();
            if (!typeof (IUnitCache).IsAssignableFrom(type))
            {
                return;
            }
            
            self.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        }
    }

    [FriendClass(typeof (UnitDBSaveComponent))]
    public static class UnitDBSaveComponentSystem
    {
        public static void AddChange(this UnitDBSaveComponent self,Type t)
        {
            self.EntityChangeSet.Add(t);
        }

        public static void SaveChange(this UnitDBSaveComponent self)
        {
            if (self.EntityChangeSet.Count <= 0) return;

            Unit unit = self.GetParent<Unit>();
            Other2UnitCache_AddOrUpdateUnit message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = unit.Id };
            message.EntityTypes.Add(typeof(Unit).FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(unit));
            foreach (var type in self.EntityChangeSet)
            {
                Entity entity = unit.GetComponent(type);
                if (entity == null || entity.IsDisposed)
                {
                    continue;
                }
                
                Log.Debug("开始保存变化部分的Entity数据 : " + type.FullName);
                message.EntityTypes.Add(type.FullName);
                message.EntityBytes.Add(MongoHelper.ToBson(entity));
            }
            
            self.EntityChangeSet.Clear();
            MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Id).InstanceId, message).Coroutine();
        }
    }
}