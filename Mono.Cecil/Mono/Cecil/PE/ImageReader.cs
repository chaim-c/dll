﻿using System;
using System.IO;
using Mono.Cecil.Metadata;

namespace Mono.Cecil.PE
{
	// Token: 0x02000049 RID: 73
	internal sealed class ImageReader : BinaryStreamReader
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00009AF6 File Offset: 0x00007CF6
		public ImageReader(Stream stream) : base(stream)
		{
			this.image = new Image();
			this.image.FileName = stream.GetFullyQualifiedName();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009B1B File Offset: 0x00007D1B
		private void MoveTo(DataDirectory directory)
		{
			this.BaseStream.Position = (long)((ulong)this.image.ResolveVirtualAddress(directory.VirtualAddress));
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void MoveTo(uint position)
		{
			this.BaseStream.Position = (long)((ulong)position);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009B4C File Offset: 0x00007D4C
		private void ReadImage()
		{
			if (this.BaseStream.Length < 128L)
			{
				throw new BadImageFormatException();
			}
			if (this.ReadUInt16() != 23117)
			{
				throw new BadImageFormatException();
			}
			base.Advance(58);
			this.MoveTo(this.ReadUInt32());
			if (this.ReadUInt32() != 17744U)
			{
				throw new BadImageFormatException();
			}
			this.image.Architecture = this.ReadArchitecture();
			ushort count = this.ReadUInt16();
			base.Advance(14);
			ushort characteristics = this.ReadUInt16();
			ushort subsystem;
			ushort characteristics2;
			this.ReadOptionalHeaders(out subsystem, out characteristics2);
			this.ReadSections(count);
			this.ReadCLIHeader();
			this.ReadMetadata();
			this.image.Kind = ImageReader.GetModuleKind(characteristics, subsystem);
			this.image.Characteristics = (ModuleCharacteristics)characteristics2;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009C10 File Offset: 0x00007E10
		private TargetArchitecture ReadArchitecture()
		{
			ushort num = this.ReadUInt16();
			ushort num2 = num;
			if (num2 <= 452)
			{
				if (num2 == 332)
				{
					return TargetArchitecture.I386;
				}
				if (num2 == 452)
				{
					return TargetArchitecture.ARMv7;
				}
			}
			else
			{
				if (num2 == 512)
				{
					return TargetArchitecture.IA64;
				}
				if (num2 == 34404)
				{
					return TargetArchitecture.AMD64;
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009C5F File Offset: 0x00007E5F
		private static ModuleKind GetModuleKind(ushort characteristics, ushort subsystem)
		{
			if ((characteristics & 8192) != 0)
			{
				return ModuleKind.Dll;
			}
			if (subsystem == 2 || subsystem == 9)
			{
				return ModuleKind.Windows;
			}
			return ModuleKind.Console;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009C78 File Offset: 0x00007E78
		private void ReadOptionalHeaders(out ushort subsystem, out ushort dll_characteristics)
		{
			bool flag = this.ReadUInt16() == 523;
			base.Advance(66);
			subsystem = this.ReadUInt16();
			dll_characteristics = this.ReadUInt16();
			base.Advance(flag ? 88 : 72);
			this.image.Debug = base.ReadDataDirectory();
			base.Advance(56);
			this.cli = base.ReadDataDirectory();
			if (this.cli.IsZero)
			{
				throw new BadImageFormatException();
			}
			base.Advance(8);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009CFC File Offset: 0x00007EFC
		private string ReadAlignedString(int length)
		{
			int i = 0;
			char[] array = new char[length];
			while (i < length)
			{
				byte b = this.ReadByte();
				if (b == 0)
				{
					break;
				}
				array[i++] = (char)b;
			}
			base.Advance(-1 + (i + 4 & -4) - i);
			return new string(array, 0, i);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009D44 File Offset: 0x00007F44
		private string ReadZeroTerminatedString(int length)
		{
			int i = 0;
			char[] array = new char[length];
			byte[] array2 = this.ReadBytes(length);
			while (i < length)
			{
				byte b = array2[i];
				if (b == 0)
				{
					break;
				}
				array[i++] = (char)b;
			}
			return new string(array, 0, i);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009D80 File Offset: 0x00007F80
		private void ReadSections(ushort count)
		{
			Section[] array = new Section[(int)count];
			for (int i = 0; i < (int)count; i++)
			{
				Section section = new Section();
				section.Name = this.ReadZeroTerminatedString(8);
				base.Advance(4);
				section.VirtualAddress = this.ReadUInt32();
				section.SizeOfRawData = this.ReadUInt32();
				section.PointerToRawData = this.ReadUInt32();
				base.Advance(16);
				array[i] = section;
				this.ReadSectionData(section);
			}
			this.image.Sections = array;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009E00 File Offset: 0x00008000
		private void ReadSectionData(Section section)
		{
			long position = this.BaseStream.Position;
			this.MoveTo(section.PointerToRawData);
			int sizeOfRawData = (int)section.SizeOfRawData;
			byte[] array = new byte[sizeOfRawData];
			int num = 0;
			int num2;
			while ((num2 = this.Read(array, num, sizeOfRawData - num)) > 0)
			{
				num += num2;
			}
			section.Data = array;
			this.BaseStream.Position = position;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009E60 File Offset: 0x00008060
		private void ReadCLIHeader()
		{
			this.MoveTo(this.cli);
			base.Advance(8);
			this.metadata = base.ReadDataDirectory();
			this.image.Attributes = (ModuleAttributes)this.ReadUInt32();
			this.image.EntryPointToken = this.ReadUInt32();
			this.image.Resources = base.ReadDataDirectory();
			this.image.StrongName = base.ReadDataDirectory();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009ED0 File Offset: 0x000080D0
		private void ReadMetadata()
		{
			this.MoveTo(this.metadata);
			if (this.ReadUInt32() != 1112167234U)
			{
				throw new BadImageFormatException();
			}
			base.Advance(8);
			this.image.RuntimeVersion = this.ReadZeroTerminatedString(this.ReadInt32());
			base.Advance(2);
			ushort num = this.ReadUInt16();
			Section sectionAtVirtualAddress = this.image.GetSectionAtVirtualAddress(this.metadata.VirtualAddress);
			if (sectionAtVirtualAddress == null)
			{
				throw new BadImageFormatException();
			}
			this.image.MetadataSection = sectionAtVirtualAddress;
			for (int i = 0; i < (int)num; i++)
			{
				this.ReadMetadataStream(sectionAtVirtualAddress);
			}
			if (this.image.TableHeap != null)
			{
				this.ReadTableHeap();
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009F7C File Offset: 0x0000817C
		private void ReadMetadataStream(Section section)
		{
			uint start = this.metadata.VirtualAddress - section.VirtualAddress + this.ReadUInt32();
			uint size = this.ReadUInt32();
			string text = this.ReadAlignedString(16);
			string a;
			if ((a = text) != null)
			{
				if (a == "#~" || a == "#-")
				{
					this.image.TableHeap = new TableHeap(section, start, size);
					return;
				}
				if (a == "#Strings")
				{
					this.image.StringHeap = new StringHeap(section, start, size);
					return;
				}
				if (a == "#Blob")
				{
					this.image.BlobHeap = new BlobHeap(section, start, size);
					return;
				}
				if (a == "#GUID")
				{
					this.image.GuidHeap = new GuidHeap(section, start, size);
					return;
				}
				if (!(a == "#US"))
				{
					return;
				}
				this.image.UserStringHeap = new UserStringHeap(section, start, size);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A070 File Offset: 0x00008270
		private void ReadTableHeap()
		{
			TableHeap tableHeap = this.image.TableHeap;
			uint pointerToRawData = tableHeap.Section.PointerToRawData;
			this.MoveTo(tableHeap.Offset + pointerToRawData);
			base.Advance(6);
			byte sizes = this.ReadByte();
			base.Advance(1);
			tableHeap.Valid = this.ReadInt64();
			tableHeap.Sorted = this.ReadInt64();
			for (int i = 0; i < 45; i++)
			{
				if (tableHeap.HasTable((Table)i))
				{
					tableHeap.Tables[i].Length = this.ReadUInt32();
				}
			}
			ImageReader.SetIndexSize(this.image.StringHeap, (uint)sizes, 1);
			ImageReader.SetIndexSize(this.image.GuidHeap, (uint)sizes, 2);
			ImageReader.SetIndexSize(this.image.BlobHeap, (uint)sizes, 4);
			this.ComputeTableInformations();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A13A File Offset: 0x0000833A
		private static void SetIndexSize(Heap heap, uint sizes, byte flag)
		{
			if (heap == null)
			{
				return;
			}
			heap.IndexSize = (((sizes & (uint)flag) > 0U) ? 4 : 2);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A150 File Offset: 0x00008350
		private int GetTableIndexSize(Table table)
		{
			return this.image.GetTableIndexSize(table);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A15E File Offset: 0x0000835E
		private int GetCodedIndexSize(CodedIndex index)
		{
			return this.image.GetCodedIndexSize(index);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A16C File Offset: 0x0000836C
		private void ComputeTableInformations()
		{
			uint num = (uint)this.BaseStream.Position - this.image.MetadataSection.PointerToRawData;
			int indexSize = this.image.StringHeap.IndexSize;
			int num2 = (this.image.BlobHeap != null) ? this.image.BlobHeap.IndexSize : 2;
			TableHeap tableHeap = this.image.TableHeap;
			TableInformation[] tables = tableHeap.Tables;
			for (int i = 0; i < 45; i++)
			{
				Table table = (Table)i;
				if (tableHeap.HasTable(table))
				{
					int num3;
					switch (table)
					{
					case Table.Module:
						num3 = 2 + indexSize + this.image.GuidHeap.IndexSize * 3;
						break;
					case Table.TypeRef:
						num3 = this.GetCodedIndexSize(CodedIndex.ResolutionScope) + indexSize * 2;
						break;
					case Table.TypeDef:
						num3 = 4 + indexSize * 2 + this.GetCodedIndexSize(CodedIndex.TypeDefOrRef) + this.GetTableIndexSize(Table.Field) + this.GetTableIndexSize(Table.Method);
						break;
					case Table.FieldPtr:
						num3 = this.GetTableIndexSize(Table.Field);
						break;
					case Table.Field:
						num3 = 2 + indexSize + num2;
						break;
					case Table.MethodPtr:
						num3 = this.GetTableIndexSize(Table.Method);
						break;
					case Table.Method:
						num3 = 8 + indexSize + num2 + this.GetTableIndexSize(Table.Param);
						break;
					case Table.ParamPtr:
						num3 = this.GetTableIndexSize(Table.Param);
						break;
					case Table.Param:
						num3 = 4 + indexSize;
						break;
					case Table.InterfaceImpl:
						num3 = this.GetTableIndexSize(Table.TypeDef) + this.GetCodedIndexSize(CodedIndex.TypeDefOrRef);
						break;
					case Table.MemberRef:
						num3 = this.GetCodedIndexSize(CodedIndex.MemberRefParent) + indexSize + num2;
						break;
					case Table.Constant:
						num3 = 2 + this.GetCodedIndexSize(CodedIndex.HasConstant) + num2;
						break;
					case Table.CustomAttribute:
						num3 = this.GetCodedIndexSize(CodedIndex.HasCustomAttribute) + this.GetCodedIndexSize(CodedIndex.CustomAttributeType) + num2;
						break;
					case Table.FieldMarshal:
						num3 = this.GetCodedIndexSize(CodedIndex.HasFieldMarshal) + num2;
						break;
					case Table.DeclSecurity:
						num3 = 2 + this.GetCodedIndexSize(CodedIndex.HasDeclSecurity) + num2;
						break;
					case Table.ClassLayout:
						num3 = 6 + this.GetTableIndexSize(Table.TypeDef);
						break;
					case Table.FieldLayout:
						num3 = 4 + this.GetTableIndexSize(Table.Field);
						break;
					case Table.StandAloneSig:
						num3 = num2;
						break;
					case Table.EventMap:
						num3 = this.GetTableIndexSize(Table.TypeDef) + this.GetTableIndexSize(Table.Event);
						break;
					case Table.EventPtr:
						num3 = this.GetTableIndexSize(Table.Event);
						break;
					case Table.Event:
						num3 = 2 + indexSize + this.GetCodedIndexSize(CodedIndex.TypeDefOrRef);
						break;
					case Table.PropertyMap:
						num3 = this.GetTableIndexSize(Table.TypeDef) + this.GetTableIndexSize(Table.Property);
						break;
					case Table.PropertyPtr:
						num3 = this.GetTableIndexSize(Table.Property);
						break;
					case Table.Property:
						num3 = 2 + indexSize + num2;
						break;
					case Table.MethodSemantics:
						num3 = 2 + this.GetTableIndexSize(Table.Method) + this.GetCodedIndexSize(CodedIndex.HasSemantics);
						break;
					case Table.MethodImpl:
						num3 = this.GetTableIndexSize(Table.TypeDef) + this.GetCodedIndexSize(CodedIndex.MethodDefOrRef) + this.GetCodedIndexSize(CodedIndex.MethodDefOrRef);
						break;
					case Table.ModuleRef:
						num3 = indexSize;
						break;
					case Table.TypeSpec:
						num3 = num2;
						break;
					case Table.ImplMap:
						num3 = 2 + this.GetCodedIndexSize(CodedIndex.MemberForwarded) + indexSize + this.GetTableIndexSize(Table.ModuleRef);
						break;
					case Table.FieldRVA:
						num3 = 4 + this.GetTableIndexSize(Table.Field);
						break;
					case Table.EncLog:
						num3 = 8;
						break;
					case Table.EncMap:
						num3 = 4;
						break;
					case Table.Assembly:
						num3 = 16 + num2 + indexSize * 2;
						break;
					case Table.AssemblyProcessor:
						num3 = 4;
						break;
					case Table.AssemblyOS:
						num3 = 12;
						break;
					case Table.AssemblyRef:
						num3 = 12 + num2 * 2 + indexSize * 2;
						break;
					case Table.AssemblyRefProcessor:
						num3 = 4 + this.GetTableIndexSize(Table.AssemblyRef);
						break;
					case Table.AssemblyRefOS:
						num3 = 12 + this.GetTableIndexSize(Table.AssemblyRef);
						break;
					case Table.File:
						num3 = 4 + indexSize + num2;
						break;
					case Table.ExportedType:
						num3 = 8 + indexSize * 2 + this.GetCodedIndexSize(CodedIndex.Implementation);
						break;
					case Table.ManifestResource:
						num3 = 8 + indexSize + this.GetCodedIndexSize(CodedIndex.Implementation);
						break;
					case Table.NestedClass:
						num3 = this.GetTableIndexSize(Table.TypeDef) + this.GetTableIndexSize(Table.TypeDef);
						break;
					case Table.GenericParam:
						num3 = 4 + this.GetCodedIndexSize(CodedIndex.TypeOrMethodDef) + indexSize;
						break;
					case Table.MethodSpec:
						num3 = this.GetCodedIndexSize(CodedIndex.MethodDefOrRef) + num2;
						break;
					case Table.GenericParamConstraint:
						num3 = this.GetTableIndexSize(Table.GenericParam) + this.GetCodedIndexSize(CodedIndex.TypeDefOrRef);
						break;
					default:
						throw new NotSupportedException();
					}
					tables[i].RowSize = (uint)num3;
					tables[i].Offset = num;
					num += (uint)(num3 * (int)tables[i].Length);
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A5F4 File Offset: 0x000087F4
		public static Image ReadImageFrom(Stream stream)
		{
			Image result;
			try
			{
				ImageReader imageReader = new ImageReader(stream);
				imageReader.ReadImage();
				result = imageReader.image;
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(stream.GetFullyQualifiedName(), inner);
			}
			return result;
		}

		// Token: 0x04000348 RID: 840
		private readonly Image image;

		// Token: 0x04000349 RID: 841
		private DataDirectory cli;

		// Token: 0x0400034A RID: 842
		private DataDirectory metadata;
	}
}
