using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Scoreboard
{
	// Token: 0x0200008B RID: 139
	public class MultiplayerScoreboardSideWidget : Widget
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x00016384 File Offset: 0x00014584
		public MultiplayerScoreboardSideWidget(UIContext context) : base(context)
		{
			this._nameColumnItemDescription = new ContainerItemDescription();
			this._nameColumnItemDescription.WidgetIndex = 3;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000163AF File Offset: 0x000145AF
		private void AvatarColumnWidthRatioUpdated()
		{
			if (this.TitlesListPanel == null)
			{
				return;
			}
			this._nameColumnItemDescription.WidthStretchRatio = this.NameColumnWidthRatio;
			this.TitlesListPanel.AddItemDescription(this._nameColumnItemDescription);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000163DC File Offset: 0x000145DC
		private void UpdateBackgroundColors()
		{
			if (string.IsNullOrEmpty(this.CultureId))
			{
				return;
			}
			base.Color = this.CultureColor;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000163F8 File Offset: 0x000145F8
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x00016400 File Offset: 0x00014600
		public Color CultureColor
		{
			get
			{
				return this._cultureColor;
			}
			set
			{
				if (value != this._cultureColor)
				{
					this._cultureColor = value;
					base.OnPropertyChanged(value, "CultureColor");
					this.UpdateBackgroundColors();
				}
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00016429 File Offset: 0x00014629
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00016431 File Offset: 0x00014631
		public string CultureId
		{
			get
			{
				return this._cultureId;
			}
			set
			{
				if (value != this._cultureId)
				{
					this._cultureId = value;
					base.OnPropertyChanged<string>(value, "CultureId");
					this.UpdateBackgroundColors();
				}
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0001645A File Offset: 0x0001465A
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00016462 File Offset: 0x00014662
		public bool UseSecondary
		{
			get
			{
				return this._useSecondary;
			}
			set
			{
				if (value != this._useSecondary)
				{
					this._useSecondary = value;
					base.OnPropertyChanged(value, "UseSecondary");
					this.UpdateBackgroundColors();
				}
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00016486 File Offset: 0x00014686
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0001648E File Offset: 0x0001468E
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
					this.AvatarColumnWidthRatioUpdated();
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x000164B2 File Offset: 0x000146B2
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x000164BA File Offset: 0x000146BA
		public ListPanel TitlesListPanel
		{
			get
			{
				return this._titlesListPanel;
			}
			set
			{
				if (value != this._titlesListPanel)
				{
					this._titlesListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "TitlesListPanel");
					this.AvatarColumnWidthRatioUpdated();
				}
			}
		}

		// Token: 0x04000362 RID: 866
		private ContainerItemDescription _nameColumnItemDescription;

		// Token: 0x04000363 RID: 867
		private float _nameColumnWidthRatio = 1f;

		// Token: 0x04000364 RID: 868
		private ListPanel _titlesListPanel;

		// Token: 0x04000365 RID: 869
		private string _cultureId;

		// Token: 0x04000366 RID: 870
		private Color _cultureColor;

		// Token: 0x04000367 RID: 871
		private bool _useSecondary;
	}
}
