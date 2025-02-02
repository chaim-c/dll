using System;
using System.Threading.Tasks;
using TaleWorlds.Diamond.Rest;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000120 RID: 288
	public static class Gatekeeper
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x000086E8 File Offset: 0x000068E8
		public static async Task<bool> IsGenerous()
		{
			bool result;
			if (Gatekeeper._isDeployBuild)
			{
				if (Gatekeeper._random == null)
				{
					Gatekeeper._random = new Random(MachineId.AsInteger());
					Gatekeeper._roll = Gatekeeper._random.Next() % 101;
				}
				int num = await Gatekeeper.GetAdmittancePercentage();
				result = (Gatekeeper._roll <= num);
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00008728 File Offset: 0x00006928
		private static async Task<int> GetAdmittancePercentage()
		{
			int result;
			try
			{
				string json = await HttpHelper.DownloadStringTaskAsync("https://taleworldswebsiteassets.blob.core.windows.net/upload/blconfig.json").ConfigureAwait(false);
				result = new RestDataJsonConverter().ReadJson<BannerlordConfig>(json).AdmittancePercentage;
			}
			catch (Exception)
			{
				result = 100;
			}
			return result;
		}

		// Token: 0x040002A0 RID: 672
		private static Random _random;

		// Token: 0x040002A1 RID: 673
		private static int _roll;

		// Token: 0x040002A2 RID: 674
		private static readonly bool _isDeployBuild;
	}
}
