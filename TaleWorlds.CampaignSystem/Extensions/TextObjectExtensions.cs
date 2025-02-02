using System;
using Helpers;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Extensions
{
	// Token: 0x02000152 RID: 338
	public static class TextObjectExtensions
	{
		// Token: 0x06001869 RID: 6249 RVA: 0x0007C662 File Offset: 0x0007A862
		public static void SetCharacterProperties(this TextObject to, string tag, CharacterObject character, bool includeDetails = false)
		{
			StringHelpers.SetCharacterProperties(tag, character, to, includeDetails);
		}
	}
}
