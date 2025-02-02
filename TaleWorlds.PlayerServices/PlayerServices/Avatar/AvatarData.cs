using System;

namespace TaleWorlds.PlayerServices.Avatar
{
	// Token: 0x02000009 RID: 9
	public class AvatarData
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002E5A File Offset: 0x0000105A
		public AvatarData()
		{
			this.Status = AvatarData.DataStatus.NotReady;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E69 File Offset: 0x00001069
		public AvatarData(byte[] image, uint width, uint height)
		{
			this.SetImageData(image, width, height);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E7A File Offset: 0x0000107A
		public AvatarData(byte[] image)
		{
			this.SetImageData(image);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E89 File Offset: 0x00001089
		public void SetImageData(byte[] image, uint width, uint height)
		{
			this.Image = image;
			this.Width = width;
			this.Height = height;
			this.Type = AvatarData.ImageType.Raw;
			this.Status = AvatarData.DataStatus.Ready;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EAE File Offset: 0x000010AE
		public void SetImageData(byte[] image)
		{
			this.Image = image;
			this.Type = AvatarData.ImageType.Image;
			this.Status = AvatarData.DataStatus.Ready;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EC5 File Offset: 0x000010C5
		public void SetFailed()
		{
			this.Status = AvatarData.DataStatus.Failed;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002ECE File Offset: 0x000010CE
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002ED6 File Offset: 0x000010D6
		public byte[] Image { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002EDF File Offset: 0x000010DF
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002EE7 File Offset: 0x000010E7
		public uint Width { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002EF0 File Offset: 0x000010F0
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002EF8 File Offset: 0x000010F8
		public uint Height { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F01 File Offset: 0x00001101
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002F09 File Offset: 0x00001109
		public AvatarData.ImageType Type { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002F12 File Offset: 0x00001112
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002F1A File Offset: 0x0000111A
		public AvatarData.DataStatus Status { get; private set; }

		// Token: 0x02000011 RID: 17
		public enum ImageType
		{
			// Token: 0x0400003D RID: 61
			Image,
			// Token: 0x0400003E RID: 62
			Raw
		}

		// Token: 0x02000012 RID: 18
		public enum DataStatus
		{
			// Token: 0x04000040 RID: 64
			NotReady,
			// Token: 0x04000041 RID: 65
			Ready,
			// Token: 0x04000042 RID: 66
			Failed
		}
	}
}
