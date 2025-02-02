using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000123 RID: 291
	public class KingdomClanTypeVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x00029CEE File Offset: 0x00027EEE
		public KingdomClanTypeVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00029D00 File Offset: 0x00027F00
		private void UpdateTypeVisual()
		{
			if (this.Type == 0)
			{
				this.SetState("Normal");
				return;
			}
			if (this.Type == 1)
			{
				this.SetState("Leader");
				return;
			}
			if (this.Type == 2)
			{
				this.SetState("Mercenary");
				return;
			}
			Debug.FailedAssert("This clan type is not defined in widget", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Kingdom\\KingdomClanTypeVisualBrushWidget.cs", "UpdateTypeVisual", 37);
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00029D61 File Offset: 0x00027F61
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00029D69 File Offset: 0x00027F69
		[Editor(false)]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (this._type != value)
				{
					this._type = value;
					base.OnPropertyChanged(value, "Type");
					this.UpdateTypeVisual();
				}
			}
		}

		// Token: 0x040006E9 RID: 1769
		private int _type = -1;
	}
}
