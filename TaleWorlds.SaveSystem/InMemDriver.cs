using System;
using System.IO;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x0200000F RID: 15
	public class InMemDriver : ISaveDriver
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002B58 File Offset: 0x00000D58
		public Task<SaveResultWithMessage> Save(string saveName, int version, MetaData metaData, GameData gameData)
		{
			byte[] data = gameData.GetData();
			MemoryStream memoryStream = new MemoryStream();
			metaData.Add("version", version.ToString());
			metaData.Serialize(memoryStream);
			memoryStream.Write(data, 0, data.Length);
			this._data = memoryStream.GetBuffer();
			return Task.FromResult<SaveResultWithMessage>(SaveResultWithMessage.Default);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public MetaData LoadMetaData(string saveName)
		{
			MemoryStream memoryStream = new MemoryStream(this._data);
			MetaData result = MetaData.Deserialize(memoryStream);
			memoryStream.Close();
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public LoadData Load(string saveName)
		{
			MemoryStream memoryStream = new MemoryStream(this._data);
			MetaData metaData = MetaData.Deserialize(memoryStream);
			byte[] array = new byte[memoryStream.Length - memoryStream.Position];
			memoryStream.Read(array, 0, array.Length);
			GameData gameData = GameData.CreateFrom(array);
			return new LoadData(metaData, gameData);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C24 File Offset: 0x00000E24
		public SaveGameFileInfo[] GetSaveGameFileInfos()
		{
			return new SaveGameFileInfo[0];
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C2C File Offset: 0x00000E2C
		public string[] GetSaveGameFileNames()
		{
			return new string[0];
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C34 File Offset: 0x00000E34
		public bool Delete(string saveName)
		{
			this._data = new byte[0];
			return true;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C43 File Offset: 0x00000E43
		public bool IsSaveGameFileExists(string saveName)
		{
			return false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C46 File Offset: 0x00000E46
		public bool IsWorkingAsync()
		{
			return false;
		}

		// Token: 0x04000017 RID: 23
		private byte[] _data;
	}
}
