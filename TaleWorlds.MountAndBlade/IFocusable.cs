using System;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200035F RID: 863
	public interface IFocusable
	{
		// Token: 0x06002F32 RID: 12082
		void OnFocusGain(Agent userAgent);

		// Token: 0x06002F33 RID: 12083
		void OnFocusLose(Agent userAgent);

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002F34 RID: 12084
		FocusableObjectType FocusableObjectType { get; }

		// Token: 0x06002F35 RID: 12085
		TextObject GetInfoTextForBeingNotInteractable(Agent userAgent);

		// Token: 0x06002F36 RID: 12086
		string GetDescriptionText(GameEntity gameEntity = null);
	}
}
