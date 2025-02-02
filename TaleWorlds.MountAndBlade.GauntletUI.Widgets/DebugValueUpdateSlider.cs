using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000016 RID: 22
	public class DebugValueUpdateSlider : SliderWidget
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00005166 File Offset: 0x00003366
		public DebugValueUpdateSlider(UIContext context) : base(context)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000516F File Offset: 0x0000336F
		protected override void OnValueIntChanged(int value)
		{
			base.OnValueIntChanged(value);
			this.OnValueChanged((float)value);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005180 File Offset: 0x00003380
		protected override void OnValueFloatChanged(float value)
		{
			base.OnValueFloatChanged(value);
			this.OnValueChanged(value);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005190 File Offset: 0x00003390
		private void OnValueChanged(float value)
		{
			if (this.WidgetToUpdate != null)
			{
				this.WidgetToUpdate.Text = this.WidgetToUpdate.GlobalPosition.Y.ToString("F0");
			}
			if (this.ValueToUpdate != null)
			{
				this.ValueToUpdate.InitialAmount = (int)value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000051E2 File Offset: 0x000033E2
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000051EA File Offset: 0x000033EA
		public TextWidget WidgetToUpdate { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000051F3 File Offset: 0x000033F3
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000051FB File Offset: 0x000033FB
		public FillBarVerticalWidget ValueToUpdate { get; set; }
	}
}
