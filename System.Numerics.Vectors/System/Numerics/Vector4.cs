﻿using System;
using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	// Token: 0x02000011 RID: 17
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0001D350 File Offset: 0x0001B550
		public static Vector4 Zero
		{
			get
			{
				return default(Vector4);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0001D366 File Offset: 0x0001B566
		public static Vector4 One
		{
			get
			{
				return new Vector4(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0001D381 File Offset: 0x0001B581
		public static Vector4 UnitX
		{
			get
			{
				return new Vector4(1f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0001D39C File Offset: 0x0001B59C
		public static Vector4 UnitY
		{
			get
			{
				return new Vector4(0f, 1f, 0f, 0f);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0001D3B7 File Offset: 0x0001B5B7
		public static Vector4 UnitZ
		{
			get
			{
				return new Vector4(0f, 0f, 1f, 0f);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0001D3D2 File Offset: 0x0001B5D2
		public static Vector4 UnitW
		{
			get
			{
				return new Vector4(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		public override int GetHashCode()
		{
			int h = this.X.GetHashCode();
			h = HashHelpers.Combine(h, this.Y.GetHashCode());
			h = HashHelpers.Combine(h, this.Z.GetHashCode());
			return HashHelpers.Combine(h, this.W.GetHashCode());
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0001D440 File Offset: 0x0001B640
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return obj is Vector4 && this.Equals((Vector4)obj);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0001D458 File Offset: 0x0001B658
		public override string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0001D46A File Offset: 0x0001B66A
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001D478 File Offset: 0x0001B678
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(this.X.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.Y.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.Z.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.W.ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001D534 File Offset: 0x0001B734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				float x = Vector4.Dot(this, this);
				return MathF.Sqrt(x);
			}
			float x2 = this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
			return MathF.Sqrt(x2);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001D5A0 File Offset: 0x0001B7A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector4.Dot(this, this);
			}
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001D600 File Offset: 0x0001B800
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 value1, Vector4 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector4 vector = value1 - value2;
				float x = Vector4.Dot(vector, vector);
				return MathF.Sqrt(x);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			float x2 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			return MathF.Sqrt(x2);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001D684 File Offset: 0x0001B884
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector4 value1, Vector4 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector4 vector = value1 - value2;
				return Vector4.Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			return num * num + num2 * num2 + num3 * num3 + num4 * num4;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 vector)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float value = vector.Length();
				return vector / value;
			}
			float x = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
			float num = 1f / MathF.Sqrt(x);
			return new Vector4(vector.X * num, vector.Y * num, vector.Z * num, vector.W * num);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001D784 File Offset: 0x0001B984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
		{
			float num = value1.X;
			num = ((num > max.X) ? max.X : num);
			num = ((num < min.X) ? min.X : num);
			float num2 = value1.Y;
			num2 = ((num2 > max.Y) ? max.Y : num2);
			num2 = ((num2 < min.Y) ? min.Y : num2);
			float num3 = value1.Z;
			num3 = ((num3 > max.Z) ? max.Z : num3);
			num3 = ((num3 < min.Z) ? min.Z : num3);
			float num4 = value1.W;
			num4 = ((num4 > max.W) ? max.W : num4);
			num4 = ((num4 < min.W) ? min.W : num4);
			return new Vector4(num, num2, num3, num4);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001D850 File Offset: 0x0001BA50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount, value1.Z + (value2.Z - value1.Z) * amount, value1.W + (value2.W - value1.W) * amount);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001D8BC File Offset: 0x0001BABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001D958 File Offset: 0x0001BB58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001DA2C File Offset: 0x0001BC2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 vector, Matrix4x4 matrix)
		{
			return new Vector4(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0001DB1C File Offset: 0x0001BD1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0001DD28 File Offset: 0x0001BF28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0001DE45 File Offset: 0x0001C045
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Add(Vector4 left, Vector4 right)
		{
			return left + right;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0001DE4E File Offset: 0x0001C04E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Subtract(Vector4 left, Vector4 right)
		{
			return left - right;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0001DE57 File Offset: 0x0001C057
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, Vector4 right)
		{
			return left * right;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001DE60 File Offset: 0x0001C060
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, float right)
		{
			return left * new Vector4(right, right, right, right);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0001DE71 File Offset: 0x0001C071
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(float left, Vector4 right)
		{
			return new Vector4(left, left, left, left) * right;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0001DE82 File Offset: 0x0001C082
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, Vector4 right)
		{
			return left / right;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0001DE8B File Offset: 0x0001C08B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, float divisor)
		{
			return left / divisor;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0001DE94 File Offset: 0x0001C094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Negate(Vector4 value)
		{
			return -value;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0001DE9C File Offset: 0x0001C09C
		[JitIntrinsic]
		public Vector4(float value)
		{
			this = new Vector4(value, value, value, value);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
		[JitIntrinsic]
		public Vector4(float x, float y, float z, float w)
		{
			this.W = w;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0001DEC7 File Offset: 0x0001C0C7
		public Vector4(Vector2 value, float z, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0001DEEF File Offset: 0x0001C0EF
		public Vector4(Vector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0001DF1C File Offset: 0x0001C11C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0001DF28 File Offset: 0x0001C128
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException(SR.Arg_NullArgumentNullRef);
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.Format(SR.Arg_ArgumentOutOfRangeException, index));
			}
			if (array.Length - index < 4)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ElementsInSourceIsGreaterThanDestination, index));
			}
			array[index] = this.X;
			array[index + 1] = this.Y;
			array[index + 2] = this.Z;
			array[index + 3] = this.W;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0001DFB0 File Offset: 0x0001C1B0
		[JitIntrinsic]
		public bool Equals(Vector4 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0001DFEC File Offset: 0x0001C1EC
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector4 vector1, Vector4 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0001E028 File Offset: 0x0001C228
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z, (value1.W < value2.W) ? value1.W : value2.W);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z, (value1.W > value2.W) ? value1.W : value2.W);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0001E12E File Offset: 0x0001C32E
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Abs(Vector4 value)
		{
			return new Vector4(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z), MathF.Abs(value.W));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0001E161 File Offset: 0x0001C361
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 SquareRoot(Vector4 value)
		{
			return new Vector4(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z), MathF.Sqrt(value.W));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0001E194 File Offset: 0x0001C394
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator +(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0001E1CF File Offset: 0x0001C3CF
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0001E20A File Offset: 0x0001C40A
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0001E245 File Offset: 0x0001C445
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 left, float right)
		{
			return left * new Vector4(right);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0001E253 File Offset: 0x0001C453
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(float left, Vector4 right)
		{
			return new Vector4(left) * right;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0001E261 File Offset: 0x0001C461
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001E29C File Offset: 0x0001C49C
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 value1, float value2)
		{
			float num = 1f / value2;
			return new Vector4(value1.X * num, value1.Y * num, value1.Z * num, value1.W * num);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0001E2D6 File Offset: 0x0001C4D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 value)
		{
			return Vector4.Zero - value;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0001E2E3 File Offset: 0x0001C4E3
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector4 left, Vector4 right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0001E2ED File Offset: 0x0001C4ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4 left, Vector4 right)
		{
			return !(left == right);
		}

		// Token: 0x0400006E RID: 110
		public float X;

		// Token: 0x0400006F RID: 111
		public float Y;

		// Token: 0x04000070 RID: 112
		public float Z;

		// Token: 0x04000071 RID: 113
		public float W;
	}
}
