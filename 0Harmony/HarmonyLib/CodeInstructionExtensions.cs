using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x0200004C RID: 76
	public static class CodeInstructionExtensions
	{
		// Token: 0x06000397 RID: 919 RVA: 0x00012221 File Offset: 0x00010421
		public static bool IsValid(this OpCode code)
		{
			return code.Size > 0;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00012230 File Offset: 0x00010430
		public static bool OperandIs(this CodeInstruction code, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (code.operand == null)
			{
				return false;
			}
			Type type = value.GetType();
			Type type2 = code.operand.GetType();
			if (AccessTools.IsInteger(type) && AccessTools.IsNumber(type2))
			{
				return Convert.ToInt64(code.operand) == Convert.ToInt64(value);
			}
			if (AccessTools.IsFloatingPoint(type) && AccessTools.IsNumber(type2))
			{
				return Convert.ToDouble(code.operand) == Convert.ToDouble(value);
			}
			return object.Equals(code.operand, value);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000122BC File Offset: 0x000104BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool OperandIs(this CodeInstruction code, MemberInfo value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return object.Equals(code.operand, value);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000122D8 File Offset: 0x000104D8
		public static bool Is(this CodeInstruction code, OpCode opcode, object operand)
		{
			return code.opcode == opcode && code.OperandIs(operand);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000122F1 File Offset: 0x000104F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool Is(this CodeInstruction code, OpCode opcode, MemberInfo operand)
		{
			return code.opcode == opcode && code.OperandIs(operand);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001230C File Offset: 0x0001050C
		public static bool IsLdarg(this CodeInstruction code, int? n = null)
		{
			return ((n == null || n.Value == 0) && code.opcode == OpCodes.Ldarg_0) || ((n == null || n.Value == 1) && code.opcode == OpCodes.Ldarg_1) || ((n == null || n.Value == 2) && code.opcode == OpCodes.Ldarg_2) || ((n == null || n.Value == 3) && code.opcode == OpCodes.Ldarg_3) || (code.opcode == OpCodes.Ldarg && (n == null || n.Value == Convert.ToInt32(code.operand))) || (code.opcode == OpCodes.Ldarg_S && (n == null || n.Value == Convert.ToInt32(code.operand)));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00012418 File Offset: 0x00010618
		public static bool IsLdarga(this CodeInstruction code, int? n = null)
		{
			return (!(code.opcode != OpCodes.Ldarga) || !(code.opcode != OpCodes.Ldarga_S)) && (n == null || n.Value == Convert.ToInt32(code.operand));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001246C File Offset: 0x0001066C
		public static bool IsStarg(this CodeInstruction code, int? n = null)
		{
			return (!(code.opcode != OpCodes.Starg) || !(code.opcode != OpCodes.Starg_S)) && (n == null || n.Value == Convert.ToInt32(code.operand));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000124BE File Offset: 0x000106BE
		public static bool IsLdloc(this CodeInstruction code, LocalBuilder variable = null)
		{
			return (CodeInstructionExtensions.opcodesLoadingLocalNormal.Contains(code.opcode) || CodeInstructionExtensions.opcodesLoadingLocalByAddress.Contains(code.opcode)) && (variable == null || object.Equals(variable, code.operand));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000124F7 File Offset: 0x000106F7
		public static bool IsStloc(this CodeInstruction code, LocalBuilder variable = null)
		{
			return CodeInstructionExtensions.opcodesStoringLocal.Contains(code.opcode) && (variable == null || object.Equals(variable, code.operand));
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001251E File Offset: 0x0001071E
		public static bool Branches(this CodeInstruction code, out Label? label)
		{
			if (CodeInstructionExtensions.opcodesBranching.Contains(code.opcode))
			{
				label = new Label?((Label)code.operand);
				return true;
			}
			label = null;
			return false;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00012554 File Offset: 0x00010754
		public static bool Calls(this CodeInstruction code, MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return (!(code.opcode != OpCodes.Call) || !(code.opcode != OpCodes.Callvirt)) && object.Equals(code.operand, method);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000125A1 File Offset: 0x000107A1
		public static bool LoadsConstant(this CodeInstruction code)
		{
			return CodeInstructionExtensions.constantLoadingCodes.Contains(code.opcode);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000125B4 File Offset: 0x000107B4
		public static bool LoadsConstant(this CodeInstruction code, long number)
		{
			OpCode opcode = code.opcode;
			return (number == -1L && opcode == OpCodes.Ldc_I4_M1) || (number == 0L && opcode == OpCodes.Ldc_I4_0) || (number == 1L && opcode == OpCodes.Ldc_I4_1) || (number == 2L && opcode == OpCodes.Ldc_I4_2) || (number == 3L && opcode == OpCodes.Ldc_I4_3) || (number == 4L && opcode == OpCodes.Ldc_I4_4) || (number == 5L && opcode == OpCodes.Ldc_I4_5) || (number == 6L && opcode == OpCodes.Ldc_I4_6) || (number == 7L && opcode == OpCodes.Ldc_I4_7) || (number == 8L && opcode == OpCodes.Ldc_I4_8) || ((!(opcode != OpCodes.Ldc_I4) || !(opcode != OpCodes.Ldc_I4_S) || !(opcode != OpCodes.Ldc_I8)) && Convert.ToInt64(code.operand) == number);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000126C8 File Offset: 0x000108C8
		public static bool LoadsConstant(this CodeInstruction code, double number)
		{
			if (code.opcode != OpCodes.Ldc_R4 && code.opcode != OpCodes.Ldc_R8)
			{
				return false;
			}
			double num = Convert.ToDouble(code.operand);
			return num == number;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001270B File Offset: 0x0001090B
		public static bool LoadsConstant(this CodeInstruction code, Enum e)
		{
			return code.LoadsConstant(Convert.ToInt64(e));
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001271C File Offset: 0x0001091C
		public static bool LoadsConstant(this CodeInstruction code, string str)
		{
			if (code.opcode != OpCodes.Ldstr)
			{
				return false;
			}
			string a = Convert.ToString(code.operand);
			return a == str;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00012750 File Offset: 0x00010950
		public static bool LoadsField(this CodeInstruction code, FieldInfo field, bool byAddress = false)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			OpCode b = field.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld;
			if (!byAddress && code.opcode == b && object.Equals(code.operand, field))
			{
				return true;
			}
			OpCode b2 = field.IsStatic ? OpCodes.Ldsflda : OpCodes.Ldflda;
			return byAddress && code.opcode == b2 && object.Equals(code.operand, field);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000127D8 File Offset: 0x000109D8
		public static bool StoresField(this CodeInstruction code, FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			OpCode b = field.IsStatic ? OpCodes.Stsfld : OpCodes.Stfld;
			return code.opcode == b && object.Equals(code.operand, field);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00012824 File Offset: 0x00010A24
		public static int LocalIndex(this CodeInstruction code)
		{
			if (code.opcode == OpCodes.Ldloc_0 || code.opcode == OpCodes.Stloc_0)
			{
				return 0;
			}
			if (code.opcode == OpCodes.Ldloc_1 || code.opcode == OpCodes.Stloc_1)
			{
				return 1;
			}
			if (code.opcode == OpCodes.Ldloc_2 || code.opcode == OpCodes.Stloc_2)
			{
				return 2;
			}
			if (code.opcode == OpCodes.Ldloc_3 || code.opcode == OpCodes.Stloc_3)
			{
				return 3;
			}
			if (code.opcode == OpCodes.Ldloc_S || code.opcode == OpCodes.Ldloc)
			{
				return Convert.ToInt32(code.operand);
			}
			if (code.opcode == OpCodes.Stloc_S || code.opcode == OpCodes.Stloc)
			{
				return Convert.ToInt32(code.operand);
			}
			if (code.opcode == OpCodes.Ldloca_S || code.opcode == OpCodes.Ldloca)
			{
				return Convert.ToInt32(code.operand);
			}
			throw new ArgumentException("Instruction is not a load or store", "code");
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00012968 File Offset: 0x00010B68
		public static int ArgumentIndex(this CodeInstruction code)
		{
			if (code.opcode == OpCodes.Ldarg_0)
			{
				return 0;
			}
			if (code.opcode == OpCodes.Ldarg_1)
			{
				return 1;
			}
			if (code.opcode == OpCodes.Ldarg_2)
			{
				return 2;
			}
			if (code.opcode == OpCodes.Ldarg_3)
			{
				return 3;
			}
			if (code.opcode == OpCodes.Ldarg_S || code.opcode == OpCodes.Ldarg)
			{
				return Convert.ToInt32(code.operand);
			}
			if (code.opcode == OpCodes.Starg_S || code.opcode == OpCodes.Starg)
			{
				return Convert.ToInt32(code.operand);
			}
			if (code.opcode == OpCodes.Ldarga_S || code.opcode == OpCodes.Ldarga)
			{
				return Convert.ToInt32(code.operand);
			}
			throw new ArgumentException("Instruction is not a load or store", "code");
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00012A64 File Offset: 0x00010C64
		public static CodeInstruction WithLabels(this CodeInstruction code, params Label[] labels)
		{
			code.labels.AddRange(labels);
			return code;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00012A73 File Offset: 0x00010C73
		public static CodeInstruction WithLabels(this CodeInstruction code, IEnumerable<Label> labels)
		{
			code.labels.AddRange(labels);
			return code;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012A84 File Offset: 0x00010C84
		public static List<Label> ExtractLabels(this CodeInstruction code)
		{
			List<Label> result = new List<Label>(code.labels);
			code.labels.Clear();
			return result;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012AA9 File Offset: 0x00010CA9
		public static CodeInstruction MoveLabelsTo(this CodeInstruction code, CodeInstruction other)
		{
			other.WithLabels(code.ExtractLabels());
			return code;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012AB9 File Offset: 0x00010CB9
		public static CodeInstruction MoveLabelsFrom(this CodeInstruction code, CodeInstruction other)
		{
			return code.WithLabels(other.ExtractLabels());
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012AC7 File Offset: 0x00010CC7
		public static CodeInstruction WithBlocks(this CodeInstruction code, params ExceptionBlock[] blocks)
		{
			code.blocks.AddRange(blocks);
			return code;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012AD6 File Offset: 0x00010CD6
		public static CodeInstruction WithBlocks(this CodeInstruction code, IEnumerable<ExceptionBlock> blocks)
		{
			code.blocks.AddRange(blocks);
			return code;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012AE8 File Offset: 0x00010CE8
		public static List<ExceptionBlock> ExtractBlocks(this CodeInstruction code)
		{
			List<ExceptionBlock> result = new List<ExceptionBlock>(code.blocks);
			code.blocks.Clear();
			return result;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012B0D File Offset: 0x00010D0D
		public static CodeInstruction MoveBlocksTo(this CodeInstruction code, CodeInstruction other)
		{
			other.WithBlocks(code.ExtractBlocks());
			return code;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00012B1D File Offset: 0x00010D1D
		public static CodeInstruction MoveBlocksFrom(this CodeInstruction code, CodeInstruction other)
		{
			return code.WithBlocks(other.ExtractBlocks());
		}

		// Token: 0x040000DB RID: 219
		internal static readonly HashSet<OpCode> opcodesCalling = new HashSet<OpCode>
		{
			OpCodes.Call,
			OpCodes.Callvirt
		};

		// Token: 0x040000DC RID: 220
		internal static readonly HashSet<OpCode> opcodesLoadingLocalByAddress = new HashSet<OpCode>
		{
			OpCodes.Ldloca_S,
			OpCodes.Ldloca
		};

		// Token: 0x040000DD RID: 221
		internal static readonly HashSet<OpCode> opcodesLoadingLocalNormal = new HashSet<OpCode>
		{
			OpCodes.Ldloc_0,
			OpCodes.Ldloc_1,
			OpCodes.Ldloc_2,
			OpCodes.Ldloc_3,
			OpCodes.Ldloc_S,
			OpCodes.Ldloc
		};

		// Token: 0x040000DE RID: 222
		internal static readonly HashSet<OpCode> opcodesStoringLocal = new HashSet<OpCode>
		{
			OpCodes.Stloc_0,
			OpCodes.Stloc_1,
			OpCodes.Stloc_2,
			OpCodes.Stloc_3,
			OpCodes.Stloc_S,
			OpCodes.Stloc
		};

		// Token: 0x040000DF RID: 223
		internal static readonly HashSet<OpCode> opcodesLoadingArgumentByAddress = new HashSet<OpCode>
		{
			OpCodes.Ldarga_S,
			OpCodes.Ldarga
		};

		// Token: 0x040000E0 RID: 224
		internal static readonly HashSet<OpCode> opcodesLoadingArgumentNormal = new HashSet<OpCode>
		{
			OpCodes.Ldarg_0,
			OpCodes.Ldarg_1,
			OpCodes.Ldarg_2,
			OpCodes.Ldarg_3,
			OpCodes.Ldarg_S,
			OpCodes.Ldarg
		};

		// Token: 0x040000E1 RID: 225
		internal static readonly HashSet<OpCode> opcodesStoringArgument = new HashSet<OpCode>
		{
			OpCodes.Starg_S,
			OpCodes.Starg
		};

		// Token: 0x040000E2 RID: 226
		internal static readonly HashSet<OpCode> opcodesBranching = new HashSet<OpCode>
		{
			OpCodes.Br_S,
			OpCodes.Brfalse_S,
			OpCodes.Brtrue_S,
			OpCodes.Beq_S,
			OpCodes.Bge_S,
			OpCodes.Bgt_S,
			OpCodes.Ble_S,
			OpCodes.Blt_S,
			OpCodes.Bne_Un_S,
			OpCodes.Bge_Un_S,
			OpCodes.Bgt_Un_S,
			OpCodes.Ble_Un_S,
			OpCodes.Blt_Un_S,
			OpCodes.Br,
			OpCodes.Brfalse,
			OpCodes.Brtrue,
			OpCodes.Beq,
			OpCodes.Bge,
			OpCodes.Bgt,
			OpCodes.Ble,
			OpCodes.Blt,
			OpCodes.Bne_Un,
			OpCodes.Bge_Un,
			OpCodes.Bgt_Un,
			OpCodes.Ble_Un,
			OpCodes.Blt_Un
		};

		// Token: 0x040000E3 RID: 227
		private static readonly HashSet<OpCode> constantLoadingCodes = new HashSet<OpCode>
		{
			OpCodes.Ldc_I4_M1,
			OpCodes.Ldc_I4_0,
			OpCodes.Ldc_I4_1,
			OpCodes.Ldc_I4_2,
			OpCodes.Ldc_I4_3,
			OpCodes.Ldc_I4_4,
			OpCodes.Ldc_I4_5,
			OpCodes.Ldc_I4_6,
			OpCodes.Ldc_I4_7,
			OpCodes.Ldc_I4_8,
			OpCodes.Ldc_I4,
			OpCodes.Ldc_I4_S,
			OpCodes.Ldc_I8,
			OpCodes.Ldc_R4,
			OpCodes.Ldc_R8
		};
	}
}
