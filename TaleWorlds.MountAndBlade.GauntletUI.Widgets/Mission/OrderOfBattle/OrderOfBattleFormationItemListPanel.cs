using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E3 RID: 227
	public class OrderOfBattleFormationItemListPanel : ListPanel
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x000207D9 File Offset: 0x0001E9D9
		public OrderOfBattleFormationItemListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000207E4 File Offset: 0x0001E9E4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			Widget latestMouseUpWidget = base.EventManager.LatestMouseUpWidget;
			if (this.IsFilterDropdownEnabled && !base.CheckIsMyChildRecursive(latestMouseUpWidget))
			{
				this.IsFilterDropdownEnabled = false;
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002081C File Offset: 0x0001EA1C
		private void OnStateChanged()
		{
			if (this.IsSelected)
			{
				Widget cardWidget = this.CardWidget;
				if (cardWidget == null)
				{
					return;
				}
				cardWidget.SetState("Selected");
				return;
			}
			else
			{
				Widget cardWidget2 = this.CardWidget;
				if (cardWidget2 == null)
				{
					return;
				}
				cardWidget2.SetState("Default");
				return;
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00020851 File Offset: 0x0001EA51
		private void OnClassDropdownEnabledStateChanged(DropdownWidget widget)
		{
			this.IsClassDropdownEnabled = widget.IsOpen;
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0002085F File Offset: 0x0001EA5F
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x00020867 File Offset: 0x0001EA67
		[Editor(false)]
		public Widget CardWidget
		{
			get
			{
				return this._cardWidget;
			}
			set
			{
				if (value != this._cardWidget)
				{
					this._cardWidget = value;
					base.OnPropertyChanged<Widget>(value, "CardWidget");
				}
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00020885 File Offset: 0x0001EA85
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00020890 File Offset: 0x0001EA90
		[Editor(false)]
		public DropdownWidget FormationClassDropdown
		{
			get
			{
				return this._formationClassDropdown;
			}
			set
			{
				if (value != this._formationClassDropdown)
				{
					if (this._formationClassDropdown != null)
					{
						DropdownWidget formationClassDropdown = this._formationClassDropdown;
						formationClassDropdown.OnOpenStateChanged = (Action<DropdownWidget>)Delegate.Remove(formationClassDropdown.OnOpenStateChanged, new Action<DropdownWidget>(this.OnClassDropdownEnabledStateChanged));
					}
					this._formationClassDropdown = value;
					base.OnPropertyChanged<DropdownWidget>(value, "FormationClassDropdown");
					if (this._formationClassDropdown != null)
					{
						DropdownWidget formationClassDropdown2 = this._formationClassDropdown;
						formationClassDropdown2.OnOpenStateChanged = (Action<DropdownWidget>)Delegate.Combine(formationClassDropdown2.OnOpenStateChanged, new Action<DropdownWidget>(this.OnClassDropdownEnabledStateChanged));
						this.OnClassDropdownEnabledStateChanged(this._formationClassDropdown);
					}
				}
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00020923 File Offset: 0x0001EB23
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0002092C File Offset: 0x0001EB2C
		[Editor(false)]
		public bool IsControlledByPlayer
		{
			get
			{
				return this._isControlledByPlayer;
			}
			set
			{
				if (value != this._isControlledByPlayer)
				{
					this._isControlledByPlayer = value;
					base.OnPropertyChanged(value, "IsControlledByPlayer");
					DropdownWidget formationClassDropdown = this.FormationClassDropdown;
					if (((formationClassDropdown != null) ? formationClassDropdown.Button : null) != null)
					{
						this.FormationClassDropdown.Button.IsEnabled = value;
					}
				}
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0002097A File Offset: 0x0001EB7A
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x00020982 File Offset: 0x0001EB82
		[Editor(false)]
		public bool IsFilterDropdownEnabled
		{
			get
			{
				return this._isFilterDropdownEnabled;
			}
			set
			{
				if (value != this._isFilterDropdownEnabled)
				{
					this._isFilterDropdownEnabled = value;
					base.OnPropertyChanged(value, "IsFilterDropdownEnabled");
				}
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000209A0 File Offset: 0x0001EBA0
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x000209A8 File Offset: 0x0001EBA8
		[Editor(false)]
		public bool IsClassDropdownEnabled
		{
			get
			{
				return this._isClassDropdownEnabled;
			}
			set
			{
				if (value != this._isClassDropdownEnabled)
				{
					this._isClassDropdownEnabled = value;
					base.OnPropertyChanged(value, "IsClassDropdownEnabled");
					if (this.FormationClassDropdown != null)
					{
						this.FormationClassDropdown.IsOpen = value;
					}
				}
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x000209DA File Offset: 0x0001EBDA
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x000209E2 File Offset: 0x0001EBE2
		[Editor(false)]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChanged(value, "IsSelected");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00020A06 File Offset: 0x0001EC06
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00020A0E File Offset: 0x0001EC0E
		[Editor(false)]
		public bool HasFormation
		{
			get
			{
				return this._hasFormation;
			}
			set
			{
				if (value != this._hasFormation)
				{
					this._hasFormation = value;
					base.OnPropertyChanged(value, "HasFormation");
				}
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00020A2C File Offset: 0x0001EC2C
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00020A34 File Offset: 0x0001EC34
		[Editor(false)]
		public float DefaultFocusYOffsetFromCenter
		{
			get
			{
				return this._defaultFocusYOffsetFromCenter;
			}
			set
			{
				if (value != this._defaultFocusYOffsetFromCenter)
				{
					this._defaultFocusYOffsetFromCenter = value;
					base.OnPropertyChanged(value, "DefaultFocusYOffsetFromCenter");
				}
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00020A52 File Offset: 0x0001EC52
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00020A5A File Offset: 0x0001EC5A
		[Editor(false)]
		public float NoFormationFocusYOffsetFromCenter
		{
			get
			{
				return this._noFormationFocusYOffsetFromCenter;
			}
			set
			{
				if (value != this._noFormationFocusYOffsetFromCenter)
				{
					this._noFormationFocusYOffsetFromCenter = value;
					base.OnPropertyChanged(value, "NoFormationFocusYOffsetFromCenter");
				}
			}
		}

		// Token: 0x0400055B RID: 1371
		private Widget _cardWidget;

		// Token: 0x0400055C RID: 1372
		private DropdownWidget _formationClassDropdown;

		// Token: 0x0400055D RID: 1373
		private bool _isControlledByPlayer;

		// Token: 0x0400055E RID: 1374
		private bool _isFilterDropdownEnabled;

		// Token: 0x0400055F RID: 1375
		private bool _isClassDropdownEnabled;

		// Token: 0x04000560 RID: 1376
		private bool _isSelected;

		// Token: 0x04000561 RID: 1377
		private bool _hasFormation;

		// Token: 0x04000562 RID: 1378
		private float _defaultFocusYOffsetFromCenter;

		// Token: 0x04000563 RID: 1379
		private float _noFormationFocusYOffsetFromCenter;
	}
}
