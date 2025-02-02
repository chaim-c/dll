﻿using System;
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
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AccessTools2
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002C6C File Offset: 0x00000E6C
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

		// Token: 0x0600003D RID: 61 RVA: 0x00002CD4 File Offset: 0x00000ED4
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

		// Token: 0x0600003E RID: 62 RVA: 0x00002D58 File Offset: 0x00000F58
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

		// Token: 0x0600003F RID: 63 RVA: 0x00002DA8 File Offset: 0x00000FA8
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

		// Token: 0x06000040 RID: 64 RVA: 0x00002DF8 File Offset: 0x00000FF8
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

		// Token: 0x06000041 RID: 65 RVA: 0x00002E24 File Offset: 0x00001024
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

		// Token: 0x06000042 RID: 66 RVA: 0x00002E50 File Offset: 0x00001050
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

		// Token: 0x06000043 RID: 67 RVA: 0x00002E7C File Offset: 0x0000107C
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

		// Token: 0x06000044 RID: 68 RVA: 0x00002EA8 File Offset: 0x000010A8
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002ED8 File Offset: 0x000010D8
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F08 File Offset: 0x00001108
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F3C File Offset: 0x0000113C
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, PropertyInfo propertyInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F70 File Offset: 0x00001170
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002F9C File Offset: 0x0000119C
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002FC8 File Offset: 0x000011C8
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002FF4 File Offset: 0x000011F4
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(Type type, string name, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(type, name, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003020 File Offset: 0x00001220
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000304C File Offset: 0x0000124C
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003078 File Offset: 0x00001278
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030A4 File Offset: 0x000012A4
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, Type type, string method, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(type, method, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000030D0 File Offset: 0x000012D0
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000030FC File Offset: 0x000012FC
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003128 File Offset: 0x00001328
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003154 File Offset: 0x00001354
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>(string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003180 File Offset: 0x00001380
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031AC File Offset: 0x000013AC
		[return: Nullable(2)]
		public static TDelegate GetDeclaredPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000031D8 File Offset: 0x000013D8
		[return: Nullable(2)]
		public static TDelegate GetPropertyGetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertyGetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003204 File Offset: 0x00001404
		[return: Nullable(2)]
		public static TDelegate GetPropertySetterDelegate<[Nullable(0)] TDelegate>([Nullable(2)] object instance, string typeColonPropertyName, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			MethodInfo methodInfo = AccessTools2.PropertySetter(typeColonPropertyName, logErrorInTrace);
			return (methodInfo != null) ? AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace) : default(TDelegate);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003230 File Offset: 0x00001430
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

		// Token: 0x06000059 RID: 89 RVA: 0x000033DC File Offset: 0x000015DC
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

		// Token: 0x0600005A RID: 90 RVA: 0x000037E4 File Offset: 0x000019E4
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate>(MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(null, methodInfo, logErrorInTrace);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000037EE File Offset: 0x000019EE
		[return: Nullable(2)]
		public static TDelegate GetDelegateObjectInstance<[Nullable(0)] TDelegate>(MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(methodInfo, logErrorInTrace);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000037F7 File Offset: 0x000019F7
		public static bool IsNumeric(this Type myType)
		{
			return AccessTools2.NumericTypes.Contains(myType);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003804 File Offset: 0x00001A04
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

		// Token: 0x0600005E RID: 94 RVA: 0x000039D2 File Offset: 0x00001BD2
		[return: Nullable(2)]
		public static TDelegate GetDelegate<[Nullable(0)] TDelegate, [Nullable(2)] TInstance>(TInstance instance, MethodInfo methodInfo, bool logErrorInTrace = true) where TDelegate : Delegate
		{
			return AccessTools2.GetDelegate<TDelegate>(instance, methodInfo, logErrorInTrace);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000039E4 File Offset: 0x00001BE4
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

		// Token: 0x06000060 RID: 96 RVA: 0x00003A14 File Offset: 0x00001C14
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

		// Token: 0x06000061 RID: 97 RVA: 0x00003A44 File Offset: 0x00001C44
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

		// Token: 0x06000062 RID: 98 RVA: 0x00003A70 File Offset: 0x00001C70
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

		// Token: 0x06000063 RID: 99 RVA: 0x00003A9C File Offset: 0x00001C9C
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

		// Token: 0x06000064 RID: 100 RVA: 0x00003ACC File Offset: 0x00001CCC
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

		// Token: 0x06000065 RID: 101 RVA: 0x00003AFC File Offset: 0x00001CFC
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

		// Token: 0x06000066 RID: 102 RVA: 0x00003B28 File Offset: 0x00001D28
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

		// Token: 0x06000067 RID: 103 RVA: 0x00003B54 File Offset: 0x00001D54
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

		// Token: 0x06000068 RID: 104 RVA: 0x00003B98 File Offset: 0x00001D98
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

		// Token: 0x06000069 RID: 105 RVA: 0x00003BDC File Offset: 0x00001DDC
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

		// Token: 0x0600006A RID: 106 RVA: 0x00003C10 File Offset: 0x00001E10
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

		// Token: 0x0600006B RID: 107 RVA: 0x00003C44 File Offset: 0x00001E44
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

		// Token: 0x0600006C RID: 108 RVA: 0x00003C74 File Offset: 0x00001E74
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

		// Token: 0x0600006D RID: 109 RVA: 0x00003CA4 File Offset: 0x00001EA4
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

		// Token: 0x0600006E RID: 110 RVA: 0x00003D30 File Offset: 0x00001F30
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

		// Token: 0x0600006F RID: 111 RVA: 0x00003DD4 File Offset: 0x00001FD4
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

		// Token: 0x06000070 RID: 112 RVA: 0x00003E24 File Offset: 0x00002024
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

		// Token: 0x06000071 RID: 113 RVA: 0x00003E74 File Offset: 0x00002074
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

		// Token: 0x06000072 RID: 114 RVA: 0x00003EBC File Offset: 0x000020BC
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

		// Token: 0x06000073 RID: 115 RVA: 0x00003F00 File Offset: 0x00002100
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

		// Token: 0x06000074 RID: 116 RVA: 0x00003FAC File Offset: 0x000021AC
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

		// Token: 0x06000075 RID: 117 RVA: 0x00004094 File Offset: 0x00002294
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

		// Token: 0x06000076 RID: 118 RVA: 0x0000424C File Offset: 0x0000244C
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

		// Token: 0x06000077 RID: 119 RVA: 0x000042B7 File Offset: 0x000024B7
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

		// Token: 0x06000078 RID: 120 RVA: 0x000042C8 File Offset: 0x000024C8
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

		// Token: 0x06000079 RID: 121 RVA: 0x0000441C File Offset: 0x0000261C
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

		// Token: 0x0600007A RID: 122 RVA: 0x000045AC File Offset: 0x000027AC
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

		// Token: 0x0600007B RID: 123 RVA: 0x00004600 File Offset: 0x00002800
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

		// Token: 0x0600007C RID: 124 RVA: 0x00004654 File Offset: 0x00002854
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

		// Token: 0x0600007D RID: 125 RVA: 0x000046D8 File Offset: 0x000028D8
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

		// Token: 0x0600007E RID: 126 RVA: 0x0000477B File Offset: 0x0000297B
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertyGetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004792 File Offset: 0x00002992
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertySetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000047A9 File Offset: 0x000029A9
		[return: Nullable(2)]
		public static MethodInfo PropertyGetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000047C0 File Offset: 0x000029C0
		[return: Nullable(2)]
		public static MethodInfo PropertySetter(Type type, string name, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(type, name, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000047D8 File Offset: 0x000029D8
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

		// Token: 0x06000083 RID: 131 RVA: 0x00004828 File Offset: 0x00002A28
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

		// Token: 0x06000084 RID: 132 RVA: 0x00004877 File Offset: 0x00002A77
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertySetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000488D File Offset: 0x00002A8D
		[return: Nullable(2)]
		public static MethodInfo DeclaredPropertyGetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.DeclaredProperty(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000048A3 File Offset: 0x00002AA3
		[return: Nullable(2)]
		public static MethodInfo PropertyGetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000048B9 File Offset: 0x00002AB9
		[return: Nullable(2)]
		public static MethodInfo PropertySetter(string typeColonPropertyName, bool logErrorInTrace = true)
		{
			PropertyInfo propertyInfo = AccessTools2.Property(typeColonPropertyName, logErrorInTrace);
			return (propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000048D0 File Offset: 0x00002AD0
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

		// Token: 0x06000089 RID: 137 RVA: 0x00004920 File Offset: 0x00002B20
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

		// Token: 0x0600008A RID: 138 RVA: 0x00004948 File Offset: 0x00002B48
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

		// Token: 0x0600008B RID: 139 RVA: 0x00004978 File Offset: 0x00002B78
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

		// Token: 0x0600008C RID: 140 RVA: 0x00004AE0 File Offset: 0x00002CE0
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

		// Token: 0x0600008D RID: 141 RVA: 0x00004B28 File Offset: 0x00002D28
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

		// Token: 0x0600008E RID: 142 RVA: 0x00004B60 File Offset: 0x00002D60
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

		// Token: 0x0600008F RID: 143 RVA: 0x00004C80 File Offset: 0x00002E80
		public static IEnumerable<Assembly> AllAssemblies()
		{
			return from a in AppDomain.CurrentDomain.GetAssemblies()
			where !a.FullName.StartsWith("Microsoft.VisualStudio")
			select a;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public static IEnumerable<Type> AllTypes()
		{
			return AccessTools2.AllAssemblies().SelectMany((Assembly a) => AccessTools2.GetTypesFromAssembly(a, true));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004CDC File Offset: 0x00002EDC
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

		// Token: 0x06000092 RID: 146 RVA: 0x00004D64 File Offset: 0x00002F64
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

		// Token: 0x06000093 RID: 147 RVA: 0x00004DC0 File Offset: 0x00002FC0
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

		// Token: 0x06000094 RID: 148 RVA: 0x00004E80 File Offset: 0x00003080
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

		// Token: 0x06000095 RID: 149 RVA: 0x00004EEC File Offset: 0x000030EC
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

		// Token: 0x06000096 RID: 150 RVA: 0x00004F3C File Offset: 0x0000313C
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

		// Token: 0x06000097 RID: 151 RVA: 0x00005088 File Offset: 0x00003288
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

		// Token: 0x06000098 RID: 152 RVA: 0x00005180 File Offset: 0x00003380
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

		// Token: 0x04000012 RID: 18
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

		// Token: 0x02000036 RID: 54
		[Nullable(0)]
		[ExcludeFromCodeCoverage]
		private readonly struct DynamicMethodDefinitionHandle
		{
			// Token: 0x06000170 RID: 368 RVA: 0x000078AC File Offset: 0x00005AAC
			public static AccessTools2.DynamicMethodDefinitionHandle? Create(string name, Type returnType, Type[] parameterTypes)
			{
				return (AccessTools2.Helper.DynamicMethodDefinitionCtor == null) ? null : new AccessTools2.DynamicMethodDefinitionHandle?(new AccessTools2.DynamicMethodDefinitionHandle(AccessTools2.Helper.DynamicMethodDefinitionCtor(name, returnType, parameterTypes)));
			}

			// Token: 0x06000171 RID: 369 RVA: 0x000078E2 File Offset: 0x00005AE2
			public DynamicMethodDefinitionHandle(object dynamicMethodDefinition)
			{
				this._dynamicMethodDefinition = dynamicMethodDefinition;
			}

			// Token: 0x06000172 RID: 370 RVA: 0x000078EC File Offset: 0x00005AEC
			public AccessTools2.ILGeneratorHandle? GetILGenerator()
			{
				return (AccessTools2.Helper.GetILGenerator == null) ? null : new AccessTools2.ILGeneratorHandle?(new AccessTools2.ILGeneratorHandle(AccessTools2.Helper.GetILGenerator(this._dynamicMethodDefinition)));
			}

			// Token: 0x06000173 RID: 371 RVA: 0x00007925 File Offset: 0x00005B25
			[NullableContext(2)]
			public MethodInfo Generate()
			{
				return (AccessTools2.Helper.Generate == null) ? null : AccessTools2.Helper.Generate(this._dynamicMethodDefinition);
			}

			// Token: 0x04000072 RID: 114
			private readonly object _dynamicMethodDefinition;
		}

		// Token: 0x02000037 RID: 55
		[Nullable(0)]
		[ExcludeFromCodeCoverage]
		private readonly struct ILGeneratorHandle
		{
			// Token: 0x06000174 RID: 372 RVA: 0x00007941 File Offset: 0x00005B41
			public ILGeneratorHandle(object ilGenerator)
			{
				this._ilGenerator = ilGenerator;
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0000794A File Offset: 0x00005B4A
			public void Emit(OpCode opcode)
			{
				AccessTools2.Helper.Emit1Delegate emit = AccessTools2.Helper.Emit1;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode);
				}
			}

			// Token: 0x06000176 RID: 374 RVA: 0x00007964 File Offset: 0x00005B64
			public void Emit(OpCode opcode, FieldInfo field)
			{
				AccessTools2.Helper.Emit2Delegate emit = AccessTools2.Helper.Emit2;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode, field);
				}
			}

			// Token: 0x06000177 RID: 375 RVA: 0x0000797F File Offset: 0x00005B7F
			public void Emit(OpCode opcode, Type type)
			{
				AccessTools2.Helper.Emit3Delegate emit = AccessTools2.Helper.Emit3;
				if (emit != null)
				{
					emit(this._ilGenerator, opcode, type);
				}
			}

			// Token: 0x04000073 RID: 115
			private readonly object _ilGenerator;
		}

		// Token: 0x02000038 RID: 56
		[NullableContext(0)]
		[ExcludeFromCodeCoverage]
		private static class Helper
		{
			// Token: 0x06000179 RID: 377 RVA: 0x00007A9C File Offset: 0x00005C9C
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

			// Token: 0x04000074 RID: 116
			[Nullable(2)]
			public static readonly AccessTools2.Helper.DynamicMethodDefinitionCtorDelegate DynamicMethodDefinitionCtor = AccessTools2.GetDeclaredConstructorDelegate<AccessTools2.Helper.DynamicMethodDefinitionCtorDelegate>("MonoMod.Utils.DynamicMethodDefinition", new Type[]
			{
				typeof(string),
				typeof(Type),
				typeof(Type[])
			}, true);

			// Token: 0x04000075 RID: 117
			[Nullable(2)]
			public static readonly AccessTools2.Helper.GetILGeneratorDelegate GetILGenerator = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.GetILGeneratorDelegate>("MonoMod.Utils.DynamicMethodDefinition:GetILGenerator", Type.EmptyTypes, null, true);

			// Token: 0x04000076 RID: 118
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit1Delegate Emit1 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit1Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode)
			}, null, true);

			// Token: 0x04000077 RID: 119
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit2Delegate Emit2 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit2Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode),
				typeof(FieldInfo)
			}, null, true);

			// Token: 0x04000078 RID: 120
			[Nullable(2)]
			public static readonly AccessTools2.Helper.Emit3Delegate Emit3 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit3Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[]
			{
				typeof(OpCode),
				typeof(Type)
			}, null, true);

			// Token: 0x04000079 RID: 121
			[Nullable(2)]
			public static readonly AccessTools2.Helper.GenerateDelegate Generate = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.GenerateDelegate>("MonoMod.Utils.DynamicMethodDefinition:Generate", Type.EmptyTypes, null, true);

			// Token: 0x0200004A RID: 74
			// (Invoke) Token: 0x060001A6 RID: 422
			public delegate object DynamicMethodDefinitionCtorDelegate(string name, Type returnType, Type[] parameterTypes);

			// Token: 0x0200004B RID: 75
			// (Invoke) Token: 0x060001AA RID: 426
			public delegate object GetILGeneratorDelegate(object instance);

			// Token: 0x0200004C RID: 76
			// (Invoke) Token: 0x060001AE RID: 430
			public delegate void Emit1Delegate(object instance, OpCode opcode);

			// Token: 0x0200004D RID: 77
			// (Invoke) Token: 0x060001B2 RID: 434
			public delegate void Emit2Delegate(object instance, OpCode opcode, FieldInfo field);

			// Token: 0x0200004E RID: 78
			// (Invoke) Token: 0x060001B6 RID: 438
			public delegate void Emit3Delegate(object instance, OpCode opcode, Type type);

			// Token: 0x0200004F RID: 79
			// (Invoke) Token: 0x060001BA RID: 442
			public delegate MethodInfo GenerateDelegate(object instance);
		}
	}
}
