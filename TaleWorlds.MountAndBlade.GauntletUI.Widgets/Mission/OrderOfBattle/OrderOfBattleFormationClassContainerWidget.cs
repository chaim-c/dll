using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E0 RID: 224
	public class OrderOfBattleFormationClassContainerWidget : Widget
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x000204D5 File Offset: 0x0001E6D5
		public OrderOfBattleFormationClassContainerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x000204DE File Offset: 0x0001E6DE
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x000204E6 File Offset: 0x0001E6E6
		[Editor(false)]
		public SliderWidget WeightSlider
		{
			get
			{
				return this._weightSlider;
			}
			set
			{
				if (value != this._weightSlider)
				{
					this._weightSlider = value;
					base.OnPropertyChanged<SliderWidget>(value, "WeightSlider");
				}
			}
		}

		// Token: 0x0400054D RID: 1357
		private SliderWidget _weightSlider;
	}
}
