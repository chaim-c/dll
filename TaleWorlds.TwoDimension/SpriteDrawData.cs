﻿using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000030 RID: 48
	public readonly struct SpriteDrawData : IEquatable<SpriteDrawData>
	{
		// Token: 0x06000209 RID: 521 RVA: 0x000082E8 File Offset: 0x000064E8
		internal SpriteDrawData(float mapX, float mapY, float scale, float width, float height, bool horizontalFlip, bool verticalFlip)
		{
			this.MapX = mapX;
			this.MapY = mapY;
			this.Scale = scale;
			this.Width = width;
			this.Height = height;
			this.HorizontalFlip = horizontalFlip;
			this.VerticalFlip = verticalFlip;
			int num = this.MapX.GetHashCode();
			num = (num * 397 ^ this.MapY.GetHashCode());
			num = (num * 397 ^ this.Scale.GetHashCode());
			num = (num * 397 ^ this.Width.GetHashCode());
			num = (num * 397 ^ this.Height.GetHashCode());
			num = (num * 397 ^ this.HorizontalFlip.GetHashCode());
			num = (num * 397 ^ this.VerticalFlip.GetHashCode());
			this._hashCode = num;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000083B8 File Offset: 0x000065B8
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			SpriteDrawData spriteDrawData = (SpriteDrawData)obj;
			return this.MapX == spriteDrawData.MapX && this.MapY == spriteDrawData.MapY && this.Scale == spriteDrawData.Scale && this.Width == spriteDrawData.Width && this.Height == spriteDrawData.Height && this.HorizontalFlip == spriteDrawData.HorizontalFlip && this.VerticalFlip == spriteDrawData.VerticalFlip;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008454 File Offset: 0x00006654
		public bool Equals(SpriteDrawData other)
		{
			return this.MapX == other.MapX && this.MapY == other.MapY && this.Scale == other.Scale && this.Width == other.Width && this.Height == other.Height && this.HorizontalFlip == other.HorizontalFlip && this.VerticalFlip == other.VerticalFlip;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000084C8 File Offset: 0x000066C8
		public static bool operator ==(SpriteDrawData a, SpriteDrawData b)
		{
			return a.MapX == b.MapX && a.MapY == b.MapY && a.Scale == b.Scale && a.Width == b.Width && a.Height == b.Height && a.HorizontalFlip == b.HorizontalFlip && a.VerticalFlip == b.VerticalFlip;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008539 File Offset: 0x00006739
		public static bool operator !=(SpriteDrawData a, SpriteDrawData b)
		{
			return !(a == b);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008545 File Offset: 0x00006745
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x0400010A RID: 266
		internal readonly float MapX;

		// Token: 0x0400010B RID: 267
		internal readonly float MapY;

		// Token: 0x0400010C RID: 268
		internal readonly float Scale;

		// Token: 0x0400010D RID: 269
		internal readonly float Width;

		// Token: 0x0400010E RID: 270
		internal readonly float Height;

		// Token: 0x0400010F RID: 271
		internal readonly bool HorizontalFlip;

		// Token: 0x04000110 RID: 272
		internal readonly bool VerticalFlip;

		// Token: 0x04000111 RID: 273
		private readonly int _hashCode;
	}
}
