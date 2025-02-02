using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	public struct MatrixFrame
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00009226 File Offset: 0x00007426
		public MatrixFrame(Mat3 rot, Vec3 o)
		{
			this.rotation = rot;
			this.origin = o;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00009238 File Offset: 0x00007438
		public MatrixFrame(float _11, float _12, float _13, float _21, float _22, float _23, float _31, float _32, float _33, float _41, float _42, float _43)
		{
			this.rotation = new Mat3(_11, _12, _13, _21, _22, _23, _31, _32, _33);
			this.origin = new Vec3(_41, _42, _43, -1f);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009278 File Offset: 0x00007478
		public MatrixFrame(float _11, float _12, float _13, float _14, float _21, float _22, float _23, float _24, float _31, float _32, float _33, float _34, float _41, float _42, float _43, float _44)
		{
			this.rotation = default(Mat3);
			this.rotation.s = new Vec3(_11, _12, _13, _14);
			this.rotation.f = new Vec3(_21, _22, _23, _24);
			this.rotation.u = new Vec3(_31, _32, _33, _34);
			this.origin = new Vec3(_41, _42, _43, _44);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000092EC File Offset: 0x000074EC
		public Vec3 TransformToParent(Vec3 v)
		{
			return new Vec3(this.rotation.s.x * v.x + this.rotation.f.x * v.y + this.rotation.u.x * v.z + this.origin.x, this.rotation.s.y * v.x + this.rotation.f.y * v.y + this.rotation.u.y * v.z + this.origin.y, this.rotation.s.z * v.x + this.rotation.f.z * v.y + this.rotation.u.z * v.z + this.origin.z, -1f);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000093FC File Offset: 0x000075FC
		public Vec3 TransformToLocal(Vec3 v)
		{
			Vec3 vec = v - this.origin;
			return new Vec3(this.rotation.s.x * vec.x + this.rotation.s.y * vec.y + this.rotation.s.z * vec.z, this.rotation.f.x * vec.x + this.rotation.f.y * vec.y + this.rotation.f.z * vec.z, this.rotation.u.x * vec.x + this.rotation.u.y * vec.y + this.rotation.u.z * vec.z, -1f);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000094F8 File Offset: 0x000076F8
		public Vec3 TransformToLocalNonUnit(Vec3 v)
		{
			Vec3 vec = v - this.origin;
			return new Vec3(this.rotation.s.x * vec.x + this.rotation.s.y * vec.y + this.rotation.s.z * vec.z, this.rotation.f.x * vec.x + this.rotation.f.y * vec.y + this.rotation.f.z * vec.z, this.rotation.u.x * vec.x + this.rotation.u.y * vec.y + this.rotation.u.z * vec.z, -1f);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000095F1 File Offset: 0x000077F1
		public bool NearlyEquals(MatrixFrame rhs, float epsilon = 1E-05f)
		{
			return this.rotation.NearlyEquals(rhs.rotation, epsilon) && this.origin.NearlyEquals(rhs.origin, epsilon);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000961C File Offset: 0x0000781C
		public Vec3 TransformToLocalNonOrthogonal(Vec3 v)
		{
			MatrixFrame matrixFrame = new MatrixFrame(this.rotation.s.x, this.rotation.s.y, this.rotation.s.z, 0f, this.rotation.f.x, this.rotation.f.y, this.rotation.f.z, 0f, this.rotation.u.x, this.rotation.u.y, this.rotation.u.z, 0f, this.origin.x, this.origin.y, this.origin.z, 1f);
			return matrixFrame.Inverse().TransformToParent(v);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00009708 File Offset: 0x00007908
		public MatrixFrame TransformToLocalNonOrthogonal(ref MatrixFrame frame)
		{
			MatrixFrame matrixFrame = new MatrixFrame(this.rotation.s.x, this.rotation.s.y, this.rotation.s.z, 0f, this.rotation.f.x, this.rotation.f.y, this.rotation.f.z, 0f, this.rotation.u.x, this.rotation.u.y, this.rotation.u.z, 0f, this.origin.x, this.origin.y, this.origin.z, 1f);
			return matrixFrame.Inverse().TransformToParent(frame);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000097F8 File Offset: 0x000079F8
		public static MatrixFrame Lerp(MatrixFrame m1, MatrixFrame m2, float alpha)
		{
			MatrixFrame result;
			result.rotation = Mat3.Lerp(m1.rotation, m2.rotation, alpha);
			result.origin = Vec3.Lerp(m1.origin, m2.origin, alpha);
			return result;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009838 File Offset: 0x00007A38
		public static MatrixFrame Slerp(MatrixFrame m1, MatrixFrame m2, float alpha)
		{
			MatrixFrame result;
			result.origin = Vec3.Lerp(m1.origin, m2.origin, alpha);
			result.rotation = Quaternion.Slerp(Quaternion.QuaternionFromMat3(m1.rotation), Quaternion.QuaternionFromMat3(m2.rotation), alpha).ToMat3;
			return result;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000988A File Offset: 0x00007A8A
		public MatrixFrame TransformToParent(MatrixFrame m)
		{
			return new MatrixFrame(this.rotation.TransformToParent(m.rotation), this.TransformToParent(m.origin));
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000098AE File Offset: 0x00007AAE
		public MatrixFrame TransformToLocal(MatrixFrame m)
		{
			return new MatrixFrame(this.rotation.TransformToLocal(m.rotation), this.TransformToLocal(m.origin));
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000098D4 File Offset: 0x00007AD4
		public Vec3 TransformToParentWithW(Vec3 _s)
		{
			return new Vec3(this.rotation.s.x * _s.x + this.rotation.f.x * _s.y + this.rotation.u.x * _s.z + this.origin.x * _s.w, this.rotation.s.y * _s.x + this.rotation.f.y * _s.y + this.rotation.u.y * _s.z + this.origin.y * _s.w, this.rotation.s.z * _s.x + this.rotation.f.z * _s.y + this.rotation.u.z * _s.z + this.origin.z * _s.w, this.rotation.s.w * _s.x + this.rotation.f.w * _s.y + this.rotation.u.w * _s.z + this.origin.w * _s.w);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00009A4E File Offset: 0x00007C4E
		public MatrixFrame GetUnitRotFrame(float removedScale)
		{
			return new MatrixFrame(this.rotation.GetUnitRotation(removedScale), this.origin);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009A67 File Offset: 0x00007C67
		public static MatrixFrame Identity
		{
			get
			{
				return new MatrixFrame(Mat3.Identity, new Vec3(0f, 0f, 0f, 1f));
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00009A8C File Offset: 0x00007C8C
		public static MatrixFrame Zero
		{
			get
			{
				return new MatrixFrame(new Mat3(Vec3.Zero, Vec3.Zero, Vec3.Zero), new Vec3(0f, 0f, 0f, 1f));
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009AC0 File Offset: 0x00007CC0
		public MatrixFrame Inverse()
		{
			this.AssertFilled();
			MatrixFrame result = default(MatrixFrame);
			float num = this[2, 2] * this[3, 3] - this[2, 3] * this[3, 2];
			float num2 = this[1, 2] * this[3, 3] - this[1, 3] * this[3, 2];
			float num3 = this[1, 2] * this[2, 3] - this[1, 3] * this[2, 2];
			float num4 = this[0, 2] * this[3, 3] - this[0, 3] * this[3, 2];
			float num5 = this[0, 2] * this[2, 3] - this[0, 3] * this[2, 2];
			float num6 = this[0, 2] * this[1, 3] - this[0, 3] * this[1, 2];
			float num7 = this[2, 1] * this[3, 3] - this[2, 3] * this[3, 1];
			float num8 = this[1, 1] * this[3, 3] - this[1, 3] * this[3, 1];
			float num9 = this[1, 1] * this[2, 3] - this[1, 3] * this[2, 1];
			float num10 = this[0, 1] * this[3, 3] - this[0, 3] * this[3, 1];
			float num11 = this[0, 1] * this[2, 3] - this[0, 3] * this[2, 1];
			float num12 = this[1, 1] * this[3, 3] - this[1, 3] * this[3, 1];
			float num13 = this[0, 1] * this[1, 3] - this[0, 3] * this[1, 1];
			float num14 = this[2, 1] * this[3, 2] - this[2, 2] * this[3, 1];
			float num15 = this[1, 1] * this[3, 2] - this[1, 2] * this[3, 1];
			float num16 = this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1];
			float num17 = this[0, 1] * this[3, 2] - this[0, 2] * this[3, 1];
			float num18 = this[0, 1] * this[2, 2] - this[0, 2] * this[2, 1];
			float num19 = this[0, 1] * this[1, 2] - this[0, 2] * this[1, 1];
			result[0, 0] = this[1, 1] * num - this[2, 1] * num2 + this[3, 1] * num3;
			result[0, 1] = -this[0, 1] * num + this[2, 1] * num4 - this[3, 1] * num5;
			result[0, 2] = this[0, 1] * num2 - this[1, 1] * num4 + this[3, 1] * num6;
			result[0, 3] = -this[0, 1] * num3 + this[1, 1] * num5 - this[2, 1] * num6;
			result[1, 0] = -this[1, 0] * num + this[2, 0] * num2 - this[3, 0] * num3;
			result[1, 1] = this[0, 0] * num - this[2, 0] * num4 + this[3, 0] * num5;
			result[1, 2] = -this[0, 0] * num2 + this[1, 0] * num4 - this[3, 0] * num6;
			result[1, 3] = this[0, 0] * num3 - this[1, 0] * num5 + this[2, 0] * num6;
			result[2, 0] = this[1, 0] * num7 - this[2, 0] * num8 + this[3, 0] * num9;
			result[2, 1] = -this[0, 0] * num7 + this[2, 0] * num10 - this[3, 0] * num11;
			result[2, 2] = this[0, 0] * num12 - this[1, 0] * num10 + this[3, 0] * num13;
			result[2, 3] = -this[0, 0] * num9 + this[1, 0] * num11 - this[2, 0] * num13;
			result[3, 0] = -this[1, 0] * num14 + this[2, 0] * num15 - this[3, 0] * num16;
			result[3, 1] = this[0, 0] * num14 - this[2, 0] * num17 + this[3, 0] * num18;
			result[3, 2] = -this[0, 0] * num15 + this[1, 0] * num17 - this[3, 0] * num19;
			result[3, 3] = this[0, 0] * num16 - this[1, 0] * num18 + this[2, 0] * num19;
			float num20 = this[0, 0] * result[0, 0] + this[1, 0] * result[0, 1] + this[2, 0] * result[0, 2] + this[3, 0] * result[0, 3];
			if (num20 != 1f)
			{
				MatrixFrame.DivideWith(ref result, num20);
			}
			return result;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A0B4 File Offset: 0x000082B4
		private static void DivideWith(ref MatrixFrame matrix, float w)
		{
			float num = 1f / w;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					ref MatrixFrame ptr = ref matrix;
					int i2 = i;
					int j2 = j;
					ptr[i2, j2] *= num;
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000A100 File Offset: 0x00008300
		public void Rotate(float radian, Vec3 axis)
		{
			float num;
			float num2;
			MathF.SinCos(radian, out num, out num2);
			MatrixFrame matrixFrame = default(MatrixFrame);
			matrixFrame[0, 0] = axis.x * axis.x * (1f - num2) + num2;
			matrixFrame[1, 0] = axis.x * axis.y * (1f - num2) - axis.z * num;
			matrixFrame[2, 0] = axis.x * axis.z * (1f - num2) + axis.y * num;
			matrixFrame[3, 0] = 0f;
			matrixFrame[0, 1] = axis.y * axis.x * (1f - num2) + axis.z * num;
			matrixFrame[1, 1] = axis.y * axis.y * (1f - num2) + num2;
			matrixFrame[2, 1] = axis.y * axis.z * (1f - num2) - axis.x * num;
			matrixFrame[3, 1] = 0f;
			matrixFrame[0, 2] = axis.x * axis.z * (1f - num2) - axis.y * num;
			matrixFrame[1, 2] = axis.y * axis.z * (1f - num2) + axis.x * num;
			matrixFrame[2, 2] = axis.z * axis.z * (1f - num2) + num2;
			matrixFrame[3, 2] = 0f;
			matrixFrame[0, 3] = 0f;
			matrixFrame[1, 3] = 0f;
			matrixFrame[2, 3] = 0f;
			matrixFrame[3, 3] = 1f;
			this.origin = this.TransformToParent(matrixFrame.origin);
			this.rotation = this.rotation.TransformToParent(matrixFrame.rotation);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public static MatrixFrame operator *(MatrixFrame m1, MatrixFrame m2)
		{
			return m1.TransformToParent(m2);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A2FE File Offset: 0x000084FE
		public static bool operator ==(MatrixFrame m1, MatrixFrame m2)
		{
			return m1.origin == m2.origin && m1.rotation == m2.rotation;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A326 File Offset: 0x00008526
		public static bool operator !=(MatrixFrame m1, MatrixFrame m2)
		{
			return m1.origin != m2.origin || m1.rotation != m2.rotation;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000A350 File Offset: 0x00008550
		public override string ToString()
		{
			string text = "MatrixFrame:\n";
			text += "Rotation:\n";
			text += this.rotation.ToString();
			return string.Concat(new object[]
			{
				text,
				"Origin: ",
				this.origin.x,
				", ",
				this.origin.y,
				", ",
				this.origin.z,
				"\n"
			});
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000A3F1 File Offset: 0x000085F1
		public override bool Equals(object obj)
		{
			return this == (MatrixFrame)obj;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000A404 File Offset: 0x00008604
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000A416 File Offset: 0x00008616
		public MatrixFrame Strafe(float a)
		{
			this.origin += this.rotation.s * a;
			return this;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000A440 File Offset: 0x00008640
		public MatrixFrame Advance(float a)
		{
			this.origin += this.rotation.f * a;
			return this;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000A46A File Offset: 0x0000866A
		public MatrixFrame Elevate(float a)
		{
			this.origin += this.rotation.u * a;
			return this;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A494 File Offset: 0x00008694
		public void Scale(Vec3 scalingVector)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			identity.rotation.s.x = scalingVector.x;
			identity.rotation.f.y = scalingVector.y;
			identity.rotation.u.z = scalingVector.z;
			this.origin = this.TransformToParent(identity.origin);
			this.rotation = this.rotation.TransformToParent(identity.rotation);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000A515 File Offset: 0x00008715
		public Vec3 GetScale()
		{
			return new Vec3(this.rotation.s.Length, this.rotation.f.Length, this.rotation.u.Length, -1f);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000A551 File Offset: 0x00008751
		public bool IsIdentity
		{
			get
			{
				return !this.origin.IsNonZero && this.rotation.IsIdentity();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000A56D File Offset: 0x0000876D
		public bool IsZero
		{
			get
			{
				return !this.origin.IsNonZero && this.rotation.IsZero();
			}
		}

		// Token: 0x17000046 RID: 70
		public float this[int i, int j]
		{
			get
			{
				float result;
				switch (i)
				{
				case 0:
					result = this.rotation.s[j];
					break;
				case 1:
					result = this.rotation.f[j];
					break;
				case 2:
					result = this.rotation.u[j];
					break;
				case 3:
					result = this.origin[j];
					break;
				default:
					throw new IndexOutOfRangeException("MatrixFrame out of bounds.");
				}
				return result;
			}
			set
			{
				switch (i)
				{
				case 0:
					this.rotation.s[j] = value;
					return;
				case 1:
					this.rotation.f[j] = value;
					return;
				case 2:
					this.rotation.u[j] = value;
					return;
				case 3:
					this.origin[j] = value;
					return;
				default:
					throw new IndexOutOfRangeException("MatrixFrame out of bounds.");
				}
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000A680 File Offset: 0x00008880
		public static MatrixFrame CreateLookAt(Vec3 position, Vec3 target, Vec3 upVector)
		{
			Vec3 vec = target - position;
			vec.Normalize();
			Vec3 vec2 = Vec3.CrossProduct(upVector, vec);
			vec2.Normalize();
			Vec3 vec3 = Vec3.CrossProduct(vec, vec2);
			float x = vec2.x;
			float x2 = vec3.x;
			float x3 = vec.x;
			float <<EMPTY_NAME>> = 0f;
			float y = vec2.y;
			float y2 = vec3.y;
			float y3 = vec.y;
			float 2 = 0f;
			float z = vec2.z;
			float z2 = vec3.z;
			float z3 = vec.z;
			float 3 = 0f;
			float 4 = -Vec3.DotProduct(vec2, position);
			float 5 = -Vec3.DotProduct(vec3, position);
			float 6 = -Vec3.DotProduct(vec, position);
			float 7 = 1f;
			return new MatrixFrame(x, x2, x3, <<EMPTY_NAME>>, y, y2, y3, 2, z, z2, z3, 3, 4, 5, 6, 7);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000A758 File Offset: 0x00008958
		public static MatrixFrame CenterFrameOfTwoPoints(Vec3 p1, Vec3 p2, Vec3 upVector)
		{
			MatrixFrame matrixFrame;
			matrixFrame.origin = (p1 + p2) * 0.5f;
			matrixFrame.rotation.s = p2 - p1;
			matrixFrame.rotation.s.Normalize();
			if (MathF.Abs(Vec3.DotProduct(matrixFrame.rotation.s, upVector)) > 0.95f)
			{
				upVector = new Vec3(0f, 1f, 0f, -1f);
			}
			matrixFrame.rotation.u = upVector;
			matrixFrame.rotation.f = Vec3.CrossProduct(matrixFrame.rotation.u, matrixFrame.rotation.s);
			matrixFrame.rotation.f.Normalize();
			matrixFrame.rotation.u = Vec3.CrossProduct(matrixFrame.rotation.s, matrixFrame.rotation.f);
			matrixFrame.Fill();
			return matrixFrame;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A850 File Offset: 0x00008A50
		public void Fill()
		{
			this.rotation.s.w = 0f;
			this.rotation.f.w = 0f;
			this.rotation.u.w = 0f;
			this.origin.w = 1f;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000A8AC File Offset: 0x00008AAC
		private void AssertFilled()
		{
		}

		// Token: 0x04000100 RID: 256
		public Mat3 rotation;

		// Token: 0x04000101 RID: 257
		public Vec3 origin;
	}
}
