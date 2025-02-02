using System;
using System.Runtime.InteropServices;

namespace TaleWorlds.TwoDimension.Standalone.Native
{
	// Token: 0x02000011 RID: 17
	internal class AutoPinner : IDisposable
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00004E0B File Offset: 0x0000300B
		public AutoPinner(object obj)
		{
			if (obj != null)
			{
				this._pinnedObject = GCHandle.Alloc(obj, GCHandleType.Pinned);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004E23 File Offset: 0x00003023
		public static implicit operator IntPtr(AutoPinner autoPinner)
		{
			if (autoPinner._pinnedObject.IsAllocated)
			{
				return autoPinner._pinnedObject.AddrOfPinnedObject();
			}
			return IntPtr.Zero;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004E43 File Offset: 0x00003043
		public void Dispose()
		{
			if (this._pinnedObject.IsAllocated)
			{
				this._pinnedObject.Free();
			}
		}

		// Token: 0x0400005A RID: 90
		private GCHandle _pinnedObject;
	}
}
