using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Common;
using MCM.UI.Adapter.MCMv5.Base;
using MCM.UI.Adapter.MCMv5.Presets;

namespace MCM.UI.Adapter.MCMv5.Providers
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MCMv5SettingsProviderWrapper : SettingsProviderWrapper
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002648 File Offset: 0x00000848
		public MCMv5SettingsProviderWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			this._methodGetPresetsDelegate = AccessTools2.GetDelegate<MCMv5SettingsProviderWrapper.GetPresetsDelegate>(@object, type, "GetPresets", null, null, true);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000267C File Offset: 0x0000087C
		[return: Nullable(2)]
		protected override BaseSettings Create(object obj)
		{
			Type type = obj.GetType();
			bool flag = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.Global.FluentGlobalSettings", true);
			BaseSettings result;
			if (flag)
			{
				result = new MCMv5FluentSettingsWrapper(obj);
			}
			else
			{
				bool flag2 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.Global.GlobalSettings", true);
				if (flag2)
				{
					result = new MCMv5AttributeSettingsWrapper(obj);
				}
				else
				{
					bool flag3 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.PerCampaign.FluentPerCampaignSettings", true);
					if (flag3)
					{
						result = new MCMv5FluentSettingsWrapper(obj);
					}
					else
					{
						bool flag4 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.PerCampaign.PerCampaignSettings", true);
						if (flag4)
						{
							result = new MCMv5AttributeSettingsWrapper(obj);
						}
						else
						{
							bool flag5 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.PerSave.FluentPerSaveSettings", true);
							if (flag5)
							{
								result = new MCMv5FluentSettingsWrapper(obj);
							}
							else
							{
								bool flag6 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.PerSave.PerSaveSettings", true);
								if (flag6)
								{
									result = new MCMv5AttributeSettingsWrapper(obj);
								}
								else
								{
									result = null;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002738 File Offset: 0x00000938
		protected override bool IsSettings(BaseSettings settings, [Nullable(2)] [NotNullWhen(true)] out object wrapped)
		{
			object obj;
			bool flag;
			if (settings is MCMv5AttributeSettingsWrapper || settings is MCMv5FluentSettingsWrapper)
			{
				IWrapper wrapper = (IWrapper)settings;
				obj = wrapper.Object;
				if (obj != null)
				{
					flag = true;
					goto IL_2C;
				}
			}
			flag = false;
			IL_2C:
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				wrapped = obj;
				result = true;
			}
			else
			{
				wrapped = null;
				result = false;
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000278C File Offset: 0x0000098C
		public override IEnumerable<ISettingsPreset> GetPresets(string settingsId)
		{
			BaseSettings settings = this.GetSettings(settingsId);
			object obj;
			bool flag;
			if (settings is MCMv5AttributeSettingsWrapper)
			{
				IWrapper wrapper = settings as IWrapper;
				if (wrapper != null)
				{
					obj = wrapper.Object;
					if (obj != null)
					{
						flag = (this._methodGetPresetsDelegate == null);
						goto IL_34;
					}
				}
			}
			flag = true;
			IL_34:
			bool flag2 = flag;
			IEnumerable<ISettingsPreset> result;
			if (flag2)
			{
				result = Enumerable.Empty<ISettingsPreset>();
			}
			else
			{
				Type type = obj.GetType();
				IEnumerable<object> presets = this._methodGetPresetsDelegate(settingsId).OfType<object>();
				bool flag3 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.Global.FluentGlobalSettings", true);
				if (flag3)
				{
					result = from x in presets
					select new MCMv5FluentSettingsPresetWrapper(x);
				}
				else
				{
					bool flag4 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.Global.GlobalSettings", true);
					if (flag4)
					{
						result = from x in presets
						select new MCMv5AttributeSettingsPresetWrapper(x);
					}
					else
					{
						bool flag5 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.PerCampaign.FluentPerCampaignSettings", true);
						if (flag5)
						{
							result = from x in presets
							select new MCMv5FluentSettingsPresetWrapper(x);
						}
						else
						{
							bool flag6 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.PerCampaign.PerCampaignSettings", true);
							if (flag6)
							{
								result = from x in presets
								select new MCMv5AttributeSettingsPresetWrapper(x);
							}
							else
							{
								bool flag7 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.PerSave.FluentPerSaveSettings", true);
								if (flag7)
								{
									result = from x in presets
									select new MCMv5FluentSettingsPresetWrapper(x);
								}
								else
								{
									bool flag8 = ReflectionUtils.ImplementsOrImplementsEquivalent(type, "MCM.Abstractions.Base.PerSave.PerSaveSettings", true);
									if (flag8)
									{
										result = from x in presets
										select new MCMv5AttributeSettingsPresetWrapper(x);
									}
									else
									{
										result = Enumerable.Empty<ISettingsPreset>();
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000296E File Offset: 0x00000B6E
		public override IEnumerable<UnavailableSetting> GetUnavailableSettings()
		{
			return Enumerable.Empty<UnavailableSetting>();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002975 File Offset: 0x00000B75
		public override IEnumerable<SettingSnapshot> SaveAvailableSnapshots()
		{
			return Enumerable.Empty<SettingSnapshot>();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000297C File Offset: 0x00000B7C
		public override IEnumerable<BaseSettings> LoadAvailableSnapshots(IEnumerable<SettingSnapshot> snapshots)
		{
			return Enumerable.Empty<BaseSettings>();
		}

		// Token: 0x04000008 RID: 8
		[Nullable(2)]
		private readonly MCMv5SettingsProviderWrapper.GetPresetsDelegate _methodGetPresetsDelegate;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x06000126 RID: 294
		[NullableContext(0)]
		private delegate IEnumerable GetPresetsDelegate(string settingsId);
	}
}
