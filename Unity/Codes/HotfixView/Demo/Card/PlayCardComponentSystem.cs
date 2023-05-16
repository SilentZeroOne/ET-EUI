using ET.EventType;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ET
{
	public class PlayCardComponentAwakeSystem: AwakeSystem<PlayCardComponent>
	{
		public override void Awake(PlayCardComponent self)
		{
			self.Awake();
		}
	}

	public class PlayCardComponentDestroySystem: DestroySystem<PlayCardComponent>
	{
		public override void Destroy(PlayCardComponent self)
		{

		}
	}

	[FriendClass(typeof (PlayCardComponent))]
	public static class PlayCardComponentSystem
	{
		public static void Awake(this PlayCardComponent self)
		{
			self.IsSelected = false;
			self.PointerSelected = false;
			self.MonoBridge = self.Parent.GetComponent<GameObjectComponent>().GameObject.GetComponent<EntityMonoBridge>();
			if (!self.MonoBridge)
			{
				Log.Error($"This card didn't have MonoBridge {self.Parent.Id}");
				return;
			}

			self.MonoBridge.OnPointerClickAction += self.OnPointerClick;
			self.MonoBridge.OnPointerEnterAction += self.OnPointerEnter;
			self.MonoBridge.OnPointerExitAction += self.OnPointerExit;
		}

		public static void OnPointerClick(this PlayCardComponent self, PointerEventData eventData)
		{
			self.Select();
		}
		
		public static void Select(this PlayCardComponent self)
		{
			self.IsSelected = !self.IsSelected;
			var card = Game.EventSystem.Get(self.MonoBridge.BelongToEntity);
			Game.EventSystem.Publish(new CardSelected()
			{
				ZoneScene = self.ZoneScene(), IsSelected = self.IsSelected, CardId = card.Id
			});
		}

		public static void OnPointerEnter(this PlayCardComponent self, PointerEventData eventData)
		{
			if (Pointer.current.press.isPressed)
			{
				self.PointerSelected = true;
				self.Select();
			}
		}

		public static void OnPointerExit(this PlayCardComponent self, PointerEventData eventData)
		{
			if (Pointer.current.press.isPressed && !self.PointerSelected)
			{
				self.Select();
			}

			self.PointerSelected = false;
		}
	}
}