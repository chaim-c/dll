using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200007E RID: 126
	public struct Ray
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000F54C File Offset: 0x0000D74C
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000F554 File Offset: 0x0000D754
		public Vec3 Origin
		{
			get
			{
				return this._origin;
			}
			private set
			{
				this._origin = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000F55D File Offset: 0x0000D75D
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0000F565 File Offset: 0x0000D765
		public Vec3 Direction
		{
			get
			{
				return this._direction;
			}
			private set
			{
				this._direction = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000F56E File Offset: 0x0000D76E
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0000F576 File Offset: 0x0000D776
		public float MaxDistance
		{
			get
			{
				return this._maxDistance;
			}
			private set
			{
				this._maxDistance = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000F57F File Offset: 0x0000D77F
		public Vec3 EndPoint
		{
			get
			{
				return this.Origin + this.Direction * this.MaxDistance;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000F59D File Offset: 0x0000D79D
		public Ray(Vec3 origin, Vec3 direction, float maxDistance = 3.4028235E+38f)
		{
			this = default(Ray);
			this.Reset(origin, direction, maxDistance);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public Ray(Vec3 origin, Vec3 direction, bool useDirectionLenForMaxDistance)
		{
			this._origin = origin;
			this._direction = direction;
			float maxDistance = this._direction.Normalize();
			if (useDirectionLenForMaxDistance)
			{
				this._maxDistance = maxDistance;
				return;
			}
			this._maxDistance = float.MaxValue;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000F5ED File Offset: 0x0000D7ED
		public void Reset(Vec3 origin, Vec3 direction, float maxDistance = 3.4028235E+38f)
		{
			this._origin = origin;
			this._direction = direction;
			this._maxDistance = maxDistance;
			this._direction.Normalize();
		}

		// Token: 0x04000146 RID: 326
		private Vec3 _origin;

		// Token: 0x04000147 RID: 327
		private Vec3 _direction;

		// Token: 0x04000148 RID: 328
		private float _maxDistance;
	}
}
