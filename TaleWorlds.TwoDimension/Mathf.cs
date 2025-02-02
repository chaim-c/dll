using System;
using System.Numerics;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000029 RID: 41
	public static class Mathf
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00007671 File Offset: 0x00005871
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000767B File Offset: 0x0000587B
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007684 File Offset: 0x00005884
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000768E File Offset: 0x0000588E
		public static float Cos(float radian)
		{
			return (float)Math.Cos((double)radian);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007698 File Offset: 0x00005898
		public static float Sin(float radian)
		{
			return (float)Math.Sin((double)radian);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000076A2 File Offset: 0x000058A2
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000076AC File Offset: 0x000058AC
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000076B8 File Offset: 0x000058B8
		public static float Clamp(float value, float min, float max)
		{
			if (value > max)
			{
				return max;
			}
			if (value >= min)
			{
				return value;
			}
			return min;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000076C7 File Offset: 0x000058C7
		public static int Clamp(int value, int min, int max)
		{
			if (value > max)
			{
				return max;
			}
			if (value >= min)
			{
				return value;
			}
			return min;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000076D6 File Offset: 0x000058D6
		public static float Min(float a, float b)
		{
			if (a <= b)
			{
				return a;
			}
			return b;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000076DF File Offset: 0x000058DF
		public static float Max(float a, float b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000076E8 File Offset: 0x000058E8
		public static bool IsZero(float f)
		{
			return f < 1E-05f && f > -1E-05f;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000076FC File Offset: 0x000058FC
		public static bool IsZero(Vector2 vector2)
		{
			return Mathf.IsZero(vector2.X) && Mathf.IsZero(vector2.Y);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007718 File Offset: 0x00005918
		public static float Sign(float f)
		{
			return (float)Math.Sign(f);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007721 File Offset: 0x00005921
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000772B File Offset: 0x0000592B
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007735 File Offset: 0x00005935
		public static float Lerp(float start, float end, float amount)
		{
			return (end - start) * amount + start;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007740 File Offset: 0x00005940
		private static float PingPong(float min, float max, float time)
		{
			int num = (int)(min * 100f);
			int num2 = (int)(max * 100f);
			int num3 = (int)(time * 100f);
			int num4 = num2 - num;
			bool flag = num3 / num4 % 2 == 0;
			int num5 = num3 % num4;
			return (float)(flag ? (num5 + num) : (num2 - num5)) / 100f;
		}

		// Token: 0x040000E6 RID: 230
		public const float PI = 3.1415927f;

		// Token: 0x040000E7 RID: 231
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x040000E8 RID: 232
		public const float Rad2Deg = 57.295776f;

		// Token: 0x040000E9 RID: 233
		public const float Epsilon = 1E-05f;
	}
}
