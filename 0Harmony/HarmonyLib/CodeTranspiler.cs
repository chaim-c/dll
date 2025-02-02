using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x0200000E RID: 14
	internal class CodeTranspiler
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002E84 File Offset: 0x00001084
		internal CodeTranspiler(List<ILInstruction> ilInstructions)
		{
			this.codeInstructions = (from ilInstruction in ilInstructions
			select ilInstruction.GetCodeInstruction()).ToList<CodeInstruction>().AsEnumerable<CodeInstruction>();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002ED7 File Offset: 0x000010D7
		internal void Add(MethodInfo transpiler)
		{
			this.transpilers.Add(transpiler);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002EE8 File Offset: 0x000010E8
		internal static object ConvertInstruction(Type type, object instruction, out Dictionary<string, object> unassigned)
		{
			Dictionary<string, object> nonExisting = new Dictionary<string, object>();
			object result = AccessTools.MakeDeepCopy(instruction, type, delegate(string namePath, Traverse trvSrc, Traverse trvDest)
			{
				object value = trvSrc.GetValue();
				if (!trvDest.FieldExists())
				{
					nonExisting[namePath] = value;
					return null;
				}
				if (namePath == "opcode")
				{
					return CodeTranspiler.ReplaceShortJumps((OpCode)value);
				}
				return value;
			}, "");
			unassigned = nonExisting;
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F28 File Offset: 0x00001128
		internal static bool ShouldAddExceptionInfo(object op, int opIndex, List<object> originalInstructions, List<object> newInstructions, Dictionary<object, Dictionary<string, object>> unassignedValues)
		{
			int num = originalInstructions.IndexOf(op);
			if (num == -1)
			{
				return false;
			}
			Dictionary<string, object> unassigned;
			if (!unassignedValues.TryGetValue(op, out unassigned))
			{
				return false;
			}
			object blocksObject;
			if (!unassigned.TryGetValue("blocks", out blocksObject))
			{
				return false;
			}
			List<ExceptionBlock> blocks = blocksObject as List<ExceptionBlock>;
			int num2 = newInstructions.Count((object instr) => instr == op);
			if (num2 <= 1)
			{
				return true;
			}
			ExceptionBlock exceptionBlock = blocks.FirstOrDefault((ExceptionBlock block) => block.blockType != ExceptionBlockType.EndExceptionBlock);
			ExceptionBlock exceptionBlock2 = blocks.FirstOrDefault((ExceptionBlock block) => block.blockType == ExceptionBlockType.EndExceptionBlock);
			if (exceptionBlock != null && exceptionBlock2 == null)
			{
				object obj = originalInstructions.Skip(num + 1).FirstOrDefault(delegate(object instr)
				{
					if (!unassignedValues.TryGetValue(instr, out unassigned))
					{
						return false;
					}
					if (!unassigned.TryGetValue("blocks", out blocksObject))
					{
						return false;
					}
					blocks = (blocksObject as List<ExceptionBlock>);
					return blocks.Count > 0;
				});
				if (obj != null)
				{
					int num3 = num + 1;
					int num4 = num3 + originalInstructions.Skip(num3).ToList<object>().IndexOf(obj) - 1;
					IEnumerable<object> first = originalInstructions.GetRange(num3, num4 - num3).Intersect(newInstructions);
					obj = newInstructions.Skip(opIndex + 1).FirstOrDefault(delegate(object instr)
					{
						if (!unassignedValues.TryGetValue(instr, out unassigned))
						{
							return false;
						}
						if (!unassigned.TryGetValue("blocks", out blocksObject))
						{
							return false;
						}
						blocks = (blocksObject as List<ExceptionBlock>);
						return blocks.Count > 0;
					});
					if (obj != null)
					{
						num3 = opIndex + 1;
						num4 = num3 + newInstructions.Skip(opIndex + 1).ToList<object>().IndexOf(obj) - 1;
						List<object> range = newInstructions.GetRange(num3, num4 - num3);
						List<object> list = first.Except(range).ToList<object>();
						return list.Count == 0;
					}
				}
			}
			if (exceptionBlock == null && exceptionBlock2 != null)
			{
				object obj2 = originalInstructions.GetRange(0, num).LastOrDefault(delegate(object instr)
				{
					if (!unassignedValues.TryGetValue(instr, out unassigned))
					{
						return false;
					}
					if (!unassigned.TryGetValue("blocks", out blocksObject))
					{
						return false;
					}
					blocks = (blocksObject as List<ExceptionBlock>);
					return blocks.Count > 0;
				});
				if (obj2 != null)
				{
					int num5 = originalInstructions.GetRange(0, num).LastIndexOf(obj2);
					int num6 = num;
					IEnumerable<object> first2 = originalInstructions.GetRange(num5, num6 - num5).Intersect(newInstructions);
					obj2 = newInstructions.GetRange(0, opIndex).LastOrDefault(delegate(object instr)
					{
						if (!unassignedValues.TryGetValue(instr, out unassigned))
						{
							return false;
						}
						if (!unassigned.TryGetValue("blocks", out blocksObject))
						{
							return false;
						}
						blocks = (blocksObject as List<ExceptionBlock>);
						return blocks.Count > 0;
					});
					if (obj2 != null)
					{
						num5 = newInstructions.GetRange(0, opIndex).LastIndexOf(obj2);
						List<object> range2 = newInstructions.GetRange(num5, opIndex - num5);
						IEnumerable<object> source = first2.Except(range2);
						return !source.Any<object>();
					}
				}
			}
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003194 File Offset: 0x00001394
		internal static IEnumerable ConvertInstructionsAndUnassignedValues(Type type, IEnumerable enumerable, out Dictionary<object, Dictionary<string, object>> unassignedValues)
		{
			Assembly assembly = type.GetGenericTypeDefinition().Assembly;
			Type type2 = assembly.GetType(typeof(List<>).FullName);
			Type type3 = type.GetGenericArguments()[0];
			Type type4 = type2.MakeGenericType(new Type[]
			{
				type3
			});
			Type type5 = assembly.GetType(type4.FullName);
			object obj = Activator.CreateInstance(type5);
			MethodInfo method = obj.GetType().GetMethod("Add");
			unassignedValues = new Dictionary<object, Dictionary<string, object>>();
			foreach (object instruction in enumerable)
			{
				Dictionary<string, object> value;
				object obj2 = CodeTranspiler.ConvertInstruction(type3, instruction, out value);
				unassignedValues.Add(obj2, value);
				method.Invoke(obj, new object[]
				{
					obj2
				});
			}
			return obj as IEnumerable;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003284 File Offset: 0x00001484
		internal static IEnumerable ConvertToOurInstructions(IEnumerable instructions, Type codeInstructionType, List<object> originalInstructions, Dictionary<object, Dictionary<string, object>> unassignedValues)
		{
			List<object> newInstructions = instructions.Cast<object>().ToList<object>();
			int index = -1;
			foreach (object obj in newInstructions)
			{
				int num = index;
				index = num + 1;
				object obj2 = AccessTools.MakeDeepCopy(obj, codeInstructionType, null, "");
				Dictionary<string, object> dictionary;
				if (unassignedValues.TryGetValue(obj, out dictionary))
				{
					bool flag = CodeTranspiler.ShouldAddExceptionInfo(obj, index, originalInstructions, newInstructions, unassignedValues);
					Traverse traverse = Traverse.Create(obj2);
					foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					{
						if (flag || keyValuePair.Key != "blocks")
						{
							traverse.Field(keyValuePair.Key).SetValue(keyValuePair.Value);
						}
					}
				}
				yield return obj2;
			}
			List<object>.Enumerator enumerator = default(List<object>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000032A9 File Offset: 0x000014A9
		private static bool IsCodeInstructionsParameter(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition().Name.StartsWith("IEnumerable", StringComparison.Ordinal);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000032CC File Offset: 0x000014CC
		internal static IEnumerable ConvertToGeneralInstructions(MethodInfo transpiler, IEnumerable enumerable, out Dictionary<object, Dictionary<string, object>> unassignedValues)
		{
			IEnumerable<Type> source = from p in transpiler.GetParameters()
			select p.ParameterType;
			Func<Type, bool> predicate;
			if ((predicate = CodeTranspiler.<>O.<0>__IsCodeInstructionsParameter) == null)
			{
				predicate = (CodeTranspiler.<>O.<0>__IsCodeInstructionsParameter = new Func<Type, bool>(CodeTranspiler.IsCodeInstructionsParameter));
			}
			Type type = source.FirstOrDefault(predicate);
			if (type == typeof(IEnumerable<CodeInstruction>))
			{
				unassignedValues = null;
				IList<CodeInstruction> result;
				if ((result = (enumerable as IList<CodeInstruction>)) == null)
				{
					result = ((enumerable as IEnumerable<CodeInstruction>) ?? enumerable.Cast<CodeInstruction>()).ToList<CodeInstruction>();
				}
				return result;
			}
			return CodeTranspiler.ConvertInstructionsAndUnassignedValues(type, enumerable, out unassignedValues);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003364 File Offset: 0x00001564
		internal static List<object> GetTranspilerCallParameters(ILGenerator generator, MethodInfo transpiler, MethodBase method, IEnumerable instructions)
		{
			List<object> parameter = new List<object>();
			(from param in transpiler.GetParameters()
			select param.ParameterType).Do(delegate(Type type)
			{
				if (type.IsAssignableFrom(typeof(ILGenerator)))
				{
					parameter.Add(generator);
					return;
				}
				if (type.IsAssignableFrom(typeof(MethodBase)))
				{
					parameter.Add(method);
					return;
				}
				if (CodeTranspiler.IsCodeInstructionsParameter(type))
				{
					parameter.Add(instructions);
				}
			});
			return parameter;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000033D8 File Offset: 0x000015D8
		internal List<CodeInstruction> GetResult(ILGenerator generator, MethodBase method)
		{
			IEnumerable instructions = this.codeInstructions;
			this.transpilers.ForEach(delegate(MethodInfo transpiler)
			{
				Dictionary<object, Dictionary<string, object>> dictionary;
				instructions = CodeTranspiler.ConvertToGeneralInstructions(transpiler, instructions, out dictionary);
				List<object> originalInstructions = null;
				if (dictionary != null)
				{
					originalInstructions = instructions.Cast<object>().ToList<object>();
				}
				List<object> transpilerCallParameters = CodeTranspiler.GetTranspilerCallParameters(generator, transpiler, method, instructions);
				IEnumerable enumerable = transpiler.Invoke(null, transpilerCallParameters.ToArray()) as IEnumerable;
				if (enumerable != null)
				{
					instructions = enumerable;
				}
				if (dictionary != null)
				{
					instructions = CodeTranspiler.ConvertToOurInstructions(instructions, typeof(CodeInstruction), originalInstructions, dictionary);
				}
			});
			return (instructions as List<CodeInstruction>) ?? instructions.Cast<CodeInstruction>().ToList<CodeInstruction>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
		private static OpCode ReplaceShortJumps(OpCode opcode)
		{
			foreach (KeyValuePair<OpCode, OpCode> keyValuePair in CodeTranspiler.allJumpCodes)
			{
				if (opcode == keyValuePair.Key)
				{
					return keyValuePair.Value;
				}
			}
			return opcode;
		}

		// Token: 0x0400000E RID: 14
		private readonly IEnumerable<CodeInstruction> codeInstructions;

		// Token: 0x0400000F RID: 15
		private readonly List<MethodInfo> transpilers = new List<MethodInfo>();

		// Token: 0x04000010 RID: 16
		private static readonly Dictionary<OpCode, OpCode> allJumpCodes = new Dictionary<OpCode, OpCode>
		{
			{
				OpCodes.Beq_S,
				OpCodes.Beq
			},
			{
				OpCodes.Bge_S,
				OpCodes.Bge
			},
			{
				OpCodes.Bge_Un_S,
				OpCodes.Bge_Un
			},
			{
				OpCodes.Bgt_S,
				OpCodes.Bgt
			},
			{
				OpCodes.Bgt_Un_S,
				OpCodes.Bgt_Un
			},
			{
				OpCodes.Ble_S,
				OpCodes.Ble
			},
			{
				OpCodes.Ble_Un_S,
				OpCodes.Ble_Un
			},
			{
				OpCodes.Blt_S,
				OpCodes.Blt
			},
			{
				OpCodes.Blt_Un_S,
				OpCodes.Blt_Un
			},
			{
				OpCodes.Bne_Un_S,
				OpCodes.Bne_Un
			},
			{
				OpCodes.Brfalse_S,
				OpCodes.Brfalse
			},
			{
				OpCodes.Brtrue_S,
				OpCodes.Brtrue
			},
			{
				OpCodes.Br_S,
				OpCodes.Br
			},
			{
				OpCodes.Leave_S,
				OpCodes.Leave
			}
		};

		// Token: 0x0200005A RID: 90
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000105 RID: 261
			public static Func<Type, bool> <0>__IsCodeInstructionsParameter;
		}
	}
}
