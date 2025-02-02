﻿using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200022D RID: 557
	public class VlandianTag : ConversationTag
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x000892C7 File Offset: 0x000874C7
		public override string StringId
		{
			get
			{
				return "VlandianTag";
			}
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000892CE File Offset: 0x000874CE
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.Culture.StringId == "vlandia";
		}

		// Token: 0x040009B4 RID: 2484
		public const string Id = "VlandianTag";
	}
}
