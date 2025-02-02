using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout
{
	// Token: 0x020000C6 RID: 198
	public class MultiplayerClassLoadoutTroopCardBrushWidget : BrushWidget
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x0001D772 File Offset: 0x0001B972
		public MultiplayerClassLoadoutTroopCardBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001D77C File Offset: 0x0001B97C
		private void OnCultureIDUpdated()
		{
			if (this.CultureID != null)
			{
				this.SetState(this.CultureID);
				BrushWidget border = this.Border;
				if (border != null)
				{
					border.SetState(this.CultureID);
				}
				BrushWidget classBorder = this.ClassBorder;
				if (classBorder != null)
				{
					classBorder.SetState(this.CultureID);
				}
				BrushWidget classFrame = this.ClassFrame;
				if (classFrame == null)
				{
					return;
				}
				classFrame.SetState(this.CultureID);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0001D7E1 File Offset: 0x0001B9E1
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x0001D7E9 File Offset: 0x0001B9E9
		[Editor(false)]
		public string CultureID
		{
			get
			{
				return this._cultureID;
			}
			set
			{
				if (value != this._cultureID)
				{
					this._cultureID = value;
					base.OnPropertyChanged<string>(value, "CultureID");
					this.OnCultureIDUpdated();
				}
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0001D812 File Offset: 0x0001BA12
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0001D81A File Offset: 0x0001BA1A
		[Editor(false)]
		public BrushWidget Border
		{
			get
			{
				return this._border;
			}
			set
			{
				if (value != this._border)
				{
					this._border = value;
					base.OnPropertyChanged<BrushWidget>(value, "Border");
					this.OnCultureIDUpdated();
				}
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001D83E File Offset: 0x0001BA3E
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0001D846 File Offset: 0x0001BA46
		[Editor(false)]
		public BrushWidget ClassBorder
		{
			get
			{
				return this._classBorder;
			}
			set
			{
				if (value != this._classBorder)
				{
					this._classBorder = value;
					base.OnPropertyChanged<BrushWidget>(value, "ClassBorder");
					this.OnCultureIDUpdated();
				}
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0001D86A File Offset: 0x0001BA6A
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0001D872 File Offset: 0x0001BA72
		[Editor(false)]
		public BrushWidget ClassFrame
		{
			get
			{
				return this._classFrame;
			}
			set
			{
				if (value != this._classFrame)
				{
					this._classFrame = value;
					base.OnPropertyChanged<BrushWidget>(value, "ClassFrame");
					base.OnPropertyChanged<BrushWidget>(value, "ClassFrame");
				}
			}
		}

		// Token: 0x040004C2 RID: 1218
		private string _cultureID;

		// Token: 0x040004C3 RID: 1219
		private BrushWidget _border;

		// Token: 0x040004C4 RID: 1220
		private BrushWidget _classBorder;

		// Token: 0x040004C5 RID: 1221
		private BrushWidget _classFrame;
	}
}
