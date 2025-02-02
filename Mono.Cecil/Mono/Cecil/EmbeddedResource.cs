using System;
using System.IO;

namespace Mono.Cecil
{
	// Token: 0x020000C2 RID: 194
	public sealed class EmbeddedResource : Resource
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00019836 File Offset: 0x00017A36
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.Embedded;
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00019839 File Offset: 0x00017A39
		public EmbeddedResource(string name, ManifestResourceAttributes attributes, byte[] data) : base(name, attributes)
		{
			this.data = data;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001984A File Offset: 0x00017A4A
		public EmbeddedResource(string name, ManifestResourceAttributes attributes, Stream stream) : base(name, attributes)
		{
			this.stream = stream;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001985B File Offset: 0x00017A5B
		internal EmbeddedResource(string name, ManifestResourceAttributes attributes, uint offset, MetadataReader reader) : base(name, attributes)
		{
			this.offset = new uint?(offset);
			this.reader = reader;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001987C File Offset: 0x00017A7C
		public Stream GetResourceStream()
		{
			if (this.stream != null)
			{
				return this.stream;
			}
			if (this.data != null)
			{
				return new MemoryStream(this.data);
			}
			if (this.offset != null)
			{
				return this.reader.GetManagedResourceStream(this.offset.Value);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000198D8 File Offset: 0x00017AD8
		public byte[] GetResourceData()
		{
			if (this.stream != null)
			{
				return EmbeddedResource.ReadStream(this.stream);
			}
			if (this.data != null)
			{
				return this.data;
			}
			if (this.offset != null)
			{
				return this.reader.GetManagedResourceStream(this.offset.Value).ToArray();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00019938 File Offset: 0x00017B38
		private static byte[] ReadStream(Stream stream)
		{
			int num3;
			if (stream.CanSeek)
			{
				int num = (int)stream.Length;
				byte[] array = new byte[num];
				int num2 = 0;
				while ((num3 = stream.Read(array, num2, num - num2)) > 0)
				{
					num2 += num3;
				}
				return array;
			}
			byte[] array2 = new byte[8192];
			MemoryStream memoryStream = new MemoryStream();
			while ((num3 = stream.Read(array2, 0, array2.Length)) > 0)
			{
				memoryStream.Write(array2, 0, num3);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0400049A RID: 1178
		private readonly MetadataReader reader;

		// Token: 0x0400049B RID: 1179
		private uint? offset;

		// Token: 0x0400049C RID: 1180
		private byte[] data;

		// Token: 0x0400049D RID: 1181
		private Stream stream;
	}
}
