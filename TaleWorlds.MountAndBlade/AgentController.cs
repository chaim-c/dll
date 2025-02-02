using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020000FC RID: 252
	public class AgentController
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00015B79 File Offset: 0x00013D79
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00015B81 File Offset: 0x00013D81
		public Agent Owner { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00015B8A File Offset: 0x00013D8A
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x00015B92 File Offset: 0x00013D92
		public Mission Mission { get; set; }

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00015B9B File Offset: 0x00013D9B
		public virtual void OnInitialize()
		{
		}
	}
}
