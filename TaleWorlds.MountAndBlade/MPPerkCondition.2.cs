using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000302 RID: 770
	public abstract class MPPerkCondition<T> : MPPerkCondition where T : MissionMultiplayerGameModeBase
	{
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000A1A60 File Offset: 0x0009FC60
		protected T GameModeInstance
		{
			get
			{
				Mission mission = Mission.Current;
				if (mission == null)
				{
					return default(T);
				}
				return mission.GetMissionBehavior<T>();
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000A1A88 File Offset: 0x0009FC88
		protected override bool IsGameModesValid(List<string> gameModes)
		{
			if (typeof(MissionMultiplayerFlagDomination).IsAssignableFrom(typeof(T)))
			{
				string value = MultiplayerGameType.Skirmish.ToString();
				string value2 = MultiplayerGameType.Captain.ToString();
				foreach (string text in gameModes)
				{
					if (!text.Equals(value, StringComparison.InvariantCultureIgnoreCase) && !text.Equals(value2, StringComparison.InvariantCultureIgnoreCase))
					{
						return false;
					}
				}
				return true;
			}
			if (typeof(MissionMultiplayerTeamDeathmatch).IsAssignableFrom(typeof(T)))
			{
				string value3 = MultiplayerGameType.TeamDeathmatch.ToString();
				using (List<string>.Enumerator enumerator = gameModes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.Equals(value3, StringComparison.InvariantCultureIgnoreCase))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (typeof(MissionMultiplayerSiege).IsAssignableFrom(typeof(T)))
			{
				string value4 = MultiplayerGameType.Siege.ToString();
				using (List<string>.Enumerator enumerator = gameModes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.Equals(value4, StringComparison.InvariantCultureIgnoreCase))
						{
							return false;
						}
					}
				}
				return true;
			}
			Debug.FailedAssert("Not implemented game mode check", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\Perks\\MPPerkCondition.cs", "IsGameModesValid", 134);
			return false;
		}
	}
}
