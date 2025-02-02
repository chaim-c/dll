using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TaleWorlds.Library
{
	// Token: 0x02000032 RID: 50
	internal static class GCHandleFactory
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00006B78 File Offset: 0x00004D78
		static GCHandleFactory()
		{
			for (int i = 0; i < 512; i++)
			{
				GCHandleFactory._handles.Add(GCHandle.Alloc(null, GCHandleType.Pinned));
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006BBC File Offset: 0x00004DBC
		public static GCHandle GetHandle()
		{
			object locker = GCHandleFactory._locker;
			lock (locker)
			{
				if (GCHandleFactory._handles.Count > 0)
				{
					GCHandle result = GCHandleFactory._handles[GCHandleFactory._handles.Count - 1];
					GCHandleFactory._handles.RemoveAt(GCHandleFactory._handles.Count - 1);
					return result;
				}
			}
			return GCHandle.Alloc(null, GCHandleType.Pinned);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006C3C File Offset: 0x00004E3C
		public static void ReturnHandle(GCHandle handle)
		{
			object locker = GCHandleFactory._locker;
			lock (locker)
			{
				GCHandleFactory._handles.Add(handle);
			}
		}

		// Token: 0x04000084 RID: 132
		private static List<GCHandle> _handles = new List<GCHandle>();

		// Token: 0x04000085 RID: 133
		private static object _locker = new object();
	}
}
