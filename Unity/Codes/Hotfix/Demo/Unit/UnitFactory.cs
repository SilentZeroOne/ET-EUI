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
	        
	        unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
	        unit.Forward = new Vector3(unitInfo.ForwardX, unitInfo.ForwardY, unitInfo.ForwardZ);
	        
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        for (int i = 0; i < unitInfo.Ks.Count; ++i)
	        {
		        numericComponent.Set(unitInfo.Ks[i], unitInfo.Vs[i]);
	        }
	        
	        unit.AddComponent<MoveComponent>();
	        if (unitInfo.MoveInfo != null)
	        {
		        if (unitInfo.MoveInfo.X.Count > 0)
		        {
			        using (ListComponent<Vector3> list = ListComponent<Vector3>.Create())
			        {
				        list.Add(unit.Position);
				        for (int i = 0; i < unitInfo.MoveInfo.X.Count; ++i)
				        {
					        list.Add(new Vector3(unitInfo.MoveInfo.X[i], unitInfo.MoveInfo.Y[i], unitInfo.MoveInfo.Z[i]));
				        }

				        unit.MoveToAsync(list).Coroutine();
			        }
		        }
	        }

	        unit.AddComponent<ObjectWait>();

	        unit.AddComponent<XunLuoPathComponent>();
	        
	        Game.EventSystem.Publish(new EventType.AfterUnitCreate() {Unit = unit});
            return unit;
        }

        /// <summary>
        /// 创建Player
        /// </summary>
        /// <param name="currentScene"></param>
        /// <returns></returns>
        public static Unit Create(Scene currentScene)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChild<Unit, int>(UnitConfigCategory.Instance.GetPlayerConfig().Id);
	        unitComponent.Add(unit);

	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        foreach (var config in PlayerNumericConfigCategory.Instance.GetAll().Values)
	        {
		        numericComponent.Set(config.Id,config.BaseValue);
	        }

	        unit.AddComponent<InventoryComponent>();
	        unit.AddComponent<ObjectWait>();
	        currentScene.ZoneScene().GetComponent<PlayerComponent>().MyId = unit.Id;

	        Game.EventSystem.Publish(new EventType.AfterUnitCreate() {Unit = unit});
	        return unit;
        }

        public static void CreateNPC(Scene currentScene)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        var npcIds = GameSceneConfigCategory.Instance.GetBySceneName(currentScene.Name).NPCsInScene;

	        foreach (var npcId in npcIds)
	        {
		        Unit unit = unitComponent.AddChild<Unit, int>(npcId);
		        unitComponent.Add(unit);
		        
		        //unit.AddComponent<InventoryComponent>();
		        unit.AddComponent<ObjectWait>();
		        unit.AddComponent<Move2DComponent>();
		        unit.AddComponent<AIComponent, int>(3);

		        Game.EventSystem.Publish(new EventType.AfterUnitCreate() {Unit = unit});
	        }
	        
	        

	        //TODO:添加NPC的Numeric
	        // NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        // foreach (var config in PlayerNumericConfigCategory.Instance.GetAll().Values)
	        // {
		       //  numericComponent.Set(config.Id,config.BaseValue);
	        // }
        }
    }
}
