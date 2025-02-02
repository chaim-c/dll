using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200004D RID: 77
	internal class Vec2iBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000AA7C File Offset: 0x00008C7C
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			Vec2i vec2i = (Vec2i)value;
			writer.WriteFloat((float)vec2i.Item1);
			writer.WriteFloat((float)vec2i.Item2);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000AAAC File Offset: 0x00008CAC
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			int x = reader.ReadInt();
			int y = reader.ReadInt();
			return new Vec2i(x, y);
		}
	}
}
