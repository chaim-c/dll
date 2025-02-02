using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000193 RID: 403
	public static class BannerlordConfig
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0004E564 File Offset: 0x0004C764
		public static int MinBattleSize
		{
			get
			{
				return BannerlordConfig._battleSizes[0];
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0004E56D File Offset: 0x0004C76D
		public static int MaxBattleSize
		{
			get
			{
				return BannerlordConfig._battleSizes[BannerlordConfig._battleSizes.Length - 1];
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0004E57E File Offset: 0x0004C77E
		public static int MinReinforcementWaveCount
		{
			get
			{
				return BannerlordConfig._reinforcementWaveCounts[0];
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0004E587 File Offset: 0x0004C787
		public static int MaxReinforcementWaveCount
		{
			get
			{
				return BannerlordConfig._reinforcementWaveCounts[BannerlordConfig._reinforcementWaveCounts.Length - 1];
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0004E598 File Offset: 0x0004C798
		public static void Initialize()
		{
			string text = Utilities.LoadBannerlordConfigFile();
			if (string.IsNullOrEmpty(text))
			{
				BannerlordConfig.Save();
			}
			else
			{
				bool flag = false;
				string[] array = text.Split(new char[]
				{
					'\n'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'='
					});
					PropertyInfo property = typeof(BannerlordConfig).GetProperty(array2[0]);
					if (property == null)
					{
						flag = true;
					}
					else
					{
						string text2 = array2[1];
						try
						{
							if (property.PropertyType == typeof(string))
							{
								string value = Regex.Replace(text2, "\\r", "");
								property.SetValue(null, value);
							}
							else if (property.PropertyType == typeof(float))
							{
								float num;
								if (float.TryParse(text2, out num))
								{
									property.SetValue(null, num);
								}
								else
								{
									flag = true;
								}
							}
							else if (property.PropertyType == typeof(int))
							{
								int num2;
								if (int.TryParse(text2, out num2))
								{
									BannerlordConfig.ConfigPropertyInt customAttribute = property.GetCustomAttribute<BannerlordConfig.ConfigPropertyInt>();
									if (customAttribute == null || customAttribute.IsValidValue(num2))
									{
										property.SetValue(null, num2);
									}
									else
									{
										flag = true;
									}
								}
								else
								{
									flag = true;
								}
							}
							else if (property.PropertyType == typeof(bool))
							{
								bool flag2;
								if (bool.TryParse(text2, out flag2))
								{
									property.SetValue(null, flag2);
								}
								else
								{
									flag = true;
								}
							}
							else
							{
								flag = true;
								Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\BannerlordConfig.cs", "Initialize", 113);
							}
						}
						catch
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					BannerlordConfig.Save();
				}
				MBAPI.IMBBannerlordConfig.ValidateOptions();
			}
			MBTextManager.TryChangeVoiceLanguage(BannerlordConfig.VoiceLanguage);
			MBTextManager.ChangeLanguage(BannerlordConfig.Language);
			MBTextManager.LocalizationDebugMode = NativeConfig.LocalizationDebugMode;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004E79C File Offset: 0x0004C99C
		public static SaveResult Save()
		{
			Dictionary<PropertyInfo, object> dictionary = new Dictionary<PropertyInfo, object>();
			foreach (PropertyInfo propertyInfo in typeof(BannerlordConfig).GetProperties())
			{
				if (propertyInfo.GetCustomAttribute<BannerlordConfig.ConfigProperty>() != null)
				{
					dictionary.Add(propertyInfo, propertyInfo.GetValue(null, null));
				}
			}
			string text = "";
			foreach (KeyValuePair<PropertyInfo, object> keyValuePair in dictionary)
			{
				text = string.Concat(new string[]
				{
					text,
					keyValuePair.Key.Name,
					"=",
					keyValuePair.Value.ToString(),
					"\n"
				});
			}
			SaveResult result = Utilities.SaveConfigFile(text);
			MBAPI.IMBBannerlordConfig.ValidateOptions();
			return result;
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0004E87C File Offset: 0x0004CA7C
		public static string DefaultLanguage
		{
			get
			{
				return BannerlordConfig.GetDefaultLanguage();
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004E883 File Offset: 0x0004CA83
		public static int GetRealBattleSize()
		{
			return BannerlordConfig._battleSizes[BannerlordConfig.BattleSize];
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004E890 File Offset: 0x0004CA90
		public static int GetRealBattleSizeForSiege()
		{
			return BannerlordConfig._siegeBattleSizes[BannerlordConfig.BattleSize];
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004E89D File Offset: 0x0004CA9D
		public static int GetReinforcementWaveCount()
		{
			return BannerlordConfig._reinforcementWaveCounts[BannerlordConfig.ReinforcementWaveCount];
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004E8AA File Offset: 0x0004CAAA
		public static int GetRealBattleSizeForSallyOut()
		{
			return BannerlordConfig._sallyOutBattleSizes[BannerlordConfig.BattleSize];
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004E8B7 File Offset: 0x0004CAB7
		private static string GetDefaultLanguage()
		{
			return LocalizedTextManager.GetLocalizationCodeOfISOLanguageCode(Utilities.GetSystemLanguage());
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0004E8C3 File Offset: 0x0004CAC3
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x0004E8CC File Offset: 0x0004CACC
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static string Language
		{
			get
			{
				return BannerlordConfig._language;
			}
			set
			{
				if (BannerlordConfig._language != value)
				{
					if (MBTextManager.LanguageExistsInCurrentConfiguration(value, NativeConfig.IsDevelopmentMode) && MBTextManager.ChangeLanguage(value))
					{
						BannerlordConfig._language = value;
					}
					else if (MBTextManager.ChangeLanguage("English"))
					{
						BannerlordConfig._language = "English";
					}
					else
					{
						Debug.FailedAssert("Language cannot be set!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\BannerlordConfig.cs", "Language", 353);
					}
					MBTextManager.LocalizationDebugMode = NativeConfig.LocalizationDebugMode;
				}
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0004E93E File Offset: 0x0004CB3E
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x0004E948 File Offset: 0x0004CB48
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static string VoiceLanguage
		{
			get
			{
				return BannerlordConfig._voiceLanguage;
			}
			set
			{
				if (BannerlordConfig._voiceLanguage != value)
				{
					if (MBTextManager.LanguageExistsInCurrentConfiguration(value, NativeConfig.IsDevelopmentMode) && MBTextManager.TryChangeVoiceLanguage(value))
					{
						BannerlordConfig._voiceLanguage = value;
						return;
					}
					if (MBTextManager.TryChangeVoiceLanguage("English"))
					{
						BannerlordConfig._voiceLanguage = "English";
						return;
					}
					Debug.FailedAssert("Voice Language cannot be set!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\BannerlordConfig.cs", "VoiceLanguage", 380);
				}
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0004E9AE File Offset: 0x0004CBAE
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x0004E9B5 File Offset: 0x0004CBB5
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool GyroOverrideForAttackDefend { get; set; } = false;

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0004E9BD File Offset: 0x0004CBBD
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x0004E9C4 File Offset: 0x0004CBC4
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2
		}, false)]
		public static int AttackDirectionControl { get; set; } = 1;

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0004E9CC File Offset: 0x0004CBCC
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x0004E9D3 File Offset: 0x0004CBD3
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2
		}, false)]
		public static int DefendDirectionControl { get; set; } = 0;

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0004E9DB File Offset: 0x0004CBDB
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x0004E9E2 File Offset: 0x0004CBE2
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2,
			3,
			4,
			5
		}, false)]
		public static int NumberOfCorpses
		{
			get
			{
				return BannerlordConfig._numberOfCorpses;
			}
			set
			{
				BannerlordConfig._numberOfCorpses = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0004E9EA File Offset: 0x0004CBEA
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x0004E9F1 File Offset: 0x0004CBF1
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool ShowBlood { get; set; } = true;

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0004E9F9 File Offset: 0x0004CBF9
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x0004EA00 File Offset: 0x0004CC00
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool DisplayAttackDirection { get; set; } = true;

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0004EA08 File Offset: 0x0004CC08
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x0004EA0F File Offset: 0x0004CC0F
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool DisplayTargetingReticule { get; set; } = true;

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0004EA17 File Offset: 0x0004CC17
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x0004EA1E File Offset: 0x0004CC1E
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool ForceVSyncInMenus { get; set; } = true;

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0004EA26 File Offset: 0x0004CC26
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x0004EA2D File Offset: 0x0004CC2D
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6
		}, false)]
		public static int BattleSize
		{
			get
			{
				return BannerlordConfig._battleSize;
			}
			set
			{
				BannerlordConfig._battleSize = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0004EA35 File Offset: 0x0004CC35
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x0004EA3C File Offset: 0x0004CC3C
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2,
			3
		}, false)]
		public static int ReinforcementWaveCount { get; set; } = 3;

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0004EA44 File Offset: 0x0004CC44
		public static float CivilianAgentCount
		{
			get
			{
				return (float)BannerlordConfig.GetRealBattleSize() * 0.5f;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0004EA52 File Offset: 0x0004CC52
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x0004EA59 File Offset: 0x0004CC59
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static float FirstPersonFov { get; set; } = 65f;

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0004EA61 File Offset: 0x0004CC61
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0004EA68 File Offset: 0x0004CC68
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static float UIScale { get; set; } = 1f;

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0004EA70 File Offset: 0x0004CC70
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x0004EA77 File Offset: 0x0004CC77
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static float CombatCameraDistance { get; set; } = 1f;

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0004EA7F File Offset: 0x0004CC7F
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x0004EA86 File Offset: 0x0004CC86
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2,
			3
		}, false)]
		public static int TurnCameraWithHorseInFirstPerson { get; set; } = 2;

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0004EA8E File Offset: 0x0004CC8E
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0004EA95 File Offset: 0x0004CC95
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool ReportDamage { get; set; } = true;

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0004EA9D File Offset: 0x0004CC9D
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0004EAA4 File Offset: 0x0004CCA4
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool ReportBark { get; set; } = true;

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0004EAAC File Offset: 0x0004CCAC
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x0004EAB3 File Offset: 0x0004CCB3
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool LockTarget { get; set; } = false;

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0004EABB File Offset: 0x0004CCBB
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x0004EAC2 File Offset: 0x0004CCC2
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableTutorialHints { get; set; } = true;

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0004EACA File Offset: 0x0004CCCA
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x0004EAD1 File Offset: 0x0004CCD1
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static int AutoSaveInterval
		{
			get
			{
				return BannerlordConfig._autoSaveInterval;
			}
			set
			{
				if (value == 4)
				{
					BannerlordConfig._autoSaveInterval = -1;
					return;
				}
				BannerlordConfig._autoSaveInterval = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0004EAE4 File Offset: 0x0004CCE4
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0004EAEB File Offset: 0x0004CCEB
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static float FriendlyTroopsBannerOpacity { get; set; } = 1f;

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0004EAF3 File Offset: 0x0004CCF3
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x0004EAFA File Offset: 0x0004CCFA
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2
		}, false)]
		public static int ReportCasualtiesType { get; set; } = 0;

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0004EB02 File Offset: 0x0004CD02
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x0004EB09 File Offset: 0x0004CD09
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2
		}, false)]
		public static int AutoTrackAttackedSettlements { get; set; } = 0;

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0004EB11 File Offset: 0x0004CD11
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x0004EB18 File Offset: 0x0004CD18
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool ReportPersonalDamage { get; set; } = true;

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0004EB20 File Offset: 0x0004CD20
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0004EB27 File Offset: 0x0004CD27
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool SlowDownOnOrder { get; set; } = true;

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0004EB2F File Offset: 0x0004CD2F
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0004EB36 File Offset: 0x0004CD36
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool StopGameOnFocusLost
		{
			get
			{
				return BannerlordConfig._stopGameOnFocusLost;
			}
			set
			{
				BannerlordConfig._stopGameOnFocusLost = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0004EB3E File Offset: 0x0004CD3E
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x0004EB45 File Offset: 0x0004CD45
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool ReportExperience { get; set; } = true;

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0004EB4D File Offset: 0x0004CD4D
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x0004EB54 File Offset: 0x0004CD54
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableDamageTakenVisuals { get; set; } = true;

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0004EB5C File Offset: 0x0004CD5C
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0004EB63 File Offset: 0x0004CD63
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableVerticalAimCorrection { get; set; } = true;

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0004EB6B File Offset: 0x0004CD6B
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0004EB72 File Offset: 0x0004CD72
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static float ZoomSensitivityModifier { get; set; } = 0.66666f;

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0004EB7A File Offset: 0x0004CD7A
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0004EB81 File Offset: 0x0004CD81
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1
		}, false)]
		public static int CrosshairType { get; set; } = 0;

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0004EB89 File Offset: 0x0004CD89
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x0004EB90 File Offset: 0x0004CD90
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableGenericAvatars { get; set; } = false;

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0004EB98 File Offset: 0x0004CD98
		// (set) Token: 0x0600149A RID: 5274 RVA: 0x0004EB9F File Offset: 0x0004CD9F
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableGenericNames { get; set; } = false;

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0004EBA7 File Offset: 0x0004CDA7
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x0004EBAE File Offset: 0x0004CDAE
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool HideFullServers { get; set; } = false;

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0004EBB6 File Offset: 0x0004CDB6
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x0004EBBD File Offset: 0x0004CDBD
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool HideEmptyServers { get; set; } = false;

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0004EBC5 File Offset: 0x0004CDC5
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x0004EBCC File Offset: 0x0004CDCC
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool HidePasswordProtectedServers { get; set; } = false;

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0004EBD4 File Offset: 0x0004CDD4
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x0004EBDB File Offset: 0x0004CDDB
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool HideUnofficialServers { get; set; } = false;

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0004EBE3 File Offset: 0x0004CDE3
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x0004EBEA File Offset: 0x0004CDEA
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool HideModuleIncompatibleServers { get; set; } = false;

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0004EBF2 File Offset: 0x0004CDF2
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x0004EBF9 File Offset: 0x0004CDF9
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1
		}, false)]
		public static int OrderType
		{
			get
			{
				return BannerlordConfig._orderType;
			}
			set
			{
				BannerlordConfig._orderType = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0004EC01 File Offset: 0x0004CE01
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x0004EC08 File Offset: 0x0004CE08
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1
		}, false)]
		public static int OrderLayoutType
		{
			get
			{
				return BannerlordConfig._orderLayoutType;
			}
			set
			{
				BannerlordConfig._orderLayoutType = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0004EC10 File Offset: 0x0004CE10
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x0004EC17 File Offset: 0x0004CE17
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableVoiceChat { get; set; } = true;

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0004EC1F File Offset: 0x0004CE1F
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x0004EC26 File Offset: 0x0004CE26
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableDeathIcon { get; set; } = true;

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0004EC2E File Offset: 0x0004CE2E
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x0004EC35 File Offset: 0x0004CE35
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableNetworkAlertIcons { get; set; } = true;

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0004EC3D File Offset: 0x0004CE3D
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x0004EC44 File Offset: 0x0004CE44
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableSingleplayerChatBox { get; set; } = true;

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0004EC4C File Offset: 0x0004CE4C
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x0004EC53 File Offset: 0x0004CE53
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool EnableMultiplayerChatBox { get; set; } = true;

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0004EC5B File Offset: 0x0004CE5B
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x0004EC62 File Offset: 0x0004CE62
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static float ChatBoxSizeX { get; set; } = 495f;

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0004EC6A File Offset: 0x0004CE6A
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x0004EC71 File Offset: 0x0004CE71
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static float ChatBoxSizeY { get; set; } = 340f;

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0004EC79 File Offset: 0x0004CE79
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x0004EC80 File Offset: 0x0004CE80
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static string LatestSaveGameName { get; set; } = string.Empty;

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0004EC88 File Offset: 0x0004CE88
		// (set) Token: 0x060014BA RID: 5306 RVA: 0x0004EC8F File Offset: 0x0004CE8F
		[BannerlordConfig.ConfigPropertyUnbounded]
		public static bool HideBattleUI { get; set; } = false;

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0004EC97 File Offset: 0x0004CE97
		// (set) Token: 0x060014BC RID: 5308 RVA: 0x0004EC9E File Offset: 0x0004CE9E
		[BannerlordConfig.ConfigPropertyInt(new int[]
		{
			0,
			1,
			2,
			3
		}, false)]
		public static int UnitSpawnPrioritization { get; set; } = 0;

		// Token: 0x04000683 RID: 1667
		private static int[] _battleSizes = new int[]
		{
			200,
			300,
			400,
			500,
			600,
			800,
			1000
		};

		// Token: 0x04000684 RID: 1668
		private static int[] _siegeBattleSizes = new int[]
		{
			150,
			230,
			320,
			425,
			540,
			625,
			1000
		};

		// Token: 0x04000685 RID: 1669
		private static int[] _sallyOutBattleSizes = new int[]
		{
			150,
			200,
			240,
			280,
			320,
			360,
			400
		};

		// Token: 0x04000686 RID: 1670
		private static int[] _reinforcementWaveCounts = new int[]
		{
			3,
			4,
			5,
			0
		};

		// Token: 0x04000687 RID: 1671
		public static double SiegeBattleSizeMultiplier = 0.8;

		// Token: 0x04000688 RID: 1672
		public const bool DefaultGyroOverrideForAttackDefend = false;

		// Token: 0x04000689 RID: 1673
		public const int DefaultAttackDirectionControl = 1;

		// Token: 0x0400068A RID: 1674
		public const int DefaultDefendDirectionControl = 0;

		// Token: 0x0400068B RID: 1675
		public const int DefaultNumberOfCorpses = 3;

		// Token: 0x0400068C RID: 1676
		public const bool DefaultShowBlood = true;

		// Token: 0x0400068D RID: 1677
		public const bool DefaultDisplayAttackDirection = true;

		// Token: 0x0400068E RID: 1678
		public const bool DefaultDisplayTargetingReticule = true;

		// Token: 0x0400068F RID: 1679
		public const bool DefaultForceVSyncInMenus = true;

		// Token: 0x04000690 RID: 1680
		public const int DefaultBattleSize = 2;

		// Token: 0x04000691 RID: 1681
		public const int DefaultReinforcementWaveCount = 3;

		// Token: 0x04000692 RID: 1682
		public const float DefaultBattleSizeMultiplier = 0.5f;

		// Token: 0x04000693 RID: 1683
		public const float DefaultFirstPersonFov = 65f;

		// Token: 0x04000694 RID: 1684
		public const float DefaultUIScale = 1f;

		// Token: 0x04000695 RID: 1685
		public const float DefaultCombatCameraDistance = 1f;

		// Token: 0x04000696 RID: 1686
		public const int DefaultCombatAI = 0;

		// Token: 0x04000697 RID: 1687
		public const int DefaultTurnCameraWithHorseInFirstPerson = 2;

		// Token: 0x04000698 RID: 1688
		public const int DefaultAutoSaveInterval = 30;

		// Token: 0x04000699 RID: 1689
		public const float DefaultFriendlyTroopsBannerOpacity = 1f;

		// Token: 0x0400069A RID: 1690
		public const bool DefaultReportDamage = true;

		// Token: 0x0400069B RID: 1691
		public const bool DefaultReportBark = true;

		// Token: 0x0400069C RID: 1692
		public const bool DefaultEnableTutorialHints = true;

		// Token: 0x0400069D RID: 1693
		public const int DefaultReportCasualtiesType = 0;

		// Token: 0x0400069E RID: 1694
		public const int DefaultAutoTrackAttackedSettlements = 0;

		// Token: 0x0400069F RID: 1695
		public const bool DefaultReportPersonalDamage = true;

		// Token: 0x040006A0 RID: 1696
		public const bool DefaultStopGameOnFocusLost = true;

		// Token: 0x040006A1 RID: 1697
		public const bool DefaultSlowDownOnOrder = true;

		// Token: 0x040006A2 RID: 1698
		public const bool DefaultReportExperience = true;

		// Token: 0x040006A3 RID: 1699
		public const bool DefaultEnableDamageTakenVisuals = true;

		// Token: 0x040006A4 RID: 1700
		public const bool DefaultEnableVoiceChat = true;

		// Token: 0x040006A5 RID: 1701
		public const bool DefaultEnableDeathIcon = true;

		// Token: 0x040006A6 RID: 1702
		public const bool DefaultEnableNetworkAlertIcons = true;

		// Token: 0x040006A7 RID: 1703
		public const bool DefaultEnableVerticalAimCorrection = true;

		// Token: 0x040006A8 RID: 1704
		public const float DefaultZoomSensitivityModifier = 0.66666f;

		// Token: 0x040006A9 RID: 1705
		public const bool DefaultSingleplayerEnableChatBox = true;

		// Token: 0x040006AA RID: 1706
		public const bool DefaultMultiplayerEnableChatBox = true;

		// Token: 0x040006AB RID: 1707
		public const float DefaultChatBoxSizeX = 495f;

		// Token: 0x040006AC RID: 1708
		public const float DefaultChatBoxSizeY = 340f;

		// Token: 0x040006AD RID: 1709
		public const int DefaultCrosshairType = 0;

		// Token: 0x040006AE RID: 1710
		public const bool DefaultEnableGenericAvatars = false;

		// Token: 0x040006AF RID: 1711
		public const bool DefaultEnableGenericNames = false;

		// Token: 0x040006B0 RID: 1712
		public const bool DefaultHideFullServers = false;

		// Token: 0x040006B1 RID: 1713
		public const bool DefaultHideEmptyServers = false;

		// Token: 0x040006B2 RID: 1714
		public const bool DefaultHidePasswordProtectedServers = false;

		// Token: 0x040006B3 RID: 1715
		public const bool DefaultHideUnofficialServers = false;

		// Token: 0x040006B4 RID: 1716
		public const bool DefaultHideModuleIncompatibleServers = false;

		// Token: 0x040006B5 RID: 1717
		public const int DefaultOrderLayoutType = 0;

		// Token: 0x040006B6 RID: 1718
		public const bool DefaultHideBattleUI = false;

		// Token: 0x040006B7 RID: 1719
		public const int DefaultUnitSpawnPrioritization = 0;

		// Token: 0x040006B8 RID: 1720
		public const int DefaultOrderType = 0;

		// Token: 0x040006B9 RID: 1721
		public const bool DefaultLockTarget = false;

		// Token: 0x040006BA RID: 1722
		private static string _language = BannerlordConfig.DefaultLanguage;

		// Token: 0x040006BB RID: 1723
		private static string _voiceLanguage = BannerlordConfig.DefaultLanguage;

		// Token: 0x040006BF RID: 1727
		private static int _numberOfCorpses = 3;

		// Token: 0x040006C4 RID: 1732
		private static int _battleSize = 2;

		// Token: 0x040006CE RID: 1742
		private static int _autoSaveInterval = 30;

		// Token: 0x040006D4 RID: 1748
		private static bool _stopGameOnFocusLost = true;

		// Token: 0x040006E1 RID: 1761
		private static int _orderType = 0;

		// Token: 0x040006E2 RID: 1762
		private static int _orderLayoutType = 0;

		// Token: 0x020004BA RID: 1210
		private interface IConfigPropertyBoundChecker<T>
		{
		}

		// Token: 0x020004BB RID: 1211
		private abstract class ConfigProperty : Attribute
		{
		}

		// Token: 0x020004BC RID: 1212
		private sealed class ConfigPropertyInt : BannerlordConfig.ConfigProperty
		{
			// Token: 0x06003712 RID: 14098 RVA: 0x000DE3F8 File Offset: 0x000DC5F8
			public ConfigPropertyInt(int[] possibleValues, bool isRange = false)
			{
				this._possibleValues = possibleValues;
				this._isRange = isRange;
				bool isRange2 = this._isRange;
			}

			// Token: 0x06003713 RID: 14099 RVA: 0x000DE418 File Offset: 0x000DC618
			public bool IsValidValue(int value)
			{
				if (this._isRange)
				{
					return value >= this._possibleValues[0] && value <= this._possibleValues[1];
				}
				int[] possibleValues = this._possibleValues;
				for (int i = 0; i < possibleValues.Length; i++)
				{
					if (possibleValues[i] == value)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04001AC4 RID: 6852
			private int[] _possibleValues;

			// Token: 0x04001AC5 RID: 6853
			private bool _isRange;
		}

		// Token: 0x020004BD RID: 1213
		private sealed class ConfigPropertyUnbounded : BannerlordConfig.ConfigProperty
		{
		}
	}
}
