using UnityEngine;

namespace ET
{
	[ComponentOf(typeof(Unit))]
	public class SpriteRendererComponent: Entity, IAwake, IDestroy
	{
		public SpriteRenderer Renderer;
	}
}