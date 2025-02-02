using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000028 RID: 40
	[EngineClass("ftdnNative_array")]
	public sealed class NativeArray : NativeObject
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004BD7 File Offset: 0x00002DD7
		internal NativeArray(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004BE6 File Offset: 0x00002DE6
		public static NativeArray Create()
		{
			return LibraryApplicationInterface.INativeArray.Create();
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004BF2 File Offset: 0x00002DF2
		public int DataSize
		{
			get
			{
				return LibraryApplicationInterface.INativeArray.GetDataSize(base.Pointer);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004C04 File Offset: 0x00002E04
		private UIntPtr GetDataPointer()
		{
			return LibraryApplicationInterface.INativeArray.GetDataPointer(base.Pointer);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004C18 File Offset: 0x00002E18
		public int GetLength<T>() where T : struct
		{
			int dataSize = this.DataSize;
			int num = Marshal.SizeOf<T>();
			return dataSize / num;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004C34 File Offset: 0x00002E34
		public T GetElementAt<T>(int index) where T : struct
		{
			IntPtr intPtr = Marshal.PtrToStructure<IntPtr>(new IntPtr((long)(base.Pointer.ToUInt64() + (ulong)((long)NativeArray.DataPointerOffset))));
			int num = Marshal.SizeOf<T>();
			return Marshal.PtrToStructure<T>(new IntPtr(intPtr.ToInt64() + (long)(index * num)));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004C7D File Offset: 0x00002E7D
		public IEnumerable<T> GetEnumerator<T>() where T : struct
		{
			int length = this.GetLength<T>();
			IntPtr ptr = new IntPtr((long)(base.Pointer.ToUInt64() + (ulong)((long)NativeArray.DataPointerOffset)));
			IntPtr dataPointer = Marshal.PtrToStructure<IntPtr>(ptr);
			int elementSize = Marshal.SizeOf<T>();
			int num;
			for (int i = 0; i < length; i = num + 1)
			{
				T t = Marshal.PtrToStructure<T>(new IntPtr(dataPointer.ToInt64() + (long)(i * elementSize)));
				yield return t;
				num = i;
			}
			yield break;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004C90 File Offset: 0x00002E90
		public T[] ToArray<T>() where T : struct
		{
			T[] array = new T[this.GetLength<T>()];
			IEnumerable<T> enumerator = this.GetEnumerator<T>();
			int num = 0;
			foreach (T t in enumerator)
			{
				array[num] = t;
				num++;
			}
			return array;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public void AddElement(int value)
		{
			LibraryApplicationInterface.INativeArray.AddIntegerElement(base.Pointer, value);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004D07 File Offset: 0x00002F07
		public void AddElement(float value)
		{
			LibraryApplicationInterface.INativeArray.AddFloatElement(base.Pointer, value);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004D1C File Offset: 0x00002F1C
		public void AddElement<T>(T value) where T : struct
		{
			int elementSize = Marshal.SizeOf<T>();
			Marshal.StructureToPtr<T>(value, NativeArray._temporaryData, false);
			LibraryApplicationInterface.INativeArray.AddElement(base.Pointer, NativeArray._temporaryData, elementSize);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004D51 File Offset: 0x00002F51
		public void Clear()
		{
			LibraryApplicationInterface.INativeArray.Clear(base.Pointer);
		}

		// Token: 0x04000060 RID: 96
		private static readonly IntPtr _temporaryData = Marshal.AllocHGlobal(16384);

		// Token: 0x04000061 RID: 97
		private const int TemporaryDataSize = 16384;

		// Token: 0x04000062 RID: 98
		private static readonly int DataPointerOffset = LibraryApplicationInterface.INativeArray.GetDataPointerOffset();
	}
}
