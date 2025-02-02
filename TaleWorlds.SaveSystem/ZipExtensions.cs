using System;
using System.IO;
using System.IO.Compression;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000025 RID: 37
	internal static class ZipExtensions
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000062BC File Offset: 0x000044BC
		public static void FillFrom(this ZipArchiveEntry entry, byte[] data)
		{
			using (Stream stream = entry.Open())
			{
				stream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000062F8 File Offset: 0x000044F8
		public static void FillFrom(this ZipArchiveEntry entry, BinaryWriter writer)
		{
			using (Stream stream = entry.Open())
			{
				byte[] data = writer.Data;
				stream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000633C File Offset: 0x0000453C
		public static BinaryReader GetBinaryReader(this ZipArchiveEntry entry)
		{
			BinaryReader result = null;
			using (Stream stream = entry.Open())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					result = new BinaryReader(memoryStream.ToArray());
				}
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000063A0 File Offset: 0x000045A0
		public static byte[] GetBinaryData(this ZipArchiveEntry entry)
		{
			byte[] result = null;
			using (Stream stream = entry.Open())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}
	}
}
