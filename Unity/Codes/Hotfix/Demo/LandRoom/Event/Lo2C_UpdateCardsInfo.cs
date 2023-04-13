using System;

namespace ET
{
	public class Lo2C_UpdateCardsInfoHandler: AMHandler<Lo2C_UpdateCardsInfo>
	{
		protected override void Run(Session session, Lo2C_UpdateCardsInfo message)
		{
			foreach (var info in message.CardsInfo)
			{
				Log.Debug($"{info.CardSuit} {info.CardWeight}");
			}
		}
	}
}