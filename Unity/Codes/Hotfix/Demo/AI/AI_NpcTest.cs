using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Move2DComponent))]
    public class AI_NpcTest : AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            if (aiConfig.TargetPos != null && aiConfig.TargetPos.Length == 2)
            {
                Unit unit = aiComponent.GetParent<Unit>();
                var moveComponent = unit.GetComponent<Move2DComponent>();
                if (moveComponent == null)
                {
                    return 1;
                }

                var targetPos = new Vector2(aiConfig.TargetPos[0], aiConfig.TargetPos[1]);
                if ((Vector2)unit.Position != targetPos && moveComponent.MoveTimer == 0)
                {
                    return 0;
                }
            }

            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            Unit unit = aiComponent.GetParent<Unit>();
            if (unit == null) return;

            var astarComponent = aiComponent.ZoneScene().CurrentScene().GetComponent<AStarComponent>();
            var steps = new Stack<MovementStep>();
            astarComponent.BuildPath((int)unit.Position.x, (int)unit.Position.y, aiConfig.TargetPos[0], aiConfig.TargetPos[1], steps);

            await unit.MoveToAsync(steps, cancellationToken);
        }
    }
}