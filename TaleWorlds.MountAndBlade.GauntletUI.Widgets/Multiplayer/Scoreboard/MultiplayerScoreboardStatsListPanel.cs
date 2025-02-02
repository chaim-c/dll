using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Scoreboard
{
	// Token: 0x0200008C RID: 140
	public class MultiplayerScoreboardStatsListPanel : ListPanel
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x000164E0 File Offset: 0x000146E0
		public MultiplayerScoreboardStatsListPanel(UIContext context) : base(context)
		{
			this._nameColumnItemDescription = new ContainerItemDescription
			{
				WidgetId = "name"
			};
			this._scoreColumnItemDescription = new ContainerItemDescription
			{
				WidgetId = "score"
			};
			this._soldiersColumnItemDescription = new ContainerItemDescription
			{
				WidgetId = "soldiers"
			};
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00016557 File Offset: 0x00014757
		private void NameColumnWidthRatioUpdated()
		{
			this._nameColumnItemDescription.WidthStretchRatio = this.NameColumnWidthRatio;
			base.AddItemDescription(this._nameColumnItemDescription);
			base.SetMeasureAndLayoutDirty();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001657C File Offset: 0x0001477C
		private void ScoreColumnWidthRatioUpdated()
		{
			this._scoreColumnItemDescription.WidthStretchRatio = this.ScoreColumnWidthRatio;
			base.AddItemDescription(this._scoreColumnItemDescription);
			base.SetMeasureAndLayoutDirty();
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000165A1 File Offset: 0x000147A1
		private void SoldiersColumnWidthRatioUpdated()
		{
			this._soldiersColumnItemDescription.WidthStretchRatio = this.SoldiersColumnWidthRatio;
			base.AddItemDescription(this._soldiersColumnItemDescription);
			base.SetMeasureAndLayoutDirty();
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000165C6 File Offset: 0x000147C6
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x000165CE File Offset: 0x000147CE
		public float NameColumnWidthRatio
		{
			get
			{
				return this._nameColumnWidthRatio;
			}
			set
			{
				if (value != this._nameColumnWidthRatio)
				{
					this._nameColumnWidthRatio = value;
					base.OnPropertyChanged(value, "NameColumnWidthRatio");
					this.NameColumnWidthRatioUpdated();
				}
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000165F2 File Offset: 0x000147F2
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x000165FA File Offset: 0x000147FA
		public float ScoreColumnWidthRatio
		{
			get
			{
				return this._scoreColumnWidthRatio;
			}
			set
			{
				if (value != this._scoreColumnWidthRatio)
				{
					this._scoreColumnWidthRatio = value;
					base.OnPropertyChanged(value, "ScoreColumnWidthRatio");
					this.ScoreColumnWidthRatioUpdated();
				}
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001661E File Offset: 0x0001481E
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00016626 File Offset: 0x00014826
		public float SoldiersColumnWidthRatio
		{
			get
			{
				return this._soldiersColumnWidthRatio;
			}
			set
			{
				if (value != this._soldiersColumnWidthRatio)
				{
					this._soldiersColumnWidthRatio = value;
					base.OnPropertyChanged(value, "SoldiersColumnWidthRatio");
					this.SoldiersColumnWidthRatioUpdated();
				}
			}
		}

		// Token: 0x04000368 RID: 872
		private ContainerItemDescription _nameColumnItemDescription;

		// Token: 0x04000369 RID: 873
		private ContainerItemDescription _scoreColumnItemDescription;

		// Token: 0x0400036A RID: 874
		private ContainerItemDescription _soldiersColumnItemDescription;

		// Token: 0x0400036B RID: 875
		private const string _nameColumnWidgetID = "name";

		// Token: 0x0400036C RID: 876
		private const string _scoreColumnWidgetID = "score";

		// Token: 0x0400036D RID: 877
		private const string _soldiersColumnWidgetID = "soldiers";

		// Token: 0x0400036E RID: 878
		private float _nameColumnWidthRatio = 1f;

		// Token: 0x0400036F RID: 879
		private float _scoreColumnWidthRatio = 1f;

		// Token: 0x04000370 RID: 880
		private float _soldiersColumnWidthRatio = 1f;
	}
}
