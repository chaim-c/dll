using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Common;

namespace MCM.Abstractions.Base.PerCampaign
{
	// Token: 0x020000B5 RID: 181
	[NullableContext(1)]
	[Nullable(0)]
	[Obsolete("Will be removed from future API", true)]
	public abstract class BasePerCampaignSettingsWrapper : PerCampaignSettings, IWrapper
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000B764 File Offset: 0x00009964
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000B76C File Offset: 0x0000996C
		public object Object { get; protected set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000B775 File Offset: 0x00009975
		public override string Id
		{
			get
			{
				BasePerCampaignSettingsWrapper.GetIdDelegate getIdDelegate = this._getIdDelegate;
				return ((getIdDelegate != null) ? getIdDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000B792 File Offset: 0x00009992
		public override string FolderName
		{
			get
			{
				BasePerCampaignSettingsWrapper.GetFolderNameDelegate getFolderNameDelegate = this._getFolderNameDelegate;
				return ((getFolderNameDelegate != null) ? getFolderNameDelegate() : null) ?? string.Empty;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000B7AF File Offset: 0x000099AF
		public override string DisplayName
		{
			get
			{
				BasePerCampaignSettingsWrapper.GetDisplayNameDelegate getDisplayNameDelegate = this._getDisplayNameDelegate;
				return ((getDisplayNameDelegate != null) ? getDisplayNameDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000B7CC File Offset: 0x000099CC
		public override int UIVersion
		{
			get
			{
				BasePerCampaignSettingsWrapper.GetUIVersionDelegate getUIVersionDelegate = this._getUIVersionDelegate;
				return (getUIVersionDelegate != null) ? getUIVersionDelegate() : 1;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public override string SubFolder
		{
			get
			{
				BasePerCampaignSettingsWrapper.GetSubFolderDelegate getSubFolderDelegate = this._getSubFolderDelegate;
				return ((getSubFolderDelegate != null) ? getSubFolderDelegate() : null) ?? string.Empty;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000B7FD File Offset: 0x000099FD
		public override char SubGroupDelimiter
		{
			get
			{
				BasePerCampaignSettingsWrapper.GetSubGroupDelimiterDelegate getSubGroupDelimiterDelegate = this._getSubGroupDelimiterDelegate;
				return (getSubGroupDelimiterDelegate != null) ? getSubGroupDelimiterDelegate() : '/';
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060003C6 RID: 966 RVA: 0x0000B814 File Offset: 0x00009A14
		// (remove) Token: 0x060003C7 RID: 967 RVA: 0x0000B840 File Offset: 0x00009A40
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

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B86C File Offset: 0x00009A6C
		protected BasePerCampaignSettingsWrapper(object @object)
		{
			this.Object = @object;
			Type type = @object.GetType();
			this._getIdDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerCampaignSettingsWrapper.GetIdDelegate>(@object, type, "Id", true);
			this._getFolderNameDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerCampaignSettingsWrapper.GetFolderNameDelegate>(@object, type, "FolderName", true);
			this._getDisplayNameDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerCampaignSettingsWrapper.GetDisplayNameDelegate>(@object, type, "DisplayName", true);
			this._getUIVersionDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerCampaignSettingsWrapper.GetUIVersionDelegate>(@object, type, "UIVersion", true);
			this._getSubFolderDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerCampaignSettingsWrapper.GetSubFolderDelegate>(@object, type, "SubFolder", true);
			this._getSubGroupDelimiterDelegate = AccessTools2.GetPropertyGetterDelegate<BasePerCampaignSettingsWrapper.GetSubGroupDelimiterDelegate>(@object, type, "SubGroupDelimiter", true);
			this._methodOnPropertyChangedDelegate = AccessTools2.GetDelegate<BasePerCampaignSettingsWrapper.OnPropertyChangedDelegate>(@object, type, "OnPropertyChanged", null, null, true);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B917 File Offset: 0x00009B17
		[NullableContext(2)]
		public override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			BasePerCampaignSettingsWrapper.OnPropertyChangedDelegate methodOnPropertyChangedDelegate = this._methodOnPropertyChangedDelegate;
			if (methodOnPropertyChangedDelegate != null)
			{
				methodOnPropertyChangedDelegate(propertyName);
			}
		}

		// Token: 0x0400013B RID: 315
		[Nullable(2)]
		private readonly BasePerCampaignSettingsWrapper.GetIdDelegate _getIdDelegate;

		// Token: 0x0400013C RID: 316
		[Nullable(2)]
		private readonly BasePerCampaignSettingsWrapper.GetFolderNameDelegate _getFolderNameDelegate;

		// Token: 0x0400013D RID: 317
		[Nullable(2)]
		private readonly BasePerCampaignSettingsWrapper.GetDisplayNameDelegate _getDisplayNameDelegate;

		// Token: 0x0400013E RID: 318
		[Nullable(2)]
		private readonly BasePerCampaignSettingsWrapper.GetUIVersionDelegate _getUIVersionDelegate;

		// Token: 0x0400013F RID: 319
		[Nullable(2)]
		private readonly BasePerCampaignSettingsWrapper.GetSubFolderDelegate _getSubFolderDelegate;

		// Token: 0x04000140 RID: 320
		[Nullable(2)]
		private readonly BasePerCampaignSettingsWrapper.GetSubGroupDelimiterDelegate _getSubGroupDelimiterDelegate;

		// Token: 0x04000141 RID: 321
		[Nullable(2)]
		private readonly BasePerCampaignSettingsWrapper.OnPropertyChangedDelegate _methodOnPropertyChangedDelegate;

		// Token: 0x020001C7 RID: 455
		// (Invoke) Token: 0x06000BC8 RID: 3016
		[NullableContext(0)]
		private delegate string GetIdDelegate();

		// Token: 0x020001C8 RID: 456
		// (Invoke) Token: 0x06000BCC RID: 3020
		[NullableContext(0)]
		private delegate string GetFolderNameDelegate();

		// Token: 0x020001C9 RID: 457
		// (Invoke) Token: 0x06000BD0 RID: 3024
		[NullableContext(0)]
		private delegate string GetDisplayNameDelegate();

		// Token: 0x020001CA RID: 458
		// (Invoke) Token: 0x06000BD4 RID: 3028
		[NullableContext(0)]
		private delegate int GetUIVersionDelegate();

		// Token: 0x020001CB RID: 459
		// (Invoke) Token: 0x06000BD8 RID: 3032
		[NullableContext(0)]
		private delegate string GetSubFolderDelegate();

		// Token: 0x020001CC RID: 460
		// (Invoke) Token: 0x06000BDC RID: 3036
		[NullableContext(0)]
		private delegate char GetSubGroupDelimiterDelegate();

		// Token: 0x020001CD RID: 461
		// (Invoke) Token: 0x06000BE0 RID: 3040
		[NullableContext(0)]
		private delegate void OnPropertyChangedDelegate(string propertyName);
	}
}
