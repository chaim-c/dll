using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200002B RID: 43
	internal class ScriptingInterfaceOfITexture : ITexture
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x00015E54 File Offset: 0x00014054
		public Texture CheckAndGetFromResource(string textureName)
		{
			byte[] array = null;
			if (textureName != null)
			{
				int byteCount = ScriptingInterfaceOfITexture._utf8.GetByteCount(textureName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(textureName, 0, textureName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_CheckAndGetFromResourceDelegate(array);
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00015EE0 File Offset: 0x000140E0
		public Texture CreateDepthTarget(string name, int width, int height)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfITexture._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_CreateDepthTargetDelegate(array, width, height);
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00015F70 File Offset: 0x00014170
		public Texture CreateFromByteArray(byte[] data, int width, int height)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(data, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray data2 = new ManagedArray(pointer, (data != null) ? data.Length : 0);
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_CreateFromByteArrayDelegate(data2, width, height);
			pinnedArrayData.Dispose();
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00015FE8 File Offset: 0x000141E8
		public Texture CreateFromMemory(byte[] data)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(data, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray data2 = new ManagedArray(pointer, (data != null) ? data.Length : 0);
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_CreateFromMemoryDelegate(data2);
			pinnedArrayData.Dispose();
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00016060 File Offset: 0x00014260
		public Texture CreateRenderTarget(string name, int width, int height, bool autoMipmaps, bool isTableau, bool createUninitialized, bool always_valid)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfITexture._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_CreateRenderTargetDelegate(array, width, height, autoMipmaps, isTableau, createUninitialized, always_valid);
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000160F8 File Offset: 0x000142F8
		public Texture CreateTextureFromPath(PlatformFilePath filePath)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_CreateTextureFromPathDelegate(filePath);
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00016142 File Offset: 0x00014342
		public void GetCurObject(UIntPtr texturePointer, bool blocking)
		{
			ScriptingInterfaceOfITexture.call_GetCurObjectDelegate(texturePointer, blocking);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00016150 File Offset: 0x00014350
		public Texture GetFromResource(string textureName)
		{
			byte[] array = null;
			if (textureName != null)
			{
				int byteCount = ScriptingInterfaceOfITexture._utf8.GetByteCount(textureName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(textureName, 0, textureName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_GetFromResourceDelegate(array);
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000161DC File Offset: 0x000143DC
		public int GetHeight(UIntPtr texturePointer)
		{
			return ScriptingInterfaceOfITexture.call_GetHeightDelegate(texturePointer);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000161E9 File Offset: 0x000143E9
		public int GetMemorySize(UIntPtr texturePointer)
		{
			return ScriptingInterfaceOfITexture.call_GetMemorySizeDelegate(texturePointer);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000161F6 File Offset: 0x000143F6
		public string GetName(UIntPtr texturePointer)
		{
			if (ScriptingInterfaceOfITexture.call_GetNameDelegate(texturePointer) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001620D File Offset: 0x0001440D
		public RenderTargetComponent GetRenderTargetComponent(UIntPtr texturePointer)
		{
			return DotNetObject.GetManagedObjectWithId(ScriptingInterfaceOfITexture.call_GetRenderTargetComponentDelegate(texturePointer)) as RenderTargetComponent;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00016224 File Offset: 0x00014424
		public TableauView GetTableauView(UIntPtr texturePointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_GetTableauViewDelegate(texturePointer);
			TableauView result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new TableauView(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001626E File Offset: 0x0001446E
		public int GetWidth(UIntPtr texturePointer)
		{
			return ScriptingInterfaceOfITexture.call_GetWidthDelegate(texturePointer);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001627B File Offset: 0x0001447B
		public bool IsLoaded(UIntPtr texturePointer)
		{
			return ScriptingInterfaceOfITexture.call_IsLoadedDelegate(texturePointer);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00016288 File Offset: 0x00014488
		public bool IsRenderTarget(UIntPtr texturePointer)
		{
			return ScriptingInterfaceOfITexture.call_IsRenderTargetDelegate(texturePointer);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00016298 File Offset: 0x00014498
		public Texture LoadTextureFromPath(string fileName, string folder)
		{
			byte[] array = null;
			if (fileName != null)
			{
				int byteCount = ScriptingInterfaceOfITexture._utf8.GetByteCount(fileName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(fileName, 0, fileName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (folder != null)
			{
				int byteCount2 = ScriptingInterfaceOfITexture._utf8.GetByteCount(folder);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(folder, 0, folder.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITexture.call_LoadTextureFromPathDelegate(array, array2);
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001636F File Offset: 0x0001456F
		public void Release(UIntPtr texturePointer)
		{
			ScriptingInterfaceOfITexture.call_ReleaseDelegate(texturePointer);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001637C File Offset: 0x0001457C
		public void ReleaseGpuMemories()
		{
			ScriptingInterfaceOfITexture.call_ReleaseGpuMemoriesDelegate();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00016388 File Offset: 0x00014588
		public void ReleaseNextFrame(UIntPtr texturePointer)
		{
			ScriptingInterfaceOfITexture.call_ReleaseNextFrameDelegate(texturePointer);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00016395 File Offset: 0x00014595
		public void RemoveContinousTableauTexture(UIntPtr texturePointer)
		{
			ScriptingInterfaceOfITexture.call_RemoveContinousTableauTextureDelegate(texturePointer);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000163A2 File Offset: 0x000145A2
		public void SaveTextureAsAlwaysValid(UIntPtr texturePointer)
		{
			ScriptingInterfaceOfITexture.call_SaveTextureAsAlwaysValidDelegate(texturePointer);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000163B0 File Offset: 0x000145B0
		public void SaveToFile(UIntPtr texturePointer, string fileName)
		{
			byte[] array = null;
			if (fileName != null)
			{
				int byteCount = ScriptingInterfaceOfITexture._utf8.GetByteCount(fileName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(fileName, 0, fileName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfITexture.call_SaveToFileDelegate(texturePointer, array);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001640C File Offset: 0x0001460C
		public void SetName(UIntPtr texturePointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfITexture._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfITexture.call_SetNameDelegate(texturePointer, array);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00016467 File Offset: 0x00014667
		public void SetTableauView(UIntPtr texturePointer, UIntPtr tableauView)
		{
			ScriptingInterfaceOfITexture.call_SetTableauViewDelegate(texturePointer, tableauView);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00016478 File Offset: 0x00014678
		public void TransformRenderTargetToResourceTexture(UIntPtr texturePointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfITexture._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfITexture._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfITexture.call_TransformRenderTargetToResourceTextureDelegate(texturePointer, array);
		}

		// Token: 0x0400047F RID: 1151
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000480 RID: 1152
		public static ScriptingInterfaceOfITexture.CheckAndGetFromResourceDelegate call_CheckAndGetFromResourceDelegate;

		// Token: 0x04000481 RID: 1153
		public static ScriptingInterfaceOfITexture.CreateDepthTargetDelegate call_CreateDepthTargetDelegate;

		// Token: 0x04000482 RID: 1154
		public static ScriptingInterfaceOfITexture.CreateFromByteArrayDelegate call_CreateFromByteArrayDelegate;

		// Token: 0x04000483 RID: 1155
		public static ScriptingInterfaceOfITexture.CreateFromMemoryDelegate call_CreateFromMemoryDelegate;

		// Token: 0x04000484 RID: 1156
		public static ScriptingInterfaceOfITexture.CreateRenderTargetDelegate call_CreateRenderTargetDelegate;

		// Token: 0x04000485 RID: 1157
		public static ScriptingInterfaceOfITexture.CreateTextureFromPathDelegate call_CreateTextureFromPathDelegate;

		// Token: 0x04000486 RID: 1158
		public static ScriptingInterfaceOfITexture.GetCurObjectDelegate call_GetCurObjectDelegate;

		// Token: 0x04000487 RID: 1159
		public static ScriptingInterfaceOfITexture.GetFromResourceDelegate call_GetFromResourceDelegate;

		// Token: 0x04000488 RID: 1160
		public static ScriptingInterfaceOfITexture.GetHeightDelegate call_GetHeightDelegate;

		// Token: 0x04000489 RID: 1161
		public static ScriptingInterfaceOfITexture.GetMemorySizeDelegate call_GetMemorySizeDelegate;

		// Token: 0x0400048A RID: 1162
		public static ScriptingInterfaceOfITexture.GetNameDelegate call_GetNameDelegate;

		// Token: 0x0400048B RID: 1163
		public static ScriptingInterfaceOfITexture.GetRenderTargetComponentDelegate call_GetRenderTargetComponentDelegate;

		// Token: 0x0400048C RID: 1164
		public static ScriptingInterfaceOfITexture.GetTableauViewDelegate call_GetTableauViewDelegate;

		// Token: 0x0400048D RID: 1165
		public static ScriptingInterfaceOfITexture.GetWidthDelegate call_GetWidthDelegate;

		// Token: 0x0400048E RID: 1166
		public static ScriptingInterfaceOfITexture.IsLoadedDelegate call_IsLoadedDelegate;

		// Token: 0x0400048F RID: 1167
		public static ScriptingInterfaceOfITexture.IsRenderTargetDelegate call_IsRenderTargetDelegate;

		// Token: 0x04000490 RID: 1168
		public static ScriptingInterfaceOfITexture.LoadTextureFromPathDelegate call_LoadTextureFromPathDelegate;

		// Token: 0x04000491 RID: 1169
		public static ScriptingInterfaceOfITexture.ReleaseDelegate call_ReleaseDelegate;

		// Token: 0x04000492 RID: 1170
		public static ScriptingInterfaceOfITexture.ReleaseGpuMemoriesDelegate call_ReleaseGpuMemoriesDelegate;

		// Token: 0x04000493 RID: 1171
		public static ScriptingInterfaceOfITexture.ReleaseNextFrameDelegate call_ReleaseNextFrameDelegate;

		// Token: 0x04000494 RID: 1172
		public static ScriptingInterfaceOfITexture.RemoveContinousTableauTextureDelegate call_RemoveContinousTableauTextureDelegate;

		// Token: 0x04000495 RID: 1173
		public static ScriptingInterfaceOfITexture.SaveTextureAsAlwaysValidDelegate call_SaveTextureAsAlwaysValidDelegate;

		// Token: 0x04000496 RID: 1174
		public static ScriptingInterfaceOfITexture.SaveToFileDelegate call_SaveToFileDelegate;

		// Token: 0x04000497 RID: 1175
		public static ScriptingInterfaceOfITexture.SetNameDelegate call_SetNameDelegate;

		// Token: 0x04000498 RID: 1176
		public static ScriptingInterfaceOfITexture.SetTableauViewDelegate call_SetTableauViewDelegate;

		// Token: 0x04000499 RID: 1177
		public static ScriptingInterfaceOfITexture.TransformRenderTargetToResourceTextureDelegate call_TransformRenderTargetToResourceTextureDelegate;

		// Token: 0x020004CE RID: 1230
		// (Invoke) Token: 0x0600183B RID: 6203
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CheckAndGetFromResourceDelegate(byte[] textureName);

		// Token: 0x020004CF RID: 1231
		// (Invoke) Token: 0x0600183F RID: 6207
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateDepthTargetDelegate(byte[] name, int width, int height);

		// Token: 0x020004D0 RID: 1232
		// (Invoke) Token: 0x06001843 RID: 6211
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateFromByteArrayDelegate(ManagedArray data, int width, int height);

		// Token: 0x020004D1 RID: 1233
		// (Invoke) Token: 0x06001847 RID: 6215
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateFromMemoryDelegate(ManagedArray data);

		// Token: 0x020004D2 RID: 1234
		// (Invoke) Token: 0x0600184B RID: 6219
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateRenderTargetDelegate(byte[] name, int width, int height, [MarshalAs(UnmanagedType.U1)] bool autoMipmaps, [MarshalAs(UnmanagedType.U1)] bool isTableau, [MarshalAs(UnmanagedType.U1)] bool createUninitialized, [MarshalAs(UnmanagedType.U1)] bool always_valid);

		// Token: 0x020004D3 RID: 1235
		// (Invoke) Token: 0x0600184F RID: 6223
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateTextureFromPathDelegate(PlatformFilePath filePath);

		// Token: 0x020004D4 RID: 1236
		// (Invoke) Token: 0x06001853 RID: 6227
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetCurObjectDelegate(UIntPtr texturePointer, [MarshalAs(UnmanagedType.U1)] bool blocking);

		// Token: 0x020004D5 RID: 1237
		// (Invoke) Token: 0x06001857 RID: 6231
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFromResourceDelegate(byte[] textureName);

		// Token: 0x020004D6 RID: 1238
		// (Invoke) Token: 0x0600185B RID: 6235
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetHeightDelegate(UIntPtr texturePointer);

		// Token: 0x020004D7 RID: 1239
		// (Invoke) Token: 0x0600185F RID: 6239
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMemorySizeDelegate(UIntPtr texturePointer);

		// Token: 0x020004D8 RID: 1240
		// (Invoke) Token: 0x06001863 RID: 6243
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr texturePointer);

		// Token: 0x020004D9 RID: 1241
		// (Invoke) Token: 0x06001867 RID: 6247
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetRenderTargetComponentDelegate(UIntPtr texturePointer);

		// Token: 0x020004DA RID: 1242
		// (Invoke) Token: 0x0600186B RID: 6251
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetTableauViewDelegate(UIntPtr texturePointer);

		// Token: 0x020004DB RID: 1243
		// (Invoke) Token: 0x0600186F RID: 6255
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetWidthDelegate(UIntPtr texturePointer);

		// Token: 0x020004DC RID: 1244
		// (Invoke) Token: 0x06001873 RID: 6259
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsLoadedDelegate(UIntPtr texturePointer);

		// Token: 0x020004DD RID: 1245
		// (Invoke) Token: 0x06001877 RID: 6263
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsRenderTargetDelegate(UIntPtr texturePointer);

		// Token: 0x020004DE RID: 1246
		// (Invoke) Token: 0x0600187B RID: 6267
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer LoadTextureFromPathDelegate(byte[] fileName, byte[] folder);

		// Token: 0x020004DF RID: 1247
		// (Invoke) Token: 0x0600187F RID: 6271
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseDelegate(UIntPtr texturePointer);

		// Token: 0x020004E0 RID: 1248
		// (Invoke) Token: 0x06001883 RID: 6275
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseGpuMemoriesDelegate();

		// Token: 0x020004E1 RID: 1249
		// (Invoke) Token: 0x06001887 RID: 6279
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseNextFrameDelegate(UIntPtr texturePointer);

		// Token: 0x020004E2 RID: 1250
		// (Invoke) Token: 0x0600188B RID: 6283
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveContinousTableauTextureDelegate(UIntPtr texturePointer);

		// Token: 0x020004E3 RID: 1251
		// (Invoke) Token: 0x0600188F RID: 6287
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SaveTextureAsAlwaysValidDelegate(UIntPtr texturePointer);

		// Token: 0x020004E4 RID: 1252
		// (Invoke) Token: 0x06001893 RID: 6291
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SaveToFileDelegate(UIntPtr texturePointer, byte[] fileName);

		// Token: 0x020004E5 RID: 1253
		// (Invoke) Token: 0x06001897 RID: 6295
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetNameDelegate(UIntPtr texturePointer, byte[] name);

		// Token: 0x020004E6 RID: 1254
		// (Invoke) Token: 0x0600189B RID: 6299
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTableauViewDelegate(UIntPtr texturePointer, UIntPtr tableauView);

		// Token: 0x020004E7 RID: 1255
		// (Invoke) Token: 0x0600189F RID: 6303
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TransformRenderTargetToResourceTextureDelegate(UIntPtr texturePointer, byte[] name);
	}
}
