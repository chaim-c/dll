using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Clan
{
	// Token: 0x02000168 RID: 360
	public class ClanFinanceTextWidget : TextWidget
	{
		// Token: 0x060012C9 RID: 4809 RVA: 0x00033536 File Offset: 0x00031736
		public ClanFinanceTextWidget(UIContext context) : base(context)
		{
			base.intPropertyChanged += this.IntText_PropertyChanged;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00033551 File Offset: 0x00031751
		private void IntText_PropertyChanged(PropertyOwnerObject widget, string propertyName, int propertyValue)
		{
			if (this.NegativeMarkWidget != null && propertyName == "IntText")
			{
				this.NegativeMarkWidget.IsVisible = (propertyValue < 0);
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00033578 File Offset: 0x00031778
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.Text != null && base.Text != string.Empty)
			{
				base.Text = MathF.Abs(base.IntText).ToString();
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000335BF File Offset: 0x000317BF
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x000335C7 File Offset: 0x000317C7
		[Editor(false)]
		public TextWidget NegativeMarkWidget
		{
			get
			{
				return this._negativeMarkWidget;
			}
			set
			{
				if (this._negativeMarkWidget != value)
				{
					this._negativeMarkWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "NegativeMarkWidget");
				}
			}
		}

		// Token: 0x0400088E RID: 2190
		private TextWidget _negativeMarkWidget;
	}
}
