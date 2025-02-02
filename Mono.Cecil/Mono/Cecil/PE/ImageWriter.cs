using System;
using System.IO;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace Mono.Cecil.PE
{
	// Token: 0x0200004A RID: 74
	internal sealed class ImageWriter : BinaryStreamWriter
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000A638 File Offset: 0x00008838
		private ImageWriter(ModuleDefinition module, MetadataBuilder metadata, Stream stream) : base(stream)
		{
			this.module = module;
			this.metadata = metadata;
			this.pe64 = (module.Architecture == TargetArchitecture.AMD64 || module.Architecture == TargetArchitecture.IA64);
			this.has_reloc = (module.Architecture == TargetArchitecture.I386);
			this.GetDebugHeader();
			this.GetWin32Resources();
			this.text_map = this.BuildTextMap();
			this.sections = (this.has_reloc ? 2 : 1);
			this.time_stamp = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A6D8 File Offset: 0x000088D8
		private void GetDebugHeader()
		{
			ISymbolWriter symbol_writer = this.metadata.symbol_writer;
			if (symbol_writer == null)
			{
				return;
			}
			if (!symbol_writer.GetDebugHeader(out this.debug_directory, out this.debug_data))
			{
				this.debug_data = Empty<byte>.Array;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A714 File Offset: 0x00008914
		private void GetWin32Resources()
		{
			Section imageResourceSection = this.GetImageResourceSection();
			if (imageResourceSection == null)
			{
				return;
			}
			byte[] array = new byte[imageResourceSection.Data.Length];
			Buffer.BlockCopy(imageResourceSection.Data, 0, array, 0, imageResourceSection.Data.Length);
			this.win32_resources = new ByteBuffer(array);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A75C File Offset: 0x0000895C
		private Section GetImageResourceSection()
		{
			if (!this.module.HasImage)
			{
				return null;
			}
			return this.module.Image.GetSection(".rsrc");
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A784 File Offset: 0x00008984
		public static ImageWriter CreateWriter(ModuleDefinition module, MetadataBuilder metadata, Stream stream)
		{
			ImageWriter imageWriter = new ImageWriter(module, metadata, stream);
			imageWriter.BuildSections();
			return imageWriter;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A7A4 File Offset: 0x000089A4
		private void BuildSections()
		{
			bool flag = this.win32_resources != null;
			if (flag)
			{
				this.sections += 1;
			}
			this.text = this.CreateSection(".text", this.text_map.GetLength(), null);
			Section previous = this.text;
			if (flag)
			{
				this.rsrc = this.CreateSection(".rsrc", (uint)this.win32_resources.length, previous);
				this.PatchWin32Resources(this.win32_resources);
				previous = this.rsrc;
			}
			if (this.has_reloc)
			{
				this.reloc = this.CreateSection(".reloc", 12U, previous);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A844 File Offset: 0x00008A44
		private Section CreateSection(string name, uint size, Section previous)
		{
			return new Section
			{
				Name = name,
				VirtualAddress = ((previous != null) ? (previous.VirtualAddress + ImageWriter.Align(previous.VirtualSize, 8192U)) : 8192U),
				VirtualSize = size,
				PointerToRawData = ((previous != null) ? (previous.PointerToRawData + previous.SizeOfRawData) : ImageWriter.Align(this.GetHeaderSize(), 512U)),
				SizeOfRawData = ImageWriter.Align(size, 512U)
			};
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A8C6 File Offset: 0x00008AC6
		private static uint Align(uint value, uint align)
		{
			align -= 1U;
			return value + align & ~align;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A958 File Offset: 0x00008B58
		private void WriteDOSHeader()
		{
			this.Write(new byte[]
			{
				77,
				90,
				144,
				0,
				3,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				byte.MaxValue,
				byte.MaxValue,
				0,
				0,
				184,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				64,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				128,
				0,
				0,
				0,
				14,
				31,
				186,
				14,
				0,
				180,
				9,
				205,
				33,
				184,
				1,
				76,
				205,
				33,
				84,
				104,
				105,
				115,
				32,
				112,
				114,
				111,
				103,
				114,
				97,
				109,
				32,
				99,
				97,
				110,
				110,
				111,
				116,
				32,
				98,
				101,
				32,
				114,
				117,
				110,
				32,
				105,
				110,
				32,
				68,
				79,
				83,
				32,
				109,
				111,
				100,
				101,
				46,
				13,
				13,
				10,
				36,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A975 File Offset: 0x00008B75
		private ushort SizeOfOptionalHeader()
		{
			return (!this.pe64) ? 224 : 240;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A98C File Offset: 0x00008B8C
		private void WritePEFileHeader()
		{
			base.WriteUInt32(17744U);
			base.WriteUInt16(this.GetMachine());
			base.WriteUInt16(this.sections);
			base.WriteUInt32(this.time_stamp);
			base.WriteUInt32(0U);
			base.WriteUInt32(0U);
			base.WriteUInt16(this.SizeOfOptionalHeader());
			ushort num = (ushort)(2 | ((!this.pe64) ? 256 : 32));
			if (this.module.Kind == ModuleKind.Dll || this.module.Kind == ModuleKind.NetModule)
			{
				num |= 8192;
			}
			base.WriteUInt16(num);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000AA24 File Offset: 0x00008C24
		private ushort GetMachine()
		{
			switch (this.module.Architecture)
			{
			case TargetArchitecture.I386:
				return 332;
			case TargetArchitecture.AMD64:
				return 34404;
			case TargetArchitecture.IA64:
				return 512;
			case TargetArchitecture.ARMv7:
				return 452;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000AA72 File Offset: 0x00008C72
		private Section LastSection()
		{
			if (this.reloc != null)
			{
				return this.reloc;
			}
			if (this.rsrc != null)
			{
				return this.rsrc;
			}
			return this.text;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AA98 File Offset: 0x00008C98
		private void WriteOptionalHeaders()
		{
			base.WriteUInt16((!this.pe64) ? 267 : 523);
			base.WriteByte(8);
			base.WriteByte(0);
			base.WriteUInt32(this.text.SizeOfRawData);
			base.WriteUInt32(((this.reloc != null) ? this.reloc.SizeOfRawData : 0U) + ((this.rsrc != null) ? this.rsrc.SizeOfRawData : 0U));
			base.WriteUInt32(0U);
			Range range = this.text_map.GetRange(TextSegment.StartupStub);
			base.WriteUInt32((range.Length > 0U) ? range.Start : 0U);
			base.WriteUInt32(8192U);
			if (!this.pe64)
			{
				base.WriteUInt32(0U);
				base.WriteUInt32(4194304U);
			}
			else
			{
				base.WriteUInt64(4194304UL);
			}
			base.WriteUInt32(8192U);
			base.WriteUInt32(512U);
			base.WriteUInt16(4);
			base.WriteUInt16(0);
			base.WriteUInt16(0);
			base.WriteUInt16(0);
			base.WriteUInt16(4);
			base.WriteUInt16(0);
			base.WriteUInt32(0U);
			Section section = this.LastSection();
			base.WriteUInt32(section.VirtualAddress + ImageWriter.Align(section.VirtualSize, 8192U));
			base.WriteUInt32(this.text.PointerToRawData);
			base.WriteUInt32(0U);
			base.WriteUInt16(this.GetSubSystem());
			base.WriteUInt16((ushort)this.module.Characteristics);
			if (!this.pe64)
			{
				base.WriteUInt32(1048576U);
				base.WriteUInt32(4096U);
				base.WriteUInt32(1048576U);
				base.WriteUInt32(4096U);
			}
			else
			{
				base.WriteUInt64(1048576UL);
				base.WriteUInt64(4096UL);
				base.WriteUInt64(1048576UL);
				base.WriteUInt64(4096UL);
			}
			base.WriteUInt32(0U);
			base.WriteUInt32(16U);
			this.WriteZeroDataDirectory();
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.ImportDirectory));
			if (this.rsrc != null)
			{
				base.WriteUInt32(this.rsrc.VirtualAddress);
				base.WriteUInt32(this.rsrc.VirtualSize);
			}
			else
			{
				this.WriteZeroDataDirectory();
			}
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			base.WriteUInt32((this.reloc != null) ? this.reloc.VirtualAddress : 0U);
			base.WriteUInt32((this.reloc != null) ? this.reloc.VirtualSize : 0U);
			if (this.text_map.GetLength(TextSegment.DebugDirectory) > 0)
			{
				base.WriteUInt32(this.text_map.GetRVA(TextSegment.DebugDirectory));
				base.WriteUInt32(28U);
			}
			else
			{
				this.WriteZeroDataDirectory();
			}
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.ImportAddressTable));
			this.WriteZeroDataDirectory();
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.CLIHeader));
			this.WriteZeroDataDirectory();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AD9A File Offset: 0x00008F9A
		private void WriteZeroDataDirectory()
		{
			base.WriteUInt32(0U);
			base.WriteUInt32(0U);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000ADAC File Offset: 0x00008FAC
		private ushort GetSubSystem()
		{
			switch (this.module.Kind)
			{
			case ModuleKind.Dll:
			case ModuleKind.Console:
			case ModuleKind.NetModule:
				return 3;
			case ModuleKind.Windows:
				return 2;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		private void WriteSectionHeaders()
		{
			this.WriteSection(this.text, 1610612768U);
			if (this.rsrc != null)
			{
				this.WriteSection(this.rsrc, 1073741888U);
			}
			if (this.reloc != null)
			{
				this.WriteSection(this.reloc, 1107296320U);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AE38 File Offset: 0x00009038
		private void WriteSection(Section section, uint characteristics)
		{
			byte[] array = new byte[8];
			string name = section.Name;
			for (int i = 0; i < name.Length; i++)
			{
				array[i] = (byte)name[i];
			}
			base.WriteBytes(array);
			base.WriteUInt32(section.VirtualSize);
			base.WriteUInt32(section.VirtualAddress);
			base.WriteUInt32(section.SizeOfRawData);
			base.WriteUInt32(section.PointerToRawData);
			base.WriteUInt32(0U);
			base.WriteUInt32(0U);
			base.WriteUInt16(0);
			base.WriteUInt16(0);
			base.WriteUInt32(characteristics);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000AEC9 File Offset: 0x000090C9
		private void MoveTo(uint pointer)
		{
			this.BaseStream.Seek((long)((ulong)pointer), SeekOrigin.Begin);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AEDA File Offset: 0x000090DA
		private void MoveToRVA(Section section, uint rva)
		{
			this.BaseStream.Seek((long)((ulong)(section.PointerToRawData + rva - section.VirtualAddress)), SeekOrigin.Begin);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AEF9 File Offset: 0x000090F9
		private void MoveToRVA(TextSegment segment)
		{
			this.MoveToRVA(this.text, this.text_map.GetRVA(segment));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AF13 File Offset: 0x00009113
		private void WriteRVA(uint rva)
		{
			if (!this.pe64)
			{
				base.WriteUInt32(rva);
				return;
			}
			base.WriteUInt64((ulong)rva);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AF30 File Offset: 0x00009130
		private void PrepareSection(Section section)
		{
			this.MoveTo(section.PointerToRawData);
			if (section.SizeOfRawData <= 4096U)
			{
				this.Write(new byte[section.SizeOfRawData]);
				this.MoveTo(section.PointerToRawData);
				return;
			}
			int num = 0;
			byte[] buffer = new byte[4096];
			while ((long)num != (long)((ulong)section.SizeOfRawData))
			{
				int num2 = Math.Min((int)(section.SizeOfRawData - (uint)num), 4096);
				this.Write(buffer, 0, num2);
				num += num2;
			}
			this.MoveTo(section.PointerToRawData);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AFBC File Offset: 0x000091BC
		private void WriteText()
		{
			this.PrepareSection(this.text);
			if (this.has_reloc)
			{
				this.WriteRVA(this.text_map.GetRVA(TextSegment.ImportHintNameTable));
				this.WriteRVA(0U);
			}
			base.WriteUInt32(72U);
			base.WriteUInt16(2);
			base.WriteUInt16((this.module.Runtime <= TargetRuntime.Net_1_1) ? 0 : 5);
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.MetadataHeader));
			base.WriteUInt32(this.GetMetadataLength());
			base.WriteUInt32((uint)this.module.Attributes);
			base.WriteUInt32(this.metadata.entry_point.ToUInt32());
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.Resources));
			base.WriteDataDirectory(this.text_map.GetDataDirectory(TextSegment.StrongNameSignature));
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.WriteZeroDataDirectory();
			this.MoveToRVA(TextSegment.Code);
			base.WriteBuffer(this.metadata.code);
			this.MoveToRVA(TextSegment.Resources);
			base.WriteBuffer(this.metadata.resources);
			if (this.metadata.data.length > 0)
			{
				this.MoveToRVA(TextSegment.Data);
				base.WriteBuffer(this.metadata.data);
			}
			this.MoveToRVA(TextSegment.MetadataHeader);
			this.WriteMetadataHeader();
			this.WriteMetadata();
			if (this.text_map.GetLength(TextSegment.DebugDirectory) > 0)
			{
				this.MoveToRVA(TextSegment.DebugDirectory);
				this.WriteDebugDirectory();
			}
			if (!this.has_reloc)
			{
				return;
			}
			this.MoveToRVA(TextSegment.ImportDirectory);
			this.WriteImportDirectory();
			this.MoveToRVA(TextSegment.StartupStub);
			this.WriteStartupStub();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B151 File Offset: 0x00009351
		private uint GetMetadataLength()
		{
			return this.text_map.GetRVA(TextSegment.DebugDirectory) - this.text_map.GetRVA(TextSegment.MetadataHeader);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B170 File Offset: 0x00009370
		private void WriteMetadataHeader()
		{
			base.WriteUInt32(1112167234U);
			base.WriteUInt16(1);
			base.WriteUInt16(1);
			base.WriteUInt32(0U);
			byte[] zeroTerminatedString = ImageWriter.GetZeroTerminatedString(this.module.runtime_version);
			base.WriteUInt32((uint)zeroTerminatedString.Length);
			base.WriteBytes(zeroTerminatedString);
			base.WriteUInt16(0);
			base.WriteUInt16(this.GetStreamCount());
			uint num = this.text_map.GetRVA(TextSegment.TableHeap) - this.text_map.GetRVA(TextSegment.MetadataHeader);
			this.WriteStreamHeader(ref num, TextSegment.TableHeap, "#~");
			this.WriteStreamHeader(ref num, TextSegment.StringHeap, "#Strings");
			this.WriteStreamHeader(ref num, TextSegment.UserStringHeap, "#US");
			this.WriteStreamHeader(ref num, TextSegment.GuidHeap, "#GUID");
			this.WriteStreamHeader(ref num, TextSegment.BlobHeap, "#Blob");
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B234 File Offset: 0x00009434
		private ushort GetStreamCount()
		{
			return (ushort)(2 + (this.metadata.user_string_heap.IsEmpty ? 0 : 1) + 1 + (this.metadata.blob_heap.IsEmpty ? 0 : 1));
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B268 File Offset: 0x00009468
		private void WriteStreamHeader(ref uint offset, TextSegment heap, string name)
		{
			uint length = (uint)this.text_map.GetLength(heap);
			if (length == 0U)
			{
				return;
			}
			base.WriteUInt32(offset);
			base.WriteUInt32(length);
			base.WriteBytes(ImageWriter.GetZeroTerminatedString(name));
			offset += length;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B2A7 File Offset: 0x000094A7
		private static byte[] GetZeroTerminatedString(string @string)
		{
			return ImageWriter.GetString(@string, @string.Length + 1 + 3 & -4);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B2BC File Offset: 0x000094BC
		private static byte[] GetSimpleString(string @string)
		{
			return ImageWriter.GetString(@string, @string.Length);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B2CC File Offset: 0x000094CC
		private static byte[] GetString(string @string, int length)
		{
			byte[] array = new byte[length];
			for (int i = 0; i < @string.Length; i++)
			{
				array[i] = (byte)@string[i];
			}
			return array;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B300 File Offset: 0x00009500
		private void WriteMetadata()
		{
			this.WriteHeap(TextSegment.TableHeap, this.metadata.table_heap);
			this.WriteHeap(TextSegment.StringHeap, this.metadata.string_heap);
			this.WriteHeap(TextSegment.UserStringHeap, this.metadata.user_string_heap);
			this.WriteGuidHeap();
			this.WriteHeap(TextSegment.BlobHeap, this.metadata.blob_heap);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B35D File Offset: 0x0000955D
		private void WriteHeap(TextSegment heap, HeapBuffer buffer)
		{
			if (buffer.IsEmpty)
			{
				return;
			}
			this.MoveToRVA(heap);
			base.WriteBuffer(buffer);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B378 File Offset: 0x00009578
		private void WriteGuidHeap()
		{
			this.MoveToRVA(TextSegment.GuidHeap);
			base.WriteBytes(this.module.Mvid.ToByteArray());
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B3A8 File Offset: 0x000095A8
		private void WriteDebugDirectory()
		{
			base.WriteInt32(this.debug_directory.Characteristics);
			base.WriteUInt32(this.time_stamp);
			base.WriteInt16(this.debug_directory.MajorVersion);
			base.WriteInt16(this.debug_directory.MinorVersion);
			base.WriteInt32(this.debug_directory.Type);
			base.WriteInt32(this.debug_directory.SizeOfData);
			base.WriteInt32(this.debug_directory.AddressOfRawData);
			base.WriteInt32((int)this.BaseStream.Position + 4);
			base.WriteBytes(this.debug_data);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B448 File Offset: 0x00009648
		private void WriteImportDirectory()
		{
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.ImportDirectory) + 40U);
			base.WriteUInt32(0U);
			base.WriteUInt32(0U);
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.ImportHintNameTable) + 14U);
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.ImportAddressTable));
			base.Advance(20);
			base.WriteUInt32(this.text_map.GetRVA(TextSegment.ImportHintNameTable));
			this.MoveToRVA(TextSegment.ImportHintNameTable);
			base.WriteUInt16(0);
			base.WriteBytes(this.GetRuntimeMain());
			base.WriteByte(0);
			base.WriteBytes(ImageWriter.GetSimpleString("mscoree.dll"));
			base.WriteUInt16(0);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B4F5 File Offset: 0x000096F5
		private byte[] GetRuntimeMain()
		{
			if (this.module.Kind != ModuleKind.Dll && this.module.Kind != ModuleKind.NetModule)
			{
				return ImageWriter.GetSimpleString("_CorExeMain");
			}
			return ImageWriter.GetSimpleString("_CorDllMain");
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B528 File Offset: 0x00009728
		private void WriteStartupStub()
		{
			TargetArchitecture architecture = this.module.Architecture;
			if (architecture == TargetArchitecture.I386)
			{
				base.WriteUInt16(9727);
				base.WriteUInt32(4194304U + this.text_map.GetRVA(TextSegment.ImportAddressTable));
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B56E File Offset: 0x0000976E
		private void WriteRsrc()
		{
			this.PrepareSection(this.rsrc);
			base.WriteBuffer(this.win32_resources);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B588 File Offset: 0x00009788
		private void WriteReloc()
		{
			this.PrepareSection(this.reloc);
			uint num = this.text_map.GetRVA(TextSegment.StartupStub);
			num += ((this.module.Architecture == TargetArchitecture.IA64) ? 32U : 2U);
			uint num2 = num & 4294963200U;
			base.WriteUInt32(num2);
			base.WriteUInt32(12U);
			TargetArchitecture architecture = this.module.Architecture;
			if (architecture == TargetArchitecture.I386)
			{
				base.WriteUInt32(12288U + num - num2);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B601 File Offset: 0x00009801
		public void WriteImage()
		{
			this.WriteDOSHeader();
			this.WritePEFileHeader();
			this.WriteOptionalHeaders();
			this.WriteSectionHeaders();
			this.WriteText();
			if (this.rsrc != null)
			{
				this.WriteRsrc();
			}
			if (this.reloc != null)
			{
				this.WriteReloc();
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B640 File Offset: 0x00009840
		private TextMap BuildTextMap()
		{
			TextMap textMap = this.metadata.text_map;
			textMap.AddMap(TextSegment.Code, this.metadata.code.length, (!this.pe64) ? 4 : 16);
			textMap.AddMap(TextSegment.Resources, this.metadata.resources.length, 8);
			textMap.AddMap(TextSegment.Data, this.metadata.data.length, 4);
			if (this.metadata.data.length > 0)
			{
				this.metadata.table_heap.FixupData(textMap.GetRVA(TextSegment.Data));
			}
			textMap.AddMap(TextSegment.StrongNameSignature, this.GetStrongNameLength(), 4);
			textMap.AddMap(TextSegment.MetadataHeader, this.GetMetadataHeaderLength());
			textMap.AddMap(TextSegment.TableHeap, this.metadata.table_heap.length, 4);
			textMap.AddMap(TextSegment.StringHeap, this.metadata.string_heap.length, 4);
			textMap.AddMap(TextSegment.UserStringHeap, this.metadata.user_string_heap.IsEmpty ? 0 : this.metadata.user_string_heap.length, 4);
			textMap.AddMap(TextSegment.GuidHeap, 16);
			textMap.AddMap(TextSegment.BlobHeap, this.metadata.blob_heap.IsEmpty ? 0 : this.metadata.blob_heap.length, 4);
			int length = 0;
			if (!this.debug_data.IsNullOrEmpty<byte>())
			{
				this.debug_directory.AddressOfRawData = (int)(textMap.GetNextRVA(TextSegment.BlobHeap) + 28U);
				length = this.debug_data.Length + 28;
			}
			textMap.AddMap(TextSegment.DebugDirectory, length, 4);
			if (!this.has_reloc)
			{
				uint nextRVA = textMap.GetNextRVA(TextSegment.DebugDirectory);
				textMap.AddMap(TextSegment.ImportDirectory, new Range(nextRVA, 0U));
				textMap.AddMap(TextSegment.ImportHintNameTable, new Range(nextRVA, 0U));
				textMap.AddMap(TextSegment.StartupStub, new Range(nextRVA, 0U));
				return textMap;
			}
			uint nextRVA2 = textMap.GetNextRVA(TextSegment.DebugDirectory);
			uint num = nextRVA2 + 48U;
			num = (num + 15U & 4294967280U);
			uint num2 = num - nextRVA2 + 27U;
			uint num3 = nextRVA2 + num2;
			num3 = ((this.module.Architecture == TargetArchitecture.IA64) ? (num3 + 15U & 4294967280U) : (2U + (num3 + 3U & 4294967292U)));
			textMap.AddMap(TextSegment.ImportDirectory, new Range(nextRVA2, num2));
			textMap.AddMap(TextSegment.ImportHintNameTable, new Range(num, 0U));
			textMap.AddMap(TextSegment.StartupStub, new Range(num3, this.GetStartupStubLength()));
			return textMap;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B884 File Offset: 0x00009A84
		private uint GetStartupStubLength()
		{
			TargetArchitecture architecture = this.module.Architecture;
			if (architecture == TargetArchitecture.I386)
			{
				return 6U;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		private int GetMetadataHeaderLength()
		{
			return 72 + (this.metadata.user_string_heap.IsEmpty ? 0 : 12) + 16 + (this.metadata.blob_heap.IsEmpty ? 0 : 16);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		private int GetStrongNameLength()
		{
			if (this.module.Assembly == null)
			{
				return 0;
			}
			byte[] publicKey = this.module.Assembly.Name.PublicKey;
			if (publicKey.IsNullOrEmpty<byte>())
			{
				return 0;
			}
			int num = publicKey.Length;
			if (num > 32)
			{
				return num - 32;
			}
			return 128;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B92F File Offset: 0x00009B2F
		public DataDirectory GetStrongNameSignatureDirectory()
		{
			return this.text_map.GetDataDirectory(TextSegment.StrongNameSignature);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B93D File Offset: 0x00009B3D
		public uint GetHeaderSize()
		{
			return (uint)(152 + this.SizeOfOptionalHeader() + this.sections * 40);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B955 File Offset: 0x00009B55
		private void PatchWin32Resources(ByteBuffer resources)
		{
			this.PatchResourceDirectoryTable(resources);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B960 File Offset: 0x00009B60
		private void PatchResourceDirectoryTable(ByteBuffer resources)
		{
			resources.Advance(12);
			int num = (int)(resources.ReadUInt16() + resources.ReadUInt16());
			for (int i = 0; i < num; i++)
			{
				this.PatchResourceDirectoryEntry(resources);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B998 File Offset: 0x00009B98
		private void PatchResourceDirectoryEntry(ByteBuffer resources)
		{
			resources.Advance(4);
			uint num = resources.ReadUInt32();
			int position = resources.position;
			resources.position = (int)(num & 2147483647U);
			if ((num & 2147483648U) != 0U)
			{
				this.PatchResourceDirectoryTable(resources);
			}
			else
			{
				this.PatchResourceDataEntry(resources);
			}
			resources.position = position;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		private void PatchResourceDataEntry(ByteBuffer resources)
		{
			Section imageResourceSection = this.GetImageResourceSection();
			uint num = resources.ReadUInt32();
			resources.position -= 4;
			resources.WriteUInt32(num - imageResourceSection.VirtualAddress + this.rsrc.VirtualAddress);
		}

		// Token: 0x0400034B RID: 843
		private const uint pe_header_size = 152U;

		// Token: 0x0400034C RID: 844
		private const uint section_header_size = 40U;

		// Token: 0x0400034D RID: 845
		private const uint file_alignment = 512U;

		// Token: 0x0400034E RID: 846
		private const uint section_alignment = 8192U;

		// Token: 0x0400034F RID: 847
		private const ulong image_base = 4194304UL;

		// Token: 0x04000350 RID: 848
		internal const uint text_rva = 8192U;

		// Token: 0x04000351 RID: 849
		private readonly ModuleDefinition module;

		// Token: 0x04000352 RID: 850
		private readonly MetadataBuilder metadata;

		// Token: 0x04000353 RID: 851
		private readonly TextMap text_map;

		// Token: 0x04000354 RID: 852
		private ImageDebugDirectory debug_directory;

		// Token: 0x04000355 RID: 853
		private byte[] debug_data;

		// Token: 0x04000356 RID: 854
		private ByteBuffer win32_resources;

		// Token: 0x04000357 RID: 855
		private readonly bool pe64;

		// Token: 0x04000358 RID: 856
		private readonly bool has_reloc;

		// Token: 0x04000359 RID: 857
		private readonly uint time_stamp;

		// Token: 0x0400035A RID: 858
		internal Section text;

		// Token: 0x0400035B RID: 859
		internal Section rsrc;

		// Token: 0x0400035C RID: 860
		internal Section reloc;

		// Token: 0x0400035D RID: 861
		private ushort sections;
	}
}
