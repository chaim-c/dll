using System;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x02000027 RID: 39
	internal class ElementSaveData : VariableSaveData
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006DF7 File Offset: 0x00004FF7
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00006DFF File Offset: 0x00004FFF
		public object ElementValue { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006E08 File Offset: 0x00005008
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00006E10 File Offset: 0x00005010
		public int ElementIndex { get; private set; }

		// Token: 0x06000163 RID: 355 RVA: 0x00006E1C File Offset: 0x0000501C
		public ElementSaveData(ContainerSaveData containerSaveData, object value, int index) : base(containerSaveData.Context)
		{
			this.ElementValue = value;
			this.ElementIndex = index;
			if (value == null)
			{
				base.InitializeDataAsNullObject(MemberTypeId.Invalid);
				return;
			}
			TypeDefinitionBase typeDefinition = containerSaveData.Context.DefinitionContext.GetTypeDefinition(value.GetType());
			TypeDefinition typeDefinition2 = typeDefinition as TypeDefinition;
			if (typeDefinition2 != null && !typeDefinition2.IsClassDefinition)
			{
				base.InitializeDataAsCustomStruct(MemberTypeId.Invalid, index, typeDefinition);
				return;
			}
			base.InitializeData(MemberTypeId.Invalid, value.GetType(), typeDefinition, value);
		}
	}
}
