using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Common;

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000C3 RID: 195
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class SettingsWrapper : BaseSettings, IWrapper
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000C2E9 File Offset: 0x0000A4E9
		[Nullable(1)]
		public override string Id
		{
			[NullableContext(1)]
			get
			{
				SettingsWrapper.GetIdDelegate getIdDelegate = this._getIdDelegate;
				return ((getIdDelegate != null) ? getIdDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000C306 File Offset: 0x0000A506
		[Nullable(1)]
		public override string FolderName
		{
			[NullableContext(1)]
			get
			{
				SettingsWrapper.GetFolderNameDelegate getFolderNameDelegate = this._getFolderNameDelegate;
				return ((getFolderNameDelegate != null) ? getFolderNameDelegate() : null) ?? string.Empty;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000C323 File Offset: 0x0000A523
		[Nullable(1)]
		public override string DisplayName
		{
			[NullableContext(1)]
			get
			{
				SettingsWrapper.GetDisplayNameDelegate getDisplayNameDelegate = this._getDisplayNameDelegate;
				return ((getDisplayNameDelegate != null) ? getDisplayNameDelegate() : null) ?? "ERROR";
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000C340 File Offset: 0x0000A540
		public override int UIVersion
		{
			get
			{
				SettingsWrapper.GetUIVersionDelegate getUIVersionDelegate = this._getUIVersionDelegate;
				return (getUIVersionDelegate != null) ? getUIVersionDelegate() : 1;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000C354 File Offset: 0x0000A554
		[Nullable(1)]
		public override string SubFolder
		{
			[NullableContext(1)]
			get
			{
				SettingsWrapper.GetSubFolderDelegate getSubFolderDelegate = this._getSubFolderDelegate;
				return ((getSubFolderDelegate != null) ? getSubFolderDelegate() : null) ?? string.Empty;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000C371 File Offset: 0x0000A571
		public override char SubGroupDelimiter
		{
			get
			{
				SettingsWrapper.GetSubGroupDelimiterDelegate getSubGroupDelimiterDelegate = this._getSubGroupDelimiterDelegate;
				return (getSubGroupDelimiterDelegate != null) ? getSubGroupDelimiterDelegate() : '/';
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000C386 File Offset: 0x0000A586
		[Nullable(1)]
		public override string FormatType
		{
			[NullableContext(1)]
			get
			{
				SettingsWrapper.GetFormatDelegate getFormatDelegate = this._getFormatDelegate;
				return ((getFormatDelegate != null) ? getFormatDelegate() : null) ?? "none";
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000414 RID: 1044 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
		// (remove) Token: 0x06000415 RID: 1045 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		public override event PropertyChangedEventHandler PropertyChanged
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000C3FA File Offset: 0x0000A5FA
		public object Object { get; }

		// Token: 0x06000417 RID: 1047 RVA: 0x0000C404 File Offset: 0x0000A604
		protected SettingsWrapper(object @object)
		{
			bool flag = @object == null;
			if (!flag)
			{
				this.Object = @object;
				Type type = @object.GetType();
				this._getIdDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsWrapper.GetIdDelegate>(@object, type, "Id", true);
				this._getFolderNameDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsWrapper.GetFolderNameDelegate>(@object, type, "FolderName", true);
				this._getDisplayNameDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsWrapper.GetDisplayNameDelegate>(@object, type, "DisplayName", true);
				this._getUIVersionDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsWrapper.GetUIVersionDelegate>(@object, type, "UIVersion", true);
				this._getSubFolderDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsWrapper.GetSubFolderDelegate>(@object, type, "SubFolder", true);
				this._getSubGroupDelimiterDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsWrapper.GetSubGroupDelimiterDelegate>(@object, type, "SubGroupDelimiter", true);
				this._getFormatDelegate = AccessTools2.GetPropertyGetterDelegate<SettingsWrapper.GetFormatDelegate>(@object, type, "FormatType", true);
				this._methodOnPropertyChangedDelegate = AccessTools2.GetDelegate<SettingsWrapper.OnPropertyChangedDelegate>(@object, type, "OnPropertyChanged", null, null, true);
				this._methodGetBuiltInPresetsDelegate = AccessTools2.GetDelegate<SettingsWrapper.GetBuiltInPresetsDelegate>(@object, type, "GetBuiltInPresets", null, null, true);
				this._methodCreateNewDelegate = AccessTools2.GetDelegate<SettingsWrapper.CreateNewDelegate>(@object, type, "CreateNew", null, null, true);
				this._methodCopyAsNewDelegate = AccessTools2.GetDelegate<SettingsWrapper.CopyAsNewDelegate>(@object, type, "CopyAsNew", null, null, true);
			}
		}

		// Token: 0x06000418 RID: 1048
		[NullableContext(1)]
		protected abstract BaseSettings Create([Nullable(2)] object @object);

		// Token: 0x06000419 RID: 1049
		[NullableContext(1)]
		protected abstract ISettingsPreset CreatePreset([Nullable(2)] object @object);

		// Token: 0x0600041A RID: 1050 RVA: 0x0000C50D File Offset: 0x0000A70D
		public override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			SettingsWrapper.OnPropertyChangedDelegate methodOnPropertyChangedDelegate = this._methodOnPropertyChangedDelegate;
			if (methodOnPropertyChangedDelegate != null)
			{
				methodOnPropertyChangedDelegate(propertyName);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000C522 File Offset: 0x0000A722
		[NullableContext(1)]
		public override IEnumerable<ISettingsPreset> GetBuiltInPresets()
		{
			SettingsWrapper.GetBuiltInPresetsDelegate methodGetBuiltInPresetsDelegate = this._methodGetBuiltInPresetsDelegate;
			IEnumerable<ISettingsPreset> enumerable;
			if (methodGetBuiltInPresetsDelegate == null)
			{
				enumerable = null;
			}
			else
			{
				IEnumerable enumerable2 = methodGetBuiltInPresetsDelegate();
				enumerable = ((enumerable2 != null) ? enumerable2.Cast<object>().Select(new Func<object, ISettingsPreset>(this.CreatePreset)).OfType<ISettingsPreset>() : null);
			}
			return enumerable ?? Enumerable.Empty<ISettingsPreset>();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000C562 File Offset: 0x0000A762
		[NullableContext(1)]
		public override BaseSettings CreateNew()
		{
			SettingsWrapper.CreateNewDelegate methodCreateNewDelegate = this._methodCreateNewDelegate;
			return this.Create((methodCreateNewDelegate != null) ? methodCreateNewDelegate() : null);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000C57C File Offset: 0x0000A77C
		[NullableContext(1)]
		public override BaseSettings CopyAsNew()
		{
			SettingsWrapper.CopyAsNewDelegate methodCopyAsNewDelegate = this._methodCopyAsNewDelegate;
			return this.Create((methodCopyAsNewDelegate != null) ? methodCopyAsNewDelegate() : null);
		}

		// Token: 0x0400015C RID: 348
		private readonly SettingsWrapper.GetIdDelegate _getIdDelegate;

		// Token: 0x0400015D RID: 349
		private readonly SettingsWrapper.GetFolderNameDelegate _getFolderNameDelegate;

		// Token: 0x0400015E RID: 350
		private readonly SettingsWrapper.GetDisplayNameDelegate _getDisplayNameDelegate;

		// Token: 0x0400015F RID: 351
		private readonly SettingsWrapper.GetUIVersionDelegate _getUIVersionDelegate;

		// Token: 0x04000160 RID: 352
		private readonly SettingsWrapper.GetSubFolderDelegate _getSubFolderDelegate;

		// Token: 0x04000161 RID: 353
		private readonly SettingsWrapper.GetSubGroupDelimiterDelegate _getSubGroupDelimiterDelegate;

		// Token: 0x04000162 RID: 354
		private readonly SettingsWrapper.GetFormatDelegate _getFormatDelegate;

		// Token: 0x04000163 RID: 355
		private readonly SettingsWrapper.OnPropertyChangedDelegate _methodOnPropertyChangedDelegate;

		// Token: 0x04000164 RID: 356
		private readonly SettingsWrapper.GetBuiltInPresetsDelegate _methodGetBuiltInPresetsDelegate;

		// Token: 0x04000165 RID: 357
		private readonly SettingsWrapper.CreateNewDelegate _methodCreateNewDelegate;

		// Token: 0x04000166 RID: 358
		private readonly SettingsWrapper.CopyAsNewDelegate _methodCopyAsNewDelegate;

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x06000BF4 RID: 3060
		[NullableContext(0)]
		private delegate string GetIdDelegate();

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x06000BF8 RID: 3064
		[NullableContext(0)]
		private delegate string GetFolderNameDelegate();

		// Token: 0x020001D6 RID: 470
		// (Invoke) Token: 0x06000BFC RID: 3068
		[NullableContext(0)]
		private delegate string GetDisplayNameDelegate();

		// Token: 0x020001D7 RID: 471
		// (Invoke) Token: 0x06000C00 RID: 3072
		[NullableContext(0)]
		private delegate int GetUIVersionDelegate();

		// Token: 0x020001D8 RID: 472
		// (Invoke) Token: 0x06000C04 RID: 3076
		[NullableContext(0)]
		private delegate string GetSubFolderDelegate();

		// Token: 0x020001D9 RID: 473
		// (Invoke) Token: 0x06000C08 RID: 3080
		[NullableContext(0)]
		private delegate char GetSubGroupDelimiterDelegate();

		// Token: 0x020001DA RID: 474
		// (Invoke) Token: 0x06000C0C RID: 3084
		[NullableContext(0)]
		private delegate string GetFormatDelegate();

		// Token: 0x020001DB RID: 475
		// (Invoke) Token: 0x06000C10 RID: 3088
		[NullableContext(0)]
		private delegate void OnPropertyChangedDelegate(string propertyName);

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06000C14 RID: 3092
		[NullableContext(0)]
		private delegate IEnumerable GetBuiltInPresetsDelegate();

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06000C18 RID: 3096
		[NullableContext(0)]
		private delegate object CreateNewDelegate();

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x06000C1C RID: 3100
		[NullableContext(0)]
		private delegate object CopyAsNewDelegate();
	}
}
