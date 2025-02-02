using System;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement
{
	// Token: 0x0200005A RID: 90
	public class LeaveKingdomPermissionEvent : EventBase
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00020A51 File Offset: 0x0001EC51
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x00020A59 File Offset: 0x0001EC59
		public Action<bool, TextObject> IsLeaveKingdomPossbile { get; private set; }

		// Token: 0x06000772 RID: 1906 RVA: 0x00020A62 File Offset: 0x0001EC62
		public LeaveKingdomPermissionEvent(Action<bool, TextObject> isLeaveKingdomPossbile)
		{
			this.IsLeaveKingdomPossbile = isLeaveKingdomPossbile;
		}
	}
}
