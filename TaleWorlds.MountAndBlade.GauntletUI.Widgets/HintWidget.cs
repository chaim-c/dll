using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000022 RID: 34
	public class HintWidget : Widget
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00007058 File Offset: 0x00005258
		public HintWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007061 File Offset: 0x00005261
		protected override void OnConnectedToRoot()
		{
			base.ParentWidget.EventFire += this.ParentWidgetEventFired;
			base.OnConnectedToRoot();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007080 File Offset: 0x00005280
		protected override void OnDisconnectedFromRoot()
		{
			base.ParentWidget.EventFire -= this.ParentWidgetEventFired;
			base.OnDisconnectedFromRoot();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000070A0 File Offset: 0x000052A0
		private void ParentWidgetEventFired(Widget widget, string eventName, object[] args)
		{
			if (base.IsVisible)
			{
				if (eventName == "HoverBegin")
				{
					base.EventFired("HoverBegin", Array.Empty<object>());
					return;
				}
				if (eventName == "HoverEnd")
				{
					base.EventFired("HoverEnd", Array.Empty<object>());
					return;
				}
				if (eventName == "DragHoverBegin")
				{
					base.EventFired("DragHoverBegin", Array.Empty<object>());
					return;
				}
				if (eventName == "DragHoverEnd")
				{
					base.EventFired("DragHoverEnd", Array.Empty<object>());
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000712C File Offset: 0x0000532C
		protected override bool OnPreviewMousePressed()
		{
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000712F File Offset: 0x0000532F
		protected override bool OnPreviewDragBegin()
		{
			return false;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007132 File Offset: 0x00005332
		protected override bool OnPreviewDrop()
		{
			return false;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007135 File Offset: 0x00005335
		protected override bool OnPreviewMouseScroll()
		{
			return false;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007138 File Offset: 0x00005338
		protected override bool OnPreviewMouseReleased()
		{
			return false;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000713B File Offset: 0x0000533B
		protected override bool OnPreviewMouseMove()
		{
			return true;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000713E File Offset: 0x0000533E
		protected override bool OnPreviewDragHover()
		{
			return false;
		}
	}
}
