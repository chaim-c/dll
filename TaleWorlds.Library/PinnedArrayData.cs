using System;
using System.Runtime.InteropServices;

namespace TaleWorlds.Library
{
	// Token: 0x02000075 RID: 117
	public struct PinnedArrayData<T>
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000D157 File Offset: 0x0000B357
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000D15F File Offset: 0x0000B35F
		public bool Pinned { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000D168 File Offset: 0x0000B368
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0000D170 File Offset: 0x0000B370
		public IntPtr Pointer { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000D179 File Offset: 0x0000B379
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000D181 File Offset: 0x0000B381
		public T[] Array { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000D18A File Offset: 0x0000B38A
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000D192 File Offset: 0x0000B392
		public T[,] Array2D { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000D19B File Offset: 0x0000B39B
		public GCHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		public PinnedArrayData(T[] array, bool manualPinning = false)
		{
			this.Array = array;
			this.Array2D = null;
			this.Pinned = false;
			this.Pointer = IntPtr.Zero;
			if (array != null)
			{
				if (!manualPinning)
				{
					try
					{
						this._handle = GCHandleFactory.GetHandle();
						this._handle.Target = array;
						this.Pointer = this.Handle.AddrOfPinnedObject();
						this.Pinned = true;
					}
					catch (ArgumentException)
					{
						manualPinning = true;
					}
				}
				if (manualPinning)
				{
					this.Pinned = false;
					int num = Marshal.SizeOf<T>();
					for (int i = 0; i < array.Length; i++)
					{
						Marshal.StructureToPtr<T>(array[i], PinnedArrayData<T>._unmanagedCache + num * i, false);
					}
					this.Pointer = PinnedArrayData<T>._unmanagedCache;
				}
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000D268 File Offset: 0x0000B468
		public PinnedArrayData(T[,] array, bool manualPinning = false)
		{
			this.Array = null;
			this.Array2D = array;
			this.Pinned = false;
			this.Pointer = IntPtr.Zero;
			if (array != null)
			{
				if (!manualPinning)
				{
					try
					{
						this._handle = GCHandleFactory.GetHandle();
						this._handle.Target = array;
						this.Pointer = this.Handle.AddrOfPinnedObject();
						this.Pinned = true;
					}
					catch (ArgumentException)
					{
						manualPinning = true;
					}
				}
				if (manualPinning)
				{
					this.Pinned = false;
					int num = Marshal.SizeOf<T>();
					for (int i = 0; i < array.GetLength(0); i++)
					{
						for (int j = 0; j < array.GetLength(1); j++)
						{
							Marshal.StructureToPtr<T>(array[i, j], PinnedArrayData<T>._unmanagedCache + num * (i * array.GetLength(1) + j), false);
						}
					}
					this.Pointer = PinnedArrayData<T>._unmanagedCache;
				}
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000D34C File Offset: 0x0000B54C
		public static bool CheckIfTypeRequiresManualPinning(Type type)
		{
			bool result = false;
			Array value = System.Array.CreateInstance(type, 10);
			GCHandle gchandle;
			try
			{
				gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
				gchandle.AddrOfPinnedObject();
			}
			catch (ArgumentException)
			{
				result = true;
			}
			if (gchandle.IsAllocated)
			{
				gchandle.Free();
			}
			return result;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000D39C File Offset: 0x0000B59C
		public void Dispose()
		{
			if (this.Pinned)
			{
				if (this.Array != null)
				{
					this._handle.Target = null;
					GCHandleFactory.ReturnHandle(this._handle);
					this.Array = null;
					this.Pointer = IntPtr.Zero;
					return;
				}
				if (this.Array2D != null)
				{
					this._handle.Target = null;
					GCHandleFactory.ReturnHandle(this._handle);
					this.Array2D = null;
					this.Pointer = IntPtr.Zero;
				}
			}
		}

		// Token: 0x0400012E RID: 302
		private static IntPtr _unmanagedCache = Marshal.AllocHGlobal(16384);

		// Token: 0x04000133 RID: 307
		private GCHandle _handle;
	}
}
