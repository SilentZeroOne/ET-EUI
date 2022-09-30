using System.Collections.Generic;
using ET.EventType;
using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GridMapManageComponent))]
    public class LeftMouseClick_AStarPathfinding : AEvent<LeftMouseClick>
    {
        protected override void Run(LeftMouseClick a)
        {
            var aStar = a.ZoneScene.CurrentScene().GetComponent<AStarComponent>();
            if (aStar != null)
            {
                Unit player = UnitHelper.GetMyUnitFromZoneScene(a.ZoneScene);
                Transform playTran = player.GetComponent<GameObjectComponent>().GameObject.transform;
                var steps = new Stack<MovementStep>();
                var gridManager = a.ZoneScene.CurrentScene().GetComponent<GridMapManageComponent>();
                var cellPos = gridManager.CurrentGrid.WorldToCell(new Vector3(a.X, a.Y, 0));
                var playerCellPos=gridManager.CurrentGrid.WorldToCell(playTran.position);

                aStar.BuildPath(playerCellPos.x, playerCellPos.y, cellPos.x, cellPos.y, steps);

                while (steps.Count > 0)
                {
                    var step = steps.Pop();

                    Log.Debug($"X:{step.GridX} Y:{step.GridY}");
                    gridManager.SetDigTile(step.GridX, step.GridY);
                }
            }
        }
    }
}