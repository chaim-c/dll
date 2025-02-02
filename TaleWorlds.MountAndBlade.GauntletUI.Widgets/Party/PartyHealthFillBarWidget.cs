using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x0200005E RID: 94
	public class PartyHealthFillBarWidget : FillBar
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		public PartyHealthFillBarWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000F33C File Offset: 0x0000D53C
		private void HealthUpdated()
		{
			if (this.brushLayer == null)
			{
				this.brushLayer = base.Brush.GetLayer("DefaultFill");
			}
			base.CurrentAmount = (base.InitialAmount = this.Health);
			if (this.IsWounded)
			{
				this.brushLayer.Color = this.WoundedColor;
			}
			else if (this.Health >= this.FullHealthyLimit)
			{
				this.brushLayer.Color = this.FullHealthyColor;
			}
			else
			{
				this.brushLayer.Color = this.HealthyColor;
			}
			if (this.HealthText != null)
			{
				this.HealthText.Text = this.Health + "%";
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000F3F1 File Offset: 0x0000D5F1
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000F3F9 File Offset: 0x0000D5F9
		[Editor(false)]
		public int Health
		{
			get
			{
				return this._health;
			}
			set
			{
				if (this._health != value)
				{
					this._health = value;
					base.OnPropertyChanged(value, "Health");
					this.HealthUpdated();
				}
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000F41D File Offset: 0x0000D61D
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000F425 File Offset: 0x0000D625
		[Editor(false)]
		public bool IsWounded
		{
			get
			{
				return this._isWounded;
			}
			set
			{
				if (this._isWounded != value)
				{
					this._isWounded = value;
					base.OnPropertyChanged(value, "IsWounded");
					this.HealthUpdated();
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000F449 File Offset: 0x0000D649
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0000F451 File Offset: 0x0000D651
		[Editor(false)]
		public TextWidget HealthText
		{
			get
			{
				return this._healthText;
			}
			set
			{
				if (this._healthText != value)
				{
					this._healthText = value;
					base.OnPropertyChanged<TextWidget>(value, "HealthText");
					this.HealthUpdated();
				}
			}
		}

		// Token: 0x04000223 RID: 547
		private readonly int FullHealthyLimit = 90;

		// Token: 0x04000224 RID: 548
		private readonly Color WoundedColor = Color.FromUint(4290199102U);

		// Token: 0x04000225 RID: 549
		private readonly Color HealthyColor = Color.FromUint(4291732560U);

		// Token: 0x04000226 RID: 550
		private readonly Color FullHealthyColor = Color.FromUint(4284921662U);

		// Token: 0x04000227 RID: 551
		private BrushLayer brushLayer;

		// Token: 0x04000228 RID: 552
		private int _health;

		// Token: 0x04000229 RID: 553
		private bool _isWounded;

		// Token: 0x0400022A RID: 554
		private TextWidget _healthText;
	}
}
