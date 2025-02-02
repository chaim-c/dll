using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x02000016 RID: 22
	public class HintViewModel : ViewModel
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000041C8 File Offset: 0x000023C8
		public HintViewModel()
		{
			this.HintText = TextObject.Empty;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000041DB File Offset: 0x000023DB
		public HintViewModel(TextObject hintText, string uniqueName = null)
		{
			this.HintText = hintText;
			this._uniqueName = uniqueName;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000041F1 File Offset: 0x000023F1
		public void ExecuteBeginHint()
		{
			if (!TextObject.IsNullOrEmpty(this.HintText))
			{
				MBInformationManager.ShowHint(this.HintText.ToString());
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004210 File Offset: 0x00002410
		public void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x0400006E RID: 110
		public TextObject HintText;

		// Token: 0x0400006F RID: 111
		private readonly string _uniqueName;
	}
}
