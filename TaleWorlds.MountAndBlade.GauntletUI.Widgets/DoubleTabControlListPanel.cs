using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000017 RID: 23
	public class DoubleTabControlListPanel : ListPanel
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00005204 File Offset: 0x00003404
		public DoubleTabControlListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000520D File Offset: 0x0000340D
		public void OnFirstTabClick(Widget widget)
		{
			if (!this._firstList.IsVisible && this._secondList.IsVisible)
			{
				this._secondList.IsVisible = false;
				this._firstList.IsVisible = true;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005241 File Offset: 0x00003441
		public void OnSecondTabClick(Widget widget)
		{
			if (this._firstList.IsVisible && !this._secondList.IsVisible)
			{
				this._secondList.IsVisible = true;
				this._firstList.IsVisible = false;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005275 File Offset: 0x00003475
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005280 File Offset: 0x00003480
		[Editor(false)]
		public ButtonWidget FirstListButton
		{
			get
			{
				return this._firstListButton;
			}
			set
			{
				if (this._firstListButton != value)
				{
					this._firstListButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "FirstListButton");
					if (this.FirstListButton != null && !this.FirstListButton.ClickEventHandlers.Contains(new Action<Widget>(this.OnFirstTabClick)))
					{
						this.FirstListButton.ClickEventHandlers.Add(new Action<Widget>(this.OnFirstTabClick));
					}
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000052EB File Offset: 0x000034EB
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000052F4 File Offset: 0x000034F4
		[Editor(false)]
		public ButtonWidget SecondListButton
		{
			get
			{
				return this._secondListButton;
			}
			set
			{
				if (this._secondListButton != value)
				{
					this._secondListButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "SecondListButton");
					if (this.SecondListButton != null && !this.SecondListButton.ClickEventHandlers.Contains(new Action<Widget>(this.OnSecondTabClick)))
					{
						this.SecondListButton.ClickEventHandlers.Add(new Action<Widget>(this.OnSecondTabClick));
					}
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000535F File Offset: 0x0000355F
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00005367 File Offset: 0x00003567
		[Editor(false)]
		public Widget FirstList
		{
			get
			{
				return this._firstList;
			}
			set
			{
				if (this._firstList != value)
				{
					this._firstList = value;
					base.OnPropertyChanged<Widget>(value, "FirstList");
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005385 File Offset: 0x00003585
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000538D File Offset: 0x0000358D
		[Editor(false)]
		public Widget SecondList
		{
			get
			{
				return this._secondList;
			}
			set
			{
				if (this._secondList != value)
				{
					this._secondList = value;
					base.OnPropertyChanged<Widget>(value, "SecondList");
				}
			}
		}

		// Token: 0x04000091 RID: 145
		private ButtonWidget _firstListButton;

		// Token: 0x04000092 RID: 146
		private ButtonWidget _secondListButton;

		// Token: 0x04000093 RID: 147
		private Widget _firstList;

		// Token: 0x04000094 RID: 148
		private Widget _secondList;
	}
}
