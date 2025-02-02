using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000040 RID: 64
	public interface ISerializableObject
	{
		// Token: 0x0600020E RID: 526
		void DeserializeFrom(IReader reader);

		// Token: 0x0600020F RID: 527
		void SerializeTo(IWriter writer);
	}
}
