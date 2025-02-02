using System;
using TaleWorlds.Library;

namespace TaleWorlds.InputSystem
{
	// Token: 0x0200000E RID: 14
	public class InputState
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000057D8 File Offset: 0x000039D8
		public Vec2 NativeResolution
		{
			get
			{
				return Input.Resolution;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000057DF File Offset: 0x000039DF
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000057E8 File Offset: 0x000039E8
		public Vec2 MousePositionRanged
		{
			get
			{
				return this._mousePositionRanged;
			}
			set
			{
				this._mousePositionRanged = value;
				this._mousePositionPixel = new Vec2(this._mousePositionRanged.x * this.NativeResolution.x, this._mousePositionRanged.y * this.NativeResolution.y);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005835 File Offset: 0x00003A35
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000583D File Offset: 0x00003A3D
		public Vec2 OldMousePositionRanged { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005846 File Offset: 0x00003A46
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000584E File Offset: 0x00003A4E
		public bool MousePositionChanged { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005857 File Offset: 0x00003A57
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00005860 File Offset: 0x00003A60
		public Vec2 MousePositionPixel
		{
			get
			{
				return this._mousePositionPixel;
			}
			set
			{
				this._mousePositionPixel = value;
				this._mousePositionRanged = new Vec2(this._mousePositionPixel.x / Input.Resolution.x, this._mousePositionPixel.y / this.NativeResolution.y);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000058AC File Offset: 0x00003AAC
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000058B4 File Offset: 0x00003AB4
		public Vec2 OldMousePositionPixel { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000058BD File Offset: 0x00003ABD
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000058C5 File Offset: 0x00003AC5
		public float MouseScrollValue { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000058CE File Offset: 0x00003ACE
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000058D6 File Offset: 0x00003AD6
		public bool MouseScrollChanged { get; private set; }

		// Token: 0x06000157 RID: 343 RVA: 0x000058E0 File Offset: 0x00003AE0
		public InputState()
		{
			this.MousePositionRanged = default(Vec2);
			this.OldMousePositionRanged = default(Vec2);
			this.MousePositionPixel = default(Vec2);
			this.OldMousePositionPixel = default(Vec2);
			this._mousePositionRanged = new Vec2(0f, 0f);
			this._mousePositionPixel = new Vec2(0f, 0f);
			this._mousePositionPixelDevice = new Vec2(0f, 0f);
			this._mousePositionRangedDevice = new Vec2(0f, 0f);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005984 File Offset: 0x00003B84
		public bool UpdateMousePosition(float mousePositionX, float mousePositionY)
		{
			this.OldMousePositionRanged = new Vec2(this._mousePositionRangedDevice.x, this._mousePositionRangedDevice.y);
			this._mousePositionRangedDevice = new Vec2(mousePositionX, mousePositionY);
			this.OldMousePositionPixel = new Vec2(this._mousePositionPixelDevice.x, this._mousePositionPixelDevice.y);
			this._mousePositionPixelDevice = new Vec2(this._mousePositionRangedDevice.x * this.NativeResolution.x, this._mousePositionRangedDevice.y * this.NativeResolution.y);
			if (this._mousePositionRangedDevice.x == this.OldMousePositionRanged.x && this._mousePositionRangedDevice.y == this.OldMousePositionRanged.y)
			{
				this.MousePositionChanged = false;
			}
			else
			{
				this.MousePositionChanged = true;
				this.MousePositionPixel = this._mousePositionPixelDevice;
				this.MousePositionRanged = this._mousePositionRangedDevice;
			}
			return this.MousePositionChanged;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005A78 File Offset: 0x00003C78
		public bool UpdateMouseScroll(float mouseScrollValue)
		{
			if (!this.MouseScrollValue.Equals(mouseScrollValue))
			{
				this.MouseScrollValue = mouseScrollValue;
				this.MouseScrollChanged = true;
			}
			else
			{
				this.MouseScrollChanged = false;
			}
			return this.MouseScrollChanged;
		}

		// Token: 0x04000143 RID: 323
		private Vec2 _mousePositionRanged;

		// Token: 0x04000145 RID: 325
		private Vec2 _mousePositionRangedDevice;

		// Token: 0x04000147 RID: 327
		private Vec2 _mousePositionPixel;

		// Token: 0x04000148 RID: 328
		private Vec2 _mousePositionPixelDevice;
	}
}
