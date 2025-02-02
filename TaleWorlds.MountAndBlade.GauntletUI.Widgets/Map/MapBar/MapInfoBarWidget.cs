using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapBar
{
	// Token: 0x0200011D RID: 285
	public class MapInfoBarWidget : Widget
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000ECF RID: 3791 RVA: 0x00029274 File Offset: 0x00027474
		// (remove) Token: 0x06000ED0 RID: 3792 RVA: 0x000292AC File Offset: 0x000274AC
		public event MapInfoBarWidget.MapBarExtendStateChangeEvent OnMapInfoBarExtendStateChange;

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000292E1 File Offset: 0x000274E1
		public MapInfoBarWidget(UIContext context) : base(context)
		{
			base.AddState("Disabled");
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000292F5 File Offset: 0x000274F5
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.RefreshBarExtendState();
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00029304 File Offset: 0x00027504
		private void OnExtendButtonClick(Widget widget)
		{
			this.IsInfoBarExtended = !this.IsInfoBarExtended;
			this.RefreshBarExtendState();
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002931C File Offset: 0x0002751C
		private void RefreshBarExtendState()
		{
			if (this.IsInfoBarExtended && base.CurrentState != "Extended")
			{
				this.SetState("Extended");
				this.RefreshVerticalVisual();
				return;
			}
			if (!this.IsInfoBarExtended && base.CurrentState != "Default")
			{
				this.SetState("Default");
				this.RefreshVerticalVisual();
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00029380 File Offset: 0x00027580
		private void RefreshVerticalVisual()
		{
			foreach (Style style in this.ExtendButtonWidget.Brush.Styles)
			{
				for (int i = 0; i < style.LayerCount; i++)
				{
					style.GetLayer(i).VerticalFlip = this.IsInfoBarExtended;
				}
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x000293FC File Offset: 0x000275FC
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x00029404 File Offset: 0x00027604
		[Editor(false)]
		public ButtonWidget ExtendButtonWidget
		{
			get
			{
				return this._extendButtonWidget;
			}
			set
			{
				if (this._extendButtonWidget != value)
				{
					this._extendButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ExtendButtonWidget");
					if (!this._extendButtonWidget.ClickEventHandlers.Contains(new Action<Widget>(this.OnExtendButtonClick)))
					{
						this._extendButtonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnExtendButtonClick));
					}
				}
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00029467 File Offset: 0x00027667
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x0002946F File Offset: 0x0002766F
		[Editor(false)]
		public bool IsInfoBarExtended
		{
			get
			{
				return this._isInfoBarExtended;
			}
			set
			{
				if (this._isInfoBarExtended != value)
				{
					this._isInfoBarExtended = value;
					base.OnPropertyChanged(value, "IsInfoBarExtended");
					MapInfoBarWidget.MapBarExtendStateChangeEvent onMapInfoBarExtendStateChange = this.OnMapInfoBarExtendStateChange;
					if (onMapInfoBarExtendStateChange == null)
					{
						return;
					}
					onMapInfoBarExtendStateChange(this.IsInfoBarExtended);
				}
			}
		}

		// Token: 0x040006CE RID: 1742
		private ButtonWidget _extendButtonWidget;

		// Token: 0x040006CF RID: 1743
		private bool _isInfoBarExtended;

		// Token: 0x020001B2 RID: 434
		// (Invoke) Token: 0x06001488 RID: 5256
		public delegate void MapBarExtendStateChangeEvent(bool newState);
	}
}
