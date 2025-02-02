using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	public class ModuleInfoModel
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0000CA77 File Offset: 0x0000AC77
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0000CA7F File Offset: 0x0000AC7F
		public string Id { get; private set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0000CA88 File Offset: 0x0000AC88
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public string Name { get; private set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0000CA99 File Offset: 0x0000AC99
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
		public ModuleCategory Category { get; private set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0000CAAA File Offset: 0x0000ACAA
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0000CAB2 File Offset: 0x0000ACB2
		public string Version { get; private set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0000CABB File Offset: 0x0000ACBB
		[JsonIgnore]
		public bool IsOptional
		{
			get
			{
				return this.Category == ModuleCategory.MultiplayerOptional;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000CAC6 File Offset: 0x0000ACC6
		[JsonConstructor]
		private ModuleInfoModel(string id, string name, string version, ModuleCategory category)
		{
			this.Id = id;
			this.Name = name;
			this.Version = version;
			this.Category = category;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		internal ModuleInfoModel(ModuleInfo moduleInfo) : this(moduleInfo.Id, moduleInfo.Name, moduleInfo.Version.ToString(), moduleInfo.Category)
		{
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000CB25 File Offset: 0x0000AD25
		public static bool ShouldIncludeInSession(ModuleInfo moduleInfo)
		{
			return !moduleInfo.IsOfficial && moduleInfo.HasMultiplayerCategory;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0000CB37 File Offset: 0x0000AD37
		public static bool TryCreateForSession(ModuleInfo moduleInfo, out ModuleInfoModel moduleInfoModel)
		{
			if (ModuleInfoModel.ShouldIncludeInSession(moduleInfo))
			{
				moduleInfoModel = new ModuleInfoModel(moduleInfo);
				return true;
			}
			moduleInfoModel = null;
			return false;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0000CB50 File Offset: 0x0000AD50
		public override bool Equals(object obj)
		{
			ModuleInfoModel moduleInfoModel;
			return (moduleInfoModel = (obj as ModuleInfoModel)) != null && this.Id == moduleInfoModel.Id && this.Version == moduleInfoModel.Version;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0000CB8D File Offset: 0x0000AD8D
		public override int GetHashCode()
		{
			return (-612338121 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Id)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Version);
		}
	}
}
