using System;
using System.Collections.Generic;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000004 RID: 4
	internal sealed class CodeWriter : ByteBuffer
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002952 File Offset: 0x00000B52
		public CodeWriter(MetadataBuilder metadata) : base(0)
		{
			this.code_base = metadata.text_map.GetNextRVA(TextSegment.CLIHeader);
			this.current = this.code_base;
			this.metadata = metadata;
			this.standalone_signatures = new Dictionary<uint, MetadataToken>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000298C File Offset: 0x00000B8C
		public uint WriteMethodBody(MethodDefinition method)
		{
			uint result = this.BeginMethod();
			if (CodeWriter.IsUnresolved(method))
			{
				if (method.rva == 0U)
				{
					return 0U;
				}
				this.WriteUnresolvedMethodBody(method);
			}
			else
			{
				if (CodeWriter.IsEmptyMethodBody(method.Body))
				{
					return 0U;
				}
				this.WriteResolvedMethodBody(method);
			}
			this.Align(4);
			this.EndMethod();
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000029DF File Offset: 0x00000BDF
		private static bool IsEmptyMethodBody(MethodBody body)
		{
			return body.instructions.IsNullOrEmpty<Instruction>() && body.variables.IsNullOrEmpty<VariableDefinition>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029FB File Offset: 0x00000BFB
		private static bool IsUnresolved(MethodDefinition method)
		{
			return method.HasBody && method.HasImage && method.body == null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A20 File Offset: 0x00000C20
		private void WriteUnresolvedMethodBody(MethodDefinition method)
		{
			CodeReader codeReader = this.metadata.module.Read<MethodDefinition, CodeReader>(method, (MethodDefinition _, MetadataReader reader) => reader.code);
			MethodSymbols methodSymbols;
			ByteBuffer buffer = codeReader.PatchRawMethodBody(method, this, out methodSymbols);
			base.WriteBytes(buffer);
			if (methodSymbols.instructions.IsNullOrEmpty<InstructionSymbol>())
			{
				return;
			}
			methodSymbols.method_token = method.token;
			methodSymbols.local_var_token = CodeWriter.GetLocalVarToken(buffer, methodSymbols);
			ISymbolWriter symbol_writer = this.metadata.symbol_writer;
			if (symbol_writer != null)
			{
				symbol_writer.Write(methodSymbols);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002AAB File Offset: 0x00000CAB
		private static MetadataToken GetLocalVarToken(ByteBuffer buffer, MethodSymbols symbols)
		{
			if (symbols.variables.IsNullOrEmpty<VariableDefinition>())
			{
				return MetadataToken.Zero;
			}
			buffer.position = 8;
			return new MetadataToken(buffer.ReadUInt32());
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private void WriteResolvedMethodBody(MethodDefinition method)
		{
			this.body = method.Body;
			this.ComputeHeader();
			if (this.RequiresFatHeader())
			{
				this.WriteFatHeader();
			}
			else
			{
				base.WriteByte((byte)(2 | this.body.CodeSize << 2));
			}
			this.WriteInstructions();
			if (this.body.HasExceptionHandlers)
			{
				this.WriteExceptionHandlers();
			}
			ISymbolWriter symbol_writer = this.metadata.symbol_writer;
			if (symbol_writer != null)
			{
				symbol_writer.Write(this.body);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B50 File Offset: 0x00000D50
		private void WriteFatHeader()
		{
			MethodBody methodBody = this.body;
			byte b = 3;
			if (methodBody.InitLocals)
			{
				b |= 16;
			}
			if (methodBody.HasExceptionHandlers)
			{
				b |= 8;
			}
			base.WriteByte(b);
			base.WriteByte(48);
			base.WriteInt16((short)methodBody.max_stack_size);
			base.WriteInt32(methodBody.code_size);
			methodBody.local_var_token = (methodBody.HasVariables ? this.GetStandAloneSignature(methodBody.Variables) : MetadataToken.Zero);
			this.WriteMetadataToken(methodBody.local_var_token);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002BD8 File Offset: 0x00000DD8
		private void WriteInstructions()
		{
			Collection<Instruction> instructions = this.body.Instructions;
			Instruction[] items = instructions.items;
			int size = instructions.size;
			for (int i = 0; i < size; i++)
			{
				Instruction instruction = items[i];
				this.WriteOpCode(instruction.opcode);
				this.WriteOperand(instruction);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C25 File Offset: 0x00000E25
		private void WriteOpCode(OpCode opcode)
		{
			if (opcode.Size == 1)
			{
				base.WriteByte(opcode.Op2);
				return;
			}
			base.WriteByte(opcode.Op1);
			base.WriteByte(opcode.Op2);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C5C File Offset: 0x00000E5C
		private void WriteOperand(Instruction instruction)
		{
			OpCode opcode = instruction.opcode;
			OperandType operandType = opcode.OperandType;
			if (operandType == OperandType.InlineNone)
			{
				return;
			}
			object operand = instruction.operand;
			if (operand == null)
			{
				throw new ArgumentException();
			}
			switch (operandType)
			{
			case OperandType.InlineBrTarget:
			{
				Instruction instruction2 = (Instruction)operand;
				base.WriteInt32(this.GetTargetOffset(instruction2) - (instruction.Offset + opcode.Size + 4));
				return;
			}
			case OperandType.InlineField:
			case OperandType.InlineMethod:
			case OperandType.InlineTok:
			case OperandType.InlineType:
				this.WriteMetadataToken(this.metadata.LookupToken((IMetadataTokenProvider)operand));
				return;
			case OperandType.InlineI:
				base.WriteInt32((int)operand);
				return;
			case OperandType.InlineI8:
				base.WriteInt64((long)operand);
				return;
			case OperandType.InlineR:
				base.WriteDouble((double)operand);
				return;
			case OperandType.InlineSig:
				this.WriteMetadataToken(this.GetStandAloneSignature((CallSite)operand));
				return;
			case OperandType.InlineString:
				this.WriteMetadataToken(new MetadataToken(TokenType.String, this.GetUserStringIndex((string)operand)));
				return;
			case OperandType.InlineSwitch:
			{
				Instruction[] array = (Instruction[])operand;
				base.WriteInt32(array.Length);
				int num = instruction.Offset + opcode.Size + 4 * (array.Length + 1);
				for (int i = 0; i < array.Length; i++)
				{
					base.WriteInt32(this.GetTargetOffset(array[i]) - num);
				}
				return;
			}
			case OperandType.InlineVar:
				base.WriteInt16((short)CodeWriter.GetVariableIndex((VariableDefinition)operand));
				return;
			case OperandType.InlineArg:
				base.WriteInt16((short)this.GetParameterIndex((ParameterDefinition)operand));
				return;
			case OperandType.ShortInlineBrTarget:
			{
				Instruction instruction3 = (Instruction)operand;
				base.WriteSByte((sbyte)(this.GetTargetOffset(instruction3) - (instruction.Offset + opcode.Size + 1)));
				return;
			}
			case OperandType.ShortInlineI:
				if (opcode == OpCodes.Ldc_I4_S)
				{
					base.WriteSByte((sbyte)operand);
					return;
				}
				base.WriteByte((byte)operand);
				return;
			case OperandType.ShortInlineR:
				base.WriteSingle((float)operand);
				return;
			case OperandType.ShortInlineVar:
				base.WriteByte((byte)CodeWriter.GetVariableIndex((VariableDefinition)operand));
				return;
			case OperandType.ShortInlineArg:
				base.WriteByte((byte)this.GetParameterIndex((ParameterDefinition)operand));
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002E84 File Offset: 0x00001084
		private int GetTargetOffset(Instruction instruction)
		{
			if (instruction == null)
			{
				Instruction instruction2 = this.body.instructions[this.body.instructions.size - 1];
				return instruction2.offset + instruction2.GetSize();
			}
			return instruction.offset;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002ECB File Offset: 0x000010CB
		private uint GetUserStringIndex(string @string)
		{
			if (@string == null)
			{
				return 0U;
			}
			return this.metadata.user_string_heap.GetStringIndex(@string);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002EE3 File Offset: 0x000010E3
		private static int GetVariableIndex(VariableDefinition variable)
		{
			return variable.Index;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002EEB File Offset: 0x000010EB
		private int GetParameterIndex(ParameterDefinition parameter)
		{
			if (!this.body.method.HasThis)
			{
				return parameter.Index;
			}
			if (parameter == this.body.this_parameter)
			{
				return 0;
			}
			return parameter.Index + 1;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F20 File Offset: 0x00001120
		private bool RequiresFatHeader()
		{
			MethodBody methodBody = this.body;
			return methodBody.CodeSize >= 64 || methodBody.InitLocals || methodBody.HasVariables || methodBody.HasExceptionHandlers || methodBody.MaxStackSize > 8;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F64 File Offset: 0x00001164
		private void ComputeHeader()
		{
			int num = 0;
			Collection<Instruction> instructions = this.body.instructions;
			Instruction[] items = instructions.items;
			int size = instructions.size;
			int num2 = 0;
			int max_stack_size = 0;
			Dictionary<Instruction, int> dictionary = null;
			if (this.body.HasExceptionHandlers)
			{
				this.ComputeExceptionHandlerStackSize(ref dictionary);
			}
			for (int i = 0; i < size; i++)
			{
				Instruction instruction = items[i];
				instruction.offset = num;
				num += instruction.GetSize();
				CodeWriter.ComputeStackSize(instruction, ref dictionary, ref num2, ref max_stack_size);
			}
			this.body.code_size = num;
			this.body.max_stack_size = max_stack_size;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FFC File Offset: 0x000011FC
		private void ComputeExceptionHandlerStackSize(ref Dictionary<Instruction, int> stack_sizes)
		{
			Collection<ExceptionHandler> exceptionHandlers = this.body.ExceptionHandlers;
			for (int i = 0; i < exceptionHandlers.Count; i++)
			{
				ExceptionHandler exceptionHandler = exceptionHandlers[i];
				switch (exceptionHandler.HandlerType)
				{
				case ExceptionHandlerType.Catch:
					CodeWriter.AddExceptionStackSize(exceptionHandler.HandlerStart, ref stack_sizes);
					break;
				case ExceptionHandlerType.Filter:
					CodeWriter.AddExceptionStackSize(exceptionHandler.FilterStart, ref stack_sizes);
					CodeWriter.AddExceptionStackSize(exceptionHandler.HandlerStart, ref stack_sizes);
					break;
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000306B File Offset: 0x0000126B
		private static void AddExceptionStackSize(Instruction handler_start, ref Dictionary<Instruction, int> stack_sizes)
		{
			if (handler_start == null)
			{
				return;
			}
			if (stack_sizes == null)
			{
				stack_sizes = new Dictionary<Instruction, int>();
			}
			stack_sizes[handler_start] = 1;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003088 File Offset: 0x00001288
		private static void ComputeStackSize(Instruction instruction, ref Dictionary<Instruction, int> stack_sizes, ref int stack_size, ref int max_stack)
		{
			int num;
			if (stack_sizes != null && stack_sizes.TryGetValue(instruction, out num))
			{
				stack_size = num;
			}
			max_stack = Math.Max(max_stack, stack_size);
			CodeWriter.ComputeStackDelta(instruction, ref stack_size);
			max_stack = Math.Max(max_stack, stack_size);
			CodeWriter.CopyBranchStackSize(instruction, ref stack_sizes, stack_size);
			CodeWriter.ComputeStackSize(instruction, ref stack_size);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000030D8 File Offset: 0x000012D8
		private static void CopyBranchStackSize(Instruction instruction, ref Dictionary<Instruction, int> stack_sizes, int stack_size)
		{
			if (stack_size == 0)
			{
				return;
			}
			OperandType operandType = instruction.opcode.OperandType;
			if (operandType != OperandType.InlineBrTarget)
			{
				if (operandType == OperandType.InlineSwitch)
				{
					Instruction[] array = (Instruction[])instruction.operand;
					for (int i = 0; i < array.Length; i++)
					{
						CodeWriter.CopyBranchStackSize(ref stack_sizes, array[i], stack_size);
					}
					return;
				}
				if (operandType != OperandType.ShortInlineBrTarget)
				{
					return;
				}
			}
			CodeWriter.CopyBranchStackSize(ref stack_sizes, (Instruction)instruction.operand, stack_size);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000313C File Offset: 0x0000133C
		private static void CopyBranchStackSize(ref Dictionary<Instruction, int> stack_sizes, Instruction target, int stack_size)
		{
			if (stack_sizes == null)
			{
				stack_sizes = new Dictionary<Instruction, int>();
			}
			int num = stack_size;
			int val;
			if (stack_sizes.TryGetValue(target, out val))
			{
				num = Math.Max(num, val);
			}
			stack_sizes[target] = num;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003174 File Offset: 0x00001374
		private static void ComputeStackSize(Instruction instruction, ref int stack_size)
		{
			FlowControl flowControl = instruction.opcode.FlowControl;
			switch (flowControl)
			{
			case FlowControl.Branch:
			case FlowControl.Break:
				break;
			default:
				switch (flowControl)
				{
				case FlowControl.Return:
				case FlowControl.Throw:
					break;
				default:
					return;
				}
				break;
			}
			stack_size = 0;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000031B0 File Offset: 0x000013B0
		private static void ComputeStackDelta(Instruction instruction, ref int stack_size)
		{
			FlowControl flowControl = instruction.opcode.FlowControl;
			if (flowControl == FlowControl.Call)
			{
				IMethodSignature methodSignature = (IMethodSignature)instruction.operand;
				if (methodSignature.HasImplicitThis() && instruction.opcode.Code != Code.Newobj)
				{
					stack_size--;
				}
				if (methodSignature.HasParameters)
				{
					stack_size -= methodSignature.Parameters.Count;
				}
				if (instruction.opcode.Code == Code.Calli)
				{
					stack_size--;
				}
				if (methodSignature.ReturnType.etype != ElementType.Void || instruction.opcode.Code == Code.Newobj)
				{
					stack_size++;
					return;
				}
			}
			else
			{
				CodeWriter.ComputePopDelta(instruction.opcode.StackBehaviourPop, ref stack_size);
				CodeWriter.ComputePushDelta(instruction.opcode.StackBehaviourPush, ref stack_size);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000326C File Offset: 0x0000146C
		private static void ComputePopDelta(StackBehaviour pop_behavior, ref int stack_size)
		{
			switch (pop_behavior)
			{
			case StackBehaviour.Pop1:
			case StackBehaviour.Popi:
			case StackBehaviour.Popref:
				stack_size--;
				return;
			case StackBehaviour.Pop1_pop1:
			case StackBehaviour.Popi_pop1:
			case StackBehaviour.Popi_popi:
			case StackBehaviour.Popi_popi8:
			case StackBehaviour.Popi_popr4:
			case StackBehaviour.Popi_popr8:
			case StackBehaviour.Popref_pop1:
			case StackBehaviour.Popref_popi:
				stack_size -= 2;
				return;
			case StackBehaviour.Popi_popi_popi:
			case StackBehaviour.Popref_popi_popi:
			case StackBehaviour.Popref_popi_popi8:
			case StackBehaviour.Popref_popi_popr4:
			case StackBehaviour.Popref_popi_popr8:
			case StackBehaviour.Popref_popi_popref:
				stack_size -= 3;
				return;
			case StackBehaviour.PopAll:
				stack_size = 0;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000032E4 File Offset: 0x000014E4
		private static void ComputePushDelta(StackBehaviour push_behaviour, ref int stack_size)
		{
			switch (push_behaviour)
			{
			case StackBehaviour.Push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
				stack_size++;
				return;
			case StackBehaviour.Push1_push1:
				stack_size += 2;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003328 File Offset: 0x00001528
		private void WriteExceptionHandlers()
		{
			this.Align(4);
			Collection<ExceptionHandler> exceptionHandlers = this.body.ExceptionHandlers;
			if (exceptionHandlers.Count < 21 && !CodeWriter.RequiresFatSection(exceptionHandlers))
			{
				this.WriteSmallSection(exceptionHandlers);
				return;
			}
			this.WriteFatSection(exceptionHandlers);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000336C File Offset: 0x0000156C
		private static bool RequiresFatSection(Collection<ExceptionHandler> handlers)
		{
			for (int i = 0; i < handlers.Count; i++)
			{
				ExceptionHandler exceptionHandler = handlers[i];
				if (CodeWriter.IsFatRange(exceptionHandler.TryStart, exceptionHandler.TryEnd))
				{
					return true;
				}
				if (CodeWriter.IsFatRange(exceptionHandler.HandlerStart, exceptionHandler.HandlerEnd))
				{
					return true;
				}
				if (exceptionHandler.HandlerType == ExceptionHandlerType.Filter && CodeWriter.IsFatRange(exceptionHandler.FilterStart, exceptionHandler.HandlerStart))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000033DB File Offset: 0x000015DB
		private static bool IsFatRange(Instruction start, Instruction end)
		{
			if (start == null)
			{
				throw new ArgumentException();
			}
			return end == null || end.Offset - start.Offset > 255 || start.Offset > 65535;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003424 File Offset: 0x00001624
		private void WriteSmallSection(Collection<ExceptionHandler> handlers)
		{
			base.WriteByte(1);
			base.WriteByte((byte)(handlers.Count * 12 + 4));
			base.WriteBytes(2);
			this.WriteExceptionHandlers(handlers, delegate(int i)
			{
				base.WriteUInt16((ushort)i);
			}, delegate(int i)
			{
				base.WriteByte((byte)i);
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003470 File Offset: 0x00001670
		private void WriteFatSection(Collection<ExceptionHandler> handlers)
		{
			base.WriteByte(65);
			int num = handlers.Count * 24 + 4;
			base.WriteByte((byte)(num & 255));
			base.WriteByte((byte)(num >> 8 & 255));
			base.WriteByte((byte)(num >> 16 & 255));
			this.WriteExceptionHandlers(handlers, new Action<int>(base.WriteInt32), new Action<int>(base.WriteInt32));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000034E0 File Offset: 0x000016E0
		private void WriteExceptionHandlers(Collection<ExceptionHandler> handlers, Action<int> write_entry, Action<int> write_length)
		{
			for (int i = 0; i < handlers.Count; i++)
			{
				ExceptionHandler exceptionHandler = handlers[i];
				write_entry((int)exceptionHandler.HandlerType);
				write_entry(exceptionHandler.TryStart.Offset);
				write_length(this.GetTargetOffset(exceptionHandler.TryEnd) - exceptionHandler.TryStart.Offset);
				write_entry(exceptionHandler.HandlerStart.Offset);
				write_length(this.GetTargetOffset(exceptionHandler.HandlerEnd) - exceptionHandler.HandlerStart.Offset);
				this.WriteExceptionHandlerSpecific(exceptionHandler);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000357C File Offset: 0x0000177C
		private void WriteExceptionHandlerSpecific(ExceptionHandler handler)
		{
			switch (handler.HandlerType)
			{
			case ExceptionHandlerType.Catch:
				this.WriteMetadataToken(this.metadata.LookupToken(handler.CatchType));
				return;
			case ExceptionHandlerType.Filter:
				base.WriteInt32(handler.FilterStart.Offset);
				return;
			default:
				base.WriteInt32(0);
				return;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000035D4 File Offset: 0x000017D4
		public MetadataToken GetStandAloneSignature(Collection<VariableDefinition> variables)
		{
			uint localVariableBlobIndex = this.metadata.GetLocalVariableBlobIndex(variables);
			return this.GetStandAloneSignatureToken(localVariableBlobIndex);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000035F8 File Offset: 0x000017F8
		public MetadataToken GetStandAloneSignature(CallSite call_site)
		{
			uint callSiteBlobIndex = this.metadata.GetCallSiteBlobIndex(call_site);
			MetadataToken standAloneSignatureToken = this.GetStandAloneSignatureToken(callSiteBlobIndex);
			call_site.MetadataToken = standAloneSignatureToken;
			return standAloneSignatureToken;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003624 File Offset: 0x00001824
		private MetadataToken GetStandAloneSignatureToken(uint signature)
		{
			MetadataToken metadataToken;
			if (this.standalone_signatures.TryGetValue(signature, out metadataToken))
			{
				return metadataToken;
			}
			metadataToken = new MetadataToken(TokenType.Signature, this.metadata.AddStandAloneSignature(signature));
			this.standalone_signatures.Add(signature, metadataToken);
			return metadataToken;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000366E File Offset: 0x0000186E
		private uint BeginMethod()
		{
			return this.current;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003676 File Offset: 0x00001876
		private void WriteMetadataToken(MetadataToken token)
		{
			base.WriteUInt32(token.ToUInt32());
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003685 File Offset: 0x00001885
		private void Align(int align)
		{
			align--;
			base.WriteBytes((this.position + align & ~align) - this.position);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000036A4 File Offset: 0x000018A4
		private void EndMethod()
		{
			this.current = (uint)((ulong)this.code_base + (ulong)((long)this.position));
		}

		// Token: 0x040000E0 RID: 224
		private readonly uint code_base;

		// Token: 0x040000E1 RID: 225
		internal readonly MetadataBuilder metadata;

		// Token: 0x040000E2 RID: 226
		private readonly Dictionary<uint, MetadataToken> standalone_signatures;

		// Token: 0x040000E3 RID: 227
		private uint current;

		// Token: 0x040000E4 RID: 228
		private MethodBody body;
	}
}
