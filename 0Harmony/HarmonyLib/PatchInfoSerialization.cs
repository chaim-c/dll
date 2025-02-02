using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HarmonyLib
{
	// Token: 0x0200003D RID: 61
	internal static class PatchInfoSerialization
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000B0DC File Offset: 0x000092DC
		internal static byte[] Serialize(this PatchInfo patchInfo)
		{
			byte[] buffer;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				PatchInfoSerialization.binaryFormatter.Serialize(memoryStream, patchInfo);
				buffer = memoryStream.GetBuffer();
			}
			return buffer;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B120 File Offset: 0x00009320
		internal static PatchInfo Deserialize(byte[] bytes)
		{
			PatchInfo result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = (PatchInfo)PatchInfoSerialization.binaryFormatter.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B164 File Offset: 0x00009364
		internal static int PriorityComparer(object obj, int index, int priority)
		{
			Traverse traverse = Traverse.Create(obj);
			int value = traverse.Field("priority").GetValue<int>();
			int value2 = traverse.Field("index").GetValue<int>();
			if (priority != value)
			{
				return -priority.CompareTo(value);
			}
			return index.CompareTo(value2);
		}

		// Token: 0x0400009C RID: 156
		internal static readonly BinaryFormatter binaryFormatter = new BinaryFormatter
		{
			Binder = new PatchInfoSerialization.Binder()
		};

		// Token: 0x02000091 RID: 145
		private class Binder : SerializationBinder
		{
			// Token: 0x060004CC RID: 1228 RVA: 0x00016D9C File Offset: 0x00014F9C
			public override Type BindToType(string assemblyName, string typeName)
			{
				Type[] array = new Type[]
				{
					typeof(PatchInfo),
					typeof(Patch[]),
					typeof(Patch)
				};
				foreach (Type type in array)
				{
					if (typeName == type.FullName)
					{
						return type;
					}
				}
				return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
			}
		}
	}
}
