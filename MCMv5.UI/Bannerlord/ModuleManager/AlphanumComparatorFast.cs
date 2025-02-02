﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000051 RID: 81
	internal sealed class AlphanumComparatorFast : IComparer<string>, IComparer
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		[NullableContext(2)]
		public int Compare(object x, object y)
		{
			return this.Compare(x as string, y as string);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		[NullableContext(2)]
		public int Compare(string s1, string s2)
		{
			bool flag = s1 == null && s2 == null;
			int result2;
			if (flag)
			{
				result2 = 0;
			}
			else
			{
				bool flag2 = s1 == null;
				if (flag2)
				{
					result2 = -1;
				}
				else
				{
					bool flag3 = s2 == null;
					if (flag3)
					{
						result2 = 1;
					}
					else
					{
						int len = s1.Length;
						int len2 = s2.Length;
						bool flag4 = len == 0 && len2 == 0;
						if (flag4)
						{
							result2 = 0;
						}
						else
						{
							bool flag5 = len == 0;
							if (flag5)
							{
								result2 = -1;
							}
							else
							{
								bool flag6 = len2 == 0;
								if (flag6)
								{
									result2 = 1;
								}
								else
								{
									int marker = 0;
									int marker2 = 0;
									while (marker < len || marker2 < len2)
									{
										bool flag7 = marker >= len;
										if (flag7)
										{
											return -1;
										}
										bool flag8 = marker2 >= len2;
										if (flag8)
										{
											return 1;
										}
										char ch = s1[marker];
										char ch2 = s2[marker2];
										StringBuilder chunk = new StringBuilder();
										StringBuilder chunk2 = new StringBuilder();
										while (marker < len && (chunk.Length == 0 || AlphanumComparatorFast.InChunk(ch, chunk[0])))
										{
											chunk.Append(ch);
											marker++;
											bool flag9 = marker < len;
											if (flag9)
											{
												ch = s1[marker];
											}
										}
										while (marker2 < len2 && (chunk2.Length == 0 || AlphanumComparatorFast.InChunk(ch2, chunk2[0])))
										{
											chunk2.Append(ch2);
											marker2++;
											bool flag10 = marker2 < len2;
											if (flag10)
											{
												ch2 = s2[marker2];
											}
										}
										bool flag11 = char.IsDigit(chunk[0]) && char.IsDigit(chunk2[0]);
										if (flag11)
										{
											int numericChunk = Convert.ToInt32(chunk.ToString());
											int numericChunk2 = Convert.ToInt32(chunk2.ToString());
											bool flag12 = numericChunk < numericChunk2;
											if (flag12)
											{
												return -1;
											}
											bool flag13 = numericChunk > numericChunk2;
											if (flag13)
											{
												return 1;
											}
										}
										else
										{
											int result = string.CompareOrdinal(chunk.ToString(), chunk2.ToString());
											bool flag14 = result >= 1;
											if (flag14)
											{
												return 1;
											}
											bool flag15 = result <= -1;
											if (flag15)
											{
												return -1;
											}
										}
									}
									result2 = 0;
								}
							}
						}
					}
				}
			}
			return result2;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000E608 File Offset: 0x0000C808
		private static bool InChunk(char ch, char otherCh)
		{
			AlphanumComparatorFast.ChunkType type = AlphanumComparatorFast.ChunkType.Alphanumeric;
			bool flag = char.IsDigit(otherCh);
			if (flag)
			{
				type = AlphanumComparatorFast.ChunkType.Numeric;
			}
			return (type != AlphanumComparatorFast.ChunkType.Alphanumeric || !char.IsDigit(ch)) && (type != AlphanumComparatorFast.ChunkType.Numeric || char.IsDigit(ch));
		}

		// Token: 0x020000C5 RID: 197
		private enum ChunkType
		{
			// Token: 0x04000210 RID: 528
			Alphanumeric,
			// Token: 0x04000211 RID: 529
			Numeric
		}
	}
}
