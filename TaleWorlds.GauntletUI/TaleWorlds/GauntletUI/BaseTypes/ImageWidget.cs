using System;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000061 RID: 97
	public class ImageWidget : BrushWidget
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001B4EB File Offset: 0x000196EB
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0001B4F3 File Offset: 0x000196F3
		public bool OverrideDefaultStateSwitchingEnabled { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001B4FC File Offset: 0x000196FC
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0001B507 File Offset: 0x00019707
		public bool OverrideDefaultStateSwitchingDisabled
		{
			get
			{
				return !this.OverrideDefaultStateSwitchingEnabled;
			}
			set
			{
				if (value != !this.OverrideDefaultStateSwitchingEnabled)
				{
					this.OverrideDefaultStateSwitchingEnabled = !value;
				}
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001B51F File Offset: 0x0001971F
		public ImageWidget(UIContext context) : base(context)
		{
			base.AddState("Pressed");
			base.AddState("Hovered");
			base.AddState("Disabled");
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001B54C File Offset: 0x0001974C
		protected override void RefreshState()
		{
			if (!this.OverrideDefaultStateSwitchingEnabled)
			{
				if (base.IsDisabled)
				{
					this.SetState("Disabled");
				}
				else if (base.IsPressed)
				{
					this.SetState("Pressed");
				}
				else if (base.IsHovered)
				{
					this.SetState("Hovered");
				}
				else
				{
					this.SetState("Default");
				}
			}
			base.RefreshState();
		}
	}
}
