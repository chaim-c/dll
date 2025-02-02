using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200006F RID: 111
	public class CampaignTutorial
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0004623B File Offset: 0x0004443B
		public TextObject Description
		{
			get
			{
				return GameTexts.FindText("str_campaign_tutorial_description", this.TutorialTypeId);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0004624D File Offset: 0x0004444D
		public TextObject Title
		{
			get
			{
				return GameTexts.FindText("str_campaign_tutorial_title", this.TutorialTypeId);
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0004625F File Offset: 0x0004445F
		public CampaignTutorial(string tutorialType, int priority)
		{
			this.TutorialTypeId = tutorialType;
			this.Priority = priority;
		}

		// Token: 0x04000450 RID: 1104
		public readonly string TutorialTypeId;

		// Token: 0x04000451 RID: 1105
		public readonly int Priority;
	}
}
