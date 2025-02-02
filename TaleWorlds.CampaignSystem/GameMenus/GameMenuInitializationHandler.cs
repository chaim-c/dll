using System;

namespace TaleWorlds.CampaignSystem.GameMenus
{
	// Token: 0x020000E1 RID: 225
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class GameMenuInitializationHandler : Attribute
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00058FFD File Offset: 0x000571FD
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00059005 File Offset: 0x00057205
		public string MenuId { get; private set; }

		// Token: 0x060013E9 RID: 5097 RVA: 0x0005900E File Offset: 0x0005720E
		public GameMenuInitializationHandler(string menuId)
		{
			this.MenuId = menuId;
		}
	}
}
