using System;

namespace HarmonyLib
{
	// Token: 0x02000005 RID: 5
	// (Invoke) Token: 0x06000006 RID: 6
	[Obsolete("Use AccessTools.FieldRefAccess<T, S> for fields and AccessTools.MethodDelegate<Func<T, S>> for property getters")]
	public delegate S GetterHandler<in T, out S>(T source);
}
