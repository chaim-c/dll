using System;
using System.Collections.Generic;
using System.Threading;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x0200000B RID: 11
	internal static class BinaryWriterFactory
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002258 File Offset: 0x00000458
		public static BinaryWriter GetBinaryWriter()
		{
			if (BinaryWriterFactory._binaryWriters.Value == null)
			{
				BinaryWriterFactory._binaryWriters.Value = new Stack<BinaryWriter>();
				for (int i = 0; i < 5; i++)
				{
					BinaryWriter item = new BinaryWriter(4096);
					BinaryWriterFactory._binaryWriters.Value.Push(item);
				}
			}
			Stack<BinaryWriter> value = BinaryWriterFactory._binaryWriters.Value;
			if (value.Count != 0)
			{
				return value.Pop();
			}
			return new BinaryWriter(4096);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022CB File Offset: 0x000004CB
		public static void ReleaseBinaryWriter(BinaryWriter writer)
		{
			if (BinaryWriterFactory._binaryWriters != null)
			{
				writer.Clear();
				BinaryWriterFactory._binaryWriters.Value.Push(writer);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022EA File Offset: 0x000004EA
		public static void Initialize()
		{
			BinaryWriterFactory._binaryWriters = new ThreadLocal<Stack<BinaryWriter>>();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022F6 File Offset: 0x000004F6
		public static void Release()
		{
			BinaryWriterFactory._binaryWriters = null;
		}

		// Token: 0x04000008 RID: 8
		private const int WritersPerThread = 5;

		// Token: 0x04000009 RID: 9
		private static ThreadLocal<Stack<BinaryWriter>> _binaryWriters;
	}
}
