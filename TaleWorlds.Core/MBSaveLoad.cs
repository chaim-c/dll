using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Load;

namespace TaleWorlds.Core
{
	// Token: 0x020000AC RID: 172
	public static class MBSaveLoad
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001C57F File Offset: 0x0001A77F
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0001C586 File Offset: 0x0001A786
		public static ApplicationVersion LastLoadedGameVersion { get; private set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001C58E File Offset: 0x0001A78E
		public static ApplicationVersion CurrentVersion { get; } = ApplicationVersion.FromParametersFile(null);

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001C598 File Offset: 0x0001A798
		public static bool IsUpdatingGameVersion
		{
			get
			{
				return MBSaveLoad.LastLoadedGameVersion.IsOlderThan(MBSaveLoad.CurrentVersion);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001C5B7 File Offset: 0x0001A7B7
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0001C5BE File Offset: 0x0001A7BE
		public static int NumberOfCurrentSaves { get; private set; }

		// Token: 0x0600086D RID: 2157 RVA: 0x0001C5C6 File Offset: 0x0001A7C6
		private static string GetAutoSaveName()
		{
			return MBSaveLoad.AutoSaveNamePrefix + MBSaveLoad.AutoSaveIndex;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001C5DC File Offset: 0x0001A7DC
		private static void IncrementAutoSaveIndex()
		{
			MBSaveLoad.AutoSaveIndex++;
			if (MBSaveLoad.AutoSaveIndex > 3)
			{
				MBSaveLoad.AutoSaveIndex = 1;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		private static void InitializeAutoSaveIndex(string saveName)
		{
			string text = string.Empty;
			if (saveName.StartsWith(MBSaveLoad.AutoSaveNamePrefix))
			{
				text = saveName;
			}
			else
			{
				foreach (string text2 in MBSaveLoad.GetSaveFileNames())
				{
					if (text2.StartsWith(MBSaveLoad.AutoSaveNamePrefix))
					{
						text = text2;
						break;
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				MBSaveLoad.AutoSaveIndex = 1;
				return;
			}
			int num;
			if (int.TryParse(text.Substring(MBSaveLoad.AutoSaveNamePrefix.Length), out num) && num > 0 && num <= 3)
			{
				MBSaveLoad.AutoSaveIndex = num;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001C67E File Offset: 0x0001A87E
		public static void SetSaveDriver(ISaveDriver saveDriver)
		{
			MBSaveLoad._saveDriver = saveDriver;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001C688 File Offset: 0x0001A888
		public static SaveGameFileInfo[] GetSaveFiles(Func<SaveGameFileInfo, bool> condition = null)
		{
			SaveGameFileInfo[] saveGameFileInfos = MBSaveLoad._saveDriver.GetSaveGameFileInfos();
			MBSaveLoad.NumberOfCurrentSaves = saveGameFileInfos.Length;
			List<SaveGameFileInfo> list = new List<SaveGameFileInfo>();
			foreach (SaveGameFileInfo saveGameFileInfo in saveGameFileInfos)
			{
				if (condition == null || condition(saveGameFileInfo))
				{
					list.Add(saveGameFileInfo);
				}
			}
			return (from info in list
			orderby info.MetaData.GetCreationTime() descending
			select info).ToArray<SaveGameFileInfo>();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001C6FD File Offset: 0x0001A8FD
		public static bool IsSaveGameFileExists(string saveFileName)
		{
			return MBSaveLoad._saveDriver.IsSaveGameFileExists(saveFileName);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001C70A File Offset: 0x0001A90A
		public static string[] GetSaveFileNames()
		{
			return MBSaveLoad._saveDriver.GetSaveGameFileNames();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001C718 File Offset: 0x0001A918
		public static LoadResult LoadSaveGameData(string saveName)
		{
			MBSaveLoad.InitializeAutoSaveIndex(saveName);
			ISaveDriver saveDriver = MBSaveLoad._saveDriver;
			LoadResult loadResult = SaveManager.Load(saveName, saveDriver, true);
			if (loadResult.Successful)
			{
				MBSaveLoad.ActiveSaveSlotName = saveName;
				return loadResult;
			}
			Debug.Print("Error: Could not load the game!", 0, Debug.DebugColor.White, 17592186044416UL);
			return null;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001C764 File Offset: 0x0001A964
		public static SaveGameFileInfo GetSaveFileWithName(string saveName)
		{
			SaveGameFileInfo[] saveFiles = MBSaveLoad.GetSaveFiles((SaveGameFileInfo x) => x.Name.Equals(saveName));
			if (saveFiles.Length == 0)
			{
				return null;
			}
			return saveFiles[0];
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001C797 File Offset: 0x0001A997
		public static void QuickSaveCurrentGame(CampaignSaveMetaDataArgs campaignMetaData, Action<ValueTuple<SaveResult, string>> onSaveCompleted)
		{
			if (MBSaveLoad.ActiveSaveSlotName == null)
			{
				MBSaveLoad.ActiveSaveSlotName = MBSaveLoad.GetNextAvailableSaveName();
			}
			Debug.Print("QuickSaveCurrentGame: " + MBSaveLoad.ActiveSaveSlotName, 0, Debug.DebugColor.White, 17592186044416UL);
			MBSaveLoad.OverwriteSaveAux(campaignMetaData, MBSaveLoad.ActiveSaveSlotName, onSaveCompleted);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
		public static void AutoSaveCurrentGame(CampaignSaveMetaDataArgs campaignMetaData, Action<ValueTuple<SaveResult, string>> onSaveCompleted)
		{
			MBSaveLoad.IncrementAutoSaveIndex();
			string autoSaveName = MBSaveLoad.GetAutoSaveName();
			Debug.Print("AutoSaveCurrentGame: " + autoSaveName, 0, Debug.DebugColor.White, 17592186044416UL);
			MBSaveLoad.OverwriteSaveAux(campaignMetaData, autoSaveName, onSaveCompleted);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001C814 File Offset: 0x0001AA14
		public static void SaveAsCurrentGame(CampaignSaveMetaDataArgs campaignMetaData, string saveName, Action<ValueTuple<SaveResult, string>> onSaveCompleted)
		{
			MBSaveLoad.ActiveSaveSlotName = saveName;
			Debug.Print("SaveAsCurrentGame: " + saveName, 0, Debug.DebugColor.White, 17592186044416UL);
			MBSaveLoad.OverwriteSaveAux(campaignMetaData, saveName, onSaveCompleted);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001C840 File Offset: 0x0001AA40
		private static void OverwriteSaveAux(CampaignSaveMetaDataArgs campaignMetaData, string saveName, Action<ValueTuple<SaveResult, string>> onSaveCompleted)
		{
			MetaData saveMetaData = MBSaveLoad.GetSaveMetaData(campaignMetaData);
			bool isOverwritingExistingSave = MBSaveLoad.IsSaveGameFileExists(saveName);
			if (!MBSaveLoad.IsMaxNumberOfSavesReached() || isOverwritingExistingSave)
			{
				MBSaveLoad.OverwriteSaveFile(saveMetaData, saveName, delegate(SaveResult r)
				{
					Action<ValueTuple<SaveResult, string>> onSaveCompleted3 = onSaveCompleted;
					if (onSaveCompleted3 != null)
					{
						onSaveCompleted3(new ValueTuple<SaveResult, string>(r, saveName));
					}
					if (r == SaveResult.Success && !isOverwritingExistingSave)
					{
						MBSaveLoad.NumberOfCurrentSaves++;
					}
				});
				return;
			}
			Action<ValueTuple<SaveResult, string>> onSaveCompleted2 = onSaveCompleted;
			if (onSaveCompleted2 == null)
			{
				return;
			}
			onSaveCompleted2(new ValueTuple<SaveResult, string>(SaveResult.SaveLimitReached, string.Empty));
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001C8BC File Offset: 0x0001AABC
		public static bool DeleteSaveGame(string saveName)
		{
			bool flag = MBSaveLoad._saveDriver.Delete(saveName);
			if (flag)
			{
				if (MBSaveLoad.NumberOfCurrentSaves > 0)
				{
					MBSaveLoad.NumberOfCurrentSaves--;
				}
				if (MBSaveLoad.ActiveSaveSlotName == saveName)
				{
					MBSaveLoad.ActiveSaveSlotName = null;
				}
			}
			return flag;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001C8F3 File Offset: 0x0001AAF3
		public static void Initialize(GameTextManager localizedTextProvider)
		{
			MBSaveLoad._textProvider = localizedTextProvider;
			MBSaveLoad.NumberOfCurrentSaves = MBSaveLoad._saveDriver.GetSaveGameFileInfos().Length;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001C90C File Offset: 0x0001AB0C
		public static void OnNewGame()
		{
			MBSaveLoad.ActiveSaveSlotName = null;
			MBSaveLoad.LastLoadedGameVersion = MBSaveLoad.CurrentVersion;
			MBSaveLoad.AutoSaveIndex = 0;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001C924 File Offset: 0x0001AB24
		public static void OnGameDestroy()
		{
			MBSaveLoad.LastLoadedGameVersion = ApplicationVersion.Empty;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001C930 File Offset: 0x0001AB30
		public static void OnStartGame(LoadResult loadResult)
		{
			MBSaveLoad.LastLoadedGameVersion = loadResult.MetaData.GetApplicationVersion();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001C944 File Offset: 0x0001AB44
		public static bool IsSaveFileNameReserved(string name)
		{
			for (int i = 1; i <= 3; i++)
			{
				if (name == MBSaveLoad.AutoSaveNamePrefix + i)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001C978 File Offset: 0x0001AB78
		private static string GetNextAvailableSaveName()
		{
			uint num = 0U;
			foreach (string text in MBSaveLoad.GetSaveFileNames())
			{
				uint num2;
				if (text.StartsWith(MBSaveLoad.DefaultSaveGamePrefix) && uint.TryParse(text.Substring(MBSaveLoad.DefaultSaveGamePrefix.Length), out num2) && num2 > num)
				{
					num = num2;
				}
			}
			num += 1U;
			return MBSaveLoad.DefaultSaveGamePrefix + num.ToString("000");
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		private static void OverwriteSaveFile(MetaData metaData, string name, Action<SaveResult> onSaveCompleted)
		{
			try
			{
				MBSaveLoad.SaveGame(name, metaData, delegate(SaveResult r)
				{
					onSaveCompleted(r);
					MBSaveLoad.ShowErrorFromResult(r);
				});
			}
			catch
			{
				MBSaveLoad.ShowErrorFromResult(SaveResult.GeneralFailure);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001CA30 File Offset: 0x0001AC30
		private static void ShowErrorFromResult(SaveResult result)
		{
			if (result != SaveResult.Success)
			{
				if (result == SaveResult.PlatformFileHelperFailure)
				{
					Debug.FailedAssert("Save Failed:\n" + Common.PlatformFileHelper.GetError(), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.Core\\MBSaveLoad.cs", "ShowErrorFromResult", 312);
				}
				if (!MBSaveLoad.DoNotShowSaveErrorAgain)
				{
					InformationManager.ShowInquiry(new InquiryData(MBSaveLoad._textProvider.FindText("str_save_unsuccessful_title", null).ToString(), MBSaveLoad._textProvider.FindText("str_game_save_result", result.ToString()).ToString(), true, false, MBSaveLoad._textProvider.FindText("str_ok", null).ToString(), MBSaveLoad._textProvider.FindText("str_dont_show_again", null).ToString(), null, delegate()
					{
						MBSaveLoad.DoNotShowSaveErrorAgain = true;
					}, "", 0f, null, null, null), false, false);
				}
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001CB14 File Offset: 0x0001AD14
		private static void SaveGame(string saveName, MetaData metaData, Action<SaveResult> onSaveCompleted)
		{
			ISaveDriver saveDriver = MBSaveLoad._saveDriver;
			try
			{
				Game.Current.Save(metaData, saveName, saveDriver, onSaveCompleted);
			}
			catch (Exception ex)
			{
				Debug.Print("Unable to create save game data", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.SilentAssert(ModuleHelper.GetModules().Any((ModuleInfo m) => !m.IsOfficial), ex.Message, false, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.Core\\MBSaveLoad.cs", "SaveGame", 345);
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		private static MetaData GetSaveMetaData(CampaignSaveMetaDataArgs data)
		{
			MetaData metaData = new MetaData();
			List<ModuleInfo> moduleInfos = ModuleHelper.GetModuleInfos(data.ModuleNames);
			metaData["Modules"] = string.Join(";", from q in moduleInfos
			select q.Name);
			foreach (ModuleInfo moduleInfo in moduleInfos)
			{
				metaData["Module_" + moduleInfo.Name] = moduleInfo.Version.ToString();
			}
			metaData.Add("ApplicationVersion", MBSaveLoad.CurrentVersion.ToString());
			metaData.Add("CreationTime", DateTime.Now.Ticks.ToString());
			foreach (KeyValuePair<string, string> keyValuePair in data.OtherData)
			{
				metaData.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return metaData;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		public static int GetMaxNumberOfSaves()
		{
			return int.MaxValue;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001CCFF File Offset: 0x0001AEFF
		public static bool IsMaxNumberOfSavesReached()
		{
			return MBSaveLoad.NumberOfCurrentSaves >= MBSaveLoad.GetMaxNumberOfSaves();
		}

		// Token: 0x040004C8 RID: 1224
		private const int MaxNumberOfAutoSaveFiles = 3;

		// Token: 0x040004C9 RID: 1225
		private static ISaveDriver _saveDriver = new FileDriver();

		// Token: 0x040004CA RID: 1226
		private static int AutoSaveIndex = 0;

		// Token: 0x040004CB RID: 1227
		private static string DefaultSaveGamePrefix = "save";

		// Token: 0x040004CC RID: 1228
		private static string AutoSaveNamePrefix = MBSaveLoad.DefaultSaveGamePrefix + "auto";

		// Token: 0x040004CD RID: 1229
		private static string ActiveSaveSlotName = null;

		// Token: 0x040004CE RID: 1230
		private static GameTextManager _textProvider;

		// Token: 0x040004CF RID: 1231
		private static bool DoNotShowSaveErrorAgain = false;
	}
}
