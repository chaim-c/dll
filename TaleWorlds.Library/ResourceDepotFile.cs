using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000081 RID: 129
	public class ResourceDepotFile
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000F9FE File Offset: 0x0000DBFE
		public ResourceDepotLocation ResourceDepotLocation { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000FA07 File Offset: 0x0000DC07
		public string BasePath
		{
			get
			{
				return this.ResourceDepotLocation.BasePath;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public string Location
		{
			get
			{
				return this.ResourceDepotLocation.Path;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000FA21 File Offset: 0x0000DC21
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0000FA29 File Offset: 0x0000DC29
		public string FileName { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000FA32 File Offset: 0x0000DC32
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0000FA3A File Offset: 0x0000DC3A
		public string FullPath { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000FA43 File Offset: 0x0000DC43
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000FA4B File Offset: 0x0000DC4B
		public string FullPathLowerCase { get; private set; }

		// Token: 0x06000490 RID: 1168 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public ResourceDepotFile(ResourceDepotLocation resourceDepotLocation, string fileName, string fullPath)
		{
			this.ResourceDepotLocation = resourceDepotLocation;
			this.FileName = fileName;
			this.FullPath = fullPath;
			this.FullPathLowerCase = fullPath.ToLower();
		}
	}
}
