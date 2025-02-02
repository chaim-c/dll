using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;
using MCM.Abstractions.GameFeatures;

namespace MCM.Abstractions
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MemorySettingsFormat : ISettingsFormat
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public IEnumerable<string> FormatTypes { get; } = new string[]
		{
			"memory"
		};

		// Token: 0x060001C8 RID: 456 RVA: 0x00007DB8 File Offset: 0x00005FB8
		public BaseSettings Load(BaseSettings settings, GameDirectory directory, string filename)
		{
			BaseSettings sett;
			bool flag = this._settings.TryGetValue(directory.Path + filename, out sett) || settings != sett;
			if (flag)
			{
				SettingsUtils.OverrideSettings(settings, sett);
			}
			return settings;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007DFC File Offset: 0x00005FFC
		public bool Save(BaseSettings settings, GameDirectory directory, string filename)
		{
			this._settings[directory.Path + filename] = settings;
			return true;
		}

		// Token: 0x0400007D RID: 125
		private readonly Dictionary<string, BaseSettings> _settings = new Dictionary<string, BaseSettings>();
	}
}
