using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200035E RID: 862
	public class DuelZoneLandmark : ScriptComponentBehavior, IFocusable
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x000C13BD File Offset: 0x000BF5BD
		public FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.None;
			}
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000C13C0 File Offset: 0x000BF5C0
		public void OnFocusGain(Agent userAgent)
		{
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000C13C2 File Offset: 0x000BF5C2
		public void OnFocusLose(Agent userAgent)
		{
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000C13C4 File Offset: 0x000BF5C4
		public TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			return TextObject.Empty;
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000C13CB File Offset: 0x000BF5CB
		public string GetDescriptionText(GameEntity gameEntity = null)
		{
			return string.Empty;
		}

		// Token: 0x040013EC RID: 5100
		public TroopType ZoneTroopType;
	}
}
