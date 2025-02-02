using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.Library
{
	// Token: 0x02000067 RID: 103
	public static class MBMath
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000B2DA File Offset: 0x000094DA
		public static float ToRadians(this float f)
		{
			return f * 0.017453292f;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000B2E3 File Offset: 0x000094E3
		public static float ToDegrees(this float f)
		{
			return f * 57.295776f;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000B2EC File Offset: 0x000094EC
		public static bool ApproximatelyEqualsTo(this float f, float comparedValue, float epsilon = 1E-05f)
		{
			return Math.Abs(f - comparedValue) <= epsilon;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000B2FC File Offset: 0x000094FC
		public static bool ApproximatelyEquals(float first, float second, float epsilon = 1E-05f)
		{
			return Math.Abs(first - second) <= epsilon;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000B30C File Offset: 0x0000950C
		public static bool IsValidValue(float f)
		{
			return !float.IsNaN(f) && !float.IsInfinity(f);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000B321 File Offset: 0x00009521
		public static int ClampIndex(int value, int minValue, int maxValue)
		{
			return MBMath.ClampInt(value, minValue, maxValue - 1);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000B32D File Offset: 0x0000952D
		public static int ClampInt(int value, int minValue, int maxValue)
		{
			return Math.Max(Math.Min(value, maxValue), minValue);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000B33C File Offset: 0x0000953C
		public static float ClampFloat(float value, float minValue, float maxValue)
		{
			return Math.Max(Math.Min(value, maxValue), minValue);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000B34B File Offset: 0x0000954B
		public static void ClampUnit(ref float value)
		{
			value = MBMath.ClampFloat(value, 0f, 1f);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000B360 File Offset: 0x00009560
		public static int GetNumberOfBitsToRepresentNumber(uint value)
		{
			int num = 0;
			for (uint num2 = value; num2 > 0U; num2 >>= 1)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000B380 File Offset: 0x00009580
		public static IEnumerable<ValueTuple<T, int>> DistributeShares<T>(int totalAward, IEnumerable<T> stakeHolders, Func<T, int> shareFunction)
		{
			List<ValueTuple<T, int>> sharesList = new List<ValueTuple<T, int>>(20);
			int num = 0;
			foreach (T t in stakeHolders)
			{
				int num2 = shareFunction(t);
				sharesList.Add(new ValueTuple<T, int>(t, num2));
				num += num2;
			}
			if (num > 0)
			{
				int remainingShares = num;
				int remaingAward = totalAward;
				int i = 0;
				while (i < sharesList.Count && remaingAward > 0)
				{
					int item = sharesList[i].Item2;
					int num3 = MathF.Round((float)remaingAward * (float)item / (float)remainingShares);
					if (num3 > remaingAward)
					{
						num3 = remaingAward;
					}
					remaingAward -= num3;
					remainingShares -= item;
					yield return new ValueTuple<T, int>(sharesList[i].Item1, num3);
					int num4 = i + 1;
					i = num4;
				}
			}
			yield break;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000B3A0 File Offset: 0x000095A0
		public static int GetNumberOfBitsToRepresentNumber(ulong value)
		{
			int num = 0;
			for (ulong num2 = value; num2 > 0UL; num2 >>= 1)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000B3C1 File Offset: 0x000095C1
		public static float Lerp(float valueFrom, float valueTo, float amount, float minimumDifference = 1E-05f)
		{
			if (Math.Abs(valueFrom - valueTo) <= minimumDifference)
			{
				return valueTo;
			}
			return valueFrom + (valueTo - valueFrom) * amount;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000B3D7 File Offset: 0x000095D7
		public static float LinearExtrapolation(float valueFrom, float valueTo, float amount)
		{
			return valueFrom + (valueTo - valueFrom) * amount;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000B3E0 File Offset: 0x000095E0
		public static Vec3 Lerp(Vec3 vecFrom, Vec3 vecTo, float amount, float minimumDifference)
		{
			return new Vec3(MBMath.Lerp(vecFrom.x, vecTo.x, amount, minimumDifference), MBMath.Lerp(vecFrom.y, vecTo.y, amount, minimumDifference), MBMath.Lerp(vecFrom.z, vecTo.z, amount, minimumDifference), -1f);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000B430 File Offset: 0x00009630
		public static Vec2 Lerp(Vec2 vecFrom, Vec2 vecTo, float amount, float minimumDifference)
		{
			return new Vec2(MBMath.Lerp(vecFrom.x, vecTo.x, amount, minimumDifference), MBMath.Lerp(vecFrom.y, vecTo.y, amount, minimumDifference));
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000B45D File Offset: 0x0000965D
		public static float Map(float input, float inputMinimum, float inputMaximum, float outputMinimum, float outputMaximum)
		{
			input = MBMath.ClampFloat(input, inputMinimum, inputMaximum);
			return (input - inputMinimum) * (outputMaximum - outputMinimum) / (inputMaximum - inputMinimum) + outputMinimum;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000B477 File Offset: 0x00009677
		public static Mat3 Lerp(ref Mat3 matFrom, ref Mat3 matTo, float amount, float minimumDifference)
		{
			return new Mat3(MBMath.Lerp(matFrom.s, matTo.s, amount, minimumDifference), MBMath.Lerp(matFrom.f, matTo.f, amount, minimumDifference), MBMath.Lerp(matFrom.u, matTo.u, amount, minimumDifference));
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public static float LerpRadians(float valueFrom, float valueTo, float amount, float minChange, float maxChange)
		{
			float smallestDifferenceBetweenTwoAngles = MBMath.GetSmallestDifferenceBetweenTwoAngles(valueFrom, valueTo);
			if (Math.Abs(smallestDifferenceBetweenTwoAngles) <= minChange)
			{
				return valueTo;
			}
			float num = (float)Math.Sign(smallestDifferenceBetweenTwoAngles) * MBMath.ClampFloat(Math.Abs(smallestDifferenceBetweenTwoAngles * amount), minChange, maxChange);
			return MBMath.WrapAngle(valueFrom + num);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000B4FC File Offset: 0x000096FC
		public static float SplitLerp(float value1, float value2, float value3, float cutOff, float amount, float minimumDifference)
		{
			if (amount <= cutOff)
			{
				float amount2 = amount / cutOff;
				return MBMath.Lerp(value1, value2, amount2, minimumDifference);
			}
			float num = 1f - cutOff;
			float amount3 = (amount - cutOff) / num;
			return MBMath.Lerp(value2, value3, amount3, minimumDifference);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000B539 File Offset: 0x00009739
		public static float InverseLerp(float valueFrom, float valueTo, float value)
		{
			return (value - valueFrom) / (valueTo - valueFrom);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000B544 File Offset: 0x00009744
		public static float SmoothStep(float edge0, float edge1, float value)
		{
			float num = MBMath.ClampFloat((value - edge0) / (edge1 - edge0), 0f, 1f);
			return num * num * (3f - 2f * num);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000B57C File Offset: 0x0000977C
		public static float BilinearLerp(float topLeft, float topRight, float botLeft, float botRight, float x, float y)
		{
			float valueFrom = MBMath.Lerp(topLeft, topRight, x, 1E-05f);
			float valueTo = MBMath.Lerp(botLeft, botRight, x, 1E-05f);
			return MBMath.Lerp(valueFrom, valueTo, y, 1E-05f);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B5B4 File Offset: 0x000097B4
		public static float GetSmallestDifferenceBetweenTwoAngles(float fromAngle, float toAngle)
		{
			float num = toAngle - fromAngle;
			if (num > 3.1415927f)
			{
				num = -6.2831855f + num;
			}
			if (num < -3.1415927f)
			{
				num = 6.2831855f + num;
			}
			return num;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public static float ClampAngle(float angle, float restrictionCenter, float restrictionRange)
		{
			restrictionRange /= 2f;
			float smallestDifferenceBetweenTwoAngles = MBMath.GetSmallestDifferenceBetweenTwoAngles(restrictionCenter, angle);
			if (smallestDifferenceBetweenTwoAngles > restrictionRange)
			{
				angle = restrictionCenter + restrictionRange;
			}
			else if (smallestDifferenceBetweenTwoAngles < -restrictionRange)
			{
				angle = restrictionCenter - restrictionRange;
			}
			if (angle > 3.1415927f)
			{
				angle -= 6.2831855f;
			}
			else if (angle < -3.1415927f)
			{
				angle += 6.2831855f;
			}
			return angle;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B640 File Offset: 0x00009840
		public static float WrapAngle(float angle)
		{
			angle = (float)Math.IEEERemainder((double)angle, 6.283185307179586);
			if (angle <= -3.1415927f)
			{
				angle += 6.2831855f;
			}
			else if (angle > 3.1415927f)
			{
				angle -= 6.2831855f;
			}
			return angle;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B67A File Offset: 0x0000987A
		public static bool IsBetween(float numberToCheck, float bottom, float top)
		{
			return numberToCheck > bottom && numberToCheck < top;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B686 File Offset: 0x00009886
		public static bool IsBetween(int value, int minValue, int maxValue)
		{
			return value >= minValue && value < maxValue;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B692 File Offset: 0x00009892
		public static bool IsBetweenInclusive(float numberToCheck, float bottom, float top)
		{
			return numberToCheck >= bottom && numberToCheck <= top;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B6A1 File Offset: 0x000098A1
		public static uint ColorFromRGBA(float red, float green, float blue, float alpha)
		{
			return ((uint)(alpha * 255f) << 24) + ((uint)(red * 255f) << 16) + ((uint)(green * 255f) << 8) + (uint)(blue * 255f);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public static Color HSBtoRGB(float hue, float saturation, float brightness, float outputAlpha)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = brightness * saturation;
			float num5 = num4 * (1f - MathF.Abs(hue * 0.016666668f % 2f - 1f));
			float num6 = brightness - num4;
			switch ((int)(hue * 0.016666668f % 6f))
			{
			case 0:
				num = num4;
				num2 = num5;
				num3 = 0f;
				break;
			case 1:
				num = num5;
				num2 = num4;
				num3 = 0f;
				break;
			case 2:
				num = 0f;
				num2 = num4;
				num3 = num5;
				break;
			case 3:
				num = 0f;
				num2 = num5;
				num3 = num4;
				break;
			case 4:
				num = num5;
				num2 = 0f;
				num3 = num4;
				break;
			case 5:
				num = num4;
				num2 = 0f;
				num3 = num5;
				break;
			}
			return new Color(num + num6, num2 + num6, num3 + num6, outputAlpha);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B7AC File Offset: 0x000099AC
		public static Vec3 RGBtoHSB(Color rgb)
		{
			Vec3 vec = new Vec3(0f, 0f, 0f, -1f);
			float num = MathF.Min(MathF.Min(rgb.Red, rgb.Green), rgb.Blue);
			float num2 = MathF.Max(MathF.Max(rgb.Red, rgb.Green), rgb.Blue);
			float num3 = num2 - num;
			vec.z = num2;
			if (MathF.Abs(num3) < 0.0001f)
			{
				vec.x = 0f;
			}
			else if (MathF.Abs(num2 - rgb.Red) < 0.0001f)
			{
				vec.x = 60f * ((rgb.Green - rgb.Blue) / num3 % 6f);
			}
			else if (MathF.Abs(num2 - rgb.Green) < 0.0001f)
			{
				vec.x = 60f * ((rgb.Blue - rgb.Red) / num3 + 2f);
			}
			else
			{
				vec.x = 60f * ((rgb.Red - rgb.Green) / num3 + 4f);
			}
			vec.x %= 360f;
			if (vec.x < 0f)
			{
				vec.x += 360f;
			}
			if (MathF.Abs(num2) < 0.0001f)
			{
				vec.y = 0f;
			}
			else
			{
				vec.y = num3 / num2;
			}
			return vec;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B920 File Offset: 0x00009B20
		public static Vec3 GammaCorrectRGB(float gamma, Vec3 rgb)
		{
			float y = 1f / gamma;
			rgb.x = MathF.Pow(rgb.x, y);
			rgb.y = MathF.Pow(rgb.y, y);
			rgb.z = MathF.Pow(rgb.z, y);
			return rgb;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B970 File Offset: 0x00009B70
		public static Vec3 GetClosestPointInLineSegmentToPoint(Vec3 point, Vec3 lineSegmentBegin, Vec3 lineSegmentEnd)
		{
			Vec3 vec = lineSegmentEnd - lineSegmentBegin;
			if (!vec.IsNonZero)
			{
				return lineSegmentBegin;
			}
			float num = Vec3.DotProduct(point - lineSegmentBegin, vec) / Vec3.DotProduct(vec, vec);
			if (num < 0f)
			{
				return lineSegmentBegin;
			}
			if (num > 1f)
			{
				return lineSegmentEnd;
			}
			return lineSegmentBegin + vec * num;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public static bool GetRayPlaneIntersectionPoint(in Vec3 planeNormal, in Vec3 planeCenter, in Vec3 rayOrigin, in Vec3 rayDirection, out float t)
		{
			float num = Vec3.DotProduct(planeNormal, rayDirection);
			if (num > 1E-06f)
			{
				Vec3 v = planeCenter - rayOrigin;
				t = Vec3.DotProduct(v, planeNormal) / num;
				return t >= 0f;
			}
			t = -1f;
			return false;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000BA2C File Offset: 0x00009C2C
		public static Vec2 GetClosestPointInLineSegmentToPoint(Vec2 point, Vec2 lineSegmentBegin, Vec2 lineSegmentEnd)
		{
			Vec2 vec = lineSegmentEnd - lineSegmentBegin;
			if (!vec.IsNonZero())
			{
				return lineSegmentBegin;
			}
			float num = Vec2.DotProduct(point - lineSegmentBegin, vec) / Vec2.DotProduct(vec, vec);
			if (num < 0f)
			{
				return lineSegmentBegin;
			}
			if (num > 1f)
			{
				return lineSegmentEnd;
			}
			return lineSegmentBegin + vec * num;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000BA84 File Offset: 0x00009C84
		public static bool CheckLineToLineSegmentIntersection(Vec2 lineOrigin, Vec2 lineDirection, Vec2 segmentA, Vec2 segmentB, out float t, out Vec2 intersect)
		{
			t = float.MaxValue;
			intersect = Vec2.Zero;
			Vec2 vec = lineOrigin - segmentA;
			Vec2 vec2 = segmentB - segmentA;
			Vec2 v = new Vec2(-lineDirection.y, lineDirection.x);
			float num = vec2.DotProduct(v);
			if (MathF.Abs(num) < 1E-05f)
			{
				return false;
			}
			float num2 = vec2.x * vec.y - vec2.y * vec.x;
			t = num2 / num;
			intersect = lineOrigin + lineDirection * t;
			float num3 = vec.DotProduct(v) / num;
			return num3 >= 0f && num3 <= 1f;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000BB40 File Offset: 0x00009D40
		public static float GetClosestPointOnLineSegment(Vec2 point, Vec2 segmentA, Vec2 segmentB, out Vec2 closest)
		{
			Vec2 vec = point - segmentA;
			Vec2 v = segmentB - segmentA;
			float num = vec.DotProduct(v) / Math.Max(v.LengthSquared, 1E-05f);
			if (num < 0f)
			{
				closest = segmentA;
			}
			else if (num > 1f)
			{
				closest = segmentB;
			}
			else
			{
				closest = segmentA + v * num;
			}
			return point.Distance(closest);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public static bool IntersectRayWithBoundaryList(Vec2 rayOrigin, Vec2 rayDir, List<Vec2> boundaries, out Vec2 intersectionPoint)
		{
			List<ValueTuple<float, Vec2>> list = new List<ValueTuple<float, Vec2>>();
			for (int j = 0; j < boundaries.Count; j++)
			{
				Vec2 segmentA = boundaries[j];
				Vec2 segmentB = boundaries[(j + 1) % boundaries.Count];
				float num;
				Vec2 item;
				if (MBMath.CheckLineToLineSegmentIntersection(rayOrigin, rayDir, segmentA, segmentB, out num, out item) && num > 0f)
				{
					list.Add(new ValueTuple<float, Vec2>(num, item));
				}
			}
			list = (from i in list
			orderby i.Item1
			select i).ToList<ValueTuple<float, Vec2>>();
			if (list.Count != 0)
			{
				intersectionPoint = list[0].Item2;
				return true;
			}
			intersectionPoint = rayOrigin;
			return false;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000BC70 File Offset: 0x00009E70
		public static string ToOrdinal(int number)
		{
			if (number < 0)
			{
				return number.ToString();
			}
			long num = (long)(number % 100);
			if (num >= 11L && num <= 13L)
			{
				return number + "th";
			}
			switch (number % 10)
			{
			case 1:
				return number + "st";
			case 2:
				return number + "nd";
			case 3:
				return number + "rd";
			default:
				return number + "th";
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public static int IndexOfMax<T>(MBReadOnlyList<T> array, Func<T, int> func)
		{
			int num = int.MinValue;
			int result = -1;
			for (int i = 0; i < array.Count; i++)
			{
				int num2 = func(array[i]);
				if (num2 > num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000BD4C File Offset: 0x00009F4C
		public static T MaxElement<T>(IEnumerable<T> collection, Func<T, float> func)
		{
			float num = float.MinValue;
			T result = default(T);
			foreach (T t in collection)
			{
				float num2 = func(t);
				if (num2 > num)
				{
					num = num2;
					result = t;
				}
			}
			return result;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		public static ValueTuple<T, T> MaxElements2<T>(IEnumerable<T> collection, Func<T, float> func)
		{
			float num = float.MinValue;
			float num2 = float.MinValue;
			T t = default(T);
			T item = default(T);
			foreach (T t2 in collection)
			{
				float num3 = func(t2);
				if (num3 > num2)
				{
					if (num3 > num)
					{
						num2 = num;
						item = t;
						num = num3;
						t = t2;
					}
					else
					{
						num2 = num3;
						item = t2;
					}
				}
			}
			return new ValueTuple<T, T>(t, item);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BE40 File Offset: 0x0000A040
		public static ValueTuple<T, T, T> MaxElements3<T>(IEnumerable<T> collection, Func<T, float> func)
		{
			float num = float.MinValue;
			float num2 = float.MinValue;
			float num3 = float.MinValue;
			T t = default(T);
			T t2 = default(T);
			T item = default(T);
			foreach (T t3 in collection)
			{
				float num4 = func(t3);
				if (num4 > num3)
				{
					if (num4 > num2)
					{
						num3 = num2;
						item = t2;
						if (num4 > num)
						{
							num2 = num;
							t2 = t;
							num = num4;
							t = t3;
						}
						else
						{
							num2 = num4;
							t2 = t3;
						}
					}
					else
					{
						num3 = num4;
						item = t3;
					}
				}
			}
			return new ValueTuple<T, T, T>(t, t2, item);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public static ValueTuple<T, T, T, T> MaxElements4<T>(IEnumerable<T> collection, Func<T, float> func)
		{
			float num = float.MinValue;
			float num2 = float.MinValue;
			float num3 = float.MinValue;
			float num4 = float.MinValue;
			T t = default(T);
			T t2 = default(T);
			T t3 = default(T);
			T item = default(T);
			foreach (T t4 in collection)
			{
				float num5 = func(t4);
				if (num5 > num4)
				{
					if (num5 > num3)
					{
						num4 = num3;
						item = t3;
						if (num5 > num2)
						{
							num3 = num2;
							t3 = t2;
							if (num5 > num)
							{
								num2 = num;
								t2 = t;
								num = num5;
								t = t4;
							}
							else
							{
								num2 = num5;
								t2 = t4;
							}
						}
						else
						{
							num3 = num5;
							t3 = t4;
						}
					}
					else
					{
						num4 = num5;
						item = t4;
					}
				}
			}
			return new ValueTuple<T, T, T, T>(t, t2, t3, item);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
		public static ValueTuple<T, T, T, T, T> MaxElements5<T>(IEnumerable<T> collection, Func<T, float> func)
		{
			float num = float.MinValue;
			float num2 = float.MinValue;
			float num3 = float.MinValue;
			float num4 = float.MinValue;
			float num5 = float.MinValue;
			T t = default(T);
			T t2 = default(T);
			T t3 = default(T);
			T t4 = default(T);
			T item = default(T);
			foreach (T t5 in collection)
			{
				float num6 = func(t5);
				if (num6 > num5)
				{
					if (num6 > num4)
					{
						num5 = num4;
						item = t4;
						if (num6 > num3)
						{
							num4 = num3;
							t4 = t3;
							if (num6 > num2)
							{
								num3 = num2;
								t3 = t2;
								if (num6 > num)
								{
									num2 = num;
									t2 = t;
									num = num6;
									t = t5;
								}
								else
								{
									num2 = num6;
									t2 = t5;
								}
							}
							else
							{
								num3 = num6;
								t3 = t5;
							}
						}
						else
						{
							num4 = num6;
							t4 = t5;
						}
					}
					else
					{
						num5 = num6;
						item = t5;
					}
				}
			}
			return new ValueTuple<T, T, T, T, T>(t, t2, t3, t4, item);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public static IList<T> TopologySort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
		{
			List<T> list = new List<T>();
			Dictionary<T, bool> visited = new Dictionary<T, bool>();
			foreach (T item in source)
			{
				MBMath.Visit<T>(item, getDependencies, list, visited);
			}
			return list;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000C138 File Offset: 0x0000A338
		private static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
		{
			bool flag;
			if (visited.TryGetValue(item, out flag))
			{
				return;
			}
			visited[item] = true;
			IEnumerable<T> enumerable = getDependencies(item);
			if (enumerable != null)
			{
				foreach (T item2 in enumerable)
				{
					MBMath.Visit<T>(item2, getDependencies, sorted, visited);
				}
			}
			visited[item] = false;
			sorted.Add(item);
		}

		// Token: 0x04000110 RID: 272
		public const float TwoPI = 6.2831855f;

		// Token: 0x04000111 RID: 273
		public const float PI = 3.1415927f;

		// Token: 0x04000112 RID: 274
		public const float HalfPI = 1.5707964f;

		// Token: 0x04000113 RID: 275
		public const float E = 2.7182817f;

		// Token: 0x04000114 RID: 276
		public const float DegreesToRadians = 0.017453292f;

		// Token: 0x04000115 RID: 277
		public const float RadiansToDegrees = 57.295776f;

		// Token: 0x04000116 RID: 278
		public const float Epsilon = 1E-05f;
	}
}
