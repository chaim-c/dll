using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x02000004 RID: 4
	public class DelegateTypeFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public DelegateTypeFactory()
		{
			DelegateTypeFactory.counter++;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
			defaultInterpolatedStringHandler.AppendLiteral("HarmonyDTFAssembly");
			defaultInterpolatedStringHandler.AppendFormatted<int>(DelegateTypeFactory.counter);
			string name = defaultInterpolatedStringHandler.ToStringAndClear();
			AssemblyBuilder assemblyBuilder = PatchTools.DefineDynamicAssembly(name);
			AssemblyBuilder assemblyBuilder2 = assemblyBuilder;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
			defaultInterpolatedStringHandler.AppendLiteral("HarmonyDTFModule");
			defaultInterpolatedStringHandler.AppendFormatted<int>(DelegateTypeFactory.counter);
			this.module = assemblyBuilder2.DefineDynamicModule(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F0 File Offset: 0x000002F0
		public Type CreateDelegateType(MethodInfo method)
		{
			TypeAttributes attr = TypeAttributes.Public | TypeAttributes.Sealed;
			ModuleBuilder moduleBuilder = this.module;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
			defaultInterpolatedStringHandler.AppendLiteral("HarmonyDTFType");
			defaultInterpolatedStringHandler.AppendFormatted<int>(DelegateTypeFactory.counter);
			TypeBuilder typeBuilder = moduleBuilder.DefineType(defaultInterpolatedStringHandler.ToStringAndClear(), attr, typeof(MulticastDelegate));
			ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName, CallingConventions.Standard, new Type[]
			{
				typeof(object),
				typeof(IntPtr)
			});
			constructorBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			ParameterInfo[] parameters = method.GetParameters();
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("Invoke", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, method.ReturnType, parameters.Types());
			methodBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			for (int i = 0; i < parameters.Length; i++)
			{
				methodBuilder.DefineParameter(i + 1, ParameterAttributes.None, parameters[i].Name);
			}
			return typeBuilder.CreateType();
		}

		// Token: 0x04000002 RID: 2
		private readonly ModuleBuilder module;

		// Token: 0x04000003 RID: 3
		private static int counter;
	}
}
