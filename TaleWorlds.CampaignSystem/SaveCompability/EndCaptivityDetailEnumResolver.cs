﻿using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Resolvers;

namespace TaleWorlds.CampaignSystem.SaveCompability
{
	// Token: 0x020000C1 RID: 193
	public class EndCaptivityDetailEnumResolver : IEnumResolver
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x00055770 File Offset: 0x00053970
		public string ResolveObject(string originalObject)
		{
			if (string.IsNullOrEmpty(originalObject))
			{
				Debug.FailedAssert("EndCaptivityDetail data is null or empty", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\SaveCompability\\EndCaptivityDetailEnumResolver.cs", "ResolveObject", 15);
				return EndCaptivityDetail.ReleasedByChoice.ToString();
			}
			if (originalObject.Equals("EscapeFromLootedParty"))
			{
				return EndCaptivityDetail.ReleasedAfterEscape.ToString();
			}
			if (originalObject.Equals("ReleasedFromPartyScreen"))
			{
				return EndCaptivityDetail.ReleasedByChoice.ToString();
			}
			if (originalObject.Equals("RemovedParty"))
			{
				return EndCaptivityDetail.ReleasedByChoice.ToString();
			}
			return originalObject;
		}
	}
}
