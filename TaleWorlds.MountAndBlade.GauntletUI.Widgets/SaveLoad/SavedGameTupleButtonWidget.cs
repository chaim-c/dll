using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.SaveLoad
{
	// Token: 0x02000053 RID: 83
	public class SavedGameTupleButtonWidget : ButtonWidget
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0000E4A9 File Offset: 0x0000C6A9
		public SavedGameTupleButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000E4B2 File Offset: 0x0000C6B2
		protected override void OnClick()
		{
			base.OnClick();
			if (this.ScreenWidget != null)
			{
				this.ScreenWidget.SetCurrentSaveTuple(this);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000E4CE File Offset: 0x0000C6CE
		private void OnSaveDeletion(Widget widget)
		{
			this.ScreenWidget.SetCurrentSaveTuple(null);
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		[Editor(false)]
		public SaveLoadScreenWidget ScreenWidget
		{
			get
			{
				return this._screenWidget;
			}
			set
			{
				if (this._screenWidget != value)
				{
					this._screenWidget = value;
					base.OnPropertyChanged<SaveLoadScreenWidget>(value, "ScreenWidget");
				}
			}
		}

		// Token: 0x040001F5 RID: 501
		private SaveLoadScreenWidget _screenWidget;
	}
}
