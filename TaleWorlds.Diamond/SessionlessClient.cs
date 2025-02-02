using System;
using System.Threading.Tasks;
using TaleWorlds.Diamond.ClientApplication;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000025 RID: 37
	public abstract class SessionlessClient<T> : DiamondClientApplicationObject, ISessionlessClient where T : SessionlessClient<T>
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x0000334D File Offset: 0x0000154D
		protected SessionlessClient(DiamondClientApplication diamondClientApplication, ISessionlessClientDriverProvider<T> driverProvider) : base(diamondClientApplication)
		{
			this._clientDriver = driverProvider.CreateDriver((T)((object)this));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003368 File Offset: 0x00001568
		protected void SendMessage(Message message)
		{
			this._clientDriver.SendMessage(message);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003378 File Offset: 0x00001578
		protected async Task<TResult> CallFunction<TResult>(Message message) where TResult : FunctionResult
		{
			return await this._clientDriver.CallFunction<TResult>(message);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000033C5 File Offset: 0x000015C5
		public Task<bool> CheckConnection()
		{
			return this._clientDriver.CheckConnection();
		}

		// Token: 0x04000031 RID: 49
		private ISessionlessClientDriver _clientDriver;
	}
}
