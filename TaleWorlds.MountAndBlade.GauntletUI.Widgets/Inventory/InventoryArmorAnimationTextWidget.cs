using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x0200012A RID: 298
	public class InventoryArmorAnimationTextWidget : TextWidget
	{
		// Token: 0x06000F67 RID: 3943 RVA: 0x0002A736 File Offset: 0x00028936
		public InventoryArmorAnimationTextWidget(UIContext context) : base(context)
		{
			base.FloatText = 0f;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002A74A File Offset: 0x0002894A
		private void HandleAnimation(float oldValue, float newValue)
		{
			if (oldValue > newValue)
			{
				this.SetState("Decrease");
				return;
			}
			if (oldValue < newValue)
			{
				this.SetState("Increase");
				return;
			}
			this.SetState("Default");
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0002A777 File Offset: 0x00028977
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x0002A77F File Offset: 0x0002897F
		[Editor(false)]
		public float FloatAmount
		{
			get
			{
				return this._floatAmount;
			}
			set
			{
				if (this._floatAmount != value)
				{
					this.HandleAnimation(this._floatAmount, value);
					this._floatAmount = value;
					base.FloatText = this._floatAmount;
					base.OnPropertyChanged(value, "FloatAmount");
				}
			}
		}

		// Token: 0x0400070C RID: 1804
		private float _floatAmount;
	}
}
