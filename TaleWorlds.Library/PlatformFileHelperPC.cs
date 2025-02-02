using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TaleWorlds.Library
{
	// Token: 0x02000078 RID: 120
	public class PlatformFileHelperPC : IPlatformFileHelper
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000D45A File Offset: 0x0000B65A
		private string DocumentsPath
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000D462 File Offset: 0x0000B662
		private string ProgramDataPath
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000D46B File Offset: 0x0000B66B
		public PlatformFileHelperPC(string applicationName)
		{
			this.ApplicationName = applicationName;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000D47C File Offset: 0x0000B67C
		public SaveResult SaveFile(PlatformFilePath path, byte[] data)
		{
			SaveResult result = SaveResult.PlatformFileHelperFailure;
			PlatformFileHelperPC.Error = "";
			try
			{
				this.CreateDirectory(path.FolderPath);
				File.WriteAllBytes(this.GetFileFullPath(path), data);
				result = SaveResult.Success;
			}
			catch (Exception ex)
			{
				PlatformFileHelperPC.Error = ex.Message;
				result = SaveResult.PlatformFileHelperFailure;
			}
			return result;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		public SaveResult SaveFileString(PlatformFilePath path, string data)
		{
			SaveResult result = SaveResult.PlatformFileHelperFailure;
			PlatformFileHelperPC.Error = "";
			try
			{
				this.CreateDirectory(path.FolderPath);
				File.WriteAllText(this.GetFileFullPath(path), data, Encoding.UTF8);
				result = SaveResult.Success;
			}
			catch (Exception ex)
			{
				PlatformFileHelperPC.Error = ex.Message;
				result = SaveResult.PlatformFileHelperFailure;
			}
			return result;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000D530 File Offset: 0x0000B730
		public Task<SaveResult> SaveFileAsync(PlatformFilePath path, byte[] data)
		{
			return Task.FromResult<SaveResult>(this.SaveFile(path, data));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000D53F File Offset: 0x0000B73F
		public Task<SaveResult> SaveFileStringAsync(PlatformFilePath path, string data)
		{
			return Task.FromResult<SaveResult>(this.SaveFileString(path, data));
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000D550 File Offset: 0x0000B750
		public SaveResult AppendLineToFileString(PlatformFilePath path, string data)
		{
			SaveResult result = SaveResult.PlatformFileHelperFailure;
			PlatformFileHelperPC.Error = "";
			try
			{
				this.CreateDirectory(path.FolderPath);
				File.AppendAllText(this.GetFileFullPath(path), "\n" + data, Encoding.UTF8);
				result = SaveResult.Success;
			}
			catch (Exception ex)
			{
				PlatformFileHelperPC.Error = ex.Message;
				result = SaveResult.PlatformFileHelperFailure;
			}
			return result;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		private string GetDirectoryFullPath(PlatformDirectoryPath directoryPath)
		{
			string path = "";
			switch (directoryPath.Type)
			{
			case PlatformFileType.User:
				path = Path.Combine(this.DocumentsPath, this.ApplicationName);
				break;
			case PlatformFileType.Application:
				path = Path.Combine(this.ProgramDataPath, this.ApplicationName);
				break;
			case PlatformFileType.Temporary:
				path = Path.Combine(this.DocumentsPath, this.ApplicationName, "Temp");
				break;
			}
			return Path.Combine(path, directoryPath.Path);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000D62D File Offset: 0x0000B82D
		public string GetFileFullPath(PlatformFilePath filePath)
		{
			return Path.GetFullPath(Path.Combine(this.GetDirectoryFullPath(filePath.FolderPath), filePath.FileName));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000D64B File Offset: 0x0000B84B
		public bool FileExists(PlatformFilePath path)
		{
			return File.Exists(this.GetFileFullPath(path));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000D65C File Offset: 0x0000B85C
		public async Task<string> GetFileContentStringAsync(PlatformFilePath path)
		{
			string result;
			if (!this.FileExists(path))
			{
				result = null;
			}
			else
			{
				string fileFullPath = this.GetFileFullPath(path);
				string text = string.Empty;
				using (FileStream sourceStream = File.Open(fileFullPath, FileMode.Open))
				{
					byte[] buffer = new byte[sourceStream.Length];
					await sourceStream.ReadAsync(buffer, 0, (int)sourceStream.Length);
					text = Encoding.UTF8.GetString(buffer);
					buffer = null;
				}
				FileStream sourceStream = null;
				result = text;
			}
			return result;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		public string GetFileContentString(PlatformFilePath path)
		{
			if (!this.FileExists(path))
			{
				return null;
			}
			string fileFullPath = this.GetFileFullPath(path);
			string result = null;
			PlatformFileHelperPC.Error = "";
			try
			{
				result = File.ReadAllText(fileFullPath, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				PlatformFileHelperPC.Error = ex.Message;
				Debug.Print(PlatformFileHelperPC.Error, 0, Debug.DebugColor.White, 17592186044416UL);
			}
			return result;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000D71C File Offset: 0x0000B91C
		public byte[] GetFileContent(PlatformFilePath path)
		{
			if (!this.FileExists(path))
			{
				return null;
			}
			string fileFullPath = this.GetFileFullPath(path);
			byte[] result = null;
			PlatformFileHelperPC.Error = "";
			try
			{
				result = File.ReadAllBytes(fileFullPath);
			}
			catch (Exception ex)
			{
				PlatformFileHelperPC.Error = ex.Message;
				Debug.Print(PlatformFileHelperPC.Error, 0, Debug.DebugColor.White, 17592186044416UL);
			}
			return result;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000D784 File Offset: 0x0000B984
		public bool DeleteFile(PlatformFilePath path)
		{
			string fileFullPath = this.GetFileFullPath(path);
			if (!this.FileExists(path))
			{
				return false;
			}
			bool result;
			try
			{
				File.Delete(fileFullPath);
				result = true;
			}
			catch (Exception ex)
			{
				PlatformFileHelperPC.Error = ex.Message;
				Debug.Print(PlatformFileHelperPC.Error, 0, Debug.DebugColor.White, 17592186044416UL);
				result = false;
			}
			return result;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		public void CreateDirectory(PlatformDirectoryPath path)
		{
			Directory.CreateDirectory(this.GetDirectoryFullPath(path));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public PlatformFilePath[] GetFiles(PlatformDirectoryPath path, string searchPattern)
		{
			string directoryFullPath = this.GetDirectoryFullPath(path);
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryFullPath);
			PlatformFilePath[] array = null;
			PlatformFileHelperPC.Error = "";
			if (directoryInfo.Exists)
			{
				try
				{
					FileInfo[] files = directoryInfo.GetFiles(searchPattern, SearchOption.AllDirectories);
					array = new PlatformFilePath[files.Length];
					for (int i = 0; i < files.Length; i++)
					{
						FileInfo fileInfo = files[i];
						fileInfo.FullName.Substring(directoryFullPath.Length);
						PlatformFilePath platformFilePath = new PlatformFilePath(path, fileInfo.Name);
						array[i] = platformFilePath;
					}
					return array;
				}
				catch (Exception ex)
				{
					PlatformFileHelperPC.Error = ex.Message;
					return array;
				}
			}
			array = new PlatformFilePath[0];
			return array;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		public void RenameFile(PlatformFilePath filePath, string newName)
		{
			string fileFullPath = this.GetFileFullPath(filePath);
			string fileFullPath2 = this.GetFileFullPath(new PlatformFilePath(filePath.FolderPath, newName));
			File.Move(fileFullPath, fileFullPath2);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000D8CD File Offset: 0x0000BACD
		public string GetError()
		{
			return PlatformFileHelperPC.Error;
		}

		// Token: 0x0400013A RID: 314
		private readonly string ApplicationName;

		// Token: 0x0400013B RID: 315
		private static string Error;
	}
}
