using System;
using System.Numerics;

namespace TaleWorlds.Library
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	public struct Vec2
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00011362 File Offset: 0x0000F562
		public float X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001136A File Offset: 0x0000F56A
		public float Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00011372 File Offset: 0x0000F572
		public Vec2(float a, float b)
		{
			this.x = a;
			this.y = b;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00011382 File Offset: 0x0000F582
		public Vec2(Vec2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001139C File Offset: 0x0000F59C
		public Vec2(Vector2 v)
		{
			this.x = v.X;
			this.y = v.Y;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000113B6 File Offset: 0x0000F5B6
		public Vec3 ToVec3(float z = 0f)
		{
			return new Vec3(this.x, this.y, z, -1f);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000113CF File Offset: 0x0000F5CF
		public static explicit operator Vector2(Vec2 vec2)
		{
			return new Vector2(vec2.x, vec2.y);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000113E2 File Offset: 0x0000F5E2
		public static implicit operator Vec2(Vector2 vec2)
		{
			return new Vec2(vec2.X, vec2.Y);
		}

		// Token: 0x17000091 RID: 145
		public float this[int i]
		{
			get
			{
				if (i == 0)
				{
					return this.x;
				}
				if (i != 1)
				{
					throw new IndexOutOfRangeException("Vec2 out of bounds.");
				}
				return this.y;
			}
			set
			{
				if (i == 0)
				{
					this.x = value;
					return;
				}
				if (i != 1)
				{
					return;
				}
				this.y = value;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00011434 File Offset: 0x0000F634
		public float Normalize()
		{
			float length = this.Length;
			if (length > 1E-05f)
			{
				this.x /= length;
				this.y /= length;
			}
			else
			{
				this.x = 0f;
				this.y = 1f;
			}
			return length;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00011488 File Offset: 0x0000F688
		public Vec2 Normalized()
		{
			Vec2 result = this;
			result.Normalize();
			return result;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000114A8 File Offset: 0x0000F6A8
		public static WindingOrder GetWindingOrder(Vec2 first, Vec2 second, Vec2 third)
		{
			Vec2 vb = second - first;
			float num = Vec2.CCW(third - second, vb);
			if (num > 0f)
			{
				return WindingOrder.Ccw;
			}
			if (num < 0f)
			{
				return WindingOrder.Cw;
			}
			return WindingOrder.None;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000114E0 File Offset: 0x0000F6E0
		public static float CCW(Vec2 va, Vec2 vb)
		{
			return va.x * vb.y - va.y * vb.x;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x000114FD File Offset: 0x0000F6FD
		public float Length
		{
			get
			{
				return MathF.Sqrt(this.x * this.x + this.y * this.y);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001151F File Offset: 0x0000F71F
		public float LengthSquared
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001153C File Offset: 0x0000F73C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && ((Vec2)obj).x == this.x && ((Vec2)obj).y == this.y;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00011593 File Offset: 0x0000F793
		public override int GetHashCode()
		{
			return (int)(1001f * this.x + 10039f * this.y);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000115AF File Offset: 0x0000F7AF
		public static bool operator ==(Vec2 v1, Vec2 v2)
		{
			return v1.x == v2.x && v1.y == v2.y;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000115CF File Offset: 0x0000F7CF
		public static bool operator !=(Vec2 v1, Vec2 v2)
		{
			return v1.x != v2.x || v1.y != v2.y;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000115F2 File Offset: 0x0000F7F2
		public static Vec2 operator -(Vec2 v)
		{
			return new Vec2(-v.x, -v.y);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00011607 File Offset: 0x0000F807
		public static Vec2 operator +(Vec2 v1, Vec2 v2)
		{
			return new Vec2(v1.x + v2.x, v1.y + v2.y);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00011628 File Offset: 0x0000F828
		public static Vec2 operator -(Vec2 v1, Vec2 v2)
		{
			return new Vec2(v1.x - v2.x, v1.y - v2.y);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00011649 File Offset: 0x0000F849
		public static Vec2 operator *(Vec2 v, float f)
		{
			return new Vec2(v.x * f, v.y * f);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00011660 File Offset: 0x0000F860
		public static Vec2 operator *(float f, Vec2 v)
		{
			return new Vec2(v.x * f, v.y * f);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00011677 File Offset: 0x0000F877
		public static Vec2 operator /(float f, Vec2 v)
		{
			return new Vec2(v.x / f, v.y / f);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001168E File Offset: 0x0000F88E
		public static Vec2 operator /(Vec2 v, float f)
		{
			return new Vec2(v.x / f, v.y / f);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public bool IsUnit()
		{
			float length = this.Length;
			return (double)length > 0.95 && (double)length < 1.05;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000116D8 File Offset: 0x0000F8D8
		public bool IsNonZero()
		{
			float num = 1E-05f;
			return this.x > num || this.x < -num || this.y > num || this.y < -num;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00011713 File Offset: 0x0000F913
		public bool NearlyEquals(Vec2 v, float epsilon = 1E-05f)
		{
			return MathF.Abs(this.x - v.x) < epsilon && MathF.Abs(this.y - v.y) < epsilon;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00011744 File Offset: 0x0000F944
		public void RotateCCW(float angleInRadians)
		{
			float num;
			float num2;
			MathF.SinCos(angleInRadians, out num, out num2);
			float num3 = this.x * num2 - this.y * num;
			this.y = this.y * num2 + this.x * num;
			this.x = num3;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001178B File Offset: 0x0000F98B
		public float DotProduct(Vec2 v)
		{
			return v.x * this.x + v.y * this.y;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000117A8 File Offset: 0x0000F9A8
		public static float DotProduct(Vec2 va, Vec2 vb)
		{
			return va.x * vb.x + va.y * vb.y;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000117C5 File Offset: 0x0000F9C5
		public float RotationInRadians
		{
			get
			{
				return MathF.Atan2(-this.x, this.y);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000117D9 File Offset: 0x0000F9D9
		public static Vec2 FromRotation(float rotation)
		{
			return new Vec2(-MathF.Sin(rotation), MathF.Cos(rotation));
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000117ED File Offset: 0x0000F9ED
		public Vec2 TransformToLocalUnitF(Vec2 a)
		{
			return new Vec2(this.y * a.x - this.x * a.y, this.x * a.x + this.y * a.y);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001182A File Offset: 0x0000FA2A
		public Vec2 TransformToParentUnitF(Vec2 a)
		{
			return new Vec2(this.y * a.x + this.x * a.y, -this.x * a.x + this.y * a.y);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00011868 File Offset: 0x0000FA68
		public Vec2 TransformToLocalUnitFLeftHanded(Vec2 a)
		{
			return new Vec2(-this.y * a.x + this.x * a.y, this.x * a.x + this.y * a.y);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000118A6 File Offset: 0x0000FAA6
		public Vec2 TransformToParentUnitFLeftHanded(Vec2 a)
		{
			return new Vec2(-this.y * a.x + this.x * a.y, this.x * a.x + this.y * a.y);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000118E4 File Offset: 0x0000FAE4
		public Vec2 RightVec()
		{
			return new Vec2(this.y, -this.x);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000118F8 File Offset: 0x0000FAF8
		public Vec2 LeftVec()
		{
			return new Vec2(-this.y, this.x);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001190C File Offset: 0x0000FB0C
		public static Vec2 Max(Vec2 v1, Vec2 v2)
		{
			return new Vec2(MathF.Max(v1.x, v2.x), MathF.Max(v1.y, v2.y));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00011935 File Offset: 0x0000FB35
		public static Vec2 Max(Vec2 v1, float f)
		{
			return new Vec2(MathF.Max(v1.x, f), MathF.Max(v1.y, f));
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00011954 File Offset: 0x0000FB54
		public static Vec2 Min(Vec2 v1, Vec2 v2)
		{
			return new Vec2(MathF.Min(v1.x, v2.x), MathF.Min(v1.y, v2.y));
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001197D File Offset: 0x0000FB7D
		public static Vec2 Min(Vec2 v1, float f)
		{
			return new Vec2(MathF.Min(v1.x, f), MathF.Min(v1.y, f));
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001199C File Offset: 0x0000FB9C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(Vec2) X: ",
				this.x,
				" Y: ",
				this.y
			});
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000119D5 File Offset: 0x0000FBD5
		public float DistanceSquared(Vec2 v)
		{
			return (v.x - this.x) * (v.x - this.x) + (v.y - this.y) * (v.y - this.y);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00011A0E File Offset: 0x0000FC0E
		public float Distance(Vec2 v)
		{
			return MathF.Sqrt((v.x - this.x) * (v.x - this.x) + (v.y - this.y) * (v.y - this.y));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00011A4C File Offset: 0x0000FC4C
		public static float DistanceToLine(Vec2 line1, Vec2 line2, Vec2 point)
		{
			float num = line2.x - line1.x;
			float num2 = line2.y - line1.y;
			return MathF.Abs(num * (line1.y - point.y) - (line1.x - point.x) * num2) / MathF.Sqrt(num * num + num2 * num2);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00011AA6 File Offset: 0x0000FCA6
		public static float DistanceToLineSegmentSquared(Vec2 line1, Vec2 line2, Vec2 point)
		{
			return point.DistanceSquared(MBMath.GetClosestPointInLineSegmentToPoint(point, line1, line2));
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00011AB7 File Offset: 0x0000FCB7
		public float DistanceToLineSegment(Vec2 v, Vec2 w, out Vec2 closestPointOnLineSegment)
		{
			return MathF.Sqrt(this.DistanceSquaredToLineSegment(v, w, out closestPointOnLineSegment));
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		public float DistanceSquaredToLineSegment(Vec2 v, Vec2 w, out Vec2 closestPointOnLineSegment)
		{
			Vec2 v2 = this;
			float num = v.DistanceSquared(w);
			if (num == 0f)
			{
				closestPointOnLineSegment = v;
			}
			else
			{
				float num2 = Vec2.DotProduct(v2 - v, w - v) / num;
				if (num2 < 0f)
				{
					closestPointOnLineSegment = v;
				}
				else if (num2 > 1f)
				{
					closestPointOnLineSegment = w;
				}
				else
				{
					Vec2 vec = v + (w - v) * num2;
					closestPointOnLineSegment = vec;
				}
			}
			return v2.DistanceSquared(closestPointOnLineSegment);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00011B56 File Offset: 0x0000FD56
		public static Vec2 Lerp(Vec2 v1, Vec2 v2, float alpha)
		{
			return v1 * (1f - alpha) + v2 * alpha;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00011B74 File Offset: 0x0000FD74
		public static Vec2 Slerp(Vec2 start, Vec2 end, float percent)
		{
			float num = Vec2.DotProduct(start, end);
			num = MBMath.ClampFloat(num, -1f, 1f);
			float num2 = MathF.Acos(num) * percent;
			Vec2 v = end - start * num;
			v.Normalize();
			return start * MathF.Cos(num2) + v * MathF.Sin(num2);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00011BD8 File Offset: 0x0000FDD8
		public float AngleBetween(Vec2 vector2)
		{
			float num = this.x * vector2.y - vector2.x * this.y;
			float num2 = this.x * vector2.x + this.y * vector2.y;
			return MathF.Atan2(num, num2);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00011C22 File Offset: 0x0000FE22
		public bool IsValid
		{
			get
			{
				return !float.IsNaN(this.x) && !float.IsNaN(this.y) && !float.IsInfinity(this.x) && !float.IsInfinity(this.y);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00011C5B File Offset: 0x0000FE5B
		public static int SideOfLine(Vec2 point, Vec2 line1, Vec2 line2)
		{
			return MathF.Sign((line2.x - line1.x) * (point.y - line1.y) - (point.x - line1.x) * (line2.y - line1.y));
		}

		// Token: 0x0400018C RID: 396
		public float x;

		// Token: 0x0400018D RID: 397
		public float y;

		// Token: 0x0400018E RID: 398
		public static readonly Vec2 Side = new Vec2(1f, 0f);

		// Token: 0x0400018F RID: 399
		public static readonly Vec2 Forward = new Vec2(0f, 1f);

		// Token: 0x04000190 RID: 400
		public static readonly Vec2 One = new Vec2(1f, 1f);

		// Token: 0x04000191 RID: 401
		public static readonly Vec2 Zero = new Vec2(0f, 0f);

		// Token: 0x04000192 RID: 402
		public static readonly Vec2 Invalid = new Vec2(float.NaN, float.NaN);

		// Token: 0x020000E3 RID: 227
		public struct StackArray6Vec2
		{
			// Token: 0x170000F7 RID: 247
			public Vec2 this[int index]
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
					default:
						Debug.FailedAssert("Index out of range.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Vec2.cs", "Item", 36);
						return Vec2.Zero;
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
					default:
						Debug.FailedAssert("Index out of range.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Vec2.cs", "Item", 52);
						return;
					}
				}
			}

			// Token: 0x040002BC RID: 700
			private Vec2 _element0;

			// Token: 0x040002BD RID: 701
			private Vec2 _element1;

			// Token: 0x040002BE RID: 702
			private Vec2 _element2;

			// Token: 0x040002BF RID: 703
			private Vec2 _element3;

			// Token: 0x040002C0 RID: 704
			private Vec2 _element4;

			// Token: 0x040002C1 RID: 705
			private Vec2 _element5;

			// Token: 0x040002C2 RID: 706
			public const int Length = 6;
		}
	}
}
