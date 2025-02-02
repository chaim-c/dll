using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000121 RID: 289
	public class DecisionSupportStrengthListPanel : ListPanel
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000297CA File Offset: 0x000279CA
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x000297D2 File Offset: 0x000279D2
		public bool IsAbstain { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000297DB File Offset: 0x000279DB
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x000297E3 File Offset: 0x000279E3
		public bool IsPlayerSupporter { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x000297EC File Offset: 0x000279EC
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x000297F4 File Offset: 0x000279F4
		public bool IsOptionSelected { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x000297FD File Offset: 0x000279FD
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x00029805 File Offset: 0x00027A05
		public bool IsKingsOutcome { get; set; }

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002980E File Offset: 0x00027A0E
		public DecisionSupportStrengthListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00029818 File Offset: 0x00027A18
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			switch (this.CurrentIndex)
			{
			case 2:
				this.StrengthButton0.IsSelected = true;
				this.StrengthButton1.IsSelected = false;
				this.StrengthButton2.IsSelected = false;
				break;
			case 3:
				this.StrengthButton0.IsSelected = false;
				this.StrengthButton1.IsSelected = true;
				this.StrengthButton2.IsSelected = false;
				break;
			case 4:
				this.StrengthButton0.IsSelected = false;
				this.StrengthButton1.IsSelected = false;
				this.StrengthButton2.IsSelected = true;
				break;
			}
			base.GamepadNavigationIndex = (this.IsOptionSelected ? -1 : 0);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000298CB File Offset: 0x00027ACB
		private void SetButtonsEnabled(bool isEnabled)
		{
			this.StrengthButton0.IsEnabled = isEnabled;
			this.StrengthButton1.IsEnabled = isEnabled;
			this.StrengthButton2.IsEnabled = isEnabled;
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000298F1 File Offset: 0x00027AF1
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x000298F9 File Offset: 0x00027AF9
		[Editor(false)]
		public int CurrentIndex
		{
			get
			{
				return this._currentIndex;
			}
			set
			{
				if (this._currentIndex != value)
				{
					this._currentIndex = value;
					base.OnPropertyChanged(value, "CurrentIndex");
				}
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00029917 File Offset: 0x00027B17
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x0002991F File Offset: 0x00027B1F
		[Editor(false)]
		public ButtonWidget StrengthButton0
		{
			get
			{
				return this._strengthButton0;
			}
			set
			{
				if (this._strengthButton0 != value)
				{
					this._strengthButton0 = value;
					base.OnPropertyChanged<ButtonWidget>(value, "StrengthButton0");
				}
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0002993D File Offset: 0x00027B3D
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x00029945 File Offset: 0x00027B45
		[Editor(false)]
		public ButtonWidget StrengthButton1
		{
			get
			{
				return this._strengthButton1;
			}
			set
			{
				if (this._strengthButton1 != value)
				{
					this._strengthButton1 = value;
					base.OnPropertyChanged<ButtonWidget>(value, "StrengthButton1");
				}
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00029963 File Offset: 0x00027B63
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0002996B File Offset: 0x00027B6B
		[Editor(false)]
		public ButtonWidget StrengthButton2
		{
			get
			{
				return this._strengthButton2;
			}
			set
			{
				if (this._strengthButton2 != value)
				{
					this._strengthButton2 = value;
					base.OnPropertyChanged<ButtonWidget>(value, "StrengthButton2");
				}
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00029989 File Offset: 0x00027B89
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00029991 File Offset: 0x00027B91
		[Editor(false)]
		public RichTextWidget StrengthButton0Text
		{
			get
			{
				return this._strengthButton0Text;
			}
			set
			{
				if (this._strengthButton0Text != value)
				{
					this._strengthButton0Text = value;
					base.OnPropertyChanged<RichTextWidget>(value, "StrengthButton0Text");
				}
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x000299AF File Offset: 0x00027BAF
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x000299B7 File Offset: 0x00027BB7
		[Editor(false)]
		public RichTextWidget StrengthButton1Text
		{
			get
			{
				return this._strengthButton1Text;
			}
			set
			{
				if (this._strengthButton1Text != value)
				{
					this._strengthButton1Text = value;
					base.OnPropertyChanged<RichTextWidget>(value, "StrengthButton1Text");
				}
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x000299D5 File Offset: 0x00027BD5
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x000299DD File Offset: 0x00027BDD
		[Editor(false)]
		public RichTextWidget StrengthButton2Text
		{
			get
			{
				return this._strengthButton2Text;
			}
			set
			{
				if (this._strengthButton2Text != value)
				{
					this._strengthButton2Text = value;
					base.OnPropertyChanged<RichTextWidget>(value, "StrengthButton2Text");
				}
			}
		}

		// Token: 0x040006DD RID: 1757
		private ButtonWidget _strengthButton0;

		// Token: 0x040006DE RID: 1758
		private RichTextWidget _strengthButton0Text;

		// Token: 0x040006DF RID: 1759
		private ButtonWidget _strengthButton1;

		// Token: 0x040006E0 RID: 1760
		private RichTextWidget _strengthButton1Text;

		// Token: 0x040006E1 RID: 1761
		private ButtonWidget _strengthButton2;

		// Token: 0x040006E2 RID: 1762
		private RichTextWidget _strengthButton2Text;

		// Token: 0x040006E3 RID: 1763
		private int _currentIndex;
	}
}
