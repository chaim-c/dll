using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000066 RID: 102
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AccessTools2
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		[return: Nullable(2)]
		public static ConstructorInfo DeclaredConstructor(Type type, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, bool searchForStatic = false, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			ConstructorInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.DeclaredConstructor: 'type' is null");
				}
				result = null;
			}
			else
			{
				bool flag2 = parameters == null;
				if (flag2)
				{
					parameters = Type.EmptyTypes;
				}
				BindingFlags flags = searchForStatic ? (AccessTools.allDeclared & ~BindingFlags.Instance) : (AccessTools.allDeclared & ~BindingFlags.Static);
				result = type.GetConstructor(flags, null, parameters, new ParameterModifier[0]);
			}
			return result;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000FD10 File Offset: 0x0000DF10
		[return: Nullable(2)]
		public static ConstructorInfo Constructor(Type type, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, bool searchForStatic = false, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			ConstructorInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.ConstructorInfo: 'type' is null");
				}
				result = null;
			}
			else
			{
				bool flag2 = parameters == null;
				if (flag2)
				{
					parameters = Type.EmptyTypes;
				}
				BindingFlags flags = searchForStatic ? (AccessTools.all & ~BindingFlags.Instance) : (AccessTools.all & ~BindingFlags.Static);
				result = AccessTools2.FindIncludingBaseTypes<ConstructorInfo>(type, (Type t) => t.GetConstructor(flags, null, parameters, new ParameterModifier[0]));
			}
			return result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000FD94 File Offset: 0x0000DF94
		[return: Nullable(2)]
		public static ConstructorInfo DeclaredConstructor(string typeString, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, bool searchForStatic = false, bool logErrorInTrace = true)
		{
			bool flag = string.IsNullOrWhiteSpace(typeString);
			ConstructorInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Constructor: 'typeString' is null or whitespace/empty");
				}
				result = null;
			}
			else
			{
				Type type = AccessTools2.TypeByName(typeString, logErrorInTrace);
				bool flag2 = type == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = AccessTools2.DeclaredConstructor(type, parameters, searchForStatic, logErrorInTrace);
				}
			}
			return result;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		[return: Nullable(2)]
		public static ConstructorInfo Constructor(string typeString, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, bool searchForStatic = false, bool logErrorInTrace = true)
		{
			bool flag = string.IsNullOrWhiteSpace(typeString);
			ConstructorInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Constructor: 'typeString' is null or whitespace/empty");
				}
				result = null;
			}
			else
			{
				Type type = AccessTools2.TypeByName(typeString, logErrorInTrace);
				bool flag2 = type == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = AccessTools2.Constructor(type, parameters, searchForStatic, logErrorInTrace);
				}
			}
			return result;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000FE34 File Offset: 0x0000E034
		[return: Nullable(2)]
		public static TDelegate GetDeclaredConstructorDelegate<[Nullable(0)] TDelegate>(Type type, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			ConstructorInfo constructorInfo = AccessTools2.DeclaredConstructor(type, parameters, false, logErrorInTrace);
			return (constructorInfo != null) ? AccessTools2.GetDelegate<TDelegate>(constructorInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000FE60 File Offset: 0x0000E060
		[return: Nullable(2)]
		public static TDelegate GetConstructorDelegate<[Nullable(0)] TDelegate>(Type type, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			ConstructorInfo constructorInfo = AccessTools2.Constructor(type, parameters, false, logErrorInTrace);
			return (constructorInfo != null) ? AccessTools2.GetDelegate<TDelegate>(constructorInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000FE8C File Offset: 0x0000E08C
		[return: Nullable(2)]
		public static TDelegate GetDeclaredConstructorDelegate<[Nullable(0)] TDelegate>(string typeString, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			ConstructorInfo constructorInfo = AccessTools2.DeclaredConstructor(typeString, parameters, false, logErrorInTrace);
			return (constructorInfo != null) ? AccessTools2.GetDelegate<TDelegate>(constructorInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		[return: Nullable(2)]
		public static TDelegate GetConstructorDelegate<[Nullable(0)] TDelegate>(string typeString, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			ConstructorInfo constructorInfo = AccessTools2.Constructor(typeString, parameters, false, logErrorInTrace);
			return (constructorInfo != null) ? AccessTools2.GetDelegate<TDelegate>(constructorInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000FF14 File Offset: 0x0000E114
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000FF44 File Offset: 0x0000E144
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000FF78 File Offset: 0x0000E178
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000FFAC File Offset: 0x0000E1AC
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00010004 File Offset: 0x0000E204
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00010030 File Offset: 0x0000E230
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001005C File Offset: 0x0000E25C
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00010088 File Offset: 0x0000E288
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000100B4 File Offset: 0x0000E2B4
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000100E0 File Offset: 0x0000E2E0
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001010C File Offset: 0x0000E30C
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010138 File Offset: 0x0000E338
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010164 File Offset: 0x0000E364
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00010190 File Offset: 0x0000E390
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000101BC File Offset: 0x0000E3BC
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000101E8 File Offset: 0x0000E3E8
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010214 File Offset: 0x0000E414
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010240 File Offset: 0x0000E440
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001026C File Offset: 0x0000E46C
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>(ConstructorInfo constructorInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			bool flag = constructorInfo == null;
			TDelegate result;
			if (flag)
			{
				result = default(TDelegate);
			}
			else
			{
				MethodInfo delegateInvoke = typeof(TDelegate).GetMethod("Invoke");
				bool flag2 = delegateInvoke == null;
				if (flag2)
				{
					result = default(TDelegate);
				}
				else
				{
					bool flag3 = !delegateInvoke.ReturnType.IsAssignableFrom(constructorInfo.DeclaringType);
					if (flag3)
					{
						result = default(TDelegate);
					}
					else
					{
						ParameterInfo[] delegateParameters = delegateInvoke.GetParameters();
						ParameterInfo[] constructorParameters = constructorInfo.GetParameters();
						bool flag4 = delegateParameters.Length - constructorParameters.Length != 0 && !AccessTools2.ParametersAreEqual(delegateParameters, constructorParameters);
						if (flag4)
						{
							result = default(TDelegate);
						}
						else
						{
							ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
							List<ParameterExpression> returnParameters = delegateParameters.Select((ParameterInfo pi, int i) => Expression.Parameter(pi.ParameterType, string.Format("p{0}", i))).ToList<ParameterExpression>();
							List<Expression> inputParameters = returnParameters.Select(delegate(ParameterExpression pe, int i)
							{
								bool flag5 = pe.IsByRef || pe.Type.Equals(constructorParameters[i].ParameterType);
								Expression result2;
								if (flag5)
								{
									result2 = pe;
								}
								else
								{
									result2 = Expression.Convert(pe, constructorParameters[i].ParameterType);
								}
								return result2;
							}).ToList<Expression>();
							Expression @new = Expression.New(constructorInfo, inputParameters);
							UnaryExpression body = Expression.Convert(@new, delegateInvoke.ReturnType);
							try
							{
								result = Expression.Lambda<TDelegate>(body, returnParameters).Compile();
							}
							catch (Exception ex)
							{
								if (logErrorInTrace)
								{
									Trace.TraceError(string.Format("AccessTools2.GetDelegate<{0}>: Error while compiling lambds expression '{1}'", typeof(TDelegate).FullName, ex));
								}
								result = default(TDelegate);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010418 File Offset: 0x0000E618
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			bool flag = methodInfo == null;
			TDelegate result;
			if (flag)
			{
				result = default(TDelegate);
			}
			else
			{
				MethodInfo delegateInvoke = typeof(TDelegate).GetMethod("Invoke");
				bool flag2 = delegateInvoke == null;
				if (flag2)
				{
					result = default(TDelegate);
				}
				else
				{
					bool areEnums = delegateInvoke.ReturnType.IsEnum || methodInfo.ReturnType.IsEnum;
					bool areNumeric = delegateInvoke.ReturnType.IsNumeric() || methodInfo.ReturnType.IsNumeric();
					bool flag3 = !areEnums && !areNumeric && !delegateInvoke.ReturnType.IsAssignableFrom(methodInfo.ReturnType);
					if (flag3)
					{
						result = default(TDelegate);
					}
					else
					{
						ParameterInfo[] delegateParameters = delegateInvoke.GetParameters();
						ParameterInfo[] methodParameters = methodInfo.GetParameters();
						bool hasSameParameters = delegateParameters.Length - methodParameters.Length == 0 && AccessTools2.ParametersAreEqual(delegateParameters, methodParameters);
						bool hasInstance = instance != null;
						bool hasInstanceType = delegateParameters.Length - methodParameters.Length == 1 && (delegateParameters[0].ParameterType.IsAssignableFrom(methodInfo.DeclaringType) || methodInfo.DeclaringType.IsAssignableFrom(delegateParameters[0].ParameterType));
						bool flag4 = !hasInstance && !hasInstanceType && !methodInfo.IsStatic;
						if (flag4)
						{
							result = default(TDelegate);
						}
						else
						{
							bool flag5 = hasInstance && methodInfo.IsStatic;
							if (flag5)
							{
								result = default(TDelegate);
							}
							else
							{
								bool flag6 = hasInstance && !methodInfo.IsStatic && !methodInfo.DeclaringType.IsAssignableFrom(instance.GetType());
								if (flag6)
								{
									result = default(TDelegate);
								}
								else
								{
									bool flag7 = hasSameParameters && hasInstanceType;
									if (flag7)
									{
										result = default(TDelegate);
									}
									else
									{
										bool flag8 = hasInstance && (hasInstanceType || !hasSameParameters);
										if (flag8)
										{
											result = default(TDelegate);
										}
										else
										{
											bool flag9 = hasInstanceType && (hasInstance || hasSameParameters);
											if (flag9)
											{
												result = default(TDelegate);
											}
											else
											{
												bool flag10 = !hasInstanceType && !hasInstance && !hasSameParameters;
												if (flag10)
												{
													result = default(TDelegate);
												}
												else
												{
													ParameterExpression instanceParameter = hasInstanceType ? Expression.Parameter(delegateParameters[0].ParameterType, "instance") : null;
													List<ParameterExpression> returnParameters = delegateParameters.Skip(hasInstanceType ? 1 : 0).Select((ParameterInfo pi, int i) => Expression.Parameter(pi.ParameterType, string.Format("p{0}", i))).ToList<ParameterExpression>();
													List<Expression> inputParameters = returnParameters.Select(delegate(ParameterExpression pe, int i)
													{
														bool flag12 = pe.IsByRef || pe.Type.Equals(methodParameters[i].ParameterType);
														Expression result2;
														if (flag12)
														{
															result2 = pe;
														}
														else
														{
															result2 = Expression.Convert(pe, methodParameters[i].ParameterType);
														}
														return result2;
													}).ToList<Expression>();
													MethodCallExpression call = hasInstance ? (instance.GetType().Equals(methodInfo.DeclaringType) ? Expression.Call(Expression.Constant(instance), methodInfo, inputParameters) : Expression.Call(Expression.Convert(Expression.Constant(instance), instance.GetType()), methodInfo, inputParameters)) : (hasSameParameters ? Expression.Call(methodInfo, inputParameters) : (hasInstanceType ? (instanceParameter.Type.Equals(methodInfo.DeclaringType) ? Expression.Call(instanceParameter, methodInfo, inputParameters) : Expression.Call(Expression.Convert(instanceParameter, methodInfo.DeclaringType), methodInfo, inputParameters)) : null));
													bool flag11 = call == null;
													if (flag11)
													{
														result = default(TDelegate);
													}
													else
													{
														UnaryExpression body = Expression.Convert(call, delegateInvoke.ReturnType);
														try
														{
															Expression body2 = body;
															IEnumerable<ParameterExpression> parameters;
															if (!hasInstanceType)
															{
																IEnumerable<ParameterExpression> enumerable = returnParameters;
																parameters = enumerable;
															}
															else
															{
																parameters = new List<ParameterExpression>
																{
																	instanceParameter
																}.Concat(returnParameters);
															}
															result = Expression.Lambda<TDelegate>(body2, parameters).Compile();
														}
														catch (Exception ex)
														{
															if (logErrorInTrace)
															{
																Trace.TraceError(string.Format("AccessTools2.GetDelegate<{0}>: Error while compiling lambds expression '{1}'", typeof(TDelegate).FullName, ex));
															}
															result = default(TDelegate);
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
			return result;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010820 File Offset: 0x0000EA20
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>(MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(null, methodInfo, logErrorInTrace);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001082A File Offset: 0x0000EA2A
		[return: Nullable(2)]
		public static TDelegate GetDelegateObjectInstance<[Nullable(0)] TDelegate>(MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010833 File Offset: 0x0000EA33
		public static bool IsNumeric(this Type myType)
		{
			return AccessTools2.NumericTypes.Contains(myType);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010840 File Offset: 0x0000EA40
		private static bool ParametersAreEqual(ParameterInfo[] delegateParameters, ParameterInfo[] methodParameters)
		{
			bool flag = delegateParameters.Length - methodParameters.Length == 0;
			bool result;
			if (flag)
			{
				for (int i = 0; i < methodParameters.Length; i++)
				{
					bool flag2 = delegateParameters[i].ParameterType.IsByRef != methodParameters[i].ParameterType.IsByRef;
					if (flag2)
					{
						return false;
					}
					bool areEnums = delegateParameters[i].ParameterType.IsEnum || methodParameters[i].ParameterType.IsEnum;
					bool areNumeric = delegateParameters[i].ParameterType.IsNumeric() || methodParameters[i].ParameterType.IsNumeric();
					bool flag3 = !areEnums && !areNumeric && !delegateParameters[i].ParameterType.IsAssignableFrom(methodParameters[i].ParameterType);
					if (flag3)
					{
						return false;
					}
				}
				result = true;
			}
			else
			{
				bool flag4 = delegateParameters.Length - methodParameters.Length == 1;
				if (flag4)
				{
					for (int j = 0; j < methodParameters.Length; j++)
					{
						bool flag5 = delegateParameters[j + 1].ParameterType.IsByRef != methodParameters[j].ParameterType.IsByRef;
						if (flag5)
						{
							return false;
						}
						bool areEnums2 = delegateParameters[j + 1].ParameterType.IsEnum || methodParameters[j].ParameterType.IsEnum;
						bool areNumeric2 = delegateParameters[j + 1].ParameterType.IsNumeric() || methodParameters[j].ParameterType.IsNumeric();
						bool flag6 = !areEnums2 && !areNumeric2 && !delegateParameters[j + 1].ParameterType.IsAssignableFrom(methodParameters[j].ParameterType);
						if (flag6)
						{
							return false;
						}
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00010A0E File Offset: 0x0000EC0E
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate, [Nullable(2)] TInstance>(TInstance instance, MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010A20 File Offset: 0x0000EC20
		[return: Nullable(2)]
		public static TDelegate GetDeclaredDelegateObjectInstance<[Nullable(0)] TDelegate>(Type type, string method, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredMethod(type, method, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegateObjectInstance<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00010A50 File Offset: 0x0000EC50
		[return: Nullable(2)]
		public static TDelegate GetDelegateObjectInstance<[Nullable(0)] TDelegate>(Type type, string method, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.Method(type, method, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegateObjectInstance<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010A80 File Offset: 0x0000EC80
		[return: Nullable(2)]
		public static TDelegate GetDeclaredDelegateObjectInstance<[Nullable(0)] TDelegate>(string typeSemicolonMethod, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredMethod(typeSemicolonMethod, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegateObjectInstance<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00010AAC File Offset: 0x0000ECAC
		[return: Nullable(2)]
		public static TDelegate GetDelegateObjectInstance<[Nullable(0)] TDelegate>(string typeSemicolonMethod, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.Method(typeSemicolonMethod, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegateObjectInstance<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		[return: Nullable(2)]
		public static TDelegate GetDeclaredDelegate<[Nullable(0)] TDelegate>(Type type, string method, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredMethod(type, method, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010B08 File Offset: 0x0000ED08
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>(Type type, string method, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.Method(type, method, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010B38 File Offset: 0x0000ED38
		[return: Nullable(2)]
		public static TDelegate GetDeclaredDelegate<[Nullable(0)] TDelegate>(string typeSemicolonMethod, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredMethod(typeSemicolonMethod, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010B64 File Offset: 0x0000ED64
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>(string typeSemicolonMethod, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.Method(typeSemicolonMethod, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010B90 File Offset: 0x0000ED90
		[return: Nullable(2)]
		public static TDelegate GetDeclaredDelegate<[Nullable(0)] TDelegate, [Nullable(2)] TInstance>(TInstance instance, string method, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			if (instance != null)
			{
				MethodInfo methodInfo = AccessTools2.DeclaredMethod(instance.GetType(), method, parameters, generics, logErrorInTrace);
				if (methodInfo != null)
				{
					return AccessTools2.GetDelegate<TDelegate, TInstance>(instance, methodInfo, logErrorInTrace);
				}
			}
			return default(TDelegate);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate, [Nullable(2)] TInstance>(TInstance instance, string method, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			if (instance != null)
			{
				MethodInfo methodInfo = AccessTools2.Method(instance.GetType(), method, parameters, generics, logErrorInTrace);
				if (methodInfo != null)
				{
					return AccessTools2.GetDelegate<TDelegate, TInstance>(instance, methodInfo, logErrorInTrace);
				}
			}
			return default(TDelegate);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010C18 File Offset: 0x0000EE18
		[return: Nullable(2)]
		public static TDelegate GetDeclaredDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredMethod(type, method, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010C4C File Offset: 0x0000EE4C
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.Method(type, method, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010C80 File Offset: 0x0000EE80
		[return: Nullable(2)]
		public static TDelegate GetDeclaredDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeSemicolonMethod, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredMethod(typeSemicolonMethod, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeSemicolonMethod, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.Method(typeSemicolonMethod, parameters, generics, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		[return: Nullable(2)]
		public static FieldInfo DeclaredField(Type type, string name, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			FieldInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.DeclaredField: 'type' is null");
				}
				result = null;
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.DeclaredField: type '{0}', 'name' is null", type));
					}
					result = null;
				}
				else
				{
					FieldInfo fieldInfo = type.GetField(name, AccessTools.allDeclared);
					bool flag3 = fieldInfo == null;
					if (flag3)
					{
						if (logErrorInTrace)
						{
							Trace.TraceError(string.Format("AccessTools2.DeclaredField: Could not find field for type '{0}' and name '{1}'", type, name));
						}
						result = null;
					}
					else
					{
						result = fieldInfo;
					}
				}
			}
			return result;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010D6C File Offset: 0x0000EF6C
		[return: Nullable(2)]
		public static FieldInfo Field(Type type, string name, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			FieldInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Field: 'type' is null");
				}
				result = null;
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.Field: type '{0}', 'name' is null", type));
					}
					result = null;
				}
				else
				{
					FieldInfo fieldInfo = AccessTools2.FindIncludingBaseTypes<FieldInfo>(type, (Type t) => t.GetField(name, AccessTools.all));
					bool flag3 = fieldInfo == null && logErrorInTrace;
					if (flag3)
					{
						Trace.TraceError(string.Format("AccessTools2.Field: Could not find field for type '{0}' and name '{1}'", type, name));
					}
					result = fieldInfo;
				}
			}
			return result;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010E10 File Offset: 0x0000F010
		[return: Nullable(2)]
		public static FieldInfo DeclaredField(string typeColonFieldname, bool logErrorInTrace = true)
		{
			Type type;
			string name;
			bool flag = !AccessTools2.TryGetComponents(typeColonFieldname, out type, out name, logErrorInTrace);
			FieldInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Field: Could not find type or field for '" + typeColonFieldname + "'");
				}
				result = null;
			}
			else
			{
				result = AccessTools2.DeclaredField(type, name, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010E60 File Offset: 0x0000F060
		[return: Nullable(2)]
		public static FieldInfo Field(string typeColonFieldname, bool logErrorInTrace = true)
		{
			Type type;
			string name;
			bool flag = !AccessTools2.TryGetComponents(typeColonFieldname, out type, out name, logErrorInTrace);
			FieldInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Field: Could not find type or field for '" + typeColonFieldname + "'");
				}
				result = null;
			}
			else
			{
				result = AccessTools2.Field(type, name, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010EB0 File Offset: 0x0000F0B0
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<object, F> FieldRefAccess<[Nullable(2)] F>(string typeColonFieldname, bool logErrorInTrace = true)
		{
			Type type;
			string name;
			bool flag = !AccessTools2.TryGetComponents(typeColonFieldname, out type, out name, logErrorInTrace);
			AccessTools.FieldRef<object, F> result;
			if (flag)
			{
				Trace.TraceError("AccessTools2.FieldRefAccess: Could not find type or field for '" + typeColonFieldname + "'");
				result = null;
			}
			else
			{
				result = AccessTools2.FieldRefAccess<F>(type, name, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<T, F> FieldRefAccess<T, [Nullable(2)] F>(string fieldName, bool logErrorInTrace = true) where T : class
		{
			bool flag = fieldName == null;
			AccessTools.FieldRef<T, F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FieldInfo field = AccessTools2.GetInstanceField(typeof(T), fieldName, logErrorInTrace);
				bool flag2 = field == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = AccessTools2.FieldRefAccessInternal<T, F>(field, false, logErrorInTrace);
				}
			}
			return result;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010F3C File Offset: 0x0000F13C
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<object, F> FieldRefAccess<[Nullable(2)] F>(Type type, string fieldName, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			AccessTools.FieldRef<object, F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = fieldName == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					FieldInfo fieldInfo = AccessTools2.Field(type, fieldName, logErrorInTrace);
					bool flag3 = fieldInfo == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						Type declaringType;
						bool flag4;
						if (!fieldInfo.IsStatic)
						{
							declaringType = fieldInfo.DeclaringType;
							flag4 = (declaringType != null);
						}
						else
						{
							flag4 = false;
						}
						bool flag5 = flag4;
						if (flag5)
						{
							bool isValueType = declaringType.IsValueType;
							if (isValueType)
							{
								if (logErrorInTrace)
								{
									Trace.TraceError("AccessTools2.FieldRefAccess<object, " + typeof(F).FullName + ">: FieldDeclaringType must be a class");
								}
								result = null;
							}
							else
							{
								result = AccessTools2.FieldRefAccessInternal<object, F>(fieldInfo, true, logErrorInTrace);
							}
						}
						else
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<T, F> FieldRefAccess<T, [Nullable(2)] F>(FieldInfo fieldInfo, bool logErrorInTrace = true) where T : class
		{
			bool flag = fieldInfo == null;
			AccessTools.FieldRef<T, F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Type declaringType;
				bool flag2;
				if (!fieldInfo.IsStatic)
				{
					declaringType = fieldInfo.DeclaringType;
					flag2 = (declaringType != null);
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					bool isValueType = declaringType.IsValueType;
					if (isValueType)
					{
						if (logErrorInTrace)
						{
							Trace.TraceError(string.Concat(new string[]
							{
								"AccessTools2.FieldRefAccess<",
								typeof(T).FullName,
								", ",
								typeof(F).FullName,
								">: FieldDeclaringType must be a class"
							}));
						}
						result = null;
					}
					else
					{
						bool? flag4 = AccessTools2.FieldRefNeedsClasscast(typeof(T), declaringType, logErrorInTrace);
						bool needCastclass;
						int num;
						if (flag4 != null)
						{
							needCastclass = flag4.GetValueOrDefault();
							num = 1;
						}
						else
						{
							num = 0;
						}
						bool flag5 = num == 0;
						if (flag5)
						{
							result = null;
						}
						else
						{
							result = AccessTools2.FieldRefAccessInternal<T, F>(fieldInfo, needCastclass, logErrorInTrace);
						}
					}
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000110D0 File Offset: 0x0000F2D0
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private static AccessTools.FieldRef<T, F> FieldRefAccessInternal<T, [Nullable(2)] F>(FieldInfo fieldInfo, bool needCastclass, bool logErrorInTrace = true) where T : class
		{
			bool flag = !AccessTools2.Helper.IsValid(logErrorInTrace);
			AccessTools.FieldRef<T, F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool isStatic = fieldInfo.IsStatic;
				if (isStatic)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Concat(new string[]
						{
							"AccessTools2.FieldRefAccessInternal<",
							typeof(T).FullName,
							", ",
							typeof(F).FullName,
							">: Field must not be static"
						}));
					}
					result = null;
				}
				else
				{
					bool flag2 = !AccessTools2.ValidateFieldType<F>(fieldInfo, logErrorInTrace);
					if (flag2)
					{
						result = null;
					}
					else
					{
						Type delegateInstanceType = typeof(T);
						Type declaringType = fieldInfo.DeclaringType;
						AccessTools2.DynamicMethodDefinitionHandle? dm = AccessTools2.DynamicMethodDefinitionHandle.Create("__refget_" + delegateInstanceType.Name + "_fi_" + fieldInfo.Name, typeof(F).MakeByRefType(), new Type[]
						{
							delegateInstanceType
						});
						AccessTools2.ILGeneratorHandle? ilgeneratorHandle = (dm != null) ? dm.GetValueOrDefault().GetILGenerator() : null;
						AccessTools2.ILGeneratorHandle il;
						int num;
						if (ilgeneratorHandle != null)
						{
							il = ilgeneratorHandle.GetValueOrDefault();
							num = 1;
						}
						else
						{
							num = 0;
						}
						bool flag3 = num == 0;
						if (flag3)
						{
							result = null;
						}
						else
						{
							il.Emit(OpCodes.Ldarg_0);
							if (needCastclass)
							{
								il.Emit(OpCodes.Castclass, declaringType);
							}
							il.Emit(OpCodes.Ldflda, fieldInfo);
							il.Emit(OpCodes.Ret);
							object obj;
							if (dm == null)
							{
								obj = null;
							}
							else
							{
								MethodInfo methodInfo = dm.GetValueOrDefault().Generate();
								obj = ((methodInfo != null) ? methodInfo.CreateDelegate(typeof(AccessTools.FieldRef<T, F>)) : null);
							}
							result = (obj as AccessTools.FieldRef<T, F>);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011288 File Offset: 0x0000F488
		private static bool? FieldRefNeedsClasscast(Type delegateInstanceType, Type declaringType, bool logErrorInTrace = true)
		{
			bool needCastclass = false;
			bool flag = delegateInstanceType != declaringType;
			if (flag)
			{
				needCastclass = delegateInstanceType.IsAssignableFrom(declaringType);
				bool flag2 = !needCastclass && !declaringType.IsAssignableFrom(delegateInstanceType);
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.FieldRefNeedsClasscast: FieldDeclaringType must be assignable from or to T (FieldRefAccess instance type) - 'instanceOfT is FieldDeclaringType' must be possible, delegateInstanceType '{0}', declaringType '{1}'", delegateInstanceType, declaringType));
					}
					return null;
				}
			}
			return new bool?(needCastclass);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000112F3 File Offset: 0x0000F4F3
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<object, TField> FieldRefAccess<[Nullable(2)] TField>(FieldInfo fieldInfo)
		{
			return (fieldInfo == null) ? null : AccessTools.FieldRefAccess<object, TField>(fieldInfo);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00011304 File Offset: 0x0000F504
		[return: Nullable(2)]
		public static MethodInfo DeclaredMethod(Type type, string name, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			MethodInfo result2;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.DeclaredMethod: 'type' is null");
				}
				result2 = null;
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.DeclaredMethod: type '{0}', 'name' is null", type));
					}
					result2 = null;
				}
				else
				{
					bool flag3 = parameters == null;
					MethodInfo result;
					if (flag3)
					{
						try
						{
							result = type.GetMethod(name, AccessTools.allDeclared);
						}
						catch (AmbiguousMatchException ex)
						{
							result = type.GetMethod(name, AccessTools.allDeclared, null, Type.EmptyTypes, new ParameterModifier[0]);
							bool flag4 = result == null;
							if (flag4)
							{
								if (logErrorInTrace)
								{
									Trace.TraceError(string.Format("AccessTools2.DeclaredMethod: Ambiguous match for type '{0}' and name '{1}' and parameters '{2}', '{3}'", new object[]
									{
										type,
										name,
										(parameters != null) ? parameters.Description() : null,
										ex
									}));
								}
								return null;
							}
						}
					}
					else
					{
						result = type.GetMethod(name, AccessTools.allDeclared, null, parameters, new ParameterModifier[0]);
					}
					bool flag5 = result == null;
					if (flag5)
					{
						if (logErrorInTrace)
						{
							Trace.TraceError(string.Format("AccessTools2.DeclaredMethod: Could not find method for type '{0}' and name '{1}' and parameters '{2}'", type, name, (parameters != null) ? parameters.Description() : null));
						}
						result2 = null;
					}
					else
					{
						bool flag6 = generics != null;
						if (flag6)
						{
							result = result.MakeGenericMethod(generics);
						}
						result2 = result;
					}
				}
			}
			return result2;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011458 File Offset: 0x0000F658
		[return: Nullable(2)]
		public static MethodInfo Method(Type type, string name, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			MethodInfo result2;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Method: 'type' is null");
				}
				result2 = null;
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.Method: type '{0}', 'name' is null", type));
					}
					result2 = null;
				}
				else
				{
					bool flag3 = parameters == null;
					MethodInfo result;
					if (flag3)
					{
						try
						{
							result = AccessTools2.FindIncludingBaseTypes<MethodInfo>(type, (Type t) => t.GetMethod(name, AccessTools.all));
						}
						catch (AmbiguousMatchException ex)
						{
							result = AccessTools2.FindIncludingBaseTypes<MethodInfo>(type, (Type t) => t.GetMethod(name, AccessTools.all, null, Type.EmptyTypes, new ParameterModifier[0]));
							bool flag4 = result == null;
							if (flag4)
							{
								if (logErrorInTrace)
								{
									string format = "AccessTools2.Method: Ambiguous match for type '{0}' and name '{1}' and parameters '{2}', '{3}'";
									object[] array = new object[4];
									array[0] = type;
									array[1] = name;
									int num = 2;
									Type[] parameters2 = parameters;
									array[num] = ((parameters2 != null) ? parameters2.Description() : null);
									array[3] = ex;
									Trace.TraceError(string.Format(format, array));
								}
								return null;
							}
						}
					}
					else
					{
						result = AccessTools2.FindIncludingBaseTypes<MethodInfo>(type, (Type t) => t.GetMethod(name, AccessTools.all, null, parameters, new ParameterModifier[0]));
					}
					bool flag5 = result == null;
					if (flag5)
					{
						if (logErrorInTrace)
						{
							string format2 = "AccessTools2.Method: Could not find method for type '{0}' and name '{1}' and parameters '{2}'";
							object name2 = name;
							Type[] parameters3 = parameters;
							Trace.TraceError(string.Format(format2, type, name2, (parameters3 != null) ? parameters3.Description() : null));
						}
						result2 = null;
					}
					else
					{
						bool flag6 = generics != null;
						if (flag6)
						{
							result = result.MakeGenericMethod(generics);
						}
						result2 = result;
					}
				}
			}
			return result2;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000115E8 File Offset: 0x0000F7E8
		[return: Nullable(2)]
		public static MethodInfo DeclaredMethod(string typeColonMethodname, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true)
		{
			Type type;
			string name;
			bool flag = !AccessTools2.TryGetComponents(typeColonMethodname, out type, out name, logErrorInTrace);
			MethodInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Method: Could not find type or property for '" + typeColonMethodname + "'");
				}
				result = null;
			}
			else
			{
				result = AccessTools2.DeclaredMethod(type, name, parameters, generics, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001163C File Offset: 0x0000F83C
		[return: Nullable(2)]
		public static MethodInfo Method(string typeColonMethodname, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameters = null, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] generics = null, bool logErrorInTrace = true)
		{
			Type type;
			string name;
			bool flag = !AccessTools2.TryGetComponents(typeColonMethodname, out type, out name, logErrorInTrace);
			MethodInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Method: Could not find type or property for '" + typeColonMethodname + "'");
				}
				result = null;
			}
			else
			{
				result = AccessTools2.Method(type, name, parameters, generics, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00011690 File Offset: 0x0000F890
		[return: Nullable(2)]
		public static PropertyInfo DeclaredProperty(Type type, string name, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			PropertyInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.DeclaredProperty: 'type' is null");
				}
				result = null;
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.DeclaredProperty: type '{0}', 'name' is null", type));
					}
					result = null;
				}
				else
				{
					PropertyInfo property = type.GetProperty(name, AccessTools.allDeclared);
					bool flag3 = property == null && logErrorInTrace;
					if (flag3)
					{
						Trace.TraceError(string.Format("AccessTools2.DeclaredProperty: Could not find property for type '{0}' and name '{1}'", type, name));
					}
					result = property;
				}
			}
			return result;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00011714 File Offset: 0x0000F914
		[return: Nullable(2)]
		public static PropertyInfo Property(Type type, string name, bool logErrorInTrace = true)
		{
			bool flag = type == null;
			PropertyInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Property: 'type' is null");
				}
				result = null;
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.Property: type '{0}', 'name' is null", type));
					}
					result = null;
				}
				else
				{
					PropertyInfo property = AccessTools2.FindIncludingBaseTypes<PropertyInfo>(type, (Type t) => t.GetProperty(name, AccessTools.all));
					bool flag3 = property == null && logErrorInTrace;
					if (flag3)
					{
						Trace.TraceError(string.Format("AccessTools2.Property: Could not find property for type '{0}' and name '{1}'", type, name));
					}
					result = property;
				}
			}
			return result;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000117B7 File Offset: 0x0000F9B7
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertyGetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000117CE File Offset: 0x0000F9CE
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertySetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000117E5 File Offset: 0x0000F9E5
		[return: Nullable(2)]
		public static MethodInfo PropertyGetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000117FC File Offset: 0x0000F9FC
		[return: Nullable(2)]
		public static MethodInfo PropertySetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00011814 File Offset: 0x0000FA14
		[return: Nullable(2)]
		public static PropertyInfo DeclaredProperty(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			Type type;
			string name;
			bool flag = !AccessTools2.TryGetComponents(typeColonPropertyName, out type, out name, logErrorInTrace);
			PropertyInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.DeclaredProperty: Could not find type or property for '" + typeColonPropertyName + "'");
				}
				result = null;
			}
			else
			{
				result = AccessTools2.DeclaredProperty(type, name, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00011864 File Offset: 0x0000FA64
		[return: Nullable(2)]
		public static PropertyInfo Property(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			Type type;
			string name;
			bool flag = !AccessTools2.TryGetComponents(typeColonPropertyName, out type, out name, logErrorInTrace);
			PropertyInfo result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.Property: Could not find type or property for '" + typeColonPropertyName + "'");
				}
				result = null;
			}
			else
			{
				result = AccessTools2.Property(type, name, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000118B3 File Offset: 0x0000FAB3
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertySetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000118C9 File Offset: 0x0000FAC9
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertyGetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000118DF File Offset: 0x0000FADF
		[return: Nullable(2)]
		public static MethodInfo PropertyGetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000118F5 File Offset: 0x0000FAF5
		[return: Nullable(2)]
		public static MethodInfo PropertySetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001190C File Offset: 0x0000FB0C
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static AccessTools.FieldRef<TField> StaticFieldRefAccess<[Nullable(2)] TField>(string typeColonFieldname, bool logErrorInTrace = true)
		{
			Type type;
			string name;
			bool flag = !AccessTools2.TryGetComponents(typeColonFieldname, out type, out name, logErrorInTrace);
			AccessTools.FieldRef<TField> result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.StaticFieldRefAccess: Could not find type or field for '" + typeColonFieldname + "'");
				}
				result = null;
			}
			else
			{
				result = AccessTools2.StaticFieldRefAccess<TField>(type, name, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001195C File Offset: 0x0000FB5C
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static AccessTools.FieldRef<F> StaticFieldRefAccess<[Nullable(2)] F>(FieldInfo fieldInfo, bool logErrorInTrace = true)
		{
			bool flag = fieldInfo == null;
			AccessTools.FieldRef<F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = AccessTools2.StaticFieldRefAccessInternal<F>(fieldInfo, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00011984 File Offset: 0x0000FB84
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static AccessTools.FieldRef<TField> StaticFieldRefAccess<[Nullable(2)] TField>(Type type, string fieldName, bool logErrorInTrace = true)
		{
			FieldInfo fieldInfo = AccessTools2.Field(type, fieldName, logErrorInTrace);
			bool flag = fieldInfo == null;
			AccessTools.FieldRef<TField> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = AccessTools2.StaticFieldRefAccess<TField>(fieldInfo, logErrorInTrace);
			}
			return result;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000119B4 File Offset: 0x0000FBB4
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		private static AccessTools.FieldRef<F> StaticFieldRefAccessInternal<[Nullable(2)] F>(FieldInfo fieldInfo, bool logErrorInTrace = true)
		{
			bool flag = !AccessTools2.Helper.IsValid(logErrorInTrace);
			AccessTools.FieldRef<F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !fieldInfo.IsStatic;
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError("AccessTools2.StaticFieldRefAccessInternal<" + typeof(F).FullName + ">: Field must be static");
					}
					result = null;
				}
				else
				{
					bool flag3 = !AccessTools2.ValidateFieldType<F>(fieldInfo, logErrorInTrace);
					if (flag3)
					{
						result = null;
					}
					else
					{
						string str = "__refget_";
						Type declaringType = fieldInfo.DeclaringType;
						AccessTools2.DynamicMethodDefinitionHandle? dm = AccessTools2.DynamicMethodDefinitionHandle.Create(str + (((declaringType != null) ? declaringType.Name : null) ?? "null") + "_static_fi_" + fieldInfo.Name, typeof(F).MakeByRefType(), new Type[0]);
						AccessTools2.ILGeneratorHandle? ilgeneratorHandle = (dm != null) ? dm.GetValueOrDefault().GetILGenerator() : null;
						AccessTools2.ILGeneratorHandle il;
						int num;
						if (ilgeneratorHandle != null)
						{
							il = ilgeneratorHandle.GetValueOrDefault();
							num = 1;
						}
						else
						{
							num = 0;
						}
						bool flag4 = num == 0;
						if (flag4)
						{
							result = null;
						}
						else
						{
							il.Emit(OpCodes.Ldsflda, fieldInfo);
							il.Emit(OpCodes.Ret);
							object obj;
							if (dm == null)
							{
								obj = null;
							}
							else
							{
								MethodInfo methodInfo = dm.GetValueOrDefault().Generate();
								obj = ((methodInfo != null) ? methodInfo.CreateDelegate(typeof(AccessTools.FieldRef<F>)) : null);
							}
							result = (obj as AccessTools.FieldRef<F>);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00011B1C File Offset: 0x0000FD1C
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			2,
			0,
			1
		})]
		public static AccessTools.StructFieldRef<T, F> StructFieldRefAccess<T, [Nullable(2)] F>([Nullable(1)] string fieldName, bool logErrorInTrace = true) where T : struct
		{
			bool flag = string.IsNullOrEmpty(fieldName);
			AccessTools.StructFieldRef<T, F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FieldInfo field = AccessTools2.GetInstanceField(typeof(T), fieldName, logErrorInTrace);
				bool flag2 = field == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = AccessTools2.StructFieldRefAccessInternal<T, F>(field, logErrorInTrace);
				}
			}
			return result;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00011B64 File Offset: 0x0000FD64
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			2,
			0,
			1
		})]
		public static AccessTools.StructFieldRef<T, F> StructFieldRefAccess<[Nullable(0)] T, F>(FieldInfo fieldInfo, bool logErrorInTrace = true) where T : struct
		{
			bool flag = fieldInfo == null;
			AccessTools.StructFieldRef<T, F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !AccessTools2.ValidateStructField<T, F>(fieldInfo, logErrorInTrace);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = AccessTools2.StructFieldRefAccessInternal<T, F>(fieldInfo, logErrorInTrace);
				}
			}
			return result;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011B9C File Offset: 0x0000FD9C
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			2,
			0,
			1
		})]
		private static AccessTools.StructFieldRef<T, F> StructFieldRefAccessInternal<T, [Nullable(2)] F>([Nullable(1)] FieldInfo fieldInfo, bool logErrorInTrace = true) where T : struct
		{
			bool flag = !AccessTools2.ValidateFieldType<F>(fieldInfo, logErrorInTrace);
			AccessTools.StructFieldRef<T, F> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				AccessTools2.DynamicMethodDefinitionHandle? dm = AccessTools2.DynamicMethodDefinitionHandle.Create("__refget_" + typeof(T).Name + "_struct_fi_" + fieldInfo.Name, typeof(F).MakeByRefType(), new Type[]
				{
					typeof(T).MakeByRefType()
				});
				AccessTools2.ILGeneratorHandle? ilgeneratorHandle = (dm != null) ? dm.GetValueOrDefault().GetILGenerator() : null;
				AccessTools2.ILGeneratorHandle il;
				int num;
				if (ilgeneratorHandle != null)
				{
					il = ilgeneratorHandle.GetValueOrDefault();
					num = 1;
				}
				else
				{
					num = 0;
				}
				bool flag2 = num == 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldflda, fieldInfo);
					il.Emit(OpCodes.Ret);
					object obj;
					if (dm == null)
					{
						obj = null;
					}
					else
					{
						MethodInfo methodInfo = dm.GetValueOrDefault().Generate();
						obj = ((methodInfo != null) ? methodInfo.CreateDelegate(typeof(AccessTools.StructFieldRef<T, F>)) : null);
					}
					result = (obj as AccessTools.StructFieldRef<T, F>);
				}
			}
			return result;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00011CBC File Offset: 0x0000FEBC
		public static IEnumerable<Assembly> AllAssemblies()
		{
			return from a in AppDomain.CurrentDomain.GetAssemblies()
			where !a.FullName.StartsWith("Microsoft.VisualStudio")
			select a;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00011CEC File Offset: 0x0000FEEC
		public static IEnumerable<Type> AllTypes()
		{
			return AccessTools2.AllAssemblies().SelectMany((Assembly a) => AccessTools2.GetTypesFromAssembly(a, true));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00011D18 File Offset: 0x0000FF18
		public static Type[] GetTypesFromAssembly(Assembly assembly, bool logErrorInTrace = true)
		{
			bool flag = assembly == null;
			Type[] result;
			if (flag)
			{
				result = Type.EmptyTypes;
			}
			else
			{
				try
				{
					result = assembly.GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.GetTypesFromAssembly: assembly {0} => {1}", assembly, ex));
					}
					result = (from type in ex.Types
					where type != null
					select type).ToArray<Type>();
				}
			}
			return result;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		public static Type[] GetTypesFromAssemblyIfValid(Assembly assembly, bool logErrorInTrace = true)
		{
			bool flag = assembly == null;
			Type[] result;
			if (flag)
			{
				result = Type.EmptyTypes;
			}
			else
			{
				try
				{
					result = assembly.GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.GetTypesFromAssemblyIfValid: assembly {0} => {1}", assembly, ex));
					}
					result = Type.EmptyTypes;
				}
			}
			return result;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00011DFC File Offset: 0x0000FFFC
		[return: Nullable(2)]
		public static Type TypeByName(string name, bool logErrorInTrace = true)
		{
			bool flag = string.IsNullOrEmpty(name);
			Type result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.TypeByName: 'name' is null or empty");
				}
				result = null;
			}
			else
			{
				Type type = Type.GetType(name, false);
				bool flag2 = type == null;
				if (flag2)
				{
					type = AccessTools2.AllTypes().FirstOrDefault((Type t) => t.FullName == name);
				}
				bool flag3 = type == null;
				if (flag3)
				{
					type = AccessTools2.AllTypes().FirstOrDefault((Type t) => t.Name == name);
				}
				bool flag4 = type == null && logErrorInTrace;
				if (flag4)
				{
					Trace.TraceError("AccessTools2.TypeByName: Could not find type named '" + name + "'");
				}
				result = type;
			}
			return result;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00011EBC File Offset: 0x000100BC
		[return: Nullable(2)]
		public static T FindIncludingBaseTypes<T>(Type type, Func<Type, T> func) where T : class
		{
			bool flag = type == null || func == null;
			T result2;
			if (flag)
			{
				result2 = default(T);
			}
			else
			{
				T result;
				for (;;)
				{
					result = func(type);
					bool flag2 = result != null;
					if (flag2)
					{
						break;
					}
					type = type.BaseType;
					bool flag3 = type == null;
					if (flag3)
					{
						goto Block_4;
					}
				}
				return result;
				Block_4:
				result2 = default(T);
			}
			return result2;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00011F28 File Offset: 0x00010128
		[return: Nullable(2)]
		private static FieldInfo GetInstanceField(Type type, string fieldName, bool logErrorInTrace = true)
		{
			FieldInfo fieldInfo = AccessTools2.Field(type, fieldName, logErrorInTrace);
			bool flag = fieldInfo == null;
			FieldInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool isStatic = fieldInfo.IsStatic;
				if (isStatic)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Format("AccessTools2.GetInstanceField: Field must not be static, type '{0}', fieldName '{1}'", type, fieldName));
					}
					result = null;
				}
				else
				{
					result = fieldInfo;
				}
			}
			return result;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00011F78 File Offset: 0x00010178
		[NullableContext(2)]
		private static bool ValidateFieldType<F>(FieldInfo fieldInfo, bool logErrorInTrace = true)
		{
			bool flag = fieldInfo == null;
			bool result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.ValidateFieldType<" + typeof(F).FullName + ">: 'fieldInfo' is null");
				}
				result = false;
			}
			else
			{
				Type returnType = typeof(F);
				Type fieldType = fieldInfo.FieldType;
				bool flag2 = returnType == fieldType;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool isEnum = fieldType.IsEnum;
					if (isEnum)
					{
						Type underlyingType = Enum.GetUnderlyingType(fieldType);
						bool flag3 = returnType != underlyingType;
						if (flag3)
						{
							if (logErrorInTrace)
							{
								Trace.TraceError(string.Format("AccessTools2.ValidateFieldType<{0}>: FieldRefAccess return type must be the same as FieldType or FieldType's underlying integral type ({1}) for enum types, fieldInfo '{2}'", typeof(F).FullName, underlyingType, fieldInfo));
							}
							return false;
						}
					}
					else
					{
						bool isValueType = fieldType.IsValueType;
						if (isValueType)
						{
							if (logErrorInTrace)
							{
								Trace.TraceError(string.Format("AccessTools2.ValidateFieldType<{0}>: FieldRefAccess return type must be the same as FieldType for value types, fieldInfo '{1}'", typeof(F).FullName, fieldInfo));
							}
							return false;
						}
						bool flag4 = !returnType.IsAssignableFrom(fieldType);
						if (flag4)
						{
							if (logErrorInTrace)
							{
								Trace.TraceError("AccessTools2.ValidateFieldType<" + typeof(F).FullName + ">: FieldRefAccess return type must be assignable from FieldType for reference types");
							}
							return false;
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000120C4 File Offset: 0x000102C4
		[NullableContext(2)]
		private static bool ValidateStructField<[Nullable(0)] T, F>(FieldInfo fieldInfo, bool logErrorInTrace = true) where T : struct
		{
			bool flag = fieldInfo == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool isStatic = fieldInfo.IsStatic;
				if (isStatic)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError(string.Concat(new string[]
						{
							"AccessTools2.ValidateStructField<",
							typeof(T).FullName,
							", ",
							typeof(F).FullName,
							">: Field must not be static"
						}));
					}
					result = false;
				}
				else
				{
					bool flag2 = fieldInfo.DeclaringType != typeof(T);
					if (flag2)
					{
						if (logErrorInTrace)
						{
							Trace.TraceError(string.Concat(new string[]
							{
								"AccessTools2.ValidateStructField<",
								typeof(T).FullName,
								", ",
								typeof(F).FullName,
								">: FieldDeclaringType must be T (StructFieldRefAccess instance type)"
							}));
						}
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000121BC File Offset: 0x000103BC
		[NullableContext(2)]
		private static bool TryGetComponents([Nullable(1)] string typeColonName, out Type type, out string name, bool logErrorInTrace = true)
		{
			bool flag = string.IsNullOrWhiteSpace(typeColonName);
			bool result;
			if (flag)
			{
				if (logErrorInTrace)
				{
					Trace.TraceError("AccessTools2.TryGetComponents: 'typeColonName' is null or whitespace/empty");
				}
				type = null;
				name = null;
				result = false;
			}
			else
			{
				string[] parts = typeColonName.Split(new char[]
				{
					':'
				});
				bool flag2 = parts.Length != 2;
				if (flag2)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError("AccessTools2.TryGetComponents: typeColonName '" + typeColonName + "', name must be specified as 'Namespace.Type1.Type2:Name");
					}
					type = null;
					name = null;
					result = false;
				}
				else
				{
					type = AccessTools2.TypeByName(parts[0], logErrorInTrace);
					name = parts[1];
					result = (type != null);
				}
			}
			return result;
		}

		// Token: 0x0400015B RID: 347
		private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
		{
			typeof(long),
			typeof(ulong),
			typeof(int),
			typeof(uint),
			typeof(short),
			typeof(ushort),
			typeof(byte),
			typeof(sbyte)
		};

		// Token: 0x020000FA RID: 250
		[Nullable(0)]
		[ExcludeFromCodeCoverage]
		private readonly struct DynamicMethodDefinitionHandle
		{
			// Token: 0x060006C2 RID: 1730 RVA: 0x00019570 File Offset: 0x00017770
			public static AccessTools2.DynamicMethodDefinitionHandle? Create(string name, Type returnType, Type[] parameterTypes)
			{
				return (AccessTools2.Helper.DynamicMethodDefinitionCtor == null) ? null : new AccessTools2.DynamicMethodDefinitionHandle?(new AccessTools2.DynamicMethodDefinitionHandle(AccessTools2.Helper.DynamicMethodDefinitionCtor(name, returnType, parameterTypes)));
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x000195A6 File Offset: 0x000177A6
			public DynamicMethodDefinitionHandle(object dynamicMethodDefinition)
			{
				this._dynamicMethodDefinition = dynamicMethodDefinition;
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x000195B0 File Offset: 0x000177B0
			public AccessTools2.ILGeneratorHandle? GetILGenerator()
			{
				return (AccessTools2.Helper.GetILGenerator == null) ? null : new AccessTools2.ILGeneratorHandle?(new AccessTools2.ILGeneratorHandle(AccessTools2.Helper.GetILGenerator(this._dynamicMethodDefinition)));
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x000195E9 File Offset: 0x000177E9
			[NullableContext(2)]
			public MethodInfo Generate()
			{
				return (AccessTools2.Helper.Generate == null) ? null : AccessTools2.Helper.Generate(this._dynamicMethodDefinition);
			}

			// Token: 0x0400031A RID: 794
			private readonly object _dynamicMethodDefinition;
		}

		// Token: 0x020000FB RID: 251
		[Nullable(0)]
		[ExcludeFromCodeCoverage]
		private readonly struct ILGeneratorHandle
		{
			// Token: 0x060006C6 RID: 1734 RVA: 0x00019605 File Offset: 0x00017805
			public ILGeneratorHandle(object ilGenerator)
			{
				this._ilGenerator = ilGenerator;
			}

			// Token: 0x060006C7 RID: 1735 RVA: 0x0001960E File Offset: 0x0001780E
			public void Emit(OpCode opcode)
			{
				AccessTools2.Helper.Emit1Delegate emit = AccessTools2.Helper.Emit1;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode);
				}
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x00019628 File Offset: 0x00017828
			public void Emit(OpCode opcode, FieldInfo field)
			{
				AccessTools2.Helper.Emit2Delegate emit = AccessTools2.Helper.Emit2;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode, field);
				}
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x00019643 File Offset: 0x00017843
			public void Emit(OpCode opcode, Type type)
			{
				AccessTools2.Helper.Emit3Delegate emit = AccessTools2.Helper.Emit3;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode, type);
				}
			}

			// Token: 0x0400031B RID: 795
			private readonly object _ilGenerator;
		}

		// Token: 0x020000FC RID: 252
		[NullableContext(0)]
		[ExcludeFromCodeCoverage]
		private static class Helper
		{
			// Token: 0x060006CB RID: 1739 RVA: 0x00019760 File Offset: 0x00017960
			public static bool IsValid(bool logErrorInTrace = true)
			{
				bool flag = AccessTools2.Helper.DynamicMethodDefinitionCtor == null;
				bool result;
				if (flag)
				{
					if (logErrorInTrace)
					{
						Trace.TraceError("AccessTools2.Helper.IsValid: DynamicMethodDefinitionCtor is null");
					}
					result = false;
				}
				else
				{
					bool flag2 = AccessTools2.Helper.GetILGenerator == null;
					if (flag2)
					{
						if (logErrorInTrace)
						{
							Trace.TraceError("AccessTools2.Helper.IsValid: GetILGenerator is null");
						}
						result = false;
					}
					else
					{
						bool flag3 = AccessTools2.Helper.Emit1 == null;
						if (flag3)
						{
							if (logErrorInTrace)
							{
								Trace.TraceError("AccessTools2.Helper.IsValid: Emit1 is null");
							}
							result = false;
						}
						else
						{
							bool flag4 = AccessTools2.Helper.Emit2 == null;
							if (flag4)
							{
								if (logErrorInTrace)
								{
									Trace.TraceError("AccessTools2.Helper.IsValid: Emit2 is null");
								}
								result = false;
							}
							else
							{
								bool flag5 = AccessTools2.Helper.Emit3 == null;
								if (flag5)
								{
									if (logErrorInTrace)
									{
										Trace.TraceError("AccessTools2.Helper.IsValid: Emit3 is null");
									}
									result = false;
								}
								else
								{
									bool flag6 = AccessTools2.Helper.Generate == null;
									if (flag6)
									{
										if (logErrorInTrace)
										{
											Trace.TraceError("AccessTools2.Helper.IsValid: Generate is null");
										}
										result = false;
									}
									else
									{
										result = true;
									}
								}
							}
						}
					}
				}
				return result;
			}

			// Token: 0x0400031C RID: 796
			[Nullable(2)]
			public static readonly AccessTools2.Helper.DynamicMethodDefinitionCtorDelegate DynamicMethodDefinitionCtor = AccessTools2.GetDeclaredConstructorDelegate<AccessTools2.Helper.DynamicMethodDefinitionCtorDelegate>("MonoMod.Utils.DynamicMethodDefinition", new Type[]
			{
				typeof(string),
				typeof(Type),
				typeof(Type[])
			}, true);

			// Token: 0x0400031D RID: 797
			[Nullable(2)]
			public static readonly AccessTools2.Helper.GetILGeneratorDelegate GetILGenerator = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.GetILGeneratorDelegate>("MonoMod.Utils.DynamicMethodDefinition:GetILGenerator", Type.EmptyTypes, null, true);

			// Token: 0x0400031E RID: 798
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit1Delegate Emit1 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit1Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode)
			}, null, true);

			// Token: 0x0400031F RID: 799
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit2Delegate Emit2 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit2Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode),
				typeof(FieldInfo)
			}, null, true);

			// Token: 0x04000320 RID: 800
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit3Delegate Emit3 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit3Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode),
				typeof(Type)
			}, null, true);

			// Token: 0x04000321 RID: 801
			[Nullable(2)]
			public static readonly AccessTools2.Helper.GenerateDelegate Generate = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.GenerateDelegate>("MonoMod.Utils.DynamicMethodDefinition:Generate", Type.EmptyTypes, null, true);

			// Token: 0x0200010E RID: 270
			// (Invoke) Token: 0x060006F8 RID: 1784
			public delegate object DynamicMethodDefinitionCtorDelegate(string name, Type returnType, Type[] parameterTypes);

			// Token: 0x0200010F RID: 271
			// (Invoke) Token: 0x060006FC RID: 1788
			public delegate object GetILGeneratorDelegate(object instance);

			// Token: 0x02000110 RID: 272
			// (Invoke) Token: 0x06000700 RID: 1792
			public delegate void Emit1Delegate(object instance, OpCode opcode);

			// Token: 0x02000111 RID: 273
			// (Invoke) Token: 0x06000704 RID: 1796
			public delegate void Emit2Delegate(object instance, OpCode opcode, FieldInfo field);

			// Token: 0x02000112 RID: 274
			// (Invoke) Token: 0x06000708 RID: 1800
			public delegate void Emit3Delegate(object instance, OpCode opcode, Type type);

			// Token: 0x02000113 RID: 275
			// (Invoke) Token: 0x0600070C RID: 1804
			public delegate MethodInfo GenerateDelegate(object instance);
		}
	}
}
