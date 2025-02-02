using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200006C RID: 108
	public class MBWorkspace<T> where T : IMBCollection, new()
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x0000C505 File Offset: 0x0000A705
		public T StartUsingWorkspace()
		{
			this._isBeingUsed = true;
			if (this._workspace == null)
			{
				this._workspace = Activator.CreateInstance<T>();
			}
			return this._workspace;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000C52C File Offset: 0x0000A72C
		public void StopUsingWorkspace()
		{
			this._isBeingUsed = false;
			this._workspace.Clear();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000C546 File Offset: 0x0000A746
		public T GetWorkspace()
		{
			return this._workspace;
		}

		// Token: 0x04000119 RID: 281
		private bool _isBeingUsed;

		// Token: 0x0400011A RID: 282
		private T _workspace;
	}
}
