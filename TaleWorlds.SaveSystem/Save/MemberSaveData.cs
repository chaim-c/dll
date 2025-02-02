using System;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x0200002A RID: 42
	internal abstract class MemberSaveData : VariableSaveData
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006F31 File Offset: 0x00005131
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00006F39 File Offset: 0x00005139
		public ObjectSaveData ObjectSaveData { get; private set; }

		// Token: 0x06000172 RID: 370 RVA: 0x00006F42 File Offset: 0x00005142
		protected MemberSaveData(ObjectSaveData objectSaveData) : base(objectSaveData.Context)
		{
			this.ObjectSaveData = objectSaveData;
		}

		// Token: 0x06000173 RID: 371
		public abstract void Initialize(TypeDefinitionBase typeDefinition);

		// Token: 0x06000174 RID: 372
		public abstract void InitializeAsCustomStruct(int structId);
	}
}
