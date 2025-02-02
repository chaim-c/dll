using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x0200000E RID: 14
	public class LauncherHintVM : ViewModel
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003317 File Offset: 0x00001517
		public LauncherHintVM(string text)
		{
			this.Text = text;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003326 File Offset: 0x00001526
		public void ExecuteBeginHint()
		{
			if (!string.IsNullOrEmpty(this.Text))
			{
				LauncherUI.AddHintInformation(this.Text);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003340 File Offset: 0x00001540
		public void ExecuteEndHint()
		{
			LauncherUI.HideHintInformation();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003347 File Offset: 0x00001547
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000334F File Offset: 0x0000154F
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

		// Token: 0x0400003D RID: 61
		private string _text;
	}
}
