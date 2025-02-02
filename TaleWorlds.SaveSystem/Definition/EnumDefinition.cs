using System;
using TaleWorlds.SaveSystem.Resolvers;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200005E RID: 94
	internal class EnumDefinition : TypeDefinitionBase
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		public EnumDefinition(Type type, SaveId saveId, IEnumResolver resolver) : base(type, saveId)
		{
			this.Resolver = resolver;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C0B5 File Offset: 0x0000A2B5
		public EnumDefinition(Type type, int saveId, IEnumResolver resolver) : this(type, new TypeSaveId(saveId), resolver)
		{
		}

		// Token: 0x040000E0 RID: 224
		public readonly IEnumResolver Resolver;
	}
}
