using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000021 RID: 33
	internal class ScriptingInterfaceOfIPhysicsShape : IPhysicsShape
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00012534 File Offset: 0x00010734
		public void AddCapsule(UIntPtr shapePointer, ref CapsuleData data)
		{
			ScriptingInterfaceOfIPhysicsShape.call_AddCapsuleDelegate(shapePointer, ref data);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00012544 File Offset: 0x00010744
		public void AddPreloadQueueWithName(string bodyName, ref Vec3 scale)
		{
			byte[] array = null;
			if (bodyName != null)
			{
				int byteCount = ScriptingInterfaceOfIPhysicsShape._utf8.GetByteCount(bodyName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIPhysicsShape._utf8.GetBytes(bodyName, 0, bodyName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIPhysicsShape.call_AddPreloadQueueWithNameDelegate(array, ref scale);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001259F File Offset: 0x0001079F
		public void AddSphere(UIntPtr shapePointer, ref Vec3 origin, float radius)
		{
			ScriptingInterfaceOfIPhysicsShape.call_AddSphereDelegate(shapePointer, ref origin, radius);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000125AE File Offset: 0x000107AE
		public int CapsuleCount(UIntPtr shapePointer)
		{
			return ScriptingInterfaceOfIPhysicsShape.call_CapsuleCountDelegate(shapePointer);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000125BB File Offset: 0x000107BB
		public void clear(UIntPtr shapePointer)
		{
			ScriptingInterfaceOfIPhysicsShape.call_clearDelegate(shapePointer);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000125C8 File Offset: 0x000107C8
		public PhysicsShape CreateBodyCopy(UIntPtr bodyPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIPhysicsShape.call_CreateBodyCopyDelegate(bodyPointer);
			PhysicsShape result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new PhysicsShape(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00012612 File Offset: 0x00010812
		public Vec3 GetBoundingBoxCenter(UIntPtr shapePointer)
		{
			return ScriptingInterfaceOfIPhysicsShape.call_GetBoundingBoxCenterDelegate(shapePointer);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001261F File Offset: 0x0001081F
		public void GetCapsule(UIntPtr shapePointer, ref CapsuleData data, int index)
		{
			ScriptingInterfaceOfIPhysicsShape.call_GetCapsuleDelegate(shapePointer, ref data, index);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001262E File Offset: 0x0001082E
		public void GetCapsuleWithMaterial(UIntPtr shapePointer, ref CapsuleData data, ref int materialIndex, int index)
		{
			ScriptingInterfaceOfIPhysicsShape.call_GetCapsuleWithMaterialDelegate(shapePointer, ref data, ref materialIndex, index);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00012640 File Offset: 0x00010840
		public int GetDominantMaterialForTriangleMesh(PhysicsShape shape, int meshIndex)
		{
			UIntPtr shape2 = (shape != null) ? shape.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIPhysicsShape.call_GetDominantMaterialForTriangleMeshDelegate(shape2, meshIndex);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00012670 File Offset: 0x00010870
		public PhysicsShape GetFromResource(string bodyName, bool mayReturnNull)
		{
			byte[] array = null;
			if (bodyName != null)
			{
				int byteCount = ScriptingInterfaceOfIPhysicsShape._utf8.GetByteCount(bodyName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIPhysicsShape._utf8.GetBytes(bodyName, 0, bodyName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIPhysicsShape.call_GetFromResourceDelegate(array, mayReturnNull);
			PhysicsShape result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new PhysicsShape(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00012700 File Offset: 0x00010900
		public string GetName(PhysicsShape shape)
		{
			UIntPtr shape2 = (shape != null) ? shape.Pointer : UIntPtr.Zero;
			if (ScriptingInterfaceOfIPhysicsShape.call_GetNameDelegate(shape2) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00012739 File Offset: 0x00010939
		public void GetSphere(UIntPtr shapePointer, ref SphereData data, int sphereIndex)
		{
			ScriptingInterfaceOfIPhysicsShape.call_GetSphereDelegate(shapePointer, ref data, sphereIndex);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00012748 File Offset: 0x00010948
		public void GetSphereWithMaterial(UIntPtr shapePointer, ref SphereData data, ref int materialIndex, int sphereIndex)
		{
			ScriptingInterfaceOfIPhysicsShape.call_GetSphereWithMaterialDelegate(shapePointer, ref data, ref materialIndex, sphereIndex);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001275C File Offset: 0x0001095C
		public void GetTriangle(UIntPtr pointer, Vec3[] data, int meshIndex, int triangleIndex)
		{
			PinnedArrayData<Vec3> pinnedArrayData = new PinnedArrayData<Vec3>(data, false);
			IntPtr pointer2 = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIPhysicsShape.call_GetTriangleDelegate(pointer, pointer2, meshIndex, triangleIndex);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012790 File Offset: 0x00010990
		public void InitDescription(UIntPtr shapePointer)
		{
			ScriptingInterfaceOfIPhysicsShape.call_InitDescriptionDelegate(shapePointer);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001279D File Offset: 0x0001099D
		public void Prepare(UIntPtr shapePointer)
		{
			ScriptingInterfaceOfIPhysicsShape.call_PrepareDelegate(shapePointer);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000127AA File Offset: 0x000109AA
		public void ProcessPreloadQueue()
		{
			ScriptingInterfaceOfIPhysicsShape.call_ProcessPreloadQueueDelegate();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000127B6 File Offset: 0x000109B6
		public void SetCapsule(UIntPtr shapePointer, ref CapsuleData data, int index)
		{
			ScriptingInterfaceOfIPhysicsShape.call_SetCapsuleDelegate(shapePointer, ref data, index);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000127C5 File Offset: 0x000109C5
		public int SphereCount(UIntPtr pointer)
		{
			return ScriptingInterfaceOfIPhysicsShape.call_SphereCountDelegate(pointer);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000127D2 File Offset: 0x000109D2
		public void Transform(UIntPtr shapePointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIPhysicsShape.call_TransformDelegate(shapePointer, ref frame);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000127E0 File Offset: 0x000109E0
		public int TriangleCountInTriangleMesh(UIntPtr pointer, int meshIndex)
		{
			return ScriptingInterfaceOfIPhysicsShape.call_TriangleCountInTriangleMeshDelegate(pointer, meshIndex);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000127EE File Offset: 0x000109EE
		public int TriangleMeshCount(UIntPtr pointer)
		{
			return ScriptingInterfaceOfIPhysicsShape.call_TriangleMeshCountDelegate(pointer);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000127FB File Offset: 0x000109FB
		public void UnloadDynamicBodies()
		{
			ScriptingInterfaceOfIPhysicsShape.call_UnloadDynamicBodiesDelegate();
		}

		// Token: 0x040002CC RID: 716
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040002CD RID: 717
		public static ScriptingInterfaceOfIPhysicsShape.AddCapsuleDelegate call_AddCapsuleDelegate;

		// Token: 0x040002CE RID: 718
		public static ScriptingInterfaceOfIPhysicsShape.AddPreloadQueueWithNameDelegate call_AddPreloadQueueWithNameDelegate;

		// Token: 0x040002CF RID: 719
		public static ScriptingInterfaceOfIPhysicsShape.AddSphereDelegate call_AddSphereDelegate;

		// Token: 0x040002D0 RID: 720
		public static ScriptingInterfaceOfIPhysicsShape.CapsuleCountDelegate call_CapsuleCountDelegate;

		// Token: 0x040002D1 RID: 721
		public static ScriptingInterfaceOfIPhysicsShape.clearDelegate call_clearDelegate;

		// Token: 0x040002D2 RID: 722
		public static ScriptingInterfaceOfIPhysicsShape.CreateBodyCopyDelegate call_CreateBodyCopyDelegate;

		// Token: 0x040002D3 RID: 723
		public static ScriptingInterfaceOfIPhysicsShape.GetBoundingBoxCenterDelegate call_GetBoundingBoxCenterDelegate;

		// Token: 0x040002D4 RID: 724
		public static ScriptingInterfaceOfIPhysicsShape.GetCapsuleDelegate call_GetCapsuleDelegate;

		// Token: 0x040002D5 RID: 725
		public static ScriptingInterfaceOfIPhysicsShape.GetCapsuleWithMaterialDelegate call_GetCapsuleWithMaterialDelegate;

		// Token: 0x040002D6 RID: 726
		public static ScriptingInterfaceOfIPhysicsShape.GetDominantMaterialForTriangleMeshDelegate call_GetDominantMaterialForTriangleMeshDelegate;

		// Token: 0x040002D7 RID: 727
		public static ScriptingInterfaceOfIPhysicsShape.GetFromResourceDelegate call_GetFromResourceDelegate;

		// Token: 0x040002D8 RID: 728
		public static ScriptingInterfaceOfIPhysicsShape.GetNameDelegate call_GetNameDelegate;

		// Token: 0x040002D9 RID: 729
		public static ScriptingInterfaceOfIPhysicsShape.GetSphereDelegate call_GetSphereDelegate;

		// Token: 0x040002DA RID: 730
		public static ScriptingInterfaceOfIPhysicsShape.GetSphereWithMaterialDelegate call_GetSphereWithMaterialDelegate;

		// Token: 0x040002DB RID: 731
		public static ScriptingInterfaceOfIPhysicsShape.GetTriangleDelegate call_GetTriangleDelegate;

		// Token: 0x040002DC RID: 732
		public static ScriptingInterfaceOfIPhysicsShape.InitDescriptionDelegate call_InitDescriptionDelegate;

		// Token: 0x040002DD RID: 733
		public static ScriptingInterfaceOfIPhysicsShape.PrepareDelegate call_PrepareDelegate;

		// Token: 0x040002DE RID: 734
		public static ScriptingInterfaceOfIPhysicsShape.ProcessPreloadQueueDelegate call_ProcessPreloadQueueDelegate;

		// Token: 0x040002DF RID: 735
		public static ScriptingInterfaceOfIPhysicsShape.SetCapsuleDelegate call_SetCapsuleDelegate;

		// Token: 0x040002E0 RID: 736
		public static ScriptingInterfaceOfIPhysicsShape.SphereCountDelegate call_SphereCountDelegate;

		// Token: 0x040002E1 RID: 737
		public static ScriptingInterfaceOfIPhysicsShape.TransformDelegate call_TransformDelegate;

		// Token: 0x040002E2 RID: 738
		public static ScriptingInterfaceOfIPhysicsShape.TriangleCountInTriangleMeshDelegate call_TriangleCountInTriangleMeshDelegate;

		// Token: 0x040002E3 RID: 739
		public static ScriptingInterfaceOfIPhysicsShape.TriangleMeshCountDelegate call_TriangleMeshCountDelegate;

		// Token: 0x040002E4 RID: 740
		public static ScriptingInterfaceOfIPhysicsShape.UnloadDynamicBodiesDelegate call_UnloadDynamicBodiesDelegate;

		// Token: 0x02000325 RID: 805
		// (Invoke) Token: 0x06001197 RID: 4503
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddCapsuleDelegate(UIntPtr shapePointer, ref CapsuleData data);

		// Token: 0x02000326 RID: 806
		// (Invoke) Token: 0x0600119B RID: 4507
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddPreloadQueueWithNameDelegate(byte[] bodyName, ref Vec3 scale);

		// Token: 0x02000327 RID: 807
		// (Invoke) Token: 0x0600119F RID: 4511
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddSphereDelegate(UIntPtr shapePointer, ref Vec3 origin, float radius);

		// Token: 0x02000328 RID: 808
		// (Invoke) Token: 0x060011A3 RID: 4515
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int CapsuleCountDelegate(UIntPtr shapePointer);

		// Token: 0x02000329 RID: 809
		// (Invoke) Token: 0x060011A7 RID: 4519
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void clearDelegate(UIntPtr shapePointer);

		// Token: 0x0200032A RID: 810
		// (Invoke) Token: 0x060011AB RID: 4523
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateBodyCopyDelegate(UIntPtr bodyPointer);

		// Token: 0x0200032B RID: 811
		// (Invoke) Token: 0x060011AF RID: 4527
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetBoundingBoxCenterDelegate(UIntPtr shapePointer);

		// Token: 0x0200032C RID: 812
		// (Invoke) Token: 0x060011B3 RID: 4531
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetCapsuleDelegate(UIntPtr shapePointer, ref CapsuleData data, int index);

		// Token: 0x0200032D RID: 813
		// (Invoke) Token: 0x060011B7 RID: 4535
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetCapsuleWithMaterialDelegate(UIntPtr shapePointer, ref CapsuleData data, ref int materialIndex, int index);

		// Token: 0x0200032E RID: 814
		// (Invoke) Token: 0x060011BB RID: 4539
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetDominantMaterialForTriangleMeshDelegate(UIntPtr shape, int meshIndex);

		// Token: 0x0200032F RID: 815
		// (Invoke) Token: 0x060011BF RID: 4543
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFromResourceDelegate(byte[] bodyName, [MarshalAs(UnmanagedType.U1)] bool mayReturnNull);

		// Token: 0x02000330 RID: 816
		// (Invoke) Token: 0x060011C3 RID: 4547
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr shape);

		// Token: 0x02000331 RID: 817
		// (Invoke) Token: 0x060011C7 RID: 4551
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetSphereDelegate(UIntPtr shapePointer, ref SphereData data, int sphereIndex);

		// Token: 0x02000332 RID: 818
		// (Invoke) Token: 0x060011CB RID: 4555
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetSphereWithMaterialDelegate(UIntPtr shapePointer, ref SphereData data, ref int materialIndex, int sphereIndex);

		// Token: 0x02000333 RID: 819
		// (Invoke) Token: 0x060011CF RID: 4559
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetTriangleDelegate(UIntPtr pointer, IntPtr data, int meshIndex, int triangleIndex);

		// Token: 0x02000334 RID: 820
		// (Invoke) Token: 0x060011D3 RID: 4563
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InitDescriptionDelegate(UIntPtr shapePointer);

		// Token: 0x02000335 RID: 821
		// (Invoke) Token: 0x060011D7 RID: 4567
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PrepareDelegate(UIntPtr shapePointer);

		// Token: 0x02000336 RID: 822
		// (Invoke) Token: 0x060011DB RID: 4571
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ProcessPreloadQueueDelegate();

		// Token: 0x02000337 RID: 823
		// (Invoke) Token: 0x060011DF RID: 4575
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCapsuleDelegate(UIntPtr shapePointer, ref CapsuleData data, int index);

		// Token: 0x02000338 RID: 824
		// (Invoke) Token: 0x060011E3 RID: 4579
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int SphereCountDelegate(UIntPtr pointer);

		// Token: 0x02000339 RID: 825
		// (Invoke) Token: 0x060011E7 RID: 4583
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TransformDelegate(UIntPtr shapePointer, ref MatrixFrame frame);

		// Token: 0x0200033A RID: 826
		// (Invoke) Token: 0x060011EB RID: 4587
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int TriangleCountInTriangleMeshDelegate(UIntPtr pointer, int meshIndex);

		// Token: 0x0200033B RID: 827
		// (Invoke) Token: 0x060011EF RID: 4591
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int TriangleMeshCountDelegate(UIntPtr pointer);

		// Token: 0x0200033C RID: 828
		// (Invoke) Token: 0x060011F3 RID: 4595
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UnloadDynamicBodiesDelegate();
	}
}
