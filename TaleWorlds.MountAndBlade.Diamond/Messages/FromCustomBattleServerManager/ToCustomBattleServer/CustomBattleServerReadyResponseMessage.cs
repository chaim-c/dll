using System;
using System.Runtime.Serialization;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x02000069 RID: 105
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer")]
	[DataContract]
	[Serializable]
	public class CustomBattleServerReadyResponseMessage : LoginResultObject
	{
	}
}
