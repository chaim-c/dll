using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000024 RID: 36
	public class MaterialPool<T> where T : Material, new()
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00007015 File Offset: 0x00005215
		public MaterialPool(int initialBufferSize)
		{
			this._materialList = new List<T>(initialBufferSize);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000702C File Offset: 0x0000522C
		public T New()
		{
			if (this._nextAvailableIndex < this._materialList.Count)
			{
				T result = this._materialList[this._nextAvailableIndex];
				this._nextAvailableIndex++;
				return result;
			}
			T t = Activator.CreateInstance<T>();
			this._materialList.Add(t);
			this._nextAvailableIndex++;
			return t;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000708D File Offset: 0x0000528D
		public void ResetAll()
		{
			this._nextAvailableIndex = 0;
		}

		// Token: 0x040000BB RID: 187
		private List<T> _materialList;

		// Token: 0x040000BC RID: 188
		private int _nextAvailableIndex;
	}
}
