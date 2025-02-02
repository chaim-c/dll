using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000025 RID: 37
	internal class ManagedObjectOwner
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000041CC File Offset: 0x000023CC
		internal static int NumberOfAliveManagedObjects
		{
			get
			{
				return ManagedObjectOwner._numberOfAliveManagedObjects;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000041D4 File Offset: 0x000023D4
		static ManagedObjectOwner()
		{
			ManagedObjectOwner._managedObjectOwners = new Dictionary<int, WeakReference>();
			ManagedObjectOwner._lastframedeletedManagedObjects = new List<ManagedObjectOwner>();
			ManagedObjectOwner._managedObjectOwnerReferences = new HashSet<ManagedObjectOwner>();
			ManagedObjectOwner._lastframedeletedManagedObjectBuffer = new List<ManagedObjectOwner>(1024);
			ManagedObjectOwner._pool = new List<ManagedObjectOwner>(8192);
			ManagedObjectOwner._managedObjectOwnerWeakReferences = new List<WeakReference>(8192);
			for (int i = 0; i < 8192; i++)
			{
				ManagedObjectOwner item = new ManagedObjectOwner();
				ManagedObjectOwner._pool.Add(item);
				WeakReference item2 = new WeakReference(null);
				ManagedObjectOwner._managedObjectOwnerWeakReferences.Add(item2);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000426C File Offset: 0x0000246C
		internal static void GarbageCollect()
		{
			HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
			lock (managedObjectOwnerReferences)
			{
				ManagedObjectOwner._lastframedeletedManagedObjectBuffer.AddRange(ManagedObjectOwner._lastframedeletedManagedObjects);
				ManagedObjectOwner._lastframedeletedManagedObjects.Clear();
				foreach (ManagedObjectOwner managedObjectOwner in ManagedObjectOwner._lastframedeletedManagedObjectBuffer)
				{
					if (managedObjectOwner._ptr != UIntPtr.Zero)
					{
						LibraryApplicationInterface.IManaged.ReleaseManagedObject(managedObjectOwner._ptr);
						managedObjectOwner._ptr = UIntPtr.Zero;
					}
					ManagedObjectOwner._numberOfAliveManagedObjects--;
					WeakReference weakReference = ManagedObjectOwner._managedObjectOwners[managedObjectOwner.NativeId];
					ManagedObjectOwner._managedObjectOwners.Remove(managedObjectOwner.NativeId);
					weakReference.Target = null;
					ManagedObjectOwner._managedObjectOwnerWeakReferences.Add(weakReference);
				}
			}
			foreach (ManagedObjectOwner managedObjectOwner2 in ManagedObjectOwner._lastframedeletedManagedObjectBuffer)
			{
				managedObjectOwner2.Destruct();
				ManagedObjectOwner._pool.Add(managedObjectOwner2);
			}
			ManagedObjectOwner._lastframedeletedManagedObjectBuffer.Clear();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000043C4 File Offset: 0x000025C4
		internal static void LogFinalize()
		{
			Debug.Print("Checking if any managed object still lives...", 0, Debug.DebugColor.White, 17592186044416UL);
			int num = 0;
			HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
			lock (managedObjectOwnerReferences)
			{
				foreach (KeyValuePair<int, WeakReference> keyValuePair in ManagedObjectOwner._managedObjectOwners)
				{
					if (keyValuePair.Value.Target != null)
					{
						Debug.Print("An object with type of " + keyValuePair.Value.Target.GetType().Name + " still lives", 0, Debug.DebugColor.White, 17592186044416UL);
						num++;
					}
				}
			}
			if (num == 0)
			{
				Debug.Print("There are no living managed objects.", 0, Debug.DebugColor.White, 17592186044416UL);
				return;
			}
			Debug.Print("There are " + num + " living managed objects.", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000044D8 File Offset: 0x000026D8
		internal static void PreFinalizeManagedObjects()
		{
			ManagedObjectOwner.GarbageCollect();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000044E0 File Offset: 0x000026E0
		internal static ManagedObject GetManagedObjectWithId(int id)
		{
			if (id == 0)
			{
				return null;
			}
			HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
			lock (managedObjectOwnerReferences)
			{
				ManagedObjectOwner managedObjectOwner = ManagedObjectOwner._managedObjectOwners[id].Target as ManagedObjectOwner;
				if (managedObjectOwner != null)
				{
					ManagedObject managedObject = managedObjectOwner.TryGetManagedObject();
					ManagedObject.ManagedObjectFetched(managedObject);
					return managedObject;
				}
			}
			return null;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000454C File Offset: 0x0000274C
		internal static void ManagedObjectGarbageCollected(ManagedObjectOwner owner, ManagedObject managedObject)
		{
			HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
			lock (managedObjectOwnerReferences)
			{
				if (owner != null && owner._managedObjectLongReference.Target as ManagedObject == managedObject)
				{
					ManagedObjectOwner._lastframedeletedManagedObjects.Add(owner);
					ManagedObjectOwner._managedObjectOwnerReferences.Remove(owner);
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000045B4 File Offset: 0x000027B4
		internal static ManagedObjectOwner CreateManagedObjectOwner(UIntPtr ptr, ManagedObject managedObject)
		{
			ManagedObjectOwner managedObjectOwner = null;
			if (ManagedObjectOwner._pool.Count > 0)
			{
				managedObjectOwner = ManagedObjectOwner._pool[ManagedObjectOwner._pool.Count - 1];
				ManagedObjectOwner._pool.RemoveAt(ManagedObjectOwner._pool.Count - 1);
			}
			else
			{
				managedObjectOwner = new ManagedObjectOwner();
			}
			managedObjectOwner.Construct(ptr, managedObject);
			HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
			lock (managedObjectOwnerReferences)
			{
				ManagedObjectOwner._numberOfAliveManagedObjects++;
				ManagedObjectOwner._managedObjectOwnerReferences.Add(managedObjectOwner);
			}
			return managedObjectOwner;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004654 File Offset: 0x00002854
		internal static string GetAliveManagedObjectNames()
		{
			string text = "";
			HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
			lock (managedObjectOwnerReferences)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (WeakReference weakReference in ManagedObjectOwner._managedObjectOwners.Values)
				{
					ManagedObjectOwner managedObjectOwner = weakReference.Target as ManagedObjectOwner;
					if (!dictionary.ContainsKey(managedObjectOwner._typeInfo.Name))
					{
						dictionary.Add(managedObjectOwner._typeInfo.Name, 1);
					}
					else
					{
						dictionary[managedObjectOwner._typeInfo.Name] = dictionary[managedObjectOwner._typeInfo.Name] + 1;
					}
				}
				foreach (string text2 in dictionary.Keys)
				{
					text = string.Concat(new object[]
					{
						text,
						text2,
						",",
						dictionary[text2],
						"-"
					});
				}
			}
			return text;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000047C8 File Offset: 0x000029C8
		internal static string GetAliveManagedObjectCreationCallstacks(string name)
		{
			return "";
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000047CF File Offset: 0x000029CF
		internal int NativeId
		{
			get
			{
				return this._nativeId;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000047D7 File Offset: 0x000029D7
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000047DF File Offset: 0x000029DF
		internal UIntPtr Pointer
		{
			get
			{
				return this._ptr;
			}
			set
			{
				if (value != UIntPtr.Zero)
				{
					LibraryApplicationInterface.IManaged.IncreaseReferenceCount(value);
				}
				this._ptr = value;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004800 File Offset: 0x00002A00
		private ManagedObjectOwner()
		{
			this._ptr = UIntPtr.Zero;
			this._managedObject = new WeakReference(null, false);
			this._managedObjectLongReference = new WeakReference(null, true);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004830 File Offset: 0x00002A30
		private void Construct(UIntPtr ptr, ManagedObject managedObject)
		{
			this._typeInfo = managedObject.GetType();
			this._managedObject.Target = managedObject;
			this._managedObjectLongReference.Target = managedObject;
			this.Pointer = ptr;
			HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
			lock (managedObjectOwnerReferences)
			{
				this._nativeId = ManagedObjectOwner._lastId;
				ManagedObjectOwner._lastId++;
				WeakReference weakReference;
				if (ManagedObjectOwner._managedObjectOwnerWeakReferences.Count > 0)
				{
					weakReference = ManagedObjectOwner._managedObjectOwnerWeakReferences[ManagedObjectOwner._managedObjectOwnerWeakReferences.Count - 1];
					ManagedObjectOwner._managedObjectOwnerWeakReferences.RemoveAt(ManagedObjectOwner._managedObjectOwnerWeakReferences.Count - 1);
					weakReference.Target = this;
				}
				else
				{
					weakReference = new WeakReference(this);
				}
				ManagedObjectOwner._managedObjectOwners.Add(this.NativeId, weakReference);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004908 File Offset: 0x00002B08
		private void Destruct()
		{
			this._managedObject.Target = null;
			this._managedObjectLongReference.Target = null;
			this._typeInfo = null;
			this._ptr = UIntPtr.Zero;
			this._nativeId = 0;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000493C File Offset: 0x00002B3C
		protected override void Finalize()
		{
			try
			{
				HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
				lock (managedObjectOwnerReferences)
				{
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004988 File Offset: 0x00002B88
		private ManagedObject TryGetManagedObject()
		{
			ManagedObject managedObject = null;
			HashSet<ManagedObjectOwner> managedObjectOwnerReferences = ManagedObjectOwner._managedObjectOwnerReferences;
			lock (managedObjectOwnerReferences)
			{
				managedObject = (this._managedObject.Target as ManagedObject);
				if (managedObject == null)
				{
					managedObject = (ManagedObject)this._typeInfo.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(UIntPtr),
						typeof(bool)
					}, null).Invoke(new object[]
					{
						this._ptr,
						false
					});
					managedObject.SetOwnerManagedObject(this);
					this._managedObject.Target = managedObject;
					this._managedObjectLongReference.Target = managedObject;
					if (!ManagedObjectOwner._managedObjectOwnerReferences.Contains(this))
					{
						ManagedObjectOwner._managedObjectOwnerReferences.Add(this);
					}
					ManagedObjectOwner._lastframedeletedManagedObjects.Remove(this);
				}
			}
			return managedObject;
		}

		// Token: 0x0400004D RID: 77
		private const int PooledManagedObjectOwnerCount = 8192;

		// Token: 0x0400004E RID: 78
		private static readonly List<ManagedObjectOwner> _pool;

		// Token: 0x0400004F RID: 79
		private static readonly List<WeakReference> _managedObjectOwnerWeakReferences;

		// Token: 0x04000050 RID: 80
		private static readonly Dictionary<int, WeakReference> _managedObjectOwners;

		// Token: 0x04000051 RID: 81
		private static readonly HashSet<ManagedObjectOwner> _managedObjectOwnerReferences;

		// Token: 0x04000052 RID: 82
		private static int _lastId = 10;

		// Token: 0x04000053 RID: 83
		private static readonly List<ManagedObjectOwner> _lastframedeletedManagedObjects;

		// Token: 0x04000054 RID: 84
		private static int _numberOfAliveManagedObjects = 0;

		// Token: 0x04000055 RID: 85
		private static readonly List<ManagedObjectOwner> _lastframedeletedManagedObjectBuffer;

		// Token: 0x04000056 RID: 86
		private Type _typeInfo;

		// Token: 0x04000057 RID: 87
		private int _nativeId;

		// Token: 0x04000058 RID: 88
		private UIntPtr _ptr;

		// Token: 0x04000059 RID: 89
		private readonly WeakReference _managedObject;

		// Token: 0x0400005A RID: 90
		private readonly WeakReference _managedObjectLongReference;
	}
}
