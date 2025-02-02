using System;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000005 RID: 5
	internal sealed class CodeReader : ByteBuffer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000036BC File Offset: 0x000018BC
		private int Offset
		{
			get
			{
				return this.position - this.start;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000036CB File Offset: 0x000018CB
		public CodeReader(Section section, MetadataReader reader) : base(section.Data)
		{
			this.code_section = section;
			this.reader = reader;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000036E7 File Offset: 0x000018E7
		public MethodBody ReadMethodBody(MethodDefinition method)
		{
			this.method = method;
			this.body = new MethodBody(method);
			this.reader.context = method;
			this.ReadMethodBody();
			return this.body;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003714 File Offset: 0x00001914
		public void MoveTo(int rva)
		{
			if (!this.IsInSection(rva))
			{
				this.code_section = this.reader.image.GetSectionAtVirtualAddress((uint)rva);
				base.Reset(this.code_section.Data);
			}
			this.position = rva - (int)this.code_section.VirtualAddress;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003765 File Offset: 0x00001965
		private bool IsInSection(int rva)
		{
			return (ulong)this.code_section.VirtualAddress <= (ulong)((long)rva) && (long)rva < (long)((ulong)(this.code_section.VirtualAddress + this.code_section.SizeOfRawData));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000037AC File Offset: 0x000019AC
		private void ReadMethodBody()
		{
			this.MoveTo(this.method.RVA);
			byte b = base.ReadByte();
			switch (b & 3)
			{
			case 2:
				this.body.code_size = b >> 2;
				this.body.MaxStackSize = 8;
				this.ReadCode();
				break;
			case 3:
				this.position--;
				this.ReadFatMethod();
				break;
			default:
				throw new InvalidOperationException();
			}
			ISymbolReader symbol_reader = this.reader.module.symbol_reader;
			if (symbol_reader != null)
			{
				Collection<Instruction> instructions = this.body.Instructions;
				symbol_reader.Read(this.body, (int offset) => CodeReader.GetInstruction(instructions, offset));
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003868 File Offset: 0x00001A68
		private void ReadFatMethod()
		{
			ushort num = base.ReadUInt16();
			this.body.max_stack_size = (int)base.ReadUInt16();
			this.body.code_size = (int)base.ReadUInt32();
			this.body.local_var_token = new MetadataToken(base.ReadUInt32());
			this.body.init_locals = ((num & 16) != 0);
			if (this.body.local_var_token.RID != 0U)
			{
				this.body.variables = this.ReadVariables(this.body.local_var_token);
			}
			this.ReadCode();
			if ((num & 8) != 0)
			{
				this.ReadSection();
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003908 File Offset: 0x00001B08
		public VariableDefinitionCollection ReadVariables(MetadataToken local_var_token)
		{
			int position = this.reader.position;
			VariableDefinitionCollection result = this.reader.ReadVariables(local_var_token);
			this.reader.position = position;
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000393C File Offset: 0x00001B3C
		private void ReadCode()
		{
			this.start = this.position;
			int num = this.body.code_size;
			if (num < 0 || (long)this.buffer.Length <= (long)((ulong)(num + this.position)))
			{
				num = 0;
			}
			int num2 = this.start + num;
			Collection<Instruction> collection = this.body.instructions = new InstructionCollection((num + 1) / 2);
			while (this.position < num2)
			{
				int offset = this.position - this.start;
				OpCode opCode = this.ReadOpCode();
				Instruction instruction = new Instruction(offset, opCode);
				if (opCode.OperandType != OperandType.InlineNone)
				{
					instruction.operand = this.ReadOperand(instruction);
				}
				collection.Add(instruction);
			}
			this.ResolveBranches(collection);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000039F4 File Offset: 0x00001BF4
		private OpCode ReadOpCode()
		{
			byte b = base.ReadByte();
			if (b == 254)
			{
				return OpCodes.TwoBytesOpCode[(int)base.ReadByte()];
			}
			return OpCodes.OneByteOpCode[(int)b];
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003A38 File Offset: 0x00001C38
		private object ReadOperand(Instruction instruction)
		{
			switch (instruction.opcode.OperandType)
			{
			case OperandType.InlineBrTarget:
				return base.ReadInt32() + this.Offset;
			case OperandType.InlineField:
			case OperandType.InlineMethod:
			case OperandType.InlineTok:
			case OperandType.InlineType:
				return this.reader.LookupToken(this.ReadToken());
			case OperandType.InlineI:
				return base.ReadInt32();
			case OperandType.InlineI8:
				return base.ReadInt64();
			case OperandType.InlineR:
				return base.ReadDouble();
			case OperandType.InlineSig:
				return this.GetCallSite(this.ReadToken());
			case OperandType.InlineString:
				return this.GetString(this.ReadToken());
			case OperandType.InlineSwitch:
			{
				int num = base.ReadInt32();
				int num2 = this.Offset + 4 * num;
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = num2 + base.ReadInt32();
				}
				return array;
			}
			case OperandType.InlineVar:
				return this.GetVariable((int)base.ReadUInt16());
			case OperandType.InlineArg:
				return this.GetParameter((int)base.ReadUInt16());
			case OperandType.ShortInlineBrTarget:
				return (int)base.ReadSByte() + this.Offset;
			case OperandType.ShortInlineI:
				if (instruction.opcode == OpCodes.Ldc_I4_S)
				{
					return base.ReadSByte();
				}
				return base.ReadByte();
			case OperandType.ShortInlineR:
				return base.ReadSingle();
			case OperandType.ShortInlineVar:
				return this.GetVariable((int)base.ReadByte());
			case OperandType.ShortInlineArg:
				return this.GetParameter((int)base.ReadByte());
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003BC5 File Offset: 0x00001DC5
		public string GetString(MetadataToken token)
		{
			return this.reader.image.UserStringHeap.Read(token.RID);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003BE3 File Offset: 0x00001DE3
		public ParameterDefinition GetParameter(int index)
		{
			return this.body.GetParameter(index);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public VariableDefinition GetVariable(int index)
		{
			return this.body.GetVariable(index);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003BFF File Offset: 0x00001DFF
		public CallSite GetCallSite(MetadataToken token)
		{
			return this.reader.ReadCallSite(token);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003C10 File Offset: 0x00001E10
		private void ResolveBranches(Collection<Instruction> instructions)
		{
			Instruction[] items = instructions.items;
			int size = instructions.size;
			int i = 0;
			while (i < size)
			{
				Instruction instruction = items[i];
				OperandType operandType = instruction.opcode.OperandType;
				if (operandType == OperandType.InlineBrTarget)
				{
					goto IL_37;
				}
				if (operandType != OperandType.InlineSwitch)
				{
					if (operandType == OperandType.ShortInlineBrTarget)
					{
						goto IL_37;
					}
				}
				else
				{
					int[] array = (int[])instruction.operand;
					Instruction[] array2 = new Instruction[array.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array2[j] = this.GetInstruction(array[j]);
					}
					instruction.operand = array2;
				}
				IL_93:
				i++;
				continue;
				IL_37:
				instruction.operand = this.GetInstruction((int)instruction.operand);
				goto IL_93;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003CBB File Offset: 0x00001EBB
		private Instruction GetInstruction(int offset)
		{
			return CodeReader.GetInstruction(this.body.Instructions, offset);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003CD0 File Offset: 0x00001ED0
		private static Instruction GetInstruction(Collection<Instruction> instructions, int offset)
		{
			int size = instructions.size;
			Instruction[] items = instructions.items;
			if (offset < 0 || offset > items[size - 1].offset)
			{
				return null;
			}
			int i = 0;
			int num = size - 1;
			while (i <= num)
			{
				int num2 = i + (num - i) / 2;
				Instruction instruction = items[num2];
				int offset2 = instruction.offset;
				if (offset == offset2)
				{
					return instruction;
				}
				if (offset < offset2)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003D3C File Offset: 0x00001F3C
		private void ReadSection()
		{
			this.Align(4);
			byte b = base.ReadByte();
			if ((b & 64) == 0)
			{
				this.ReadSmallSection();
			}
			else
			{
				this.ReadFatSection();
			}
			if ((b & 128) != 0)
			{
				this.ReadSection();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003D8C File Offset: 0x00001F8C
		private void ReadSmallSection()
		{
			int count = (int)(base.ReadByte() / 12);
			base.Advance(2);
			this.ReadExceptionHandlers(count, () => (int)base.ReadUInt16(), () => (int)base.ReadByte());
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003DCC File Offset: 0x00001FCC
		private void ReadFatSection()
		{
			this.position--;
			int count = (base.ReadInt32() >> 8) / 24;
			this.ReadExceptionHandlers(count, new Func<int>(base.ReadInt32), new Func<int>(base.ReadInt32));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003E14 File Offset: 0x00002014
		private void ReadExceptionHandlers(int count, Func<int> read_entry, Func<int> read_length)
		{
			for (int i = 0; i < count; i++)
			{
				ExceptionHandler exceptionHandler = new ExceptionHandler((ExceptionHandlerType)(read_entry() & 7));
				exceptionHandler.TryStart = this.GetInstruction(read_entry());
				exceptionHandler.TryEnd = this.GetInstruction(exceptionHandler.TryStart.Offset + read_length());
				exceptionHandler.HandlerStart = this.GetInstruction(read_entry());
				exceptionHandler.HandlerEnd = this.GetInstruction(exceptionHandler.HandlerStart.Offset + read_length());
				this.ReadExceptionHandlerSpecific(exceptionHandler);
				this.body.ExceptionHandlers.Add(exceptionHandler);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003EBC File Offset: 0x000020BC
		private void ReadExceptionHandlerSpecific(ExceptionHandler handler)
		{
			switch (handler.HandlerType)
			{
			case ExceptionHandlerType.Catch:
				handler.CatchType = (TypeReference)this.reader.LookupToken(this.ReadToken());
				return;
			case ExceptionHandlerType.Filter:
				handler.FilterStart = this.GetInstruction(base.ReadInt32());
				return;
			default:
				base.Advance(4);
				return;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003F17 File Offset: 0x00002117
		private void Align(int align)
		{
			align--;
			base.Advance((this.position + align & ~align) - this.position);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003F36 File Offset: 0x00002136
		public MetadataToken ReadToken()
		{
			return new MetadataToken(base.ReadUInt32());
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003F44 File Offset: 0x00002144
		public ByteBuffer PatchRawMethodBody(MethodDefinition method, CodeWriter writer, out MethodSymbols symbols)
		{
			ByteBuffer byteBuffer = new ByteBuffer();
			symbols = new MethodSymbols(method.Name);
			this.method = method;
			this.reader.context = method;
			this.MoveTo(method.RVA);
			byte b = base.ReadByte();
			MetadataToken zero;
			switch (b & 3)
			{
			case 2:
				byteBuffer.WriteByte(b);
				zero = MetadataToken.Zero;
				symbols.code_size = b >> 2;
				this.PatchRawCode(byteBuffer, symbols.code_size, writer);
				break;
			case 3:
				this.position--;
				this.PatchRawFatMethod(byteBuffer, symbols, writer, out zero);
				break;
			default:
				throw new NotSupportedException();
			}
			ISymbolReader symbol_reader = this.reader.module.symbol_reader;
			if (symbol_reader != null && writer.metadata.write_symbols)
			{
				symbols.method_token = CodeReader.GetOriginalToken(writer.metadata, method);
				symbols.local_var_token = zero;
				symbol_reader.Read(symbols);
			}
			return byteBuffer;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004034 File Offset: 0x00002234
		private void PatchRawFatMethod(ByteBuffer buffer, MethodSymbols symbols, CodeWriter writer, out MetadataToken local_var_token)
		{
			ushort num = base.ReadUInt16();
			buffer.WriteUInt16(num);
			buffer.WriteUInt16(base.ReadUInt16());
			symbols.code_size = base.ReadInt32();
			buffer.WriteInt32(symbols.code_size);
			local_var_token = this.ReadToken();
			if (local_var_token.RID > 0U)
			{
				buffer.WriteUInt32(((symbols.variables = this.ReadVariables(local_var_token)) != null) ? writer.GetStandAloneSignature(symbols.variables).ToUInt32() : 0U);
			}
			else
			{
				buffer.WriteUInt32(0U);
			}
			this.PatchRawCode(buffer, symbols.code_size, writer);
			if ((num & 8) != 0)
			{
				this.PatchRawSection(buffer, writer.metadata);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000040EC File Offset: 0x000022EC
		private static MetadataToken GetOriginalToken(MetadataBuilder metadata, MethodDefinition method)
		{
			MetadataToken result;
			if (metadata.TryGetOriginalMethodToken(method.token, out result))
			{
				return result;
			}
			return MetadataToken.Zero;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004110 File Offset: 0x00002310
		private void PatchRawCode(ByteBuffer buffer, int code_size, CodeWriter writer)
		{
			MetadataBuilder metadata = writer.metadata;
			buffer.WriteBytes(base.ReadBytes(code_size));
			int position = buffer.position;
			buffer.position -= code_size;
			while (buffer.position < position)
			{
				byte b = buffer.ReadByte();
				OpCode opCode;
				if (b != 254)
				{
					opCode = OpCodes.OneByteOpCode[(int)b];
				}
				else
				{
					byte b2 = buffer.ReadByte();
					opCode = OpCodes.TwoBytesOpCode[(int)b2];
				}
				switch (opCode.OperandType)
				{
				case OperandType.InlineBrTarget:
				case OperandType.InlineI:
				case OperandType.ShortInlineR:
					buffer.position += 4;
					break;
				case OperandType.InlineField:
				case OperandType.InlineMethod:
				case OperandType.InlineTok:
				case OperandType.InlineType:
				{
					IMetadataTokenProvider provider = this.reader.LookupToken(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(metadata.LookupToken(provider).ToUInt32());
					break;
				}
				case OperandType.InlineI8:
				case OperandType.InlineR:
					buffer.position += 8;
					break;
				case OperandType.InlineSig:
				{
					CallSite callSite = this.GetCallSite(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(writer.GetStandAloneSignature(callSite).ToUInt32());
					break;
				}
				case OperandType.InlineString:
				{
					string @string = this.GetString(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(new MetadataToken(TokenType.String, metadata.user_string_heap.GetStringIndex(@string)).ToUInt32());
					break;
				}
				case OperandType.InlineSwitch:
				{
					int num = buffer.ReadInt32();
					buffer.position += num * 4;
					break;
				}
				case OperandType.InlineVar:
				case OperandType.InlineArg:
					buffer.position += 2;
					break;
				case OperandType.ShortInlineBrTarget:
				case OperandType.ShortInlineI:
				case OperandType.ShortInlineVar:
				case OperandType.ShortInlineArg:
					buffer.position++;
					break;
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004324 File Offset: 0x00002524
		private void PatchRawSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			int position = this.position;
			this.Align(4);
			buffer.WriteBytes(this.position - position);
			byte b = base.ReadByte();
			if ((b & 64) == 0)
			{
				buffer.WriteByte(b);
				this.PatchRawSmallSection(buffer, metadata);
			}
			else
			{
				this.PatchRawFatSection(buffer, metadata);
			}
			if ((b & 128) != 0)
			{
				this.PatchRawSection(buffer, metadata);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004384 File Offset: 0x00002584
		private void PatchRawSmallSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			byte b = base.ReadByte();
			buffer.WriteByte(b);
			base.Advance(2);
			buffer.WriteUInt16(0);
			int count = (int)(b / 12);
			this.PatchRawExceptionHandlers(buffer, metadata, count, false);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000043BC File Offset: 0x000025BC
		private void PatchRawFatSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			this.position--;
			int num = base.ReadInt32();
			buffer.WriteInt32(num);
			int count = (num >> 8) / 24;
			this.PatchRawExceptionHandlers(buffer, metadata, count, true);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000043F8 File Offset: 0x000025F8
		private void PatchRawExceptionHandlers(ByteBuffer buffer, MetadataBuilder metadata, int count, bool fat_entry)
		{
			for (int i = 0; i < count; i++)
			{
				ExceptionHandlerType exceptionHandlerType;
				if (fat_entry)
				{
					uint num = base.ReadUInt32();
					exceptionHandlerType = (ExceptionHandlerType)(num & 7U);
					buffer.WriteUInt32(num);
				}
				else
				{
					ushort num2 = base.ReadUInt16();
					exceptionHandlerType = (ExceptionHandlerType)(num2 & 7);
					buffer.WriteUInt16(num2);
				}
				buffer.WriteBytes(base.ReadBytes(fat_entry ? 16 : 6));
				ExceptionHandlerType exceptionHandlerType2 = exceptionHandlerType;
				if (exceptionHandlerType2 == ExceptionHandlerType.Catch)
				{
					IMetadataTokenProvider provider = this.reader.LookupToken(this.ReadToken());
					buffer.WriteUInt32(metadata.LookupToken(provider).ToUInt32());
				}
				else
				{
					buffer.WriteUInt32(base.ReadUInt32());
				}
			}
		}

		// Token: 0x040000E6 RID: 230
		internal readonly MetadataReader reader;

		// Token: 0x040000E7 RID: 231
		private int start;

		// Token: 0x040000E8 RID: 232
		private Section code_section;

		// Token: 0x040000E9 RID: 233
		private MethodDefinition method;

		// Token: 0x040000EA RID: 234
		private MethodBody body;
	}
}
