using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001A5 RID: 421
	[ScriptingInterfaceBase]
	internal interface IMBItem
	{
		// Token: 0x060016AB RID: 5803
		[EngineMethod("get_item_usage_index", false)]
		int GetItemUsageIndex(string itemusagename);

		// Token: 0x060016AC RID: 5804
		[EngineMethod("get_item_holster_index", false)]
		int GetItemHolsterIndex(string itemholstername);

		// Token: 0x060016AD RID: 5805
		[EngineMethod("get_item_is_passive_usage", false)]
		bool GetItemIsPassiveUsage(string itemUsageName);

		// Token: 0x060016AE RID: 5806
		[EngineMethod("get_holster_frame_by_index", false)]
		void GetHolsterFrameByIndex(int index, ref MatrixFrame outFrame);

		// Token: 0x060016AF RID: 5807
		[EngineMethod("get_item_usage_set_flags", false)]
		int GetItemUsageSetFlags(string ItemUsageName);

		// Token: 0x060016B0 RID: 5808
		[EngineMethod("get_item_usage_reload_action_code", false)]
		int GetItemUsageReloadActionCode(string itemUsageName, int usageDirection, bool isMounted, int leftHandUsageSetIndex, bool isLeftStance);

		// Token: 0x060016B1 RID: 5809
		[EngineMethod("get_item_usage_strike_type", false)]
		int GetItemUsageStrikeType(string itemUsageName, int usageDirection, bool isMounted, int leftHandUsageSetIndex, bool isLeftStance);

		// Token: 0x060016B2 RID: 5810
		[EngineMethod("get_missile_range", false)]
		float GetMissileRange(float shot_speed, float z_diff);
	}
}
