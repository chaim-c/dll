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
	// Token: 0x02000152 RID: 338
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AccessTools2
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x0001DE80 File Offset: 0x0001C080
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

		// Token: 0x06000903 RID: 2307 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
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

		// Token: 0x06000904 RID: 2308 RVA: 0x0001DF6C File Offset: 0x0001C16C
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

		// Token: 0x06000905 RID: 2309 RVA: 0x0001DFBC File Offset: 0x0001C1BC
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

		// Token: 0x06000906 RID: 2310 RVA: 0x0001E00C File Offset: 0x0001C20C
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

		// Token: 0x06000907 RID: 2311 RVA: 0x0001E038 File Offset: 0x0001C238
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

		// Token: 0x06000908 RID: 2312 RVA: 0x0001E064 File Offset: 0x0001C264
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

		// Token: 0x06000909 RID: 2313 RVA: 0x0001E090 File Offset: 0x0001C290
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

		// Token: 0x0600090A RID: 2314 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001E11C File Offset: 0x0001C31C
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001E150 File Offset: 0x0001C350
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001E184 File Offset: 0x0001C384
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001E1DC File Offset: 0x0001C3DC
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001E208 File Offset: 0x0001C408
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001E234 File Offset: 0x0001C434
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001E260 File Offset: 0x0001C460
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001E28C File Offset: 0x0001C48C
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001E310 File Offset: 0x0001C510
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001E33C File Offset: 0x0001C53C
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001E368 File Offset: 0x0001C568
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001E394 File Offset: 0x0001C594
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001E3EC File Offset: 0x0001C5EC
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001E418 File Offset: 0x0001C618
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001E444 File Offset: 0x0001C644
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

		// Token: 0x0600091F RID: 2335 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
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

		// Token: 0x06000920 RID: 2336 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>(MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(null, methodInfo, logErrorInTrace);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001EA02 File Offset: 0x0001CC02
		[return: Nullable(2)]
		public static TDelegate GetDelegateObjectInstance<[Nullable(0)] TDelegate>(MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001EA0B File Offset: 0x0001CC0B
		public static bool IsNumeric(this Type myType)
		{
			return AccessTools2.NumericTypes.Contains(myType);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001EA18 File Offset: 0x0001CC18
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

		// Token: 0x06000924 RID: 2340 RVA: 0x0001EBE6 File Offset: 0x0001CDE6
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate, [Nullable(2)] TInstance>(TInstance instance, MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001EBF8 File Offset: 0x0001CDF8
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

		// Token: 0x06000926 RID: 2342 RVA: 0x0001EC28 File Offset: 0x0001CE28
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

		// Token: 0x06000927 RID: 2343 RVA: 0x0001EC58 File Offset: 0x0001CE58
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

		// Token: 0x06000928 RID: 2344 RVA: 0x0001EC84 File Offset: 0x0001CE84
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

		// Token: 0x06000929 RID: 2345 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
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

		// Token: 0x0600092A RID: 2346 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
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

		// Token: 0x0600092B RID: 2347 RVA: 0x0001ED10 File Offset: 0x0001CF10
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

		// Token: 0x0600092C RID: 2348 RVA: 0x0001ED3C File Offset: 0x0001CF3C
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

		// Token: 0x0600092D RID: 2349 RVA: 0x0001ED68 File Offset: 0x0001CF68
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

		// Token: 0x0600092E RID: 2350 RVA: 0x0001EDAC File Offset: 0x0001CFAC
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

		// Token: 0x0600092F RID: 2351 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
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

		// Token: 0x06000930 RID: 2352 RVA: 0x0001EE24 File Offset: 0x0001D024
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

		// Token: 0x06000931 RID: 2353 RVA: 0x0001EE58 File Offset: 0x0001D058
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

		// Token: 0x06000932 RID: 2354 RVA: 0x0001EE88 File Offset: 0x0001D088
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

		// Token: 0x06000933 RID: 2355 RVA: 0x0001EEB8 File Offset: 0x0001D0B8
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

		// Token: 0x06000934 RID: 2356 RVA: 0x0001EF44 File Offset: 0x0001D144
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

		// Token: 0x06000935 RID: 2357 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
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

		// Token: 0x06000936 RID: 2358 RVA: 0x0001F038 File Offset: 0x0001D238
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

		// Token: 0x06000937 RID: 2359 RVA: 0x0001F088 File Offset: 0x0001D288
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

		// Token: 0x06000938 RID: 2360 RVA: 0x0001F0D0 File Offset: 0x0001D2D0
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

		// Token: 0x06000939 RID: 2361 RVA: 0x0001F114 File Offset: 0x0001D314
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

		// Token: 0x0600093A RID: 2362 RVA: 0x0001F1C0 File Offset: 0x0001D3C0
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

		// Token: 0x0600093B RID: 2363 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
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

		// Token: 0x0600093C RID: 2364 RVA: 0x0001F460 File Offset: 0x0001D660
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

		// Token: 0x0600093D RID: 2365 RVA: 0x0001F4CB File Offset: 0x0001D6CB
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

		// Token: 0x0600093E RID: 2366 RVA: 0x0001F4DC File Offset: 0x0001D6DC
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

		// Token: 0x0600093F RID: 2367 RVA: 0x0001F630 File Offset: 0x0001D830
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

		// Token: 0x06000940 RID: 2368 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
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

		// Token: 0x06000941 RID: 2369 RVA: 0x0001F814 File Offset: 0x0001DA14
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

		// Token: 0x06000942 RID: 2370 RVA: 0x0001F868 File Offset: 0x0001DA68
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

		// Token: 0x06000943 RID: 2371 RVA: 0x0001F8EC File Offset: 0x0001DAEC
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

		// Token: 0x06000944 RID: 2372 RVA: 0x0001F98F File Offset: 0x0001DB8F
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertyGetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001F9A6 File Offset: 0x0001DBA6
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertySetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001F9BD File Offset: 0x0001DBBD
		[return: Nullable(2)]
		public static MethodInfo PropertyGetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		[return: Nullable(2)]
		public static MethodInfo PropertySetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001F9EC File Offset: 0x0001DBEC
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

		// Token: 0x06000949 RID: 2377 RVA: 0x0001FA3C File Offset: 0x0001DC3C
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

		// Token: 0x0600094A RID: 2378 RVA: 0x0001FA8B File Offset: 0x0001DC8B
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertySetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001FAA1 File Offset: 0x0001DCA1
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertyGetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001FAB7 File Offset: 0x0001DCB7
		[return: Nullable(2)]
		public static MethodInfo PropertyGetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001FACD File Offset: 0x0001DCCD
		[return: Nullable(2)]
		public static MethodInfo PropertySetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
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

		// Token: 0x0600094F RID: 2383 RVA: 0x0001FB34 File Offset: 0x0001DD34
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

		// Token: 0x06000950 RID: 2384 RVA: 0x0001FB5C File Offset: 0x0001DD5C
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

		// Token: 0x06000951 RID: 2385 RVA: 0x0001FB8C File Offset: 0x0001DD8C
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

		// Token: 0x06000952 RID: 2386 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
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

		// Token: 0x06000953 RID: 2387 RVA: 0x0001FD3C File Offset: 0x0001DF3C
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

		// Token: 0x06000954 RID: 2388 RVA: 0x0001FD74 File Offset: 0x0001DF74
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

		// Token: 0x06000955 RID: 2389 RVA: 0x0001FE94 File Offset: 0x0001E094
		public static IEnumerable<Assembly> AllAssemblies()
		{
			return from a in AppDomain.CurrentDomain.GetAssemblies()
			where !a.FullName.StartsWith("Microsoft.VisualStudio")
			select a;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001FEC4 File Offset: 0x0001E0C4
		public static IEnumerable<Type> AllTypes()
		{
			return AccessTools2.AllAssemblies().SelectMany((Assembly a) => AccessTools2.GetTypesFromAssembly(a, true));
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001FEF0 File Offset: 0x0001E0F0
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

		// Token: 0x06000958 RID: 2392 RVA: 0x0001FF78 File Offset: 0x0001E178
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

		// Token: 0x06000959 RID: 2393 RVA: 0x0001FFD4 File Offset: 0x0001E1D4
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

		// Token: 0x0600095A RID: 2394 RVA: 0x00020094 File Offset: 0x0001E294
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

		// Token: 0x0600095B RID: 2395 RVA: 0x00020100 File Offset: 0x0001E300
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

		// Token: 0x0600095C RID: 2396 RVA: 0x00020150 File Offset: 0x0001E350
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

		// Token: 0x0600095D RID: 2397 RVA: 0x0002029C File Offset: 0x0001E49C
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

		// Token: 0x0600095E RID: 2398 RVA: 0x00020394 File Offset: 0x0001E594
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

		// Token: 0x0400029D RID: 669
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

		// Token: 0x0200029B RID: 667
		[Nullable(0)]
		[ExcludeFromCodeCoverage]
		private readonly struct DynamicMethodDefinitionHandle
		{
			// Token: 0x06000EBB RID: 3771 RVA: 0x0002C86C File Offset: 0x0002AA6C
			public static AccessTools2.DynamicMethodDefinitionHandle? Create(string name, Type returnType, Type[] parameterTypes)
			{
				return (AccessTools2.Helper.DynamicMethodDefinitionCtor == null) ? null : new AccessTools2.DynamicMethodDefinitionHandle?(new AccessTools2.DynamicMethodDefinitionHandle(AccessTools2.Helper.DynamicMethodDefinitionCtor(name, returnType, parameterTypes)));
			}

			// Token: 0x06000EBC RID: 3772 RVA: 0x0002C8A2 File Offset: 0x0002AAA2
			public DynamicMethodDefinitionHandle(object dynamicMethodDefinition)
			{
				this._dynamicMethodDefinition = dynamicMethodDefinition;
			}

			// Token: 0x06000EBD RID: 3773 RVA: 0x0002C8AC File Offset: 0x0002AAAC
			public AccessTools2.ILGeneratorHandle? GetILGenerator()
			{
				return (AccessTools2.Helper.GetILGenerator == null) ? null : new AccessTools2.ILGeneratorHandle?(new AccessTools2.ILGeneratorHandle(AccessTools2.Helper.GetILGenerator(this._dynamicMethodDefinition)));
			}

			// Token: 0x06000EBE RID: 3774 RVA: 0x0002C8E5 File Offset: 0x0002AAE5
			[NullableContext(2)]
			public MethodInfo Generate()
			{
				return (AccessTools2.Helper.Generate == null) ? null : AccessTools2.Helper.Generate(this._dynamicMethodDefinition);
			}

			// Token: 0x04000667 RID: 1639
			private readonly object _dynamicMethodDefinition;
		}

		// Token: 0x0200029C RID: 668
		[Nullable(0)]
		[ExcludeFromCodeCoverage]
		private readonly struct ILGeneratorHandle
		{
			// Token: 0x06000EBF RID: 3775 RVA: 0x0002C901 File Offset: 0x0002AB01
			public ILGeneratorHandle(object ilGenerator)
			{
				this._ilGenerator = ilGenerator;
			}

			// Token: 0x06000EC0 RID: 3776 RVA: 0x0002C90A File Offset: 0x0002AB0A
			public void Emit(OpCode opcode)
			{
				AccessTools2.Helper.Emit1Delegate emit = AccessTools2.Helper.Emit1;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode);
				}
			}

			// Token: 0x06000EC1 RID: 3777 RVA: 0x0002C924 File Offset: 0x0002AB24
			public void Emit(OpCode opcode, FieldInfo field)
			{
				AccessTools2.Helper.Emit2Delegate emit = AccessTools2.Helper.Emit2;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode, field);
				}
			}

			// Token: 0x06000EC2 RID: 3778 RVA: 0x0002C93F File Offset: 0x0002AB3F
			public void Emit(OpCode opcode, Type type)
			{
				AccessTools2.Helper.Emit3Delegate emit = AccessTools2.Helper.Emit3;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode, type);
				}
			}

			// Token: 0x04000668 RID: 1640
			private readonly object _ilGenerator;
		}

		// Token: 0x0200029D RID: 669
		[NullableContext(0)]
		[ExcludeFromCodeCoverage]
		private static class Helper
		{
			// Token: 0x06000EC4 RID: 3780 RVA: 0x0002CA5C File Offset: 0x0002AC5C
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

			// Token: 0x04000669 RID: 1641
			[Nullable(2)]
			public static readonly AccessTools2.Helper.DynamicMethodDefinitionCtorDelegate DynamicMethodDefinitionCtor = AccessTools2.GetDeclaredConstructorDelegate<AccessTools2.Helper.DynamicMethodDefinitionCtorDelegate>("MonoMod.Utils.DynamicMethodDefinition", new Type[]
			{
				typeof(string),
				typeof(Type),
				typeof(Type[])
			}, true);

			// Token: 0x0400066A RID: 1642
			[Nullable(2)]
			public static readonly AccessTools2.Helper.GetILGeneratorDelegate GetILGenerator = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.GetILGeneratorDelegate>("MonoMod.Utils.DynamicMethodDefinition:GetILGenerator", Type.EmptyTypes, null, true);

			// Token: 0x0400066B RID: 1643
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit1Delegate Emit1 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit1Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode)
			}, null, true);

			// Token: 0x0400066C RID: 1644
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit2Delegate Emit2 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit2Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode),
				typeof(FieldInfo)
			}, null, true);

			// Token: 0x0400066D RID: 1645
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit3Delegate Emit3 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit3Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode),
				typeof(Type)
			}, null, true);

			// Token: 0x0400066E RID: 1646
			[Nullable(2)]
			public static readonly AccessTools2.Helper.GenerateDelegate Generate = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.GenerateDelegate>("MonoMod.Utils.DynamicMethodDefinition:Generate", Type.EmptyTypes, null, true);

			// Token: 0x020002B2 RID: 690
			// (Invoke) Token: 0x06000F03 RID: 3843
			public delegate object DynamicMethodDefinitionCtorDelegate(string name, Type returnType, Type[] parameterTypes);

			// Token: 0x020002B3 RID: 691
			// (Invoke) Token: 0x06000F07 RID: 3847
			public delegate object GetILGeneratorDelegate(object instance);

			// Token: 0x020002B4 RID: 692
			// (Invoke) Token: 0x06000F0B RID: 3851
			public delegate void Emit1Delegate(object instance, OpCode opcode);

			// Token: 0x020002B5 RID: 693
			// (Invoke) Token: 0x06000F0F RID: 3855
			public delegate void Emit2Delegate(object instance, OpCode opcode, FieldInfo field);

			// Token: 0x020002B6 RID: 694
			// (Invoke) Token: 0x06000F13 RID: 3859
			public delegate void Emit3Delegate(object instance, OpCode opcode, Type type);

			// Token: 0x020002B7 RID: 695
			// (Invoke) Token: 0x06000F17 RID: 3863
			public delegate MethodInfo GenerateDelegate(object instance);
		}
	}
}
