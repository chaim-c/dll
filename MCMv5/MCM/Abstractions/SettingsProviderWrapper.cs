using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Wrapper;
using MCM.Common;

namespace MCM.Abstractions
{
	// Token: 0x02000066 RID: 102
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SettingsProviderWrapper : BaseSettingsProvider, IWrapper
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00008D34 File Offset: 0x00006F34
		public override IEnumerable<SettingsDefinition> SettingsDefinitions
		{
			get
			{
				SettingsProviderWrapper.GetSettingsDefinitionsDelegate methodGetSettingsDefinitions = this._methodGetSettingsDefinitions;
				IEnumerable<SettingsDefinition> enumerable;
				if (methodGetSettingsDefinitions == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = from object x in methodGetSettingsDefinitions()
					select new SettingsDefinitionWrapper(x);
				}
				IEnumerable<SettingsDefinition> enumerable2 = enumerable;
				return enumerable2 ?? Enumerable.Empty<SettingsDefinition>();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00008D87 File Offset: 0x00006F87
		public object Object { get; }

		// Token: 0x0600024F RID: 591 RVA: 0x00008D90 File Offset: 0x00006F90
		protected SettingsProviderWrapper(object @object)
		{
			this.Object = @object;
			Type type = @object.GetType();
			this._methodGetSettingsDefinitions = AccessTools2.GetPropertyGetterDelegate<SettingsProviderWrapper.GetSettingsDefinitionsDelegate>(@object, type, "SettingsDefinitions", true);
			this._methodGetSettingsDelegate = AccessTools2.GetDelegate<SettingsProviderWrapper.GetSettingsDelegate>(@object, type, "GetSettings", null, null, true);
			this._methodSaveSettingsDelegate = AccessTools2.GetDelegate<SettingsProviderWrapper.SaveSettingsDelegate>(@object, type, "SaveSettings", null, null, true);
			this._methodOverrideSettingsDelegate = AccessTools2.GetDelegate<SettingsProviderWrapper.OverrideSettingsDelegate>(@object, type, "OverrideSettings", null, null, true);
			this._methodResetSettingsDelegate = AccessTools2.GetDelegate<SettingsProviderWrapper.ResetSettingsDelegate>(@object, type, "ResetSettings", null, null, true);
		}

		// Token: 0x06000250 RID: 592
		[return: Nullable(2)]
		protected abstract BaseSettings Create(object obj);

		// Token: 0x06000251 RID: 593
		protected abstract bool IsSettings(BaseSettings settings, [Nullable(2)] [NotNullWhen(true)] out object wrapped);

		// Token: 0x06000252 RID: 594 RVA: 0x00008E1C File Offset: 0x0000701C
		[return: Nullable(2)]
		public override BaseSettings GetSettings(string id)
		{
			SettingsProviderWrapper.GetSettingsDelegate methodGetSettingsDelegate = this._methodGetSettingsDelegate;
			object obj = (methodGetSettingsDelegate != null) ? methodGetSettingsDelegate(id) : null;
			return (obj != null) ? this.Create(obj) : null;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008E4C File Offset: 0x0000704C
		public override void SaveSettings(BaseSettings settings)
		{
			object wrapped;
			bool flag = this.IsSettings(settings, out wrapped);
			if (flag)
			{
				SettingsProviderWrapper.SaveSettingsDelegate methodSaveSettingsDelegate = this._methodSaveSettingsDelegate;
				if (methodSaveSettingsDelegate != null)
				{
					methodSaveSettingsDelegate(wrapped);
				}
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008E7C File Offset: 0x0000707C
		public override void OverrideSettings(BaseSettings settings)
		{
			object wrapped;
			bool flag = this.IsSettings(settings, out wrapped);
			if (flag)
			{
				SettingsProviderWrapper.OverrideSettingsDelegate methodOverrideSettingsDelegate = this._methodOverrideSettingsDelegate;
				if (methodOverrideSettingsDelegate != null)
				{
					methodOverrideSettingsDelegate(wrapped);
				}
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008EAC File Offset: 0x000070AC
		public override void ResetSettings(BaseSettings settings)
		{
			object wrapped;
			bool flag = this.IsSettings(settings, out wrapped);
			if (flag)
			{
				SettingsProviderWrapper.ResetSettingsDelegate methodResetSettingsDelegate = this._methodResetSettingsDelegate;
				if (methodResetSettingsDelegate != null)
				{
					methodResetSettingsDelegate(wrapped);
				}
			}
		}

		// Token: 0x06000256 RID: 598
		public abstract override IEnumerable<ISettingsPreset> GetPresets(string id);

		// Token: 0x040000B7 RID: 183
		[Nullable(2)]
		private readonly SettingsProviderWrapper.GetSettingsDefinitionsDelegate _methodGetSettingsDefinitions;

		// Token: 0x040000B8 RID: 184
		[Nullable(2)]
		private readonly SettingsProviderWrapper.GetSettingsDelegate _methodGetSettingsDelegate;

		// Token: 0x040000B9 RID: 185
		[Nullable(2)]
		private readonly SettingsProviderWrapper.SaveSettingsDelegate _methodSaveSettingsDelegate;

		// Token: 0x040000BA RID: 186
		[Nullable(2)]
		private readonly SettingsProviderWrapper.OverrideSettingsDelegate _methodOverrideSettingsDelegate;

		// Token: 0x040000BB RID: 187
		[Nullable(2)]
		private readonly SettingsProviderWrapper.ResetSettingsDelegate _methodResetSettingsDelegate;

		// Token: 0x020001AB RID: 427
		// (Invoke) Token: 0x06000B2F RID: 2863
		[NullableContext(0)]
		private delegate IEnumerable GetSettingsDefinitionsDelegate();

		// Token: 0x020001AC RID: 428
		// (Invoke) Token: 0x06000B33 RID: 2867
		[NullableContext(0)]
		[return: Nullable(2)]
		private delegate object GetSettingsDelegate(string id);

		// Token: 0x020001AD RID: 429
		// (Invoke) Token: 0x06000B37 RID: 2871
		[NullableContext(0)]
		private delegate void SaveSettingsDelegate(object settings);

		// Token: 0x020001AE RID: 430
		// (Invoke) Token: 0x06000B3B RID: 2875
		[NullableContext(0)]
		private delegate void OverrideSettingsDelegate(object settings);

		// Token: 0x020001AF RID: 431
		// (Invoke) Token: 0x06000B3F RID: 2879
		[NullableContext(0)]
		private delegate void ResetSettingsDelegate(object settings);
	}
}
