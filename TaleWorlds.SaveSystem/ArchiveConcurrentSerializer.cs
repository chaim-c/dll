using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000018 RID: 24
	internal class ArchiveConcurrentSerializer : IArchiveContext
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public ArchiveConcurrentSerializer()
		{
			this._locker = new object();
			this._writers = new Dictionary<int, BinaryWriter>();
			this._folders = new ConcurrentBag<SaveEntryFolder>();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003ADC File Offset: 0x00001CDC
		public void SerializeFolderConcurrent(SaveEntryFolder folder)
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			object locker = this._locker;
			BinaryWriter binaryWriter;
			lock (locker)
			{
				if (!this._writers.TryGetValue(managedThreadId, out binaryWriter))
				{
					binaryWriter = new BinaryWriter(262144);
					this._writers.Add(managedThreadId, binaryWriter);
				}
			}
			foreach (SaveEntry entry in folder.AllEntries)
			{
				this.SerializeEntryConcurrent(entry, binaryWriter);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003B90 File Offset: 0x00001D90
		public SaveEntryFolder CreateFolder(SaveEntryFolder parentFolder, FolderId folderId, int entryCount)
		{
			int globalId = Interlocked.Increment(ref this._folderCount) - 1;
			SaveEntryFolder saveEntryFolder = new SaveEntryFolder(parentFolder, globalId, folderId, entryCount);
			parentFolder.AddChildFolderEntry(saveEntryFolder);
			this._folders.Add(saveEntryFolder);
			return saveEntryFolder;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003BCC File Offset: 0x00001DCC
		private void SerializeEntryConcurrent(SaveEntry entry, BinaryWriter writer)
		{
			BinaryWriter binaryWriter = BinaryWriterFactory.GetBinaryWriter();
			binaryWriter.Write3ByteInt(entry.FolderId);
			binaryWriter.Write3ByteInt(entry.Id.Id);
			binaryWriter.WriteByte((byte)entry.Id.Extension);
			binaryWriter.WriteShort((short)entry.Data.Length);
			binaryWriter.WriteBytes(entry.Data);
			byte[] data = binaryWriter.Data;
			BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter);
			writer.WriteBytes(data);
			Interlocked.Increment(ref this._entryCount);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003C4C File Offset: 0x00001E4C
		public byte[] FinalizeAndGetBinaryDataConcurrent()
		{
			BinaryWriter binaryWriter = new BinaryWriter();
			binaryWriter.WriteInt(this._folderCount);
			foreach (SaveEntryFolder saveEntryFolder in this._folders)
			{
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
			foreach (BinaryWriter writer in this._writers.Values)
			{
				binaryWriter.AppendData(writer);
			}
			return binaryWriter.Data;
		}

		// Token: 0x0400001E RID: 30
		private int _entryCount;

		// Token: 0x0400001F RID: 31
		private int _folderCount;

		// Token: 0x04000020 RID: 32
		private object _locker;

		// Token: 0x04000021 RID: 33
		private Dictionary<int, BinaryWriter> _writers;

		// Token: 0x04000022 RID: 34
		private ConcurrentBag<SaveEntryFolder> _folders;
	}
}
