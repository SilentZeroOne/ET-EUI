using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        public static async ETTask<Unit> Create(Scene currentScene, UnitInfo unitInfo)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unitComponent.Add(unit);

	        LandRoomComponent landRoomComponent = currentScene.GetComponent<LandRoomComponent>();
	        landRoomComponent?.AddUnit(unit);

	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        for (int i = 0; i < unitInfo.Ks.Count; ++i)
	        {
		        numericComponent.Set(unitInfo.Ks[i], unitInfo.Vs[i]);
	        }

	        unit.AddComponent<ObjectWait>();

	        if (unitInfo.UnitId != currentScene.ZoneScene().GetComponent<PlayerComponent>().MyId)
		        await UnitHelper.GetRoleInfo(currentScene.ZoneScene(), unitInfo.UnitId);

	        Game.EventSystem.Publish(new EventType.AfterUnitCreate() { Unit = unit, CreateView = landRoomComponent != null });
            return unit;
        }
    }
}
