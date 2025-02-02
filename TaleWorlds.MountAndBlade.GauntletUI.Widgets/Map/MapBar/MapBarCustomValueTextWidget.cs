using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapBar
{
	// Token: 0x02000118 RID: 280
	public class MapBarCustomValueTextWidget : TextWidget
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00028A86 File Offset: 0x00026C86
		public MapBarCustomValueTextWidget(UIContext context) : base(context)
		{
			base.OverrideDefaultStateSwitchingEnabled = true;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00028A98 File Offset: 0x00026C98
		private void RefreshTextAnimation(int valueDifference)
		{
			if (valueDifference > 0)
			{
				if (base.CurrentState == "Positive")
				{
					base.BrushRenderer.RestartAnimation();
					return;
				}
				this.SetState("Positive");
				return;
			}
			else
			{
				if (valueDifference >= 0)
				{
					Debug.FailedAssert("Value change in party label cannot be 0", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Map\\MapBar\\MapBarCustomValueTextWidget.cs", "RefreshTextAnimation", 40);
					return;
				}
				if (base.CurrentState == "Negative")
				{
					base.BrushRenderer.RestartAnimation();
					return;
				}
				this.SetState("Negative");
				return;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00028B17 File Offset: 0x00026D17
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00028B1F File Offset: 0x00026D1F
		[Editor(false)]
		public int ValueAsInt
		{
			get
			{
				return this._totalTroops;
			}
			set
			{
				if (value != this._totalTroops)
				{
					this.RefreshTextAnimation(value - this._totalTroops);
					this._totalTroops = value;
					base.OnPropertyChanged(value, "ValueAsInt");
				}
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00028B4B File Offset: 0x00026D4B
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00028B54 File Offset: 0x00026D54
		[Editor(false)]
		public bool IsWarning
		{
			get
			{
				return this._isWarning;
			}
			set
			{
				if (value != this._isWarning)
				{
					this._isWarning = value;
					base.OnPropertyChanged(value, "IsWarning");
					base.ReadOnlyBrush.GetStyleOrDefault(base.CurrentState);
					Color fontColor = Color.Black;
					if (value)
					{
						fontColor = this.WarningColor;
					}
					else
					{
						fontColor = this.NormalColor;
					}
					foreach (Style style in base.Brush.Styles)
					{
						style.FontColor = fontColor;
					}
				}
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00028BF4 File Offset: 0x00026DF4
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00028BFC File Offset: 0x00026DFC
		[Editor(false)]
		public Color NormalColor
		{
			get
			{
				return this._normalColor;
			}
			set
			{
				if (value.Alpha != this._normalColor.Alpha || value.Blue != this._normalColor.Blue || value.Red != this._normalColor.Red || value.Green != this._normalColor.Green)
				{
					this._normalColor = value;
					base.OnPropertyChanged(value, "NormalColor");
				}
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00028C68 File Offset: 0x00026E68
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00028C70 File Offset: 0x00026E70
		[Editor(false)]
		public Color WarningColor
		{
			get
			{
				return this._warningColor;
			}
			set
			{
				if (value.Alpha != this._warningColor.Alpha || value.Blue != this._warningColor.Blue || value.Red != this._warningColor.Red || value.Green != this._warningColor.Green)
				{
					this._warningColor = value;
					base.OnPropertyChanged(value, "WarningColor");
				}
			}
		}

		// Token: 0x040006B9 RID: 1721
		private bool _isWarning;

		// Token: 0x040006BA RID: 1722
		private Color _normalColor;

		// Token: 0x040006BB RID: 1723
		private Color _warningColor;

		// Token: 0x040006BC RID: 1724
		private int _totalTroops;
	}
}
