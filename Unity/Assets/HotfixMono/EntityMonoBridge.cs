using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
	public class EntityMonoBridge: MonoBehaviour,IPointerClickHandler
	{
		//Instance ID
		public long BelongToEntity;

		public Action OnMouseDownAction;
		public Action OnPointerClickAction;

		public void OnMouseDown()
		{
			this.OnMouseDownAction?.Invoke();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnPointerClickAction?.Invoke();
		}
	}
}