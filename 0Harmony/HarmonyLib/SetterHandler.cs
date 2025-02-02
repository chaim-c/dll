using System;

namespace HarmonyLib
{
	// Token: 0x02000006 RID: 6
	// (Invoke) Token: 0x0600000A RID: 10
	[Obsolete("Use AccessTools.FieldRefAccess<T, S> for fields and AccessTools.MethodDelegate<Action<T, S>> for property setters")]
	public delegate void SetterHandler<in T, in S>(T source, S value);
}
