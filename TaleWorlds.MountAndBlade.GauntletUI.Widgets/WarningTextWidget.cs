using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000042 RID: 66
	public class WarningTextWidget : TextWidget
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000B31F File Offset: 0x0000951F
		public WarningTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000B328 File Offset: 0x00009528
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000B330 File Offset: 0x00009530
		[Editor(false)]
		public bool IsWarned
		{
			get
			{
				return this._isWarned;
			}
			set
			{
				if (this._isWarned != value)
				{
					this._isWarned = value;
					base.OnPropertyChanged(value, "IsWarned");
					this.SetState(this._isWarned ? "Warned" : "Default");
				}
			}
		}

		// Token: 0x04000175 RID: 373
		private bool _isWarned;
	}
}
