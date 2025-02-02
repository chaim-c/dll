using System;
using System.Collections.Generic;

namespace TaleWorlds.Engine
{
	// Token: 0x02000050 RID: 80
	public class JobManager
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x0000513F File Offset: 0x0000333F
		public JobManager()
		{
			this._jobs = new List<Job>();
			this._locker = new object();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00005160 File Offset: 0x00003360
		public void AddJob(Job job)
		{
			object locker = this._locker;
			lock (locker)
			{
				this._jobs.Add(job);
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000051A8 File Offset: 0x000033A8
		internal void OnTick(float dt)
		{
			object locker = this._locker;
			lock (locker)
			{
				for (int i = 0; i < this._jobs.Count; i++)
				{
					Job job = this._jobs[i];
					job.DoJob(dt);
					if (job.Finished)
					{
						this._jobs.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x040000A6 RID: 166
		private List<Job> _jobs;

		// Token: 0x040000A7 RID: 167
		private object _locker;
	}
}
