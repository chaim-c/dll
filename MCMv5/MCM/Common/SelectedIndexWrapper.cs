using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Common
{
	// Token: 0x0200001E RID: 30
	[NullableContext(2)]
	[Nullable(0)]
	public readonly ref struct SelectedIndexWrapper
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003F38 File Offset: 0x00002138
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003F52 File Offset: 0x00002152
		public int SelectedIndex
		{
			get
			{
				SelectedIndexWrapper.GetSelectedIndexDelegate getSelectedIndex = this._getSelectedIndex;
				return (getSelectedIndex != null) ? getSelectedIndex(this._object) : -1;
			}
			set
			{
				SelectedIndexWrapper.SetSelectedIndexDelegate setSelectedIndex = this._setSelectedIndex;
				if (setSelectedIndex != null)
				{
					setSelectedIndex(this._object, value);
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003F70 File Offset: 0x00002170
		public SelectedIndexWrapper(object @object)
		{
			this._object = @object;
			Type type = (@object != null) ? @object.GetType() : null;
			SelectedIndexWrapper.GetSelectedIndexDelegate getSelectedIndex;
			if (type == null)
			{
				getSelectedIndex = null;
			}
			else
			{
				getSelectedIndex = SelectedIndexWrapper._getSelectedIndexCache.GetOrAdd(type, (Type t) => AccessTools2.GetPropertyGetterDelegate<SelectedIndexWrapper.GetSelectedIndexDelegate>(t, "SelectedIndex", false));
			}
			this._getSelectedIndex = getSelectedIndex;
			SelectedIndexWrapper.SetSelectedIndexDelegate setSelectedIndex;
			if (type == null)
			{
				setSelectedIndex = null;
			}
			else
			{
				setSelectedIndex = SelectedIndexWrapper._setSelectedIndexCache.GetOrAdd(type, (Type t) => AccessTools2.GetPropertySetterDelegate<SelectedIndexWrapper.SetSelectedIndexDelegate>(t, "SelectedIndex", false));
			}
			this._setSelectedIndex = setSelectedIndex;
		}

		// Token: 0x0400002B RID: 43
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private static readonly ConcurrentDictionary<Type, SelectedIndexWrapper.GetSelectedIndexDelegate> _getSelectedIndexCache = new ConcurrentDictionary<Type, SelectedIndexWrapper.GetSelectedIndexDelegate>();

		// Token: 0x0400002C RID: 44
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private static readonly ConcurrentDictionary<Type, SelectedIndexWrapper.SetSelectedIndexDelegate> _setSelectedIndexCache = new ConcurrentDictionary<Type, SelectedIndexWrapper.SetSelectedIndexDelegate>();

		// Token: 0x0400002D RID: 45
		private readonly SelectedIndexWrapper.GetSelectedIndexDelegate _getSelectedIndex;

		// Token: 0x0400002E RID: 46
		private readonly SelectedIndexWrapper.SetSelectedIndexDelegate _setSelectedIndex;

		// Token: 0x0400002F RID: 47
		private readonly object _object;

		// Token: 0x0200016E RID: 366
		// (Invoke) Token: 0x060009F7 RID: 2551
		[NullableContext(0)]
		private delegate int GetSelectedIndexDelegate(object instance);

		// Token: 0x0200016F RID: 367
		// (Invoke) Token: 0x060009FB RID: 2555
		[NullableContext(0)]
		private delegate void SetSelectedIndexDelegate(object instance, int value);
	}
}
