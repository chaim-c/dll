using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000051 RID: 81
	public class Job
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00005224 File Offset: 0x00003424
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0000522C File Offset: 0x0000342C
		public bool Finished { get; protected set; }

		// Token: 0x060006F9 RID: 1785 RVA: 0x00005235 File Offset: 0x00003435
		public virtual void DoJob(float dt)
		{
		}
	}
}
