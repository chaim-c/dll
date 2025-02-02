using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Localization;

namespace MCM.UI
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class MCMUISettings : AttributeGlobalSettings<MCMUISettings>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026F8 File Offset: 0x000008F8
		public override string Id
		{
			get
			{
				return "MCMUI_v4";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002700 File Offset: 0x00000900
		public override string DisplayName
		{
			get
			{
				string value = "{=MCMUISettings_Name}MCM UI {VERSION}";
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				Dictionary<string, object> dictionary2 = dictionary;
				string key = "VERSION";
				Version version = typeof(MCMUISettings).Assembly.GetName().Version;
				dictionary2.Add(key, ((version != null) ? version.ToString(3) : null) ?? "ERROR");
				return new TextObject(value, dictionary).ToString();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000275E File Offset: 0x0000095E
		public override string FolderName
		{
			get
			{
				return "MCM";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002765 File Offset: 0x00000965
		public override string FormatType
		{
			get
			{
				return "json2";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000276C File Offset: 0x0000096C
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002774 File Offset: 0x00000974
		[SettingPropertyBool("{=MCMUISettings_Name_HideMainMenuEntry}Hide Main Menu Entry", Order = 1, RequireRestart = false, HintText = "{=MCMUISettings_Name_HideMainMenuEntryDesc}Hides MCM's Main Menu 'Mod Options' Menu Entry.")]
		[SettingPropertyGroup("{=MCMUISettings_Name_General}General")]
		public bool UseStandardOptionScreen
		{
			get
			{
				return this._useStandardOptionScreen;
			}
			set
			{
				bool flag = this._useStandardOptionScreen != value;
				if (flag)
				{
					this._useStandardOptionScreen = value;
					this.OnPropertyChanged(null);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027A4 File Offset: 0x000009A4
		[NullableContext(2)]
		public override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			bool flag = propertyName == "SAVE_TRIGGERED" || propertyName == "LOADING_COMPLETE";
			bool flag2 = flag;
			if (flag2)
			{
				MCMUISubModule.UpdateOptionScreen(this);
			}
		}

		// Token: 0x0400000A RID: 10
		private bool _useStandardOptionScreen;
	}
}
