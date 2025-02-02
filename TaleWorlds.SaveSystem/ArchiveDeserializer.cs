using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000016 RID: 22
	internal class ArchiveDeserializer
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000038CD File Offset: 0x00001ACD
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000038D5 File Offset: 0x00001AD5
		public SaveEntryFolder RootFolder { get; private set; }

		// Token: 0x0600007C RID: 124 RVA: 0x000038DE File Offset: 0x00001ADE
		public ArchiveDeserializer()
		{
			this.RootFolder = new SaveEntryFolder(-1, -1, new FolderId(-1, SaveFolderExtension.Root), 3);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000038FC File Offset: 0x00001AFC
		public void LoadFrom(byte[] binaryArchive)
		{
			Dictionary<int, SaveEntryFolder> dictionary = new Dictionary<int, SaveEntryFolder>();
			List<SaveEntry> list = new List<SaveEntry>();
			BinaryReader binaryReader = new BinaryReader(binaryArchive);
			int num = binaryReader.ReadInt();
			for (int i = 0; i < num; i++)
			{
				int parentGlobalId = binaryReader.Read3ByteInt();
				int globalId = binaryReader.Read3ByteInt();
				int localId = binaryReader.Read3ByteInt();
				SaveFolderExtension extension = (SaveFolderExtension)binaryReader.ReadByte();
				FolderId folderId = new FolderId(localId, extension);
				SaveEntryFolder saveEntryFolder = new SaveEntryFolder(parentGlobalId, globalId, folderId, 3);
				dictionary.Add(saveEntryFolder.GlobalId, saveEntryFolder);
			}
			int num2 = binaryReader.ReadInt();
			for (int j = 0; j < num2; j++)
			{
				int entryFolderId = binaryReader.Read3ByteInt();
				int id = binaryReader.Read3ByteInt();
				SaveEntryExtension extension2 = (SaveEntryExtension)binaryReader.ReadByte();
				short length = binaryReader.ReadShort();
				byte[] data = binaryReader.ReadBytes((int)length);
				SaveEntry item = SaveEntry.CreateFrom(entryFolderId, new EntryId(id, extension2), data);
				list.Add(item);
			}
			foreach (SaveEntryFolder saveEntryFolder2 in dictionary.Values)
			{
				if (saveEntryFolder2.ParentGlobalId != -1)
				{
					dictionary[saveEntryFolder2.ParentGlobalId].AddChildFolderEntry(saveEntryFolder2);
				}
				else
				{
					this.RootFolder.AddChildFolderEntry(saveEntryFolder2);
				}
			}
			foreach (SaveEntry saveEntry in list)
			{
				if (saveEntry.FolderId != -1)
				{
					dictionary[saveEntry.FolderId].AddEntry(saveEntry);
				}
				else
				{
					this.RootFolder.AddEntry(saveEntry);
				}
			}
		}
	}
}
