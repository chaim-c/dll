using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Information.RundownTooltip
{
	// Token: 0x0200013E RID: 318
	public class RundownLineWidget : ListPanel
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0002F81E File Offset: 0x0002DA1E
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x0002F826 File Offset: 0x0002DA26
		public TextWidget NameTextWidget { get; set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0002F82F File Offset: 0x0002DA2F
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x0002F837 File Offset: 0x0002DA37
		public TextWidget ValueTextWidget { get; set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0002F840 File Offset: 0x0002DA40
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x0002F848 File Offset: 0x0002DA48
		public float Value { get; set; }

		// Token: 0x060010FE RID: 4350 RVA: 0x0002F851 File Offset: 0x0002DA51
		public RundownLineWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0002F85C File Offset: 0x0002DA5C
		public void RefreshValueOffset(float columnWidth)
		{
			if (columnWidth >= 0f && this.NameTextWidget.Size.X > 1E-05f && this.ValueTextWidget.Size.X > 1E-05f)
			{
				this.ValueTextWidget.ScaledPositionXOffset = columnWidth - (this.NameTextWidget.Size.X + this.ValueTextWidget.Size.X + base.ScaledMarginLeft + base.ScaledMarginRight);
			}
		}
	}
}
