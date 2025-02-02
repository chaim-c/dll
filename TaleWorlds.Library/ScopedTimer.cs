using System;
using System.Diagnostics;

namespace TaleWorlds.Library
{
	// Token: 0x02000085 RID: 133
	public class ScopedTimer : IDisposable
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0000FB7F File Offset: 0x0000DD7F
		public ScopedTimer(string scopeName)
		{
			this.scopeName_ = scopeName;
			this.watch_ = new Stopwatch();
			this.watch_.Start();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public void Dispose()
		{
			this.watch_.Stop();
			Console.WriteLine(string.Concat(new object[]
			{
				"ScopedTimer: ",
				this.scopeName_,
				" elapsed ms: ",
				this.watch_.Elapsed.TotalMilliseconds
			}));
		}

		// Token: 0x04000160 RID: 352
		private readonly Stopwatch watch_;

		// Token: 0x04000161 RID: 353
		private readonly string scopeName_;
	}
}
