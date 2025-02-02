using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.Launcher.Library.UserDatas;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000010 RID: 16
	public class LauncherModsDLLManager
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003BB4 File Offset: 0x00001DB4
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003BBC File Offset: 0x00001DBC
		public bool ShouldUpdateSaveData { get; private set; }

		// Token: 0x06000084 RID: 132 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public LauncherModsDLLManager(UserData userData, List<SubModuleInfo> allSubmodules)
		{
			Debug.Print("Init LauncherModsDLLManager", 0, Debug.DebugColor.White, 17592186044416UL);
			this._userData = userData;
			this._subModulesWithDLLs = new Dictionary<SubModuleInfo, LauncherDLLData>();
			List<SubModuleInfo> list = new List<SubModuleInfo>();
			for (int i = 0; i < allSubmodules.Count; i++)
			{
				SubModuleInfo subModuleInfo = allSubmodules[i];
				if (subModuleInfo.DLLExists && !subModuleInfo.IsTWCertifiedDLL)
				{
					uint num = (uint)new FileInfo(subModuleInfo.DLLPath).Length;
					uint? dlllatestSizeInBytes = userData.GetDLLLatestSizeInBytes(subModuleInfo.DLLName);
					this._subModulesWithDLLs.Add(subModuleInfo, new LauncherDLLData(subModuleInfo, false, "", num));
					if (dlllatestSizeInBytes == null)
					{
						goto IL_B7;
					}
					uint? num2 = dlllatestSizeInBytes;
					uint num3 = num;
					if (!(num2.GetValueOrDefault() == num3 & num2 != null))
					{
						goto IL_B7;
					}
					this._subModulesWithDLLs[subModuleInfo].SetIsDLLDangerous(userData.GetDLLLatestIsDangerous(subModuleInfo.DLLName));
					this._subModulesWithDLLs[subModuleInfo].SetDLLVerifyInformation(userData.GetDLLLatestVerifyInformation(subModuleInfo.DLLName));
					IL_11B:
					this._subModulesWithDLLs[subModuleInfo].SetDLLSize(num);
					goto IL_12D;
					IL_B7:
					Debug.Print("Need to verify: " + subModuleInfo.DLLName, 0, Debug.DebugColor.White, 17592186044416UL);
					list.Add(subModuleInfo);
					goto IL_11B;
				}
				IL_12D:;
			}
			this.VerifySubModules(list);
			this.UpdateUserDataLatestValues();
			this.ShouldUpdateSaveData = (list.Count > 0);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003D30 File Offset: 0x00001F30
		private void UpdateUserDataLatestValues()
		{
			foreach (KeyValuePair<SubModuleInfo, LauncherDLLData> keyValuePair in this._subModulesWithDLLs)
			{
				this._userData.SetDLLLatestIsDangerous(keyValuePair.Key.DLLName, keyValuePair.Value.IsDangerous);
				this._userData.SetDLLLatestSizeInBytes(keyValuePair.Key.DLLName, keyValuePair.Value.Size);
				this._userData.SetDLLLatestVerifyInformation(keyValuePair.Key.DLLName, keyValuePair.Value.VerifyInformation);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003DE8 File Offset: 0x00001FE8
		private void VerifySubModules(List<SubModuleInfo> subModulesToVerify)
		{
			if (subModulesToVerify.Count > 0)
			{
				ResultData dllverifyReport = this.GetDLLVerifyReport((from s in subModulesToVerify
				select s.DLLPath).ToArray<string>());
				if (dllverifyReport != null)
				{
					int k;
					int i;
					for (i = 0; i < subModulesToVerify.Count; i = k + 1)
					{
						DLLResult dllresult = dllverifyReport.DLLs.FirstOrDefault((DLLResult r) => r.DLLName == subModulesToVerify[i].DLLPath);
						this._subModulesWithDLLs[subModulesToVerify[i]].SetIsDLLDangerous(!dllresult.IsSafe);
						this._subModulesWithDLLs[subModulesToVerify[i]].SetDLLVerifyInformation(dllresult.Information);
						Debug.Print(dllresult.Information, 0, Debug.DebugColor.White, 17592186044416UL);
						k = i;
					}
					return;
				}
				for (int j = 0; j < subModulesToVerify.Count; j++)
				{
					this._subModulesWithDLLs[subModulesToVerify[j]].SetDLLSize(0U);
					this._subModulesWithDLLs[subModulesToVerify[j]].SetIsDLLDangerous(true);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003F78 File Offset: 0x00002178
		private ResultData GetDLLVerifyReport(string[] dlls)
		{
			ResultData result;
			try
			{
				new List<bool>();
				string text = "";
				string text2 = "";
				Debug.Print("Starting verifying DLLs", 0, Debug.DebugColor.White, 17592186044416UL);
				Process process = new Process();
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.FileName = "..\\ModVerifier\\ModVerifier.exe";
				process.StartInfo.Arguments = string.Join(" ", from c in dlls
				where !string.IsNullOrEmpty(c)
				select c) + " -ld1";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				StringBuilder outputString = new StringBuilder();
				StringBuilder errorString = new StringBuilder();
				process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
				{
					if (e.Data != null)
					{
						outputString.AppendLine(e.Data);
					}
				};
				process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
				{
					if (e.Data != null)
					{
						errorString.AppendLine(e.Data);
					}
				};
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				stopwatch.Stop();
				Debug.Print(((float)stopwatch.ElapsedMilliseconds / 1000f).ToString("0.0000") + " seconds", 0, Debug.DebugColor.White, 17592186044416UL);
				text += outputString;
				text2 += errorString;
				try
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(ResultData));
					object obj;
					using (TextReader textReader = new StringReader(text))
					{
						obj = xmlSerializer.Deserialize(textReader);
					}
					result = (obj as ResultData);
				}
				catch (Exception)
				{
					Debug.Print("Error while verifying DLLs", 0, Debug.DebugColor.White, 17592186044416UL);
					Debug.Print("Verify Tool output:", 0, Debug.DebugColor.White, 17592186044416UL);
					Debug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
					Debug.Print("Verify Tool error output:", 0, Debug.DebugColor.White, 17592186044416UL);
					Debug.Print(text2, 0, Debug.DebugColor.White, 17592186044416UL);
					result = null;
				}
			}
			catch (Exception ex)
			{
				Debug.Print("Couldn't verify dlls", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print(ex.StackTrace, 0, Debug.DebugColor.White, 17592186044416UL);
				result = null;
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004240 File Offset: 0x00002440
		public LauncherDLLData GetSubModuleVerifyData(SubModuleInfo subModuleInfo)
		{
			if (this._subModulesWithDLLs.ContainsKey(subModuleInfo))
			{
				return this._subModulesWithDLLs[subModuleInfo];
			}
			return null;
		}

		// Token: 0x04000047 RID: 71
		private Dictionary<SubModuleInfo, LauncherDLLData> _subModulesWithDLLs;

		// Token: 0x04000048 RID: 72
		private UserData _userData;
	}
}
