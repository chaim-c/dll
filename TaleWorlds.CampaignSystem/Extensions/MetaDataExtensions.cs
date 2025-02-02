using System;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Extensions
{
	// Token: 0x02000154 RID: 340
	public static class MetaDataExtensions
	{
		// Token: 0x0600186E RID: 6254 RVA: 0x0007C700 File Offset: 0x0007A900
		public static string GetUniqueGameId(this MetaData metaData)
		{
			string result;
			if (metaData == null || !metaData.TryGetValue("UniqueGameId", out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0007C728 File Offset: 0x0007A928
		public static int GetMainHeroLevel(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("MainHeroLevel", out s))
			{
				return 0;
			}
			return int.Parse(s);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0007C750 File Offset: 0x0007A950
		public static float GetMainPartyFood(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("MainPartyFood", out s))
			{
				return 0f;
			}
			return float.Parse(s);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0007C77C File Offset: 0x0007A97C
		public static int GetMainHeroGold(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("MainHeroGold", out s))
			{
				return 0;
			}
			return int.Parse(s);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0007C7A4 File Offset: 0x0007A9A4
		public static float GetClanInfluence(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("ClanInfluence", out s))
			{
				return 0f;
			}
			return float.Parse(s);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0007C7D0 File Offset: 0x0007A9D0
		public static int GetClanFiefs(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("ClanFiefs", out s))
			{
				return 0;
			}
			return int.Parse(s);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0007C7F8 File Offset: 0x0007A9F8
		public static int GetMainPartyHealthyMemberCount(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("MainPartyHealthyMemberCount", out s))
			{
				return 0;
			}
			return int.Parse(s);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0007C820 File Offset: 0x0007AA20
		public static int GetMainPartyPrisonerMemberCount(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("MainPartyPrisonerMemberCount", out s))
			{
				return 0;
			}
			return int.Parse(s);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0007C848 File Offset: 0x0007AA48
		public static int GetMainPartyWoundedMemberCount(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("MainPartyWoundedMemberCount", out s))
			{
				return 0;
			}
			return int.Parse(s);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0007C870 File Offset: 0x0007AA70
		public static string GetClanBannerCode(this MetaData metaData)
		{
			string result;
			if (metaData == null || !metaData.TryGetValue("ClanBannerCode", out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0007C898 File Offset: 0x0007AA98
		public static string GetCharacterName(this MetaData metaData)
		{
			string result;
			if (metaData == null || !metaData.TryGetValue("CharacterName", out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0007C8C0 File Offset: 0x0007AAC0
		public static string GetCharacterVisualCode(this MetaData metaData)
		{
			string result;
			if (metaData == null || !metaData.TryGetValue("MainHeroVisual", out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0007C8E8 File Offset: 0x0007AAE8
		public static double GetDayLong(this MetaData metaData)
		{
			string s;
			if (metaData == null || !metaData.TryGetValue("DayLong", out s))
			{
				return 0.0;
			}
			return double.Parse(s);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0007C918 File Offset: 0x0007AB18
		public static bool GetIronmanMode(this MetaData metaData)
		{
			string s;
			int num;
			return metaData != null && metaData.TryGetValue("IronmanMode", out s) && int.TryParse(s, out num) && num == 1;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0007C948 File Offset: 0x0007AB48
		public static int GetPlayerHealthPercentage(this MetaData metaData)
		{
			string s;
			int result;
			if (metaData == null || !metaData.TryGetValue("HealthPercentage", out s) || !int.TryParse(s, out result))
			{
				return 100;
			}
			return result;
		}
	}
}
