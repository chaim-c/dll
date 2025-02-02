using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200003F RID: 63
	public class TabControlWidget : Widget
	{
		// Token: 0x0600036B RID: 875 RVA: 0x0000AE41 File Offset: 0x00009041
		public TabControlWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000AE4C File Offset: 0x0000904C
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this.FirstButton.ClickEventHandlers.Contains(new Action<Widget>(this.OnFirstButtonClick)))
			{
				this.FirstButton.ClickEventHandlers.Add(new Action<Widget>(this.OnFirstButtonClick));
			}
			if (!this.SecondButton.ClickEventHandlers.Contains(new Action<Widget>(this.OnSecondButtonClick)))
			{
				this.SecondButton.ClickEventHandlers.Add(new Action<Widget>(this.OnSecondButtonClick));
			}
			this.FirstButton.IsSelected = this.FirstItem.IsVisible;
			this.SecondButton.IsSelected = this.SecondItem.IsVisible;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000AF00 File Offset: 0x00009100
		public void OnFirstButtonClick(Widget widget)
		{
			if (!this._firstItem.IsVisible && this._secondItem.IsVisible)
			{
				this._secondItem.IsVisible = false;
				this._firstItem.IsVisible = true;
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000AF34 File Offset: 0x00009134
		public void OnSecondButtonClick(Widget widget)
		{
			if (this._firstItem.IsVisible && !this._secondItem.IsVisible)
			{
				this._secondItem.IsVisible = true;
				this._firstItem.IsVisible = false;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000AF68 File Offset: 0x00009168
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000AF70 File Offset: 0x00009170
		[Editor(false)]
		public ButtonWidget FirstButton
		{
			get
			{
				return this._firstButton;
			}
			set
			{
				if (this._firstButton != value)
				{
					this._firstButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "FirstButton");
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000AF8E File Offset: 0x0000918E
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000AF96 File Offset: 0x00009196
		[Editor(false)]
		public ButtonWidget SecondButton
		{
			get
			{
				return this._secondButton;
			}
			set
			{
				if (this._secondButton != value)
				{
					this._secondButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "SecondButton");
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000AFB4 File Offset: 0x000091B4
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000AFBC File Offset: 0x000091BC
		[Editor(false)]
		public Widget SecondItem
		{
			get
			{
				return this._secondItem;
			}
			set
			{
				if (this._secondItem != value)
				{
					this._secondItem = value;
					base.OnPropertyChanged<Widget>(value, "SecondItem");
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000AFDA File Offset: 0x000091DA
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000AFE2 File Offset: 0x000091E2
		[Editor(false)]
		public Widget FirstItem
		{
			get
			{
				return this._firstItem;
			}
			set
			{
				if (this._firstItem != value)
				{
					this._firstItem = value;
					base.OnPropertyChanged<Widget>(value, "FirstItem");
				}
			}
		}

		// Token: 0x0400016D RID: 365
		private ButtonWidget _firstButton;

		// Token: 0x0400016E RID: 366
		private ButtonWidget _secondButton;

		// Token: 0x0400016F RID: 367
		private Widget _firstItem;

		// Token: 0x04000170 RID: 368
		private Widget _secondItem;
	}
}
