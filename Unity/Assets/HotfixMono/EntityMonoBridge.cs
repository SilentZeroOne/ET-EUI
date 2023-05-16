using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
	public class EntityMonoBridge: MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
	{
		//Instance ID
		public long BelongToEntity;

		public Action OnMouseDownAction;
		public Action<PointerEventData> OnPointerClickAction;
		public Action<PointerEventData> OnPointerEnterAction;
		public Action<PointerEventData> OnPointerExitAction;
		
		public Action<PointerEventData> OnBeginDragAction;
		public Action<PointerEventData> OnEndDragAction;
		public void OnMouseDown()
		{
			this.OnMouseDownAction?.Invoke();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnPointerClickAction?.Invoke(eventData);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			OnBeginDragAction?.Invoke(eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			OnEndDragAction?.Invoke(eventData);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			OnPointerEnterAction?.Invoke(eventData);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			OnPointerExitAction?.Invoke(eventData);
		}
	}
}