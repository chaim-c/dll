using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x0200005C RID: 92
	public class PartyFormationDropdownWidget : DropdownWidget
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000EE6C File Offset: 0x0000D06C
		public PartyFormationDropdownWidget(UIContext context) : base(context)
		{
			base.DoNotHandleDropdownListPanel = true;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000EE7C File Offset: 0x0000D07C
		private void ListStateChangerUpdated()
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000EE7E File Offset: 0x0000D07E
		private void SeperatorStateChangerUpdated()
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000EE80 File Offset: 0x0000D080
		protected override void OpenPanel()
		{
			base.ListPanel.IsVisible = true;
			this.SeperatorStateChanger.IsVisible = true;
			this.ListStateChanger.Delay = this.SeperatorStateChanger.VisualDefinition.TransitionDuration;
			this.ListStateChanger.State = "Opened";
			this.ListStateChanger.Start();
			this.SeperatorStateChanger.Delay = 0f;
			this.SeperatorStateChanger.State = "Opened";
			this.SeperatorStateChanger.Start();
			base.Context.TwoDimensionContext.PlaySound("dropdown");
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000EF1C File Offset: 0x0000D11C
		protected override void ClosePanel()
		{
			this.ListStateChanger.Delay = 0f;
			this.ListStateChanger.State = "Closed";
			this.ListStateChanger.Start();
			this.SeperatorStateChanger.Delay = this.ListStateChanger.TargetWidget.VisualDefinition.TransitionDuration;
			this.SeperatorStateChanger.State = "Closed";
			this.SeperatorStateChanger.Start();
			base.Context.TwoDimensionContext.PlaySound("dropdown");
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		[Editor(false)]
		public DelayedStateChanger SeperatorStateChanger
		{
			get
			{
				return this._seperatorStateChanger;
			}
			set
			{
				if (this._seperatorStateChanger != value)
				{
					this._seperatorStateChanger = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "SeperatorStateChanger");
					this.SeperatorStateChangerUpdated();
				}
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		[Editor(false)]
		public DelayedStateChanger ListStateChanger
		{
			get
			{
				return this._listStateChanger;
			}
			set
			{
				if (this._listStateChanger != value)
				{
					this._listStateChanger = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "ListStateChanger");
					this.ListStateChangerUpdated();
				}
			}
		}

		// Token: 0x0400021A RID: 538
		private DelayedStateChanger _seperatorStateChanger;

		// Token: 0x0400021B RID: 539
		private DelayedStateChanger _listStateChanger;
	}
}
