using System;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.ObjectSystem
{
	// Token: 0x02000011 RID: 17
	public class SaveableObjectSystemTypeDefiner : SaveableTypeDefiner
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000044BA File Offset: 0x000026BA
		public SaveableObjectSystemTypeDefiner() : base(10000)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000044C7 File Offset: 0x000026C7
		protected override void DefineBasicTypes()
		{
			base.DefineBasicTypes();
			base.AddBasicTypeDefinition(typeof(MBGUID), 1005, new MBGUIDBasicTypeSerializer());
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000044E9 File Offset: 0x000026E9
		protected override void DefineClassTypes()
		{
			base.AddClassDefinition(typeof(MBObjectBase), 34, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000044FE File Offset: 0x000026FE
		protected override void DefineStructTypes()
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004500 File Offset: 0x00002700
		protected override void DefineEnumTypes()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004502 File Offset: 0x00002702
		protected override void DefineInterfaceTypes()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004504 File Offset: 0x00002704
		protected override void DefineRootClassTypes()
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004506 File Offset: 0x00002706
		protected override void DefineGenericClassDefinitions()
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004508 File Offset: 0x00002708
		protected override void DefineGenericStructDefinitions()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000450A File Offset: 0x0000270A
		protected override void DefineContainerDefinitions()
		{
		}
	}
}
