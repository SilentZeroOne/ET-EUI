using System;
using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    //ChildType测试代码 取消注释 编译Server.hotfix 可发现报错
                    //unitComponent.AddChild<Player, string>("Player");
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    foreach (var config in PlayerNumericConfigCategory.Instance.GetAll())
                    {
                        if (config.Value.BaseValue == 0) continue;
                        
                        //大于3000 直接赋值
                        if (config.Key > 3000)
                        {
                            numericComponent.SetNoEvent(config.Key, config.Value.BaseValue);
                        }
                        else
                        {
                            //否则使用推导值
                            int baseKey = config.Key * 10 + 1;
                            numericComponent.SetNoEvent(baseKey, config.Value.BaseValue);
                        }
                    }

                    //WORKFLOW: 所有需要初始化添加到unit上的component都在这里添加
                    
                    unitComponent.Add(unit);
                    // // 加入aoi
                    // unit.AddComponent<AOIEntity, int, Vector3>(9 * 1000, unit.Position);
                    return unit;
                }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }
    }
}