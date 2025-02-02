using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000019 RID: 25
	public class EncyclopediaTroopScrollablePanel : ScrollablePanel
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005678 File Offset: 0x00003878
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00005680 File Offset: 0x00003880
		public bool PanWithMouseEnabled { get; set; }

		// Token: 0x06000141 RID: 321 RVA: 0x00005689 File Offset: 0x00003889
		public EncyclopediaTroopScrollablePanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005694 File Offset: 0x00003894
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.PanWithMouseEnabled)
			{
				bool flag = this.IsMouseOverWidget(this);
				if (flag)
				{
					foreach (Widget widget in base.AllChildrenAndThis)
					{
						if (this.IsMouseOverWidget(widget) && widget is ButtonWidget)
						{
							flag = false;
						}
					}
				}
				if (flag && base.HorizontalScrollbar != null && this._canScrollHorizontal)
				{
					base.SetActiveCursor(UIContext.MouseCursors.Move);
					if (Input.IsKeyPressed(InputKey.LeftMouseButton))
					{
						this._isDragging = true;
					}
				}
			}
			if (Input.IsKeyReleased(InputKey.LeftMouseButton))
			{
				this._isDragging = false;
			}
			if (this._isDragging)
			{
				base.HorizontalScrollbar.ValueFloat -= Input.MouseMoveX;
				base.VerticalScrollbar.ValueFloat -= Input.MouseMoveY;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005780 File Offset: 0x00003980
		private bool IsMouseOverWidget(Widget widget)
		{
			return widget.GlobalPosition.X <= Input.MousePositionPixel.X && Input.MousePositionPixel.X <= widget.GlobalPosition.X + widget.Size.X && widget.GlobalPosition.Y <= Input.MousePositionPixel.Y && Input.MousePositionPixel.Y <= widget.GlobalPosition.Y + widget.Size.Y;
		}

		// Token: 0x04000097 RID: 151
		private bool _isDragging;
	}
}
