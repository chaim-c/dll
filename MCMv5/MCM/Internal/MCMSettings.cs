using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Localization;

namespace MCM.Internal
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class MCMSettings : AttributeGlobalSettings<MCMSettings>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002F98 File Offset: 0x00001198
		public override string Id
		{
			get
			{
				return "MCM_v5";
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override string DisplayName
		{
			get
			{
				string value = "{=MCMSettings_Name}Mod Configuration Menu {VERSION}";
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				Dictionary<string, object> dictionary2 = dictionary;
				string key = "VERSION";
				Version version = typeof(MCMSettings).Assembly.GetName().Version;
				dictionary2.Add(key, ((version != null) ? version.ToString(3) : null) ?? "ERROR");
				return new TextObject(value, dictionary).ToString() ?? "ERROR";
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003007 File Offset: 0x00001207
		public override string FolderName
		{
			get
			{
				return "MCM";
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000300E File Offset: 0x0000120E
		public override string FormatType
		{
			get
			{
				return "none";
			}
		}
	}
}
