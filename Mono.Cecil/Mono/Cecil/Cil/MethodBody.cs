using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000010 RID: 16
	public sealed class MethodBody : IVariableDefinitionProvider
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004E3A File Offset: 0x0000303A
		public MethodDefinition Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004E42 File Offset: 0x00003042
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004E4A File Offset: 0x0000304A
		public int MaxStackSize
		{
			get
			{
				return this.max_stack_size;
			}
			set
			{
				this.max_stack_size = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004E53 File Offset: 0x00003053
		public int CodeSize
		{
			get
			{
				return this.code_size;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004E5B File Offset: 0x0000305B
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00004E63 File Offset: 0x00003063
		public bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
			set
			{
				this.init_locals = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004E6C File Offset: 0x0000306C
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00004E74 File Offset: 0x00003074
		public MetadataToken LocalVarToken
		{
			get
			{
				return this.local_var_token;
			}
			set
			{
				this.local_var_token = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004E80 File Offset: 0x00003080
		public Collection<Instruction> Instructions
		{
			get
			{
				Collection<Instruction> result;
				if ((result = this.instructions) == null)
				{
					result = (this.instructions = new InstructionCollection());
				}
				return result;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004EA5 File Offset: 0x000030A5
		public bool HasExceptionHandlers
		{
			get
			{
				return !this.exceptions.IsNullOrEmpty<ExceptionHandler>();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004EB8 File Offset: 0x000030B8
		public Collection<ExceptionHandler> ExceptionHandlers
		{
			get
			{
				Collection<ExceptionHandler> result;
				if ((result = this.exceptions) == null)
				{
					result = (this.exceptions = new Collection<ExceptionHandler>());
				}
				return result;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004EDD File Offset: 0x000030DD
		public bool HasVariables
		{
			get
			{
				return !this.variables.IsNullOrEmpty<VariableDefinition>();
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004EF0 File Offset: 0x000030F0
		public Collection<VariableDefinition> Variables
		{
			get
			{
				Collection<VariableDefinition> result;
				if ((result = this.variables) == null)
				{
					result = (this.variables = new VariableDefinitionCollection());
				}
				return result;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004F15 File Offset: 0x00003115
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004F1D File Offset: 0x0000311D
		public Scope Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004F28 File Offset: 0x00003128
		public ParameterDefinition ThisParameter
		{
			get
			{
				if (this.method == null || this.method.DeclaringType == null)
				{
					throw new NotSupportedException();
				}
				if (!this.method.HasThis)
				{
					return null;
				}
				if (this.this_parameter == null)
				{
					Interlocked.CompareExchange<ParameterDefinition>(ref this.this_parameter, MethodBody.CreateThisParameter(this.method), null);
				}
				return this.this_parameter;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004F88 File Offset: 0x00003188
		private static ParameterDefinition CreateThisParameter(MethodDefinition method)
		{
			TypeDefinition declaringType = method.DeclaringType;
			TypeReference parameterType = (declaringType.IsValueType || declaringType.IsPrimitive) ? new PointerType(declaringType) : declaringType;
			return new ParameterDefinition(parameterType, method);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004FBD File Offset: 0x000031BD
		public MethodBody(MethodDefinition method)
		{
			this.method = method;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004FCC File Offset: 0x000031CC
		public ILProcessor GetILProcessor()
		{
			return new ILProcessor(this);
		}

		// Token: 0x0400011D RID: 285
		internal readonly MethodDefinition method;

		// Token: 0x0400011E RID: 286
		internal ParameterDefinition this_parameter;

		// Token: 0x0400011F RID: 287
		internal int max_stack_size;

		// Token: 0x04000120 RID: 288
		internal int code_size;

		// Token: 0x04000121 RID: 289
		internal bool init_locals;

		// Token: 0x04000122 RID: 290
		internal MetadataToken local_var_token;

		// Token: 0x04000123 RID: 291
		internal Collection<Instruction> instructions;

		// Token: 0x04000124 RID: 292
		internal Collection<ExceptionHandler> exceptions;

		// Token: 0x04000125 RID: 293
		internal Collection<VariableDefinition> variables;

		// Token: 0x04000126 RID: 294
		private Scope scope;
	}
}
