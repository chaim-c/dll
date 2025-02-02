using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000029 RID: 41
	public abstract class NativeObject
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004D63 File Offset: 0x00002F63
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004D6B File Offset: 0x00002F6B
		public UIntPtr Pointer { get; private set; }

		// Token: 0x06000103 RID: 259 RVA: 0x00004D7C File Offset: 0x00002F7C
		internal void Construct(UIntPtr pointer)
		{
			this.Pointer = pointer;
			LibraryApplicationInterface.IManaged.IncreaseReferenceCount(this.Pointer);
			List<List<NativeObject>> nativeObjectFirstReferences = NativeObject._nativeObjectFirstReferences;
			lock (nativeObjectFirstReferences)
			{
				NativeObject._nativeObjectFirstReferences[0].Add(this);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004DE0 File Offset: 0x00002FE0
		~NativeObject()
		{
			if (!this._manualInvalidated)
			{
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(this.Pointer);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004E20 File Offset: 0x00003020
		public void ManualInvalidate()
		{
			if (!this._manualInvalidated)
			{
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(this.Pointer);
				this._manualInvalidated = true;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004E44 File Offset: 0x00003044
		static NativeObject()
		{
			int classTypeDefinitionCount = LibraryApplicationInterface.IManaged.GetClassTypeDefinitionCount();
			NativeObject._typeDefinitions = new List<EngineClassTypeDefinition>();
			NativeObject._constructors = new List<ConstructorInfo>();
			for (int i = 0; i < classTypeDefinitionCount; i++)
			{
				EngineClassTypeDefinition item = default(EngineClassTypeDefinition);
				LibraryApplicationInterface.IManaged.GetClassTypeDefinition(i, ref item);
				NativeObject._typeDefinitions.Add(item);
				NativeObject._constructors.Add(null);
			}
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					if (NativeObject.DoesNativeObjectDefinedAssembly(assembly))
					{
						foreach (Type type in assembly.GetTypes())
						{
							if (type.GetCustomAttributesSafe(typeof(EngineClass), false).Length == 1)
							{
								list.Add(type);
							}
						}
					}
				}
				catch (Exception)
				{
				}
			}
			foreach (Type type2 in list)
			{
				EngineClass engineClass = (EngineClass)type2.GetCustomAttributesSafe(typeof(EngineClass), false)[0];
				if (!type2.IsAbstract)
				{
					ConstructorInfo constructor = type2.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(UIntPtr)
					}, null);
					int typeDefinitionId = NativeObject.GetTypeDefinitionId(engineClass.EngineType);
					if (typeDefinitionId != -1)
					{
						NativeObject._constructors[typeDefinitionId] = constructor;
					}
				}
			}
			NativeObject._nativeObjectFirstReferences = new List<List<NativeObject>>();
			for (int l = 0; l < 10; l++)
			{
				NativeObject._nativeObjectFirstReferences.Add(new List<NativeObject>());
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004FFC File Offset: 0x000031FC
		internal static void HandleNativeObjects()
		{
			List<List<NativeObject>> nativeObjectFirstReferences = NativeObject._nativeObjectFirstReferences;
			lock (nativeObjectFirstReferences)
			{
				List<NativeObject> list = NativeObject._nativeObjectFirstReferences[9];
				for (int i = 9; i > 0; i--)
				{
					NativeObject._nativeObjectFirstReferences[i] = NativeObject._nativeObjectFirstReferences[i - 1];
				}
				list.Clear();
				NativeObject._nativeObjectFirstReferences[0] = list;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000507C File Offset: 0x0000327C
		[LibraryCallback]
		internal static int GetAliveNativeObjectCount()
		{
			return NativeObject._numberOfAliveNativeObjects;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005085 File Offset: 0x00003285
		[LibraryCallback]
		internal static string GetAliveNativeObjectNames()
		{
			return "";
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000508C File Offset: 0x0000328C
		private static int GetTypeDefinitionId(string typeName)
		{
			foreach (EngineClassTypeDefinition engineClassTypeDefinition in NativeObject._typeDefinitions)
			{
				if (engineClassTypeDefinition.TypeName == typeName)
				{
					return engineClassTypeDefinition.TypeId;
				}
			}
			return -1;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000050F4 File Offset: 0x000032F4
		private static bool DoesNativeObjectDefinedAssembly(Assembly assembly)
		{
			if (typeof(NativeObject).Assembly.FullName == assembly.FullName)
			{
				return true;
			}
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			string fullName = typeof(NativeObject).Assembly.FullName;
			AssemblyName[] array = referencedAssemblies;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].FullName == fullName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005161 File Offset: 0x00003361
		[Obsolete]
		protected void AddUnmanagedMemoryPressure(int size)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005164 File Offset: 0x00003364
		internal static NativeObject CreateNativeObjectWrapper(NativeObjectPointer nativeObjectPointer)
		{
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				ConstructorInfo constructorInfo = NativeObject._constructors[nativeObjectPointer.TypeId];
				if (constructorInfo != null)
				{
					return (NativeObject)constructorInfo.Invoke(new object[]
					{
						nativeObjectPointer.Pointer
					});
				}
			}
			return null;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000051BE File Offset: 0x000033BE
		internal static T CreateNativeObjectWrapper<T>(NativeObjectPointer nativeObjectPointer) where T : NativeObject
		{
			return (T)((object)NativeObject.CreateNativeObjectWrapper(nativeObjectPointer));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000051CC File Offset: 0x000033CC
		public override int GetHashCode()
		{
			return this.Pointer.GetHashCode();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000051E7 File Offset: 0x000033E7
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && ((NativeObject)obj).Pointer == this.Pointer;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005219 File Offset: 0x00003419
		public static bool operator ==(NativeObject a, NativeObject b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005230 File Offset: 0x00003430
		public static bool operator !=(NativeObject a, NativeObject b)
		{
			return !(a == b);
		}

		// Token: 0x04000063 RID: 99
		private static List<EngineClassTypeDefinition> _typeDefinitions;

		// Token: 0x04000064 RID: 100
		private static List<ConstructorInfo> _constructors;

		// Token: 0x04000065 RID: 101
		private const int NativeObjectFirstReferencesTickCount = 10;

		// Token: 0x04000066 RID: 102
		private static List<List<NativeObject>> _nativeObjectFirstReferences;

		// Token: 0x04000067 RID: 103
		private static volatile int _numberOfAliveNativeObjects;

		// Token: 0x04000068 RID: 104
		private bool _manualInvalidated;
	}
}
