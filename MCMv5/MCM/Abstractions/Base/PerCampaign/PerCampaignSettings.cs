using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.PerCampaign
{
	// Token: 0x020000B9 RID: 185
	public abstract class PerCampaignSettings<T> : PerCampaignSettings where T : PerCampaignSettings, new()
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000BC4C File Offset: 0x00009E4C
		[Nullable(2)]
		public static T Instance
		{
			[NullableContext(2)]
			get
			{
				bool flag = !PerCampaignSettings.Cache.ContainsKey(typeof(T));
				if (flag)
				{
					PerCampaignSettings.Cache.TryAdd(typeof(T), Activator.CreateInstance<T>().Id);
				}
				BaseSettingsProvider instance = BaseSettingsProvider.Instance;
				return ((instance != null) ? instance.GetSettings(PerCampaignSettings.Cache[typeof(T)]) : null) as T;
			}
		}
	}
}
