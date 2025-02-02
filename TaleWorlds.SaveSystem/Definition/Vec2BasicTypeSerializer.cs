﻿using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200004C RID: 76
	internal class Vec2BasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000AA4C File Offset: 0x00008C4C
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			Vec2 vec = (Vec2)value;
			writer.WriteVec2(vec);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000AA67 File Offset: 0x00008C67
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadVec2();
		}
	}
}
