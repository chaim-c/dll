using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000019 RID: 25
	internal class ArchiveSerializer : IArchiveContext
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003D4C File Offset: 0x00001F4C
		public ArchiveSerializer()
		{
			this._writer = BinaryWriterFactory.GetBinaryWriter();
			this._folders = new List<SaveEntryFolder>();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003D6C File Offset: 0x00001F6C
		public void SerializeEntry(SaveEntry entry)
		{
			this._writer.Write3ByteInt(entry.FolderId);
			this._writer.Write3ByteInt(entry.Id.Id);
			this._writer.WriteByte((byte)entry.Id.Extension);
			this._writer.WriteShort((short)entry.Data.Length);
			this._writer.WriteBytes(entry.Data);
			this._entryCount++;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public void SerializeFolder(SaveEntryFolder folder)
		{
			foreach (SaveEntry entry in folder.AllEntries)
			{
				this.SerializeEntry(entry);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003E40 File Offset: 0x00002040
		public SaveEntryFolder CreateFolder(SaveEntryFolder parentFolder, FolderId folderId, int entryCount)
		{
			int folderCount = this._folderCount;
			this._folderCount++;
			SaveEntryFolder saveEntryFolder = new SaveEntryFolder(parentFolder, folderCount, folderId, entryCount);
			parentFolder.AddChildFolderEntry(saveEntryFolder);
			this._folders.Add(saveEntryFolder);
			return saveEntryFolder;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003E80 File Offset: 0x00002080
		public byte[] FinalizeAndGetBinaryData()
		{
			BinaryWriter binaryWriter = BinaryWriterFactory.GetBinaryWriter();
			binaryWriter.WriteInt(this._folderCount);
			for (int i = 0; i < this._folderCount; i++)
			{
				SaveEntryFolder saveEntryFolder = this._folders[i];
				int parentGlobalId = saveEntryFolder.ParentGlobalId;
				int globalId = saveEntryFolder.GlobalId;
				int localId = saveEntryFolder.FolderId.LocalId;
				SaveFolderExtension extension = saveEntryFolder.FolderId.Extension;
				binaryWriter.Write3ByteInt(parentGlobalId);
				binaryWriter.Write3ByteInt(globalId);
				binaryWriter.Write3ByteInt(localId);
				binaryWriter.WriteByte((byte)extension);
			}
			binaryWriter.WriteInt(this._entryCount);
			binaryWriter.AppendData(this._writer);
			byte[] data = binaryWriter.Data;
			BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter);
			BinaryWriterFactory.ReleaseBinaryWriter(this._writer);
			this._writer = null;
			return data;
		}

		// Token: 0x04000023 RID: 35
		private BinaryWriter _writer;

		// Token: 0x04000024 RID: 36
		private int _entryCount;

		// Token: 0x04000025 RID: 37
		private int _folderCount;

		// Token: 0x04000026 RID: 38
		private List<SaveEntryFolder> _folders;
	}
}
