using System;

namespace TaleWorlds.Diamond.InnerProcess
{
	// Token: 0x02000054 RID: 84
	public interface IInnerProcessServer
	{
		// Token: 0x060001EF RID: 495
		InnerProcessServerSession AddNewConnection(IInnerProcessClient client);

		// Token: 0x060001F0 RID: 496
		void Update();
	}
}
