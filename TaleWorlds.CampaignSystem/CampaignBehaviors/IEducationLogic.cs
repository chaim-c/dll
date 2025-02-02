using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200039B RID: 923
	public interface IEducationLogic
	{
		// Token: 0x0600378C RID: 14220
		void Finalize(Hero child, List<string> chosenOptions);

		// Token: 0x0600378D RID: 14221
		void GetOptionProperties(Hero child, string optionKey, List<string> previousChoices, out TextObject optionTitle, out TextObject description, out TextObject effect, out ValueTuple<CharacterAttribute, int>[] attributes, out ValueTuple<SkillObject, int>[] skills, out ValueTuple<SkillObject, int>[] focusPoints, out EducationCampaignBehavior.EducationCharacterProperties[] characterProperties);

		// Token: 0x0600378E RID: 14222
		void GetPageProperties(Hero child, List<string> previousChoices, out TextObject title, out TextObject description, out TextObject instruction, out EducationCampaignBehavior.EducationCharacterProperties[] defaultProperties, out string[] availableOptions);

		// Token: 0x0600378F RID: 14223
		void GetStageProperties(Hero child, out int pageCount);

		// Token: 0x06003790 RID: 14224
		bool IsValidEducationNotification(EducationMapNotification educationMapNotification);
	}
}
