using System;
using TaleWorlds.SaveSystem.Resolvers;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000069 RID: 105
	internal class StructDefinition : TypeDefinition
	{
		// Token: 0x0600032A RID: 810 RVA: 0x0000E4B7 File Offset: 0x0000C6B7
		public StructDefinition(Type type, int saveId) : this(type, saveId, null)
		{
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000E4C2 File Offset: 0x0000C6C2
		public StructDefinition(Type type, int saveId, IObjectResolver objectResolver) : base(type, saveId, objectResolver)
		{
		}
	}
}
