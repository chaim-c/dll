using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000047 RID: 71
	public static class AccessToolsExtensions
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000FA42 File Offset: 0x0000DC42
		public static IEnumerable<Type> InnerTypes(this Type type)
		{
			return AccessTools.InnerTypes(type);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000FA4A File Offset: 0x0000DC4A
		public static T FindIncludingBaseTypes<T>(this Type type, Func<Type, T> func) where T : class
		{
			return AccessTools.FindIncludingBaseTypes<T>(type, func);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000FA53 File Offset: 0x0000DC53
		public static T FindIncludingInnerTypes<T>(this Type type, Func<Type, T> func) where T : class
		{
			return AccessTools.FindIncludingInnerTypes<T>(type, func);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public static FieldInfo DeclaredField(this Type type, string name)
		{
			return AccessTools.DeclaredField(type, name);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000FA65 File Offset: 0x0000DC65
		public static FieldInfo Field(this Type type, string name)
		{
			return AccessTools.Field(type, name);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000FA6E File Offset: 0x0000DC6E
		public static FieldInfo DeclaredField(this Type type, int idx)
		{
			return AccessTools.DeclaredField(type, idx);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000FA77 File Offset: 0x0000DC77
		public static PropertyInfo DeclaredProperty(this Type type, string name)
		{
			return AccessTools.DeclaredProperty(type, name);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000FA80 File Offset: 0x0000DC80
		public static PropertyInfo DeclaredIndexer(this Type type, Type[] parameters = null)
		{
			return AccessTools.DeclaredIndexer(type, parameters);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000FA89 File Offset: 0x0000DC89
		public static MethodInfo DeclaredPropertyGetter(this Type type, string name)
		{
			return AccessTools.DeclaredPropertyGetter(type, name);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000FA92 File Offset: 0x0000DC92
		public static MethodInfo DeclaredIndexerGetter(this Type type, Type[] parameters = null)
		{
			return AccessTools.DeclaredIndexerGetter(type, parameters);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000FA9B File Offset: 0x0000DC9B
		public static MethodInfo DeclaredPropertySetter(this Type type, string name)
		{
			return AccessTools.DeclaredPropertySetter(type, name);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public static MethodInfo DeclaredIndexerSetter(this Type type, Type[] parameters)
		{
			return AccessTools.DeclaredIndexerSetter(type, parameters);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000FAAD File Offset: 0x0000DCAD
		public static PropertyInfo Property(this Type type, string name)
		{
			return AccessTools.Property(type, name);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000FAB6 File Offset: 0x0000DCB6
		public static PropertyInfo Indexer(this Type type, Type[] parameters = null)
		{
			return AccessTools.Indexer(type, parameters);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public static MethodInfo PropertyGetter(this Type type, string name)
		{
			return AccessTools.PropertyGetter(type, name);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public static MethodInfo IndexerGetter(this Type type, Type[] parameters = null)
		{
			return AccessTools.IndexerGetter(type, parameters);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000FAD1 File Offset: 0x0000DCD1
		public static MethodInfo PropertySetter(this Type type, string name)
		{
			return AccessTools.PropertySetter(type, name);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000FADA File Offset: 0x0000DCDA
		public static MethodInfo IndexerSetter(this Type type, Type[] parameters = null)
		{
			return AccessTools.IndexerSetter(type, parameters);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000FAE3 File Offset: 0x0000DCE3
		public static MethodInfo DeclaredMethod(this Type type, string name, Type[] parameters = null, Type[] generics = null)
		{
			return AccessTools.DeclaredMethod(type, name, parameters, generics);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000FAEE File Offset: 0x0000DCEE
		public static MethodInfo Method(this Type type, string name, Type[] parameters = null, Type[] generics = null)
		{
			return AccessTools.Method(type, name, parameters, generics);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000FAF9 File Offset: 0x0000DCF9
		public static List<string> GetMethodNames(this Type type)
		{
			return AccessTools.GetMethodNames(type);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000FB01 File Offset: 0x0000DD01
		public static List<string> GetFieldNames(this Type type)
		{
			return AccessTools.GetFieldNames(type);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000FB09 File Offset: 0x0000DD09
		public static List<string> GetPropertyNames(this Type type)
		{
			return AccessTools.GetPropertyNames(type);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000FB11 File Offset: 0x0000DD11
		public static ConstructorInfo DeclaredConstructor(this Type type, Type[] parameters = null, bool searchForStatic = false)
		{
			return AccessTools.DeclaredConstructor(type, parameters, searchForStatic);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000FB1B File Offset: 0x0000DD1B
		public static ConstructorInfo Constructor(this Type type, Type[] parameters = null, bool searchForStatic = false)
		{
			return AccessTools.Constructor(type, parameters, searchForStatic);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000FB25 File Offset: 0x0000DD25
		public static List<ConstructorInfo> GetDeclaredConstructors(this Type type, bool? searchForStatic = null)
		{
			return AccessTools.GetDeclaredConstructors(type, searchForStatic);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000FB2E File Offset: 0x0000DD2E
		public static List<MethodInfo> GetDeclaredMethods(this Type type)
		{
			return AccessTools.GetDeclaredMethods(type);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000FB36 File Offset: 0x0000DD36
		public static List<PropertyInfo> GetDeclaredProperties(this Type type)
		{
			return AccessTools.GetDeclaredProperties(type);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000FB3E File Offset: 0x0000DD3E
		public static List<FieldInfo> GetDeclaredFields(this Type type)
		{
			return AccessTools.GetDeclaredFields(type);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000FB46 File Offset: 0x0000DD46
		public static Type Inner(this Type type, string name)
		{
			return AccessTools.Inner(type, name);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000FB4F File Offset: 0x0000DD4F
		public static Type FirstInner(this Type type, Func<Type, bool> predicate)
		{
			return AccessTools.FirstInner(type, predicate);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000FB58 File Offset: 0x0000DD58
		public static MethodInfo FirstMethod(this Type type, Func<MethodInfo, bool> predicate)
		{
			return AccessTools.FirstMethod(type, predicate);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000FB61 File Offset: 0x0000DD61
		public static ConstructorInfo FirstConstructor(this Type type, Func<ConstructorInfo, bool> predicate)
		{
			return AccessTools.FirstConstructor(type, predicate);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000FB6A File Offset: 0x0000DD6A
		public static PropertyInfo FirstProperty(this Type type, Func<PropertyInfo, bool> predicate)
		{
			return AccessTools.FirstProperty(type, predicate);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000FB73 File Offset: 0x0000DD73
		public static AccessTools.FieldRef<object, F> FieldRefAccess<F>(this Type type, string fieldName)
		{
			return AccessTools.FieldRefAccess<F>(type, fieldName);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000FB7C File Offset: 0x0000DD7C
		public static ref F StaticFieldRefAccess<F>(this Type type, string fieldName)
		{
			return AccessTools.StaticFieldRefAccess<F>(type, fieldName);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000FB85 File Offset: 0x0000DD85
		public static void ThrowMissingMemberException(this Type type, params string[] names)
		{
			AccessTools.ThrowMissingMemberException(type, names);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000FB8E File Offset: 0x0000DD8E
		public static object GetDefaultValue(this Type type)
		{
			return AccessTools.GetDefaultValue(type);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000FB96 File Offset: 0x0000DD96
		public static object CreateInstance(this Type type)
		{
			return AccessTools.CreateInstance(type);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000FB9E File Offset: 0x0000DD9E
		public static bool IsStruct(this Type type)
		{
			return AccessTools.IsStruct(type);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000FBA6 File Offset: 0x0000DDA6
		public static bool IsClass(this Type type)
		{
			return AccessTools.IsClass(type);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000FBAE File Offset: 0x0000DDAE
		public static bool IsValue(this Type type)
		{
			return AccessTools.IsValue(type);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000FBB6 File Offset: 0x0000DDB6
		public static bool IsInteger(this Type type)
		{
			return AccessTools.IsInteger(type);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000FBBE File Offset: 0x0000DDBE
		public static bool IsFloatingPoint(this Type type)
		{
			return AccessTools.IsFloatingPoint(type);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000FBC6 File Offset: 0x0000DDC6
		public static bool IsNumber(this Type type)
		{
			return AccessTools.IsNumber(type);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000FBCE File Offset: 0x0000DDCE
		public static bool IsVoid(this Type type)
		{
			return AccessTools.IsVoid(type);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000FBD6 File Offset: 0x0000DDD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsStatic(this Type type)
		{
			return AccessTools.IsStatic(type);
		}
	}
}
