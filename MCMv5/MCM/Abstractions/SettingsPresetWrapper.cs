using System;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions.Base;
using MCM.Common;

namespace MCM.Abstractions
{
	// Token: 0x02000061 RID: 97
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SettingsPresetWrapper<[Nullable(0)] TSetting> : ISettingsPreset, IWrapper where TSetting : BaseSettings, IWrapper
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public string SettingsId
		{
			get
			{
				SettingsPresetWrapper<TSetting>.GetSettingsIdDelegate methodGetSettingsIdDelegate = this._methodGetSettingsIdDelegate;
				return ((methodGetSettingsIdDelegate != null) ? methodGetSettingsIdDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00008BDF File Offset: 0x00006DDF
		public string Id
		{
			get
			{
				SettingsPresetWrapper<TSetting>.GetIdDelegate methodGetIdDelegate = this._methodGetIdDelegate;
				return ((methodGetIdDelegate != null) ? methodGetIdDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008BFC File Offset: 0x00006DFC
		public string Name
		{
			get
			{
				SettingsPresetWrapper<TSetting>.GetNameDelegate methodGetNameDelegate = this._methodGetNameDelegate;
				return ((methodGetNameDelegate != null) ? methodGetNameDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008C19 File Offset: 0x00006E19
		[Nullable(2)]
		public object Object { [NullableContext(2)] get; }

		// Token: 0x06000235 RID: 565 RVA: 0x00008C24 File Offset: 0x00006E24
		[NullableContext(2)]
		protected SettingsPresetWrapper(object @object)
		{
			bool flag = @object == null;
			if (!flag)
			{
				this.Object = @object;
				Type type = @object.GetType();
				this._methodGetSettingsIdDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsPresetWrapper<TSetting>.GetSettingsIdDelegate>(@object, type, "SettingsId", true);
				this._methodGetIdDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsPresetWrapper<TSetting>.GetIdDelegate>(@object, type, "Id", true);
				this._methodGetNameDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsPresetWrapper<TSetting>.GetNameDelegate>(@object, type, "Name", true);
				this._methodLoadPresetDelegate = AccessTools2.GetDelegate<SettingsPresetWrapper<TSetting>.LoadPresetDelegate>(@object, type, "LoadPreset", null, null, true);
				this._methodSavePresetDelegate = AccessTools2.GetDelegate<SettingsPresetWrapper<TSetting>.SavePresetDelegate>(@object, type, "SavePreset", null, null, true);
			}
		}

		// Token: 0x06000236 RID: 566
		protected abstract TSetting Create([Nullable(2)] object @object);

		// Token: 0x06000237 RID: 567 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public BaseSettings LoadPreset()
		{
			SettingsPresetWrapper<TSetting>.LoadPresetDelegate methodLoadPresetDelegate = this._methodLoadPresetDelegate;
			return this.Create((methodLoadPresetDelegate != null) ? methodLoadPresetDelegate() : null);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008CD4 File Offset: 0x00006ED4
		public bool SavePreset(BaseSettings settings)
		{
			TSetting tsetting = settings as TSetting;
			if (tsetting != null)
			{
				object obj = tsetting.Object;
				if (obj != null)
				{
					SettingsPresetWrapper<TSetting>.SavePresetDelegate methodSavePresetDelegate = this._methodSavePresetDelegate;
					return methodSavePresetDelegate != null && methodSavePresetDelegate(obj);
				}
			}
			return false;
		}

		// Token: 0x040000AB RID: 171
		[Nullable(new byte[]
		{
			2,
			0
		})]
		private readonly SettingsPresetWrapper<TSetting>.GetSettingsIdDelegate _methodGetSettingsIdDelegate;

		// Token: 0x040000AC RID: 172
		[Nullable(new byte[]
		{
			2,
			0
		})]
		private readonly SettingsPresetWrapper<TSetting>.GetIdDelegate _methodGetIdDelegate;

		// Token: 0x040000AD RID: 173
		[Nullable(new byte[]
		{
			2,
			0
		})]
		private readonly SettingsPresetWrapper<TSetting>.GetNameDelegate _methodGetNameDelegate;

		// Token: 0x040000AE RID: 174
		[Nullable(new byte[]
		{
			2,
			0
		})]
		private readonly SettingsPresetWrapper<TSetting>.LoadPresetDelegate _methodLoadPresetDelegate;

		// Token: 0x040000AF RID: 175
		[Nullable(new byte[]
		{
			2,
			0
		})]
		private readonly SettingsPresetWrapper<TSetting>.SavePresetDelegate _methodSavePresetDelegate;

		// Token: 0x020001A6 RID: 422
		// (Invoke) Token: 0x06000B1B RID: 2843
		[NullableContext(0)]
		private delegate string GetSettingsIdDelegate();

		// Token: 0x020001A7 RID: 423
		// (Invoke) Token: 0x06000B1F RID: 2847
		[NullableContext(0)]
		private delegate string GetIdDelegate();

		// Token: 0x020001A8 RID: 424
		// (Invoke) Token: 0x06000B23 RID: 2851
		[NullableContext(0)]
		private delegate string GetNameDelegate();

		// Token: 0x020001A9 RID: 425
		// (Invoke) Token: 0x06000B27 RID: 2855
		[NullableContext(0)]
		private delegate object LoadPresetDelegate();

		// Token: 0x020001AA RID: 426
		// (Invoke) Token: 0x06000B2B RID: 2859
		[NullableContext(0)]
		private delegate bool SavePresetDelegate(object settings);
	}
}
