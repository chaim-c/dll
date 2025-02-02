using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200002F RID: 47
	internal class ScriptingInterfaceOfITwoDimensionView : ITwoDimensionView
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x0001677B File Offset: 0x0001497B
		public bool AddCachedTextMesh(UIntPtr pointer, UIntPtr material, ref TwoDimensionTextMeshDrawData meshDrawData)
		{
			return ScriptingInterfaceOfITwoDimensionView.call_AddCachedTextMeshDelegate(pointer, material, ref meshDrawData);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001678C File Offset: 0x0001498C
		public void AddNewMesh(UIntPtr pointer, float[] vertices, float[] uvs, uint[] indices, int vertexCount, int indexCount, UIntPtr material, ref TwoDimensionMeshDrawData meshDrawData)
		{
			PinnedArrayData<float> pinnedArrayData = new PinnedArrayData<float>(vertices, false);
			IntPtr pointer2 = pinnedArrayData.Pointer;
			PinnedArrayData<float> pinnedArrayData2 = new PinnedArrayData<float>(uvs, false);
			IntPtr pointer3 = pinnedArrayData2.Pointer;
			PinnedArrayData<uint> pinnedArrayData3 = new PinnedArrayData<uint>(indices, false);
			IntPtr pointer4 = pinnedArrayData3.Pointer;
			ScriptingInterfaceOfITwoDimensionView.call_AddNewMeshDelegate(pointer, pointer2, pointer3, pointer4, vertexCount, indexCount, material, ref meshDrawData);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			pinnedArrayData3.Dispose();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000167FA File Offset: 0x000149FA
		public void AddNewQuadMesh(UIntPtr pointer, UIntPtr material, ref TwoDimensionMeshDrawData meshDrawData)
		{
			ScriptingInterfaceOfITwoDimensionView.call_AddNewQuadMeshDelegate(pointer, material, ref meshDrawData);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001680C File Offset: 0x00014A0C
		public void AddNewTextMesh(UIntPtr pointer, float[] vertices, float[] uvs, uint[] indices, int vertexCount, int indexCount, UIntPtr material, ref TwoDimensionTextMeshDrawData meshDrawData)
		{
			PinnedArrayData<float> pinnedArrayData = new PinnedArrayData<float>(vertices, false);
			IntPtr pointer2 = pinnedArrayData.Pointer;
			PinnedArrayData<float> pinnedArrayData2 = new PinnedArrayData<float>(uvs, false);
			IntPtr pointer3 = pinnedArrayData2.Pointer;
			PinnedArrayData<uint> pinnedArrayData3 = new PinnedArrayData<uint>(indices, false);
			IntPtr pointer4 = pinnedArrayData3.Pointer;
			ScriptingInterfaceOfITwoDimensionView.call_AddNewTextMeshDelegate(pointer, pointer2, pointer3, pointer4, vertexCount, indexCount, material, ref meshDrawData);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			pinnedArrayData3.Dispose();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001687A File Offset: 0x00014A7A
		public void BeginFrame(UIntPtr pointer)
		{
			ScriptingInterfaceOfITwoDimensionView.call_BeginFrameDelegate(pointer);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00016887 File Offset: 0x00014A87
		public void Clear(UIntPtr pointer)
		{
			ScriptingInterfaceOfITwoDimensionView.call_ClearDelegate(pointer);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00016894 File Offset: 0x00014A94
		public TwoDimensionView CreateTwoDimensionView()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITwoDimensionView.call_CreateTwoDimensionViewDelegate();
			TwoDimensionView result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new TwoDimensionView(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000168DD File Offset: 0x00014ADD
		public void EndFrame(UIntPtr pointer)
		{
			ScriptingInterfaceOfITwoDimensionView.call_EndFrameDelegate(pointer);
		}

		// Token: 0x040004A8 RID: 1192
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040004A9 RID: 1193
		public static ScriptingInterfaceOfITwoDimensionView.AddCachedTextMeshDelegate call_AddCachedTextMeshDelegate;

		// Token: 0x040004AA RID: 1194
		public static ScriptingInterfaceOfITwoDimensionView.AddNewMeshDelegate call_AddNewMeshDelegate;

		// Token: 0x040004AB RID: 1195
		public static ScriptingInterfaceOfITwoDimensionView.AddNewQuadMeshDelegate call_AddNewQuadMeshDelegate;

		// Token: 0x040004AC RID: 1196
		public static ScriptingInterfaceOfITwoDimensionView.AddNewTextMeshDelegate call_AddNewTextMeshDelegate;

		// Token: 0x040004AD RID: 1197
		public static ScriptingInterfaceOfITwoDimensionView.BeginFrameDelegate call_BeginFrameDelegate;

		// Token: 0x040004AE RID: 1198
		public static ScriptingInterfaceOfITwoDimensionView.ClearDelegate call_ClearDelegate;

		// Token: 0x040004AF RID: 1199
		public static ScriptingInterfaceOfITwoDimensionView.CreateTwoDimensionViewDelegate call_CreateTwoDimensionViewDelegate;

		// Token: 0x040004B0 RID: 1200
		public static ScriptingInterfaceOfITwoDimensionView.EndFrameDelegate call_EndFrameDelegate;

		// Token: 0x020004F3 RID: 1267
		// (Invoke) Token: 0x060018CF RID: 6351
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool AddCachedTextMeshDelegate(UIntPtr pointer, UIntPtr material, ref TwoDimensionTextMeshDrawData meshDrawData);

		// Token: 0x020004F4 RID: 1268
		// (Invoke) Token: 0x060018D3 RID: 6355
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddNewMeshDelegate(UIntPtr pointer, IntPtr vertices, IntPtr uvs, IntPtr indices, int vertexCount, int indexCount, UIntPtr material, ref TwoDimensionMeshDrawData meshDrawData);

		// Token: 0x020004F5 RID: 1269
		// (Invoke) Token: 0x060018D7 RID: 6359
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddNewQuadMeshDelegate(UIntPtr pointer, UIntPtr material, ref TwoDimensionMeshDrawData meshDrawData);

		// Token: 0x020004F6 RID: 1270
		// (Invoke) Token: 0x060018DB RID: 6363
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddNewTextMeshDelegate(UIntPtr pointer, IntPtr vertices, IntPtr uvs, IntPtr indices, int vertexCount, int indexCount, UIntPtr material, ref TwoDimensionTextMeshDrawData meshDrawData);

		// Token: 0x020004F7 RID: 1271
		// (Invoke) Token: 0x060018DF RID: 6367
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BeginFrameDelegate(UIntPtr pointer);

		// Token: 0x020004F8 RID: 1272
		// (Invoke) Token: 0x060018E3 RID: 6371
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearDelegate(UIntPtr pointer);

		// Token: 0x020004F9 RID: 1273
		// (Invoke) Token: 0x060018E7 RID: 6375
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateTwoDimensionViewDelegate();

		// Token: 0x020004FA RID: 1274
		// (Invoke) Token: 0x060018EB RID: 6379
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EndFrameDelegate(UIntPtr pointer);
	}
}
