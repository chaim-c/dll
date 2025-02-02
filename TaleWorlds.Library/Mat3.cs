using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public struct Mat3
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x000083A1 File Offset: 0x000065A1
		public Mat3(Vec3 s, Vec3 f, Vec3 u)
		{
			this.s = s;
			this.f = f;
			this.u = u;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000083B8 File Offset: 0x000065B8
		public Mat3(float sx, float sy, float sz, float fx, float fy, float fz, float ux, float uy, float uz)
		{
			this.s = new Vec3(sx, sy, sz, -1f);
			this.f = new Vec3(fx, fy, fz, -1f);
			this.u = new Vec3(ux, uy, uz, -1f);
		}

		// Token: 0x17000040 RID: 64
		public Vec3 this[int i]
		{
			get
			{
				switch (i)
				{
				case 0:
					return this.s;
				case 1:
					return this.f;
				case 2:
					return this.u;
				default:
					throw new IndexOutOfRangeException("Vec3 out of bounds.");
				}
			}
			set
			{
				switch (i)
				{
				case 0:
					this.s = value;
					return;
				case 1:
					this.f = value;
					return;
				case 2:
					this.u = value;
					return;
				default:
					throw new IndexOutOfRangeException("Vec3 out of bounds.");
				}
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008474 File Offset: 0x00006674
		public void RotateAboutSide(float a)
		{
			float num;
			float num2;
			MathF.SinCos(a, out num, out num2);
			Vec3 vec = this.f * num2 + this.u * num;
			Vec3 vec2 = this.u * num2 - this.f * num;
			this.u = vec2;
			this.f = vec;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000084D8 File Offset: 0x000066D8
		public void RotateAboutForward(float a)
		{
			float num;
			float num2;
			MathF.SinCos(a, out num, out num2);
			Vec3 vec = this.s * num2 - this.u * num;
			Vec3 vec2 = this.u * num2 + this.s * num;
			this.s = vec;
			this.u = vec2;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000853C File Offset: 0x0000673C
		public void RotateAboutUp(float a)
		{
			float num;
			float num2;
			MathF.SinCos(a, out num, out num2);
			Vec3 vec = this.s * num2 + this.f * num;
			Vec3 vec2 = this.f * num2 - this.s * num;
			this.s = vec;
			this.f = vec2;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000859D File Offset: 0x0000679D
		public void RotateAboutAnArbitraryVector(Vec3 v, float a)
		{
			this.s = this.s.RotateAboutAnArbitraryVector(v, a);
			this.f = this.f.RotateAboutAnArbitraryVector(v, a);
			this.u = this.u.RotateAboutAnArbitraryVector(v, a);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000085D8 File Offset: 0x000067D8
		public bool IsOrthonormal()
		{
			bool result = this.s.IsUnit && this.f.IsUnit && this.u.IsUnit;
			float num = Vec3.DotProduct(this.s, this.f);
			if (num > 0.01f || num < -0.01f)
			{
				result = false;
			}
			else
			{
				Vec3 v = Vec3.CrossProduct(this.s, this.f);
				if (!this.u.NearlyEquals(v, 0.01f))
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000865C File Offset: 0x0000685C
		public bool IsLeftHanded()
		{
			return Vec3.DotProduct(Vec3.CrossProduct(this.s, this.f), this.u) < 0f;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008681 File Offset: 0x00006881
		public bool NearlyEquals(Mat3 rhs, float epsilon = 1E-05f)
		{
			return this.s.NearlyEquals(rhs.s, epsilon) && this.f.NearlyEquals(rhs.f, epsilon) && this.u.NearlyEquals(rhs.u, epsilon);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000086C0 File Offset: 0x000068C0
		public Vec3 TransformToParent(Vec3 v)
		{
			return new Vec3(this.s.x * v.x + this.f.x * v.y + this.u.x * v.z, this.s.y * v.x + this.f.y * v.y + this.u.y * v.z, this.s.z * v.x + this.f.z * v.y + this.u.z * v.z, -1f);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008780 File Offset: 0x00006980
		public Vec3 TransformToParent(ref Vec3 v)
		{
			return new Vec3(this.s.x * v.x + this.f.x * v.y + this.u.x * v.z, this.s.y * v.x + this.f.y * v.y + this.u.y * v.z, this.s.z * v.x + this.f.z * v.y + this.u.z * v.z, -1f);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008840 File Offset: 0x00006A40
		public Vec3 TransformToLocal(Vec3 v)
		{
			return new Vec3(this.s.x * v.x + this.s.y * v.y + this.s.z * v.z, this.f.x * v.x + this.f.y * v.y + this.f.z * v.z, this.u.x * v.x + this.u.y * v.y + this.u.z * v.z, -1f);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000088FF File Offset: 0x00006AFF
		public Mat3 TransformToParent(Mat3 m)
		{
			return new Mat3(this.TransformToParent(m.s), this.TransformToParent(m.f), this.TransformToParent(m.u));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000892C File Offset: 0x00006B2C
		public Mat3 TransformToLocal(Mat3 m)
		{
			Mat3 result;
			result.s = this.TransformToLocal(m.s);
			result.f = this.TransformToLocal(m.f);
			result.u = this.TransformToLocal(m.u);
			return result;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008974 File Offset: 0x00006B74
		public void Orthonormalize()
		{
			this.f.Normalize();
			this.s = Vec3.CrossProduct(this.f, this.u);
			this.s.Normalize();
			this.u = Vec3.CrossProduct(this.s, this.f);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000089C7 File Offset: 0x00006BC7
		public void OrthonormalizeAccordingToForwardAndKeepUpAsZAxis()
		{
			this.f.z = 0f;
			this.f.Normalize();
			this.u = Vec3.Up;
			this.s = Vec3.CrossProduct(this.f, this.u);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008A08 File Offset: 0x00006C08
		public Mat3 GetUnitRotation(float removedScale)
		{
			float num = 1f / removedScale;
			return new Mat3(this.s * num, this.f * num, this.u * num);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008A48 File Offset: 0x00006C48
		public Vec3 MakeUnit()
		{
			return new Vec3
			{
				x = this.s.Normalize(),
				y = this.f.Normalize(),
				z = this.u.Normalize()
			};
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008A94 File Offset: 0x00006C94
		public bool IsUnit()
		{
			return this.s.IsUnit && this.f.IsUnit && this.u.IsUnit;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008ABD File Offset: 0x00006CBD
		public void ApplyScaleLocal(float scaleAmount)
		{
			this.s *= scaleAmount;
			this.f *= scaleAmount;
			this.u *= scaleAmount;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008AF8 File Offset: 0x00006CF8
		public void ApplyScaleLocal(Vec3 scaleAmountXYZ)
		{
			this.s *= scaleAmountXYZ.x;
			this.f *= scaleAmountXYZ.y;
			this.u *= scaleAmountXYZ.z;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool HasScale()
		{
			return !this.s.IsUnit || !this.f.IsUnit || !this.u.IsUnit;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008B76 File Offset: 0x00006D76
		public Vec3 GetScaleVector()
		{
			return new Vec3(this.s.Length, this.f.Length, this.u.Length, -1f);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00008BA3 File Offset: 0x00006DA3
		public Vec3 GetScaleVectorSquared()
		{
			return new Vec3(this.s.LengthSquared, this.f.LengthSquared, this.u.LengthSquared, -1f);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008BD0 File Offset: 0x00006DD0
		public void ToQuaternion(out Quaternion quat)
		{
			quat = Quaternion.QuaternionFromMat3(this);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008BE3 File Offset: 0x00006DE3
		public Quaternion ToQuaternion()
		{
			return Quaternion.QuaternionFromMat3(this);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public static Mat3 Lerp(Mat3 m1, Mat3 m2, float alpha)
		{
			Mat3 identity = Mat3.Identity;
			identity.f = Vec3.Lerp(m1.f, m2.f, alpha);
			identity.u = Vec3.Lerp(m1.u, m2.u, alpha);
			identity.Orthonormalize();
			return identity;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008C40 File Offset: 0x00006E40
		public static Mat3 CreateMat3WithForward(in Vec3 direction)
		{
			Mat3 identity = Mat3.Identity;
			identity.f = direction;
			identity.f.Normalize();
			if (MathF.Abs(identity.f.z) < 0.99f)
			{
				identity.u = new Vec3(0f, 0f, 1f, -1f);
			}
			else
			{
				identity.u = new Vec3(0f, 1f, 0f, -1f);
			}
			identity.s = Vec3.CrossProduct(identity.f, identity.u);
			identity.s.Normalize();
			identity.u = Vec3.CrossProduct(identity.s, identity.f);
			identity.u.Normalize();
			return identity;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008D14 File Offset: 0x00006F14
		public Vec3 GetEulerAngles()
		{
			Mat3 mat = this;
			mat.Orthonormalize();
			return new Vec3(MathF.Asin(mat.f.z), MathF.Atan2(-mat.s.z, mat.u.z), MathF.Atan2(-mat.f.x, mat.f.y), -1f);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008D84 File Offset: 0x00006F84
		public Mat3 Transpose()
		{
			return new Mat3(this.s.x, this.f.x, this.u.x, this.s.y, this.f.y, this.u.y, this.s.z, this.f.z, this.u.z);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00008DFC File Offset: 0x00006FFC
		public static Mat3 Identity
		{
			get
			{
				return new Mat3(new Vec3(1f, 0f, 0f, -1f), new Vec3(0f, 1f, 0f, -1f), new Vec3(0f, 0f, 1f, -1f));
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008E59 File Offset: 0x00007059
		public static Mat3 operator *(Mat3 v, float a)
		{
			return new Mat3(v.s * a, v.f * a, v.u * a);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008E84 File Offset: 0x00007084
		public static bool operator ==(Mat3 m1, Mat3 m2)
		{
			return m1.f == m2.f && m1.u == m2.u;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008EAC File Offset: 0x000070AC
		public static bool operator !=(Mat3 m1, Mat3 m2)
		{
			return m1.f != m2.f || m1.u != m2.u;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008ED4 File Offset: 0x000070D4
		public override string ToString()
		{
			string text = "Mat3: ";
			text = string.Concat(new object[]
			{
				text,
				"s: ",
				this.s.x,
				", ",
				this.s.y,
				", ",
				this.s.z,
				";"
			});
			text = string.Concat(new object[]
			{
				text,
				"f: ",
				this.f.x,
				", ",
				this.f.y,
				", ",
				this.f.z,
				";"
			});
			text = string.Concat(new object[]
			{
				text,
				"u: ",
				this.u.x,
				", ",
				this.u.y,
				", ",
				this.u.z,
				";"
			});
			return text + "\n";
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000902D File Offset: 0x0000722D
		public override bool Equals(object obj)
		{
			return this == (Mat3)obj;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009040 File Offset: 0x00007240
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009054 File Offset: 0x00007254
		public bool IsIdentity()
		{
			return this.s.x == 1f && this.s.y == 0f && this.s.z == 0f && this.f.x == 0f && this.f.y == 1f && this.f.z == 0f && this.u.x == 0f && this.u.y == 0f && this.u.z == 1f;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00009108 File Offset: 0x00007308
		public bool IsZero()
		{
			return this.s.x == 0f && this.s.y == 0f && this.s.z == 0f && this.f.x == 0f && this.f.y == 0f && this.f.z == 0f && this.u.x == 0f && this.u.y == 0f && this.u.z == 0f;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000091BC File Offset: 0x000073BC
		public bool IsUniformScaled()
		{
			Vec3 scaleVectorSquared = this.GetScaleVectorSquared();
			return MBMath.ApproximatelyEquals(scaleVectorSquared.x, scaleVectorSquared.y, 0.01f) && MBMath.ApproximatelyEquals(scaleVectorSquared.x, scaleVectorSquared.z, 0.01f);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009200 File Offset: 0x00007400
		public void ApplyEulerAngles(Vec3 eulerAngles)
		{
			this.RotateAboutUp(eulerAngles.z);
			this.RotateAboutSide(eulerAngles.x);
			this.RotateAboutForward(eulerAngles.y);
		}

		// Token: 0x040000FD RID: 253
		public Vec3 s;

		// Token: 0x040000FE RID: 254
		public Vec3 f;

		// Token: 0x040000FF RID: 255
		public Vec3 u;
	}
}
