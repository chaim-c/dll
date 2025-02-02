using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.ExtraWidgets.Graph
{
	// Token: 0x02000016 RID: 22
	public class GraphLineWidget : Widget
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006D3D File Offset: 0x00004F3D
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00006D45 File Offset: 0x00004F45
		public string LineBrushStateName { get; set; }

		// Token: 0x06000124 RID: 292 RVA: 0x00006D4E File Offset: 0x00004F4E
		public GraphLineWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006D58 File Offset: 0x00004F58
		private void OnPointContainerEventFire(Widget widget, string eventName, object[] eventArgs)
		{
			GraphLinePointWidget arg;
			if (eventName == "ItemAdd" && eventArgs.Length != 0 && (arg = (eventArgs[0] as GraphLinePointWidget)) != null)
			{
				Action<GraphLineWidget, GraphLinePointWidget> onPointAdded = this.OnPointAdded;
				if (onPointAdded == null)
				{
					return;
				}
				onPointAdded(this, arg);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006D94 File Offset: 0x00004F94
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00006D9C File Offset: 0x00004F9C
		public Widget PointContainerWidget
		{
			get
			{
				return this._pointContainerWidget;
			}
			set
			{
				if (value != this._pointContainerWidget)
				{
					if (this._pointContainerWidget != null)
					{
						this._pointContainerWidget.EventFire -= this.OnPointContainerEventFire;
					}
					this._pointContainerWidget = value;
					if (this._pointContainerWidget != null)
					{
						this._pointContainerWidget.EventFire += this.OnPointContainerEventFire;
					}
					base.OnPropertyChanged<Widget>(value, "PointContainerWidget");
				}
			}
		}

		// Token: 0x0400008F RID: 143
		public Action<GraphLineWidget, GraphLinePointWidget> OnPointAdded;

		// Token: 0x04000090 RID: 144
		private Widget _pointContainerWidget;
	}
}
