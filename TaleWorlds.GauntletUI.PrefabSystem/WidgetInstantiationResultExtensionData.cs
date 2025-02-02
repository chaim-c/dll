using System;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x0200001B RID: 27
	internal struct WidgetInstantiationResultExtensionData
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003F94 File Offset: 0x00002194
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003F9C File Offset: 0x0000219C
		public string Name { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003FA5 File Offset: 0x000021A5
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003FAD File Offset: 0x000021AD
		public bool PassToChildWidgetCreation { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003FB6 File Offset: 0x000021B6
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003FBE File Offset: 0x000021BE
		public object Data { get; set; }
	}
}
