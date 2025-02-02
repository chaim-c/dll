using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000B3 RID: 179
	public class DumpIntegrityCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x0004220C File Offset: 0x0004040C
		public override void SyncData(IDataStore dataStore)
		{
			TextObject textObject;
			DumpIntegrityCampaignBehavior.IsGameIntegrityAchieved(out textObject);
			this.UpdateDumpInfo();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00042227 File Offset: 0x00040427
		public override void RegisterEvents()
		{
			CampaignEvents.OnConfigChangedEvent.AddNonSerializedListener(this, new Action(this.OnConfigChanged));
			CampaignEvents.OnNewGameCreatedPartialFollowUpEndEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreatedPartialFollowUpEnd));
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00042258 File Offset: 0x00040458
		private void OnConfigChanged()
		{
			TextObject textObject;
			DumpIntegrityCampaignBehavior.IsGameIntegrityAchieved(out textObject);
			this.UpdateDumpInfo();
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00042274 File Offset: 0x00040474
		private void OnNewGameCreatedPartialFollowUpEnd(CampaignGameStarter campaignGameStarter)
		{
			TextObject textObject;
			DumpIntegrityCampaignBehavior.IsGameIntegrityAchieved(out textObject);
			this.UpdateDumpInfo();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00042290 File Offset: 0x00040490
		private void UpdateDumpInfo()
		{
			this._saveIntegrityDumpInfo.Clear();
			this._usedModulesDumpInfo.Clear();
			this._usedVersionsDumpInfo.Clear();
			Campaign campaign = Campaign.Current;
			if (((campaign != null) ? campaign.PreviouslyUsedModules : null) != null && Campaign.Current.UsedGameVersions != null && Campaign.Current.NewGameVersion != null)
			{
				this._saveIntegrityDumpInfo.Add(new KeyValuePair<string, string>("New Game Version", Campaign.Current.NewGameVersion));
				this._saveIntegrityDumpInfo.Add(new KeyValuePair<string, string>("Has Used Cheats", (!DumpIntegrityCampaignBehavior.CheckCheatUsage()).ToString()));
				this._saveIntegrityDumpInfo.Add(new KeyValuePair<string, string>("Has Installed Unofficial Modules", (!DumpIntegrityCampaignBehavior.CheckIfModulesAreDefault()).ToString()));
				this._saveIntegrityDumpInfo.Add(new KeyValuePair<string, string>("Has Reverted to Older Versions", (!DumpIntegrityCampaignBehavior.CheckIfVersionIntegrityIsAchieved()).ToString()));
				TextObject textObject;
				this._saveIntegrityDumpInfo.Add(new KeyValuePair<string, string>("Game Integrity is Achieved", DumpIntegrityCampaignBehavior.IsGameIntegrityAchieved(out textObject).ToString()));
			}
			Campaign campaign2 = Campaign.Current;
			if (((campaign2 != null) ? campaign2.PreviouslyUsedModules : null) != null)
			{
				string[] moduleNames = SandBoxManager.Instance.ModuleManager.ModuleNames;
				using (List<string>.Enumerator enumerator = Campaign.Current.PreviouslyUsedModules.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string module = enumerator.Current;
						bool flag = moduleNames.FindIndex((string x) => x == module) != -1;
						this._usedModulesDumpInfo.Add(new KeyValuePair<string, string>(module, flag.ToString()));
					}
				}
			}
			Campaign campaign3 = Campaign.Current;
			if (((campaign3 != null) ? campaign3.UsedGameVersions : null) != null && Campaign.Current.UsedGameVersions.Count > 0)
			{
				foreach (string key in Campaign.Current.UsedGameVersions)
				{
					this._usedVersionsDumpInfo.Add(new KeyValuePair<string, string>(key, ""));
				}
			}
			this.SendDataToWatchdog();
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000424D8 File Offset: 0x000406D8
		private void SendDataToWatchdog()
		{
			foreach (KeyValuePair<string, string> keyValuePair in this._saveIntegrityDumpInfo)
			{
				Utilities.SetWatchdogValue("crash_tags.txt", "Campaign Dump Integrity", keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<string, string> keyValuePair2 in this._usedModulesDumpInfo)
			{
				Utilities.SetWatchdogValue("crash_tags.txt", "Used Mods", keyValuePair2.Key, keyValuePair2.Value);
			}
			foreach (KeyValuePair<string, string> keyValuePair3 in this._usedVersionsDumpInfo)
			{
				Utilities.SetWatchdogValue("crash_tags.txt", "Used Game Versions", keyValuePair3.Key, keyValuePair3.Value);
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000425F4 File Offset: 0x000407F4
		public static bool IsGameIntegrityAchieved(out TextObject reason)
		{
			reason = TextObject.Empty;
			bool result = true;
			if (!DumpIntegrityCampaignBehavior.CheckCheatUsage())
			{
				reason = new TextObject("{=sO8Zh3ZH}Achievements are disabled due to cheat usage.", null);
				result = false;
			}
			else if (!DumpIntegrityCampaignBehavior.CheckIfModulesAreDefault())
			{
				reason = new TextObject("{=R0AbAxqX}Achievements are disabled due to unofficial modules.", null);
				result = false;
			}
			else if (!DumpIntegrityCampaignBehavior.CheckIfVersionIntegrityIsAchieved())
			{
				reason = new TextObject("{=dt00CQCM}Achievements are disabled due to version downgrade.", null);
				result = false;
			}
			return result;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00042654 File Offset: 0x00040854
		private static bool CheckIfVersionIntegrityIsAchieved()
		{
			for (int i = 0; i < Campaign.Current.UsedGameVersions.Count; i++)
			{
				if (i < Campaign.Current.UsedGameVersions.Count - 1 && ApplicationVersion.FromString(Campaign.Current.UsedGameVersions[i], 45697) > ApplicationVersion.FromString(Campaign.Current.UsedGameVersions[i + 1], 45697))
				{
					Debug.Print("Dump integrity is compromised due to version downgrade", 0, Debug.DebugColor.DarkRed, 17592186044416UL);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000426E4 File Offset: 0x000408E4
		private static bool CheckIfModulesAreDefault()
		{
			bool flag = Campaign.Current.PreviouslyUsedModules.All((string x) => x.Equals("Native", StringComparison.OrdinalIgnoreCase) || x.Equals("SandBoxCore", StringComparison.OrdinalIgnoreCase) || x.Equals("CustomBattle", StringComparison.OrdinalIgnoreCase) || x.Equals("SandBox", StringComparison.OrdinalIgnoreCase) || x.Equals("Multiplayer", StringComparison.OrdinalIgnoreCase) || x.Equals("BirthAndDeath", StringComparison.OrdinalIgnoreCase) || x.Equals("StoryMode", StringComparison.OrdinalIgnoreCase));
			if (!flag)
			{
				Debug.Print("Dump integrity is compromised due to non-default modules being used", 0, Debug.DebugColor.DarkRed, 17592186044416UL);
			}
			return flag;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00042738 File Offset: 0x00040938
		private static bool CheckCheatUsage()
		{
			if (!Campaign.Current.EnabledCheatsBefore && Game.Current.CheatMode)
			{
				Campaign.Current.EnabledCheatsBefore = Game.Current.CheatMode;
			}
			if (Campaign.Current.EnabledCheatsBefore)
			{
				Debug.Print("Dump integrity is compromised due to cheat usage", 0, Debug.DebugColor.DarkRed, 17592186044416UL);
			}
			return !Campaign.Current.EnabledCheatsBefore;
		}

		// Token: 0x04000340 RID: 832
		private readonly List<KeyValuePair<string, string>> _saveIntegrityDumpInfo = new List<KeyValuePair<string, string>>();

		// Token: 0x04000341 RID: 833
		private readonly List<KeyValuePair<string, string>> _usedModulesDumpInfo = new List<KeyValuePair<string, string>>();

		// Token: 0x04000342 RID: 834
		private readonly List<KeyValuePair<string, string>> _usedVersionsDumpInfo = new List<KeyValuePair<string, string>>();
	}
}
