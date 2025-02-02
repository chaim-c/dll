using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Options
{
	// Token: 0x0200006E RID: 110
	public class OptionsBrightnessImageSliderWidget : SliderWidget
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00011A98 File Offset: 0x0000FC98
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		public bool IsMax { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00011AA9 File Offset: 0x0000FCA9
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00011AB1 File Offset: 0x0000FCB1
		public Widget ImageWidget { get; set; }

		// Token: 0x060005EA RID: 1514 RVA: 0x00011ABA File Offset: 0x0000FCBA
		public OptionsBrightnessImageSliderWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isInitialized)
			{
				float value;
				if (this.IsMax)
				{
					value = (float)(base.ValueInt - 1) * 0.003f + 1f;
				}
				else
				{
					value = (float)(base.ValueInt + 1) * 0.003f;
				}
				this.SetColorOfImage(MBMath.ClampFloat(value, 0f, 1f));
				this._isInitialized = true;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00011B34 File Offset: 0x0000FD34
		protected override void OnValueFloatChanged(float value)
		{
			base.OnValueFloatChanged(value);
			float value2;
			if (this.IsMax)
			{
				value2 = (value - 1f) * 0.003f + 1f;
			}
			else
			{
				value2 = (value + 1f) * 0.003f;
			}
			this.SetColorOfImage(MBMath.ClampFloat(value2, 0f, 1f));
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00011B90 File Offset: 0x0000FD90
		private void SetColorOfImage(float value)
		{
			this.ImageWidget.Color = new Color(value, value, value, 1f);
		}

		// Token: 0x0400028D RID: 653
		private bool _isInitialized;
	}
}
