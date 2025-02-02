using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x02000052 RID: 82
	internal class Tools
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x00013778 File Offset: 0x00011978
		internal static Tools.TypeAndName TypColonName(string typeColonName)
		{
			if (typeColonName == null)
			{
				throw new ArgumentNullException("typeColonName");
			}
			string[] array = typeColonName.Split(new char[]
			{
				':'
			});
			if (array.Length != 2)
			{
				throw new ArgumentException(" must be specified as 'Namespace.Type1.Type2:MemberName", "typeColonName");
			}
			return new Tools.TypeAndName
			{
				type = AccessTools.TypeByName(array[0]),
				name = array[1]
			};
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000137E0 File Offset: 0x000119E0
		internal static void ValidateFieldType<F>(FieldInfo fieldInfo)
		{
			Type typeFromHandle = typeof(F);
			Type fieldType = fieldInfo.FieldType;
			if (typeFromHandle == fieldType)
			{
				return;
			}
			if (fieldType.IsEnum)
			{
				Type underlyingType = Enum.GetUnderlyingType(fieldType);
				if (typeFromHandle != underlyingType)
				{
					string str = "FieldRefAccess return type must be the same as FieldType or ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(54, 1);
					defaultInterpolatedStringHandler.AppendLiteral("FieldType's underlying integral type (");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(underlyingType);
					defaultInterpolatedStringHandler.AppendLiteral(") for enum types");
					throw new ArgumentException(str + defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
			else
			{
				if (fieldType.IsValueType)
				{
					throw new ArgumentException("FieldRefAccess return type must be the same as FieldType for value types");
				}
				if (!typeFromHandle.IsAssignableFrom(fieldType))
				{
					throw new ArgumentException("FieldRefAccess return type must be assignable from FieldType for reference types");
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001388C File Offset: 0x00011A8C
		internal static AccessTools.FieldRef<T, F> FieldRefAccess<T, F>(FieldInfo fieldInfo, bool needCastclass)
		{
			Tools.ValidateFieldType<F>(fieldInfo);
			Type typeFromHandle = typeof(T);
			Type declaringType = fieldInfo.DeclaringType;
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition("__refget_" + typeFromHandle.Name + "_fi_" + fieldInfo.Name, typeof(F).MakeByRefType(), new Type[]
			{
				typeFromHandle
			});
			ILGenerator ilgenerator = dynamicMethodDefinition.GetILGenerator();
			if (fieldInfo.IsStatic)
			{
				ilgenerator.Emit(OpCodes.Ldsflda, fieldInfo);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Ldarg_0);
				if (needCastclass)
				{
					ilgenerator.Emit(OpCodes.Castclass, declaringType);
				}
				ilgenerator.Emit(OpCodes.Ldflda, fieldInfo);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return (AccessTools.FieldRef<T, F>)dynamicMethodDefinition.Generate().CreateDelegate(typeof(AccessTools.FieldRef<T, F>));
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013954 File Offset: 0x00011B54
		internal static AccessTools.StructFieldRef<T, F> StructFieldRefAccess<T, F>(FieldInfo fieldInfo) where T : struct
		{
			Tools.ValidateFieldType<F>(fieldInfo);
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition("__refget_" + typeof(T).Name + "_struct_fi_" + fieldInfo.Name, typeof(F).MakeByRefType(), new Type[]
			{
				typeof(T).MakeByRefType()
			});
			ILGenerator ilgenerator = dynamicMethodDefinition.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldflda, fieldInfo);
			ilgenerator.Emit(OpCodes.Ret);
			return (AccessTools.StructFieldRef<T, F>)dynamicMethodDefinition.Generate().CreateDelegate(typeof(AccessTools.StructFieldRef<T, F>));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000139FC File Offset: 0x00011BFC
		internal static AccessTools.FieldRef<F> StaticFieldRefAccess<F>(FieldInfo fieldInfo)
		{
			if (!fieldInfo.IsStatic)
			{
				throw new ArgumentException("Field must be static");
			}
			Tools.ValidateFieldType<F>(fieldInfo);
			string str = "__refget_";
			Type declaringType = fieldInfo.DeclaringType;
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition(str + (((declaringType != null) ? declaringType.Name : null) ?? "null") + "_static_fi_" + fieldInfo.Name, typeof(F).MakeByRefType(), Array.Empty<Type>());
			ILGenerator ilgenerator = dynamicMethodDefinition.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldsflda, fieldInfo);
			ilgenerator.Emit(OpCodes.Ret);
			return (AccessTools.FieldRef<F>)dynamicMethodDefinition.Generate().CreateDelegate(typeof(AccessTools.FieldRef<F>));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00013AA4 File Offset: 0x00011CA4
		internal static FieldInfo GetInstanceField(Type type, string fieldName)
		{
			FieldInfo fieldInfo = AccessTools.Field(type, fieldName);
			if (fieldInfo == null)
			{
				throw new MissingFieldException(type.Name, fieldName);
			}
			if (fieldInfo.IsStatic)
			{
				throw new ArgumentException("Field must not be static");
			}
			return fieldInfo;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00013AE0 File Offset: 0x00011CE0
		internal static bool FieldRefNeedsClasscast(Type delegateInstanceType, Type declaringType)
		{
			bool flag = false;
			if (delegateInstanceType != declaringType)
			{
				flag = delegateInstanceType.IsAssignableFrom(declaringType);
				if (!flag && !declaringType.IsAssignableFrom(delegateInstanceType))
				{
					throw new ArgumentException("FieldDeclaringType must be assignable from or to T (FieldRefAccess instance type) - \"instanceOfT is FieldDeclaringType\" must be possible");
				}
			}
			return flag;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00013B18 File Offset: 0x00011D18
		internal static void ValidateStructField<T, F>(FieldInfo fieldInfo) where T : struct
		{
			if (fieldInfo.IsStatic)
			{
				throw new ArgumentException("Field must not be static");
			}
			if (fieldInfo.DeclaringType != typeof(T))
			{
				throw new ArgumentException("FieldDeclaringType must be T (StructFieldRefAccess instance type)");
			}
		}

		// Token: 0x040000EB RID: 235
		internal static readonly bool isWindows = Environment.OSVersion.Platform.Equals(PlatformID.Win32NT);

		// Token: 0x020001AE RID: 430
		internal struct TypeAndName
		{
			// Token: 0x04000235 RID: 565
			internal Type type;

			// Token: 0x04000236 RID: 566
			internal string name;
		}
	}
}
