using System;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000040 RID: 64
	internal class BasicTypeDefinition : TypeDefinitionBase
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A8C5 File Offset: 0x00008AC5
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000A8CD File Offset: 0x00008ACD
		public IBasicTypeSerializer Serializer { get; private set; }

		// Token: 0x0600024F RID: 591 RVA: 0x0000A8D6 File Offset: 0x00008AD6
		public BasicTypeDefinition(Type type, int saveId, IBasicTypeSerializer serializer) : base(type, new TypeSaveId(saveId))
		{
			this.Serializer = serializer;
		}
	}
}
