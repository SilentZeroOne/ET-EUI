

using System;

namespace ET
{
	public class Lo2C_NoticeNumericHandler: AMHandler<Lo2C_NoticeNumeric>
	{
		protected override void Run(Session session, Lo2C_NoticeNumeric message)
		{
			session.ZoneScene()?.CurrentScene()?.GetComponent<UnitComponent>()?.Get(message.UnitId)?.GetComponent<NumericComponent>()
					?.Set(message.NumericType, message.NewValue);
		}
	}
}