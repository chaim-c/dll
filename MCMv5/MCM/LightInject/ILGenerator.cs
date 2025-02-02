using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace MCM.LightInject
{
	// Token: 0x020000F5 RID: 245
	[ExcludeFromCodeCoverage]
	internal class ILGenerator
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x00012410 File Offset: 0x00010610
		public ILGenerator(ParameterExpression[] parameters)
		{
			this.parameters = parameters;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00012444 File Offset: 0x00010644
		public Expression CurrentExpression
		{
			get
			{
				List<ParameterExpression> variables = (from l in this.locals
				select l.Variable).ToList<ParameterExpression>();
				List<Expression> ex = new List<Expression>(this.expressions)
				{
					this.stack.Peek()
				};
				return Expression.Block(variables, ex);
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000124AC File Offset: 0x000106AC
		public void Emit(OpCode code, ConstructorInfo constructor)
		{
			bool flag = code == OpCodes.Newobj;
			if (flag)
			{
				int parameterCount = constructor.GetParameters().Length;
				NewExpression expression = Expression.New(constructor, this.Pop(parameterCount));
				this.stack.Push(expression);
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00012508 File Offset: 0x00010708
		public void Emit(OpCode code)
		{
			bool flag = code == OpCodes.Ldarg_0;
			if (flag)
			{
				this.stack.Push(this.parameters[0]);
			}
			else
			{
				bool flag2 = code == OpCodes.Ldarg_1;
				if (flag2)
				{
					this.stack.Push(this.parameters[1]);
				}
				else
				{
					bool flag3 = code == OpCodes.Ldarg_2;
					if (flag3)
					{
						this.stack.Push(this.parameters[2]);
					}
					else
					{
						bool flag4 = code == OpCodes.Ldarg_3;
						if (flag4)
						{
							this.stack.Push(this.parameters[3]);
						}
						else
						{
							bool flag5 = code == OpCodes.Ldloc_0;
							if (flag5)
							{
								this.stack.Push(this.locals[0].Variable);
							}
							else
							{
								bool flag6 = code == OpCodes.Ldloc_1;
								if (flag6)
								{
									this.stack.Push(this.locals[1].Variable);
								}
								else
								{
									bool flag7 = code == OpCodes.Ldloc_2;
									if (flag7)
									{
										this.stack.Push(this.locals[2].Variable);
									}
									else
									{
										bool flag8 = code == OpCodes.Ldloc_3;
										if (flag8)
										{
											this.stack.Push(this.locals[3].Variable);
										}
										else
										{
											bool flag9 = code == OpCodes.Stloc_0;
											if (flag9)
											{
												Expression valueExpression = this.stack.Pop();
												BinaryExpression assignExpression = Expression.Assign(this.locals[0].Variable, valueExpression);
												this.expressions.Add(assignExpression);
											}
											else
											{
												bool flag10 = code == OpCodes.Stloc_1;
												if (flag10)
												{
													Expression valueExpression2 = this.stack.Pop();
													BinaryExpression assignExpression2 = Expression.Assign(this.locals[1].Variable, valueExpression2);
													this.expressions.Add(assignExpression2);
												}
												else
												{
													bool flag11 = code == OpCodes.Stloc_2;
													if (flag11)
													{
														Expression valueExpression3 = this.stack.Pop();
														BinaryExpression assignExpression3 = Expression.Assign(this.locals[2].Variable, valueExpression3);
														this.expressions.Add(assignExpression3);
													}
													else
													{
														bool flag12 = code == OpCodes.Stloc_3;
														if (flag12)
														{
															Expression valueExpression4 = this.stack.Pop();
															BinaryExpression assignExpression4 = Expression.Assign(this.locals[3].Variable, valueExpression4);
															this.expressions.Add(assignExpression4);
														}
														else
														{
															bool flag13 = code == OpCodes.Ldelem_Ref;
															if (flag13)
															{
																Expression[] indexes = new Expression[]
																{
																	this.stack.Pop()
																};
																for (int i = 0; i < indexes.Length; i++)
																{
																	indexes[0] = Expression.Convert(indexes[i], typeof(int));
																}
																Expression array = this.stack.Pop();
																this.stack.Push(Expression.ArrayAccess(array, indexes));
															}
															else
															{
																bool flag14 = code == OpCodes.Ldlen;
																if (flag14)
																{
																	Expression array2 = this.stack.Pop();
																	this.stack.Push(Expression.ArrayLength(array2));
																}
																else
																{
																	bool flag15 = code == OpCodes.Conv_I4;
																	if (flag15)
																	{
																		this.stack.Push(Expression.Convert(this.stack.Pop(), typeof(int)));
																	}
																	else
																	{
																		bool flag16 = code == OpCodes.Ldc_I4_0;
																		if (flag16)
																		{
																			this.stack.Push(Expression.Constant(0, typeof(int)));
																		}
																		else
																		{
																			bool flag17 = code == OpCodes.Ldc_I4_1;
																			if (flag17)
																			{
																				this.stack.Push(Expression.Constant(1, typeof(int)));
																			}
																			else
																			{
																				bool flag18 = code == OpCodes.Ldc_I4_2;
																				if (flag18)
																				{
																					this.stack.Push(Expression.Constant(2, typeof(int)));
																				}
																				else
																				{
																					bool flag19 = code == OpCodes.Ldc_I4_3;
																					if (flag19)
																					{
																						this.stack.Push(Expression.Constant(3, typeof(int)));
																					}
																					else
																					{
																						bool flag20 = code == OpCodes.Ldc_I4_4;
																						if (flag20)
																						{
																							this.stack.Push(Expression.Constant(4, typeof(int)));
																						}
																						else
																						{
																							bool flag21 = code == OpCodes.Ldc_I4_5;
																							if (flag21)
																							{
																								this.stack.Push(Expression.Constant(5, typeof(int)));
																							}
																							else
																							{
																								bool flag22 = code == OpCodes.Ldc_I4_6;
																								if (flag22)
																								{
																									this.stack.Push(Expression.Constant(6, typeof(int)));
																								}
																								else
																								{
																									bool flag23 = code == OpCodes.Ldc_I4_7;
																									if (flag23)
																									{
																										this.stack.Push(Expression.Constant(7, typeof(int)));
																									}
																									else
																									{
																										bool flag24 = code == OpCodes.Ldc_I4_8;
																										if (flag24)
																										{
																											this.stack.Push(Expression.Constant(8, typeof(int)));
																										}
																										else
																										{
																											bool flag25 = code == OpCodes.Sub;
																											if (flag25)
																											{
																												Expression right = this.stack.Pop();
																												Expression left = this.stack.Pop();
																												this.stack.Push(Expression.Subtract(left, right));
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
																													this.stack.Push(Expression.Constant(null));
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
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00012B54 File Offset: 0x00010D54
		public void Emit(OpCode code, LocalBuilder localBuilder)
		{
			bool flag = code == OpCodes.Stloc;
			if (flag)
			{
				Expression valueExpression = this.stack.Pop();
				BinaryExpression assignExpression = Expression.Assign(localBuilder.Variable, valueExpression);
				this.expressions.Add(assignExpression);
			}
			else
			{
				bool flag2 = code == OpCodes.Ldloc;
				if (!flag2)
				{
					throw new NotSupportedException(code.ToString());
				}
				this.stack.Push(localBuilder.Variable);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00012BD4 File Offset: 0x00010DD4
		public void Emit(OpCode code, int arg)
		{
			bool flag = code == OpCodes.Ldc_I4;
			if (flag)
			{
				this.stack.Push(Expression.Constant(arg, typeof(int)));
			}
			else
			{
				bool flag2 = code == OpCodes.Ldarg;
				if (flag2)
				{
					this.stack.Push(this.parameters[arg]);
				}
				else
				{
					bool flag3 = code == OpCodes.Ldloc;
					if (flag3)
					{
						this.stack.Push(this.locals[arg].Variable);
					}
					else
					{
						bool flag4 = code == OpCodes.Stloc;
						if (!flag4)
						{
							throw new NotSupportedException(code.ToString());
						}
						Expression valueExpression = this.stack.Pop();
						BinaryExpression assignExpression = Expression.Assign(this.locals[arg].Variable, valueExpression);
						this.expressions.Add(assignExpression);
					}
				}
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00012CD0 File Offset: 0x00010ED0
		public void Emit(OpCode code, long arg)
		{
			bool flag = code == OpCodes.Ldc_I8;
			if (flag)
			{
				this.stack.Push(Expression.Constant(arg, typeof(long)));
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00012D26 File Offset: 0x00010F26
		public void PushConstantValue(object arg, Type type)
		{
			this.stack.Push(Expression.Constant(arg, type));
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00012D3C File Offset: 0x00010F3C
		public void Emit(OpCode code, sbyte arg)
		{
			bool flag = code == OpCodes.Ldc_I4_S;
			if (flag)
			{
				this.stack.Push(Expression.Constant((int)arg, typeof(int)));
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00012D94 File Offset: 0x00010F94
		public void Emit(OpCode code, byte arg)
		{
			bool flag = code == OpCodes.Ldloc_S;
			if (flag)
			{
				this.stack.Push(this.locals[(int)arg].Variable);
			}
			else
			{
				bool flag2 = code == OpCodes.Ldarg_S;
				if (flag2)
				{
					this.stack.Push(this.parameters[(int)arg]);
				}
				else
				{
					bool flag3 = code == OpCodes.Stloc_S;
					if (!flag3)
					{
						throw new NotSupportedException(code.ToString());
					}
					Expression valueExpression = this.stack.Pop();
					BinaryExpression assignExpression = Expression.Assign(this.locals[(int)arg].Variable, valueExpression);
					this.expressions.Add(assignExpression);
				}
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00012E58 File Offset: 0x00011058
		public void Emit(OpCode code, string arg)
		{
			bool flag = code == OpCodes.Ldstr;
			if (flag)
			{
				this.stack.Push(Expression.Constant(arg, typeof(string)));
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00012EAC File Offset: 0x000110AC
		public LocalBuilder DeclareLocal(Type type)
		{
			LocalBuilder localBuilder = new LocalBuilder(type, this.locals.Count);
			this.locals.Add(localBuilder);
			return localBuilder;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00012EE0 File Offset: 0x000110E0
		public void Emit(OpCode code, Type type)
		{
			bool flag = code == OpCodes.Newarr;
			if (flag)
			{
				this.stack.Push(Expression.NewArrayBounds(type, this.Pop(1)));
			}
			else
			{
				bool flag2 = code == OpCodes.Stelem;
				if (flag2)
				{
					Expression value = this.stack.Pop();
					Expression index = this.stack.Pop();
					Expression array = this.stack.Pop();
					IndexExpression arrayAccess = Expression.ArrayAccess(array, new Expression[]
					{
						index
					});
					BinaryExpression assignExpression = Expression.Assign(arrayAccess, value);
					this.expressions.Add(assignExpression);
				}
				else
				{
					bool flag3 = code == OpCodes.Castclass;
					if (flag3)
					{
						this.stack.Push(Expression.Convert(this.stack.Pop(), type));
					}
					else
					{
						bool flag4 = code == OpCodes.Box;
						if (flag4)
						{
							this.stack.Push(Expression.Convert(this.stack.Pop(), typeof(object)));
						}
						else
						{
							bool flag5 = code == OpCodes.Unbox_Any;
							if (!flag5)
							{
								throw new NotSupportedException(code.ToString());
							}
							this.stack.Push(Expression.Convert(this.stack.Pop(), type));
						}
					}
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001303C File Offset: 0x0001123C
		public void Emit(OpCode code, MethodInfo methodInfo)
		{
			bool flag = code == OpCodes.Callvirt || code == OpCodes.Call;
			if (flag)
			{
				int parameterCount = methodInfo.GetParameters().Length;
				Expression[] arguments = this.Pop(parameterCount);
				bool flag2 = !methodInfo.IsStatic;
				MethodCallExpression methodCallExpression;
				if (flag2)
				{
					Expression instance = this.stack.Pop();
					methodCallExpression = Expression.Call(instance, methodInfo, arguments);
				}
				else
				{
					methodCallExpression = Expression.Call(null, methodInfo, arguments);
				}
				bool flag3 = methodInfo.ReturnType == typeof(void);
				if (flag3)
				{
					this.expressions.Add(methodCallExpression);
				}
				else
				{
					this.stack.Push(methodCallExpression);
				}
				return;
			}
			throw new NotSupportedException(code.ToString());
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00013108 File Offset: 0x00011308
		private Expression[] Pop(int numberOfElements)
		{
			Expression[] expressionsToPop = new Expression[numberOfElements];
			for (int i = 0; i < numberOfElements; i++)
			{
				expressionsToPop[i] = this.stack.Pop();
			}
			return expressionsToPop.Reverse<Expression>().ToArray<Expression>();
		}

		// Token: 0x040001A7 RID: 423
		private readonly ParameterExpression[] parameters;

		// Token: 0x040001A8 RID: 424
		private readonly Stack<Expression> stack = new Stack<Expression>();

		// Token: 0x040001A9 RID: 425
		private readonly List<LocalBuilder> locals = new List<LocalBuilder>();

		// Token: 0x040001AA RID: 426
		private readonly List<Expression> expressions = new List<Expression>();
	}
}
