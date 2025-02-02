using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public struct ManagedArray
	{
		// Token: 0x060002AF RID: 687 RVA: 0x000082A0 File Offset: 0x000064A0
		public ManagedArray(IntPtr array, int length)
		{
			this.Array = array;
			this.Length = length;
		}

		// Token: 0x040000F7 RID: 247
		internal IntPtr Array;

		// Token: 0x040000F8 RID: 248
		internal int Length;
	}
}
