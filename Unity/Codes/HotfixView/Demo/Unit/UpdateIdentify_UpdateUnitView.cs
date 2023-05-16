using BM;
using ET.EventType;
using UnityEngine;
using UnityEngine.U2D;

namespace ET
{
	public class UpdateIdentify_UpdateUnitView: AEvent<UpdateIdentify>
	{
		protected override void Run(UpdateIdentify a)
		{
			var unit = UnitHelper.GetUnit(a.ZoneScene, a.UnitId);
			
			if (a.Idengtify == (int)Identify.LandLord)
			{
				var poker = AssetComponent.Load<SpriteAtlas>("Pokers.spriteatlas".StringToAB());
				unit.GetComponent<GameObjectComponent>().GameObject.GetComponent<SpriteRenderer>().sprite = poker.GetSprite("Role1");
			}
		}
	}
}