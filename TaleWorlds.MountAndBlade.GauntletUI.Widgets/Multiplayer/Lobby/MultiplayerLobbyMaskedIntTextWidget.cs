using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x0200009F RID: 159
	public class MultiplayerLobbyMaskedIntTextWidget : TextWidget
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x00018BBB File Offset: 0x00016DBB
		public MultiplayerLobbyMaskedIntTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00018BC4 File Offset: 0x00016DC4
		private void IntValueUpdated()
		{
			if (this.IntValue == this.MaskedIntValue)
			{
				base.Text = this.MaskText;
				return;
			}
			base.IntText = this.IntValue;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00018BED File Offset: 0x00016DED
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x00018BF5 File Offset: 0x00016DF5
		[Editor(false)]
		public int IntValue
		{
			get
			{
				return this._intValue;
			}
			set
			{
				if (this._intValue != value)
				{
					this._intValue = value;
					base.OnPropertyChanged(value, "IntValue");
					this.IntValueUpdated();
				}
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00018C19 File Offset: 0x00016E19
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x00018C21 File Offset: 0x00016E21
		[Editor(false)]
		public int MaskedIntValue
		{
			get
			{
				return this._maskedIntValue;
			}
			set
			{
				if (this._maskedIntValue != value)
				{
					this._maskedIntValue = value;
					base.OnPropertyChanged(value, "MaskedIntValue");
				}
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00018C3F File Offset: 0x00016E3F
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x00018C47 File Offset: 0x00016E47
		[Editor(false)]
		public string MaskText
		{
			get
			{
				return this._maskText;
			}
			set
			{
				if (this._maskText != value)
				{
					this._maskText = value;
					base.OnPropertyChanged<string>(value, "MaskText");
				}
			}
		}

		// Token: 0x040003E4 RID: 996
		private int _intValue;

		// Token: 0x040003E5 RID: 997
		private int _maskedIntValue;

		// Token: 0x040003E6 RID: 998
		private string _maskText;
	}
}
