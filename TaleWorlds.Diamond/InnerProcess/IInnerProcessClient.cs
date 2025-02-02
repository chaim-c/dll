using System;

namespace TaleWorlds.Diamond.InnerProcess
{
	// Token: 0x02000050 RID: 80
	public interface IInnerProcessClient
	{
		// Token: 0x060001D9 RID: 473
		void EnqueueMessage(Message message);

		// Token: 0x060001DA RID: 474
		void HandleConnected(InnerProcessServerSession serverSession);
	}
}
