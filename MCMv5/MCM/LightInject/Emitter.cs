using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;

namespace MCM.LightInject
{
	// Token: 0x0200011E RID: 286
	[ExcludeFromCodeCoverage]
	internal class Emitter : IEmitter
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x0001586F File Offset: 0x00013A6F
		public Emitter(ILGenerator generator, Type[] parameterTypes)
		{
			this.generator = generator;
			this.parameterTypes = parameterTypes;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x000158A8 File Offset: 0x00013AA8
		public Type StackType
		{
			get
			{
				return (this.stack.Count == 0) ? null : this.stack.Peek();
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000158C5 File Offset: 0x00013AC5
		public List<Instruction> Instructions
		{
			get
			{
				return this.instructions;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000158D0 File Offset: 0x00013AD0
		public void Emit(OpCode code)
		{
			bool flag = code == OpCodes.Ldarg_0;
			if (flag)
			{
				this.stack.Push(this.parameterTypes[0]);
			}
			else
			{
				bool flag2 = code == OpCodes.Ldarg_1;
				if (flag2)
				{
					this.stack.Push(this.parameterTypes[1]);
				}
				else
				{
					bool flag3 = code == OpCodes.Ldarg_2;
					if (flag3)
					{
						this.stack.Push(this.parameterTypes[2]);
					}
					else
					{
						bool flag4 = code == OpCodes.Ldarg_3;
						if (flag4)
						{
							this.stack.Push(this.parameterTypes[3]);
						}
						else
						{
							bool flag5 = code == OpCodes.Ldloc_0;
							if (flag5)
							{
								this.stack.Push(this.variables[0].LocalType);
							}
							else
							{
								bool flag6 = code == OpCodes.Ldloc_1;
								if (flag6)
								{
									this.stack.Push(this.variables[1].LocalType);
								}
								else
								{
									bool flag7 = code == OpCodes.Ldloc_2;
									if (flag7)
									{
										this.stack.Push(this.variables[2].LocalType);
									}
									else
									{
										bool flag8 = code == OpCodes.Ldloc_3;
										if (flag8)
										{
											this.stack.Push(this.variables[3].LocalType);
										}
										else
										{
											bool flag9 = code == OpCodes.Stloc_0;
											if (flag9)
											{
												this.stack.Pop();
											}
											else
											{
												bool flag10 = code == OpCodes.Stloc_1;
												if (flag10)
												{
													this.stack.Pop();
												}
												else
												{
													bool flag11 = code == OpCodes.Stloc_2;
													if (flag11)
													{
														this.stack.Pop();
													}
													else
													{
														bool flag12 = code == OpCodes.Stloc_3;
														if (flag12)
														{
															this.stack.Pop();
														}
														else
														{
															bool flag13 = code == OpCodes.Ldelem_Ref;
															if (flag13)
															{
																this.stack.Pop();
																Type arrayType = this.stack.Pop();
																this.stack.Push(arrayType.GetElementType());
															}
															else
															{
																bool flag14 = code == OpCodes.Ldlen;
																if (flag14)
																{
																	this.stack.Pop();
																	this.stack.Push(typeof(int));
																}
																else
																{
																	bool flag15 = code == OpCodes.Conv_I4;
																	if (flag15)
																	{
																		this.stack.Pop();
																		this.stack.Push(typeof(int));
																	}
																	else
																	{
																		bool flag16 = code == OpCodes.Ldc_I4_0;
																		if (flag16)
																		{
																			this.stack.Push(typeof(int));
																		}
																		else
																		{
																			bool flag17 = code == OpCodes.Ldc_I4_1;
																			if (flag17)
																			{
																				this.stack.Push(typeof(int));
																			}
																			else
																			{
																				bool flag18 = code == OpCodes.Ldc_I4_2;
																				if (flag18)
																				{
																					this.stack.Push(typeof(int));
																				}
																				else
																				{
																					bool flag19 = code == OpCodes.Ldc_I4_3;
																					if (flag19)
																					{
																						this.stack.Push(typeof(int));
																					}
																					else
																					{
																						bool flag20 = code == OpCodes.Ldc_I4_4;
																						if (flag20)
																						{
																							this.stack.Push(typeof(int));
																						}
																						else
																						{
																							bool flag21 = code == OpCodes.Ldc_I4_5;
																							if (flag21)
																							{
																								this.stack.Push(typeof(int));
																							}
																							else
																							{
																								bool flag22 = code == OpCodes.Ldc_I4_6;
																								if (flag22)
																								{
																									this.stack.Push(typeof(int));
																								}
																								else
																								{
																									bool flag23 = code == OpCodes.Ldc_I4_7;
																									if (flag23)
																									{
																										this.stack.Push(typeof(int));
																									}
																									else
																									{
																										bool flag24 = code == OpCodes.Ldc_I4_8;
																										if (flag24)
																										{
																											this.stack.Push(typeof(int));
																										}
																										else
																										{
																											bool flag25 = code == OpCodes.Sub;
																											if (flag25)
																											{
																												this.stack.Pop();
																												this.stack.Pop();
																												this.stack.Push(typeof(int));
																											}
																											else
																											{
																												bool flag26 = code == OpCodes.Ret;
																												if (!flag26)
																												{
																													bool flag27 = code == OpCodes.Ldnull;
																													if (!flag27)
																													{
																														throw new NotSupportedException(code.ToString());
																													}
																													this.stack.Push(null);
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			this.instructions.Add(new Instruction(code, delegate(ILGenerator il)
			{
				il.Emit(code);
			}));
			bool flag28 = code == OpCodes.Ret;
			if (flag28)
			{
				foreach (Instruction instruction in this.instructions)
				{
					instruction.Emit(this.generator);
				}
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00015EFC File Offset: 0x000140FC
		public void Emit(OpCode code, int arg)
		{
			bool flag = code == OpCodes.Ldc_I4;
			if (flag)
			{
				this.stack.Push(typeof(int));
			}
			else
			{
				bool flag2 = code == OpCodes.Ldarg;
				if (flag2)
				{
					this.stack.Push(this.parameterTypes[arg]);
				}
				else
				{
					bool flag3 = code == OpCodes.Ldloc;
					if (flag3)
					{
						this.stack.Push(this.variables[arg].LocalType);
					}
					else
					{
						bool flag4 = code == OpCodes.Ldloca;
						if (flag4)
						{
							this.stack.Push(this.variables[arg].LocalType.MakePointerType());
						}
						else
						{
							bool flag5 = code == OpCodes.Stloc;
							if (!flag5)
							{
								throw new NotSupportedException(code.ToString());
							}
							this.stack.Pop();
						}
					}
				}
			}
			this.instructions.Add(new Instruction<int>(code, arg, delegate(ILGenerator il)
			{
				il.Emit(code, arg);
			}));
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00016068 File Offset: 0x00014268
		public void Emit(OpCode code, long arg)
		{
			bool flag = code == OpCodes.Ldc_I8;
			if (flag)
			{
				this.stack.Push(typeof(long));
				this.instructions.Add(new Instruction<long>(code, arg, delegate(ILGenerator il)
				{
					il.Emit(code, arg);
				}));
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000160FC File Offset: 0x000142FC
		public void Emit(OpCode code, sbyte arg)
		{
			bool flag = code == OpCodes.Ldc_I4_S;
			if (flag)
			{
				this.stack.Push(typeof(int));
				this.instructions.Add(new Instruction<int>(code, (int)arg, delegate(ILGenerator il)
				{
					il.Emit(code, arg);
				}));
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00016190 File Offset: 0x00014390
		public void Emit(OpCode code, byte arg)
		{
			bool flag = code == OpCodes.Ldloc_S;
			if (flag)
			{
				this.stack.Push(this.variables[(int)arg].LocalType);
			}
			else
			{
				bool flag2 = code == OpCodes.Ldarg_S;
				if (flag2)
				{
					this.stack.Push(this.parameterTypes[(int)arg]);
				}
				else
				{
					bool flag3 = code == OpCodes.Stloc_S;
					if (!flag3)
					{
						throw new NotSupportedException(code.ToString());
					}
					this.stack.Pop();
				}
			}
			this.instructions.Add(new Instruction<byte>(code, arg, delegate(ILGenerator il)
			{
				il.Emit(code, arg);
			}));
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00016284 File Offset: 0x00014484
		public void Emit(OpCode code, Type type)
		{
			bool flag = code == OpCodes.Newarr;
			if (flag)
			{
				this.stack.Pop();
				this.stack.Push(type.MakeArrayType());
			}
			else
			{
				bool flag2 = code == OpCodes.Stelem;
				if (flag2)
				{
					this.stack.Pop();
					this.stack.Pop();
					this.stack.Pop();
				}
				else
				{
					bool flag3 = code == OpCodes.Castclass;
					if (flag3)
					{
						this.stack.Pop();
						this.stack.Push(type);
					}
					else
					{
						bool flag4 = code == OpCodes.Box;
						if (flag4)
						{
							this.stack.Pop();
							this.stack.Push(typeof(object));
						}
						else
						{
							bool flag5 = code == OpCodes.Unbox_Any;
							if (flag5)
							{
								this.stack.Pop();
								this.stack.Push(type);
							}
							else
							{
								bool flag6 = code == OpCodes.Initobj;
								if (!flag6)
								{
									throw new NotSupportedException(code.ToString());
								}
								this.stack.Pop();
							}
						}
					}
				}
			}
			this.instructions.Add(new Instruction<Type>(code, type, delegate(ILGenerator il)
			{
				il.Emit(code, type);
			}));
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00016438 File Offset: 0x00014638
		public void Emit(OpCode code, ConstructorInfo constructor)
		{
			bool flag = code == OpCodes.Newobj;
			if (flag)
			{
				int parameterCount = constructor.GetParameters().Length;
				for (int i = 0; i < parameterCount; i++)
				{
					this.stack.Pop();
				}
				this.stack.Push(constructor.DeclaringType);
				this.instructions.Add(new Instruction<ConstructorInfo>(code, constructor, delegate(ILGenerator il)
				{
					il.Emit(code, constructor);
				}));
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000164F8 File Offset: 0x000146F8
		public void Emit(OpCode code, LocalBuilder localBuilder)
		{
			bool flag = code == OpCodes.Stloc;
			if (flag)
			{
				this.stack.Pop();
			}
			else
			{
				bool flag2 = code == OpCodes.Ldloc;
				if (!flag2)
				{
					throw new NotSupportedException(code.ToString());
				}
				this.stack.Push(localBuilder.LocalType);
			}
			this.instructions.Add(new Instruction<LocalBuilder>(code, localBuilder, delegate(ILGenerator il)
			{
				il.Emit(code, localBuilder);
			}));
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000165B0 File Offset: 0x000147B0
		public void Emit(OpCode code, MethodInfo methodInfo)
		{
			bool flag = code == OpCodes.Callvirt || code == OpCodes.Call;
			if (flag)
			{
				int parameterCount = methodInfo.GetParameters().Length;
				for (int i = 0; i < parameterCount; i++)
				{
					this.stack.Pop();
				}
				bool flag2 = !methodInfo.IsStatic;
				if (flag2)
				{
					this.stack.Pop();
				}
				bool flag3 = methodInfo.ReturnType != typeof(void);
				if (flag3)
				{
					this.stack.Push(methodInfo.ReturnType);
				}
				this.instructions.Add(new Instruction<MethodInfo>(code, methodInfo, delegate(ILGenerator il)
				{
					il.Emit(code, methodInfo);
				}));
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000166CC File Offset: 0x000148CC
		public LocalBuilder DeclareLocal(Type type)
		{
			LocalBuilder localBuilder = this.generator.DeclareLocal(type);
			this.variables.Add(localBuilder);
			return localBuilder;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000166FC File Offset: 0x000148FC
		public void Emit(OpCode code, string arg)
		{
			bool flag = code == OpCodes.Ldstr;
			if (flag)
			{
				this.stack.Push(typeof(string));
				this.instructions.Add(new Instruction<string>(code, arg, delegate(ILGenerator il)
				{
					il.Emit(code, arg);
				}));
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00016790 File Offset: 0x00014990
		public void PushConstantValue(object arg, Type type)
		{
			this.stack.Push(type);
			this.instructions.Add(new Instruction<object>(OpCodes.Nop, arg, delegate(ILGenerator il)
			{
				il.PushConstantValue(arg, type);
			}));
		}

		// Token: 0x0400020B RID: 523
		private readonly ILGenerator generator;

		// Token: 0x0400020C RID: 524
		private readonly Type[] parameterTypes;

		// Token: 0x0400020D RID: 525
		private readonly Stack<Type> stack = new Stack<Type>();

		// Token: 0x0400020E RID: 526
		private readonly List<LocalBuilder> variables = new List<LocalBuilder>();

		// Token: 0x0400020F RID: 527
		private readonly List<Instruction> instructions = new List<Instruction>();
	}
}
