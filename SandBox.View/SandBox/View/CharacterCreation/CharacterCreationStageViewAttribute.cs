using System;

namespace SandBox.View.CharacterCreation
{
	// Token: 0x0200005C RID: 92
	public sealed class CharacterCreationStageViewAttribute : Attribute
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00022C69 File Offset: 0x00020E69
		public CharacterCreationStageViewAttribute(Type stageType)
		{
			this.StageType = stageType;
		}

		// Token: 0x04000246 RID: 582
		public readonly Type StageType;
	}
}
