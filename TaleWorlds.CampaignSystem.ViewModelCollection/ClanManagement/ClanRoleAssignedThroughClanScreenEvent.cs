using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200010F RID: 271
	public class ClanRoleAssignedThroughClanScreenEvent : EventBase
	{
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0005E8A2 File Offset: 0x0005CAA2
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x0005E8AA File Offset: 0x0005CAAA
		public SkillEffect.PerkRole Role { get; private set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0005E8B3 File Offset: 0x0005CAB3
		// (set) Token: 0x06001A2B RID: 6699 RVA: 0x0005E8BB File Offset: 0x0005CABB
		public Hero HeroObject { get; private set; }

		// Token: 0x06001A2C RID: 6700 RVA: 0x0005E8C4 File Offset: 0x0005CAC4
		public ClanRoleAssignedThroughClanScreenEvent(SkillEffect.PerkRole role, Hero heroObject)
		{
			this.Role = role;
			this.HeroObject = heroObject;
		}
	}
}
