using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000127 RID: 295
	public class KingdomTabControlListPanel : ListPanel
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x0002A1FB File Offset: 0x000283FB
		public KingdomTabControlListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002A204 File Offset: 0x00028404
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.FiefsButton.IsSelected = this.FiefsPanel.IsVisible;
			this.PoliciesButton.IsSelected = this.PoliciesPanel.IsVisible;
			this.ClansButton.IsSelected = this.ClansPanel.IsVisible;
			this.ArmiesButton.IsSelected = this.ArmiesPanel.IsVisible;
			this.DiplomacyButton.IsSelected = this.DiplomacyPanel.IsVisible;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0002A286 File Offset: 0x00028486
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0002A28E File Offset: 0x0002848E
		[Editor(false)]
		public Widget DiplomacyPanel
		{
			get
			{
				return this._diplomacyPanel;
			}
			set
			{
				if (this._diplomacyPanel != value)
				{
					this._diplomacyPanel = value;
					base.OnPropertyChanged<Widget>(value, "DiplomacyPanel");
				}
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0002A2AC File Offset: 0x000284AC
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0002A2B4 File Offset: 0x000284B4
		[Editor(false)]
		public Widget ArmiesPanel
		{
			get
			{
				return this._armiesPanel;
			}
			set
			{
				if (this._armiesPanel != value)
				{
					this._armiesPanel = value;
					base.OnPropertyChanged<Widget>(value, "ArmiesPanel");
				}
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0002A2D2 File Offset: 0x000284D2
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x0002A2DA File Offset: 0x000284DA
		[Editor(false)]
		public Widget ClansPanel
		{
			get
			{
				return this._clansPanel;
			}
			set
			{
				if (this._clansPanel != value)
				{
					this._clansPanel = value;
					base.OnPropertyChanged<Widget>(value, "ClansPanel");
				}
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0002A2F8 File Offset: 0x000284F8
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x0002A300 File Offset: 0x00028500
		[Editor(false)]
		public Widget PoliciesPanel
		{
			get
			{
				return this._policiesPanel;
			}
			set
			{
				if (this._policiesPanel != value)
				{
					this._policiesPanel = value;
					base.OnPropertyChanged<Widget>(value, "PoliciesPanel");
				}
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0002A31E File Offset: 0x0002851E
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0002A326 File Offset: 0x00028526
		[Editor(false)]
		public Widget FiefsPanel
		{
			get
			{
				return this._fiefsPanel;
			}
			set
			{
				if (this._fiefsPanel != value)
				{
					this._fiefsPanel = value;
					base.OnPropertyChanged<Widget>(value, "FiefsPanel");
				}
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0002A344 File Offset: 0x00028544
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x0002A34C File Offset: 0x0002854C
		[Editor(false)]
		public ButtonWidget FiefsButton
		{
			get
			{
				return this._fiefsButton;
			}
			set
			{
				if (this._fiefsButton != value)
				{
					this._fiefsButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "FiefsButton");
				}
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0002A36A File Offset: 0x0002856A
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x0002A372 File Offset: 0x00028572
		[Editor(false)]
		public ButtonWidget PoliciesButton
		{
			get
			{
				return this._policiesButton;
			}
			set
			{
				if (this._policiesButton != value)
				{
					this._policiesButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "PoliciesButton");
				}
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0002A390 File Offset: 0x00028590
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x0002A398 File Offset: 0x00028598
		[Editor(false)]
		public ButtonWidget ClansButton
		{
			get
			{
				return this._clansButton;
			}
			set
			{
				if (this._clansButton != value)
				{
					this._clansButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ClansButton");
				}
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0002A3B6 File Offset: 0x000285B6
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0002A3BE File Offset: 0x000285BE
		[Editor(false)]
		public ButtonWidget ArmiesButton
		{
			get
			{
				return this._armiesButton;
			}
			set
			{
				if (this._armiesButton != value)
				{
					this._armiesButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ArmiesButton");
				}
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0002A3DC File Offset: 0x000285DC
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x0002A3E4 File Offset: 0x000285E4
		[Editor(false)]
		public ButtonWidget DiplomacyButton
		{
			get
			{
				return this._diplomacyButton;
			}
			set
			{
				if (this._diplomacyButton != value)
				{
					this._diplomacyButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "DiplomacyButton");
				}
			}
		}

		// Token: 0x040006FB RID: 1787
		private Widget _armiesPanel;

		// Token: 0x040006FC RID: 1788
		private Widget _clansPanel;

		// Token: 0x040006FD RID: 1789
		private Widget _policiesPanel;

		// Token: 0x040006FE RID: 1790
		private Widget _fiefsPanel;

		// Token: 0x040006FF RID: 1791
		private Widget _diplomacyPanel;

		// Token: 0x04000700 RID: 1792
		private ButtonWidget _fiefsButton;

		// Token: 0x04000701 RID: 1793
		private ButtonWidget _clansButton;

		// Token: 0x04000702 RID: 1794
		private ButtonWidget _policiesButton;

		// Token: 0x04000703 RID: 1795
		private ButtonWidget _armiesButton;

		// Token: 0x04000704 RID: 1796
		private ButtonWidget _diplomacyButton;
	}
}
