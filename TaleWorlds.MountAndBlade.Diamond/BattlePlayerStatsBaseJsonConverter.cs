using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F6 RID: 246
	public class BattlePlayerStatsBaseJsonConverter : JsonConverter
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x00005697 File Offset: 0x00003897
		public override bool CanConvert(Type objectType)
		{
			return typeof(BattlePlayerStatsBase).IsAssignableFrom(objectType);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000056AC File Offset: 0x000038AC
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jobject = JObject.Load(reader);
			string a = (string)jobject["GameType"];
			BattlePlayerStatsBase battlePlayerStatsBase;
			if (a == "Skirmish")
			{
				battlePlayerStatsBase = new BattlePlayerStatsSkirmish();
			}
			else if (a == "Captain")
			{
				battlePlayerStatsBase = new BattlePlayerStatsCaptain();
			}
			else if (a == "Siege")
			{
				battlePlayerStatsBase = new BattlePlayerStatsSiege();
			}
			else if (a == "TeamDeathmatch")
			{
				battlePlayerStatsBase = new BattlePlayerStatsTeamDeathmatch();
			}
			else if (a == "Duel")
			{
				battlePlayerStatsBase = new BattlePlayerStatsDuel();
			}
			else
			{
				if (!(a == "Battle"))
				{
					return null;
				}
				battlePlayerStatsBase = new BattlePlayerStatsBattle();
			}
			serializer.Populate(jobject.CreateReader(), battlePlayerStatsBase);
			return battlePlayerStatsBase;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00005760 File Offset: 0x00003960
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00005763 File Offset: 0x00003963
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
