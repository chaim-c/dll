﻿using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Objects.AreaMarkers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics.Towns
{
	// Token: 0x0200006E RID: 110
	public class WorkshopMissionHandler : MissionLogic
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001C88C File Offset: 0x0001AA8C
		public IEnumerable<Tuple<Workshop, GameEntity>> WorkshopSignEntities
		{
			get
			{
				return this._workshopSignEntities.AsEnumerable<Tuple<Workshop, GameEntity>>();
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001C89C File Offset: 0x0001AA9C
		public WorkshopMissionHandler(Settlement settlement)
		{
			this._settlement = settlement;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001C8F2 File Offset: 0x0001AAF2
		public override void OnBehaviorInitialize()
		{
			this._missionAgentHandler = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			this._workshopSignEntities = new List<Tuple<Workshop, GameEntity>>();
			this._listOfCurrentProps = new List<GameEntity>();
			this._propFrames = new Dictionary<int, Dictionary<string, List<MatrixFrame>>>();
			this._areaMarkers = new List<WorkshopAreaMarker>();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001C934 File Offset: 0x0001AB34
		public override void EarlyStart()
		{
			for (int i = 0; i < this._settlement.Town.Workshops.Length; i++)
			{
				if (!this._settlement.Town.Workshops[i].WorkshopType.IsHidden)
				{
					this._propFrames.Add(i, new Dictionary<string, List<MatrixFrame>>());
					foreach (string key in this._propKinds)
					{
						this._propFrames[i].Add(key, new List<MatrixFrame>());
					}
				}
			}
			List<WorkshopAreaMarker> list = base.Mission.ActiveMissionObjects.FindAllWithType<WorkshopAreaMarker>().ToList<WorkshopAreaMarker>();
			this._areaMarkers = list.FindAll((WorkshopAreaMarker x) => x.GameEntity.HasTag("workshop_area_marker"));
			foreach (WorkshopAreaMarker workshopAreaMarker in this._areaMarkers)
			{
			}
			foreach (GameEntity gameEntity in base.Mission.Scene.FindEntitiesWithTag("shop_prop").ToList<GameEntity>())
			{
				WorkshopAreaMarker workshopAreaMarker2 = this.FindWorkshop(gameEntity);
				if (workshopAreaMarker2 != null && this._propFrames.ContainsKey(workshopAreaMarker2.AreaIndex) && (this._settlement.Town.Workshops[workshopAreaMarker2.AreaIndex] == null || !this._settlement.Town.Workshops[workshopAreaMarker2.AreaIndex].WorkshopType.IsHidden))
				{
					foreach (string text in this._propKinds)
					{
						if (gameEntity.HasTag(text))
						{
							this._propFrames[workshopAreaMarker2.AreaIndex][text].Add(gameEntity.GetGlobalFrame());
							this._listOfCurrentProps.Add(gameEntity);
							break;
						}
					}
				}
			}
			this.SetBenches();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		public override void AfterStart()
		{
			this.InitShopSigns();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001CB64 File Offset: 0x0001AD64
		private WorkshopAreaMarker FindWorkshop(GameEntity prop)
		{
			foreach (WorkshopAreaMarker workshopAreaMarker in this._areaMarkers)
			{
				if (workshopAreaMarker.IsPositionInRange(prop.GlobalPosition))
				{
					return workshopAreaMarker;
				}
			}
			return null;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		private void SetBenches()
		{
			MissionAgentHandler missionAgentHandler = this._missionAgentHandler;
			if (missionAgentHandler != null)
			{
				missionAgentHandler.RemovePropReference(this._listOfCurrentProps);
			}
			foreach (GameEntity gameEntity in this._listOfCurrentProps)
			{
				gameEntity.Remove(89);
			}
			this._listOfCurrentProps.Clear();
			foreach (KeyValuePair<int, Dictionary<string, List<MatrixFrame>>> keyValuePair in this._propFrames)
			{
				int key = keyValuePair.Key;
				foreach (KeyValuePair<string, List<MatrixFrame>> keyValuePair2 in keyValuePair.Value)
				{
					List<string> prefabNames = this.GetPrefabNames(key, keyValuePair2.Key);
					if (prefabNames.Count != 0)
					{
						for (int i = 0; i < keyValuePair2.Value.Count; i++)
						{
							MatrixFrame frame = keyValuePair2.Value[i];
							this._listOfCurrentProps.Add(GameEntity.Instantiate(base.Mission.Scene, prefabNames[i % prefabNames.Count], frame));
						}
					}
				}
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001CD38 File Offset: 0x0001AF38
		private void InitShopSigns()
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign && this._settlement != null && this._settlement.IsTown)
			{
				List<GameEntity> list = base.Mission.Scene.FindEntitiesWithTag("shop_sign").ToList<GameEntity>();
				foreach (WorkshopAreaMarker workshopAreaMarker in base.Mission.ActiveMissionObjects.FindAllWithType<WorkshopAreaMarker>().ToList<WorkshopAreaMarker>())
				{
					Workshop workshop = this._settlement.Town.Workshops[workshopAreaMarker.AreaIndex];
					if (this._workshopSignEntities.All((Tuple<Workshop, GameEntity> x) => x.Item1 != workshop))
					{
						for (int i = 0; i < list.Count; i++)
						{
							GameEntity gameEntity = list[i];
							if (workshopAreaMarker.IsPositionInRange(gameEntity.GlobalPosition))
							{
								this._workshopSignEntities.Add(new Tuple<Workshop, GameEntity>(workshop, gameEntity));
								list.RemoveAt(i);
								break;
							}
						}
					}
				}
				foreach (Tuple<Workshop, GameEntity> tuple in this._workshopSignEntities)
				{
					GameEntity item = tuple.Item2;
					WorkshopType workshopType = tuple.Item1.WorkshopType;
					item.ClearComponents();
					MetaMesh copy = MetaMesh.GetCopy((workshopType != null) ? workshopType.SignMeshName : "shop_sign_merchantavailable", true, false);
					item.AddMultiMesh(copy, true);
				}
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		private List<string> GetPrefabNames(int areaIndex, string propKind)
		{
			List<string> list = new List<string>();
			Workshop workshop = this._settlement.Town.Workshops[areaIndex];
			if (workshop.WorkshopType != null)
			{
				if (propKind == this._propKinds[0])
				{
					list.Add(workshop.WorkshopType.PropMeshName1);
				}
				else if (propKind == this._propKinds[1])
				{
					list.Add(workshop.WorkshopType.PropMeshName2);
				}
				else if (propKind == this._propKinds[2])
				{
					list.AddRange(workshop.WorkshopType.PropMeshName3List);
				}
				else if (propKind == this._propKinds[3])
				{
					list.Add(workshop.WorkshopType.PropMeshName4);
				}
				else if (propKind == this._propKinds[4])
				{
					list.Add(workshop.WorkshopType.PropMeshName5);
				}
				else if (propKind == this._propKinds[5])
				{
					list.Add(workshop.WorkshopType.PropMeshName6);
				}
			}
			return list;
		}

		// Token: 0x040001E1 RID: 481
		private Settlement _settlement;

		// Token: 0x040001E2 RID: 482
		private MissionAgentHandler _missionAgentHandler;

		// Token: 0x040001E3 RID: 483
		private string[] _propKinds = new string[]
		{
			"a",
			"b",
			"c",
			"d",
			"e",
			"f"
		};

		// Token: 0x040001E4 RID: 484
		private Dictionary<int, Dictionary<string, List<MatrixFrame>>> _propFrames;

		// Token: 0x040001E5 RID: 485
		private List<GameEntity> _listOfCurrentProps;

		// Token: 0x040001E6 RID: 486
		private List<WorkshopAreaMarker> _areaMarkers;

		// Token: 0x040001E7 RID: 487
		private List<Tuple<Workshop, GameEntity>> _workshopSignEntities;
	}
}
