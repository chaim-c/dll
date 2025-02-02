using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000C1 RID: 193
	public abstract class GlobalSettings<T> : GlobalSettings where T : GlobalSettings, new()
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000C228 File Offset: 0x0000A428
		[Nullable(2)]
		public static T Instance
		{
			[NullableContext(2)]
			get
			{
				bool flag = !GlobalSettings.Cache.ContainsKey(typeof(T));
				if (flag)
				{
					GlobalSettings.Cache.TryAdd(typeof(T), Activator.CreateInstance<T>().Id);
				}
				BaseSettingsProvider instance = BaseSettingsProvider.Instance;
				return ((instance != null) ? instance.GetSettings(GlobalSettings.Cache[typeof(T)]) : null) as T;
			}
		}
	}
}
