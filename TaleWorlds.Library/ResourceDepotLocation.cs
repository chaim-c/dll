using System;
using System.IO;

namespace TaleWorlds.Library
{
	// Token: 0x02000082 RID: 130
	public class ResourceDepotLocation
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000FA7D File Offset: 0x0000DC7D
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000FA85 File Offset: 0x0000DC85
		public string BasePath { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000FA8E File Offset: 0x0000DC8E
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000FA96 File Offset: 0x0000DC96
		public string Path { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000FA9F File Offset: 0x0000DC9F
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0000FAA7 File Offset: 0x0000DCA7
		public string FullPath { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		public FileSystemWatcher Watcher { get; private set; }

		// Token: 0x06000499 RID: 1177 RVA: 0x0000FAC1 File Offset: 0x0000DCC1
		public ResourceDepotLocation(string basePath, string path, string fullPath)
		{
			this.BasePath = basePath;
			this.Path = path;
			this.FullPath = fullPath;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000FAE0 File Offset: 0x0000DCE0
		public void StartWatchingChanges(FileSystemEventHandler onChangeEvent, RenamedEventHandler onRenameEvent)
		{
			this.Watcher = new FileSystemWatcher
			{
				Path = this.FullPath,
				NotifyFilter = (NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.CreationTime),
				Filter = "*.*",
				IncludeSubdirectories = true,
				EnableRaisingEvents = true
			};
			this.Watcher.Changed += onChangeEvent;
			this.Watcher.Created += onChangeEvent;
			this.Watcher.Deleted += onChangeEvent;
			this.Watcher.Renamed += onRenameEvent;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000FB55 File Offset: 0x0000DD55
		public void StopWatchingChanges()
		{
			this.Watcher.Dispose();
		}
	}
}
