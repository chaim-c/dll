using System;
using System.Threading.Tasks;

namespace TaleWorlds.Library
{
	// Token: 0x0200000E RID: 14
	public abstract class AwaitableAsyncRunner
	{
		// Token: 0x06000035 RID: 53
		public abstract Task RunAsync();

		// Token: 0x06000036 RID: 54
		public abstract void OnTick(float dt);
	}
}
