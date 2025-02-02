using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000028 RID: 40
	public class ItemTableauWidget : TextureWidget
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00007A78 File Offset: 0x00005C78
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00007A80 File Offset: 0x00005C80
		[Editor(false)]
		public string ItemModifierId
		{
			get
			{
				return this._itemModifierId;
			}
			set
			{
				if (value != this._itemModifierId)
				{
					this._itemModifierId = value;
					base.OnPropertyChanged<string>(value, "ItemModifierId");
					base.SetTextureProviderProperty("ItemModifierId", value);
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00007AAF File Offset: 0x00005CAF
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00007AB7 File Offset: 0x00005CB7
		[Editor(false)]
		public string StringId
		{
			get
			{
				return this._stringId;
			}
			set
			{
				if (value != this._stringId)
				{
					this._stringId = value;
					base.OnPropertyChanged<string>(value, "StringId");
					if (value != null)
					{
						base.SetTextureProviderProperty("StringId", value);
					}
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00007AE9 File Offset: 0x00005CE9
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00007AF1 File Offset: 0x00005CF1
		[Editor(false)]
		public float InitialTiltRotation
		{
			get
			{
				return this._initialTiltRotation;
			}
			set
			{
				if (value != this._initialTiltRotation)
				{
					this._initialTiltRotation = value;
					base.OnPropertyChanged(value, "InitialTiltRotation");
					base.SetTextureProviderProperty("InitialTiltRotation", value);
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00007B20 File Offset: 0x00005D20
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00007B28 File Offset: 0x00005D28
		[Editor(false)]
		public float InitialPanRotation
		{
			get
			{
				return this._initialPanRotation;
			}
			set
			{
				if (value != this._initialPanRotation)
				{
					this._initialPanRotation = value;
					base.OnPropertyChanged(value, "InitialPanRotation");
					base.SetTextureProviderProperty("InitialPanRotation", value);
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00007B57 File Offset: 0x00005D57
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00007B5F File Offset: 0x00005D5F
		[Editor(false)]
		public string BannerCode
		{
			get
			{
				return this._bannerCode;
			}
			set
			{
				if (value != this._bannerCode)
				{
					this._bannerCode = value;
					base.OnPropertyChanged<string>(value, "BannerCode");
					base.SetTextureProviderProperty("BannerCode", value);
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007B8E File Offset: 0x00005D8E
		public ItemTableauWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "ItemTableauTextureProvider";
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007BA2 File Offset: 0x00005DA2
		protected override void OnMousePressed()
		{
			base.SetTextureProviderProperty("CurrentlyRotating", true);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007BB5 File Offset: 0x00005DB5
		protected override void OnRightStickMovement()
		{
			base.OnRightStickMovement();
			base.SetTextureProviderProperty("RotateItemVertical", base.EventManager.RightStickVerticalScrollAmount);
			base.SetTextureProviderProperty("RotateItemHorizontal", base.EventManager.RightStickHorizontalScrollAmount);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007BF3 File Offset: 0x00005DF3
		protected override void OnMouseReleased()
		{
			base.SetTextureProviderProperty("CurrentlyRotating", false);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007C06 File Offset: 0x00005E06
		protected override bool OnPreviewRightStickMovement()
		{
			return true;
		}

		// Token: 0x040000FA RID: 250
		private string _itemModifierId;

		// Token: 0x040000FB RID: 251
		private string _stringId;

		// Token: 0x040000FC RID: 252
		private float _initialTiltRotation;

		// Token: 0x040000FD RID: 253
		private float _initialPanRotation;

		// Token: 0x040000FE RID: 254
		private string _bannerCode;
	}
}
