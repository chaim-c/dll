using System;
using System.Linq;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x02000018 RID: 24
	public class UserData
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000558A File Offset: 0x0000378A
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005592 File Offset: 0x00003792
		public GameType GameType { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000559B File Offset: 0x0000379B
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000055A3 File Offset: 0x000037A3
		public UserGameTypeData SingleplayerData { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000055AC File Offset: 0x000037AC
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000055B4 File Offset: 0x000037B4
		public UserGameTypeData MultiplayerData { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000055BD File Offset: 0x000037BD
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000055C5 File Offset: 0x000037C5
		public DLLCheckDataCollection DLLCheckData { get; set; }

		// Token: 0x06000109 RID: 265 RVA: 0x000055CE File Offset: 0x000037CE
		public UserData()
		{
			this.GameType = GameType.Singleplayer;
			this.SingleplayerData = new UserGameTypeData();
			this.MultiplayerData = new UserGameTypeData();
			this.DLLCheckData = new DLLCheckDataCollection();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005600 File Offset: 0x00003800
		public UserModData GetUserModData(bool isMultiplayer, string id)
		{
			return (isMultiplayer ? this.MultiplayerData : this.SingleplayerData).ModDatas.Find((UserModData x) => x.Id == id);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005644 File Offset: 0x00003844
		public uint? GetDLLLatestSizeInBytes(string dllName)
		{
			DLLCheckData dllcheckData = this.DLLCheckData.DLLData.FirstOrDefault((DLLCheckData d) => d.DLLName == dllName);
			if (dllcheckData == null)
			{
				return null;
			}
			return new uint?(dllcheckData.LatestSizeInBytes);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005694 File Offset: 0x00003894
		public bool GetDLLLatestIsDangerous(string dllName)
		{
			DLLCheckData dllcheckData = this.DLLCheckData.DLLData.FirstOrDefault((DLLCheckData d) => d.DLLName == dllName);
			return dllcheckData == null || dllcheckData.IsDangerous;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000056D8 File Offset: 0x000038D8
		public string GetDLLLatestVerifyInformation(string dllName)
		{
			DLLCheckData dllcheckData = this.DLLCheckData.DLLData.FirstOrDefault((DLLCheckData d) => d.DLLName == dllName);
			return ((dllcheckData != null) ? dllcheckData.DLLVerifyInformation : null) ?? "";
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005724 File Offset: 0x00003924
		public void SetDLLLatestSizeInBytes(string dllName, uint sizeInBytes)
		{
			this.EnsureDLLIsAdded(dllName);
			this.DLLCheckData.DLLData.Find((DLLCheckData d) => d.DLLName == dllName).LatestSizeInBytes = sizeInBytes;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000576C File Offset: 0x0000396C
		public void SetDLLLatestVerifyInformation(string dllName, string verifyInformation)
		{
			this.EnsureDLLIsAdded(dllName);
			this.DLLCheckData.DLLData.Find((DLLCheckData d) => d.DLLName == dllName).DLLVerifyInformation = verifyInformation;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000057B4 File Offset: 0x000039B4
		public void SetDLLLatestIsDangerous(string dllName, bool isDangerous)
		{
			this.EnsureDLLIsAdded(dllName);
			this.DLLCheckData.DLLData.Find((DLLCheckData d) => d.DLLName == dllName).IsDangerous = isDangerous;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000057FC File Offset: 0x000039FC
		private void EnsureDLLIsAdded(string dllName)
		{
			if (!this.DLLCheckData.DLLData.Any((DLLCheckData d) => d.DLLName == dllName))
			{
				this.DLLCheckData.DLLData.Add(new DLLCheckData(dllName));
			}
		}
	}
}
