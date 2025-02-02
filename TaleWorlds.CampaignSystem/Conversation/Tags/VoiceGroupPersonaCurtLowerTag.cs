﻿using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000245 RID: 581
	public class VoiceGroupPersonaCurtLowerTag : ConversationTag
	{
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x000896B7 File Offset: 0x000878B7
		public override string StringId
		{
			get
			{
				return "VoiceGroupPersonaCurtLowerTag";
			}
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000896BE File Offset: 0x000878BE
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetPersona() == DefaultTraits.PersonaCurt && ConversationTagHelper.UsesLowRegister(character);
		}

		// Token: 0x040009CC RID: 2508
		public const string Id = "VoiceGroupPersonaCurtLowerTag";
	}
}
