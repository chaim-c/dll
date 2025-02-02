using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000274 RID: 628
	public class EquipmentControllerLeaveLogic : MissionLogic
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000772FB File Offset: 0x000754FB
		// (set) Token: 0x06002116 RID: 8470 RVA: 0x00077303 File Offset: 0x00075503
		public bool IsEquipmentSelectionActive { get; private set; }

		// Token: 0x06002117 RID: 8471 RVA: 0x0007730C File Offset: 0x0007550C
		public void SetIsEquipmentSelectionActive(bool isActive)
		{
			this.IsEquipmentSelectionActive = isActive;
			Debug.Print("IsEquipmentSelectionActive: " + isActive.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00077337 File Offset: 0x00075537
		public override InquiryData OnEndMissionRequest(out bool canLeave)
		{
			canLeave = !this.IsEquipmentSelectionActive;
			return null;
		}
	}
}
