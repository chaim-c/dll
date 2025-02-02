using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.ScreenSystem;

namespace SandBox.View
{
	// Token: 0x0200000A RID: 10
	public static class SandBoxViewCheats
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003A54 File Offset: 0x00001C54
		[CommandLineFunctionality.CommandLineArgumentFunction("illumination", "global")]
		private static string TryGlobalIllumination(List<string> values)
		{
			string text = "";
			foreach (Settlement settlement in MBObjectManager.Instance.GetObjectTypeList<Settlement>())
			{
				if (settlement.Culture != null && settlement.MapFaction != null)
				{
					string[] array = new string[5];
					array[0] = text;
					int num = 1;
					Vec2 position2D = settlement.Position2D;
					array[num] = position2D.x.ToString();
					array[2] = ",";
					int num2 = 3;
					position2D = settlement.Position2D;
					array[num2] = position2D.y.ToString();
					array[4] = ",";
					text = string.Concat(array);
					text += settlement.MapFaction.Color;
					text += "-";
				}
			}
			MapScreen obj = ScreenManager.TopScreen as MapScreen;
			MBMapScene.GetGlobalIlluminationOfString((Scene)typeof(MapScreen).GetField("_mapScene", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj), text);
			return "Success";
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003B68 File Offset: 0x00001D68
		[CommandLineFunctionality.CommandLineArgumentFunction("remove_all_circle_notifications", "campaign")]
		public static string ClearAllCircleNotifications(List<string> strings)
		{
			if (!CampaignCheats.CheckCheatUsage(ref CampaignCheats.ErrorType))
			{
				return CampaignCheats.ErrorType;
			}
			MapScreen.Instance.MapNotificationView.ResetNotifications();
			return "Cleared";
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003B90 File Offset: 0x00001D90
		[CommandLineFunctionality.CommandLineArgumentFunction("set_custom_maximum_map_height", "campaign")]
		private static string SetCustomMaximumHeight(List<string> strings)
		{
			string result = string.Format("Format is \"campaign.set_custom_maximum_map_height [MaxHeight]\".\n If the given number is below the current base maximum: {0}, it won't be used.", Campaign.MapMaximumHeight);
			if (!CampaignCheats.CheckCheatUsage(ref CampaignCheats.ErrorType))
			{
				return CampaignCheats.ErrorType;
			}
			if (CampaignCheats.CheckHelp(strings))
			{
				return result;
			}
			int num;
			if (CampaignCheats.CheckParameters(strings, 1) && int.TryParse(strings[0], out num))
			{
				Type typeFromHandle = typeof(MapCameraView);
				PropertyInfo property = typeFromHandle.GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic);
				MapCameraView mapCameraView = (MapCameraView)property.GetValue(null);
				typeFromHandle.GetField("_customMaximumCameraHeight", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(mapCameraView, (float)num);
				property.SetValue(null, mapCameraView);
			}
			return "Success";
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003C34 File Offset: 0x00001E34
		[CommandLineFunctionality.CommandLineArgumentFunction("focus_tournament", "campaign")]
		public static string FocusTournament(List<string> strings)
		{
			if (!CampaignCheats.CheckCheatUsage(ref CampaignCheats.ErrorType))
			{
				return CampaignCheats.ErrorType;
			}
			if (CampaignCheats.CheckHelp(strings))
			{
				return "Format is \"campaign.focus_tournament\".";
			}
			Settlement settlement = Settlement.FindFirst((Settlement x) => x.IsTown && Campaign.Current.TournamentManager.GetTournamentGame(x.Town) != null);
			if (settlement == null)
			{
				return "There isn't any tournament right now.";
			}
			((MapCameraView)typeof(MapCameraView).GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
			settlement.Party.SetAsCameraFollowParty();
			return "Success";
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003CC8 File Offset: 0x00001EC8
		[CommandLineFunctionality.CommandLineArgumentFunction("focus_hostile_army", "campaign")]
		public static string FocusHostileArmy(List<string> strings)
		{
			if (!CampaignCheats.CheckCheatUsage(ref CampaignCheats.ErrorType))
			{
				return CampaignCheats.ErrorType;
			}
			if (CampaignCheats.CheckHelp(strings))
			{
				return "Format is \"campaign.focus_hostile_army\".";
			}
			Army army = null;
			foreach (Kingdom kingdom in Kingdom.All)
			{
				if (kingdom != Clan.PlayerClan.MapFaction && !kingdom.Armies.IsEmpty<Army>() && kingdom.IsAtWarWith(Clan.PlayerClan.MapFaction))
				{
					army = kingdom.Armies.GetRandomElement<Army>();
				}
				if (army != null)
				{
					break;
				}
			}
			if (army == null)
			{
				return "There isn't any hostile army right now.";
			}
			((MapCameraView)typeof(MapCameraView).GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
			army.LeaderParty.Party.SetAsCameraFollowParty();
			return "Success";
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003DB8 File Offset: 0x00001FB8
		[CommandLineFunctionality.CommandLineArgumentFunction("focus_mobile_party", "campaign")]
		public static string FocusMobileParty(List<string> strings)
		{
			if (!CampaignCheats.CheckCheatUsage(ref CampaignCheats.ErrorType))
			{
				return CampaignCheats.ErrorType;
			}
			string text = "Format is \"campaign.focus_mobile_party [PartyName]\".";
			if (CampaignCheats.CheckParameters(strings, 0) || CampaignCheats.CheckHelp(strings))
			{
				return text;
			}
			string text2 = CampaignCheats.ConcatenateString(strings);
			foreach (MobileParty mobileParty in MobileParty.All)
			{
				if (string.Equals(text2.Replace(" ", ""), mobileParty.Name.ToString().Replace(" ", ""), StringComparison.OrdinalIgnoreCase))
				{
					((MapCameraView)typeof(MapCameraView).GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
					mobileParty.Party.SetAsCameraFollowParty();
					return "Success";
				}
			}
			return "Party is not found: " + text2 + "\n" + text;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003EB8 File Offset: 0x000020B8
		[CommandLineFunctionality.CommandLineArgumentFunction("focus_hero", "campaign")]
		public static string FocusHero(List<string> strings)
		{
			if (!CampaignCheats.CheckCheatUsage(ref CampaignCheats.ErrorType))
			{
				return CampaignCheats.ErrorType;
			}
			string text = "Format is \"campaign.focus_hero [HeroName]\".";
			if (CampaignCheats.CheckParameters(strings, 0) || CampaignCheats.CheckHelp(strings))
			{
				return text;
			}
			string text2 = CampaignCheats.ConcatenateString(strings);
			Hero hero = CampaignCheats.GetHero(text2);
			if (hero == null)
			{
				return "Hero is not found: " + text2 + "\n" + text;
			}
			MapCameraView mapCameraView = (MapCameraView)typeof(MapCameraView).GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
			if (hero.CurrentSettlement != null)
			{
				mapCameraView.SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
				hero.CurrentSettlement.Party.SetAsCameraFollowParty();
				return "Success";
			}
			if (hero.PartyBelongedTo != null)
			{
				mapCameraView.SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
				hero.PartyBelongedTo.Party.SetAsCameraFollowParty();
				return "Success";
			}
			if (hero.PartyBelongedToAsPrisoner != null)
			{
				mapCameraView.SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
				hero.PartyBelongedToAsPrisoner.SetAsCameraFollowParty();
				return "Success";
			}
			return "Party is not found: " + text2 + "\n" + text;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003FB4 File Offset: 0x000021B4
		[CommandLineFunctionality.CommandLineArgumentFunction("focus_infested_hideout", "campaign")]
		public static string FocusInfestedHideout(List<string> strings)
		{
			if (!CampaignCheats.CheckCheatUsage(ref CampaignCheats.ErrorType))
			{
				return CampaignCheats.ErrorType;
			}
			string text = "Format is \"campaign.focus_infested_hideout [Optional: Number of troops]\".";
			if (CampaignCheats.CheckHelp(strings))
			{
				return text;
			}
			MBList<Settlement> mblist = (from t in Settlement.All
			where t.IsHideout && t.Parties.Count > 0
			select t).ToMBList<Settlement>();
			if (mblist.IsEmpty<Settlement>())
			{
				return "All hideouts are empty!";
			}
			Settlement randomElement;
			if (strings.Count > 0)
			{
				int troopCount = -1;
				int.TryParse(strings[0], out troopCount);
				if (troopCount == -1)
				{
					return "Incorrect input.\n" + text;
				}
				MBList<Settlement> mblist2 = (from t in mblist
				where t.Parties.Sum((MobileParty p) => p.MemberRoster.TotalManCount) >= troopCount
				select t).ToMBList<Settlement>();
				if (mblist2.IsEmpty<Settlement>())
				{
					return "Can't find suitable hideout.";
				}
				randomElement = mblist2.GetRandomElement<Settlement>();
			}
			else
			{
				randomElement = mblist.GetRandomElement<Settlement>();
			}
			if (randomElement != null)
			{
				((MapCameraView)typeof(MapCameraView).GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
				randomElement.Party.SetAsCameraFollowParty();
				return "Success";
			}
			return "Unable to find such a hideout.";
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000040DC File Offset: 0x000022DC
		[CommandLineFunctionality.CommandLineArgumentFunction("focus_issue", "campaign")]
		public static string FocusIssues(List<string> strings)
		{
			if (!CampaignCheats.CheckCheatUsage(ref CampaignCheats.ErrorType))
			{
				return CampaignCheats.ErrorType;
			}
			string text = "Format is \"campaign.focus_issue [IssueName]\".";
			if (CampaignCheats.CheckParameters(strings, 0) || CampaignCheats.CheckHelp(strings))
			{
				return text;
			}
			MapCameraView mapCameraView = (MapCameraView)typeof(MapCameraView).GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
			string text2 = CampaignCheats.ConcatenateString(strings);
			IssueBase issueBase = null;
			foreach (KeyValuePair<Hero, IssueBase> keyValuePair in Campaign.Current.IssueManager.Issues)
			{
				keyValuePair.Value.Title.ToString();
				if (keyValuePair.Value.Title.ToString().ToLower().Replace(" ", "").Contains(text2.ToLower().Replace(" ", "")))
				{
					if (keyValuePair.Value.IssueSettlement != null)
					{
						issueBase = keyValuePair.Value;
						mapCameraView.SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
						keyValuePair.Value.IssueSettlement.Party.SetAsCameraFollowParty();
					}
					else if (keyValuePair.Value.IssueOwner.PartyBelongedTo != null)
					{
						issueBase = keyValuePair.Value;
						mapCameraView.SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
						MobileParty partyBelongedTo = keyValuePair.Value.IssueOwner.PartyBelongedTo;
						if (partyBelongedTo != null)
						{
							partyBelongedTo.Party.SetAsCameraFollowParty();
						}
					}
					else if (keyValuePair.Value.IssueOwner.CurrentSettlement != null)
					{
						issueBase = keyValuePair.Value;
						mapCameraView.SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
						keyValuePair.Value.IssueOwner.CurrentSettlement.Party.SetAsCameraFollowParty();
					}
					if (issueBase != null)
					{
						return "Found issue: " + issueBase.Title.ToString() + ". Issue Owner: " + issueBase.IssueOwner.Name.ToString();
					}
				}
			}
			return "Issue Not Found.\n" + text;
		}
	}
}
