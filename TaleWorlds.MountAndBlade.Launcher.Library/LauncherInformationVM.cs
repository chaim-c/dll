using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x0200000D RID: 13
	public class LauncherInformationVM : ViewModel
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003283 File Offset: 0x00001483
		public LauncherInformationVM()
		{
			LauncherUI.OnAddHintInformation += this.ExecuteEnableHint;
			LauncherUI.OnHideHintInformation += this.ExecuteDisableHint;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000032AD File Offset: 0x000014AD
		private void ExecuteEnableHint(string text)
		{
			this.IsEnabled = true;
			this.Text = text;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000032BD File Offset: 0x000014BD
		private void ExecuteDisableHint()
		{
			this.IsEnabled = false;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000032C6 File Offset: 0x000014C6
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000032CE File Offset: 0x000014CE
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000032EC File Offset: 0x000014EC
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000032F4 File Offset: 0x000014F4
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x0400003B RID: 59
		private bool _isEnabled;

		// Token: 0x0400003C RID: 60
		private string _text;
	}
}
