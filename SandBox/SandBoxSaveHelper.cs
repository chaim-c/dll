using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Load;

namespace SandBox
{
	// Token: 0x02000028 RID: 40
	public static class SandBoxSaveHelper
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000109 RID: 265 RVA: 0x000073F8 File Offset: 0x000055F8
		// (remove) Token: 0x0600010A RID: 266 RVA: 0x0000742C File Offset: 0x0000562C
		public static event Action<SandBoxSaveHelper.SaveHelperState> OnStateChange;

		// Token: 0x0600010B RID: 267 RVA: 0x00007460 File Offset: 0x00005660
		public static void TryLoadSave(SaveGameFileInfo saveInfo, Action<LoadResult> onStartGame, Action onCancel = null)
		{
			SandBoxSaveHelper._newlineTextObject.SetTextVariable("newline", "\n");
			Action<SandBoxSaveHelper.SaveHelperState> onStateChange = SandBoxSaveHelper.OnStateChange;
			if (onStateChange != null)
			{
				onStateChange(SandBoxSaveHelper.SaveHelperState.Start);
			}
			bool flag = true;
			ApplicationVersion applicationVersion = saveInfo.MetaData.GetApplicationVersion();
			if (applicationVersion < SandBoxSaveHelper.SaveResetVersion)
			{
				InquiryData data = new InquiryData(SandBoxSaveHelper._moduleMissmatchInquiryTitle.ToString(), SandBoxSaveHelper._saveResetVersionProblemText.ToString(), true, false, new TextObject("{=yS7PvrTD}OK", null).ToString(), null, delegate()
				{
					SandBoxSaveHelper._isInquiryActive = false;
					Action onCancel2 = onCancel;
					if (onCancel2 == null)
					{
						return;
					}
					onCancel2();
				}, null, "", 0f, null, null, null);
				SandBoxSaveHelper._isInquiryActive = true;
				InformationManager.ShowInquiry(data, false, false);
				Action<SandBoxSaveHelper.SaveHelperState> onStateChange2 = SandBoxSaveHelper.OnStateChange;
				if (onStateChange2 == null)
				{
					return;
				}
				onStateChange2(SandBoxSaveHelper.SaveHelperState.Inquiry);
				return;
			}
			else
			{
				List<SandBoxSaveHelper.ModuleCheckResult> list = SandBoxSaveHelper.CheckModules(saveInfo.MetaData);
				if (list.Count <= 0)
				{
					SandBoxSaveHelper.LoadGameAction(saveInfo, onStartGame, onCancel);
					return;
				}
				IEnumerable<IGrouping<ModuleCheckResultType, SandBoxSaveHelper.ModuleCheckResult>> enumerable = from m in list
				group m by m.Type;
				string text = string.Empty;
				GameTextManager globalTextManager = Module.CurrentModule.GlobalTextManager;
				foreach (IGrouping<ModuleCheckResultType, SandBoxSaveHelper.ModuleCheckResult> grouping in enumerable)
				{
					SandBoxSaveHelper._stringSpaceStringTextObject.SetTextVariable("STR1", globalTextManager.FindText("str_load_module_error", Enum.GetName(typeof(ModuleCheckResultType), grouping.Key)));
					SandBoxSaveHelper._stringSpaceStringTextObject.SetTextVariable("STR2", grouping.ElementAt(0).ModuleName);
					text += SandBoxSaveHelper._stringSpaceStringTextObject.ToString();
					for (int i = 1; i < grouping.Count<SandBoxSaveHelper.ModuleCheckResult>(); i++)
					{
						SandBoxSaveHelper._stringSpaceStringTextObject.SetTextVariable("STR1", text);
						SandBoxSaveHelper._stringSpaceStringTextObject.SetTextVariable("STR2", grouping.ElementAt(i).ModuleName);
						text = SandBoxSaveHelper._stringSpaceStringTextObject.ToString();
					}
					SandBoxSaveHelper._newlineTextObject.SetTextVariable("STR1", text);
					SandBoxSaveHelper._newlineTextObject.SetTextVariable("STR2", "");
					text = SandBoxSaveHelper._newlineTextObject.ToString();
				}
				SandBoxSaveHelper._newlineTextObject.SetTextVariable("STR1", text);
				SandBoxSaveHelper._newlineTextObject.SetTextVariable("STR2", " ");
				text = SandBoxSaveHelper._newlineTextObject.ToString();
				bool flag2 = MBSaveLoad.CurrentVersion >= applicationVersion || flag;
				if (flag2)
				{
					SandBoxSaveHelper._newlineTextObject.SetTextVariable("STR1", text);
					SandBoxSaveHelper._newlineTextObject.SetTextVariable("STR2", new TextObject("{=lh0so0uX}Do you want to load the saved game with different modules?", null));
					text = SandBoxSaveHelper._newlineTextObject.ToString();
				}
				InquiryData data2 = new InquiryData(SandBoxSaveHelper._moduleMissmatchInquiryTitle.ToString(), text, flag2, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=3CpNUnVl}Cancel", null).ToString(), delegate()
				{
					SandBoxSaveHelper._isInquiryActive = false;
					SandBoxSaveHelper.LoadGameAction(saveInfo, onStartGame, onCancel);
				}, delegate()
				{
					SandBoxSaveHelper._isInquiryActive = false;
					Action onCancel2 = onCancel;
					if (onCancel2 == null)
					{
						return;
					}
					onCancel2();
				}, "", 0f, null, null, null);
				SandBoxSaveHelper._isInquiryActive = true;
				InformationManager.ShowInquiry(data2, false, false);
				Action<SandBoxSaveHelper.SaveHelperState> onStateChange3 = SandBoxSaveHelper.OnStateChange;
				if (onStateChange3 == null)
				{
					return;
				}
				onStateChange3(SandBoxSaveHelper.SaveHelperState.Inquiry);
				return;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000077CC File Offset: 0x000059CC
		private static List<SandBoxSaveHelper.ModuleCheckResult> CheckModules(MetaData fileMetaData)
		{
			List<ModuleInfo> moduleInfos = ModuleHelper.GetModuleInfos(Utilities.GetModulesNames());
			string[] modulesInSaveFile = fileMetaData.GetModules();
			List<SandBoxSaveHelper.ModuleCheckResult> list = new List<SandBoxSaveHelper.ModuleCheckResult>();
			string[] modulesInSaveFile2 = modulesInSaveFile;
			for (int i = 0; i < modulesInSaveFile2.Length; i++)
			{
				string moduleName = modulesInSaveFile2[i];
				if (moduleInfos.All((ModuleInfo loadedModule) => loadedModule.Name != moduleName))
				{
					list.Add(new SandBoxSaveHelper.ModuleCheckResult(moduleName, ModuleCheckResultType.ModuleRemoved));
				}
				else if (!fileMetaData.GetModuleVersion(moduleName).IsSame(moduleInfos.Single((ModuleInfo loadedModule) => loadedModule.Name == moduleName).Version, false))
				{
					list.Add(new SandBoxSaveHelper.ModuleCheckResult(moduleName, ModuleCheckResultType.VersionMismatch));
				}
			}
			IEnumerable<ModuleInfo> source = moduleInfos;
			Func<ModuleInfo, bool> <>9__2;
			Func<ModuleInfo, bool> predicate;
			if ((predicate = <>9__2) == null)
			{
				predicate = (<>9__2 = ((ModuleInfo loadedModule) => modulesInSaveFile.All((string moduleName) => loadedModule.Name != moduleName)));
			}
			foreach (ModuleInfo moduleInfo in source.Where(predicate))
			{
				list.Add(new SandBoxSaveHelper.ModuleCheckResult(moduleInfo.Name, ModuleCheckResultType.ModuleAdded));
			}
			return list;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007910 File Offset: 0x00005B10
		private static void LoadGameAction(SaveGameFileInfo saveInfo, Action<LoadResult> onStartGame, Action onCancel)
		{
			Action<SandBoxSaveHelper.SaveHelperState> onStateChange = SandBoxSaveHelper.OnStateChange;
			if (onStateChange != null)
			{
				onStateChange(SandBoxSaveHelper.SaveHelperState.LoadGame);
			}
			LoadResult loadResult = MBSaveLoad.LoadSaveGameData(saveInfo.Name);
			if (loadResult != null)
			{
				if (onStartGame != null)
				{
					onStartGame(loadResult);
					return;
				}
			}
			else
			{
				InquiryData data = new InquiryData(SandBoxSaveHelper._errorTitle.ToString(), SandBoxSaveHelper._saveLoadingProblemText.ToString(), true, false, new TextObject("{=WiNRdfsm}Done", null).ToString(), string.Empty, delegate()
				{
					SandBoxSaveHelper._isInquiryActive = false;
					Action onCancel2 = onCancel;
					if (onCancel2 == null)
					{
						return;
					}
					onCancel2();
				}, null, "", 0f, null, null, null);
				SandBoxSaveHelper._isInquiryActive = true;
				InformationManager.ShowInquiry(data, false, false);
				Action<SandBoxSaveHelper.SaveHelperState> onStateChange2 = SandBoxSaveHelper.OnStateChange;
				if (onStateChange2 == null)
				{
					return;
				}
				onStateChange2(SandBoxSaveHelper.SaveHelperState.Inquiry);
			}
		}

		// Token: 0x0400004B RID: 75
		private static readonly ApplicationVersion SaveResetVersion = new ApplicationVersion(ApplicationVersionType.EarlyAccess, 1, 7, 0, 0);

		// Token: 0x0400004C RID: 76
		private static readonly TextObject _stringSpaceStringTextObject = new TextObject("{=7AFlpaem}{STR1} {STR2}", null);

		// Token: 0x0400004D RID: 77
		private static readonly TextObject _newlineTextObject = new TextObject("{=ol0rBSrb}{STR1}{newline}{STR2}", null);

		// Token: 0x0400004E RID: 78
		private static readonly TextObject _moduleMissmatchInquiryTitle = new TextObject("{=r7xdYj4q}Module Mismatch", null);

		// Token: 0x0400004F RID: 79
		private static readonly TextObject _errorTitle = new TextObject("{=oZrVNUOk}Error", null);

		// Token: 0x04000050 RID: 80
		private static readonly TextObject _saveLoadingProblemText = new TextObject("{=onLDP7mP}A problem occured while trying to load the saved game.", null);

		// Token: 0x04000051 RID: 81
		private static readonly TextObject _saveResetVersionProblemText = new TextObject("{=5hbSkbQg}This save file is from a game version that is older than e1.7.0. Please switch your game version to e1.7.0, load the save file and save the game. This will allow it to work on newer versions beyond e1.7.0.", null);

		// Token: 0x04000052 RID: 82
		private static bool _isInquiryActive;

		// Token: 0x020000FF RID: 255
		public enum SaveHelperState
		{
			// Token: 0x040004AF RID: 1199
			Start,
			// Token: 0x040004B0 RID: 1200
			Inquiry,
			// Token: 0x040004B1 RID: 1201
			LoadGame
		}

		// Token: 0x02000100 RID: 256
		private readonly struct ModuleCheckResult
		{
			// Token: 0x06000B55 RID: 2901 RVA: 0x00051CC7 File Offset: 0x0004FEC7
			public ModuleCheckResult(string moduleName, ModuleCheckResultType type)
			{
				this.ModuleName = moduleName;
				this.Type = type;
			}

			// Token: 0x040004B2 RID: 1202
			public readonly string ModuleName;

			// Token: 0x040004B3 RID: 1203
			public readonly ModuleCheckResultType Type;
		}
	}
}
