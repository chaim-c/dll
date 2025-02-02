using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.SceneInformationPopupTypes
{
	// Token: 0x020000B0 RID: 176
	public class FindingSecondBannerPieceSceneNotificationItem : SceneNotificationData
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x00053392 File Offset: 0x00051592
		public Hero PlayerHero { get; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0005339A File Offset: 0x0005159A
		public override string SceneID
		{
			get
			{
				return "scn_second_banner_piece_notification";
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000533A4 File Offset: 0x000515A4
		public override TextObject TitleText
		{
			get
			{
				GameTexts.SetVariable("DAY_OF_YEAR", CampaignSceneNotificationHelper.GetFormalDayAndSeasonText(this._creationCampaignTime));
				GameTexts.SetVariable("YEAR", this._creationCampaignTime.GetYear);
				return GameTexts.FindText("str_second_banner_piece_found", null);
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000533E9 File Offset: 0x000515E9
		public override IEnumerable<Banner> GetBanners()
		{
			return new List<Banner>
			{
				this.PlayerHero.ClanBanner
			};
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00053401 File Offset: 0x00051601
		public FindingSecondBannerPieceSceneNotificationItem(Hero playerHero)
		{
			this.PlayerHero = playerHero;
			this._creationCampaignTime = CampaignTime.Now;
		}

		// Token: 0x04000624 RID: 1572
		private readonly CampaignTime _creationCampaignTime;
	}
}
