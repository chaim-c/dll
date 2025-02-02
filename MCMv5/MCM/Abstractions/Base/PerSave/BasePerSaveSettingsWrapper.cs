using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Common;

namespace MCM.Abstractions.Base.PerSave
{
	// Token: 0x020000AE RID: 174
	[NullableContext(1)]
	[Nullable(0)]
	[Obsolete("Will be removed from future API", true)]
	public abstract class BasePerSaveSettingsWrapper : PerSaveSettings, IWrapper
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000B19A File Offset: 0x0000939A
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000B1A2 File Offset: 0x000093A2
		public object Object { get; protected set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000B1AB File Offset: 0x000093AB
		public override string Id
		{
			get
			{
				BasePerSaveSettingsWrapper.GetIdDelegate getIdDelegate = this._getIdDelegate;
				return ((getIdDelegate != null) ? getIdDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000B1C8 File Offset: 0x000093C8
		public override string FolderName
		{
			get
			{
				BasePerSaveSettingsWrapper.GetFolderNameDelegate getFolderNameDelegate = this._getFolderNameDelegate;
				return ((getFolderNameDelegate != null) ? getFolderNameDelegate() : null) ?? string.Empty;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000B1E5 File Offset: 0x000093E5
		public override string DisplayName
		{
			get
			{
				BasePerSaveSettingsWrapper.GetDisplayNameDelegate getDisplayNameDelegate = this._getDisplayNameDelegate;
				return ((getDisplayNameDelegate != null) ? getDisplayNameDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000B202 File Offset: 0x00009402
		public override int UIVersion
		{
			get
			{
				BasePerSaveSettingsWrapper.GetUIVersionDelegate getUIVersionDelegate = this._getUIVersionDelegate;
				return (getUIVersionDelegate != null) ? getUIVersionDelegate() : 1;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000B216 File Offset: 0x00009416
		public override string SubFolder
		{
			get
			{
				BasePerSaveSettingsWrapper.GetSubFolderDelegate getSubFolderDelegate = this._getSubFolderDelegate;
				return ((getSubFolderDelegate != null) ? getSubFolderDelegate() : null) ?? string.Empty;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000B233 File Offset: 0x00009433
		public override char SubGroupDelimiter
		{
			get
			{
				BasePerSaveSettingsWrapper.GetSubGroupDelimiterDelegate getSubGroupDelimiterDelegate = this._getSubGroupDelimiterDelegate;
				return (getSubGroupDelimiterDelegate != null) ? getSubGroupDelimiterDelegate() : '/';
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600039C RID: 924 RVA: 0x0000B248 File Offset: 0x00009448
		// (remove) Token: 0x0600039D RID: 925 RVA: 0x0000B274 File Offset: 0x00009474
		[Nullable(2)]
		public override event PropertyChangedEventHandler PropertyChanged
		{
			[NullableContext(2)]
			add
			{
				INotifyPropertyChanged notifyPropertyChanged = this.Object as INotifyPropertyChanged;
				bool flag = notifyPropertyChanged != null;
				if (flag)
				{
					notifyPropertyChanged.PropertyChanged += value;
				}
			}
			[NullableContext(2)]
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

		// Token: 0x0600039E RID: 926 RVA: 0x0000B2A0 File Offset: 0x000094A0
		protected BasePerSaveSettingsWrapper(object @object)
		{
			this.Object = @object;
			Type type = @object.GetType();
			this._getIdDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerSaveSettingsWrapper.GetIdDelegate>(@object, type, "Id", true);
			this._getFolderNameDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerSaveSettingsWrapper.GetFolderNameDelegate>(@object, type, "FolderName", true);
			this._getDisplayNameDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerSaveSettingsWrapper.GetDisplayNameDelegate>(@object, type, "DisplayName", true);
			this._getUIVersionDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerSaveSettingsWrapper.GetUIVersionDelegate>(@object, type, "UIVersion", true);
			this._getSubFolderDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerSaveSettingsWrapper.GetSubFolderDelegate>(@object, type, "SubFolder", true);
			this._getSubGroupDelimiterDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerSaveSettingsWrapper.GetSubGroupDelimiterDelegate>(@object, type, "SubGroupDelimiter", true);
			this._methodOnPropertyChangedDelegate = AccessTools2.GetDelegate<BasePerSaveSettingsWrapper.OnPropertyChangedDelegate>(@object, type, "OnPropertyChanged", null, null, true);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B34B File Offset: 0x0000954B
		[NullableContext(2)]
		public override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			BasePerSaveSettingsWrapper.OnPropertyChangedDelegate methodOnPropertyChangedDelegate = this._methodOnPropertyChangedDelegate;
			if (methodOnPropertyChangedDelegate != null)
			{
				methodOnPropertyChangedDelegate(propertyName);
			}
		}

		// Token: 0x04000128 RID: 296
		[Nullable(2)]
		private readonly BasePerSaveSettingsWrapper.GetIdDelegate _getIdDelegate;

		// Token: 0x04000129 RID: 297
		[Nullable(2)]
		private readonly BasePerSaveSettingsWrapper.GetFolderNameDelegate _getFolderNameDelegate;

		// Token: 0x0400012A RID: 298
		[Nullable(2)]
		private readonly BasePerSaveSettingsWrapper.GetDisplayNameDelegate _getDisplayNameDelegate;

		// Token: 0x0400012B RID: 299
		[Nullable(2)]
		private readonly BasePerSaveSettingsWrapper.GetUIVersionDelegate _getUIVersionDelegate;

		// Token: 0x0400012C RID: 300
		[Nullable(2)]
		private readonly BasePerSaveSettingsWrapper.GetSubFolderDelegate _getSubFolderDelegate;

		// Token: 0x0400012D RID: 301
		[Nullable(2)]
		private readonly BasePerSaveSettingsWrapper.GetSubGroupDelimiterDelegate _getSubGroupDelimiterDelegate;

		// Token: 0x0400012E RID: 302
		[Nullable(2)]
		private readonly BasePerSaveSettingsWrapper.OnPropertyChangedDelegate _methodOnPropertyChangedDelegate;

		// Token: 0x020001BD RID: 445
		// (Invoke) Token: 0x06000BA4 RID: 2980
		[NullableContext(0)]
		private delegate string GetIdDelegate();

		// Token: 0x020001BE RID: 446
		// (Invoke) Token: 0x06000BA8 RID: 2984
		[NullableContext(0)]
		private delegate string GetFolderNameDelegate();

		// Token: 0x020001BF RID: 447
		// (Invoke) Token: 0x06000BAC RID: 2988
		[NullableContext(0)]
		private delegate string GetDisplayNameDelegate();

		// Token: 0x020001C0 RID: 448
		// (Invoke) Token: 0x06000BB0 RID: 2992
		[NullableContext(0)]
		private delegate int GetUIVersionDelegate();

		// Token: 0x020001C1 RID: 449
		// (Invoke) Token: 0x06000BB4 RID: 2996
		[NullableContext(0)]
		private delegate string GetSubFolderDelegate();

		// Token: 0x020001C2 RID: 450
		// (Invoke) Token: 0x06000BB8 RID: 3000
		[NullableContext(0)]
		private delegate char GetSubGroupDelimiterDelegate();

		// Token: 0x020001C3 RID: 451
		// (Invoke) Token: 0x06000BBC RID: 3004
		[NullableContext(0)]
		private delegate void OnPropertyChangedDelegate(string propertyName);
	}
}
