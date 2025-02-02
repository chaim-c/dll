using System;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace Mono.Cecil.PE
{
	// Token: 0x02000048 RID: 72
	internal sealed class Image
	{
		// Token: 0x06000219 RID: 537 RVA: 0x000098A6 File Offset: 0x00007AA6
		public Image()
		{
			this.counter = new Func<Table, int>(this.GetTableLength);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000098CD File Offset: 0x00007ACD
		public bool HasTable(Table table)
		{
			return this.GetTableLength(table) > 0;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000098D9 File Offset: 0x00007AD9
		public int GetTableLength(Table table)
		{
			return (int)this.TableHeap[table].Length;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000098EC File Offset: 0x00007AEC
		public int GetTableIndexSize(Table table)
		{
			if (this.GetTableLength(table) >= 65536)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009900 File Offset: 0x00007B00
		public int GetCodedIndexSize(CodedIndex coded_index)
		{
			int num = this.coded_index_sizes[(int)coded_index];
			if (num != 0)
			{
				return num;
			}
			return this.coded_index_sizes[(int)coded_index] = coded_index.GetSize(this.counter);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009934 File Offset: 0x00007B34
		public uint ResolveVirtualAddress(uint rva)
		{
			Section sectionAtVirtualAddress = this.GetSectionAtVirtualAddress(rva);
			if (sectionAtVirtualAddress == null)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.ResolveVirtualAddressInSection(rva, sectionAtVirtualAddress);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000995A File Offset: 0x00007B5A
		public uint ResolveVirtualAddressInSection(uint rva, Section section)
		{
			return rva + section.PointerToRawData - section.VirtualAddress;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000996C File Offset: 0x00007B6C
		public Section GetSection(string name)
		{
			foreach (Section section in this.Sections)
			{
				if (section.Name == name)
				{
					return section;
				}
			}
			return null;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000099A4 File Offset: 0x00007BA4
		public Section GetSectionAtVirtualAddress(uint rva)
		{
			foreach (Section section in this.Sections)
			{
				if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.SizeOfRawData)
				{
					return section;
				}
			}
			return null;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000099E8 File Offset: 0x00007BE8
		public ImageDebugDirectory GetDebugHeader(out byte[] header)
		{
			Section sectionAtVirtualAddress = this.GetSectionAtVirtualAddress(this.Debug.VirtualAddress);
			ByteBuffer byteBuffer = new ByteBuffer(sectionAtVirtualAddress.Data);
			byteBuffer.position = (int)(this.Debug.VirtualAddress - sectionAtVirtualAddress.VirtualAddress);
			ImageDebugDirectory result = new ImageDebugDirectory
			{
				Characteristics = byteBuffer.ReadInt32(),
				TimeDateStamp = byteBuffer.ReadInt32(),
				MajorVersion = byteBuffer.ReadInt16(),
				MinorVersion = byteBuffer.ReadInt16(),
				Type = byteBuffer.ReadInt32(),
				SizeOfData = byteBuffer.ReadInt32(),
				AddressOfRawData = byteBuffer.ReadInt32(),
				PointerToRawData = byteBuffer.ReadInt32()
			};
			if (result.SizeOfData == 0 || result.PointerToRawData == 0)
			{
				header = Empty<byte>.Array;
				return result;
			}
			byteBuffer.position = (int)((long)result.PointerToRawData - (long)((ulong)sectionAtVirtualAddress.PointerToRawData));
			header = new byte[result.SizeOfData];
			Buffer.BlockCopy(byteBuffer.buffer, byteBuffer.position, header, 0, header.Length);
			return result;
		}

		// Token: 0x04000335 RID: 821
		public ModuleKind Kind;

		// Token: 0x04000336 RID: 822
		public string RuntimeVersion;

		// Token: 0x04000337 RID: 823
		public TargetArchitecture Architecture;

		// Token: 0x04000338 RID: 824
		public ModuleCharacteristics Characteristics;

		// Token: 0x04000339 RID: 825
		public string FileName;

		// Token: 0x0400033A RID: 826
		public Section[] Sections;

		// Token: 0x0400033B RID: 827
		public Section MetadataSection;

		// Token: 0x0400033C RID: 828
		public uint EntryPointToken;

		// Token: 0x0400033D RID: 829
		public ModuleAttributes Attributes;

		// Token: 0x0400033E RID: 830
		public DataDirectory Debug;

		// Token: 0x0400033F RID: 831
		public DataDirectory Resources;

		// Token: 0x04000340 RID: 832
		public DataDirectory StrongName;

		// Token: 0x04000341 RID: 833
		public StringHeap StringHeap;

		// Token: 0x04000342 RID: 834
		public BlobHeap BlobHeap;

		// Token: 0x04000343 RID: 835
		public UserStringHeap UserStringHeap;

		// Token: 0x04000344 RID: 836
		public GuidHeap GuidHeap;

		// Token: 0x04000345 RID: 837
		public TableHeap TableHeap;

		// Token: 0x04000346 RID: 838
		private readonly int[] coded_index_sizes = new int[13];

		// Token: 0x04000347 RID: 839
		private readonly Func<Table, int> counter;
	}
}
