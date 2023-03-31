using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        public static Unit Create(Scene currentScene, UnitInfo unitInfo)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unitComponent.Add(unit);

	        LandRoomComponent landRoomComponent = currentScene.GetComponent<LandRoomComponent>();
	        landRoomComponent?.AddUnit(unit);
	        
	        //unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
	        //unit.Forward = new Vector3(unitInfo.ForwardX, unitInfo.ForwardY, unitInfo.ForwardZ);
	        
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        for (int i = 0; i < unitInfo.Ks.Count; ++i)
	        {
		        numericComponent.Set(unitInfo.Ks[i], unitInfo.Vs[i]);
	        }

	        unit.AddComponent<ObjectWait>();

	        Game.EventSystem.Publish(new EventType.AfterUnitCreate() { Unit = unit, CreateView = landRoomComponent != null });
            return unit;
        }
    }
}
