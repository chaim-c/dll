using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200000C RID: 12
	internal class ScriptingInterfaceOfICompositeComponent : ICompositeComponent
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000D1A9 File Offset: 0x0000B3A9
		public void AddComponent(UIntPtr pointer, UIntPtr componentPointer)
		{
			ScriptingInterfaceOfICompositeComponent.call_AddComponentDelegate(pointer, componentPointer);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public void AddMultiMesh(UIntPtr compositeComponentPointer, string multiMeshName)
		{
			byte[] array = null;
			if (multiMeshName != null)
			{
				int byteCount = ScriptingInterfaceOfICompositeComponent._utf8.GetByteCount(multiMeshName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfICompositeComponent._utf8.GetBytes(multiMeshName, 0, multiMeshName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfICompositeComponent.call_AddMultiMeshDelegate(compositeComponentPointer, array);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000D214 File Offset: 0x0000B414
		public void AddPrefabEntity(UIntPtr pointer, UIntPtr scenePointer, string prefabName)
		{
			byte[] array = null;
			if (prefabName != null)
			{
				int byteCount = ScriptingInterfaceOfICompositeComponent._utf8.GetByteCount(prefabName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfICompositeComponent._utf8.GetBytes(prefabName, 0, prefabName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfICompositeComponent.call_AddPrefabEntityDelegate(pointer, scenePointer, array);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000D270 File Offset: 0x0000B470
		public CompositeComponent CreateCompositeComponent()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfICompositeComponent.call_CreateCompositeComponentDelegate();
			CompositeComponent result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new CompositeComponent(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		public CompositeComponent CreateCopy(UIntPtr pointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfICompositeComponent.call_CreateCopyDelegate(pointer);
			CompositeComponent result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new CompositeComponent(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000D306 File Offset: 0x0000B506
		public void GetBoundingBox(UIntPtr compositeComponentPointer, ref BoundingBox outBoundingBox)
		{
			ScriptingInterfaceOfICompositeComponent.call_GetBoundingBoxDelegate(compositeComponentPointer, ref outBoundingBox);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000D314 File Offset: 0x0000B514
		public uint GetFactor1(UIntPtr compositeComponentPointer)
		{
			return ScriptingInterfaceOfICompositeComponent.call_GetFactor1Delegate(compositeComponentPointer);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000D321 File Offset: 0x0000B521
		public uint GetFactor2(UIntPtr compositeComponentPointer)
		{
			return ScriptingInterfaceOfICompositeComponent.call_GetFactor2Delegate(compositeComponentPointer);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000D330 File Offset: 0x0000B530
		public MetaMesh GetFirstMetaMesh(UIntPtr compositeComponentPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfICompositeComponent.call_GetFirstMetaMeshDelegate(compositeComponentPointer);
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000D37A File Offset: 0x0000B57A
		public void GetFrame(UIntPtr compositeComponentPointer, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfICompositeComponent.call_GetFrameDelegate(compositeComponentPointer, ref outFrame);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000D388 File Offset: 0x0000B588
		public Vec3 GetVectorUserData(UIntPtr compositeComponentPointer)
		{
			return ScriptingInterfaceOfICompositeComponent.call_GetVectorUserDataDelegate(compositeComponentPointer);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000D395 File Offset: 0x0000B595
		public bool IsVisible(UIntPtr compositeComponentPointer)
		{
			return ScriptingInterfaceOfICompositeComponent.call_IsVisibleDelegate(compositeComponentPointer);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000D3A2 File Offset: 0x0000B5A2
		public void Release(UIntPtr compositeComponentPointer)
		{
			ScriptingInterfaceOfICompositeComponent.call_ReleaseDelegate(compositeComponentPointer);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000D3AF File Offset: 0x0000B5AF
		public void SetFactor1(UIntPtr compositeComponentPointer, uint factorColor1)
		{
			ScriptingInterfaceOfICompositeComponent.call_SetFactor1Delegate(compositeComponentPointer, factorColor1);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000D3BD File Offset: 0x0000B5BD
		public void SetFactor2(UIntPtr compositeComponentPointer, uint factorColor2)
		{
			ScriptingInterfaceOfICompositeComponent.call_SetFactor2Delegate(compositeComponentPointer, factorColor2);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000D3CB File Offset: 0x0000B5CB
		public void SetFrame(UIntPtr compositeComponentPointer, ref MatrixFrame meshFrame)
		{
			ScriptingInterfaceOfICompositeComponent.call_SetFrameDelegate(compositeComponentPointer, ref meshFrame);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000D3D9 File Offset: 0x0000B5D9
		public void SetMaterial(UIntPtr compositeComponentPointer, UIntPtr materialPointer)
		{
			ScriptingInterfaceOfICompositeComponent.call_SetMaterialDelegate(compositeComponentPointer, materialPointer);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
		public void SetVectorArgument(UIntPtr compositeComponentPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			ScriptingInterfaceOfICompositeComponent.call_SetVectorArgumentDelegate(compositeComponentPointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000D3FA File Offset: 0x0000B5FA
		public void SetVectorUserData(UIntPtr compositeComponentPointer, ref Vec3 vectorArg)
		{
			ScriptingInterfaceOfICompositeComponent.call_SetVectorUserDataDelegate(compositeComponentPointer, ref vectorArg);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000D408 File Offset: 0x0000B608
		public void SetVisibilityMask(UIntPtr compositeComponentPointer, VisibilityMaskFlags visibilityMask)
		{
			ScriptingInterfaceOfICompositeComponent.call_SetVisibilityMaskDelegate(compositeComponentPointer, visibilityMask);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000D416 File Offset: 0x0000B616
		public void SetVisible(UIntPtr compositeComponentPointer, bool visible)
		{
			ScriptingInterfaceOfICompositeComponent.call_SetVisibleDelegate(compositeComponentPointer, visible);
		}

		// Token: 0x04000028 RID: 40
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000029 RID: 41
		public static ScriptingInterfaceOfICompositeComponent.AddComponentDelegate call_AddComponentDelegate;

		// Token: 0x0400002A RID: 42
		public static ScriptingInterfaceOfICompositeComponent.AddMultiMeshDelegate call_AddMultiMeshDelegate;

		// Token: 0x0400002B RID: 43
		public static ScriptingInterfaceOfICompositeComponent.AddPrefabEntityDelegate call_AddPrefabEntityDelegate;

		// Token: 0x0400002C RID: 44
		public static ScriptingInterfaceOfICompositeComponent.CreateCompositeComponentDelegate call_CreateCompositeComponentDelegate;

		// Token: 0x0400002D RID: 45
		public static ScriptingInterfaceOfICompositeComponent.CreateCopyDelegate call_CreateCopyDelegate;

		// Token: 0x0400002E RID: 46
		public static ScriptingInterfaceOfICompositeComponent.GetBoundingBoxDelegate call_GetBoundingBoxDelegate;

		// Token: 0x0400002F RID: 47
		public static ScriptingInterfaceOfICompositeComponent.GetFactor1Delegate call_GetFactor1Delegate;

		// Token: 0x04000030 RID: 48
		public static ScriptingInterfaceOfICompositeComponent.GetFactor2Delegate call_GetFactor2Delegate;

		// Token: 0x04000031 RID: 49
		public static ScriptingInterfaceOfICompositeComponent.GetFirstMetaMeshDelegate call_GetFirstMetaMeshDelegate;

		// Token: 0x04000032 RID: 50
		public static ScriptingInterfaceOfICompositeComponent.GetFrameDelegate call_GetFrameDelegate;

		// Token: 0x04000033 RID: 51
		public static ScriptingInterfaceOfICompositeComponent.GetVectorUserDataDelegate call_GetVectorUserDataDelegate;

		// Token: 0x04000034 RID: 52
		public static ScriptingInterfaceOfICompositeComponent.IsVisibleDelegate call_IsVisibleDelegate;

		// Token: 0x04000035 RID: 53
		public static ScriptingInterfaceOfICompositeComponent.ReleaseDelegate call_ReleaseDelegate;

		// Token: 0x04000036 RID: 54
		public static ScriptingInterfaceOfICompositeComponent.SetFactor1Delegate call_SetFactor1Delegate;

		// Token: 0x04000037 RID: 55
		public static ScriptingInterfaceOfICompositeComponent.SetFactor2Delegate call_SetFactor2Delegate;

		// Token: 0x04000038 RID: 56
		public static ScriptingInterfaceOfICompositeComponent.SetFrameDelegate call_SetFrameDelegate;

		// Token: 0x04000039 RID: 57
		public static ScriptingInterfaceOfICompositeComponent.SetMaterialDelegate call_SetMaterialDelegate;

		// Token: 0x0400003A RID: 58
		public static ScriptingInterfaceOfICompositeComponent.SetVectorArgumentDelegate call_SetVectorArgumentDelegate;

		// Token: 0x0400003B RID: 59
		public static ScriptingInterfaceOfICompositeComponent.SetVectorUserDataDelegate call_SetVectorUserDataDelegate;

		// Token: 0x0400003C RID: 60
		public static ScriptingInterfaceOfICompositeComponent.SetVisibilityMaskDelegate call_SetVisibilityMaskDelegate;

		// Token: 0x0400003D RID: 61
		public static ScriptingInterfaceOfICompositeComponent.SetVisibleDelegate call_SetVisibleDelegate;

		// Token: 0x02000096 RID: 150
		// (Invoke) Token: 0x0600075B RID: 1883
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddComponentDelegate(UIntPtr pointer, UIntPtr componentPointer);

		// Token: 0x02000097 RID: 151
		// (Invoke) Token: 0x0600075F RID: 1887
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMultiMeshDelegate(UIntPtr compositeComponentPointer, byte[] multiMeshName);

		// Token: 0x02000098 RID: 152
		// (Invoke) Token: 0x06000763 RID: 1891
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddPrefabEntityDelegate(UIntPtr pointer, UIntPtr scenePointer, byte[] prefabName);

		// Token: 0x02000099 RID: 153
		// (Invoke) Token: 0x06000767 RID: 1895
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateCompositeComponentDelegate();

		// Token: 0x0200009A RID: 154
		// (Invoke) Token: 0x0600076B RID: 1899
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateCopyDelegate(UIntPtr pointer);

		// Token: 0x0200009B RID: 155
		// (Invoke) Token: 0x0600076F RID: 1903
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoundingBoxDelegate(UIntPtr compositeComponentPointer, ref BoundingBox outBoundingBox);

		// Token: 0x0200009C RID: 156
		// (Invoke) Token: 0x06000773 RID: 1907
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFactor1Delegate(UIntPtr compositeComponentPointer);

		// Token: 0x0200009D RID: 157
		// (Invoke) Token: 0x06000777 RID: 1911
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFactor2Delegate(UIntPtr compositeComponentPointer);

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x0600077B RID: 1915
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFirstMetaMeshDelegate(UIntPtr compositeComponentPointer);

		// Token: 0x0200009F RID: 159
		// (Invoke) Token: 0x0600077F RID: 1919
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetFrameDelegate(UIntPtr compositeComponentPointer, ref MatrixFrame outFrame);

		// Token: 0x020000A0 RID: 160
		// (Invoke) Token: 0x06000783 RID: 1923
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetVectorUserDataDelegate(UIntPtr compositeComponentPointer);

		// Token: 0x020000A1 RID: 161
		// (Invoke) Token: 0x06000787 RID: 1927
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsVisibleDelegate(UIntPtr compositeComponentPointer);

		// Token: 0x020000A2 RID: 162
		// (Invoke) Token: 0x0600078B RID: 1931
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseDelegate(UIntPtr compositeComponentPointer);

		// Token: 0x020000A3 RID: 163
		// (Invoke) Token: 0x0600078F RID: 1935
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor1Delegate(UIntPtr compositeComponentPointer, uint factorColor1);

		// Token: 0x020000A4 RID: 164
		// (Invoke) Token: 0x06000793 RID: 1939
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor2Delegate(UIntPtr compositeComponentPointer, uint factorColor2);

		// Token: 0x020000A5 RID: 165
		// (Invoke) Token: 0x06000797 RID: 1943
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameDelegate(UIntPtr compositeComponentPointer, ref MatrixFrame meshFrame);

		// Token: 0x020000A6 RID: 166
		// (Invoke) Token: 0x0600079B RID: 1947
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaterialDelegate(UIntPtr compositeComponentPointer, UIntPtr materialPointer);

		// Token: 0x020000A7 RID: 167
		// (Invoke) Token: 0x0600079F RID: 1951
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorArgumentDelegate(UIntPtr compositeComponentPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x020000A8 RID: 168
		// (Invoke) Token: 0x060007A3 RID: 1955
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorUserDataDelegate(UIntPtr compositeComponentPointer, ref Vec3 vectorArg);

		// Token: 0x020000A9 RID: 169
		// (Invoke) Token: 0x060007A7 RID: 1959
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVisibilityMaskDelegate(UIntPtr compositeComponentPointer, VisibilityMaskFlags visibilityMask);

		// Token: 0x020000AA RID: 170
		// (Invoke) Token: 0x060007AB RID: 1963
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVisibleDelegate(UIntPtr compositeComponentPointer, [MarshalAs(UnmanagedType.U1)] bool visible);
	}
}
