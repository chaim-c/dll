using System;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200006C RID: 108
	public class TypeDefinitionBase
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000EA88 File Offset: 0x0000CC88
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000EA90 File Offset: 0x0000CC90
		public SaveId SaveId { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000EA99 File Offset: 0x0000CC99
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000EAA1 File Offset: 0x0000CCA1
		public Type Type { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000EAAA File Offset: 0x0000CCAA
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000EAB2 File Offset: 0x0000CCB2
		public byte TypeLevel { get; private set; }

		// Token: 0x06000350 RID: 848 RVA: 0x0000EABB File Offset: 0x0000CCBB
		protected TypeDefinitionBase(Type type, SaveId saveId)
		{
			this.Type = type;
			this.SaveId = saveId;
			this.TypeLevel = TypeDefinitionBase.GetClassLevel(type);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public static byte GetClassLevel(Type type)
		{
			byte b = 1;
			if (type.IsClass)
			{
				Type type2 = type;
				while (type2 != typeof(object))
				{
					b += 1;
					type2 = type2.BaseType;
				}
			}
			return b;
		}
	}
}
