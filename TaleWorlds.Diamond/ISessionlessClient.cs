using System;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000014 RID: 20
	public interface ISessionlessClient
	{
		// Token: 0x06000060 RID: 96
		Task<bool> CheckConnection();
	}
}
