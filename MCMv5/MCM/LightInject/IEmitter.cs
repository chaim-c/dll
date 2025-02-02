using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MCM.LightInject
{
	// Token: 0x020000DF RID: 223
	internal interface IEmitter
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600049E RID: 1182
		Type StackType { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600049F RID: 1183
		List<Instruction> Instructions { get; }

		// Token: 0x060004A0 RID: 1184
		void Emit(OpCode code);

		// Token: 0x060004A1 RID: 1185
		void Emit(OpCode code, string arg);

		// Token: 0x060004A2 RID: 1186
		void PushConstantValue(object arg, Type type);

		// Token: 0x060004A3 RID: 1187
		void Emit(OpCode code, int arg);

		// Token: 0x060004A4 RID: 1188
		void Emit(OpCode code, long arg);

		// Token: 0x060004A5 RID: 1189
		void Emit(OpCode code, sbyte arg);

		// Token: 0x060004A6 RID: 1190
		void Emit(OpCode code, byte arg);

		// Token: 0x060004A7 RID: 1191
		void Emit(OpCode code, Type type);

		// Token: 0x060004A8 RID: 1192
		void Emit(OpCode code, ConstructorInfo constructor);

		// Token: 0x060004A9 RID: 1193
		void Emit(OpCode code, LocalBuilder localBuilder);

		// Token: 0x060004AA RID: 1194
		void Emit(OpCode code, MethodInfo methodInfo);

		// Token: 0x060004AB RID: 1195
		LocalBuilder DeclareLocal(Type type);
	}
}
