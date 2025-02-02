using System;
using System.Collections.Generic;
using System.IO;

namespace TaleWorlds.Library
{
	// Token: 0x02000080 RID: 128
	public class ResourceDepot
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000478 RID: 1144 RVA: 0x0000F610 File Offset: 0x0000D810
		// (remove) Token: 0x06000479 RID: 1145 RVA: 0x0000F648 File Offset: 0x0000D848
		public event ResourceChangeEvent OnResourceChange;

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000F67D File Offset: 0x0000D87D
		public MBReadOnlyList<ResourceDepotLocation> ResourceLocations
		{
			get
			{
				return this._resourceLocations;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000F685 File Offset: 0x0000D885
		public ResourceDepot()
		{
			this._resourceLocations = new MBList<ResourceDepotLocation>();
			this._files = new Dictionary<string, ResourceDepotFile>();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		public void AddLocation(string basePath, string location)
		{
			ResourceDepotLocation item = new ResourceDepotLocation(basePath, location, Path.GetFullPath(basePath + location));
			this._resourceLocations.Add(item);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		public void CollectResources()
		{
			this._files.Clear();
			foreach (ResourceDepotLocation resourceDepotLocation in this._resourceLocations)
			{
				string fullPath = Path.GetFullPath(resourceDepotLocation.BasePath + resourceDepotLocation.Path);
				string[] files = Directory.GetFiles(resourceDepotLocation.BasePath + resourceDepotLocation.Path, "*", SearchOption.AllDirectories);
				for (int i = 0; i < files.Length; i++)
				{
					string text = Path.GetFullPath(files[i]);
					text = text.Replace('\\', '/');
					string text2 = text.Substring(fullPath.Length);
					string key = text2.ToLower();
					ResourceDepotFile value = new ResourceDepotFile(resourceDepotLocation, text2, text);
					if (this._files.ContainsKey(key))
					{
						this._files[key] = value;
					}
					else
					{
						this._files.Add(key, value);
					}
				}
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		public string[] GetFiles(string subDirectory, string extension, bool excludeSubContents = false)
		{
			string value = extension.ToLower();
			List<string> list = new List<string>();
			foreach (ResourceDepotFile resourceDepotFile in this._files.Values)
			{
				string text = Path.GetFullPath(resourceDepotFile.BasePath + resourceDepotFile.Location + subDirectory).Replace('\\', '/').ToLower();
				string fullPath = resourceDepotFile.FullPath;
				string fullPathLowerCase = resourceDepotFile.FullPathLowerCase;
				bool flag = (!excludeSubContents && fullPathLowerCase.StartsWith(text)) || (excludeSubContents && string.Equals(Directory.GetParent(text).FullName, text, StringComparison.CurrentCultureIgnoreCase));
				bool flag2 = fullPathLowerCase.EndsWith(value, StringComparison.OrdinalIgnoreCase);
				if (flag && flag2)
				{
					list.Add(fullPath);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
		public string GetFilePath(string file)
		{
			file = file.Replace('\\', '/');
			return this._files[file.ToLower()].FullPath;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		public IEnumerable<string> GetFilesEndingWith(string fileEndName)
		{
			fileEndName = fileEndName.Replace('\\', '/');
			foreach (KeyValuePair<string, ResourceDepotFile> keyValuePair in this._files)
			{
				if (keyValuePair.Key.EndsWith(fileEndName.ToLower()))
				{
					yield return keyValuePair.Value.FullPath;
				}
			}
			Dictionary<string, ResourceDepotFile>.Enumerator enumerator = default(Dictionary<string, ResourceDepotFile>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000F904 File Offset: 0x0000DB04
		public void StartWatchingChangesInDepot()
		{
			foreach (ResourceDepotLocation resourceDepotLocation in this._resourceLocations)
			{
				resourceDepotLocation.StartWatchingChanges(new FileSystemEventHandler(this.OnAnyChangeInDepotLocations), new RenamedEventHandler(this.OnAnyRenameInDepotLocations));
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000F96C File Offset: 0x0000DB6C
		public void StopWatchingChangesInDepot()
		{
			foreach (ResourceDepotLocation resourceDepotLocation in this._resourceLocations)
			{
				resourceDepotLocation.StopWatchingChanges();
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		private void OnAnyChangeInDepotLocations(object source, FileSystemEventArgs e)
		{
			this._isThereAnyUnhandledChange = true;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000F9C5 File Offset: 0x0000DBC5
		private void OnAnyRenameInDepotLocations(object source, RenamedEventArgs e)
		{
			this._isThereAnyUnhandledChange = true;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000F9CE File Offset: 0x0000DBCE
		public void CheckForChanges()
		{
			if (this._isThereAnyUnhandledChange)
			{
				this.CollectResources();
				ResourceChangeEvent onResourceChange = this.OnResourceChange;
				if (onResourceChange != null)
				{
					onResourceChange();
				}
				this._isThereAnyUnhandledChange = false;
			}
		}

		// Token: 0x0400014A RID: 330
		private readonly MBList<ResourceDepotLocation> _resourceLocations;

		// Token: 0x0400014B RID: 331
		private readonly Dictionary<string, ResourceDepotFile> _files;

		// Token: 0x0400014C RID: 332
		private bool _isThereAnyUnhandledChange;
	}
}
