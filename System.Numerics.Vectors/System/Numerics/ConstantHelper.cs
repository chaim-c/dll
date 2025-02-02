using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x02000006 RID: 6
	internal class ConstantHelper
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000021F0 File Offset: 0x000003F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte GetByteWithAllBitsSet()
		{
			byte result = 0;
			*(&result) = byte.MaxValue;
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000220C File Offset: 0x0000040C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static sbyte GetSByteWithAllBitsSet()
		{
			sbyte result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002224 File Offset: 0x00000424
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetUInt16WithAllBitsSet()
		{
			ushort result = 0;
			*(&result) = ushort.MaxValue;
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002240 File Offset: 0x00000440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static short GetInt16WithAllBitsSet()
		{
			short result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002258 File Offset: 0x00000458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint GetUInt32WithAllBitsSet()
		{
			uint result = 0U;
			*(&result) = uint.MaxValue;
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002270 File Offset: 0x00000470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetInt32WithAllBitsSet()
		{
			int result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002288 File Offset: 0x00000488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetUInt64WithAllBitsSet()
		{
			ulong result = 0UL;
			*(&result) = ulong.MaxValue;
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022A0 File Offset: 0x000004A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long GetInt64WithAllBitsSet()
		{
			long result = 0L;
			*(&result) = -1L;
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022B8 File Offset: 0x000004B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float GetSingleWithAllBitsSet()
		{
			float result = 0f;
			*(int*)(&result) = -1;
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022D4 File Offset: 0x000004D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double GetDoubleWithAllBitsSet()
		{
			double result = 0.0;
			*(long*)(&result) = -1L;
			return result;
		}
	}
}
