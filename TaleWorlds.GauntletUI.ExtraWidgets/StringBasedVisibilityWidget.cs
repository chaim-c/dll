﻿using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.ExtraWidgets
{
	// Token: 0x02000010 RID: 16
	public class StringBasedVisibilityWidget : Widget
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000061B5 File Offset: 0x000043B5
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000061BD File Offset: 0x000043BD
		public StringBasedVisibilityWidget.WatchTypes WatchType { get; set; }

		// Token: 0x060000F8 RID: 248 RVA: 0x000061C6 File Offset: 0x000043C6
		public StringBasedVisibilityWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000061CF File Offset: 0x000043CF
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000061D8 File Offset: 0x000043D8
		[Editor(false)]
		public string FirstString
		{
			get
			{
				return this._firstString;
			}
			set
			{
				if (this._firstString != value)
				{
					this._firstString = value;
					base.OnPropertyChanged<string>(value, "FirstString");
					StringBasedVisibilityWidget.WatchTypes watchType = this.WatchType;
					if (watchType == StringBasedVisibilityWidget.WatchTypes.Equal)
					{
						base.IsVisible = string.Equals(value, this.SecondString, StringComparison.OrdinalIgnoreCase);
						return;
					}
					if (watchType != StringBasedVisibilityWidget.WatchTypes.NotEqual)
					{
						return;
					}
					base.IsVisible = !string.Equals(value, this.SecondString, StringComparison.OrdinalIgnoreCase);
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000623F File Offset: 0x0000443F
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00006248 File Offset: 0x00004448
		[Editor(false)]
		public string SecondString
		{
			get
			{
				return this._secondString;
			}
			set
			{
				if (this._secondString != value)
				{
					this._secondString = value;
					base.OnPropertyChanged<string>(value, "SecondString");
					StringBasedVisibilityWidget.WatchTypes watchType = this.WatchType;
					if (watchType == StringBasedVisibilityWidget.WatchTypes.Equal)
					{
						base.IsVisible = string.Equals(value, this.FirstString, StringComparison.OrdinalIgnoreCase);
						return;
					}
					if (watchType != StringBasedVisibilityWidget.WatchTypes.NotEqual)
					{
						return;
					}
					base.IsVisible = !string.Equals(value, this.FirstString, StringComparison.OrdinalIgnoreCase);
				}
			}
		}

		// Token: 0x04000074 RID: 116
		private string _firstString;

		// Token: 0x04000075 RID: 117
		private string _secondString;

		// Token: 0x0200001A RID: 26
		public enum WatchTypes
		{
			// Token: 0x040000B4 RID: 180
			Equal,
			// Token: 0x040000B5 RID: 181
			NotEqual
		}
	}
}
