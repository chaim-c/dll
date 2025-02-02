using System;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000011 RID: 17
	public class LauncherDLLData
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000425E File Offset: 0x0000245E
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00004266 File Offset: 0x00002466
		public SubModuleInfo SubModule { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000426F File Offset: 0x0000246F
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00004277 File Offset: 0x00002477
		public bool IsDangerous { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004280 File Offset: 0x00002480
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00004288 File Offset: 0x00002488
		public string VerifyInformation { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004291 File Offset: 0x00002491
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004299 File Offset: 0x00002499
		public uint Size { get; private set; }

		// Token: 0x06000091 RID: 145 RVA: 0x000042A2 File Offset: 0x000024A2
		public LauncherDLLData(SubModuleInfo subModule, bool isDangerous, string verifyInformation, uint size)
		{
			this.SubModule = subModule;
			this.IsDangerous = isDangerous;
			this.VerifyInformation = verifyInformation;
			this.Size = size;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000042C7 File Offset: 0x000024C7
		public void SetIsDLLDangerous(bool isDangerous)
		{
			this.IsDangerous = isDangerous;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000042D0 File Offset: 0x000024D0
		public void SetDLLSize(uint size)
		{
			this.Size = size;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000042D9 File Offset: 0x000024D9
		public void SetDLLVerifyInformation(string info)
		{
			this.VerifyInformation = info;
		}
	}
}
