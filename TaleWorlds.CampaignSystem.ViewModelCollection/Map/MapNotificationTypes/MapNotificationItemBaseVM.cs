using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000040 RID: 64
	public class MapNotificationItemBaseVM : ViewModel
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001BC1F File Offset: 0x00019E1F
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0001BC27 File Offset: 0x00019E27
		public INavigationHandler NavigationHandler { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001BC30 File Offset: 0x00019E30
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0001BC38 File Offset: 0x00019E38
		private protected Action<Vec2> FastMoveCameraToPosition { protected get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001BC41 File Offset: 0x00019E41
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0001BC49 File Offset: 0x00019E49
		public InformationData Data { get; private set; }

		// Token: 0x0600057C RID: 1404 RVA: 0x0001BC54 File Offset: 0x00019E54
		public MapNotificationItemBaseVM(InformationData data)
		{
			this.Data = data;
			this.ForceInspection = false;
			this.SoundId = data.SoundEventPath;
			this.RefreshValues();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = this.Data.TitleText.ToString();
			this.DescriptionText = this.Data.DescriptionText.ToString();
			this._removeHintText = this._removeHintTextObject.ToString();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001BCF4 File Offset: 0x00019EF4
		public void SetNavigationHandler(INavigationHandler navigationHandler)
		{
			this.NavigationHandler = navigationHandler;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001BCFD File Offset: 0x00019EFD
		public void SetFastMoveCameraToPosition(Action<Vec2> fastMoveCameraToPosition)
		{
			this.FastMoveCameraToPosition = fastMoveCameraToPosition;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001BD06 File Offset: 0x00019F06
		public void ExecuteAction()
		{
			Action onInspect = this._onInspect;
			if (onInspect == null)
			{
				return;
			}
			onInspect();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001BD18 File Offset: 0x00019F18
		public void ExecuteRemove()
		{
			Action<MapNotificationItemBaseVM> onRemove = this.OnRemove;
			if (onRemove != null)
			{
				onRemove(this);
			}
			Action<MapNotificationItemBaseVM> onFocus = this.OnFocus;
			if (onFocus == null)
			{
				return;
			}
			onFocus(null);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001BD3D File Offset: 0x00019F3D
		public void ExecuteSetFocused()
		{
			this.IsFocused = true;
			Action<MapNotificationItemBaseVM> onFocus = this.OnFocus;
			if (onFocus == null)
			{
				return;
			}
			onFocus(this);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001BD57 File Offset: 0x00019F57
		public void ExecuteSetUnfocused()
		{
			this.IsFocused = false;
			Action<MapNotificationItemBaseVM> onFocus = this.OnFocus;
			if (onFocus == null)
			{
				return;
			}
			onFocus(null);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001BD71 File Offset: 0x00019F71
		public virtual void ManualRefreshRelevantStatus()
		{
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001BD73 File Offset: 0x00019F73
		internal void GoToMapPosition(Vec2 position)
		{
			Action<Vec2> fastMoveCameraToPosition = this.FastMoveCameraToPosition;
			if (fastMoveCameraToPosition == null)
			{
				return;
			}
			fastMoveCameraToPosition(position);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001BD86 File Offset: 0x00019F86
		public void SetRemoveInputKey(HotKey hotKey)
		{
			this.RemoveInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001BD95 File Offset: 0x00019F95
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0001BD9D File Offset: 0x00019F9D
		[DataSourceProperty]
		public InputKeyItemVM RemoveInputKey
		{
			get
			{
				return this._removeInputKey;
			}
			set
			{
				if (value != this._removeInputKey)
				{
					this._removeInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "RemoveInputKey");
				}
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001BDBB File Offset: 0x00019FBB
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0001BDC3 File Offset: 0x00019FC3
		[DataSourceProperty]
		public bool IsFocused
		{
			get
			{
				return this._isFocused;
			}
			set
			{
				if (value != this._isFocused)
				{
					this._isFocused = value;
					base.OnPropertyChangedWithValue(value, "IsFocused");
					Action<MapNotificationItemBaseVM> onFocus = this.OnFocus;
					if (onFocus == null)
					{
						return;
					}
					onFocus(this);
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001BDF2 File Offset: 0x00019FF2
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001BDFA File Offset: 0x00019FFA
		[DataSourceProperty]
		public string NotificationIdentifier
		{
			get
			{
				return this._notificationIdentifier;
			}
			set
			{
				if (value != this._notificationIdentifier)
				{
					this._notificationIdentifier = value;
					base.OnPropertyChangedWithValue<string>(value, "NotificationIdentifier");
				}
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001BE1D File Offset: 0x0001A01D
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001BE25 File Offset: 0x0001A025
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001BE48 File Offset: 0x0001A048
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001BE50 File Offset: 0x0001A050
		[DataSourceProperty]
		public bool ForceInspection
		{
			get
			{
				return this._forceInspection;
			}
			set
			{
				if (value != this._forceInspection)
				{
					Game game = Game.Current;
					if (game != null && !game.IsDevelopmentMode)
					{
						this._forceInspection = value;
						base.OnPropertyChangedWithValue(value, "ForceInspection");
					}
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001BE84 File Offset: 0x0001A084
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001BE8C File Offset: 0x0001A08C
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (value != this._descriptionText)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001BEAF File Offset: 0x0001A0AF
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0001BEB7 File Offset: 0x0001A0B7
		[DataSourceProperty]
		public string SoundId
		{
			get
			{
				return this._soundId;
			}
			set
			{
				if (value != this._soundId)
				{
					this._soundId = value;
					base.OnPropertyChangedWithValue<string>(value, "SoundId");
				}
			}
		}

		// Token: 0x0400024F RID: 591
		internal Action<MapNotificationItemBaseVM> OnRemove;

		// Token: 0x04000250 RID: 592
		internal Action<MapNotificationItemBaseVM> OnFocus;

		// Token: 0x04000251 RID: 593
		protected Action _onInspect;

		// Token: 0x04000253 RID: 595
		private readonly TextObject _removeHintTextObject = new TextObject("{=Bcs9s2tC}Right Click to Remove", null);

		// Token: 0x04000254 RID: 596
		private string _removeHintText;

		// Token: 0x04000255 RID: 597
		private InputKeyItemVM _removeInputKey;

		// Token: 0x04000256 RID: 598
		private bool _isFocused;

		// Token: 0x04000257 RID: 599
		private string _titleText;

		// Token: 0x04000258 RID: 600
		private string _descriptionText;

		// Token: 0x04000259 RID: 601
		private string _soundId;

		// Token: 0x0400025A RID: 602
		private bool _forceInspection;

		// Token: 0x0400025B RID: 603
		private string _notificationIdentifier = "Default";
	}
}
