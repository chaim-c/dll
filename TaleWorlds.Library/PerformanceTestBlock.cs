using System;
using System.Diagnostics;

namespace TaleWorlds.Library
{
	// Token: 0x02000074 RID: 116
	public class PerformanceTestBlock : IDisposable
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000D094 File Offset: 0x0000B294
		public PerformanceTestBlock(string name)
		{
			this._name = name;
			Debug.Print(this._name + " block is started.", 0, Debug.DebugColor.White, 17592186044416UL);
			this._stopwatch = new Stopwatch();
			this._stopwatch.Start();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		void IDisposable.Dispose()
		{
			float num = (float)this._stopwatch.ElapsedMilliseconds / 1000f;
			Debug.Print(string.Concat(new object[]
			{
				this._name,
				" completed in ",
				num,
				" seconds."
			}), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x0400012C RID: 300
		private readonly string _name;

		// Token: 0x0400012D RID: 301
		private readonly Stopwatch _stopwatch;
	}
}
