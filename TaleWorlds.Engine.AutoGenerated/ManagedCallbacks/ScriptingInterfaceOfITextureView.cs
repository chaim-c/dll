using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x0200002C RID: 44
	internal class ScriptingInterfaceOfITextureView : ITextureView
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x000164E8 File Offset: 0x000146E8
		public TextureView CreateTextureView()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITextureView.call_CreateTextureViewDelegate();
			TextureView result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new TextureView(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00016531 File Offset: 0x00014731
		public void SetTexture(UIntPtr pointer, UIntPtr texture_ptr)
		{
			ScriptingInterfaceOfITextureView.call_SetTextureDelegate(pointer, texture_ptr);
		}

		// Token: 0x0400049A RID: 1178
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400049B RID: 1179
		public static ScriptingInterfaceOfITextureView.CreateTextureViewDelegate call_CreateTextureViewDelegate;

		// Token: 0x0400049C RID: 1180
		public static ScriptingInterfaceOfITextureView.SetTextureDelegate call_SetTextureDelegate;

		// Token: 0x020004E8 RID: 1256
		// (Invoke) Token: 0x060018A3 RID: 6307
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateTextureViewDelegate();

		// Token: 0x020004E9 RID: 1257
		// (Invoke) Token: 0x060018A7 RID: 6311
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTextureDelegate(UIntPtr pointer, UIntPtr texture_ptr);
	}
}
