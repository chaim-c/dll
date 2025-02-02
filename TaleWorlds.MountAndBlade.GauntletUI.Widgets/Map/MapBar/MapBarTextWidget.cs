using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapBar
{
	// Token: 0x0200011A RID: 282
	public class MapBarTextWidget : TextWidget
	{
		// Token: 0x06000EB6 RID: 3766 RVA: 0x00028DD0 File Offset: 0x00026FD0
		public MapBarTextWidget(UIContext context) : base(context)
		{
			base.intPropertyChanged += this.TextPropertyChanged;
			base.OverrideDefaultStateSwitchingEnabled = true;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00028DFC File Offset: 0x00026FFC
		private void TextPropertyChanged(PropertyOwnerObject widget, string propertyName, int propertyValue)
		{
			if (propertyName == "IntText")
			{
				if (this._prevValue != -99)
				{
					if (propertyValue - this._prevValue > 0)
					{
						if (base.CurrentState == "Positive")
						{
							base.BrushRenderer.RestartAnimation();
						}
						else
						{
							this.SetState("Positive");
						}
					}
					else if (propertyValue - this._prevValue < 0)
					{
						if (base.CurrentState == "Negative")
						{
							base.BrushRenderer.RestartAnimation();
						}
						else
						{
							this.SetState("Negative");
						}
					}
				}
				this._prevValue = propertyValue;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00028E95 File Offset: 0x00027095
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x00028EA0 File Offset: 0x000270A0
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

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00028F40 File Offset: 0x00027140
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x00028F48 File Offset: 0x00027148
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

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00028FB4 File Offset: 0x000271B4
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00028FBC File Offset: 0x000271BC
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

		// Token: 0x040006C2 RID: 1730
		private int _prevValue = -99;

		// Token: 0x040006C3 RID: 1731
		private bool _isWarning;

		// Token: 0x040006C4 RID: 1732
		private Color _normalColor;

		// Token: 0x040006C5 RID: 1733
		private Color _warningColor;
	}
}
