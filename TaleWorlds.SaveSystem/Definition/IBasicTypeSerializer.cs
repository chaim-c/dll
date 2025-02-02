using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000041 RID: 65
	public interface IBasicTypeSerializer
	{
		// Token: 0x06000250 RID: 592
		void Serialize(IWriter writer, object value);

		// Token: 0x06000251 RID: 593
		object Deserialize(IReader reader);
	}
}
