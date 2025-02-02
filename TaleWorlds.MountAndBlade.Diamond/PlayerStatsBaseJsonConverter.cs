using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaleWorlds.Diamond;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014F RID: 335
	public class PlayerStatsBaseJsonConverter : JsonConverter
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x0000DAD2 File Offset: 0x0000BCD2
		public override bool CanConvert(Type objectType)
		{
			return typeof(AccessObject).IsAssignableFrom(objectType);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jobject = JObject.Load(reader);
			string text = (string)jobject["gameType"];
			if (text == null)
			{
				text = (string)jobject["GameType"];
			}
			PlayerStatsBase playerStatsBase;
			if (text == "Skirmish")
			{
				playerStatsBase = new PlayerStatsSkirmish();
			}
			else if (text == "Captain")
			{
				playerStatsBase = new PlayerStatsCaptain();
			}
			else if (text == "TeamDeathmatch")
			{
				playerStatsBase = new PlayerStatsTeamDeathmatch();
			}
			else if (text == "Siege")
			{
				playerStatsBase = new PlayerStatsSiege();
			}
			else if (text == "Duel")
			{
				playerStatsBase = new PlayerStatsDuel();
			}
			else
			{
				if (!(text == "Battle"))
				{
					return null;
				}
				playerStatsBase = new PlayerStatsBattle();
			}
			serializer.Populate(jobject.CreateReader(), playerStatsBase);
			return playerStatsBase;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0000DBAF File Offset: 0x0000BDAF
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
