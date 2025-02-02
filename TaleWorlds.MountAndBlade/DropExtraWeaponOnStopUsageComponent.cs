using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200035D RID: 861
	internal class DropExtraWeaponOnStopUsageComponent : UsableMissionObjectComponent
	{
		// Token: 0x06002F2A RID: 12074 RVA: 0x000C1374 File Offset: 0x000BF574
		protected internal override void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
			if (isSuccessful && !GameNetwork.IsClientOrReplay && !userAgent.Equipment[EquipmentIndex.ExtraWeaponSlot].IsEmpty && !Mission.Current.MissionIsEnding)
			{
				userAgent.DropItem(EquipmentIndex.ExtraWeaponSlot, WeaponClass.Undefined);
			}
		}
	}
}
