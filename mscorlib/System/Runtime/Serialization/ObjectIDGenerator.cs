﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074A RID: 1866
	[ComVisible(true)]
	[Serializable]
	public class ObjectIDGenerator
	{
		// Token: 0x0600525E RID: 21086 RVA: 0x00121314 File Offset: 0x0011F514
		public ObjectIDGenerator()
		{
			this.m_currentCount = 1;
			this.m_currentSize = ObjectIDGenerator.sizes[0];
			this.m_ids = new long[this.m_currentSize * 4];
			this.m_objs = new object[this.m_currentSize * 4];
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x00121364 File Offset: 0x0011F564
		private int FindElement(object obj, out bool found)
		{
			int num = RuntimeHelpers.GetHashCode(obj);
			int num2 = 1 + (num & int.MaxValue) % (this.m_currentSize - 2);
			int i;
			for (;;)
			{
				int num3 = (num & int.MaxValue) % this.m_currentSize * 4;
				for (i = num3; i < num3 + 4; i++)
				{
					if (this.m_objs[i] == null)
					{
						goto Block_1;
					}
					if (this.m_objs[i] == obj)
					{
						goto Block_2;
					}
				}
				num += num2;
			}
			Block_1:
			found = false;
			return i;
			Block_2:
			found = true;
			return i;
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x001213D0 File Offset: 0x0011F5D0
		public virtual long GetId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			bool flag;
			int num = this.FindElement(obj, out flag);
			long result;
			if (!flag)
			{
				this.m_objs[num] = obj;
				long[] ids = this.m_ids;
				int num2 = num;
				int currentCount = this.m_currentCount;
				this.m_currentCount = currentCount + 1;
				ids[num2] = (long)currentCount;
				result = this.m_ids[num];
				if (this.m_currentCount > this.m_currentSize * 4 / 2)
				{
					this.Rehash();
				}
			}
			else
			{
				result = this.m_ids[num];
			}
			firstTime = !flag;
			return result;
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x00121458 File Offset: 0x0011F658
		public virtual long HasId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			bool flag;
			int num = this.FindElement(obj, out flag);
			if (flag)
			{
				firstTime = false;
				return this.m_ids[num];
			}
			firstTime = true;
			return 0L;
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x0012149C File Offset: 0x0011F69C
		private void Rehash()
		{
			int[] array = AppContextSwitches.UseNewMaxArraySize ? ObjectIDGenerator.sizesWithMaxArraySwitch : ObjectIDGenerator.sizes;
			int num = 0;
			int currentSize = this.m_currentSize;
			while (num < array.Length && array[num] <= currentSize)
			{
				num++;
			}
			if (num == array.Length)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
			}
			this.m_currentSize = array[num];
			long[] ids = new long[this.m_currentSize * 4];
			object[] objs = new object[this.m_currentSize * 4];
			long[] ids2 = this.m_ids;
			object[] objs2 = this.m_objs;
			this.m_ids = ids;
			this.m_objs = objs;
			for (int i = 0; i < objs2.Length; i++)
			{
				if (objs2[i] != null)
				{
					bool flag;
					int num2 = this.FindElement(objs2[i], out flag);
					this.m_objs[num2] = objs2[i];
					this.m_ids[num2] = ids2[i];
				}
			}
		}

		// Token: 0x04002474 RID: 9332
		private const int numbins = 4;

		// Token: 0x04002475 RID: 9333
		internal int m_currentCount;

		// Token: 0x04002476 RID: 9334
		internal int m_currentSize;

		// Token: 0x04002477 RID: 9335
		internal long[] m_ids;

		// Token: 0x04002478 RID: 9336
		internal object[] m_objs;

		// Token: 0x04002479 RID: 9337
		private static readonly int[] sizes = new int[]
		{
			5,
			11,
			29,
			47,
			97,
			197,
			397,
			797,
			1597,
			3203,
			6421,
			12853,
			25717,
			51437,
			102877,
			205759,
			411527,
			823117,
			1646237,
			3292489,
			6584983
		};

		// Token: 0x0400247A RID: 9338
		private static readonly int[] sizesWithMaxArraySwitch = new int[]
		{
			5,
			11,
			29,
			47,
			97,
			197,
			397,
			797,
			1597,
			3203,
			6421,
			12853,
			25717,
			51437,
			102877,
			205759,
			411527,
			823117,
			1646237,
			3292489,
			6584983,
			13169977,
			26339969,
			52679969,
			105359939,
			210719881,
			421439783
		};
	}
}
