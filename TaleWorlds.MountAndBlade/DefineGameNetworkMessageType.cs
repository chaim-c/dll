using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002DF RID: 735
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class DefineGameNetworkMessageType : Attribute
	{
		// Token: 0x06002804 RID: 10244 RVA: 0x0009A410 File Offset: 0x00098610
		public DefineGameNetworkMessageType(GameNetworkMessageSendType sendType)
		{
			this.SendType = sendType;
		}

		// Token: 0x04000F37 RID: 3895
		public readonly GameNetworkMessageSendType SendType;
	}
}
