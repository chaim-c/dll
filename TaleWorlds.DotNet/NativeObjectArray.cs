using System;
using System.Collections;
using System.Collections.Generic;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200002A RID: 42
	[EngineClass("ftdnNative_object_array")]
	public sealed class NativeObjectArray : NativeObject, IEnumerable<NativeObject>, IEnumerable
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000523C File Offset: 0x0000343C
		internal NativeObjectArray(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000524B File Offset: 0x0000344B
		public static NativeObjectArray Create()
		{
			return LibraryApplicationInterface.INativeObjectArray.Create();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005257 File Offset: 0x00003457
		public int Count
		{
			get
			{
				return LibraryApplicationInterface.INativeObjectArray.GetCount(base.Pointer);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005269 File Offset: 0x00003469
		public NativeObject GetElementAt(int index)
		{
			return LibraryApplicationInterface.INativeObjectArray.GetElementAtIndex(base.Pointer, index);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000527C File Offset: 0x0000347C
		public void AddElement(NativeObject nativeObject)
		{
			LibraryApplicationInterface.INativeObjectArray.AddElement(base.Pointer, (nativeObject != null) ? nativeObject.Pointer : UIntPtr.Zero);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000052A4 File Offset: 0x000034A4
		public void Clear()
		{
			LibraryApplicationInterface.INativeObjectArray.Clear(base.Pointer);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000052B6 File Offset: 0x000034B6
		IEnumerator<NativeObject> IEnumerable<NativeObject>.GetEnumerator()
		{
			int count = this.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				NativeObject elementAt = this.GetElementAt(i);
				yield return elementAt;
				num = i;
			}
			yield break;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000052C5 File Offset: 0x000034C5
		IEnumerator IEnumerable.GetEnumerator()
		{
			int count = this.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				NativeObject elementAt = this.GetElementAt(i);
				yield return elementAt;
				num = i;
			}
			yield break;
		}
	}
}
