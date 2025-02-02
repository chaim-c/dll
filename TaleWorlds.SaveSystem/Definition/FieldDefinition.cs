using System;
using System.Reflection;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200005F RID: 95
	public class FieldDefinition : MemberDefinition
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000C0C5 File Offset: 0x0000A2C5
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000C0CD File Offset: 0x0000A2CD
		public FieldInfo FieldInfo { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000C0DE File Offset: 0x0000A2DE
		public SaveableFieldAttribute SaveableFieldAttribute { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000C0E7 File Offset: 0x0000A2E7
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000C0EF File Offset: 0x0000A2EF
		public GetFieldValueDelegate GetFieldValueMethod { get; private set; }

		// Token: 0x060002D9 RID: 729 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public FieldDefinition(FieldInfo fieldInfo, MemberTypeId id) : base(fieldInfo, id)
		{
			this.FieldInfo = fieldInfo;
			this.SaveableFieldAttribute = fieldInfo.GetCustomAttribute<SaveableFieldAttribute>();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000C115 File Offset: 0x0000A315
		public override Type GetMemberType()
		{
			return this.FieldInfo.FieldType;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000C124 File Offset: 0x0000A324
		public override object GetValue(object target)
		{
			object result;
			if (this.GetFieldValueMethod != null)
			{
				result = this.GetFieldValueMethod(target);
			}
			else
			{
				result = this.FieldInfo.GetValue(target);
			}
			return result;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C156 File Offset: 0x0000A356
		public void InitializeForAutoGeneration(GetFieldValueDelegate getFieldValueMethod)
		{
			this.GetFieldValueMethod = getFieldValueMethod;
		}
	}
}
