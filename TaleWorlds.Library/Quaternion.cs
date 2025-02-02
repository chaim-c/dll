using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200007D RID: 125
	[Serializable]
	public struct Quaternion
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0000E1E5 File Offset: 0x0000C3E5
		public Quaternion(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000E204 File Offset: 0x0000C404
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000E216 File Offset: 0x0000C416
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000E22C File Offset: 0x0000C42C
		public static bool operator ==(Quaternion a, Quaternion b)
		{
			return a == b || (a != null && b != null && (a.X == b.X && a.Y == b.Y && a.Z == b.Z) && a.W == b.W);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000E295 File Offset: 0x0000C495
		public static bool operator !=(Quaternion a, Quaternion b)
		{
			return !(a == b);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000E2A1 File Offset: 0x0000C4A1
		public static Quaternion operator +(Quaternion a, Quaternion b)
		{
			return new Quaternion(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		public static Quaternion operator -(Quaternion a, Quaternion b)
		{
			return new Quaternion(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000E317 File Offset: 0x0000C517
		public static Quaternion operator *(Quaternion a, float b)
		{
			return new Quaternion(a.X * b, a.Y * b, a.Z * b, a.W * b);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000E33E File Offset: 0x0000C53E
		public static Quaternion operator *(float s, Quaternion v)
		{
			return v * s;
		}

		// Token: 0x1700006E RID: 110
		public float this[int i]
		{
			get
			{
				float result;
				switch (i)
				{
				case 0:
					result = this.W;
					break;
				case 1:
					result = this.X;
					break;
				case 2:
					result = this.Y;
					break;
				case 3:
					result = this.Z;
					break;
				default:
					throw new IndexOutOfRangeException("Quaternion out of bounds.");
				}
				return result;
			}
			set
			{
				switch (i)
				{
				case 0:
					this.W = value;
					return;
				case 1:
					this.X = value;
					return;
				case 2:
					this.Y = value;
					return;
				case 3:
					this.Z = value;
					return;
				default:
					throw new IndexOutOfRangeException("Quaternion out of bounds.");
				}
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public float Normalize()
		{
			float num = MathF.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W);
			if (num <= 1E-07f)
			{
				this.X = 0f;
				this.Y = 0f;
				this.Z = 0f;
				this.W = 1f;
			}
			else
			{
				float num2 = 1f / num;
				this.X *= num2;
				this.Y *= num2;
				this.Z *= num2;
				this.W *= num2;
			}
			return num;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
		public float SafeNormalize()
		{
			double num = Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z + (double)this.W * (double)this.W);
			if (num <= 1E-07)
			{
				this.X = 0f;
				this.Y = 0f;
				this.Z = 0f;
				this.W = 1f;
			}
			else
			{
				this.X = (float)((double)this.X / num);
				this.Y = (float)((double)this.Y / num);
				this.Z = (float)((double)this.Z / num);
				this.W = (float)((double)this.W / num);
			}
			return (float)num;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000E588 File Offset: 0x0000C788
		public float NormalizeWeighted()
		{
			float num = this.X * this.X + this.Y * this.Y + this.Z * this.Z;
			if (num <= 1E-09f)
			{
				this.X = 1f;
				this.Y = 0f;
				this.Z = 0f;
				this.W = 0f;
			}
			else
			{
				this.W = MathF.Sqrt(1f - num);
			}
			return num;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000E608 File Offset: 0x0000C808
		public void SetToRotationX(float angle)
		{
			float x;
			float w;
			MathF.SinCos(angle * 0.5f, out x, out w);
			this.X = x;
			this.Y = 0f;
			this.Z = 0f;
			this.W = w;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000E64C File Offset: 0x0000C84C
		public void SetToRotationY(float angle)
		{
			float y;
			float w;
			MathF.SinCos(angle * 0.5f, out y, out w);
			this.X = 0f;
			this.Y = y;
			this.Z = 0f;
			this.W = w;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000E690 File Offset: 0x0000C890
		public void SetToRotationZ(float angle)
		{
			float z;
			float w;
			MathF.SinCos(angle * 0.5f, out z, out w);
			this.X = 0f;
			this.Y = 0f;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000E6D1 File Offset: 0x0000C8D1
		public void Flip()
		{
			this.X = -this.X;
			this.Y = -this.Y;
			this.Z = -this.Z;
			this.W = -this.W;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000E707 File Offset: 0x0000C907
		public bool IsIdentity
		{
			get
			{
				return this.X == 0f && this.Y == 0f && this.Z == 0f && this.W == 1f;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000E740 File Offset: 0x0000C940
		public bool IsUnit
		{
			get
			{
				return MBMath.ApproximatelyEquals(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W, 1f, 0.2f);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000E793 File Offset: 0x0000C993
		public static Quaternion Identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		public Quaternion TransformToParent(Quaternion q)
		{
			return new Quaternion
			{
				X = this.Y * q.Z - this.Z * q.Y + this.W * q.X + this.X * q.W,
				Y = this.Z * q.X - this.X * q.Z + this.W * q.Y + this.Y * q.W,
				Z = this.X * q.Y - this.Y * q.X + this.W * q.Z + this.Z * q.W,
				W = this.W * q.W - (this.X * q.X + this.Y * q.Y + this.Z * q.Z)
			};
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public Quaternion TransformToLocal(Quaternion q)
		{
			return new Quaternion
			{
				X = this.Z * q.Y - this.Y * q.Z + this.W * q.X - this.X * q.W,
				Y = this.X * q.Z - this.Z * q.X + this.W * q.Y - this.Y * q.W,
				Z = this.Y * q.X - this.X * q.Y + this.W * q.Z - this.Z * q.W,
				W = this.W * q.W + (this.X * q.X + this.Y * q.Y + this.Z * q.Z)
			};
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		public Quaternion TransformToLocalWithoutNormalize(Quaternion q)
		{
			return new Quaternion
			{
				X = this.Z * q.Y - this.Y * q.Z + this.W * q.X - this.X * q.W,
				Y = this.X * q.Z - this.Z * q.X + this.W * q.Y - this.Y * q.W,
				Z = this.Y * q.X - this.X * q.Y + this.W * q.Z - this.Z * q.W,
				W = this.W * q.W + (this.X * q.X + this.Y * q.Y + this.Z * q.Z)
			};
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public static Quaternion Slerp(Quaternion from, Quaternion to, float t)
		{
			float num = from.Dotp4(to);
			float num2;
			if (num < 0f)
			{
				num = -num;
				num2 = -1f;
			}
			else
			{
				num2 = 1f;
			}
			float num6;
			float num7;
			if (0.9 >= (double)num)
			{
				float num3 = MathF.Acos(num);
				float num4 = 1f / MathF.Sin(num3);
				float num5 = t * num3;
				num6 = MathF.Sin(num3 - num5) * num4;
				num7 = MathF.Sin(num5) * num4;
			}
			else
			{
				num6 = 1f - t;
				num7 = t;
			}
			num7 *= num2;
			return new Quaternion
			{
				X = num6 * from.X + num7 * to.X,
				Y = num6 * from.Y + num7 * to.Y,
				Z = num6 * from.Z + num7 * to.Z,
				W = num6 * from.W + num7 * to.W
			};
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		public static Quaternion Lerp(Quaternion from, Quaternion to, float t)
		{
			float num = from.Dotp4(to);
			float num2 = 1f - t;
			float num3;
			if (num < 0f)
			{
				num = -num;
				num3 = -t;
			}
			else
			{
				num3 = t;
			}
			return new Quaternion
			{
				X = num2 * from.X + num3 * to.X,
				Y = num2 * from.Y + num3 * to.Y,
				Z = num2 * from.Z + num3 * to.Z,
				W = num2 * from.W + num3 * to.W
			};
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000EC70 File Offset: 0x0000CE70
		public static Mat3 Mat3FromQuaternion(Quaternion quat)
		{
			Mat3 result = default(Mat3);
			float num = quat.X + quat.X;
			float num2 = quat.Y + quat.Y;
			float num3 = quat.Z + quat.Z;
			float num4 = quat.X * num;
			float num5 = quat.X * num2;
			float num6 = quat.X * num3;
			float num7 = quat.Y * num2;
			float num8 = quat.Y * num3;
			float num9 = quat.Z * num3;
			float num10 = quat.W * num;
			float num11 = quat.W * num2;
			float num12 = quat.W * num3;
			result.s.x = 1f - (num7 + num9);
			result.s.y = num5 + num12;
			result.s.z = num6 - num11;
			result.f.x = num5 - num12;
			result.f.y = 1f - (num4 + num9);
			result.f.z = num8 + num10;
			result.u.x = num6 + num11;
			result.u.y = num8 - num10;
			result.u.z = 1f - (num4 + num7);
			return result;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		public static Quaternion QuaternionFromEulerAngles(float yaw, float pitch, float roll)
		{
			float num = yaw * 0.017453292f;
			float num2 = pitch * 0.017453292f;
			float num3 = roll * 0.017453292f;
			float num4 = MathF.Cos(num * 0.5f);
			float num5 = MathF.Sin(num * 0.5f);
			float num6 = MathF.Cos(num2 * 0.5f);
			float num7 = MathF.Sin(num2 * 0.5f);
			float num8 = MathF.Cos(num3 * 0.5f);
			float num9 = MathF.Sin(num3 * 0.5f);
			float w = num8 * num6 * num4 + num9 * num7 * num5;
			float x = num9 * num6 * num4 - num8 * num7 * num5;
			float y = num8 * num7 * num4 + num9 * num6 * num5;
			float z = num8 * num6 * num5 - num9 * num7 * num4;
			return new Quaternion(x, y, z, w);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000EE84 File Offset: 0x0000D084
		public static Quaternion QuaternionFromMat3(Mat3 m)
		{
			Quaternion result = default(Quaternion);
			float num;
			if (m[2][2] < 0f)
			{
				if (m[0][0] > m[1][1])
				{
					num = 1f + m[0][0] - m[1][1] - m[2][2];
					result.W = m[1][2] - m[2][1];
					result.X = num;
					result.Y = m[0][1] + m[1][0];
					result.Z = m[2][0] + m[0][2];
				}
				else
				{
					num = 1f - m[0][0] + m[1][1] - m[2][2];
					result.W = m[2][0] - m[0][2];
					result.X = m[0][1] + m[1][0];
					result.Y = num;
					result.Z = m[1][2] + m[2][1];
				}
			}
			else if (m[0][0] < -m[1][1])
			{
				num = 1f - m[0][0] - m[1][1] + m[2][2];
				result.W = m[0][1] - m[1][0];
				result.X = m[2][0] + m[0][2];
				result.Y = m[1][2] + m[2][1];
				result.Z = num;
			}
			else
			{
				num = 1f + m[0][0] + m[1][1] + m[2][2];
				result.W = num;
				result.X = m[1][2] - m[2][1];
				result.Y = m[2][0] - m[0][2];
				result.Z = m[0][1] - m[1][0];
			}
			float num2 = 0.5f / MathF.Sqrt(num);
			result.W *= num2;
			result.X *= num2;
			result.Y *= num2;
			result.Z *= num2;
			return result;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000F258 File Offset: 0x0000D458
		public static void AxisAngleFromQuaternion(out Vec3 axis, out float angle, Quaternion quat)
		{
			axis = default(Vec3);
			float w = quat.W;
			if (w > 0.9999999f)
			{
				axis.x = 1f;
				axis.y = 0f;
				axis.z = 0f;
				angle = 0f;
				return;
			}
			float num = MathF.Sqrt(1f - w * w);
			if (num < 0.0001f)
			{
				num = 1f;
			}
			axis.x = quat.X / num;
			axis.y = quat.Y / num;
			axis.z = quat.Z / num;
			angle = MathF.Acos(w) * 2f;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		public static Quaternion QuaternionFromAxisAngle(Vec3 axis, float angle)
		{
			Quaternion result = default(Quaternion);
			float num;
			float w;
			MathF.SinCos(angle * 0.5f, out num, out w);
			result.X = axis.x * num;
			result.Y = axis.y * num;
			result.Z = axis.z * num;
			result.W = w;
			return result;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000F358 File Offset: 0x0000D558
		public static Vec3 EulerAngleFromQuaternion(Quaternion quat)
		{
			float w = quat.W;
			float x = quat.X;
			float y = quat.Y;
			float z = quat.Z;
			float num = w * w;
			float num2 = x * x;
			float num3 = y * y;
			float num4 = z * z;
			return new Vec3
			{
				z = MathF.Atan2(2f * (x * y + z * w), num2 - num3 - num4 + num),
				x = MathF.Atan2(2f * (y * z + x * w), -num2 - num3 + num4 + num),
				y = MathF.Asin(-2f * (x * z - y * w))
			};
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000F404 File Offset: 0x0000D604
		public static Quaternion FindShortestArcAsQuaternion(Vec3 v0, Vec3 v1)
		{
			Vec3 vec = Vec3.CrossProduct(v0, v1);
			float num = Vec3.DotProduct(v0, v1);
			if ((double)num < -0.9999900000002526)
			{
				Vec3 vec2 = default(Vec3);
				if (MathF.Abs(v0.z) < 0.8f)
				{
					vec2 = Vec3.CrossProduct(v0, new Vec3(0f, 0f, 1f, -1f));
				}
				else
				{
					vec2 = Vec3.CrossProduct(v0, new Vec3(1f, 0f, 0f, -1f));
				}
				vec2.Normalize();
				return new Quaternion(vec2.x, vec2.y, vec2.z, 0f);
			}
			float num2 = MathF.Sqrt((1f + num) * 2f);
			float num3 = 1f / num2;
			return new Quaternion(vec.x * num3, vec.y * num3, vec.z * num3, num2 * 0.5f);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000F4F6 File Offset: 0x0000D6F6
		public float Dotp4(Quaternion q2)
		{
			return this.X * q2.X + this.Y * q2.Y + this.Z * q2.Z + this.W * q2.W;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000F52F File Offset: 0x0000D72F
		public Mat3 ToMat3
		{
			get
			{
				return Quaternion.Mat3FromQuaternion(this);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000F53C File Offset: 0x0000D73C
		public bool InverseDirection(Quaternion q2)
		{
			return this.Dotp4(q2) < 0f;
		}

		// Token: 0x04000142 RID: 322
		public float W;

		// Token: 0x04000143 RID: 323
		public float X;

		// Token: 0x04000144 RID: 324
		public float Y;

		// Token: 0x04000145 RID: 325
		public float Z;
	}
}
