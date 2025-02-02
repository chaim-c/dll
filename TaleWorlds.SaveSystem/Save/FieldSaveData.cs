using System;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x02000028 RID: 40
	internal class FieldSaveData : MemberSaveData
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006E9C File Offset: 0x0000509C
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006EA4 File Offset: 0x000050A4
		public FieldDefinition FieldDefinition { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006EAD File Offset: 0x000050AD
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00006EB5 File Offset: 0x000050B5
		public MemberTypeId SaveId { get; private set; }

		// Token: 0x06000168 RID: 360 RVA: 0x00006EBE File Offset: 0x000050BE
		public FieldSaveData(ObjectSaveData objectSaveData, FieldDefinition fieldDefinition, MemberTypeId saveId) : base(objectSaveData)
		{
			this.FieldDefinition = fieldDefinition;
			this.SaveId = saveId;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006ED8 File Offset: 0x000050D8
		public override void Initialize(TypeDefinitionBase typeDefinition)
		{
			object value = this.FieldDefinition.GetValue(base.ObjectSaveData.Target);
			Type fieldType = this.FieldDefinition.FieldInfo.FieldType;
			base.InitializeData(this.SaveId, fieldType, typeDefinition, value);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006F1C File Offset: 0x0000511C
		public override void InitializeAsCustomStruct(int structId)
		{
			base.InitializeDataAsCustomStruct(this.SaveId, structId, base.TypeDefinition);
		}
	}
}
