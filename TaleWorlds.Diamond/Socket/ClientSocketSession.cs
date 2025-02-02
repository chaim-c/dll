using System;
using System.Threading.Tasks;
using TaleWorlds.Network;

namespace TaleWorlds.Diamond.Socket
{
	// Token: 0x02000035 RID: 53
	public abstract class ClientSocketSession : ClientsideSession, IClientSession
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00003D11 File Offset: 0x00001F11
		protected ClientSocketSession(IClient client, string address, int port)
		{
			this._client = client;
			this._address = address;
			this._port = port;
			base.AddMessageHandler<SocketMessage>(new MessageContractHandlerDelegate<SocketMessage>(this.HandleSocketMessage));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003D40 File Offset: 0x00001F40
		private void HandleSocketMessage(SocketMessage socketMessage)
		{
			Message message = socketMessage.Message;
			this._client.HandleMessage(message);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003D60 File Offset: 0x00001F60
		protected override void OnConnected()
		{
			base.OnConnected();
			this._client.OnConnected();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003D73 File Offset: 0x00001F73
		protected override void OnCantConnect()
		{
			base.OnCantConnect();
			this._client.OnCantConnect();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003D86 File Offset: 0x00001F86
		protected override void OnDisconnected()
		{
			base.OnDisconnected();
			this._client.OnDisconnected();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003D99 File Offset: 0x00001F99
		void IClientSession.Connect()
		{
			this.Connect(this._address, this._port, true);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003DAE File Offset: 0x00001FAE
		void IClientSession.Disconnect()
		{
			base.SendDisconnectMessage();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003DB6 File Offset: 0x00001FB6
		Task<TReturn> IClientSession.CallFunction<TReturn>(Message message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003DBD File Offset: 0x00001FBD
		void IClientSession.SendMessage(Message message)
		{
			base.SendMessage(new SocketMessage(message));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003DCB File Offset: 0x00001FCB
		Task<LoginResult> IClientSession.Login(LoginMessage message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003DD2 File Offset: 0x00001FD2
		Task<bool> IClientSession.CheckConnection()
		{
			return Task.FromResult<bool>(true);
		}

		// Token: 0x0400004D RID: 77
		private string _address;

		// Token: 0x0400004E RID: 78
		private int _port;

		// Token: 0x0400004F RID: 79
		private IClient _client;
	}
}
