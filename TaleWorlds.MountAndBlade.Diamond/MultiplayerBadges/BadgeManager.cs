using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x0200016F RID: 367
	public static class BadgeManager
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0001044F File Offset: 0x0000E64F
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00010456 File Offset: 0x0000E656
		public static List<Badge> Badges { get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0001045E File Offset: 0x0000E65E
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x00010465 File Offset: 0x0000E665
		public static bool IsInitialized { get; private set; }

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001046D File Offset: 0x0000E66D
		public static void InitializeWithXML(string xmlPath)
		{
			Debug.Print("BadgeManager::InitializeWithXML", 0, Debug.DebugColor.White, 17592186044416UL);
			if (BadgeManager.IsInitialized)
			{
				return;
			}
			BadgeManager.LoadFromXml(xmlPath);
			BadgeManager.IsInitialized = true;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001049C File Offset: 0x0000E69C
		public static void OnFinalize()
		{
			Debug.Print("BadgeManager::OnFinalize", 0, Debug.DebugColor.White, 17592186044416UL);
			if (!BadgeManager.IsInitialized)
			{
				return;
			}
			BadgeManager._badgesById.Clear();
			BadgeManager._badgesByType.Clear();
			BadgeManager.Badges.Clear();
			BadgeManager._badgesById = null;
			BadgeManager._badgesByType = null;
			BadgeManager.Badges = null;
			BadgeManager.IsInitialized = false;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00010500 File Offset: 0x0000E700
		private static void LoadFromXml(string path)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (StreamReader streamReader = new StreamReader(path))
			{
				string xml = streamReader.ReadToEnd();
				xmlDocument.LoadXml(xml);
				streamReader.Close();
			}
			BadgeManager._badgesById = new Dictionary<string, Badge>();
			BadgeManager._badgesByType = new Dictionary<BadgeType, List<Badge>>();
			BadgeManager.Badges = new List<Badge>();
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Badges")
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name == "Badge")
						{
							BadgeType badgeType = BadgeType.Custom;
							if (!Enum.TryParse<BadgeType>(xmlNode2.Attributes["type"].Value, true, out badgeType))
							{
								Debug.FailedAssert("No 'type' was provided for a badge", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "LoadFromXml", 82);
							}
							Badge badge = null;
							if (badgeType > BadgeType.OnLogin)
							{
								if (badgeType == BadgeType.Conditional)
								{
									badge = new ConditionalBadge(BadgeManager.Badges.Count, badgeType);
								}
							}
							else
							{
								badge = new Badge(BadgeManager.Badges.Count, badgeType);
							}
							badge.Deserialize(xmlNode2);
							BadgeManager._badgesById[badge.StringId] = badge;
							BadgeManager.Badges.Add(badge);
							List<Badge> list;
							if (!BadgeManager._badgesByType.TryGetValue(badgeType, out list))
							{
								list = new List<Badge>();
								BadgeManager._badgesByType.Add(badgeType, list);
							}
							list.Add(badge);
						}
					}
				}
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001071C File Offset: 0x0000E91C
		public static Badge GetByIndex(int index)
		{
			if (index == -1 || BadgeManager.Badges == null || BadgeManager.Badges.Count <= index || index < 0)
			{
				return null;
			}
			return BadgeManager.Badges[index];
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00010748 File Offset: 0x0000E948
		public static Badge GetById(string id)
		{
			Badge result;
			if (id == null || !BadgeManager._badgesById.TryGetValue(id, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001076C File Offset: 0x0000E96C
		public static List<Badge> GetByType(BadgeType type)
		{
			List<Badge> list;
			if (!BadgeManager._badgesByType.TryGetValue(type, out list))
			{
				list = new List<Badge>();
				BadgeManager._badgesByType.Add(type, list);
			}
			return list;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001079C File Offset: 0x0000E99C
		public static string GetBadgeConditionValue(this PlayerData playerData, BadgeCondition condition)
		{
			if (playerData == null)
			{
				Debug.FailedAssert("PlayerData is null on get value", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "GetBadgeConditionValue", 143);
				return "";
			}
			string a;
			if (!condition.Parameters.TryGetValue("property", out a))
			{
				Debug.FailedAssert("Condition with type PlayerData does not have Property parameter", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "GetBadgeConditionValue", 150);
				return "";
			}
			if (a == "ShownBadgeId")
			{
				return playerData.ShownBadgeId;
			}
			return "";
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00010818 File Offset: 0x0000EA18
		public static int GetBadgeConditionNumericValue(this PlayerData playerData, BadgeCondition condition)
		{
			if (playerData == null)
			{
				Debug.FailedAssert("PlayerData is null on get value", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "GetBadgeConditionNumericValue", 167);
				return 0;
			}
			string text;
			if (!condition.Parameters.TryGetValue("property", out text))
			{
				Debug.FailedAssert("Condition with type PlayerDataNumeric does not have Property parameter", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "GetBadgeConditionNumericValue", 174);
				return 0;
			}
			int result = 0;
			string[] array = text.Split(new char[]
			{
				'.'
			});
			string text2 = array[0];
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
			if (num <= 1096112509U)
			{
				if (num <= 267161228U)
				{
					if (num != 192547213U)
					{
						if (num == 267161228U)
						{
							if (text2 == "Stats")
							{
								if (array.Length == 3 && playerData.Stats != null)
								{
									string b = array[1].Trim().ToLower();
									PlayerStatsBase[] stats = playerData.Stats;
									int i = 0;
									while (i < stats.Length)
									{
										PlayerStatsBase playerStatsBase = stats[i];
										if (playerStatsBase.GameType.Trim().ToLower() == b)
										{
											text2 = array[2];
											if (text2 == "KillCount")
											{
												result = playerStatsBase.KillCount;
												break;
											}
											if (text2 == "DeathCount")
											{
												result = playerStatsBase.DeathCount;
												break;
											}
											if (text2 == "AssistCount")
											{
												result = playerStatsBase.AssistCount;
												break;
											}
											if (text2 == "WinCount")
											{
												result = playerStatsBase.WinCount;
												break;
											}
											if (!(text2 == "LoseCount"))
											{
												break;
											}
											result = playerStatsBase.LoseCount;
											break;
										}
										else
										{
											i++;
										}
									}
								}
							}
						}
					}
					else if (text2 == "AssistCount")
					{
						result = playerData.AssistCount;
					}
				}
				else if (num != 1093842208U)
				{
					if (num == 1096112509U)
					{
						if (text2 == "Level")
						{
							result = playerData.Level;
						}
					}
				}
				else if (text2 == "WinCount")
				{
					result = playerData.WinCount;
				}
			}
			else if (num <= 2667250970U)
			{
				if (num != 1128891543U)
				{
					if (num == 2667250970U)
					{
						if (text2 == "Playtime")
						{
							result = playerData.Playtime;
						}
					}
				}
				else if (text2 == "LoseCount")
				{
					result = playerData.LoseCount;
				}
			}
			else if (num != 3945868512U)
			{
				if (num == 4058818476U)
				{
					if (text2 == "DeathCount")
					{
						result = playerData.DeathCount;
					}
				}
			}
			else if (text2 == "KillCount")
			{
				result = playerData.KillCount;
			}
			return result;
		}

		// Token: 0x040004D8 RID: 1240
		public const string PropertyParameterName = "property";

		// Token: 0x040004D9 RID: 1241
		public const string ValueParameterName = "value";

		// Token: 0x040004DA RID: 1242
		public const string MinValueParameterName = "min_value";

		// Token: 0x040004DB RID: 1243
		public const string MaxValueParameterName = "max_value";

		// Token: 0x040004DC RID: 1244
		public const string IsBestParameterName = "is_best";

		// Token: 0x040004DF RID: 1247
		private static Dictionary<string, Badge> _badgesById;

		// Token: 0x040004E0 RID: 1248
		private static Dictionary<BadgeType, List<Badge>> _badgesByType;
	}
}
