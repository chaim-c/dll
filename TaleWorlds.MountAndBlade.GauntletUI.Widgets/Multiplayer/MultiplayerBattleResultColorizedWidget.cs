using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x0200007E RID: 126
	public class MultiplayerBattleResultColorizedWidget : Widget
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x0001511F File Offset: 0x0001331F
		public MultiplayerBattleResultColorizedWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00015130 File Offset: 0x00013330
		private void BattleResultUpdated()
		{
			if (this.BattleResult == 2)
			{
				base.Color = this.DrawColor;
				return;
			}
			if (this.BattleResult == 1)
			{
				base.Color = this.VictoryColor;
				return;
			}
			if (this.BattleResult == 0)
			{
				base.Color = this.DefeatColor;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001517D File Offset: 0x0001337D
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00015185 File Offset: 0x00013385
		[Editor(false)]
		public int BattleResult
		{
			get
			{
				return this._battleResult;
			}
			set
			{
				if (this._battleResult != value)
				{
					this._battleResult = value;
					base.OnPropertyChanged(value, "BattleResult");
					this.BattleResultUpdated();
				}
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x000151A9 File Offset: 0x000133A9
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x000151B1 File Offset: 0x000133B1
		[Editor(false)]
		public Color DrawColor
		{
			get
			{
				return this._drawColor;
			}
			set
			{
				if (this._drawColor != value)
				{
					this._drawColor = value;
					base.OnPropertyChanged(value, "DrawColor");
				}
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x000151D4 File Offset: 0x000133D4
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x000151DC File Offset: 0x000133DC
		[Editor(false)]
		public Color VictoryColor
		{
			get
			{
				return this._victoryColor;
			}
			set
			{
				if (this._victoryColor != value)
				{
					this._victoryColor = value;
					base.OnPropertyChanged(value, "VictoryColor");
				}
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000151FF File Offset: 0x000133FF
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x00015207 File Offset: 0x00013407
		[Editor(false)]
		public Color DefeatColor
		{
			get
			{
				return this._defeatColor;
			}
			set
			{
				if (this._defeatColor != value)
				{
					this._defeatColor = value;
					base.OnPropertyChanged(value, "DefeatColor");
				}
			}
		}

		// Token: 0x0400031F RID: 799
		private int _battleResult = -1;

		// Token: 0x04000320 RID: 800
		private Color _drawColor;

		// Token: 0x04000321 RID: 801
		private Color _victoryColor;

		// Token: 0x04000322 RID: 802
		private Color _defeatColor;
	}
}
