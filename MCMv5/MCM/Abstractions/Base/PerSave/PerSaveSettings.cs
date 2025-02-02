using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.PerSave
{
	// Token: 0x020000B2 RID: 178
	public abstract class PerSaveSettings<T> : PerSaveSettings where T : PerSaveSettings, new()
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000B680 File Offset: 0x00009880
		[Nullable(2)]
		public static T Instance
		{
			[NullableContext(2)]
			get
			{
				bool flag = !PerSaveSettings.Cache.ContainsKey(typeof(T));
				if (flag)
				{
					PerSaveSettings.Cache.TryAdd(typeof(T), Activator.CreateInstance<T>().Id);
				}
				BaseSettingsProvider instance = BaseSettingsProvider.Instance;
				return ((instance != null) ? instance.GetSettings(PerSaveSettings.Cache[typeof(T)]) : null) as T;
			}
		}
	}
}
