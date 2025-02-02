using System;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI.ExtraWidgets
{
	// Token: 0x0200000C RID: 12
	public class MouseWidget : Widget
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000052F7 File Offset: 0x000034F7
		public MouseWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005300 File Offset: 0x00003500
		protected override void OnUpdate(float dt)
		{
			if (base.IsVisible)
			{
				this.UpdatePressedKeys();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005310 File Offset: 0x00003510
		public void UpdatePressedKeys()
		{
			Color color = new Color(1f, 0f, 0f, 1f);
			this.LeftMouseButton.Color = Color.White;
			this.RightMouseButton.Color = Color.White;
			this.MiddleMouseButton.Color = Color.White;
			this.MouseX1Button.Color = Color.White;
			this.MouseX2Button.Color = Color.White;
			this.MouseScrollUp.IsVisible = false;
			this.MouseScrollDown.IsVisible = false;
			this.KeyboardKeys.Text = "";
			if (Input.IsKeyDown(InputKey.LeftMouseButton))
			{
				this.LeftMouseButton.Color = color;
			}
			if (Input.IsKeyDown(InputKey.RightMouseButton))
			{
				this.RightMouseButton.Color = color;
			}
			if (Input.IsKeyDown(InputKey.MiddleMouseButton))
			{
				this.MiddleMouseButton.Color = color;
			}
			if (Input.IsKeyDown(InputKey.X1MouseButton))
			{
				this.MouseX1Button.Color = color;
			}
			if (Input.IsKeyDown(InputKey.X2MouseButton))
			{
				this.MouseX2Button.Color = color;
			}
			if (Input.IsKeyDown(InputKey.MouseScrollUp))
			{
				this.MouseScrollUp.IsVisible = true;
			}
			if (Input.IsKeyDown(InputKey.MouseScrollDown))
			{
				this.MouseScrollDown.IsVisible = true;
			}
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "UpdatePressedKeys");
			for (int i = 0; i < 256; i++)
			{
				if (Key.GetInputType((InputKey)i) == Key.InputType.Keyboard && Input.IsKeyDown((InputKey)i))
				{
					InputKey inputKey = (InputKey)i;
					mbstringBuilder.Append<string>(inputKey.ToString());
					mbstringBuilder.Append<string>(", ");
				}
			}
			this.KeyboardKeys.Text = mbstringBuilder.ToStringAndRelease().TrimEnd(MouseWidget._trimChars);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000054CE File Offset: 0x000036CE
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000054D6 File Offset: 0x000036D6
		public Widget LeftMouseButton { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000054DF File Offset: 0x000036DF
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000054E7 File Offset: 0x000036E7
		public Widget RightMouseButton { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000054F0 File Offset: 0x000036F0
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000054F8 File Offset: 0x000036F8
		public Widget MiddleMouseButton { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005501 File Offset: 0x00003701
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005509 File Offset: 0x00003709
		public Widget MouseX1Button { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005512 File Offset: 0x00003712
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000551A File Offset: 0x0000371A
		public Widget MouseX2Button { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005523 File Offset: 0x00003723
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000552B File Offset: 0x0000372B
		public Widget MouseScrollUp { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005534 File Offset: 0x00003734
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000553C File Offset: 0x0000373C
		public Widget MouseScrollDown { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005545 File Offset: 0x00003745
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000554D File Offset: 0x0000374D
		public TextWidget KeyboardKeys { get; set; }

		// Token: 0x0400004B RID: 75
		private static readonly char[] _trimChars = new char[]
		{
			' ',
			','
		};
	}
}
