using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Scoreboard
{
	// Token: 0x0200004E RID: 78
	public class ScoreboardBattleResultTitleBackgroundWidget : Widget
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0000D92B File Offset: 0x0000BB2B
		public ScoreboardBattleResultTitleBackgroundWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000D934 File Offset: 0x0000BB34
		private void BattleResultUpdated()
		{
			this.DefeatWidget.IsVisible = (this.BattleResult == 0);
			this.VictoryWidget.IsVisible = (this.BattleResult == 1);
			this.RetreatWidget.IsVisible = (this.BattleResult == 2);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000D972 File Offset: 0x0000BB72
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000D97A File Offset: 0x0000BB7A
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000D99E File Offset: 0x0000BB9E
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		[Editor(false)]
		public Widget VictoryWidget
		{
			get
			{
				return this._victoryWidget;
			}
			set
			{
				if (this._victoryWidget != value)
				{
					this._victoryWidget = value;
					base.OnPropertyChanged<Widget>(value, "VictoryWidget");
				}
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		[Editor(false)]
		public Widget DefeatWidget
		{
			get
			{
				return this._defeatWidget;
			}
			set
			{
				if (this._defeatWidget != value)
				{
					this._defeatWidget = value;
					base.OnPropertyChanged<Widget>(value, "DefeatWidget");
				}
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000D9EA File Offset: 0x0000BBEA
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000D9F2 File Offset: 0x0000BBF2
		[Editor(false)]
		public Widget RetreatWidget
		{
			get
			{
				return this._retreatWidget;
			}
			set
			{
				if (this._retreatWidget != value)
				{
					this._retreatWidget = value;
					base.OnPropertyChanged<Widget>(value, "RetreatWidget");
				}
			}
		}

		// Token: 0x040001CF RID: 463
		private int _battleResult;

		// Token: 0x040001D0 RID: 464
		private Widget _victoryWidget;

		// Token: 0x040001D1 RID: 465
		private Widget _defeatWidget;

		// Token: 0x040001D2 RID: 466
		private Widget _retreatWidget;
	}
}
