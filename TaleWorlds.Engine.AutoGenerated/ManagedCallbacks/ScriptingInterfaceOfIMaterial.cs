using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000018 RID: 24
	internal class ScriptingInterfaceOfIMaterial : IMaterial
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00010AB8 File Offset: 0x0000ECB8
		public void AddMaterialShaderFlag(UIntPtr materialPointer, string flagName, bool showErrors)
		{
			byte[] array = null;
			if (flagName != null)
			{
				int byteCount = ScriptingInterfaceOfIMaterial._utf8.GetByteCount(flagName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMaterial._utf8.GetBytes(flagName, 0, flagName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMaterial.call_AddMaterialShaderFlagDelegate(materialPointer, array, showErrors);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00010B14 File Offset: 0x0000ED14
		public Material CreateCopy(UIntPtr materialPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMaterial.call_CreateCopyDelegate(materialPointer);
			Material result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Material(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00010B5E File Offset: 0x0000ED5E
		public int GetAlphaBlendMode(UIntPtr materialPointer)
		{
			return ScriptingInterfaceOfIMaterial.call_GetAlphaBlendModeDelegate(materialPointer);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00010B6B File Offset: 0x0000ED6B
		public float GetAlphaTestValue(UIntPtr materialPointer)
		{
			return ScriptingInterfaceOfIMaterial.call_GetAlphaTestValueDelegate(materialPointer);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00010B78 File Offset: 0x0000ED78
		public Material GetDefaultMaterial()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMaterial.call_GetDefaultMaterialDelegate();
			Material result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Material(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00010BC1 File Offset: 0x0000EDC1
		public uint GetFlags(UIntPtr materialPointer)
		{
			return ScriptingInterfaceOfIMaterial.call_GetFlagsDelegate(materialPointer);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		public Material GetFromResource(string materialName)
		{
			byte[] array = null;
			if (materialName != null)
			{
				int byteCount = ScriptingInterfaceOfIMaterial._utf8.GetByteCount(materialName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMaterial._utf8.GetBytes(materialName, 0, materialName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMaterial.call_GetFromResourceDelegate(array);
			Material result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Material(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public string GetName(UIntPtr materialPointer)
		{
			if (ScriptingInterfaceOfIMaterial.call_GetNameDelegate(materialPointer) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00010C74 File Offset: 0x0000EE74
		public Material GetOutlineMaterial(UIntPtr materialPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMaterial.call_GetOutlineMaterialDelegate(materialPointer);
			Material result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Material(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		public Shader GetShader(UIntPtr materialPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMaterial.call_GetShaderDelegate(materialPointer);
			Shader result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Shader(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00010D0A File Offset: 0x0000EF0A
		public ulong GetShaderFlags(UIntPtr materialPointer)
		{
			return ScriptingInterfaceOfIMaterial.call_GetShaderFlagsDelegate(materialPointer);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00010D18 File Offset: 0x0000EF18
		public Texture GetTexture(UIntPtr materialPointer, int textureType)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMaterial.call_GetTextureDelegate(materialPointer, textureType);
			Texture result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Texture(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00010D63 File Offset: 0x0000EF63
		public void Release(UIntPtr materialPointer)
		{
			ScriptingInterfaceOfIMaterial.call_ReleaseDelegate(materialPointer);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00010D70 File Offset: 0x0000EF70
		public void SetAlphaBlendMode(UIntPtr materialPointer, int alphaBlendMode)
		{
			ScriptingInterfaceOfIMaterial.call_SetAlphaBlendModeDelegate(materialPointer, alphaBlendMode);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00010D7E File Offset: 0x0000EF7E
		public void SetAlphaTestValue(UIntPtr materialPointer, float alphaTestValue)
		{
			ScriptingInterfaceOfIMaterial.call_SetAlphaTestValueDelegate(materialPointer, alphaTestValue);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00010D8C File Offset: 0x0000EF8C
		public void SetAreaMapScale(UIntPtr materialPointer, float scale)
		{
			ScriptingInterfaceOfIMaterial.call_SetAreaMapScaleDelegate(materialPointer, scale);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00010D9A File Offset: 0x0000EF9A
		public void SetEnableSkinning(UIntPtr materialPointer, bool enable)
		{
			ScriptingInterfaceOfIMaterial.call_SetEnableSkinningDelegate(materialPointer, enable);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public void SetFlags(UIntPtr materialPointer, uint flags)
		{
			ScriptingInterfaceOfIMaterial.call_SetFlagsDelegate(materialPointer, flags);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00010DB6 File Offset: 0x0000EFB6
		public void SetMeshVectorArgument(UIntPtr materialPointer, float x, float y, float z, float w)
		{
			ScriptingInterfaceOfIMaterial.call_SetMeshVectorArgumentDelegate(materialPointer, x, y, z, w);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00010DCC File Offset: 0x0000EFCC
		public void SetName(UIntPtr materialPointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIMaterial._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMaterial._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMaterial.call_SetNameDelegate(materialPointer, array);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00010E27 File Offset: 0x0000F027
		public void SetShader(UIntPtr materialPointer, UIntPtr shaderPointer)
		{
			ScriptingInterfaceOfIMaterial.call_SetShaderDelegate(materialPointer, shaderPointer);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00010E35 File Offset: 0x0000F035
		public void SetShaderFlags(UIntPtr materialPointer, ulong shaderFlags)
		{
			ScriptingInterfaceOfIMaterial.call_SetShaderFlagsDelegate(materialPointer, shaderFlags);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00010E43 File Offset: 0x0000F043
		public void SetTexture(UIntPtr materialPointer, int textureType, UIntPtr texturePointer)
		{
			ScriptingInterfaceOfIMaterial.call_SetTextureDelegate(materialPointer, textureType, texturePointer);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00010E52 File Offset: 0x0000F052
		public void SetTextureAtSlot(UIntPtr materialPointer, int textureSlotIndex, UIntPtr texturePointer)
		{
			ScriptingInterfaceOfIMaterial.call_SetTextureAtSlotDelegate(materialPointer, textureSlotIndex, texturePointer);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00010E61 File Offset: 0x0000F061
		public bool UsingSkinning(UIntPtr materialPointer)
		{
			return ScriptingInterfaceOfIMaterial.call_UsingSkinningDelegate(materialPointer);
		}

		// Token: 0x040001F5 RID: 501
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040001F6 RID: 502
		public static ScriptingInterfaceOfIMaterial.AddMaterialShaderFlagDelegate call_AddMaterialShaderFlagDelegate;

		// Token: 0x040001F7 RID: 503
		public static ScriptingInterfaceOfIMaterial.CreateCopyDelegate call_CreateCopyDelegate;

		// Token: 0x040001F8 RID: 504
		public static ScriptingInterfaceOfIMaterial.GetAlphaBlendModeDelegate call_GetAlphaBlendModeDelegate;

		// Token: 0x040001F9 RID: 505
		public static ScriptingInterfaceOfIMaterial.GetAlphaTestValueDelegate call_GetAlphaTestValueDelegate;

		// Token: 0x040001FA RID: 506
		public static ScriptingInterfaceOfIMaterial.GetDefaultMaterialDelegate call_GetDefaultMaterialDelegate;

		// Token: 0x040001FB RID: 507
		public static ScriptingInterfaceOfIMaterial.GetFlagsDelegate call_GetFlagsDelegate;

		// Token: 0x040001FC RID: 508
		public static ScriptingInterfaceOfIMaterial.GetFromResourceDelegate call_GetFromResourceDelegate;

		// Token: 0x040001FD RID: 509
		public static ScriptingInterfaceOfIMaterial.GetNameDelegate call_GetNameDelegate;

		// Token: 0x040001FE RID: 510
		public static ScriptingInterfaceOfIMaterial.GetOutlineMaterialDelegate call_GetOutlineMaterialDelegate;

		// Token: 0x040001FF RID: 511
		public static ScriptingInterfaceOfIMaterial.GetShaderDelegate call_GetShaderDelegate;

		// Token: 0x04000200 RID: 512
		public static ScriptingInterfaceOfIMaterial.GetShaderFlagsDelegate call_GetShaderFlagsDelegate;

		// Token: 0x04000201 RID: 513
		public static ScriptingInterfaceOfIMaterial.GetTextureDelegate call_GetTextureDelegate;

		// Token: 0x04000202 RID: 514
		public static ScriptingInterfaceOfIMaterial.ReleaseDelegate call_ReleaseDelegate;

		// Token: 0x04000203 RID: 515
		public static ScriptingInterfaceOfIMaterial.SetAlphaBlendModeDelegate call_SetAlphaBlendModeDelegate;

		// Token: 0x04000204 RID: 516
		public static ScriptingInterfaceOfIMaterial.SetAlphaTestValueDelegate call_SetAlphaTestValueDelegate;

		// Token: 0x04000205 RID: 517
		public static ScriptingInterfaceOfIMaterial.SetAreaMapScaleDelegate call_SetAreaMapScaleDelegate;

		// Token: 0x04000206 RID: 518
		public static ScriptingInterfaceOfIMaterial.SetEnableSkinningDelegate call_SetEnableSkinningDelegate;

		// Token: 0x04000207 RID: 519
		public static ScriptingInterfaceOfIMaterial.SetFlagsDelegate call_SetFlagsDelegate;

		// Token: 0x04000208 RID: 520
		public static ScriptingInterfaceOfIMaterial.SetMeshVectorArgumentDelegate call_SetMeshVectorArgumentDelegate;

		// Token: 0x04000209 RID: 521
		public static ScriptingInterfaceOfIMaterial.SetNameDelegate call_SetNameDelegate;

		// Token: 0x0400020A RID: 522
		public static ScriptingInterfaceOfIMaterial.SetShaderDelegate call_SetShaderDelegate;

		// Token: 0x0400020B RID: 523
		public static ScriptingInterfaceOfIMaterial.SetShaderFlagsDelegate call_SetShaderFlagsDelegate;

		// Token: 0x0400020C RID: 524
		public static ScriptingInterfaceOfIMaterial.SetTextureDelegate call_SetTextureDelegate;

		// Token: 0x0400020D RID: 525
		public static ScriptingInterfaceOfIMaterial.SetTextureAtSlotDelegate call_SetTextureAtSlotDelegate;

		// Token: 0x0400020E RID: 526
		public static ScriptingInterfaceOfIMaterial.UsingSkinningDelegate call_UsingSkinningDelegate;

		// Token: 0x02000257 RID: 599
		// (Invoke) Token: 0x06000E5F RID: 3679
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMaterialShaderFlagDelegate(UIntPtr materialPointer, byte[] flagName, [MarshalAs(UnmanagedType.U1)] bool showErrors);

		// Token: 0x02000258 RID: 600
		// (Invoke) Token: 0x06000E63 RID: 3683
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateCopyDelegate(UIntPtr materialPointer);

		// Token: 0x02000259 RID: 601
		// (Invoke) Token: 0x06000E67 RID: 3687
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAlphaBlendModeDelegate(UIntPtr materialPointer);

		// Token: 0x0200025A RID: 602
		// (Invoke) Token: 0x06000E6B RID: 3691
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAlphaTestValueDelegate(UIntPtr materialPointer);

		// Token: 0x0200025B RID: 603
		// (Invoke) Token: 0x06000E6F RID: 3695
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetDefaultMaterialDelegate();

		// Token: 0x0200025C RID: 604
		// (Invoke) Token: 0x06000E73 RID: 3699
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFlagsDelegate(UIntPtr materialPointer);

		// Token: 0x0200025D RID: 605
		// (Invoke) Token: 0x06000E77 RID: 3703
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFromResourceDelegate(byte[] materialName);

		// Token: 0x0200025E RID: 606
		// (Invoke) Token: 0x06000E7B RID: 3707
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr materialPointer);

		// Token: 0x0200025F RID: 607
		// (Invoke) Token: 0x06000E7F RID: 3711
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetOutlineMaterialDelegate(UIntPtr materialPointer);

		// Token: 0x02000260 RID: 608
		// (Invoke) Token: 0x06000E83 RID: 3715
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetShaderDelegate(UIntPtr materialPointer);

		// Token: 0x02000261 RID: 609
		// (Invoke) Token: 0x06000E87 RID: 3719
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate ulong GetShaderFlagsDelegate(UIntPtr materialPointer);

		// Token: 0x02000262 RID: 610
		// (Invoke) Token: 0x06000E8B RID: 3723
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetTextureDelegate(UIntPtr materialPointer, int textureType);

		// Token: 0x02000263 RID: 611
		// (Invoke) Token: 0x06000E8F RID: 3727
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseDelegate(UIntPtr materialPointer);

		// Token: 0x02000264 RID: 612
		// (Invoke) Token: 0x06000E93 RID: 3731
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAlphaBlendModeDelegate(UIntPtr materialPointer, int alphaBlendMode);

		// Token: 0x02000265 RID: 613
		// (Invoke) Token: 0x06000E97 RID: 3735
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAlphaTestValueDelegate(UIntPtr materialPointer, float alphaTestValue);

		// Token: 0x02000266 RID: 614
		// (Invoke) Token: 0x06000E9B RID: 3739
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAreaMapScaleDelegate(UIntPtr materialPointer, float scale);

		// Token: 0x02000267 RID: 615
		// (Invoke) Token: 0x06000E9F RID: 3743
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEnableSkinningDelegate(UIntPtr materialPointer, [MarshalAs(UnmanagedType.U1)] bool enable);

		// Token: 0x02000268 RID: 616
		// (Invoke) Token: 0x06000EA3 RID: 3747
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFlagsDelegate(UIntPtr materialPointer, uint flags);

		// Token: 0x02000269 RID: 617
		// (Invoke) Token: 0x06000EA7 RID: 3751
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMeshVectorArgumentDelegate(UIntPtr materialPointer, float x, float y, float z, float w);

		// Token: 0x0200026A RID: 618
		// (Invoke) Token: 0x06000EAB RID: 3755
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetNameDelegate(UIntPtr materialPointer, byte[] name);

		// Token: 0x0200026B RID: 619
		// (Invoke) Token: 0x06000EAF RID: 3759
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetShaderDelegate(UIntPtr materialPointer, UIntPtr shaderPointer);

		// Token: 0x0200026C RID: 620
		// (Invoke) Token: 0x06000EB3 RID: 3763
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetShaderFlagsDelegate(UIntPtr materialPointer, ulong shaderFlags);

		// Token: 0x0200026D RID: 621
		// (Invoke) Token: 0x06000EB7 RID: 3767
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTextureDelegate(UIntPtr materialPointer, int textureType, UIntPtr texturePointer);

		// Token: 0x0200026E RID: 622
		// (Invoke) Token: 0x06000EBB RID: 3771
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTextureAtSlotDelegate(UIntPtr materialPointer, int textureSlotIndex, UIntPtr texturePointer);

		// Token: 0x0200026F RID: 623
		// (Invoke) Token: 0x06000EBF RID: 3775
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool UsingSkinningDelegate(UIntPtr materialPointer);
	}
}
