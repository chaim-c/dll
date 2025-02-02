using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D6 RID: 726
	public class CommunityGameJoinData
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x00099C77 File Offset: 0x00097E77
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x00099C7F File Offset: 0x00097E7F
		public string Name { get; set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x00099C88 File Offset: 0x00097E88
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x00099C90 File Offset: 0x00097E90
		public PlayerId PlayerId { get; set; }
	}
}
