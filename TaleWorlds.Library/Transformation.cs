using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	public struct Transformation
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000109E8 File Offset: 0x0000EBE8
		public static Transformation Identity
		{
			get
			{
				return new Transformation(new Vec3(0f, 0f, 0f, 1f), Mat3.Identity, Vec3.One);
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00010A12 File Offset: 0x0000EC12
		public Transformation(Vec3 origin, Mat3 rotation, Vec3 scale)
		{
			this.Origin = origin;
			this.Rotation = rotation;
			this.Scale = scale;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00010A2C File Offset: 0x0000EC2C
		public MatrixFrame AsMatrixFrame
		{
			get
			{
				MatrixFrame result = default(MatrixFrame);
				result.origin = this.Origin;
				result.rotation = this.Rotation;
				result.rotation.ApplyScaleLocal(this.Scale);
				result.Fill();
				return result;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00010A78 File Offset: 0x0000EC78
		public static Transformation CreateFromMatrixFrame(MatrixFrame matrixFrame)
		{
			Mat3 rotation = matrixFrame.rotation;
			Vec3 scaleVector = matrixFrame.rotation.GetScaleVector();
			rotation.ApplyScaleLocal(new Vec3(1f / scaleVector.X, 1f / scaleVector.Y, 1f / scaleVector.Z, -1f));
			return new Transformation(matrixFrame.origin, rotation, scaleVector);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00010ADE File Offset: 0x0000ECDE
		public bool HasNegativeScale()
		{
			return this.Scale.X * this.Scale.Y * this.Scale.Z < 0f;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00010B0A File Offset: 0x0000ED0A
		public static Transformation CreateFromRotation(Mat3 rotation)
		{
			return new Transformation(Vec3.Zero, rotation, Vec3.One);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public Vec3 TransformToParent(Vec3 v)
		{
			return this.AsMatrixFrame.TransformToParent(v);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00010B38 File Offset: 0x0000ED38
		public Transformation TransformToParent(Transformation t)
		{
			return Transformation.CreateFromMatrixFrame(this.AsMatrixFrame.TransformToParent(t.AsMatrixFrame));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00010B60 File Offset: 0x0000ED60
		public Vec3 TransformToLocal(Vec3 v)
		{
			return this.AsMatrixFrame.TransformToLocal(v);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00010B7C File Offset: 0x0000ED7C
		public Transformation TransformToLocal(Transformation t)
		{
			return Transformation.CreateFromMatrixFrame(this.AsMatrixFrame.TransformToLocal(t.AsMatrixFrame));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		public void Rotate(float radian, Vec3 axis)
		{
			Transformation transformation = this;
			transformation.Scale = Vec3.One;
			MatrixFrame asMatrixFrame = transformation.AsMatrixFrame;
			asMatrixFrame.Rotate(radian, axis);
			this.Rotation = asMatrixFrame.rotation;
			this.Origin = asMatrixFrame.origin;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00010BED File Offset: 0x0000EDED
		public static bool operator ==(Transformation t1, Transformation t2)
		{
			return t1.Origin == t2.Origin && t1.Rotation == t2.Rotation && t1.Scale == t2.Scale;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00010C28 File Offset: 0x0000EE28
		public void ApplyScale(Vec3 vec3)
		{
			this.Scale.x = this.Scale.x * vec3.x;
			this.Scale.y = this.Scale.y * vec3.y;
			this.Scale.z = this.Scale.z * vec3.z;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00010C74 File Offset: 0x0000EE74
		public static bool operator !=(Transformation t1, Transformation t2)
		{
			return t1.Origin != t2.Origin || t1.Rotation != t2.Rotation || t1.Scale != t2.Scale;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00010CAF File Offset: 0x0000EEAF
		public override bool Equals(object obj)
		{
			return this == (Transformation)obj;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00010CC2 File Offset: 0x0000EEC2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00010CD4 File Offset: 0x0000EED4
		public override string ToString()
		{
			string text = "Transformation:\n";
			text = string.Concat(new object[]
			{
				text,
				"Origin: ",
				this.Origin.x,
				", ",
				this.Origin.y,
				", ",
				this.Origin.z,
				"\n"
			});
			text += "Rotation:\n";
			text += this.Rotation.ToString();
			return string.Concat(new object[]
			{
				text,
				"Scale: ",
				this.Scale.x,
				", ",
				this.Scale.y,
				", ",
				this.Scale.z,
				"\n"
			});
		}

		// Token: 0x0400017C RID: 380
		public Vec3 Origin;

		// Token: 0x0400017D RID: 381
		public Mat3 Rotation;

		// Token: 0x0400017E RID: 382
		public Vec3 Scale;
	}
}
