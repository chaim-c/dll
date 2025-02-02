using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Base
{
	// Token: 0x020000AB RID: 171
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BaseSettings : INotifyPropertyChanged
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000381 RID: 897 RVA: 0x0000B040 File Offset: 0x00009240
		// (remove) Token: 0x06000382 RID: 898 RVA: 0x0000B078 File Offset: 0x00009278
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000383 RID: 899
		public abstract string Id { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000384 RID: 900
		public abstract string DisplayName { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000B0AD File Offset: 0x000092AD
		public virtual string FolderName { get; } = string.Empty;

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000B0B5 File Offset: 0x000092B5
		public virtual string SubFolder
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000B0BC File Offset: 0x000092BC
		public virtual string FormatType
		{
			get
			{
				return "none";
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000B0C3 File Offset: 0x000092C3
		public virtual string DiscoveryType
		{
			get
			{
				return "none";
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000B0CA File Offset: 0x000092CA
		public virtual int UIVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000B0CD File Offset: 0x000092CD
		public virtual char SubGroupDelimiter
		{
			get
			{
				return '/';
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B0D1 File Offset: 0x000092D1
		[NullableContext(2)]
		public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B0EC File Offset: 0x000092EC
		public virtual BaseSettings CreateNew()
		{
			Type type = base.GetType();
			BaseSettings.SettingsCtor constructor = BaseSettings._cachedConstructors.GetOrAdd(type, (Type t) => AccessTools2.GetConstructorDelegate<BaseSettings.SettingsCtor>(t, null, true) ?? (() => (BaseSettings)FormatterServices.GetUninitializedObject(t)));
			return constructor();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B138 File Offset: 0x00009338
		public virtual BaseSettings CopyAsNew()
		{
			BaseSettings newSettings = this.CreateNew();
			SettingsUtils.OverrideSettings(newSettings, this);
			return newSettings;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B15A File Offset: 0x0000935A
		public virtual IEnumerable<ISettingsPreset> GetBuiltInPresets()
		{
			yield return new MemorySettingsPreset(this.Id, "default", "{=BaseSettings_Default}Default", new Func<BaseSettings>(this.CreateNew));
			yield break;
		}

		// Token: 0x04000121 RID: 289
		private static readonly ConcurrentDictionary<Type, BaseSettings.SettingsCtor> _cachedConstructors = new ConcurrentDictionary<Type, BaseSettings.SettingsCtor>();

		// Token: 0x04000122 RID: 290
		public const string SaveTriggered = "SAVE_TRIGGERED";

		// Token: 0x04000123 RID: 291
		public const string LoadingComplete = "LOADING_COMPLETE";

		// Token: 0x04000124 RID: 292
		public const string DefaultPresetId = "default";

		// Token: 0x04000125 RID: 293
		public const string DefaultPresetName = "{=BaseSettings_Default}Default";

		// Token: 0x020001B9 RID: 441
		// (Invoke) Token: 0x06000B93 RID: 2963
		[NullableContext(0)]
		private delegate BaseSettings SettingsCtor();
	}
}
