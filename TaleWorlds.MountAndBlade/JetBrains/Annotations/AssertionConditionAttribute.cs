using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000DF RID: 223
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class AssertionConditionAttribute : Attribute
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x0000F167 File Offset: 0x0000D367
		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			this.ConditionType = conditionType;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0000F176 File Offset: 0x0000D376
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0000F17E File Offset: 0x0000D37E
		public AssertionConditionType ConditionType { get; private set; }
	}
}
