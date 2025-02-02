using System;
using System.Reflection;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000063 RID: 99
	public abstract class MemberDefinition
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000C31D File Offset: 0x0000A51D
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000C325 File Offset: 0x0000A525
		public MemberTypeId Id { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000C32E File Offset: 0x0000A52E
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000C336 File Offset: 0x0000A536
		public MemberInfo MemberInfo { get; private set; }

		// Token: 0x060002ED RID: 749 RVA: 0x0000C33F File Offset: 0x0000A53F
		protected MemberDefinition(MemberInfo memberInfo, MemberTypeId id)
		{
			this.MemberInfo = memberInfo;
			this.Id = id;
		}

		// Token: 0x060002EE RID: 750
		public abstract Type GetMemberType();

		// Token: 0x060002EF RID: 751
		public abstract object GetValue(object target);
	}
}
