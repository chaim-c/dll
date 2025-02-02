using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000012 RID: 18
	internal class ScriptingInterfaceOfIGameEntityComponent : IGameEntityComponent
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x0000F544 File Offset: 0x0000D744
		public GameEntity GetEntity(GameEntityComponent entityComponent)
		{
			UIntPtr entityComponent2 = (entityComponent != null) ? entityComponent.Pointer : UIntPtr.Zero;
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntityComponent.call_GetEntityDelegate(entityComponent2);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		public MetaMesh GetFirstMetaMesh(GameEntityComponent entityComponent)
		{
			UIntPtr entityComponent2 = (entityComponent != null) ? entityComponent.Pointer : UIntPtr.Zero;
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntityComponent.call_GetFirstMetaMeshDelegate(entityComponent2);
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0400015B RID: 347
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400015C RID: 348
		public static ScriptingInterfaceOfIGameEntityComponent.GetEntityDelegate call_GetEntityDelegate;

		// Token: 0x0400015D RID: 349
		public static ScriptingInterfaceOfIGameEntityComponent.GetFirstMetaMeshDelegate call_GetFirstMetaMeshDelegate;

		// Token: 0x020001C3 RID: 451
		// (Invoke) Token: 0x06000C0F RID: 3087
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetEntityDelegate(UIntPtr entityComponent);

		// Token: 0x020001C4 RID: 452
		// (Invoke) Token: 0x06000C13 RID: 3091
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFirstMetaMeshDelegate(UIntPtr entityComponent);
	}
}
