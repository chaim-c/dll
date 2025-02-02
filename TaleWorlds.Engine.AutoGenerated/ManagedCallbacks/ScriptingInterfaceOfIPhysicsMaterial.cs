using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000020 RID: 32
	internal class ScriptingInterfaceOfIPhysicsMaterial : IPhysicsMaterial
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00012462 File Offset: 0x00010662
		public float GetDynamicFrictionAtIndex(int index)
		{
			return ScriptingInterfaceOfIPhysicsMaterial.call_GetDynamicFrictionAtIndexDelegate(index);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001246F File Offset: 0x0001066F
		public PhysicsMaterialFlags GetFlagsAtIndex(int index)
		{
			return ScriptingInterfaceOfIPhysicsMaterial.call_GetFlagsAtIndexDelegate(index);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001247C File Offset: 0x0001067C
		public PhysicsMaterial GetIndexWithName(string materialName)
		{
			byte[] array = null;
			if (materialName != null)
			{
				int byteCount = ScriptingInterfaceOfIPhysicsMaterial._utf8.GetByteCount(materialName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIPhysicsMaterial._utf8.GetBytes(materialName, 0, materialName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIPhysicsMaterial.call_GetIndexWithNameDelegate(array);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000124D6 File Offset: 0x000106D6
		public int GetMaterialCount()
		{
			return ScriptingInterfaceOfIPhysicsMaterial.call_GetMaterialCountDelegate();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000124E2 File Offset: 0x000106E2
		public string GetMaterialNameAtIndex(int index)
		{
			if (ScriptingInterfaceOfIPhysicsMaterial.call_GetMaterialNameAtIndexDelegate(index) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000124F9 File Offset: 0x000106F9
		public float GetRestitutionAtIndex(int index)
		{
			return ScriptingInterfaceOfIPhysicsMaterial.call_GetRestitutionAtIndexDelegate(index);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00012506 File Offset: 0x00010706
		public float GetSoftnessAtIndex(int index)
		{
			return ScriptingInterfaceOfIPhysicsMaterial.call_GetSoftnessAtIndexDelegate(index);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00012513 File Offset: 0x00010713
		public float GetStaticFrictionAtIndex(int index)
		{
			return ScriptingInterfaceOfIPhysicsMaterial.call_GetStaticFrictionAtIndexDelegate(index);
		}

		// Token: 0x040002C3 RID: 707
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040002C4 RID: 708
		public static ScriptingInterfaceOfIPhysicsMaterial.GetDynamicFrictionAtIndexDelegate call_GetDynamicFrictionAtIndexDelegate;

		// Token: 0x040002C5 RID: 709
		public static ScriptingInterfaceOfIPhysicsMaterial.GetFlagsAtIndexDelegate call_GetFlagsAtIndexDelegate;

		// Token: 0x040002C6 RID: 710
		public static ScriptingInterfaceOfIPhysicsMaterial.GetIndexWithNameDelegate call_GetIndexWithNameDelegate;

		// Token: 0x040002C7 RID: 711
		public static ScriptingInterfaceOfIPhysicsMaterial.GetMaterialCountDelegate call_GetMaterialCountDelegate;

		// Token: 0x040002C8 RID: 712
		public static ScriptingInterfaceOfIPhysicsMaterial.GetMaterialNameAtIndexDelegate call_GetMaterialNameAtIndexDelegate;

		// Token: 0x040002C9 RID: 713
		public static ScriptingInterfaceOfIPhysicsMaterial.GetRestitutionAtIndexDelegate call_GetRestitutionAtIndexDelegate;

		// Token: 0x040002CA RID: 714
		public static ScriptingInterfaceOfIPhysicsMaterial.GetSoftnessAtIndexDelegate call_GetSoftnessAtIndexDelegate;

		// Token: 0x040002CB RID: 715
		public static ScriptingInterfaceOfIPhysicsMaterial.GetStaticFrictionAtIndexDelegate call_GetStaticFrictionAtIndexDelegate;

		// Token: 0x0200031D RID: 797
		// (Invoke) Token: 0x06001177 RID: 4471
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetDynamicFrictionAtIndexDelegate(int index);

		// Token: 0x0200031E RID: 798
		// (Invoke) Token: 0x0600117B RID: 4475
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate PhysicsMaterialFlags GetFlagsAtIndexDelegate(int index);

		// Token: 0x0200031F RID: 799
		// (Invoke) Token: 0x0600117F RID: 4479
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate PhysicsMaterial GetIndexWithNameDelegate(byte[] materialName);

		// Token: 0x02000320 RID: 800
		// (Invoke) Token: 0x06001183 RID: 4483
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMaterialCountDelegate();

		// Token: 0x02000321 RID: 801
		// (Invoke) Token: 0x06001187 RID: 4487
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMaterialNameAtIndexDelegate(int index);

		// Token: 0x02000322 RID: 802
		// (Invoke) Token: 0x0600118B RID: 4491
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRestitutionAtIndexDelegate(int index);

		// Token: 0x02000323 RID: 803
		// (Invoke) Token: 0x0600118F RID: 4495
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetSoftnessAtIndexDelegate(int index);

		// Token: 0x02000324 RID: 804
		// (Invoke) Token: 0x06001193 RID: 4499
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetStaticFrictionAtIndexDelegate(int index);
	}
}
