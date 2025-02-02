using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x0200012E RID: 302
	public class InventoryEquippedItemSlotWidget : InventoryItemButtonWidget
	{
		// Token: 0x06000F98 RID: 3992 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public InventoryEquippedItemSlotWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0002AFDC File Offset: 0x000291DC
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (base.ScreenWidget == null || this.Background == null)
			{
				return;
			}
			bool flag = base.ScreenWidget.TargetEquipmentIndex == this.TargetEquipmentIndex;
			bool flag2 = this.TargetEquipmentIndex == 0 && base.ScreenWidget.TargetEquipmentIndex >= 0 && base.ScreenWidget.TargetEquipmentIndex <= 3;
			if (flag || flag2)
			{
				this.Background.SetState("Selected");
				return;
			}
			this.Background.SetState("Default");
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0002B064 File Offset: 0x00029264
		private void ProcessSelectItem()
		{
			if (base.ScreenWidget != null)
			{
				base.IsSelected = true;
				this.SetState("Selected");
				base.ScreenWidget.SetCurrentTuple(this, true);
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002B08D File Offset: 0x0002928D
		protected override void OnMouseReleased()
		{
			base.OnMouseReleased();
			this.ProcessSelectItem();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0002B09B File Offset: 0x0002929B
		private void ImageIdentifierOnPropertyChanged(PropertyOwnerObject owner, string propertyName, object value)
		{
			if (propertyName == "ImageId")
			{
				base.IsHidden = string.IsNullOrEmpty((string)value);
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0002B0BB File Offset: 0x000292BB
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x0002B0C4 File Offset: 0x000292C4
		[Editor(false)]
		public ImageIdentifierWidget ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (this._imageIdentifier != value)
				{
					if (this._imageIdentifier != null)
					{
						this._imageIdentifier.PropertyChanged -= this.ImageIdentifierOnPropertyChanged;
					}
					this._imageIdentifier = value;
					if (this._imageIdentifier != null)
					{
						this._imageIdentifier.PropertyChanged += this.ImageIdentifierOnPropertyChanged;
					}
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0002B12B File Offset: 0x0002932B
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x0002B133 File Offset: 0x00029333
		[Editor(false)]
		public Widget Background
		{
			get
			{
				return this._background;
			}
			set
			{
				if (this._background != value)
				{
					this._background = value;
					this._background.AddState("Selected");
					base.OnPropertyChanged<Widget>(value, "Background");
				}
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002B161 File Offset: 0x00029361
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0002B169 File Offset: 0x00029369
		[Editor(false)]
		public int TargetEquipmentIndex
		{
			get
			{
				return this._targetEquipmentIndex;
			}
			set
			{
				if (this._targetEquipmentIndex != value)
				{
					this._targetEquipmentIndex = value;
					base.OnPropertyChanged(value, "TargetEquipmentIndex");
				}
			}
		}

		// Token: 0x04000720 RID: 1824
		private ImageIdentifierWidget _imageIdentifier;

		// Token: 0x04000721 RID: 1825
		private Widget _background;

		// Token: 0x04000722 RID: 1826
		private int _targetEquipmentIndex;
	}
}
