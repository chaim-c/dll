using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x02000035 RID: 53
	public class CodeInstruction
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00009686 File Offset: 0x00007886
		internal CodeInstruction()
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000096A4 File Offset: 0x000078A4
		public CodeInstruction(OpCode opcode, object operand = null)
		{
			this.opcode = opcode;
			this.operand = operand;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000096D0 File Offset: 0x000078D0
		public CodeInstruction(CodeInstruction instruction)
		{
			this.opcode = instruction.opcode;
			this.operand = instruction.operand;
			List<Label> list = instruction.labels;
			List<Label> list2 = new List<Label>(list.Count);
			list2.AddRange(list);
			this.labels = list2;
			List<ExceptionBlock> list3 = instruction.blocks;
			List<ExceptionBlock> list4 = new List<ExceptionBlock>(list3.Count);
			list4.AddRange(list3);
			this.blocks = list4;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000974F File Offset: 0x0000794F
		public CodeInstruction Clone()
		{
			return new CodeInstruction(this)
			{
				labels = new List<Label>(),
				blocks = new List<ExceptionBlock>()
			};
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009770 File Offset: 0x00007970
		public CodeInstruction Clone(OpCode opcode)
		{
			CodeInstruction codeInstruction = this.Clone();
			codeInstruction.opcode = opcode;
			return codeInstruction;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000978C File Offset: 0x0000798C
		public CodeInstruction Clone(object operand)
		{
			CodeInstruction codeInstruction = this.Clone();
			codeInstruction.operand = operand;
			return codeInstruction;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000097A8 File Offset: 0x000079A8
		public static CodeInstruction Call(Type type, string name, Type[] parameters = null, Type[] generics = null)
		{
			MethodInfo methodInfo = AccessTools.Method(type, name, parameters, generics);
			if (methodInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 4);
				defaultInterpolatedStringHandler.AppendLiteral("No method found for type=");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(", name=");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				defaultInterpolatedStringHandler.AppendLiteral(", parameters=");
				defaultInterpolatedStringHandler.AppendFormatted(parameters.Description());
				defaultInterpolatedStringHandler.AppendLiteral(", generics=");
				defaultInterpolatedStringHandler.AppendFormatted(generics.Description());
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return new CodeInstruction(OpCodes.Call, methodInfo);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009840 File Offset: 0x00007A40
		public static CodeInstruction Call(string typeColonMethodname, Type[] parameters = null, Type[] generics = null)
		{
			MethodInfo methodInfo = AccessTools.Method(typeColonMethodname, parameters, generics);
			if (methodInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 3);
				defaultInterpolatedStringHandler.AppendLiteral("No method found for ");
				defaultInterpolatedStringHandler.AppendFormatted(typeColonMethodname);
				defaultInterpolatedStringHandler.AppendLiteral(", parameters=");
				defaultInterpolatedStringHandler.AppendFormatted(parameters.Description());
				defaultInterpolatedStringHandler.AppendLiteral(", generics=");
				defaultInterpolatedStringHandler.AppendFormatted(generics.Description());
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return new CodeInstruction(OpCodes.Call, methodInfo);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000098C1 File Offset: 0x00007AC1
		public static CodeInstruction Call(Expression<Action> expression)
		{
			return new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo(expression));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000098D3 File Offset: 0x00007AD3
		public static CodeInstruction Call<T>(Expression<Action<T>> expression)
		{
			return new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo<T>(expression));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000098E5 File Offset: 0x00007AE5
		public static CodeInstruction Call<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			return new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo<T, TResult>(expression));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000098F7 File Offset: 0x00007AF7
		public static CodeInstruction Call(LambdaExpression expression)
		{
			return new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo(expression));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000990C File Offset: 0x00007B0C
		public static CodeInstruction CallClosure<T>(T closure) where T : Delegate
		{
			if (closure.Method.IsStatic && closure.Target == null)
			{
				return new CodeInstruction(OpCodes.Call, closure.Method);
			}
			Type[] array = (from x in closure.Method.GetParameters()
			select x.ParameterType).ToArray<Type>();
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition(closure.Method.Name, closure.Method.ReturnType, array);
			ILGenerator ilgenerator = dynamicMethodDefinition.GetILGenerator();
			Type type = closure.Target.GetType();
			bool flag;
			if (closure.Target != null)
			{
				flag = type.GetFields().Any((FieldInfo x) => !x.IsStatic);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				int count = CodeInstruction.State.closureCache.Count;
				CodeInstruction.State.closureCache[count] = closure;
				ilgenerator.Emit(OpCodes.Ldsfld, AccessTools.Field(typeof(Transpilers), "closureCache"));
				ilgenerator.Emit(OpCodes.Ldc_I4, count);
				ilgenerator.Emit(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Dictionary<int, Delegate>), "Item"));
			}
			else
			{
				if (closure.Target == null)
				{
					ilgenerator.Emit(OpCodes.Ldnull);
				}
				else
				{
					ilgenerator.Emit(OpCodes.Newobj, AccessTools.FirstConstructor(type, (ConstructorInfo x) => !x.IsStatic && x.GetParameters().Length == 0));
				}
				ilgenerator.Emit(OpCodes.Ldftn, closure.Method);
				ilgenerator.Emit(OpCodes.Newobj, AccessTools.Constructor(typeof(T), new Type[]
				{
					typeof(object),
					typeof(IntPtr)
				}, false));
			}
			for (int i = 0; i < array.Length; i++)
			{
				ilgenerator.Emit(OpCodes.Ldarg, i);
			}
			ilgenerator.Emit(OpCodes.Callvirt, AccessTools.Method(typeof(T), "Invoke", null, null));
			ilgenerator.Emit(OpCodes.Ret);
			return new CodeInstruction(OpCodes.Call, dynamicMethodDefinition.Generate());
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009B6C File Offset: 0x00007D6C
		public static CodeInstruction LoadField(Type type, string name, bool useAddress = false)
		{
			FieldInfo fieldInfo = AccessTools.Field(type, name);
			if (fieldInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 2);
				defaultInterpolatedStringHandler.AppendLiteral("No field found for ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return new CodeInstruction(useAddress ? (fieldInfo.IsStatic ? OpCodes.Ldsflda : OpCodes.Ldflda) : (fieldInfo.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld), fieldInfo);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public static CodeInstruction StoreField(Type type, string name)
		{
			FieldInfo fieldInfo = AccessTools.Field(type, name);
			if (fieldInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 2);
				defaultInterpolatedStringHandler.AppendLiteral("No field found for ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return new CodeInstruction(fieldInfo.IsStatic ? OpCodes.Stsfld : OpCodes.Stfld, fieldInfo);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00009C6C File Offset: 0x00007E6C
		public static CodeInstruction LoadLocal(int index, bool useAddress = false)
		{
			if (useAddress)
			{
				if (index < 256)
				{
					return new CodeInstruction(OpCodes.Ldloca_S, Convert.ToByte(index));
				}
				return new CodeInstruction(OpCodes.Ldloca, index);
			}
			else
			{
				if (index == 0)
				{
					return new CodeInstruction(OpCodes.Ldloc_0, null);
				}
				if (index == 1)
				{
					return new CodeInstruction(OpCodes.Ldloc_1, null);
				}
				if (index == 2)
				{
					return new CodeInstruction(OpCodes.Ldloc_2, null);
				}
				if (index == 3)
				{
					return new CodeInstruction(OpCodes.Ldloc_3, null);
				}
				if (index < 256)
				{
					return new CodeInstruction(OpCodes.Ldloc_S, Convert.ToByte(index));
				}
				return new CodeInstruction(OpCodes.Ldloc, index);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009D18 File Offset: 0x00007F18
		public static CodeInstruction StoreLocal(int index)
		{
			if (index == 0)
			{
				return new CodeInstruction(OpCodes.Stloc_0, null);
			}
			if (index == 1)
			{
				return new CodeInstruction(OpCodes.Stloc_1, null);
			}
			if (index == 2)
			{
				return new CodeInstruction(OpCodes.Stloc_2, null);
			}
			if (index == 3)
			{
				return new CodeInstruction(OpCodes.Stloc_3, null);
			}
			if (index < 256)
			{
				return new CodeInstruction(OpCodes.Stloc_S, Convert.ToByte(index));
			}
			return new CodeInstruction(OpCodes.Stloc, index);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009D94 File Offset: 0x00007F94
		public static CodeInstruction LoadArgument(int index, bool useAddress = false)
		{
			if (useAddress)
			{
				if (index < 256)
				{
					return new CodeInstruction(OpCodes.Ldarga_S, Convert.ToByte(index));
				}
				return new CodeInstruction(OpCodes.Ldarga, index);
			}
			else
			{
				if (index == 0)
				{
					return new CodeInstruction(OpCodes.Ldarg_0, null);
				}
				if (index == 1)
				{
					return new CodeInstruction(OpCodes.Ldarg_1, null);
				}
				if (index == 2)
				{
					return new CodeInstruction(OpCodes.Ldarg_2, null);
				}
				if (index == 3)
				{
					return new CodeInstruction(OpCodes.Ldarg_3, null);
				}
				if (index < 256)
				{
					return new CodeInstruction(OpCodes.Ldarg_S, Convert.ToByte(index));
				}
				return new CodeInstruction(OpCodes.Ldarg, index);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009E40 File Offset: 0x00008040
		public static CodeInstruction StoreArgument(int index)
		{
			if (index < 256)
			{
				return new CodeInstruction(OpCodes.Starg_S, Convert.ToByte(index));
			}
			return new CodeInstruction(OpCodes.Starg, index);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009E70 File Offset: 0x00008070
		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (Label label in this.labels)
			{
				List<string> list2 = list;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Label");
				defaultInterpolatedStringHandler.AppendFormatted<int>(label.GetHashCode());
				list2.Add(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			foreach (ExceptionBlock exceptionBlock in this.blocks)
			{
				list.Add("EX_" + exceptionBlock.blockType.ToString().Replace("Block", ""));
			}
			string str = (list.Count > 0) ? (" [" + string.Join(", ", list.ToArray()) + "]") : "";
			string text = Emitter.FormatArgument(this.operand, null);
			if (text.Length > 0)
			{
				text = " " + text;
			}
			OpCode opCode = this.opcode;
			return opCode.ToString() + text + str;
		}

		// Token: 0x0400007A RID: 122
		public OpCode opcode;

		// Token: 0x0400007B RID: 123
		public object operand;

		// Token: 0x0400007C RID: 124
		public List<Label> labels = new List<Label>();

		// Token: 0x0400007D RID: 125
		public List<ExceptionBlock> blocks = new List<ExceptionBlock>();

		// Token: 0x0200007D RID: 125
		internal static class State
		{
			// Token: 0x04000182 RID: 386
			internal static readonly Dictionary<int, Delegate> closureCache = new Dictionary<int, Delegate>();
		}
	}
}
