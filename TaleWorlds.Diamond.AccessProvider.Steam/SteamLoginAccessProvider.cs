using System;
using System.Text;
using Steamworks;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.Diamond.AccessProvider.Steam
{
	// Token: 0x02000002 RID: 2
	public class SteamLoginAccessProvider : ILoginAccessProvider
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
		void ILoginAccessProvider.Initialize(string preferredUserName, PlatformInitParams initParams)
		{
			if (SteamAPI.Init() && Packsize.Test())
			{
				this._appId = SteamUtils.GetAppID().m_AppId;
				this._steamId = SteamUser.GetSteamID().m_SteamID;
				this._steamUserName = SteamFriends.GetPersonaName();
				this._initParams = initParams;
				byte[] array = new byte[1042];
				SteamAPICall_t hAPICall = SteamUser.RequestEncryptedAppTicket(Encoding.UTF8.GetBytes(""), array.Length);
				CallResult<EncryptedAppTicketResponse_t>.Create(new CallResult<EncryptedAppTicketResponse_t>.APIDispatchDelegate(this.EncryptedAppTicketResponseReceived)).Set(hAPICall, null);
				while (this._appTicket == null)
				{
					SteamAPI.RunCallbacks();
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E2 File Offset: 0x000002E2
		private int AppId
		{
			get
			{
				return Convert.ToInt32(this._appId);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020EF File Offset: 0x000002EF
		string ILoginAccessProvider.GetUserName()
		{
			return this._steamUserName;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F7 File Offset: 0x000002F7
		PlayerId ILoginAccessProvider.GetPlayerId()
		{
			return new PlayerId(2, 0UL, this._steamId);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		AccessObjectResult ILoginAccessProvider.CreateAccessObject()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return AccessObjectResult.CreateFailed(new TextObject("{=uunRVBPN}Steam is not running.", null));
			}
			byte[] array = new byte[1024];
			uint num;
			if (SteamUser.GetAuthSessionTicket(array, 1024, out num) == HAuthTicket.Invalid)
			{
				return AccessObjectResult.CreateFailed(new TextObject("{=XSU8Bbbb}Invalid Steam session.", null));
			}
			StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
			foreach (byte b in array)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			string externalAccessToken = stringBuilder.ToString();
			return AccessObjectResult.CreateSuccess(new SteamAccessObject(this._steamUserName, externalAccessToken, this.AppId, this._appTicket));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021C4 File Offset: 0x000003C4
		private void EncryptedAppTicketResponseReceived(EncryptedAppTicketResponse_t response, bool bIOFailure)
		{
			byte[] array = new byte[2048];
			uint num;
			SteamUser.GetEncryptedAppTicket(array, 2048, out num);
			byte[] array2 = new byte[num];
			Array.Copy(array, array2, (long)((ulong)num));
			this._appTicket = BitConverter.ToString(array2).Replace("-", "");
		}

		// Token: 0x04000001 RID: 1
		private string _steamUserName;

		// Token: 0x04000002 RID: 2
		private ulong _steamId;

		// Token: 0x04000003 RID: 3
		private PlatformInitParams _initParams;

		// Token: 0x04000004 RID: 4
		private uint _appId;

		// Token: 0x04000005 RID: 5
		private string _appTicket;
	}
}
