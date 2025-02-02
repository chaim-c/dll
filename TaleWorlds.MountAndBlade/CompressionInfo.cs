using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D8 RID: 728
	public class CompressionInfo
	{
		// Token: 0x02000593 RID: 1427
		[EngineStruct("Integer_compression_info", false)]
		public struct Integer
		{
			// Token: 0x06003A42 RID: 14914 RVA: 0x000E6764 File Offset: 0x000E4964
			public Integer(int minimumValue, int maximumValue, bool maximumValueGiven)
			{
				this.maximumValue = maximumValue;
				this.minimumValue = minimumValue;
				uint value = (uint)(maximumValue - minimumValue);
				this.numberOfBits = MBMath.GetNumberOfBitsToRepresentNumber(value);
			}

			// Token: 0x06003A43 RID: 14915 RVA: 0x000E678F File Offset: 0x000E498F
			public Integer(int minimumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.numberOfBits = numberOfBits;
				if (minimumValue == -2147483648 && numberOfBits == 32)
				{
					this.maximumValue = int.MaxValue;
					return;
				}
				this.maximumValue = minimumValue + (1 << numberOfBits) - 1;
			}

			// Token: 0x06003A44 RID: 14916 RVA: 0x000E67C8 File Offset: 0x000E49C8
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x06003A45 RID: 14917 RVA: 0x000E67D0 File Offset: 0x000E49D0
			public int GetMaximumValue()
			{
				return this.maximumValue;
			}

			// Token: 0x04001D9F RID: 7583
			[CustomEngineStructMemberData("min_value")]
			private readonly int minimumValue;

			// Token: 0x04001DA0 RID: 7584
			[CustomEngineStructMemberData("max_value")]
			private readonly int maximumValue;

			// Token: 0x04001DA1 RID: 7585
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}

		// Token: 0x02000594 RID: 1428
		[EngineStruct("Unsigned_integer_compression_info", false)]
		public struct UnsignedInteger
		{
			// Token: 0x06003A46 RID: 14918 RVA: 0x000E67D8 File Offset: 0x000E49D8
			public UnsignedInteger(uint minimumValue, uint maximumValue, bool maximumValueGiven)
			{
				this.minimumValue = minimumValue;
				this.maximumValue = maximumValue;
				uint value = maximumValue - minimumValue;
				this.numberOfBits = MBMath.GetNumberOfBitsToRepresentNumber(value);
			}

			// Token: 0x06003A47 RID: 14919 RVA: 0x000E6803 File Offset: 0x000E4A03
			public UnsignedInteger(uint minimumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.numberOfBits = numberOfBits;
				if (minimumValue == 0U && numberOfBits == 32)
				{
					this.maximumValue = uint.MaxValue;
					return;
				}
				this.maximumValue = (uint)((ulong)minimumValue + (1UL << numberOfBits) - 1UL);
			}

			// Token: 0x06003A48 RID: 14920 RVA: 0x000E6837 File Offset: 0x000E4A37
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x04001DA2 RID: 7586
			[CustomEngineStructMemberData("min_value")]
			private readonly uint minimumValue;

			// Token: 0x04001DA3 RID: 7587
			[CustomEngineStructMemberData("max_value")]
			private readonly uint maximumValue;

			// Token: 0x04001DA4 RID: 7588
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}

		// Token: 0x02000595 RID: 1429
		[EngineStruct("Integer64_compression_info", false)]
		public struct LongInteger
		{
			// Token: 0x06003A49 RID: 14921 RVA: 0x000E6840 File Offset: 0x000E4A40
			public LongInteger(long minimumValue, long maximumValue, bool maximumValueGiven)
			{
				this.maximumValue = maximumValue;
				this.minimumValue = minimumValue;
				ulong value = (ulong)(maximumValue - minimumValue);
				this.numberOfBits = MBMath.GetNumberOfBitsToRepresentNumber(value);
			}

			// Token: 0x06003A4A RID: 14922 RVA: 0x000E686C File Offset: 0x000E4A6C
			public LongInteger(long minimumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.numberOfBits = numberOfBits;
				if (minimumValue == -9223372036854775808L && numberOfBits == 64)
				{
					this.maximumValue = long.MaxValue;
					return;
				}
				this.maximumValue = minimumValue + (1L << numberOfBits) - 1L;
			}

			// Token: 0x06003A4B RID: 14923 RVA: 0x000E68BA File Offset: 0x000E4ABA
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x04001DA5 RID: 7589
			[CustomEngineStructMemberData("min_value")]
			private readonly long minimumValue;

			// Token: 0x04001DA6 RID: 7590
			[CustomEngineStructMemberData("max_value")]
			private readonly long maximumValue;

			// Token: 0x04001DA7 RID: 7591
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}

		// Token: 0x02000596 RID: 1430
		[EngineStruct("Unsigned_integer64_compression_info", false)]
		public struct UnsignedLongInteger
		{
			// Token: 0x06003A4C RID: 14924 RVA: 0x000E68C4 File Offset: 0x000E4AC4
			public UnsignedLongInteger(ulong minimumValue, ulong maximumValue, bool maximumValueGiven)
			{
				this.minimumValue = minimumValue;
				this.maximumValue = maximumValue;
				ulong value = maximumValue - minimumValue;
				this.numberOfBits = MBMath.GetNumberOfBitsToRepresentNumber(value);
			}

			// Token: 0x06003A4D RID: 14925 RVA: 0x000E68EF File Offset: 0x000E4AEF
			public UnsignedLongInteger(ulong minimumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.numberOfBits = numberOfBits;
				if (minimumValue == 0UL && numberOfBits == 64)
				{
					this.maximumValue = ulong.MaxValue;
					return;
				}
				this.maximumValue = minimumValue + (1UL << numberOfBits) - 1UL;
			}

			// Token: 0x06003A4E RID: 14926 RVA: 0x000E6922 File Offset: 0x000E4B22
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x04001DA8 RID: 7592
			[CustomEngineStructMemberData("min_value")]
			private readonly ulong minimumValue;

			// Token: 0x04001DA9 RID: 7593
			[CustomEngineStructMemberData("max_value")]
			private readonly ulong maximumValue;

			// Token: 0x04001DAA RID: 7594
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}

		// Token: 0x02000597 RID: 1431
		[EngineStruct("Float_compression_info", false)]
		public struct Float
		{
			// Token: 0x170009B1 RID: 2481
			// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000E692A File Offset: 0x000E4B2A
			public static CompressionInfo.Float FullPrecision { get; } = new CompressionInfo.Float(true);

			// Token: 0x06003A50 RID: 14928 RVA: 0x000E6934 File Offset: 0x000E4B34
			public Float(float minimumValue, float maximumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.maximumValue = maximumValue;
				this.numberOfBits = numberOfBits;
				float num = maximumValue - minimumValue;
				int num2 = (1 << numberOfBits) - 1;
				this.precision = num / (float)num2;
			}

			// Token: 0x06003A51 RID: 14929 RVA: 0x000E6970 File Offset: 0x000E4B70
			public Float(float minimumValue, int numberOfBits, float precision)
			{
				this.minimumValue = minimumValue;
				this.precision = precision;
				this.numberOfBits = numberOfBits;
				int num = (1 << numberOfBits) - 1;
				float num2 = precision * (float)num;
				this.maximumValue = num2 + minimumValue;
			}

			// Token: 0x06003A52 RID: 14930 RVA: 0x000E69A9 File Offset: 0x000E4BA9
			private Float(bool isFullPrecision)
			{
				this.minimumValue = float.MinValue;
				this.maximumValue = float.MaxValue;
				this.precision = 0f;
				this.numberOfBits = 32;
			}

			// Token: 0x06003A53 RID: 14931 RVA: 0x000E69D4 File Offset: 0x000E4BD4
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x06003A54 RID: 14932 RVA: 0x000E69DC File Offset: 0x000E4BDC
			public float GetMaximumValue()
			{
				return this.maximumValue;
			}

			// Token: 0x06003A55 RID: 14933 RVA: 0x000E69E4 File Offset: 0x000E4BE4
			public float GetMinimumValue()
			{
				return this.minimumValue;
			}

			// Token: 0x06003A56 RID: 14934 RVA: 0x000E69EC File Offset: 0x000E4BEC
			public float GetPrecision()
			{
				return this.precision;
			}

			// Token: 0x04001DAC RID: 7596
			[CustomEngineStructMemberData("min_value")]
			private readonly float minimumValue;

			// Token: 0x04001DAD RID: 7597
			[CustomEngineStructMemberData("max_value")]
			private readonly float maximumValue;

			// Token: 0x04001DAE RID: 7598
			[CustomEngineStructMemberData(true)]
			private readonly float precision;

			// Token: 0x04001DAF RID: 7599
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}
	}
}
