using System;
using System.Runtime.Serialization;
using TaleWorlds.Diamond;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E4 RID: 228
	[MessageDescription("BattleServerManager", "BattleServer")]
	[DataContract]
	[Serializable]
	public class BattleServerReadyResponseMessage : LoginResultObject
	{
	}
}
