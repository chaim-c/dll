using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000024 RID: 36
	internal static class TypeExtensions
	{
		// Token: 0x0600013F RID: 319 RVA: 0x000061F8 File Offset: 0x000043F8
		internal static bool IsContainer(this Type type)
		{
			ContainerType containerType;
			return type.IsContainer(out containerType);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006210 File Offset: 0x00004410
		internal static bool IsContainer(this Type type, out ContainerType containerType)
		{
			containerType = ContainerType.None;
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(Dictionary<, >))
				{
					containerType = ContainerType.Dictionary;
					return true;
				}
				if (genericTypeDefinition == typeof(List<>))
				{
					containerType = ContainerType.List;
					return true;
				}
				if (genericTypeDefinition == typeof(MBList<>))
				{
					containerType = ContainerType.CustomList;
					return true;
				}
				if (genericTypeDefinition == typeof(MBReadOnlyList<>))
				{
					containerType = ContainerType.CustomReadOnlyList;
					return true;
				}
				if (genericTypeDefinition == typeof(Queue<>))
				{
					containerType = ContainerType.Queue;
					return true;
				}
			}
			else if (type.IsArray)
			{
				containerType = ContainerType.Array;
				return true;
			}
			return false;
		}
	}
}
