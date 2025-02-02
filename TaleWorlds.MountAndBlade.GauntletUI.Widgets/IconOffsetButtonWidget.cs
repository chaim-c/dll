using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000024 RID: 36
	public class IconOffsetButtonWidget : ButtonWidget
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000072A3 File Offset: 0x000054A3
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000072AB File Offset: 0x000054AB
		public int NormalXOffset { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000072B4 File Offset: 0x000054B4
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000072BC File Offset: 0x000054BC
		public int NormalYOffset { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000072C5 File Offset: 0x000054C5
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000072CD File Offset: 0x000054CD
		public int PressedXOffset { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000072D6 File Offset: 0x000054D6
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000072DE File Offset: 0x000054DE
		public int PressedYOffset { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000072E7 File Offset: 0x000054E7
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000072EF File Offset: 0x000054EF
		public Widget ButtonIcon { get; set; }

		// Token: 0x060001DF RID: 479 RVA: 0x000072F8 File Offset: 0x000054F8
		public IconOffsetButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007304 File Offset: 0x00005504
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.ButtonIcon != null)
			{
				if (base.IsPressed || base.IsSelected)
				{
					this.ButtonIcon.PositionYOffset = (float)this.PressedYOffset;
					this.ButtonIcon.PositionXOffset = (float)this.PressedXOffset;
					return;
				}
				this.ButtonIcon.PositionYOffset = (float)this.NormalYOffset;
				this.ButtonIcon.PositionXOffset = (float)this.NormalXOffset;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000737C File Offset: 0x0000557C
		protected override void RefreshState()
		{
			if (base.IsSelected)
			{
				this.SetState("Selected");
				return;
			}
			if (base.IsDisabled)
			{
				this.SetState("Disabled");
				return;
			}
			if (base.IsPressed)
			{
				this.SetState("Pressed");
				return;
			}
			if (base.IsHovered)
			{
				this.SetState("Hovered");
				return;
			}
			this.SetState("Default");
		}
	}
}
