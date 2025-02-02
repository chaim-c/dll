using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200000F RID: 15
	internal class ScriptingInterfaceOfIDecal : IDecal
	{
		// Token: 0x060000EC RID: 236 RVA: 0x0000DE28 File Offset: 0x0000C028
		public Decal CreateCopy(UIntPtr pointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIDecal.call_CreateCopyDelegate(pointer);
			Decal result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Decal(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000DE74 File Offset: 0x0000C074
		public Decal CreateDecal(string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIDecal._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDecal._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIDecal.call_CreateDecalDelegate(array);
			Decal result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Decal(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000DF00 File Offset: 0x0000C100
		public uint GetFactor1(UIntPtr decalPointer)
		{
			return ScriptingInterfaceOfIDecal.call_GetFactor1Delegate(decalPointer);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000DF0D File Offset: 0x0000C10D
		public void GetFrame(UIntPtr decalPointer, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIDecal.call_GetFrameDelegate(decalPointer, ref outFrame);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000DF1C File Offset: 0x0000C11C
		public Material GetMaterial(UIntPtr decalPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIDecal.call_GetMaterialDelegate(decalPointer);
			Material result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Material(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000DF66 File Offset: 0x0000C166
		public void SetFactor1(UIntPtr decalPointer, uint factorColor1)
		{
			ScriptingInterfaceOfIDecal.call_SetFactor1Delegate(decalPointer, factorColor1);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000DF74 File Offset: 0x0000C174
		public void SetFactor1Linear(UIntPtr decalPointer, uint linearFactorColor1)
		{
			ScriptingInterfaceOfIDecal.call_SetFactor1LinearDelegate(decalPointer, linearFactorColor1);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000DF82 File Offset: 0x0000C182
		public void SetFrame(UIntPtr decalPointer, ref MatrixFrame decalFrame)
		{
			ScriptingInterfaceOfIDecal.call_SetFrameDelegate(decalPointer, ref decalFrame);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000DF90 File Offset: 0x0000C190
		public void SetMaterial(UIntPtr decalPointer, UIntPtr materialPointer)
		{
			ScriptingInterfaceOfIDecal.call_SetMaterialDelegate(decalPointer, materialPointer);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000DF9E File Offset: 0x0000C19E
		public void SetVectorArgument(UIntPtr decalPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			ScriptingInterfaceOfIDecal.call_SetVectorArgumentDelegate(decalPointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000DFB1 File Offset: 0x0000C1B1
		public void SetVectorArgument2(UIntPtr decalPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			ScriptingInterfaceOfIDecal.call_SetVectorArgument2Delegate(decalPointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x04000093 RID: 147
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000094 RID: 148
		public static ScriptingInterfaceOfIDecal.CreateCopyDelegate call_CreateCopyDelegate;

		// Token: 0x04000095 RID: 149
		public static ScriptingInterfaceOfIDecal.CreateDecalDelegate call_CreateDecalDelegate;

		// Token: 0x04000096 RID: 150
		public static ScriptingInterfaceOfIDecal.GetFactor1Delegate call_GetFactor1Delegate;

		// Token: 0x04000097 RID: 151
		public static ScriptingInterfaceOfIDecal.GetFrameDelegate call_GetFrameDelegate;

		// Token: 0x04000098 RID: 152
		public static ScriptingInterfaceOfIDecal.GetMaterialDelegate call_GetMaterialDelegate;

		// Token: 0x04000099 RID: 153
		public static ScriptingInterfaceOfIDecal.SetFactor1Delegate call_SetFactor1Delegate;

		// Token: 0x0400009A RID: 154
		public static ScriptingInterfaceOfIDecal.SetFactor1LinearDelegate call_SetFactor1LinearDelegate;

		// Token: 0x0400009B RID: 155
		public static ScriptingInterfaceOfIDecal.SetFrameDelegate call_SetFrameDelegate;

		// Token: 0x0400009C RID: 156
		public static ScriptingInterfaceOfIDecal.SetMaterialDelegate call_SetMaterialDelegate;

		// Token: 0x0400009D RID: 157
		public static ScriptingInterfaceOfIDecal.SetVectorArgumentDelegate call_SetVectorArgumentDelegate;

		// Token: 0x0400009E RID: 158
		public static ScriptingInterfaceOfIDecal.SetVectorArgument2Delegate call_SetVectorArgument2Delegate;

		// Token: 0x020000FE RID: 254
		// (Invoke) Token: 0x060008FB RID: 2299
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateCopyDelegate(UIntPtr pointer);

		// Token: 0x020000FF RID: 255
		// (Invoke) Token: 0x060008FF RID: 2303
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateDecalDelegate(byte[] name);

		// Token: 0x02000100 RID: 256
		// (Invoke) Token: 0x06000903 RID: 2307
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFactor1Delegate(UIntPtr decalPointer);

		// Token: 0x02000101 RID: 257
		// (Invoke) Token: 0x06000907 RID: 2311
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetFrameDelegate(UIntPtr decalPointer, ref MatrixFrame outFrame);

		// Token: 0x02000102 RID: 258
		// (Invoke) Token: 0x0600090B RID: 2315
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetMaterialDelegate(UIntPtr decalPointer);

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x0600090F RID: 2319
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor1Delegate(UIntPtr decalPointer, uint factorColor1);

		// Token: 0x02000104 RID: 260
		// (Invoke) Token: 0x06000913 RID: 2323
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor1LinearDelegate(UIntPtr decalPointer, uint linearFactorColor1);

		// Token: 0x02000105 RID: 261
		// (Invoke) Token: 0x06000917 RID: 2327
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameDelegate(UIntPtr decalPointer, ref MatrixFrame decalFrame);

		// Token: 0x02000106 RID: 262
		// (Invoke) Token: 0x0600091B RID: 2331
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaterialDelegate(UIntPtr decalPointer, UIntPtr materialPointer);

		// Token: 0x02000107 RID: 263
		// (Invoke) Token: 0x0600091F RID: 2335
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorArgumentDelegate(UIntPtr decalPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x02000108 RID: 264
		// (Invoke) Token: 0x06000923 RID: 2339
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorArgument2Delegate(UIntPtr decalPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);
	}
}
