using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000066 RID: 102
	public static class MathF
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000AFD6 File Offset: 0x000091D6
		public static float Sqrt(float x)
		{
			return (float)Math.Sqrt((double)x);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public static float Sin(float x)
		{
			return (float)Math.Sin((double)x);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000AFEA File Offset: 0x000091EA
		public static float Asin(float x)
		{
			return (float)Math.Asin((double)x);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public static float Cos(float x)
		{
			return (float)Math.Cos((double)x);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000AFFE File Offset: 0x000091FE
		public static float Acos(float x)
		{
			return (float)Math.Acos((double)x);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000B008 File Offset: 0x00009208
		public static float Tan(float x)
		{
			return (float)Math.Tan((double)x);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000B012 File Offset: 0x00009212
		public static float Tanh(float x)
		{
			return (float)Math.Tanh((double)x);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000B01C File Offset: 0x0000921C
		public static float Atan(float x)
		{
			return (float)Math.Atan((double)x);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000B026 File Offset: 0x00009226
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000B032 File Offset: 0x00009232
		public static double Pow(double x, double y)
		{
			return Math.Pow(x, y);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000B03B File Offset: 0x0000923B
		[Obsolete("Types must match!", true)]
		public static double Pow(float x, double y)
		{
			return Math.Pow((double)x, y);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000B045 File Offset: 0x00009245
		public static float Pow(float x, float y)
		{
			return (float)Math.Pow((double)x, (double)y);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000B051 File Offset: 0x00009251
		public static int PowTwo32(int x)
		{
			return 1 << x;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000B059 File Offset: 0x00009259
		public static ulong PowTwo64(int x)
		{
			return 1UL << x;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000B062 File Offset: 0x00009262
		public static bool IsValidValue(float f)
		{
			return !float.IsNaN(f) && !float.IsInfinity(f);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000B077 File Offset: 0x00009277
		public static float Clamp(float value, float minValue, float maxValue)
		{
			return MathF.Max(MathF.Min(value, maxValue), minValue);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000B086 File Offset: 0x00009286
		public static float AngleClamp(float angle)
		{
			while (angle < 0f)
			{
				angle += 6.2831855f;
			}
			while (angle > 6.2831855f)
			{
				angle -= 6.2831855f;
			}
			return angle;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000B0AF File Offset: 0x000092AF
		public static float Lerp(float valueFrom, float valueTo, float amount, float minimumDifference = 1E-05f)
		{
			if (Math.Abs(valueFrom - valueTo) <= minimumDifference)
			{
				return valueTo;
			}
			return valueFrom + (valueTo - valueFrom) * amount;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000B0C8 File Offset: 0x000092C8
		public static float AngleLerp(float angleFrom, float angleTo, float amount, float minimumDifference = 1E-05f)
		{
			float num = (angleTo - angleFrom) % 6.2831855f;
			float num2 = 2f * num % 6.2831855f - num;
			return MathF.AngleClamp(angleFrom + num2 * amount);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000B0F9 File Offset: 0x000092F9
		public static int Round(double f)
		{
			return (int)Math.Round(f);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000B102 File Offset: 0x00009302
		public static int Round(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000B10C File Offset: 0x0000930C
		public static float Round(float f, int digits)
		{
			return (float)Math.Round((double)f, digits);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000B117 File Offset: 0x00009317
		[Obsolete("Type is already int!", true)]
		public static int Round(int f)
		{
			return (int)Math.Round((double)((float)f));
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000B122 File Offset: 0x00009322
		public static int Floor(double f)
		{
			return (int)Math.Floor(f);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000B12B File Offset: 0x0000932B
		public static int Floor(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000B135 File Offset: 0x00009335
		[Obsolete("Type is already int!", true)]
		public static int Floor(int f)
		{
			return (int)Math.Floor((double)((float)f));
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000B140 File Offset: 0x00009340
		public static int Ceiling(double f)
		{
			return (int)Math.Ceiling(f);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000B149 File Offset: 0x00009349
		public static int Ceiling(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B153 File Offset: 0x00009353
		[Obsolete("Type is already int!", true)]
		public static int Ceiling(int f)
		{
			return (int)Math.Ceiling((double)((float)f));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B15E File Offset: 0x0000935E
		public static double Abs(double f)
		{
			if (f < 0.0)
			{
				return -f;
			}
			return f;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B170 File Offset: 0x00009370
		public static float Abs(float f)
		{
			if (f < 0f)
			{
				return -f;
			}
			return f;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B17E File Offset: 0x0000937E
		public static int Abs(int f)
		{
			if ((float)f < 0f)
			{
				return -f;
			}
			return f;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B18D File Offset: 0x0000938D
		public static double Max(double a, double b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B196 File Offset: 0x00009396
		public static float Max(float a, float b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B19F File Offset: 0x0000939F
		[Obsolete("Types must match!", true)]
		public static float Max(float a, int b)
		{
			if (a <= (float)b)
			{
				return (float)b;
			}
			return a;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B1AA File Offset: 0x000093AA
		[Obsolete("Types must match!", true)]
		public static float Max(int a, float b)
		{
			if ((float)a <= b)
			{
				return b;
			}
			return (float)a;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000B1B5 File Offset: 0x000093B5
		public static int Max(int a, int b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000B1BE File Offset: 0x000093BE
		public static long Max(long a, long b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000B1C7 File Offset: 0x000093C7
		public static uint Max(uint a, uint b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public static float Max(float a, float b, float c)
		{
			return Math.Max(a, Math.Max(b, c));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000B1DF File Offset: 0x000093DF
		public static double Min(double a, double b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000B1E8 File Offset: 0x000093E8
		public static float Min(float a, float b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000B1F1 File Offset: 0x000093F1
		public static short Min(short a, short b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000B1FA File Offset: 0x000093FA
		public static int Min(int a, int b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000B203 File Offset: 0x00009403
		public static long Min(long a, long b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000B20C File Offset: 0x0000940C
		public static uint Min(uint a, uint b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000B215 File Offset: 0x00009415
		[Obsolete("Types must match!", true)]
		public static int Min(int a, float b)
		{
			if ((float)a >= b)
			{
				return (int)b;
			}
			return a;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000B220 File Offset: 0x00009420
		[Obsolete("Types must match!", true)]
		public static int Min(float a, int b)
		{
			if (a >= (float)b)
			{
				return b;
			}
			return (int)a;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000B22B File Offset: 0x0000942B
		public static float Min(float a, float b, float c)
		{
			return Math.Min(a, Math.Min(b, c));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000B23C File Offset: 0x0000943C
		public static float PingPong(float min, float max, float time)
		{
			int num = (int)(min * 100f);
			int num2 = (int)(max * 100f);
			int num3 = (int)(time * 100f);
			int num4 = num2 - num;
			bool flag = num3 / num4 % 2 == 0;
			int num5 = num3 % num4;
			return (float)(flag ? (num5 + num) : (num2 - num5)) / 100f;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B288 File Offset: 0x00009488
		public static int GreatestCommonDivisor(int a, int b)
		{
			while (b != 0)
			{
				int num = a % b;
				a = b;
				b = num;
			}
			return a;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B298 File Offset: 0x00009498
		public static float Log(float a)
		{
			return (float)Math.Log((double)a);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000B2A2 File Offset: 0x000094A2
		public static float Log(float a, float newBase)
		{
			return (float)Math.Log((double)a, (double)newBase);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000B2AE File Offset: 0x000094AE
		public static int Sign(float f)
		{
			return Math.Sign(f);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000B2B6 File Offset: 0x000094B6
		public static int Sign(int f)
		{
			return Math.Sign(f);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000B2BE File Offset: 0x000094BE
		public static void SinCos(float a, out float sa, out float ca)
		{
			sa = MathF.Sin(a);
			ca = MathF.Cos(a);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000B2D0 File Offset: 0x000094D0
		public static float Log10(float val)
		{
			return (float)Math.Log10((double)val);
		}

		// Token: 0x04000109 RID: 265
		public const float DegToRad = 0.017453292f;

		// Token: 0x0400010A RID: 266
		public const float RadToDeg = 57.29578f;

		// Token: 0x0400010B RID: 267
		public const float TwoPI = 6.2831855f;

		// Token: 0x0400010C RID: 268
		public const float PI = 3.1415927f;

		// Token: 0x0400010D RID: 269
		public const float HalfPI = 1.5707964f;

		// Token: 0x0400010E RID: 270
		public const float E = 2.7182817f;

		// Token: 0x0400010F RID: 271
		public const float Epsilon = 1E-05f;
	}
}
