using System;

namespace TaleWorlds.CampaignSystem.GameMenus
{
	// Token: 0x020000E0 RID: 224
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class GameMenuEventHandler : Attribute
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00058FAD File Offset: 0x000571AD
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x00058FB5 File Offset: 0x000571B5
		public string MenuId { get; private set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00058FBE File Offset: 0x000571BE
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00058FC6 File Offset: 0x000571C6
		public string MenuOptionId { get; private set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00058FCF File Offset: 0x000571CF
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00058FD7 File Offset: 0x000571D7
		public GameMenuEventHandler.EventType Type { get; private set; }

		// Token: 0x060013E6 RID: 5094 RVA: 0x00058FE0 File Offset: 0x000571E0
		public GameMenuEventHandler(string menuId, string menuOptionId, GameMenuEventHandler.EventType type)
		{
			this.MenuId = menuId;
			this.MenuOptionId = menuOptionId;
			this.Type = type;
		}

		// Token: 0x020004EE RID: 1262
		public enum EventType
		{
			// Token: 0x04001526 RID: 5414
			OnCondition,
			// Token: 0x04001527 RID: 5415
			OnConsequence
		}
	}
}
