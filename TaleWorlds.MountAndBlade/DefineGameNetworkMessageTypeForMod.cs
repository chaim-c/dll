using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002DE RID: 734
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefineGameNetworkMessageTypeForMod : Attribute
	{
		// Token: 0x06002803 RID: 10243 RVA: 0x0009A401 File Offset: 0x00098601
		public DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType sendType)
		{
			this.SendType = sendType;
		}

		// Token: 0x04000F36 RID: 3894
		public readonly GameNetworkMessageSendType SendType;
	}
}
