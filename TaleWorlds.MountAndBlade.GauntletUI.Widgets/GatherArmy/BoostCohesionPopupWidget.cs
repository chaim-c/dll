using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GatherArmy
{
	// Token: 0x02000140 RID: 320
	public class BoostCohesionPopupWidget : Widget
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x0002FD21 File Offset: 0x0002DF21
		public BoostCohesionPopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0002FD2C File Offset: 0x0002DF2C
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.ClosePopupButton != null && !this.ClosePopupButton.ClickEventHandlers.Contains(new Action<Widget>(this.ClosePopup)))
			{
				this.ClosePopupButton.ClickEventHandlers.Add(new Action<Widget>(this.ClosePopup));
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0002FD82 File Offset: 0x0002DF82
		public void ClosePopup(Widget widget)
		{
			base.ParentWidget.IsVisible = false;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0002FD90 File Offset: 0x0002DF90
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x0002FD98 File Offset: 0x0002DF98
		[Editor(false)]
		public ButtonWidget ClosePopupButton
		{
			get
			{
				return this._closePopupButton;
			}
			set
			{
				if (this._closePopupButton != value)
				{
					this._closePopupButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ClosePopupButton");
				}
			}
		}

		// Token: 0x040007D7 RID: 2007
		private ButtonWidget _closePopupButton;
	}
}
