using System;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x0200002C RID: 44
	internal class PropertySaveData : MemberSaveData
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007777 File Offset: 0x00005977
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000777F File Offset: 0x0000597F
		public PropertyDefinition PropertyDefinition { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007788 File Offset: 0x00005988
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00007790 File Offset: 0x00005990
		public MemberTypeId SaveId { get; private set; }

		// Token: 0x0600018F RID: 399 RVA: 0x00007799 File Offset: 0x00005999
		public PropertySaveData(ObjectSaveData objectSaveData, PropertyDefinition propertyDefinition, MemberTypeId saveId) : base(objectSaveData)
		{
			this.PropertyDefinition = propertyDefinition;
			this.SaveId = saveId;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000077B0 File Offset: 0x000059B0
		public override void Initialize(TypeDefinitionBase typeDefinition)
		{
			object value = this.PropertyDefinition.GetValue(base.ObjectSaveData.Target);
			base.InitializeData(this.SaveId, this.PropertyDefinition.PropertyInfo.PropertyType, typeDefinition, value);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000077F2 File Offset: 0x000059F2
		public override void InitializeAsCustomStruct(int structId)
		{
			base.InitializeDataAsCustomStruct(this.SaveId, structId, base.TypeDefinition);
		}
	}
}
