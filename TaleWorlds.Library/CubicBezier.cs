using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000025 RID: 37
	public class CubicBezier
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00005236 File Offset: 0x00003436
		public static CubicBezier CreateEase(double controlPoint1X, double controlPoint1Y, double controlPoint2X, double controlPoint2Y)
		{
			return new CubicBezier(controlPoint1X, controlPoint1Y, controlPoint2X, controlPoint2Y, 0.0, 1.0);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005253 File Offset: 0x00003453
		public static CubicBezier CreateYBeginToYEndWithRelativeControlDirs(double yBegin, double yEnd, double controlDir1X, double controlDir1Y, double controlDir2X, double controlDir2Y)
		{
			return new CubicBezier(controlDir1X, yBegin + controlDir1Y, 1.0 + controlDir2X, yEnd + controlDir2Y, yBegin, yEnd);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005270 File Offset: 0x00003470
		public static CubicBezier CreateYBeginToYEnd(double yBegin, double yEnd, double controlPoint1X, double controlPoint1Y, double controlPoint2X, double controlPoint2Y)
		{
			return new CubicBezier(controlPoint1X, controlPoint1Y, controlPoint2X, controlPoint2Y, yBegin, yEnd);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005280 File Offset: 0x00003480
		private CubicBezier(double x1, double y1, double x2, double y2, double yBegin = 0.0, double yEnd = 1.0)
		{
			this._y0 = yBegin;
			this._y3 = yEnd;
			this._x1 = x1;
			this._y1 = y1;
			this._x2 = x2;
			this._y2 = y2;
			if (0.0 > this._x1 || this._x1 > 1.0 || 0.0 > this._x2 || this._x2 > 1.0)
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\CubicBezier.cs", ".ctor", 129);
				throw new ArgumentOutOfRangeException();
			}
			for (int i = 0; i < 11; i++)
			{
				this._sampleValues[i] = CubicBezier.CalcBezierX((double)i * 0.1, this._x1, this._x2);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005364 File Offset: 0x00003564
		public double Sample(double x)
		{
			if (CubicBezier.AlmostEq(x, 0.0))
			{
				return this._y0;
			}
			if (CubicBezier.AlmostEq(x, 1.0))
			{
				return this._y3;
			}
			double num = this._y3 - this._y0;
			if (CubicBezier.AlmostEq(this._x1 * num, this._y1 - this._y0) && CubicBezier.AlmostEq((1.0 - this._x2) * num, this._y3 - this._y2))
			{
				return this._y0 + x * num;
			}
			return CubicBezier.CalcBezierY(this.GetTForX(x), this._y0, this._y1, this._y2, this._y3);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005420 File Offset: 0x00003620
		private static bool AlmostEq(double a, double b)
		{
			return MathF.Abs(a - b) < 1E-09;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005435 File Offset: 0x00003635
		private static double AY(double aA0, double aA1, double aA2, double aA3)
		{
			return 1.0 * aA3 - 3.0 * aA2 + 3.0 * aA1 - 1.0 * aA0;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005466 File Offset: 0x00003666
		private static double A(double aA1, double aA2)
		{
			return 1.0 - 3.0 * aA2 + 3.0 * aA1;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005489 File Offset: 0x00003689
		private static double BY(double aA0, double aA1, double aA2)
		{
			return 3.0 * aA2 - 6.0 * aA1 + 3.0 * aA0;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000054AE File Offset: 0x000036AE
		private static double B(double aA1, double aA2)
		{
			return 3.0 * aA2 - 6.0 * aA1;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000054C7 File Offset: 0x000036C7
		private static double CY(double aA0, double aA1)
		{
			return 3.0 * aA1 - 3.0 * aA0;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000054E0 File Offset: 0x000036E0
		private static double C(double aA1)
		{
			return 3.0 * aA1;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000054ED File Offset: 0x000036ED
		private static double DY(double aA0)
		{
			return aA0;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000054F0 File Offset: 0x000036F0
		private static double CalcBezierY(double aT, double aA0, double aA1, double aA2, double aA3)
		{
			return ((CubicBezier.AY(aA0, aA1, aA2, aA3) * aT + CubicBezier.BY(aA0, aA1, aA2)) * aT + CubicBezier.CY(aA0, aA1)) * aT + CubicBezier.DY(aA0);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000551A File Offset: 0x0000371A
		private static double CalcBezierX(double aT, double aA1, double aA2)
		{
			return ((CubicBezier.A(aA1, aA2) * aT + CubicBezier.B(aA1, aA2)) * aT + CubicBezier.C(aA1)) * aT;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005538 File Offset: 0x00003738
		private static double GetSlopeX(double aT, double aA1, double aA2)
		{
			return 3.0 * CubicBezier.A(aA1, aA2) * aT * aT + 2.0 * CubicBezier.B(aA1, aA2) * aT + CubicBezier.C(aA1);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000556C File Offset: 0x0000376C
		private static double BinarySubdivide(double aX, double aA, double aB, double mX1, double mX2)
		{
			int num = 0;
			double num2;
			double num3;
			do
			{
				num2 = aA + (aB - aA) / 2.0;
				num3 = CubicBezier.CalcBezierX(num2, mX1, mX2) - aX;
				if (num3 > 0.0)
				{
					aB = num2;
				}
				else
				{
					aA = num2;
				}
			}
			while (MathF.Abs(num3) > 1E-07 && ++num < 10);
			return num2;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000055C8 File Offset: 0x000037C8
		private double NewtonRaphsonIterate(double aX, double aGuessT, double mX1, double mX2)
		{
			for (int i = 0; i < 4; i++)
			{
				double slopeX = CubicBezier.GetSlopeX(aGuessT, mX1, mX2);
				if (MathF.Abs(slopeX) < 1E-07)
				{
					return aGuessT;
				}
				double num = CubicBezier.CalcBezierX(aGuessT, mX1, mX2) - aX;
				aGuessT -= num / slopeX;
			}
			return aGuessT;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005614 File Offset: 0x00003814
		private double GetTForX(double aX)
		{
			double num = 0.0;
			int num2 = 1;
			int num3 = 10;
			while (num2 != num3 && this._sampleValues[num2] <= aX)
			{
				num += 0.1;
				num2++;
			}
			num2--;
			double num4 = (aX - this._sampleValues[num2]) / (this._sampleValues[num2 + 1] - this._sampleValues[num2]);
			double num5 = num + num4 * 0.1;
			double slopeX = CubicBezier.GetSlopeX(num5, this._x1, this._x2);
			double result;
			if (slopeX >= 0.001)
			{
				result = this.NewtonRaphsonIterate(aX, num5, this._x1, this._x2);
			}
			else if (CubicBezier.AlmostEq(slopeX, 0.0))
			{
				result = num5;
			}
			else
			{
				result = CubicBezier.BinarySubdivide(aX, num, num + 0.1, this._x1, this._x2);
			}
			return result;
		}

		// Token: 0x0400006B RID: 107
		private readonly double _x1;

		// Token: 0x0400006C RID: 108
		private readonly double _y1;

		// Token: 0x0400006D RID: 109
		private readonly double _x2;

		// Token: 0x0400006E RID: 110
		private readonly double _y2;

		// Token: 0x0400006F RID: 111
		private readonly double _y0;

		// Token: 0x04000070 RID: 112
		private readonly double _y3;

		// Token: 0x04000071 RID: 113
		private const int NewtonIterations = 4;

		// Token: 0x04000072 RID: 114
		private const double NewtonMinSlope = 0.001;

		// Token: 0x04000073 RID: 115
		private const double SubdivisionPrecision = 1E-07;

		// Token: 0x04000074 RID: 116
		private const int SubdivisionMaxIterations = 10;

		// Token: 0x04000075 RID: 117
		private const int KSplineTableSize = 11;

		// Token: 0x04000076 RID: 118
		private const double KSampleStepSize = 0.1;

		// Token: 0x04000077 RID: 119
		private readonly double[] _sampleValues = new double[11];
	}
}
