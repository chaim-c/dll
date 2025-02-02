using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.Launcher.Library.CustomWidgets
{
	// Token: 0x02000022 RID: 34
	public class LauncherHintTriggerWidget : Widget
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00005F59 File Offset: 0x00004159
		public LauncherHintTriggerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005F62 File Offset: 0x00004162
		protected override void OnConnectedToRoot()
		{
			base.ParentWidget.EventFire += this.ParentWidgetEventFired;
			base.OnConnectedToRoot();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005F81 File Offset: 0x00004181
		protected override void OnDisconnectedFromRoot()
		{
			base.ParentWidget.EventFire -= this.ParentWidgetEventFired;
			base.OnDisconnectedFromRoot();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005FA0 File Offset: 0x000041A0
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
				}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005FF0 File Offset: 0x000041F0
		protected override bool OnPreviewMousePressed()
		{
			return false;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005FF3 File Offset: 0x000041F3
		protected override bool OnPreviewDragBegin()
		{
			return false;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005FF6 File Offset: 0x000041F6
		protected override bool OnPreviewDrop()
		{
			return false;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005FF9 File Offset: 0x000041F9
		protected override bool OnPreviewMouseScroll()
		{
			return false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005FFC File Offset: 0x000041FC
		protected override bool OnPreviewMouseReleased()
		{
			return false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005FFF File Offset: 0x000041FF
		protected override bool OnPreviewMouseMove()
		{
			return true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006002 File Offset: 0x00004202
		protected override bool OnPreviewDragHover()
		{
			return false;
		}
	}
}
