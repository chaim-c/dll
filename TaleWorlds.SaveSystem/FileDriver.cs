using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x0200000D RID: 13
	public class FileDriver : ISaveDriver
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002300 File Offset: 0x00000500
		private static PlatformDirectoryPath SavePath
		{
			get
			{
				string path = "Game Saves\\";
				return new PlatformDirectoryPath(PlatformFileType.User, path);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000231A File Offset: 0x0000051A
		private PlatformFilePath GetSaveFilePath(string fileName)
		{
			return new PlatformFilePath(FileDriver.SavePath, fileName);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002328 File Offset: 0x00000528
		public Task<SaveResultWithMessage> Save(string saveName, int version, MetaData metaData, GameData gameData)
		{
			SaveResult result = SaveResult.FileDriverFailure;
			PlatformFilePath saveFilePath = this.GetSaveFilePath(saveName + ".sav");
			MemoryStream memoryStream = new MemoryStream();
			metaData.Add("Version", version.ToString());
			metaData.Serialize(memoryStream);
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionLevel.Fastest, true))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(deflateStream))
				{
					GameData.Write(binaryWriter, gameData);
				}
			}
			ArraySegment<byte> arraySegment;
			if (memoryStream.TryGetBuffer(out arraySegment))
			{
				byte[] array = arraySegment.Array;
				Array.Resize<byte>(ref array, arraySegment.Count);
				result = FileHelper.SaveFile(saveFilePath, array);
			}
			memoryStream.Close();
			string error = Common.PlatformFileHelper.GetError();
			return Task.FromResult<SaveResultWithMessage>(new SaveResultWithMessage(result, error));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002404 File Offset: 0x00000604
		public MetaData LoadMetaData(string saveName)
		{
			byte[] fileContent = FileHelper.GetFileContent(this.GetSaveFilePath(saveName + ".sav"));
			if (fileContent != null)
			{
				return MetaData.Deserialize(new MemoryStream(fileContent));
			}
			Debug.Print("[Load meta data error]: " + saveName, 0, Debug.DebugColor.White, 17592186044416UL);
			return null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002454 File Offset: 0x00000654
		public LoadData Load(string saveName)
		{
			byte[] fileContent = FileHelper.GetFileContent(this.GetSaveFilePath(saveName + ".sav"));
			if (fileContent != null)
			{
				MemoryStream stream = new MemoryStream(fileContent);
				MetaData metaData = MetaData.Deserialize(stream);
				using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
				{
					try
					{
						GameData gameData;
						if (this.GetApplicationVersionOfMetaData(metaData) < ApplicationVersion.FromString("v1.1.0", 45697))
						{
							gameData = (GameData)new BinaryFormatter().Deserialize(deflateStream);
							return new LoadData(metaData, gameData);
						}
						using (BinaryReader binaryReader = new BinaryReader(deflateStream))
						{
							gameData = GameData.Read(binaryReader);
						}
						return new LoadData(metaData, gameData);
					}
					catch (Exception ex)
					{
						Debug.Print(ex.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
						return null;
					}
				}
			}
			Debug.Print("[Load error]: " + saveName, 0, Debug.DebugColor.White, 17592186044416UL);
			return null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002564 File Offset: 0x00000764
		public SaveGameFileInfo[] GetSaveGameFileInfos()
		{
			PlatformFilePath[] files = FileHelper.GetFiles(FileDriver.SavePath, "*.sav");
			List<SaveGameFileInfo> list = new List<SaveGameFileInfo>((files != null) ? files.Length : 0);
			if (files != null)
			{
				foreach (PlatformFilePath platformFilePath in files)
				{
					string fileNameWithoutExtension = platformFilePath.GetFileNameWithoutExtension();
					MetaData metaData = SaveManager.LoadMetaData(fileNameWithoutExtension, this);
					list.Add(new SaveGameFileInfo
					{
						Name = fileNameWithoutExtension,
						MetaData = metaData,
						IsCorrupted = (metaData == null || this.GetApplicationVersionOfMetaData(metaData) == ApplicationVersion.Empty)
					});
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002604 File Offset: 0x00000804
		private ApplicationVersion GetApplicationVersionOfMetaData(MetaData metaData)
		{
			string text = (metaData != null) ? metaData["ApplicationVersion"] : null;
			if (text == null)
			{
				return ApplicationVersion.Empty;
			}
			return ApplicationVersion.FromString(text, 45697);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002638 File Offset: 0x00000838
		public string[] GetSaveGameFileNames()
		{
			List<string> list = new List<string>();
			PlatformFilePath[] files = FileHelper.GetFiles(FileDriver.SavePath, "*.sav");
			if (files != null)
			{
				foreach (PlatformFilePath platformFilePath in files)
				{
					string fileNameWithoutExtension = platformFilePath.GetFileNameWithoutExtension();
					list.Add(fileNameWithoutExtension);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000268C File Offset: 0x0000088C
		public bool Delete(string saveName)
		{
			PlatformFilePath saveFilePath = this.GetSaveFilePath(saveName + ".sav");
			if (FileHelper.FileExists(saveFilePath))
			{
				FileHelper.DeleteFile(saveFilePath);
				return true;
			}
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026BC File Offset: 0x000008BC
		public bool IsSaveGameFileExists(string saveName)
		{
			return FileHelper.FileExists(this.GetSaveFilePath(saveName + ".sav"));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026D4 File Offset: 0x000008D4
		public bool IsWorkingAsync()
		{
			return false;
		}

		// Token: 0x04000012 RID: 18
		private const string SaveDirectoryName = "Game Saves";
	}
}
