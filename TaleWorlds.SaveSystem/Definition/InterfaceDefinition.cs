using System;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000062 RID: 98
	internal class InterfaceDefinition : TypeDefinitionBase
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x0000C304 File Offset: 0x0000A504
		public InterfaceDefinition(Type type, SaveId saveId) : base(type, saveId)
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C30E File Offset: 0x0000A50E
		public InterfaceDefinition(Type type, int saveId) : base(type, new TypeSaveId(saveId))
		{
		}
	}
}
