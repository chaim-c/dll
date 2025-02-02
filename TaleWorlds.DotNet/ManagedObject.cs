using System;
using System.Collections.Generic;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000024 RID: 36
	public abstract class ManagedObject
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003F2A File Offset: 0x0000212A
		internal ManagedObjectOwner ManagedObjectOwner
		{
			get
			{
				return this._managedObjectOwner;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003F34 File Offset: 0x00002134
		internal static void FinalizeManagedObjects()
		{
			List<List<ManagedObject>> managedObjectFirstReferences = ManagedObject._managedObjectFirstReferences;
			lock (managedObjectFirstReferences)
			{
				ManagedObject._managedObjectFirstReferences.Clear();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003F78 File Offset: 0x00002178
		protected void AddUnmanagedMemoryPressure(int size)
		{
			GC.AddMemoryPressure((long)size);
			this.forcedMemory = size;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003F88 File Offset: 0x00002188
		static ManagedObject()
		{
			for (int i = 0; i < 200; i++)
			{
				ManagedObject._managedObjectFirstReferences.Add(new List<ManagedObject>());
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003FBE File Offset: 0x000021BE
		protected ManagedObject(UIntPtr ptr, bool createManagedObjectOwner)
		{
			if (createManagedObjectOwner)
			{
				this.SetOwnerManagedObject(ManagedObjectOwner.CreateManagedObjectOwner(ptr, this));
			}
			this.Initialize();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003FDC File Offset: 0x000021DC
		internal void SetOwnerManagedObject(ManagedObjectOwner owner)
		{
			this._managedObjectOwner = owner;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003FE5 File Offset: 0x000021E5
		private void Initialize()
		{
			ManagedObject.ManagedObjectFetched(this);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003FF0 File Offset: 0x000021F0
		~ManagedObject()
		{
			if (this.forcedMemory > 0)
			{
				GC.RemoveMemoryPressure((long)this.forcedMemory);
			}
			ManagedObjectOwner.ManagedObjectGarbageCollected(this._managedObjectOwner, this);
			this._managedObjectOwner = null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004040 File Offset: 0x00002240
		internal static void HandleManagedObjects()
		{
			List<List<ManagedObject>> managedObjectFirstReferences = ManagedObject._managedObjectFirstReferences;
			lock (managedObjectFirstReferences)
			{
				List<ManagedObject> list = ManagedObject._managedObjectFirstReferences[199];
				for (int i = 199; i > 0; i--)
				{
					ManagedObject._managedObjectFirstReferences[i] = ManagedObject._managedObjectFirstReferences[i - 1];
				}
				list.Clear();
				ManagedObject._managedObjectFirstReferences[0] = list;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000040C4 File Offset: 0x000022C4
		internal static void ManagedObjectFetched(ManagedObject managedObject)
		{
			List<List<ManagedObject>> managedObjectFirstReferences = ManagedObject._managedObjectFirstReferences;
			lock (managedObjectFirstReferences)
			{
				if (!Managed.Closing)
				{
					ManagedObject._managedObjectFirstReferences[0].Add(managedObject);
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004118 File Offset: 0x00002318
		internal static void FlushManagedObjects()
		{
			List<List<ManagedObject>> managedObjectFirstReferences = ManagedObject._managedObjectFirstReferences;
			lock (managedObjectFirstReferences)
			{
				for (int i = 0; i < 200; i++)
				{
					ManagedObject._managedObjectFirstReferences[i].Clear();
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004174 File Offset: 0x00002374
		[LibraryCallback]
		internal static int GetAliveManagedObjectCount()
		{
			return ManagedObjectOwner.NumberOfAliveManagedObjects;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000417B File Offset: 0x0000237B
		[LibraryCallback]
		internal static string GetAliveManagedObjectNames()
		{
			return ManagedObjectOwner.GetAliveManagedObjectNames();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004182 File Offset: 0x00002382
		[LibraryCallback]
		internal static string GetCreationCallstack(string name)
		{
			return ManagedObjectOwner.GetAliveManagedObjectCreationCallstacks(name);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000418A File Offset: 0x0000238A
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004197 File Offset: 0x00002397
		internal UIntPtr Pointer
		{
			get
			{
				return this._managedObjectOwner.Pointer;
			}
			set
			{
				this._managedObjectOwner.Pointer = value;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000041A5 File Offset: 0x000023A5
		public int GetManagedId()
		{
			return this._managedObjectOwner.NativeId;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000041B2 File Offset: 0x000023B2
		[LibraryCallback]
		internal string GetClassOfObject()
		{
			return base.GetType().Name;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000041BF File Offset: 0x000023BF
		public override int GetHashCode()
		{
			return this._managedObjectOwner.NativeId;
		}

		// Token: 0x04000049 RID: 73
		private const int ManagedObjectFirstReferencesTickCount = 200;

		// Token: 0x0400004A RID: 74
		private static List<List<ManagedObject>> _managedObjectFirstReferences = new List<List<ManagedObject>>();

		// Token: 0x0400004B RID: 75
		private ManagedObjectOwner _managedObjectOwner;

		// Token: 0x0400004C RID: 76
		private int forcedMemory;
	}
}
