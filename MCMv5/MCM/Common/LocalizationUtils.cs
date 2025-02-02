using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Common
{
	// Token: 0x0200001D RID: 29
	public static class LocalizationUtils
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003F1C File Offset: 0x0000211C
		[NullableContext(1)]
		public static string Localize(string rawText, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Dictionary<string, object> attributes = null)
		{
			return LocalizationUtils.TextObjectWrapper.Create(rawText, attributes).Localize();
		}

		// Token: 0x0200016D RID: 365
		[NullableContext(2)]
		[Nullable(0)]
		private readonly ref struct TextObjectWrapper
		{
			// Token: 0x060009F2 RID: 2546 RVA: 0x00021BFE File Offset: 0x0001FDFE
			[NullableContext(1)]
			public static LocalizationUtils.TextObjectWrapper Create(string rawText, [Nullable(new byte[]
			{
				2,
				1,
				1
			})] Dictionary<string, object> attributes = null)
			{
				LocalizationUtils.TextObjectWrapper.TextObjectCtorDelegate textObjectCtor = LocalizationUtils.TextObjectWrapper._textObjectCtor;
				return new LocalizationUtils.TextObjectWrapper((textObjectCtor != null) ? textObjectCtor(rawText, attributes) : null);
			}

			// Token: 0x060009F3 RID: 2547 RVA: 0x00021C18 File Offset: 0x0001FE18
			private TextObjectWrapper(object @object)
			{
				this._object = @object;
			}

			// Token: 0x060009F4 RID: 2548 RVA: 0x00021C22 File Offset: 0x0001FE22
			[NullableContext(1)]
			public string Localize()
			{
				return (this._object != null && LocalizationUtils.TextObjectWrapper._toString != null) ? (LocalizationUtils.TextObjectWrapper._toString(this._object) ?? string.Empty) : string.Empty;
			}

			// Token: 0x040002B2 RID: 690
			private static readonly LocalizationUtils.TextObjectWrapper.TextObjectCtorDelegate _textObjectCtor = AccessTools2.GetDeclaredConstructorDelegate<LocalizationUtils.TextObjectWrapper.TextObjectCtorDelegate>("TaleWorlds.Localization.TextObject", new Type[]
			{
				typeof(string),
				typeof(Dictionary<string, object>)
			}, true);

			// Token: 0x040002B3 RID: 691
			private static readonly LocalizationUtils.TextObjectWrapper.ToStringDelegate _toString = AccessTools2.GetDeclaredDelegate<LocalizationUtils.TextObjectWrapper.ToStringDelegate>("TaleWorlds.Localization.TextObject:ToString", Type.EmptyTypes, null, true);

			// Token: 0x040002B4 RID: 692
			private readonly object _object;

			// Token: 0x020002AF RID: 687
			// (Invoke) Token: 0x06000EF1 RID: 3825
			[NullableContext(0)]
			private delegate object TextObjectCtorDelegate(string rawText, [Nullable(new byte[]
			{
				2,
				1,
				1
			})] Dictionary<string, object> attributes = null);

			// Token: 0x020002B0 RID: 688
			// (Invoke) Token: 0x06000EF5 RID: 3829
			[NullableContext(0)]
			[return: Nullable(2)]
			private delegate string ToStringDelegate(object instance);
		}
	}
}
