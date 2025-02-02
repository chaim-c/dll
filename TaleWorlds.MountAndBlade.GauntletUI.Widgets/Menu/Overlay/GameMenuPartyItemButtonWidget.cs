using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.Overlay
{
	// Token: 0x02000102 RID: 258
	public class GameMenuPartyItemButtonWidget : ButtonWidget
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00025DC7 File Offset: 0x00023FC7
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x00025DCF File Offset: 0x00023FCF
		public Brush PartyBackgroundBrush { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00025DD8 File Offset: 0x00023FD8
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x00025DE0 File Offset: 0x00023FE0
		public Brush CharacterBackgroundBrush { get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00025DE9 File Offset: 0x00023FE9
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x00025DF1 File Offset: 0x00023FF1
		public ImageWidget BackgroundImageWidget { get; set; }

		// Token: 0x06000D9E RID: 3486 RVA: 0x00025DFC File Offset: 0x00023FFC
		public GameMenuPartyItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00025E55 File Offset: 0x00024055
		private string GetRelationBackgroundName(int relation)
		{
			return "";
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00025E5C File Offset: 0x0002405C
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._popupWidget == null)
			{
				Widget widget = this;
				while (widget != base.EventManager.Root && this._popupWidget == null && this._parentKnowsPopup)
				{
					if (widget is OverlayBaseWidget)
					{
						OverlayBaseWidget overlayBaseWidget = (OverlayBaseWidget)widget;
						if (overlayBaseWidget.PopupWidget == null)
						{
							this._parentKnowsPopup = false;
							break;
						}
						this._popupWidget = overlayBaseWidget.PopupWidget;
					}
					else
					{
						widget = widget.ParentWidget;
					}
				}
			}
			if (this.CurrentCharacterImageWidget != null)
			{
				this.CurrentCharacterImageWidget.Brush.SaturationFactor = (float)(this.IsMergedWithArmy ? 0 : -100);
				this.CurrentCharacterImageWidget.Brush.ValueFactor = (float)(this.IsMergedWithArmy ? 0 : -20);
			}
			if (!this._initialized)
			{
				this.BackgroundImageWidget.Brush = (this.IsPartyItem ? this.PartyBackgroundBrush : this.CharacterBackgroundBrush);
				this._initialized = true;
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00025F43 File Offset: 0x00024143
		protected override void OnClick()
		{
			base.OnClick();
			if (this._popupWidget != null)
			{
				this._popupWidget.SetCurrentCharacter(this);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00025F5F File Offset: 0x0002415F
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x00025F67 File Offset: 0x00024167
		[Editor(false)]
		public int Relation
		{
			get
			{
				return this._relation;
			}
			set
			{
				if (this._relation != value)
				{
					this._relation = value;
					base.OnPropertyChanged(value, "Relation");
				}
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00025F85 File Offset: 0x00024185
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x00025F8D File Offset: 0x0002418D
		[Editor(false)]
		public string Location
		{
			get
			{
				return this._location;
			}
			set
			{
				if (this._location != value)
				{
					this._location = value;
					base.OnPropertyChanged<string>(value, "Location");
				}
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00025FB0 File Offset: 0x000241B0
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x00025FB8 File Offset: 0x000241B8
		[Editor(false)]
		public string Power
		{
			get
			{
				return this._power;
			}
			set
			{
				if (this._power != value)
				{
					this._power = value;
					base.OnPropertyChanged<string>(value, "Power");
				}
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00025FDB File Offset: 0x000241DB
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x00025FE3 File Offset: 0x000241E3
		[Editor(false)]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (this._description != value)
				{
					this._description = value;
					base.OnPropertyChanged<string>(value, "Description");
				}
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00026006 File Offset: 0x00024206
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x0002600E File Offset: 0x0002420E
		[Editor(false)]
		public string Profession
		{
			get
			{
				return this._profession;
			}
			set
			{
				if (this._profession != value)
				{
					this._profession = value;
					base.OnPropertyChanged<string>(value, "Profession");
				}
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00026031 File Offset: 0x00024231
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x00026039 File Offset: 0x00024239
		[Editor(false)]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (this._name != value)
				{
					this._name = value;
					base.OnPropertyChanged<string>(value, "Name");
				}
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0002605C File Offset: 0x0002425C
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x00026064 File Offset: 0x00024264
		[Editor(false)]
		public bool IsMergedWithArmy
		{
			get
			{
				return this._isMergedWithArmy;
			}
			set
			{
				if (this._isMergedWithArmy != value)
				{
					this._isMergedWithArmy = value;
				}
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00026076 File Offset: 0x00024276
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0002607E File Offset: 0x0002427E
		[Editor(false)]
		public bool IsPartyItem
		{
			get
			{
				return this._isPartyItem;
			}
			set
			{
				if (this._isPartyItem != value)
				{
					this._isPartyItem = value;
				}
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00026090 File Offset: 0x00024290
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00026098 File Offset: 0x00024298
		[Editor(false)]
		public Widget ContextMenu
		{
			get
			{
				return this._contextMenu;
			}
			set
			{
				if (this._contextMenu != value)
				{
					this._contextMenu = value;
					base.OnPropertyChanged<Widget>(value, "ContextMenu");
				}
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x000260B6 File Offset: 0x000242B6
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x000260BE File Offset: 0x000242BE
		[Editor(false)]
		public ImageIdentifierWidget CurrentCharacterImageWidget
		{
			get
			{
				return this._currentCharacterImageWidget;
			}
			set
			{
				if (this._currentCharacterImageWidget != value)
				{
					this._currentCharacterImageWidget = value;
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "CurrentCharacterImageWidget");
				}
			}
		}

		// Token: 0x04000642 RID: 1602
		private bool _initialized;

		// Token: 0x04000643 RID: 1603
		private int _relation;

		// Token: 0x04000644 RID: 1604
		private string _location = "";

		// Token: 0x04000645 RID: 1605
		private string _description = "";

		// Token: 0x04000646 RID: 1606
		private string _profession = "";

		// Token: 0x04000647 RID: 1607
		private string _power = "";

		// Token: 0x04000648 RID: 1608
		private string _name = "";

		// Token: 0x04000649 RID: 1609
		private Widget _contextMenu;

		// Token: 0x0400064A RID: 1610
		private ImageIdentifierWidget _currentCharacterImageWidget;

		// Token: 0x0400064B RID: 1611
		private OverlayPopupWidget _popupWidget;

		// Token: 0x0400064C RID: 1612
		private bool _parentKnowsPopup = true;

		// Token: 0x0400064D RID: 1613
		private bool _isMergedWithArmy = true;

		// Token: 0x0400064E RID: 1614
		private bool _isPartyItem;
	}
}
