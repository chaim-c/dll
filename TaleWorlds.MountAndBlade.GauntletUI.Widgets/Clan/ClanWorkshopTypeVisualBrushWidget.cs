using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Clan
{
	// Token: 0x0200016D RID: 365
	public class ClanWorkshopTypeVisualBrushWidget : BrushWidget
	{
		// Token: 0x060012E0 RID: 4832 RVA: 0x00033977 File Offset: 0x00031B77
		public ClanWorkshopTypeVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0003398B File Offset: 0x00031B8B
		private void SetVisualState(string type)
		{
			this.RegisterBrushStatesOfWidget();
			this.SetState(type);
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0003399A File Offset: 0x00031B9A
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x000339A2 File Offset: 0x00031BA2
		[Editor(false)]
		public string WorkshopType
		{
			get
			{
				return this._workshopType;
			}
			set
			{
				if (this._workshopType != value)
				{
					this._workshopType = value;
					base.OnPropertyChanged<string>(value, "WorkshopType");
					this.SetVisualState(value);
				}
			}
		}

		// Token: 0x04000894 RID: 2196
		private string _workshopType = "";
	}
}
