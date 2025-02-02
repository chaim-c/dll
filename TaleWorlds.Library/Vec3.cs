using System;
using System.Numerics;
using System.Xml.Serialization;

namespace TaleWorlds.Library
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	public struct Vec3
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00011E3B File Offset: 0x0001003B
		public float X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00011E43 File Offset: 0x00010043
		public float Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00011E4B File Offset: 0x0001004B
		public float Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00011E53 File Offset: 0x00010053
		public Vec3(float x = 0f, float y = 0f, float z = 0f, float w = -1f)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00011E72 File Offset: 0x00010072
		public Vec3(Vec3 c, float w = -1f)
		{
			this.x = c.x;
			this.y = c.y;
			this.z = c.z;
			this.w = w;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00011E9F File Offset: 0x0001009F
		public Vec3(Vec2 xy, float z = 0f, float w = -1f)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00011EC7 File Offset: 0x000100C7
		public Vec3(Vector3 vector3)
		{
			this = new Vec3(vector3.X, vector3.Y, vector3.Z, -1f);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00011EE6 File Offset: 0x000100E6
		public static Vec3 Abs(Vec3 vec)
		{
			return new Vec3(MathF.Abs(vec.x), MathF.Abs(vec.y), MathF.Abs(vec.z), -1f);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00011F13 File Offset: 0x00010113
		public static explicit operator Vector3(Vec3 vec3)
		{
			return new Vector3(vec3.x, vec3.y, vec3.z);
		}

		// Token: 0x1700009B RID: 155
		public float this[int i]
		{
			get
			{
				switch (i)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				case 3:
					return this.w;
				default:
					throw new IndexOutOfRangeException("Vec3 out of bounds.");
				}
			}
			set
			{
				switch (i)
				{
				case 0:
					this.x = value;
					return;
				case 1:
					this.y = value;
					return;
				case 2:
					this.z = value;
					return;
				case 3:
					this.w = value;
					return;
				default:
					throw new IndexOutOfRangeException("Vec3 out of bounds.");
				}
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00011FBB File Offset: 0x000101BB
		public static float DotProduct(Vec3 v1, Vec3 v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00011FE6 File Offset: 0x000101E6
		public static Vec3 Lerp(Vec3 v1, Vec3 v2, float alpha)
		{
			return v1 * (1f - alpha) + v2 * alpha;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00012004 File Offset: 0x00010204
		public static Vec3 Slerp(Vec3 start, Vec3 end, float percent)
		{
			float num = Vec3.DotProduct(start, end);
			num = MBMath.ClampFloat(num, -1f, 1f);
			float num2 = MathF.Acos(num) * percent;
			Vec3 v = end - start * num;
			v.Normalize();
			return start * MathF.Cos(num2) + v * MathF.Sin(num2);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00012066 File Offset: 0x00010266
		public static Vec3 Vec3Max(Vec3 v1, Vec3 v2)
		{
			return new Vec3(MathF.Max(v1.x, v2.x), MathF.Max(v1.y, v2.y), MathF.Max(v1.z, v2.z), -1f);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000120A5 File Offset: 0x000102A5
		public static Vec3 Vec3Min(Vec3 v1, Vec3 v2)
		{
			return new Vec3(MathF.Min(v1.x, v2.x), MathF.Min(v1.y, v2.y), MathF.Min(v1.z, v2.z), -1f);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000120E4 File Offset: 0x000102E4
		public static Vec3 CrossProduct(Vec3 va, Vec3 vb)
		{
			return new Vec3(va.y * vb.z - va.z * vb.y, va.z * vb.x - va.x * vb.z, va.x * vb.y - va.y * vb.x, -1f);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001214C File Offset: 0x0001034C
		public static Vec3 operator -(Vec3 v)
		{
			return new Vec3(-v.x, -v.y, -v.z, -1f);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001216D File Offset: 0x0001036D
		public static Vec3 operator +(Vec3 v1, Vec3 v2)
		{
			return new Vec3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, -1f);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000121A0 File Offset: 0x000103A0
		public static Vec3 operator -(Vec3 v1, Vec3 v2)
		{
			return new Vec3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, -1f);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000121D3 File Offset: 0x000103D3
		public static Vec3 operator *(Vec3 v, float f)
		{
			return new Vec3(v.x * f, v.y * f, v.z * f, -1f);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000121F7 File Offset: 0x000103F7
		public static Vec3 operator *(float f, Vec3 v)
		{
			return new Vec3(v.x * f, v.y * f, v.z * f, -1f);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001221C File Offset: 0x0001041C
		public static Vec3 operator *(Vec3 v, MatrixFrame frame)
		{
			return new Vec3(frame.rotation.s.x * v.x + frame.rotation.f.x * v.y + frame.rotation.u.x * v.z + frame.origin.x * v.w, frame.rotation.s.y * v.x + frame.rotation.f.y * v.y + frame.rotation.u.y * v.z + frame.origin.y * v.w, frame.rotation.s.z * v.x + frame.rotation.f.z * v.y + frame.rotation.u.z * v.z + frame.origin.z * v.w, frame.rotation.s.w * v.x + frame.rotation.f.w * v.y + frame.rotation.u.w * v.z + frame.origin.w * v.w);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00012396 File Offset: 0x00010596
		public static Vec3 operator /(Vec3 v, float f)
		{
			f = 1f / f;
			return new Vec3(v.x * f, v.y * f, v.z * f, -1f);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000123C3 File Offset: 0x000105C3
		public static bool operator ==(Vec3 v1, Vec3 v2)
		{
			return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000123F1 File Offset: 0x000105F1
		public static bool operator !=(Vec3 v1, Vec3 v2)
		{
			return v1.x != v2.x || v1.y != v2.y || v1.z != v2.z;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00012422 File Offset: 0x00010622
		public float Length
		{
			get
			{
				return MathF.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00012452 File Offset: 0x00010652
		public float LengthSquared
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00012480 File Offset: 0x00010680
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && (((Vec3)obj).x == this.x && ((Vec3)obj).y == this.y) && ((Vec3)obj).z == this.z;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000124EA File Offset: 0x000106EA
		public override int GetHashCode()
		{
			return (int)(1001f * this.x + 10039f * this.y + 117f * this.z);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00012514 File Offset: 0x00010714
		public bool IsValid
		{
			get
			{
				return !float.IsNaN(this.x) && !float.IsNaN(this.y) && !float.IsNaN(this.z) && !float.IsInfinity(this.x) && !float.IsInfinity(this.y) && !float.IsInfinity(this.z);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00012574 File Offset: 0x00010774
		public bool IsValidXYZW
		{
			get
			{
				return !float.IsNaN(this.x) && !float.IsNaN(this.y) && !float.IsNaN(this.z) && !float.IsNaN(this.w) && !float.IsInfinity(this.x) && !float.IsInfinity(this.y) && !float.IsInfinity(this.z) && !float.IsInfinity(this.w);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x000125EC File Offset: 0x000107EC
		public bool IsUnit
		{
			get
			{
				float lengthSquared = this.LengthSquared;
				return lengthSquared > 0.98010004f && lengthSquared < 1.0201f;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00012612 File Offset: 0x00010812
		public bool IsNonZero
		{
			get
			{
				return this.x != 0f || this.y != 0f || this.z != 0f;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00012640 File Offset: 0x00010840
		public Vec3 NormalizedCopy()
		{
			Vec3 result = this;
			result.Normalize();
			return result;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00012660 File Offset: 0x00010860
		public float Normalize()
		{
			float length = this.Length;
			if (length > 1E-05f)
			{
				float num = 1f / length;
				this.x *= num;
				this.y *= num;
				this.z *= num;
			}
			else
			{
				this.x = 0f;
				this.y = 1f;
				this.z = 0f;
			}
			return length;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000126D4 File Offset: 0x000108D4
		public Vec3 ClampedCopy(float min, float max)
		{
			Vec3 vec = this;
			vec.x = MathF.Clamp(vec.x, min, max);
			vec.y = MathF.Clamp(vec.y, min, max);
			vec.z = MathF.Clamp(vec.z, min, max);
			return vec;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00012728 File Offset: 0x00010928
		public Vec3 ClampedCopy(float min, float max, out bool valueClamped)
		{
			Vec3 result = this;
			valueClamped = false;
			for (int i = 0; i < 3; i++)
			{
				if (result[i] < min)
				{
					result[i] = min;
					valueClamped = true;
				}
				else if (result[i] > max)
				{
					result[i] = max;
					valueClamped = true;
				}
			}
			return result;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001277C File Offset: 0x0001097C
		public void NormalizeWithoutChangingZ()
		{
			this.z = MBMath.ClampFloat(this.z, -0.99999f, 0.99999f);
			float length = this.AsVec2.Length;
			float num = MathF.Sqrt(1f - this.z * this.z);
			if (length < num - 1E-07f || length > num + 1E-07f)
			{
				if (length > 1E-09f)
				{
					float num2 = num / length;
					this.x *= num2;
					this.y *= num2;
					return;
				}
				this.x = 0f;
				this.y = num;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001281C File Offset: 0x00010A1C
		public bool NearlyEquals(Vec3 v, float epsilon = 1E-05f)
		{
			return MathF.Abs(this.x - v.x) < epsilon && MathF.Abs(this.y - v.y) < epsilon && MathF.Abs(this.z - v.z) < epsilon;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001286C File Offset: 0x00010A6C
		public void RotateAboutX(float a)
		{
			float num;
			float num2;
			MathF.SinCos(a, out num, out num2);
			float num3 = this.y * num2 - this.z * num;
			this.z = this.z * num2 + this.y * num;
			this.y = num3;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000128B4 File Offset: 0x00010AB4
		public void RotateAboutY(float a)
		{
			float num;
			float num2;
			MathF.SinCos(a, out num, out num2);
			float num3 = this.x * num2 + this.z * num;
			this.z = this.z * num2 - this.x * num;
			this.x = num3;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000128FC File Offset: 0x00010AFC
		public void RotateAboutZ(float a)
		{
			float num;
			float num2;
			MathF.SinCos(a, out num, out num2);
			float num3 = this.x * num2 - this.y * num;
			this.y = this.y * num2 + this.x * num;
			this.x = num3;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00012944 File Offset: 0x00010B44
		public Vec3 RotateAboutAnArbitraryVector(Vec3 vec, float a)
		{
			float num = vec.x;
			float num2 = vec.y;
			float num3 = vec.z;
			float num4 = num * this.x;
			float num5 = num * this.y;
			float num6 = num * this.z;
			float num7 = num2 * this.x;
			float num8 = num2 * this.y;
			float num9 = num2 * this.z;
			float num10 = num3 * this.x;
			float num11 = num3 * this.y;
			float num12 = num3 * this.z;
			float num13;
			float num14;
			MathF.SinCos(a, out num13, out num14);
			return new Vec3
			{
				x = num * (num4 + num8 + num12) + (this.x * (num2 * num2 + num3 * num3) - num * (num8 + num12)) * num14 + (-num11 + num9) * num13,
				y = num2 * (num4 + num8 + num12) + (this.y * (num * num + num3 * num3) - num2 * (num4 + num12)) * num14 + (num10 - num6) * num13,
				z = num3 * (num4 + num8 + num12) + (this.z * (num * num + num2 * num2) - num3 * (num4 + num8)) * num14 + (-num7 + num5) * num13
			};
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00012A6C File Offset: 0x00010C6C
		public Vec3 Reflect(Vec3 normal)
		{
			return this - normal * (2f * Vec3.DotProduct(this, normal));
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00012A91 File Offset: 0x00010C91
		public Vec3 ProjectOnUnitVector(Vec3 ov)
		{
			return ov * (this.x * ov.x + this.y * ov.y + this.z * ov.z);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00012AC4 File Offset: 0x00010CC4
		public float DistanceSquared(Vec3 v)
		{
			return (v.x - this.x) * (v.x - this.x) + (v.y - this.y) * (v.y - this.y) + (v.z - this.z) * (v.z - this.z);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00012B24 File Offset: 0x00010D24
		public float Distance(Vec3 v)
		{
			return MathF.Sqrt((v.x - this.x) * (v.x - this.x) + (v.y - this.y) * (v.y - this.y) + (v.z - this.z) * (v.z - this.z));
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00012B89 File Offset: 0x00010D89
		public static float AngleBetweenTwoVectors(Vec3 v1, Vec3 v2)
		{
			return MathF.Acos(MathF.Clamp(Vec3.DotProduct(v1, v2) / (v1.Length * v2.Length), -1f, 1f));
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00012BB6 File Offset: 0x00010DB6
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00012BC9 File Offset: 0x00010DC9
		public Vec2 AsVec2
		{
			get
			{
				return new Vec2(this.x, this.y);
			}
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.x,
				", ",
				this.y,
				", ",
				this.z,
				")"
			});
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00012C48 File Offset: 0x00010E48
		public uint ToARGB
		{
			get
			{
				uint a = (uint)(this.w * 256f);
				uint a2 = (uint)(this.x * 256f);
				uint a3 = (uint)(this.y * 256f);
				uint a4 = (uint)(this.z * 256f);
				return MathF.Min(a, 255U) << 24 | MathF.Min(a2, 255U) << 16 | MathF.Min(a3, 255U) << 8 | MathF.Min(a4, 255U);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00012CC2 File Offset: 0x00010EC2
		public float RotationZ
		{
			get
			{
				return MathF.Atan2(-this.x, this.y);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00012CD6 File Offset: 0x00010ED6
		public float RotationX
		{
			get
			{
				return MathF.Atan2(this.z, MathF.Sqrt(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00012D04 File Offset: 0x00010F04
		public static Vec3 Parse(string input)
		{
			input = input.Replace(" ", "");
			string[] array = input.Split(new char[]
			{
				','
			});
			if (array.Length < 3 || array.Length > 4)
			{
				throw new ArgumentOutOfRangeException();
			}
			float num = float.Parse(array[0]);
			float num2 = float.Parse(array[1]);
			float num3 = float.Parse(array[2]);
			float num4 = (array.Length == 4) ? float.Parse(array[3]) : -1f;
			return new Vec3(num, num2, num3, num4);
		}

		// Token: 0x04000199 RID: 409
		[XmlAttribute]
		public float x;

		// Token: 0x0400019A RID: 410
		[XmlAttribute]
		public float y;

		// Token: 0x0400019B RID: 411
		[XmlAttribute]
		public float z;

		// Token: 0x0400019C RID: 412
		[XmlAttribute]
		public float w;

		// Token: 0x0400019D RID: 413
		public static readonly Vec3 Side = new Vec3(1f, 0f, 0f, -1f);

		// Token: 0x0400019E RID: 414
		public static readonly Vec3 Forward = new Vec3(0f, 1f, 0f, -1f);

		// Token: 0x0400019F RID: 415
		public static readonly Vec3 Up = new Vec3(0f, 0f, 1f, -1f);

		// Token: 0x040001A0 RID: 416
		public static readonly Vec3 One = new Vec3(1f, 1f, 1f, -1f);

		// Token: 0x040001A1 RID: 417
		public static readonly Vec3 Zero = new Vec3(0f, 0f, 0f, -1f);

		// Token: 0x040001A2 RID: 418
		public static readonly Vec3 Invalid = new Vec3(float.NaN, float.NaN, float.NaN, -1f);

		// Token: 0x020000E4 RID: 228
		public struct StackArray8Vec3
		{
			// Token: 0x170000F8 RID: 248
			public Vec3 this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return this._element0;
					case 1:
						return this._element1;
					case 2:
						return this._element2;
					case 3:
						return this._element3;
					case 4:
						return this._element4;
					case 5:
						return this._element5;
					case 6:
						return this._element6;
					case 7:
						return this._element7;
					default:
						Debug.FailedAssert("Index out of range.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Vec3.cs", "Item", 41);
						return Vec3.Zero;
					}
				}
				set
				{
					switch (index)
					{
					case 0:
						this._element0 = value;
						return;
					case 1:
						this._element1 = value;
						return;
					case 2:
						this._element2 = value;
						return;
					case 3:
						this._element3 = value;
						return;
					case 4:
						this._element4 = value;
						return;
					case 5:
						this._element5 = value;
						return;
					case 6:
						this._element6 = value;
						return;
					case 7:
						this._element7 = value;
						return;
					default:
						Debug.FailedAssert("Index out of range.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Vec3.cs", "Item", 59);
						return;
					}
				}
			}

			// Token: 0x040002C3 RID: 707
			private Vec3 _element0;

			// Token: 0x040002C4 RID: 708
			private Vec3 _element1;

			// Token: 0x040002C5 RID: 709
			private Vec3 _element2;

			// Token: 0x040002C6 RID: 710
			private Vec3 _element3;

			// Token: 0x040002C7 RID: 711
			private Vec3 _element4;

			// Token: 0x040002C8 RID: 712
			private Vec3 _element5;

			// Token: 0x040002C9 RID: 713
			private Vec3 _element6;

			// Token: 0x040002CA RID: 714
			private Vec3 _element7;

			// Token: 0x040002CB RID: 715
			public const int Length = 8;
		}
	}
}
