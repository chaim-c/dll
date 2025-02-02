using System;
using System.Reflection;

namespace TaleWorlds.Library
{
	// Token: 0x0200002C RID: 44
	internal static class EnumHelper<T1>
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00006689 File Offset: 0x00004889
		public static bool Overlaps(sbyte p1, sbyte p2)
		{
			return (p1 & p2) != 0;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006691 File Offset: 0x00004891
		public static bool Overlaps(byte p1, byte p2)
		{
			return (p1 & p2) > 0;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006699 File Offset: 0x00004899
		public static bool Overlaps(short p1, short p2)
		{
			return (p1 & p2) != 0;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000066A1 File Offset: 0x000048A1
		public static bool Overlaps(ushort p1, ushort p2)
		{
			return (p1 & p2) > 0;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000066A9 File Offset: 0x000048A9
		public static bool Overlaps(int p1, int p2)
		{
			return (p1 & p2) != 0;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000066B1 File Offset: 0x000048B1
		public static bool Overlaps(uint p1, uint p2)
		{
			return (p1 & p2) > 0U;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000066B9 File Offset: 0x000048B9
		public static bool Overlaps(long p1, long p2)
		{
			return (p1 & p2) != 0L;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000066C2 File Offset: 0x000048C2
		public static bool Overlaps(ulong p1, ulong p2)
		{
			return (p1 & p2) > 0UL;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000066CB File Offset: 0x000048CB
		public static bool ContainsAll(sbyte p1, sbyte p2)
		{
			return (p1 & p2) == p2;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000066D3 File Offset: 0x000048D3
		public static bool ContainsAll(byte p1, byte p2)
		{
			return (p1 & p2) == p2;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000066DB File Offset: 0x000048DB
		public static bool ContainsAll(short p1, short p2)
		{
			return (p1 & p2) == p2;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000066E3 File Offset: 0x000048E3
		public static bool ContainsAll(ushort p1, ushort p2)
		{
			return (p1 & p2) == p2;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000066EB File Offset: 0x000048EB
		public static bool ContainsAll(int p1, int p2)
		{
			return (p1 & p2) == p2;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000066F3 File Offset: 0x000048F3
		public static bool ContainsAll(uint p1, uint p2)
		{
			return (p1 & p2) == p2;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000066FB File Offset: 0x000048FB
		public static bool ContainsAll(long p1, long p2)
		{
			return (p1 & p2) == p2;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006703 File Offset: 0x00004903
		public static bool ContainsAll(ulong p1, ulong p2)
		{
			return (p1 & p2) == p2;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000670C File Offset: 0x0000490C
		public static bool initProc(T1 p1, T1 p2)
		{
			Type type = typeof(T1);
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			Type[] types = new Type[]
			{
				type,
				type
			};
			MethodInfo method = typeof(EnumHelper<T1>).GetMethod("Overlaps", types);
			if (method == null)
			{
				method = typeof(T1).GetMethod("Overlaps", types);
			}
			if (method == null)
			{
				throw new MissingMethodException("Unknown type of enum");
			}
			EnumHelper<T1>.HasAnyFlag = (Func<T1, T1, bool>)Delegate.CreateDelegate(typeof(Func<T1, T1, bool>), method);
			return EnumHelper<T1>.HasAnyFlag(p1, p2);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000067B4 File Offset: 0x000049B4
		public static bool initAllProc(T1 p1, T1 p2)
		{
			Type type = typeof(T1);
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			Type[] types = new Type[]
			{
				type,
				type
			};
			MethodInfo method = typeof(EnumHelper<T1>).GetMethod("ContainsAll", types);
			if (method == null)
			{
				method = typeof(T1).GetMethod("ContainsAll", types);
			}
			if (method == null)
			{
				throw new MissingMethodException("Unknown type of enum");
			}
			EnumHelper<T1>.HasAllFlags = (Func<T1, T1, bool>)Delegate.CreateDelegate(typeof(Func<T1, T1, bool>), method);
			return EnumHelper<T1>.HasAllFlags(p1, p2);
		}

		// Token: 0x04000082 RID: 130
		public static Func<T1, T1, bool> HasAnyFlag = new Func<T1, T1, bool>(EnumHelper<T1>.initProc);

		// Token: 0x04000083 RID: 131
		public static Func<T1, T1, bool> HasAllFlags = new Func<T1, T1, bool>(EnumHelper<T1>.initAllProc);
	}
}
