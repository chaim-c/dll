using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001AA RID: 426
	[ScriptingInterfaceBase]
	internal interface IMBPeer
	{
		// Token: 0x06001765 RID: 5989
		[EngineMethod("set_user_data", false)]
		void SetUserData(int index, MBNetworkPeer data);

		// Token: 0x06001766 RID: 5990
		[EngineMethod("set_controlled_agent", false)]
		void SetControlledAgent(int index, UIntPtr missionPointer, int agentIndex);

		// Token: 0x06001767 RID: 5991
		[EngineMethod("set_team", false)]
		void SetTeam(int index, int teamIndex);

		// Token: 0x06001768 RID: 5992
		[EngineMethod("is_active", false)]
		bool IsActive(int index);

		// Token: 0x06001769 RID: 5993
		[EngineMethod("set_is_synchronized", false)]
		void SetIsSynchronized(int index, bool value);

		// Token: 0x0600176A RID: 5994
		[EngineMethod("get_is_synchronized", false)]
		bool GetIsSynchronized(int index);

		// Token: 0x0600176B RID: 5995
		[EngineMethod("send_existing_objects", false)]
		void SendExistingObjects(int index, UIntPtr missionPointer);

		// Token: 0x0600176C RID: 5996
		[EngineMethod("begin_module_event", false)]
		void BeginModuleEvent(int index, bool isReliable);

		// Token: 0x0600176D RID: 5997
		[EngineMethod("end_module_event", false)]
		void EndModuleEvent(bool isReliable);

		// Token: 0x0600176E RID: 5998
		[EngineMethod("get_average_ping_in_milliseconds", false)]
		double GetAveragePingInMilliseconds(int index);

		// Token: 0x0600176F RID: 5999
		[EngineMethod("get_average_loss_percent", false)]
		double GetAverageLossPercent(int index);

		// Token: 0x06001770 RID: 6000
		[EngineMethod("set_relevant_game_options", false)]
		void SetRelevantGameOptions(int index, bool sendMeBloodEvents, bool sendMeSoundEvents);

		// Token: 0x06001771 RID: 6001
		[EngineMethod("get_reversed_host", false)]
		uint GetReversedHost(int index);

		// Token: 0x06001772 RID: 6002
		[EngineMethod("get_host", false)]
		uint GetHost(int index);

		// Token: 0x06001773 RID: 6003
		[EngineMethod("get_port", false)]
		ushort GetPort(int index);
	}
}
