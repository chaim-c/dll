using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Common
{
	// Token: 0x0200001A RID: 26
	[NullableContext(2)]
	[Nullable(0)]
	public class RefWrapper : IRef, INotifyPropertyChanged, IWrapper
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003BC0 File Offset: 0x00001DC0
		[Nullable(1)]
		public object Object { [NullableContext(1)] get; }

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600008D RID: 141 RVA: 0x00003BC8 File Offset: 0x00001DC8
		// (remove) Token: 0x0600008E RID: 142 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				INotifyPropertyChanged notifyPropertyChanged = this.Object as INotifyPropertyChanged;
				bool flag = notifyPropertyChanged != null;
				if (flag)
				{
					notifyPropertyChanged.PropertyChanged += value;
				}
			}
			remove
			{
				INotifyPropertyChanged notifyPropertyChanged = this.Object as INotifyPropertyChanged;
				bool flag = notifyPropertyChanged != null;
				if (flag)
				{
					notifyPropertyChanged.PropertyChanged -= value;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003C1E File Offset: 0x00001E1E
		[Nullable(1)]
		public Type Type
		{
			[NullableContext(1)]
			get
			{
				return this._getTypeDelegate();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003C2B File Offset: 0x00001E2B
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003C38 File Offset: 0x00001E38
		public object Value
		{
			get
			{
				return this._getValueDelegate();
			}
			set
			{
				bool flag = this._setValueDelegate != null && value != null;
				if (flag)
				{
					this._setValueDelegate(value);
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003C68 File Offset: 0x00001E68
		[NullableContext(1)]
		public RefWrapper(object @object)
		{
			this.Object = @object;
			Type type = @object.GetType();
			this._getTypeDelegate = AccessTools2.GetPropertyGetterDelegate<RefWrapper.GetTypeDelegate>(@object, type, "Type", true);
			this._getValueDelegate = AccessTools2.GetPropertyGetterDelegate<RefWrapper.GetValueDelegate>(@object, type, "Value", true);
			this._setValueDelegate = AccessTools2.GetPropertySetterDelegate<RefWrapper.SetValueDelegate>(@object, type, "Value", true);
		}

		// Token: 0x04000021 RID: 33
		private readonly RefWrapper.GetTypeDelegate _getTypeDelegate;

		// Token: 0x04000022 RID: 34
		private readonly RefWrapper.GetValueDelegate _getValueDelegate;

		// Token: 0x04000023 RID: 35
		private readonly RefWrapper.SetValueDelegate _setValueDelegate;

		// Token: 0x0200016A RID: 362
		// (Invoke) Token: 0x060009E7 RID: 2535
		[NullableContext(0)]
		private delegate Type GetTypeDelegate();

		// Token: 0x0200016B RID: 363
		// (Invoke) Token: 0x060009EB RID: 2539
		[NullableContext(0)]
		private delegate object GetValueDelegate();

		// Token: 0x0200016C RID: 364
		// (Invoke) Token: 0x060009EF RID: 2543
		[NullableContext(0)]
		private delegate void SetValueDelegate(object value);
	}
}
