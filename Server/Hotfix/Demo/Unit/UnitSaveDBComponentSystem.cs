using System;

namespace ET
{
    public class UnitSaveDBComponentAwakeSystem: AwakeSystem<UnitSaveDBComponent>
    {
        public override void Awake(UnitSaveDBComponent self)
        {
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(10000, TimerType.SaveChangeDBData, self);
        }
    }
    
    public class UnitSaveDBComponentDestroySystem : DestroySystem<UnitSaveDBComponent>
    {
        public override void Destroy(UnitSaveDBComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }
    
    public class UnitAddComponentSystem: AddComponentSystem<Unit>
    {
        public override void AddComponent(Unit self, Entity component)
        {
            Type type = component.GetType();
            if (!(typeof (IUnitCache).IsAssignableFrom(type)))
            {
                return;
            }
            self.GetComponent<UnitSaveDBComponent>()?.AddChange(type);
        }
    }
    
    public class UnitGetComponentSystem : GetComponentSystem<Unit>
    {
        public override void GetComponent(Unit self, Entity component)
        {
            Type type = component.GetType();
            if (!(typeof (IUnitCache).IsAssignableFrom(type)))
            {
                return;
            }
            self.GetComponent<UnitSaveDBComponent>()?.AddChange(type);
        }
    }

    public static class UnitSaveDBComponentSystem
    {
        public static void AddChange(this UnitSaveDBComponent self, Type t)
        {
            self.EntityChangeTypeSet.Add(t);
        }
        
        public static void SaveChange(this UnitSaveDBComponent self)
        {
            if (self.EntityChangeTypeSet.Count <= 0)
            {
                return;
            }

            Unit unit = self.GetParent<Unit>();
            Other2UnitCache_AddOrUpdateUnit message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = unit.Id };
            message.EntityTypes.Add(unit.GetType().FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(unit));
            foreach (var type in self.EntityChangeTypeSet)
            {
                Entity entity = unit.GetComponent(type);
                if (entity == null || entity.IsDisposed)
                {
                    continue;
                }

                Log.Debug("开始保存变化的部分Entity数据: " + type.FullName);
                message.EntityTypes.Add(type.FullName);
                message.EntityBytes.Add(MongoHelper.ToBson(entity));
            }

            self.EntityChangeTypeSet.Clear();
            MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Id).InstanceId, message).Coroutine();
        }
    }

    [Timer(TimerType.SaveChangeDBData)]
    public class UnitSaveDBComponentTimer: ATimer<UnitSaveDBComponent>
    {
        public override void Run(UnitSaveDBComponent self)
        {
            try
            {
                if (self.IsDisposed || self.Parent == null)
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
}