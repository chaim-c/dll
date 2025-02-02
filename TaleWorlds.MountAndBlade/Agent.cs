using System;
using System.Collections.Generic;
using System.Diagnostics;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020000F8 RID: 248
	public sealed class Agent : DotNetObject, IAgent, IFocusable, IUsable, IFormationUnit, ITrackableBase
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0000F374 File Offset: 0x0000D574
		public static Agent Main
		{
			get
			{
				Mission mission = Mission.Current;
				if (mission == null)
				{
					return null;
				}
				return mission.MainAgent;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000926 RID: 2342 RVA: 0x0000F388 File Offset: 0x0000D588
		// (remove) Token: 0x06000927 RID: 2343 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		public event Agent.OnAgentHealthChangedDelegate OnAgentHealthChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000928 RID: 2344 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		// (remove) Token: 0x06000929 RID: 2345 RVA: 0x0000F430 File Offset: 0x0000D630
		public event Agent.OnMountHealthChangedDelegate OnMountHealthChanged;

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0000F465 File Offset: 0x0000D665
		public bool IsPlayerControlled
		{
			get
			{
				return this.IsMine || this.MissionPeer != null;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0000F47A File Offset: 0x0000D67A
		public bool IsMine
		{
			get
			{
				return this.Controller == Agent.ControllerType.Player;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0000F485 File Offset: 0x0000D685
		public bool IsMainAgent
		{
			get
			{
				return this == Agent.Main;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0000F48F File Offset: 0x0000D68F
		public bool IsHuman
		{
			get
			{
				return (this.GetAgentFlags() & AgentFlag.IsHumanoid) > AgentFlag.None;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		public bool IsMount
		{
			get
			{
				return (this.GetAgentFlags() & AgentFlag.Mountable) > AgentFlag.None;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0000F4AD File Offset: 0x0000D6AD
		public bool IsAIControlled
		{
			get
			{
				return this.Controller == Agent.ControllerType.AI && !GameNetwork.IsClientOrReplay;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0000F4C2 File Offset: 0x0000D6C2
		public bool IsPlayerTroop
		{
			get
			{
				return !GameNetwork.IsMultiplayer && this.Origin != null && this.Origin.Troop == Game.Current.PlayerTroop;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		public bool IsUsingGameObject
		{
			get
			{
				return this.CurrentlyUsedGameObject != null;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0000F4F7 File Offset: 0x0000D6F7
		public bool CanLeadFormationsRemotely
		{
			get
			{
				return this._canLeadFormationsRemotely;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0000F4FF File Offset: 0x0000D6FF
		public bool IsDetachableFromFormation
		{
			get
			{
				return this._isDetachableFromFormation;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0000F507 File Offset: 0x0000D707
		public float AgentScale
		{
			get
			{
				return MBAPI.IMBAgent.GetAgentScale(this.GetPtr());
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0000F519 File Offset: 0x0000D719
		public bool CrouchMode
		{
			get
			{
				return MBAPI.IMBAgent.GetCrouchMode(this.GetPtr());
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0000F52B File Offset: 0x0000D72B
		public bool WalkMode
		{
			get
			{
				return MBAPI.IMBAgent.GetWalkMode(this.GetPtr());
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0000F53D File Offset: 0x0000D73D
		public Vec3 Position
		{
			get
			{
				return AgentHelper.GetAgentPosition(this.PositionPointer);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0000F54A File Offset: 0x0000D74A
		public Vec3 VisualPosition
		{
			get
			{
				return MBAPI.IMBAgent.GetVisualPosition(this.GetPtr());
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0000F55C File Offset: 0x0000D75C
		public Vec2 MovementVelocity
		{
			get
			{
				return MBAPI.IMBAgent.GetMovementVelocity(this.GetPtr());
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0000F56E File Offset: 0x0000D76E
		public Vec3 AverageVelocity
		{
			get
			{
				return MBAPI.IMBAgent.GetAverageVelocity(this.GetPtr());
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0000F580 File Offset: 0x0000D780
		public float MaximumForwardUnlimitedSpeed
		{
			get
			{
				return MBAPI.IMBAgent.GetMaximumForwardUnlimitedSpeed(this.GetPtr());
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0000F592 File Offset: 0x0000D792
		public float MovementDirectionAsAngle
		{
			get
			{
				return MBAPI.IMBAgent.GetMovementDirectionAsAngle(this.GetPtr());
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		public bool IsLookRotationInSlowMotion
		{
			get
			{
				return MBAPI.IMBAgent.IsLookRotationInSlowMotion(this.GetPtr());
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0000F5B6 File Offset: 0x0000D7B6
		public Agent.AgentPropertiesModifiers PropertyModifiers
		{
			get
			{
				return this._propertyModifiers;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0000F5BE File Offset: 0x0000D7BE
		public MBActionSet ActionSet
		{
			get
			{
				return new MBActionSet(MBAPI.IMBAgent.GetActionSetNo(this.GetPtr()));
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0000F5D5 File Offset: 0x0000D7D5
		public MBReadOnlyList<AgentComponent> Components
		{
			get
			{
				return this._components;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0000F5DD File Offset: 0x0000D7DD
		public MBReadOnlyList<Agent.Hitter> HitterList
		{
			get
			{
				return this._hitterList;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0000F5E5 File Offset: 0x0000D7E5
		public Agent.GuardMode CurrentGuardMode
		{
			get
			{
				return MBAPI.IMBAgent.GetCurrentGuardMode(this.GetPtr());
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0000F5F7 File Offset: 0x0000D7F7
		public Agent ImmediateEnemy
		{
			get
			{
				return MBAPI.IMBAgent.GetImmediateEnemy(this.GetPtr());
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0000F609 File Offset: 0x0000D809
		public bool IsDoingPassiveAttack
		{
			get
			{
				return MBAPI.IMBAgent.GetIsDoingPassiveAttack(this.GetPtr());
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0000F61B File Offset: 0x0000D81B
		public bool IsPassiveUsageConditionsAreMet
		{
			get
			{
				return MBAPI.IMBAgent.GetIsPassiveUsageConditionsAreMet(this.GetPtr());
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0000F62D File Offset: 0x0000D82D
		public float CurrentAimingError
		{
			get
			{
				return MBAPI.IMBAgent.GetCurrentAimingError(this.GetPtr());
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0000F63F File Offset: 0x0000D83F
		public float CurrentAimingTurbulance
		{
			get
			{
				return MBAPI.IMBAgent.GetCurrentAimingTurbulance(this.GetPtr());
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0000F651 File Offset: 0x0000D851
		public Agent.UsageDirection AttackDirection
		{
			get
			{
				return MBAPI.IMBAgent.GetAttackDirectionUsage(this.GetPtr());
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0000F663 File Offset: 0x0000D863
		public float WalkingSpeedLimitOfMountable
		{
			get
			{
				return MBAPI.IMBAgent.GetWalkSpeedLimitOfMountable(this.GetPtr());
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0000F675 File Offset: 0x0000D875
		public Agent RiderAgent
		{
			get
			{
				return this.GetRiderAgentAux();
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0000F67D File Offset: 0x0000D87D
		public bool HasMount
		{
			get
			{
				return this.MountAgent != null;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0000F688 File Offset: 0x0000D888
		public bool CanLogCombatFor
		{
			get
			{
				return (this.RiderAgent != null && !this.RiderAgent.IsAIControlled) || (!this.IsMount && !this.IsAIControlled);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
		public float MissileRangeAdjusted
		{
			get
			{
				return this.GetMissileRangeWithHeightDifference();
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		public float MaximumMissileRange
		{
			get
			{
				return this.GetMissileRange();
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		FocusableObjectType IFocusable.FocusableObjectType
		{
			get
			{
				if (!this.IsMount)
				{
					return FocusableObjectType.Agent;
				}
				return FocusableObjectType.Mount;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0000F6D1 File Offset: 0x0000D8D1
		public string Name
		{
			get
			{
				if (this.MissionPeer == null)
				{
					return this._name.ToString();
				}
				return this.MissionPeer.Name;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0000F6F2 File Offset: 0x0000D8F2
		public AgentMovementLockedState MovementLockedState
		{
			get
			{
				return this.GetMovementLockedState();
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0000F6FA File Offset: 0x0000D8FA
		public Monster Monster { get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0000F702 File Offset: 0x0000D902
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0000F70A File Offset: 0x0000D90A
		public bool IsRunningAway { get; private set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0000F713 File Offset: 0x0000D913
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0000F71B File Offset: 0x0000D91B
		public BodyProperties BodyPropertiesValue { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0000F724 File Offset: 0x0000D924
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0000F72C File Offset: 0x0000D92C
		public CommonAIComponent CommonAIComponent { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0000F735 File Offset: 0x0000D935
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0000F73D File Offset: 0x0000D93D
		public HumanAIComponent HumanAIComponent { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0000F746 File Offset: 0x0000D946
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0000F74E File Offset: 0x0000D94E
		public int BodyPropertiesSeed { get; internal set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0000F757 File Offset: 0x0000D957
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0000F75F File Offset: 0x0000D95F
		public float LastRangedHitTime { get; private set; } = float.MinValue;

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0000F768 File Offset: 0x0000D968
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0000F770 File Offset: 0x0000D970
		public float LastMeleeHitTime { get; private set; } = float.MinValue;

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0000F779 File Offset: 0x0000D979
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0000F781 File Offset: 0x0000D981
		public float LastRangedAttackTime { get; private set; } = float.MinValue;

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0000F78A File Offset: 0x0000D98A
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x0000F792 File Offset: 0x0000D992
		public float LastMeleeAttackTime { get; private set; } = float.MinValue;

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0000F79B File Offset: 0x0000D99B
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x0000F7A3 File Offset: 0x0000D9A3
		public bool IsFemale { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public ItemObject Banner
		{
			get
			{
				MissionEquipment equipment = this.Equipment;
				if (equipment == null)
				{
					return null;
				}
				return equipment.GetBanner();
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0000F7BF File Offset: 0x0000D9BF
		public ItemObject FormationBanner
		{
			get
			{
				return this._formationBanner;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public MissionWeapon WieldedWeapon
		{
			get
			{
				EquipmentIndex wieldedItemIndex = this.GetWieldedItemIndex(Agent.HandIndex.MainHand);
				if (wieldedItemIndex < EquipmentIndex.WeaponItemBeginSlot)
				{
					return MissionWeapon.Invalid;
				}
				return this.Equipment[wieldedItemIndex];
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0000F7F3 File Offset: 0x0000D9F3
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0000F7FB File Offset: 0x0000D9FB
		public bool IsItemUseDisabled { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0000F804 File Offset: 0x0000DA04
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0000F80C File Offset: 0x0000DA0C
		public bool SyncHealthToAllClients { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0000F815 File Offset: 0x0000DA15
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0000F81D File Offset: 0x0000DA1D
		public UsableMissionObject CurrentlyUsedGameObject { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0000F826 File Offset: 0x0000DA26
		public bool CombatActionsEnabled
		{
			get
			{
				return this.CurrentlyUsedGameObject == null || !this.CurrentlyUsedGameObject.DisableCombatActionsOnUse;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0000F840 File Offset: 0x0000DA40
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0000F848 File Offset: 0x0000DA48
		public Mission Mission { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0000F851 File Offset: 0x0000DA51
		public bool IsHero
		{
			get
			{
				return this.Character != null && this.Character.IsHero;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0000F868 File Offset: 0x0000DA68
		public int Index { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0000F870 File Offset: 0x0000DA70
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0000F878 File Offset: 0x0000DA78
		public MissionEquipment Equipment { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0000F881 File Offset: 0x0000DA81
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0000F889 File Offset: 0x0000DA89
		public TextObject AgentRole { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0000F892 File Offset: 0x0000DA92
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0000F89A File Offset: 0x0000DA9A
		public bool HasBeenBuilt { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0000F8A3 File Offset: 0x0000DAA3
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0000F8AB File Offset: 0x0000DAAB
		public Agent.MortalityState CurrentMortalityState { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0000F8BC File Offset: 0x0000DABC
		public Equipment SpawnEquipment { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0000F8C5 File Offset: 0x0000DAC5
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0000F8CD File Offset: 0x0000DACD
		public FormationPositionPreference FormationPositionPreference { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0000F8D6 File Offset: 0x0000DAD6
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0000F8DE File Offset: 0x0000DADE
		public bool RandomizeColors { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0000F8E7 File Offset: 0x0000DAE7
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x0000F8EF File Offset: 0x0000DAEF
		public float CharacterPowerCached { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x0000F900 File Offset: 0x0000DB00
		public float WalkSpeedCached { get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0000F909 File Offset: 0x0000DB09
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0000F911 File Offset: 0x0000DB11
		public float RunSpeedCached { get; private set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0000F91A File Offset: 0x0000DB1A
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0000F922 File Offset: 0x0000DB22
		public IAgentOriginBase Origin { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0000F92B File Offset: 0x0000DB2B
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0000F933 File Offset: 0x0000DB33
		public Team Team { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0000F93C File Offset: 0x0000DB3C
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0000F944 File Offset: 0x0000DB44
		public int KillCount { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0000F94D File Offset: 0x0000DB4D
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x0000F955 File Offset: 0x0000DB55
		public AgentDrivenProperties AgentDrivenProperties { get; private set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0000F95E File Offset: 0x0000DB5E
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x0000F966 File Offset: 0x0000DB66
		public float BaseHealthLimit { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0000F96F File Offset: 0x0000DB6F
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x0000F977 File Offset: 0x0000DB77
		public string HorseCreationKey { get; private set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0000F980 File Offset: 0x0000DB80
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0000F988 File Offset: 0x0000DB88
		public float HealthLimit { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0000F991 File Offset: 0x0000DB91
		public bool IsRangedCached
		{
			get
			{
				return this.Equipment.ContainsNonConsumableRangedWeaponWithAmmo();
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0000F99E File Offset: 0x0000DB9E
		public bool HasMeleeWeaponCached
		{
			get
			{
				return this.Equipment.ContainsMeleeWeapon();
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0000F9AB File Offset: 0x0000DBAB
		public bool HasShieldCached
		{
			get
			{
				return this.Equipment.ContainsShield();
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		public bool HasSpearCached
		{
			get
			{
				return this.Equipment.ContainsSpear();
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0000F9C5 File Offset: 0x0000DBC5
		public bool HasThrownCached
		{
			get
			{
				return this.Equipment.ContainsThrownWeapon();
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0000F9D2 File Offset: 0x0000DBD2
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x0000F9E4 File Offset: 0x0000DBE4
		public Agent.AIStateFlag AIStateFlags
		{
			get
			{
				return MBAPI.IMBAgent.GetAIStateFlags(this.GetPtr());
			}
			set
			{
				MBAPI.IMBAgent.SetAIStateFlags(this.GetPtr(), value);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public MatrixFrame Frame
		{
			get
			{
				MatrixFrame result = default(MatrixFrame);
				MBAPI.IMBAgent.GetRotationFrame(this.GetPtr(), ref result);
				return result;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0000FA20 File Offset: 0x0000DC20
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0000FA32 File Offset: 0x0000DC32
		public Agent.MovementControlFlag MovementFlags
		{
			get
			{
				return (Agent.MovementControlFlag)MBAPI.IMBAgent.GetMovementFlags(this.GetPtr());
			}
			set
			{
				MBAPI.IMBAgent.SetMovementFlags(this.GetPtr(), value);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0000FA45 File Offset: 0x0000DC45
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0000FA57 File Offset: 0x0000DC57
		public Vec2 MovementInputVector
		{
			get
			{
				return MBAPI.IMBAgent.GetMovementInputVector(this.GetPtr());
			}
			set
			{
				MBAPI.IMBAgent.SetMovementInputVector(this.GetPtr(), value);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		public CapsuleData CollisionCapsule
		{
			get
			{
				CapsuleData result = default(CapsuleData);
				MBAPI.IMBAgent.GetCollisionCapsule(this.GetPtr(), ref result);
				return result;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0000FA94 File Offset: 0x0000DC94
		public Vec3 CollisionCapsuleCenter
		{
			get
			{
				CapsuleData collisionCapsule = this.CollisionCapsule;
				return (collisionCapsule.GetBoxMax() + collisionCapsule.GetBoxMin()) * 0.5f;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public MBAgentVisuals AgentVisuals
		{
			get
			{
				MBAgentVisuals agentVisuals;
				if (!this._visualsWeakRef.TryGetTarget(out agentVisuals))
				{
					agentVisuals = MBAPI.IMBAgent.GetAgentVisuals(this.GetPtr());
					this._visualsWeakRef.SetTarget(agentVisuals);
				}
				return agentVisuals;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0000FB02 File Offset: 0x0000DD02
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0000FB14 File Offset: 0x0000DD14
		public bool HeadCameraMode
		{
			get
			{
				return MBAPI.IMBAgent.GetHeadCameraMode(this.GetPtr());
			}
			set
			{
				MBAPI.IMBAgent.SetHeadCameraMode(this.GetPtr(), value);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0000FB27 File Offset: 0x0000DD27
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0000FB2F File Offset: 0x0000DD2F
		public Agent MountAgent
		{
			get
			{
				return this.GetMountAgentAux();
			}
			private set
			{
				this.SetMountAgent(value);
				this.UpdateAgentStats();
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0000FB3E File Offset: 0x0000DD3E
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x0000FB46 File Offset: 0x0000DD46
		public IDetachment Detachment
		{
			get
			{
				return this._detachment;
			}
			set
			{
				this._detachment = value;
				if (this._detachment != null)
				{
					Formation formation = this.Formation;
					if (formation == null)
					{
						return;
					}
					formation.Team.DetachmentManager.RemoveScoresOfAgentFromDetachments(this);
				}
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0000FB72 File Offset: 0x0000DD72
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x0000FB80 File Offset: 0x0000DD80
		public bool IsPaused
		{
			get
			{
				return this.AIStateFlags.HasAnyFlag(Agent.AIStateFlag.Paused);
			}
			set
			{
				if (value)
				{
					this.AIStateFlags |= Agent.AIStateFlag.Paused;
					return;
				}
				this.AIStateFlags &= ~Agent.AIStateFlag.Paused;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0000FBA3 File Offset: 0x0000DDA3
		public bool IsDetachedFromFormation
		{
			get
			{
				return this._detachment != null;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		public Agent.WatchState CurrentWatchState
		{
			get
			{
				Agent.AIStateFlag aistateFlags = this.AIStateFlags;
				if ((aistateFlags & Agent.AIStateFlag.Alarmed) == Agent.AIStateFlag.Alarmed)
				{
					return Agent.WatchState.Alarmed;
				}
				if ((aistateFlags & Agent.AIStateFlag.Cautious) == Agent.AIStateFlag.Cautious)
				{
					return Agent.WatchState.Cautious;
				}
				return Agent.WatchState.Patrolling;
			}
			private set
			{
				Agent.AIStateFlag aistateFlag = this.AIStateFlags;
				switch (value)
				{
				case Agent.WatchState.Patrolling:
					aistateFlag &= ~(Agent.AIStateFlag.Cautious | Agent.AIStateFlag.Alarmed);
					break;
				case Agent.WatchState.Cautious:
					aistateFlag |= Agent.AIStateFlag.Cautious;
					aistateFlag &= ~Agent.AIStateFlag.Alarmed;
					break;
				case Agent.WatchState.Alarmed:
					aistateFlag |= Agent.AIStateFlag.Alarmed;
					aistateFlag &= ~Agent.AIStateFlag.Cautious;
					break;
				default:
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Agent.cs", "CurrentWatchState", 900);
					break;
				}
				this.AIStateFlags = aistateFlag;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0000FC3D File Offset: 0x0000DE3D
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0000FC45 File Offset: 0x0000DE45
		public float Defensiveness
		{
			get
			{
				return this._defensiveness;
			}
			set
			{
				if (MathF.Abs(value - this._defensiveness) > 0.0001f)
				{
					this._defensiveness = value;
					this.UpdateAgentProperties();
				}
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0000FC68 File Offset: 0x0000DE68
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0000FC70 File Offset: 0x0000DE70
		public Formation Formation
		{
			get
			{
				return this._formation;
			}
			set
			{
				if (this._formation != value)
				{
					if (GameNetwork.IsServer && this.HasBeenBuilt && this.Mission.GetMissionBehavior<MissionNetworkComponent>() != null)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new AgentSetFormation(this.Index, (value != null) ? value.Index : -1));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
					this.SetNativeFormationNo((value != null) ? value.Index : -1);
					IDetachment detachment = null;
					float detachmentWeight = 0f;
					if (this._formation != null)
					{
						if (this.IsDetachedFromFormation)
						{
							detachment = this.Detachment;
							detachmentWeight = this.DetachmentWeight;
						}
						this._formation.RemoveUnit(this);
						foreach (IDetachment detachment2 in this._formation.Detachments)
						{
							if (!detachment2.IsUsedByFormation(value))
							{
								this.Team.DetachmentManager.RemoveScoresOfAgentFromDetachment(this, detachment2);
							}
						}
					}
					this._formation = value;
					if (this._formation != null)
					{
						if (!this._formation.HasBeenPositioned)
						{
							this._formation.SetPositioning(new WorldPosition?(this.GetWorldPosition()), new Vec2?(this.LookDirection.AsVec2), null);
						}
						this._formation.AddUnit(this);
						if (detachment != null && this._formation.Detachments.IndexOf(detachment) >= 0 && detachment.IsStandingPointAvailableForAgent(this))
						{
							detachment.AddAgent(this, -1);
							this._formation.DetachUnit(this, detachment.IsLoose);
							this.Detachment = detachment;
							this.DetachmentWeight = detachmentWeight;
						}
					}
					this.UpdateCachedAndFormationValues(this._formation != null && this._formation.PostponeCostlyOperations, false);
				}
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0000FE34 File Offset: 0x0000E034
		IFormationUnit IFormationUnit.FollowedUnit
		{
			get
			{
				if (!this.IsActive())
				{
					return null;
				}
				if (this.IsAIControlled)
				{
					return this.GetFollowedUnit();
				}
				return null;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0000FE50 File Offset: 0x0000E050
		public bool IsShieldUsageEncouraged
		{
			get
			{
				return this.Formation.FiringOrder.OrderEnum == FiringOrder.RangedWeaponUsageOrderEnum.HoldYourFire || !this.Equipment.HasAnyWeaponWithFlags(WeaponFlags.RangedWeapon | WeaponFlags.NotUsableWithOneHand);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0000FE78 File Offset: 0x0000E078
		public bool IsPlayerUnit
		{
			get
			{
				return this.IsPlayerControlled || this.IsPlayerTroop;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0000FE8A File Offset: 0x0000E08A
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0000FE94 File Offset: 0x0000E094
		public Agent.ControllerType Controller
		{
			get
			{
				return this.GetController();
			}
			set
			{
				Agent.ControllerType controller = this.Controller;
				if (value != controller)
				{
					this.SetController(value);
					bool flag = value == Agent.ControllerType.Player;
					if (flag)
					{
						this.Mission.MainAgent = this;
						this.SetAgentFlags(this.GetAgentFlags() | AgentFlag.CanRide);
					}
					if (this.Formation != null)
					{
						this.Formation.OnAgentControllerChanged(this, controller);
					}
					if (value != Agent.ControllerType.AI && this.GetAgentFlags().HasAnyFlag(AgentFlag.IsHumanoid))
					{
						this.SetMaximumSpeedLimit(-1f, false);
						if (this.WalkMode)
						{
							this.EventControlFlags |= Agent.EventControlFlag.Run;
						}
					}
					foreach (MissionBehavior missionBehavior in this.Mission.MissionBehaviors)
					{
						missionBehavior.OnAgentControllerChanged(this, controller);
					}
					if (flag)
					{
						foreach (MissionBehavior missionBehavior2 in this.Mission.MissionBehaviors)
						{
							missionBehavior2.OnAgentControllerSetToPlayer(this.Mission.MainAgent);
						}
					}
					if (GameNetwork.IsServer)
					{
						MissionPeer missionPeer = this.MissionPeer;
						NetworkCommunicator networkCommunicator = (missionPeer != null) ? missionPeer.GetNetworkPeer() : null;
						if (networkCommunicator != null && !networkCommunicator.IsServerPeer)
						{
							GameNetwork.BeginModuleEventAsServer(networkCommunicator);
							GameNetwork.WriteMessage(new SetAgentIsPlayer(this.Index, this.Controller != Agent.ControllerType.AI));
							GameNetwork.EndModuleEventAsServer();
						}
					}
				}
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00010018 File Offset: 0x0000E218
		public uint ClothingColor1
		{
			get
			{
				if (this._clothingColor1 != null)
				{
					return this._clothingColor1.Value;
				}
				if (this.Team != null)
				{
					return this.Team.Color;
				}
				Debug.FailedAssert("Clothing color is not set.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Agent.cs", "ClothingColor1", 1115);
				return uint.MaxValue;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0001006C File Offset: 0x0000E26C
		public uint ClothingColor2
		{
			get
			{
				if (this._clothingColor2 != null)
				{
					return this._clothingColor2.Value;
				}
				return this.ClothingColor1;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00010090 File Offset: 0x0000E290
		public MatrixFrame LookFrame
		{
			get
			{
				return new MatrixFrame
				{
					origin = this.Position,
					rotation = this.LookRotation
				};
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x000100C0 File Offset: 0x0000E2C0
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x000100D2 File Offset: 0x0000E2D2
		public float LookDirectionAsAngle
		{
			get
			{
				return MBAPI.IMBAgent.GetLookDirectionAsAngle(this.GetPtr());
			}
			set
			{
				MBAPI.IMBAgent.SetLookDirectionAsAngle(this.GetPtr(), value);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public Mat3 LookRotation
		{
			get
			{
				Mat3 mat;
				mat.f = this.LookDirection;
				mat.u = Vec3.Up;
				mat.s = Vec3.CrossProduct(mat.f, mat.u);
				mat.s.Normalize();
				mat.u = Vec3.CrossProduct(mat.s, mat.f);
				return mat;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001014C File Offset: 0x0000E34C
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0001015E File Offset: 0x0000E35E
		public bool IsLookDirectionLocked
		{
			get
			{
				return MBAPI.IMBAgent.GetIsLookDirectionLocked(this.GetPtr());
			}
			set
			{
				MBAPI.IMBAgent.SetIsLookDirectionLocked(this.GetPtr(), value);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00010174 File Offset: 0x0000E374
		public bool IsCheering
		{
			get
			{
				ActionIndexValueCache currentActionValue = this.GetCurrentActionValue(1);
				for (int i = 0; i < Agent.DefaultTauntActions.Length; i++)
				{
					if (Agent.DefaultTauntActions[i] != null && Agent.DefaultTauntActions[i] == currentActionValue)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000101BC File Offset: 0x0000E3BC
		public bool IsInBeingStruckAction
		{
			get
			{
				return MBMath.IsBetween((int)this.GetCurrentActionType(1), 47, 51) || MBMath.IsBetween((int)this.GetCurrentActionType(0), 47, 51);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000101E2 File Offset: 0x0000E3E2
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x000101EC File Offset: 0x0000E3EC
		public MissionPeer MissionPeer
		{
			get
			{
				return this._missionPeer;
			}
			set
			{
				if (this._missionPeer != value)
				{
					MissionPeer missionPeer = this._missionPeer;
					this._missionPeer = value;
					if (missionPeer != null && missionPeer.ControlledAgent == this)
					{
						missionPeer.ControlledAgent = null;
					}
					if (this._missionPeer != null && this._missionPeer.ControlledAgent != this)
					{
						this._missionPeer.ControlledAgent = this;
						if (GameNetwork.IsServerOrRecorder)
						{
							this.SyncHealthToClients();
							Agent.OnAgentHealthChangedDelegate onAgentHealthChanged = this.OnAgentHealthChanged;
							if (onAgentHealthChanged != null)
							{
								onAgentHealthChanged(this, this.Health, this.Health);
							}
						}
					}
					if (value != null)
					{
						this.Controller = (value.IsMine ? Agent.ControllerType.Player : Agent.ControllerType.None);
					}
					if (GameNetwork.IsServer && this.IsHuman && !this._isDeleted)
					{
						NetworkCommunicator networkCommunicator = (value != null) ? value.GetNetworkPeer() : null;
						this.SetNetworkPeer(networkCommunicator);
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new SetAgentPeer(this.Index, networkCommunicator));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
				}
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x000102D0 File Offset: 0x0000E4D0
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public BasicCharacterObject Character
		{
			get
			{
				return this._character;
			}
			set
			{
				this._character = value;
				if (value != null)
				{
					this.Health = (float)this._character.HitPoints;
					this.BaseHealthLimit = (float)this._character.MaxHitPoints();
					this.HealthLimit = this.BaseHealthLimit;
					this.CharacterPowerCached = value.GetPower();
					this._name = value.Name;
					this.IsFemale = value.IsFemale;
				}
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00010343 File Offset: 0x0000E543
		IMissionTeam IAgent.Team
		{
			get
			{
				return this.Team;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0001034B File Offset: 0x0000E54B
		IFormationArrangement IFormationUnit.Formation
		{
			get
			{
				return this._formation.Arrangement;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00010358 File Offset: 0x0000E558
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x00010360 File Offset: 0x0000E560
		int IFormationUnit.FormationFileIndex { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00010369 File Offset: 0x0000E569
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00010371 File Offset: 0x0000E571
		int IFormationUnit.FormationRankIndex { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001037A File Offset: 0x0000E57A
		private UIntPtr Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00010382 File Offset: 0x0000E582
		private UIntPtr FlagsPointer
		{
			get
			{
				return this._flagsPointer;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0001038A File Offset: 0x0000E58A
		private UIntPtr PositionPointer
		{
			get
			{
				return this._positionPointer;
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00010394 File Offset: 0x0000E594
		internal Agent(Mission mission, Mission.AgentCreationResult creationResult, Agent.CreationType creationType, Monster monster)
		{
			this.AgentRole = TextObject.Empty;
			this.Mission = mission;
			this.Index = creationResult.Index;
			this._pointer = creationResult.AgentPtr;
			this._positionPointer = creationResult.PositionPtr;
			this._flagsPointer = creationResult.FlagsPtr;
			this._indexPointer = creationResult.IndexPtr;
			this._statePointer = creationResult.StatePtr;
			this._lastHitInfo = default(Agent.AgentLastHitInfo);
			this._lastHitInfo.Initialize();
			MBAPI.IMBAgent.SetMonoObject(this.GetPtr(), this);
			this.Monster = monster;
			this.KillCount = 0;
			this.HasBeenBuilt = false;
			this._creationType = creationType;
			this._agentControllers = new List<AgentController>();
			this._components = new MBList<AgentComponent>();
			this._hitterList = new MBList<Agent.Hitter>();
			((IFormationUnit)this).FormationFileIndex = -1;
			((IFormationUnit)this).FormationRankIndex = -1;
			this._synchedBodyComponents = null;
			this._cachedAndFormationValuesUpdateTimer = new Timer(this.Mission.CurrentTime, 0.45f + MBRandom.RandomFloat * 0.1f, true);
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000104F1 File Offset: 0x0000E6F1
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00010503 File Offset: 0x0000E703
		public Vec3 LookDirection
		{
			get
			{
				return MBAPI.IMBAgent.GetLookDirection(this.GetPtr());
			}
			set
			{
				MBAPI.IMBAgent.SetLookDirection(this.GetPtr(), value);
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00010516 File Offset: 0x0000E716
		bool IAgent.IsEnemyOf(IAgent agent)
		{
			return this.IsEnemyOf((Agent)agent);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00010524 File Offset: 0x0000E724
		bool IAgent.IsFriendOf(IAgent agent)
		{
			return this.IsFriendOf((Agent)agent);
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00010532 File Offset: 0x0000E732
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0001053C File Offset: 0x0000E73C
		public float Health
		{
			get
			{
				return this._health;
			}
			set
			{
				float num = (float)(value.ApproximatelyEqualsTo(0f, 1E-05f) ? 0 : MathF.Ceiling(value));
				if (!this._health.ApproximatelyEqualsTo(num, 1E-05f))
				{
					float health = this._health;
					this._health = num;
					if (GameNetwork.IsServerOrRecorder)
					{
						this.SyncHealthToClients();
					}
					Agent.OnAgentHealthChangedDelegate onAgentHealthChanged = this.OnAgentHealthChanged;
					if (onAgentHealthChanged != null)
					{
						onAgentHealthChanged(this, health, this._health);
					}
					if (this.RiderAgent != null)
					{
						Agent.OnMountHealthChangedDelegate onMountHealthChanged = this.RiderAgent.OnMountHealthChanged;
						if (onMountHealthChanged == null)
						{
							return;
						}
						onMountHealthChanged(this.RiderAgent, this, health, this._health);
					}
				}
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000105D8 File Offset: 0x0000E7D8
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x000105F4 File Offset: 0x0000E7F4
		public float Age
		{
			get
			{
				return this.BodyPropertiesValue.Age;
			}
			set
			{
				this.BodyPropertiesValue = new BodyProperties(new DynamicBodyProperties(value, this.BodyPropertiesValue.Weight, this.BodyPropertiesValue.Build), this.BodyPropertiesValue.StaticProperties);
				BodyProperties bodyPropertiesValue = this.BodyPropertiesValue;
				this.BodyPropertiesValue = bodyPropertiesValue;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001064A File Offset: 0x0000E84A
		Vec3 ITrackableBase.GetPosition()
		{
			return this.Position;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00010654 File Offset: 0x0000E854
		public Vec3 Velocity
		{
			get
			{
				Vec2 movementVelocity = MBAPI.IMBAgent.GetMovementVelocity(this.GetPtr());
				Vec3 v = new Vec3(movementVelocity, 0f, -1f);
				return this.Frame.rotation.TransformToParent(v);
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00010698 File Offset: 0x0000E898
		TextObject ITrackableBase.GetName()
		{
			if (this.Character != null)
			{
				return new TextObject(this.Character.Name.ToString(), null);
			}
			return TextObject.Empty;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000106BE File Offset: 0x0000E8BE
		[MBCallback]
		internal void SetAgentAIPerformingRetreatBehavior(bool isAgentAIPerformingRetreatBehavior)
		{
			if (!GameNetwork.IsClientOrReplay && this.Mission != null)
			{
				this.IsRunningAway = isAgentAIPerformingRetreatBehavior;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x000106D6 File Offset: 0x0000E8D6
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x000106E8 File Offset: 0x0000E8E8
		public Agent.EventControlFlag EventControlFlags
		{
			get
			{
				return (Agent.EventControlFlag)MBAPI.IMBAgent.GetEventControlFlags(this.GetPtr());
			}
			set
			{
				MBAPI.IMBAgent.SetEventControlFlags(this.GetPtr(), value);
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000106FB File Offset: 0x0000E8FB
		[MBCallback]
		public float GetMissileRangeWithHeightDifferenceAux(float targetZ)
		{
			return MBAPI.IMBAgent.GetMissileRangeWithHeightDifference(this.GetPtr(), targetZ);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001070E File Offset: 0x0000E90E
		[MBCallback]
		internal int GetFormationUnitSpacing()
		{
			return this.Formation.UnitSpacing;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001071B File Offset: 0x0000E91B
		[MBCallback]
		public string GetSoundAndCollisionInfoClassName()
		{
			return this.Monster.SoundAndCollisionInfoClassName;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00010728 File Offset: 0x0000E928
		[MBCallback]
		internal bool IsInSameFormationWith(Agent otherAgent)
		{
			Formation formation = otherAgent.Formation;
			return this.Formation != null && formation != null && this.Formation == formation;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00010752 File Offset: 0x0000E952
		[MBCallback]
		internal void OnWeaponSwitchingToAlternativeStart(EquipmentIndex slotIndex, int usageIndex)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new StartSwitchingWeaponUsageIndex(this.Index, slotIndex, usageIndex, Agent.MovementFlagToDirection(this.MovementFlags)));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00010785 File Offset: 0x0000E985
		[MBCallback]
		internal void OnWeaponReloadPhaseChange(EquipmentIndex slotIndex, short reloadPhase)
		{
			this.Equipment.SetReloadPhaseOfSlot(slotIndex, reloadPhase);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetWeaponReloadPhase(this.Index, slotIndex, reloadPhase));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000107BC File Offset: 0x0000E9BC
		[MBCallback]
		internal void OnWeaponAmmoReload(EquipmentIndex slotIndex, EquipmentIndex ammoSlotIndex, short totalAmmo)
		{
			if (this.Equipment[slotIndex].CurrentUsageItem.IsRangedWeapon)
			{
				this.Equipment.SetReloadedAmmoOfSlot(slotIndex, ammoSlotIndex, totalAmmo);
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetWeaponAmmoData(this.Index, slotIndex, ammoSlotIndex, totalAmmo));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
			this.UpdateAgentProperties();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00010820 File Offset: 0x0000EA20
		[MBCallback]
		internal void OnWeaponAmmoConsume(EquipmentIndex slotIndex, short totalAmmo)
		{
			if (this.Equipment[slotIndex].CurrentUsageItem.IsRangedWeapon)
			{
				this.Equipment.SetConsumedAmmoOfSlot(slotIndex, totalAmmo);
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetWeaponAmmoData(this.Index, slotIndex, EquipmentIndex.None, totalAmmo));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
			this.UpdateAgentProperties();
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00010882 File Offset: 0x0000EA82
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0001088F File Offset: 0x0000EA8F
		public AgentState State
		{
			get
			{
				return AgentHelper.GetAgentState(this._statePointer);
			}
			set
			{
				if (this.State != value)
				{
					MBAPI.IMBAgent.SetStateFlags(this.GetPtr(), value);
				}
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000108AC File Offset: 0x0000EAAC
		[MBCallback]
		internal void OnShieldDamaged(EquipmentIndex slotIndex, int inflictedDamage)
		{
			int num = MathF.Max(0, (int)this.Equipment[slotIndex].HitPoints - inflictedDamage);
			this.ChangeWeaponHitPoints(slotIndex, (short)num);
			if (num == 0)
			{
				this.RemoveEquippedWeapon(slotIndex);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x000108EC File Offset: 0x0000EAEC
		public MissionWeapon WieldedOffhandWeapon
		{
			get
			{
				EquipmentIndex wieldedItemIndex = this.GetWieldedItemIndex(Agent.HandIndex.OffHand);
				if (wieldedItemIndex < EquipmentIndex.WeaponItemBeginSlot)
				{
					return MissionWeapon.Invalid;
				}
				return this.Equipment[wieldedItemIndex];
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00010918 File Offset: 0x0000EB18
		[MBCallback]
		internal void OnWeaponAmmoRemoved(EquipmentIndex slotIndex)
		{
			if (!this.Equipment[slotIndex].AmmoWeapon.IsEmpty)
			{
				this.Equipment.SetConsumedAmmoOfSlot(slotIndex, 0);
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00010950 File Offset: 0x0000EB50
		[MBCallback]
		internal void OnMount(Agent mount)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				if (mount.IsAIControlled && mount.IsRetreating(false))
				{
					mount.StopRetreatingMoraleComponent();
				}
				this.CheckToDropFlaggedItem();
			}
			if (this.HasBeenBuilt)
			{
				foreach (AgentComponent agentComponent in this._components)
				{
					agentComponent.OnMount(mount);
				}
				this.Mission.OnAgentMount(this);
			}
			this.UpdateAgentStats();
			Action onAgentMountedStateChanged = this.OnAgentMountedStateChanged;
			if (onAgentMountedStateChanged != null)
			{
				onAgentMountedStateChanged();
			}
			if (GameNetwork.IsServerOrRecorder)
			{
				mount.SyncHealthToClients();
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00010A00 File Offset: 0x0000EC00
		[MBCallback]
		internal void OnDismount(Agent mount)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				Formation formation = this.Formation;
				if (formation != null)
				{
					formation.OnAgentLostMount(this);
				}
				this.CheckToDropFlaggedItem();
			}
			foreach (AgentComponent agentComponent in this._components)
			{
				agentComponent.OnDismount(mount);
			}
			this.Mission.OnAgentDismount(this);
			if (this.IsActive())
			{
				this.UpdateAgentStats();
				Action onAgentMountedStateChanged = this.OnAgentMountedStateChanged;
				if (onAgentMountedStateChanged == null)
				{
					return;
				}
				onAgentMountedStateChanged();
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00010A9C File Offset: 0x0000EC9C
		[MBCallback]
		internal void OnAgentAlarmedStateChanged(Agent.AIStateFlag flag)
		{
			foreach (MissionBehavior missionBehavior in Mission.Current.MissionBehaviors)
			{
				missionBehavior.OnAgentAlarmedStateChanged(this, flag);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00010AF4 File Offset: 0x0000ECF4
		[MBCallback]
		internal void OnRetreating()
		{
			if (!GameNetwork.IsClientOrReplay && this.Mission != null && !this.Mission.MissionEnded)
			{
				if (this.IsUsingGameObject)
				{
					this.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				foreach (AgentComponent agentComponent in this._components)
				{
					agentComponent.OnRetreating();
				}
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00010B70 File Offset: 0x0000ED70
		[MBCallback]
		internal void UpdateMountAgentCache(Agent newMountAgent)
		{
			this._cachedMountAgent = newMountAgent;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00010B79 File Offset: 0x0000ED79
		[MBCallback]
		internal void UpdateRiderAgentCache(Agent newRiderAgent)
		{
			this._cachedRiderAgent = newRiderAgent;
			if (newRiderAgent == null)
			{
				Mission.Current.AddMountWithoutRider(this);
				return;
			}
			Mission.Current.RemoveMountWithoutRider(this);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00010B9C File Offset: 0x0000ED9C
		[MBCallback]
		public void UpdateAgentStats()
		{
			if (this.IsActive())
			{
				this.UpdateAgentProperties();
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00010BAC File Offset: 0x0000EDAC
		[MBCallback]
		public float GetWeaponInaccuracy(EquipmentIndex weaponSlotIndex, int weaponUsageIndex)
		{
			WeaponComponentData weaponComponentDataForUsage = this.Equipment[weaponSlotIndex].GetWeaponComponentDataForUsage(weaponUsageIndex);
			int effectiveSkill = MissionGameModels.Current.AgentStatCalculateModel.GetEffectiveSkill(this, weaponComponentDataForUsage.RelevantSkill);
			return MissionGameModels.Current.AgentStatCalculateModel.GetWeaponInaccuracy(this, weaponComponentDataForUsage, effectiveSkill);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00010BF8 File Offset: 0x0000EDF8
		[MBCallback]
		public float DebugGetHealth()
		{
			return this.Health;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00010C00 File Offset: 0x0000EE00
		public void SetTargetPosition(Vec2 value)
		{
			MBAPI.IMBAgent.SetTargetPosition(this.GetPtr(), ref value);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00010C14 File Offset: 0x0000EE14
		public void SetGuardState(Agent guardedAgent, bool isGuarding)
		{
			if (isGuarding)
			{
				this.AIStateFlags |= Agent.AIStateFlag.Guard;
			}
			else
			{
				this.AIStateFlags &= ~Agent.AIStateFlag.Guard;
			}
			this.SetGuardedAgent(guardedAgent);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00010C40 File Offset: 0x0000EE40
		public void SetCanLeadFormationsRemotely(bool value)
		{
			this._canLeadFormationsRemotely = value;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00010C49 File Offset: 0x0000EE49
		public void SetAveragePingInMilliseconds(double averagePingInMilliseconds)
		{
			MBAPI.IMBAgent.SetAveragePingInMilliseconds(this.GetPtr(), averagePingInMilliseconds);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public void SetTargetPositionAndDirection(Vec2 targetPosition, Vec3 targetDirection)
		{
			MBAPI.IMBAgent.SetTargetPositionAndDirection(this.GetPtr(), ref targetPosition, ref targetDirection);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00010C72 File Offset: 0x0000EE72
		public void SetWatchState(Agent.WatchState watchState)
		{
			this.CurrentWatchState = watchState;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00010C7C File Offset: 0x0000EE7C
		[MBCallback]
		internal void OnWieldedItemIndexChange(bool isOffHand, bool isWieldedInstantly, bool isWieldedOnSpawn)
		{
			if (this.IsMainAgent)
			{
				Agent.OnMainAgentWieldedItemChangeDelegate onMainAgentWieldedItemChange = this.OnMainAgentWieldedItemChange;
				if (onMainAgentWieldedItemChange != null)
				{
					onMainAgentWieldedItemChange();
				}
			}
			Action onAgentWieldedItemChange = this.OnAgentWieldedItemChange;
			if (onAgentWieldedItemChange != null)
			{
				onAgentWieldedItemChange();
			}
			if (GameNetwork.IsServerOrRecorder)
			{
				int mainHandCurUsageIndex = 0;
				EquipmentIndex wieldedItemIndex = this.GetWieldedItemIndex(Agent.HandIndex.MainHand);
				if (wieldedItemIndex != EquipmentIndex.None)
				{
					mainHandCurUsageIndex = this.Equipment[wieldedItemIndex].CurrentUsageIndex;
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetWieldedItemIndex(this.Index, isOffHand, isWieldedInstantly, isWieldedOnSpawn, this.GetWieldedItemIndex(isOffHand ? Agent.HandIndex.OffHand : Agent.HandIndex.MainHand), mainHandCurUsageIndex));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			this.CheckEquipmentForCapeClothSimulationStateChange();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00010D0E File Offset: 0x0000EF0E
		public void SetFormationBanner(ItemObject banner)
		{
			this._formationBanner = banner;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00010D17 File Offset: 0x0000EF17
		public void SetIsAIPaused(bool isPaused)
		{
			this.IsPaused = isPaused;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00010D20 File Offset: 0x0000EF20
		public void ResetEnemyCaches()
		{
			MBAPI.IMBAgent.ResetEnemyCaches(this.GetPtr());
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00010D34 File Offset: 0x0000EF34
		public void SetTargetPositionSynched(ref Vec2 targetPosition)
		{
			if (this.MovementLockedState == AgentMovementLockedState.None || this.GetTargetPosition() != targetPosition)
			{
				if (GameNetwork.IsClientOrReplay)
				{
					this._lastSynchedTargetPosition = targetPosition;
					this._checkIfTargetFrameIsChanged = true;
					return;
				}
				this.SetTargetPosition(targetPosition);
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetAgentTargetPosition(this.Index, ref targetPosition));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public void SetTargetPositionAndDirectionSynched(ref Vec2 targetPosition, ref Vec3 targetDirection)
		{
			if (this.MovementLockedState == AgentMovementLockedState.None || this.GetTargetDirection() != targetDirection)
			{
				if (GameNetwork.IsClientOrReplay)
				{
					this._lastSynchedTargetDirection = targetDirection;
					this._checkIfTargetFrameIsChanged = true;
					return;
				}
				this.SetTargetPositionAndDirection(targetPosition, targetDirection);
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetAgentTargetPositionAndDirection(this.Index, ref targetPosition, ref targetDirection));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00010E23 File Offset: 0x0000F023
		public void SetBodyArmorMaterialType(ArmorComponent.ArmorMaterialTypes bodyArmorMaterialType)
		{
			MBAPI.IMBAgent.SetBodyArmorMaterialType(this.GetPtr(), bodyArmorMaterialType);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00010E36 File Offset: 0x0000F036
		public void SetUsedGameObjectForClient(UsableMissionObject usedObject)
		{
			this.CurrentlyUsedGameObject = usedObject;
			usedObject.OnUse(this);
			this.Mission.OnObjectUsed(this, usedObject);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00010E54 File Offset: 0x0000F054
		public void SetTeam(Team team, bool sync)
		{
			if (this.Team != team)
			{
				Team team2 = this.Team;
				Team team3 = this.Team;
				if (team3 != null)
				{
					team3.RemoveAgentFromTeam(this);
				}
				this.Team = team;
				Team team4 = this.Team;
				if (team4 != null)
				{
					team4.AddAgentToTeam(this);
				}
				this.SetTeamInternal((team != null) ? team.MBTeam : MBTeam.InvalidTeam);
				if (sync && GameNetwork.IsServer && this.Mission.HasMissionBehavior<MissionNetworkComponent>())
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new AgentSetTeam(this.Index, (team != null) ? team.TeamIndex : -1));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				foreach (MissionBehavior missionBehavior in Mission.Current.MissionBehaviors)
				{
					missionBehavior.OnAgentTeamChanged(team2, team, this);
				}
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00010F40 File Offset: 0x0000F140
		public void SetClothingColor1(uint color)
		{
			this._clothingColor1 = new uint?(color);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00010F4E File Offset: 0x0000F14E
		public void SetClothingColor2(uint color)
		{
			this._clothingColor2 = new uint?(color);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00010F5C File Offset: 0x0000F15C
		public void SetWieldedItemIndexAsClient(Agent.HandIndex handIndex, EquipmentIndex equipmentIndex, bool isWieldedInstantly, bool isWieldedOnSpawn, int mainHandCurrentUsageIndex)
		{
			MBAPI.IMBAgent.SetWieldedItemIndexAsClient(this.GetPtr(), (int)handIndex, (int)equipmentIndex, isWieldedInstantly, isWieldedOnSpawn, mainHandCurrentUsageIndex);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00010F75 File Offset: 0x0000F175
		public void SetPreciseRangedAimingEnabled(bool set)
		{
			if (set)
			{
				this.SetScriptedFlags(this.GetScriptedFlags() | Agent.AIScriptedFrameFlags.RangerCanMoveForClearTarget);
				return;
			}
			this.SetScriptedFlags(this.GetScriptedFlags() & ~Agent.AIScriptedFrameFlags.RangerCanMoveForClearTarget);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00010F9F File Offset: 0x0000F19F
		public void SetAsConversationAgent(bool set)
		{
			if (set)
			{
				this.SetScriptedFlags(this.GetScriptedFlags() | Agent.AIScriptedFrameFlags.InConversation);
				this.DisableLookToPointOfInterest();
				return;
			}
			this.SetScriptedFlags(this.GetScriptedFlags() & ~Agent.AIScriptedFrameFlags.InConversation);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00010FCF File Offset: 0x0000F1CF
		public void SetCrouchMode(bool set)
		{
			if (set)
			{
				this.SetScriptedFlags(this.GetScriptedFlags() | Agent.AIScriptedFrameFlags.Crouch);
				return;
			}
			this.SetScriptedFlags(this.GetScriptedFlags() & ~Agent.AIScriptedFrameFlags.Crouch);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00010FF9 File Offset: 0x0000F1F9
		public void SetWeaponAmountInSlot(EquipmentIndex equipmentSlot, short amount, bool enforcePrimaryItem)
		{
			MBAPI.IMBAgent.SetWeaponAmountInSlot(this.GetPtr(), (int)equipmentSlot, amount, enforcePrimaryItem);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001100E File Offset: 0x0000F20E
		public void SetWeaponAmmoAsClient(EquipmentIndex equipmentIndex, EquipmentIndex ammoEquipmentIndex, short ammo)
		{
			MBAPI.IMBAgent.SetWeaponAmmoAsClient(this.GetPtr(), (int)equipmentIndex, (int)ammoEquipmentIndex, ammo);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00011023 File Offset: 0x0000F223
		public void SetWeaponReloadPhaseAsClient(EquipmentIndex equipmentIndex, short reloadState)
		{
			MBAPI.IMBAgent.SetWeaponReloadPhaseAsClient(this.GetPtr(), (int)equipmentIndex, reloadState);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00011037 File Offset: 0x0000F237
		public void SetReloadAmmoInSlot(EquipmentIndex equipmentIndex, EquipmentIndex ammoSlotIndex, short reloadedAmmo)
		{
			MBAPI.IMBAgent.SetReloadAmmoInSlot(this.GetPtr(), (int)equipmentIndex, (int)ammoSlotIndex, reloadedAmmo);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001104C File Offset: 0x0000F24C
		public void SetUsageIndexOfWeaponInSlotAsClient(EquipmentIndex slotIndex, int usageIndex)
		{
			MBAPI.IMBAgent.SetUsageIndexOfWeaponInSlotAsClient(this.GetPtr(), (int)slotIndex, usageIndex);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00011060 File Offset: 0x0000F260
		public void SetRandomizeColors(bool shouldRandomize)
		{
			this.RandomizeColors = shouldRandomize;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00011069 File Offset: 0x0000F269
		[MBCallback]
		internal void OnRemoveWeapon(EquipmentIndex slotIndex)
		{
			this.RemoveEquippedWeapon(slotIndex);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00011072 File Offset: 0x0000F272
		public void SetFormationFrameDisabled()
		{
			MBAPI.IMBAgent.SetFormationFrameDisabled(this.GetPtr());
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00011084 File Offset: 0x0000F284
		public void SetFormationFrameEnabled(WorldPosition position, Vec2 direction, Vec2 positionVelocity, float formationDirectionEnforcingFactor)
		{
			MBAPI.IMBAgent.SetFormationFrameEnabled(this.GetPtr(), position, direction, positionVelocity, formationDirectionEnforcingFactor);
			if (this.Mission.IsTeleportingAgents)
			{
				this.TeleportToPosition(position.GetGroundVec3());
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000110B6 File Offset: 0x0000F2B6
		public void SetShouldCatchUpWithFormation(bool value)
		{
			MBAPI.IMBAgent.SetShouldCatchUpWithFormation(this.GetPtr(), value);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000110C9 File Offset: 0x0000F2C9
		public void SetFormationIntegrityData(Vec2 position, Vec2 currentFormationDirection, Vec2 averageVelocityOfCloseAgents, float averageMaxUnlimitedSpeedOfCloseAgents, float deviationOfPositions)
		{
			MBAPI.IMBAgent.SetFormationIntegrityData(this.GetPtr(), position, currentFormationDirection, averageVelocityOfCloseAgents, averageMaxUnlimitedSpeedOfCloseAgents, deviationOfPositions);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public void SetGuardedAgent(Agent guardedAgent)
		{
			int guardedAgentIndex = (guardedAgent != null) ? guardedAgent.Index : -1;
			MBAPI.IMBAgent.SetGuardedAgentIndex(this.GetPtr(), guardedAgentIndex);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001110F File Offset: 0x0000F30F
		[MBCallback]
		internal void OnWeaponUsageIndexChange(EquipmentIndex slotIndex, int usageIndex)
		{
			this.Equipment.SetUsageIndexOfSlot(slotIndex, usageIndex);
			this.UpdateAgentProperties();
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new WeaponUsageIndexChangeMessage(this.Index, slotIndex, usageIndex));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001114A File Offset: 0x0000F34A
		public void SetCurrentActionProgress(int channelNo, float progress)
		{
			MBAPI.IMBAgent.SetCurrentActionProgress(this.GetPtr(), channelNo, progress);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001115E File Offset: 0x0000F35E
		public void SetCurrentActionSpeed(int channelNo, float speed)
		{
			MBAPI.IMBAgent.SetCurrentActionSpeed(this.GetPtr(), channelNo, speed);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00011174 File Offset: 0x0000F374
		public bool SetActionChannel(int channelNo, ActionIndexCache actionIndexCache, bool ignorePriority = false, ulong additionalFlags = 0UL, float blendWithNextActionFactor = 0f, float actionSpeed = 1f, float blendInPeriod = -0.2f, float blendOutPeriodToNoAnim = 0.4f, float startProgress = 0f, bool useLinearSmoothing = false, float blendOutPeriod = -0.2f, int actionShift = 0, bool forceFaceMorphRestart = true)
		{
			int index = actionIndexCache.Index;
			return MBAPI.IMBAgent.SetActionChannel(this.GetPtr(), channelNo, index + actionShift, additionalFlags, ignorePriority, blendWithNextActionFactor, actionSpeed, blendInPeriod, blendOutPeriodToNoAnim, startProgress, useLinearSmoothing, blendOutPeriod, forceFaceMorphRestart);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000111B0 File Offset: 0x0000F3B0
		public bool SetActionChannel(int channelNo, ActionIndexValueCache actionIndexCache, bool ignorePriority = false, ulong additionalFlags = 0UL, float blendWithNextActionFactor = 0f, float actionSpeed = 1f, float blendInPeriod = -0.2f, float blendOutPeriodToNoAnim = 0.4f, float startProgress = 0f, bool useLinearSmoothing = false, float blendOutPeriod = -0.2f, int actionShift = 0, bool forceFaceMorphRestart = true)
		{
			int index = actionIndexCache.Index;
			return MBAPI.IMBAgent.SetActionChannel(this.GetPtr(), channelNo, index + actionShift, additionalFlags, ignorePriority, blendWithNextActionFactor, actionSpeed, blendInPeriod, blendOutPeriodToNoAnim, startProgress, useLinearSmoothing, blendOutPeriod, forceFaceMorphRestart);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000111ED File Offset: 0x0000F3ED
		[MBCallback]
		internal void OnWeaponAmountChange(EquipmentIndex slotIndex, short amount)
		{
			this.Equipment.SetAmountOfSlot(slotIndex, amount, false);
			this.UpdateAgentProperties();
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetWeaponNetworkData(this.Index, slotIndex, amount));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00011229 File Offset: 0x0000F429
		public void SetAttackState(int attackState)
		{
			MBAPI.IMBAgent.SetAttackState(this.GetPtr(), attackState);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001123C File Offset: 0x0000F43C
		public void SetAIBehaviorParams(HumanAIComponent.AISimpleBehaviorKind behavior, float y1, float x2, float y2, float x3, float y3)
		{
			MBAPI.IMBAgent.SetAIBehaviorParams(this.GetPtr(), (int)behavior, y1, x2, y2, x3, y3);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00011257 File Offset: 0x0000F457
		public void SetAllBehaviorParams(HumanAIComponent.BehaviorValues[] behaviorParams)
		{
			MBAPI.IMBAgent.SetAllAIBehaviorParams(this.GetPtr(), behaviorParams);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001126A File Offset: 0x0000F46A
		public void SetMovementDirection(in Vec2 direction)
		{
			MBAPI.IMBAgent.SetMovementDirection(this.GetPtr(), direction);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001127D File Offset: 0x0000F47D
		public void SetScriptedFlags(Agent.AIScriptedFrameFlags flags)
		{
			MBAPI.IMBAgent.SetScriptedFlags(this.GetPtr(), (int)flags);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00011290 File Offset: 0x0000F490
		public void SetScriptedCombatFlags(Agent.AISpecialCombatModeFlags flags)
		{
			MBAPI.IMBAgent.SetScriptedCombatFlags(this.GetPtr(), (int)flags);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000112A4 File Offset: 0x0000F4A4
		public void SetScriptedPositionAndDirection(ref WorldPosition scriptedPosition, float scriptedDirection, bool addHumanLikeDelay, Agent.AIScriptedFrameFlags additionalFlags = Agent.AIScriptedFrameFlags.None)
		{
			MBAPI.IMBAgent.SetScriptedPositionAndDirection(this.GetPtr(), ref scriptedPosition, scriptedDirection, addHumanLikeDelay, (int)additionalFlags);
			if (this.Mission.IsTeleportingAgents && scriptedPosition.AsVec2 != this.Position.AsVec2)
			{
				this.TeleportToPosition(scriptedPosition.GetGroundVec3());
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000112FC File Offset: 0x0000F4FC
		public void SetScriptedPosition(ref WorldPosition position, bool addHumanLikeDelay, Agent.AIScriptedFrameFlags additionalFlags = Agent.AIScriptedFrameFlags.None)
		{
			MBAPI.IMBAgent.SetScriptedPosition(this.GetPtr(), ref position, addHumanLikeDelay, (int)additionalFlags);
			if (this.Mission.IsTeleportingAgents && position.AsVec2 != this.Position.AsVec2)
			{
				this.TeleportToPosition(position.GetGroundVec3());
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00011351 File Offset: 0x0000F551
		public void SetScriptedTargetEntityAndPosition(GameEntity target, WorldPosition position, Agent.AISpecialCombatModeFlags additionalFlags = Agent.AISpecialCombatModeFlags.None, bool ignoreIfAlreadyAttacking = false)
		{
			MBAPI.IMBAgent.SetScriptedTargetEntity(this.GetPtr(), target.Pointer, ref position, (int)additionalFlags, ignoreIfAlreadyAttacking);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001136E File Offset: 0x0000F56E
		public void SetAgentExcludeStateForFaceGroupId(int faceGroupId, bool isExcluded)
		{
			MBAPI.IMBAgent.SetAgentExcludeStateForFaceGroupId(this.GetPtr(), faceGroupId, isExcluded);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00011382 File Offset: 0x0000F582
		public void SetLookAgent(Agent agent)
		{
			this._lookAgentCache = agent;
			MBAPI.IMBAgent.SetLookAgent(this.GetPtr(), (agent != null) ? agent.GetPtr() : UIntPtr.Zero);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000113AB File Offset: 0x0000F5AB
		public void SetInteractionAgent(Agent agent)
		{
			MBAPI.IMBAgent.SetInteractionAgent(this.GetPtr(), (agent != null) ? agent.GetPtr() : UIntPtr.Zero);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000113CD File Offset: 0x0000F5CD
		public void SetLookToPointOfInterest(Vec3 point)
		{
			MBAPI.IMBAgent.SetLookToPointOfInterest(this.GetPtr(), point);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000113E0 File Offset: 0x0000F5E0
		public void SetAgentFlags(AgentFlag agentFlags)
		{
			MBAPI.IMBAgent.SetAgentFlags(this.GetPtr(), (uint)agentFlags);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000113F3 File Offset: 0x0000F5F3
		public void SetSelectedMountIndex(int mountIndex)
		{
			MBAPI.IMBAgent.SetSelectedMountIndex(this.GetPtr(), mountIndex);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00011406 File Offset: 0x0000F606
		public int GetSelectedMountIndex()
		{
			return MBAPI.IMBAgent.GetSelectedMountIndex(this.GetPtr());
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00011418 File Offset: 0x0000F618
		public int GetFiringOrder()
		{
			return MBAPI.IMBAgent.GetFiringOrder(this.GetPtr());
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001142A File Offset: 0x0000F62A
		public void SetFiringOrder(FiringOrder.RangedWeaponUsageOrderEnum order)
		{
			MBAPI.IMBAgent.SetFiringOrder(this.GetPtr(), (int)order);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001143D File Offset: 0x0000F63D
		public int GetRidingOrder()
		{
			return MBAPI.IMBAgent.GetRidingOrder(this.GetPtr());
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001144F File Offset: 0x0000F64F
		public void SetRidingOrder(RidingOrder.RidingOrderEnum order)
		{
			MBAPI.IMBAgent.SetRidingOrder(this.GetPtr(), (int)order);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00011462 File Offset: 0x0000F662
		public int GetTargetFormationIndex()
		{
			return MBAPI.IMBAgent.GetTargetFormationIndex(this.GetPtr());
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00011474 File Offset: 0x0000F674
		public void SetTargetFormationIndex(int targetFormationIndex)
		{
			MBAPI.IMBAgent.SetTargetFormationIndex(this.GetPtr(), targetFormationIndex);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00011487 File Offset: 0x0000F687
		public void SetAgentFacialAnimation(Agent.FacialAnimChannel channel, string animationName, bool loop)
		{
			MBAPI.IMBAgent.SetAgentFacialAnimation(this.GetPtr(), (int)channel, animationName, loop);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001149C File Offset: 0x0000F69C
		public bool SetHandInverseKinematicsFrame(ref MatrixFrame leftGlobalFrame, ref MatrixFrame rightGlobalFrame)
		{
			return MBAPI.IMBAgent.SetHandInverseKinematicsFrame(this.GetPtr(), ref leftGlobalFrame, ref rightGlobalFrame);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public void SetNativeFormationNo(int formationNo)
		{
			MBAPI.IMBAgent.SetFormationNo(this.GetPtr(), formationNo);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000114C3 File Offset: 0x0000F6C3
		public void SetDirectionChangeTendency(float tendency)
		{
			MBAPI.IMBAgent.SetDirectionChangeTendency(this.GetPtr(), tendency);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000114D8 File Offset: 0x0000F6D8
		public float GetBattleImportance()
		{
			BasicCharacterObject character = this.Character;
			float num = (character != null) ? character.GetBattlePower() : 1f;
			if (this.Team != null && this == this.Team.GeneralAgent)
			{
				num *= 2f;
			}
			else if (this.Formation != null && this == this.Formation.Captain)
			{
				num *= 1.2f;
			}
			return num;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001153C File Offset: 0x0000F73C
		public void SetSynchedPrefabComponentVisibility(int componentIndex, bool visibility)
		{
			this._synchedBodyComponents[componentIndex].SetVisible(visibility);
			this.AgentVisuals.LazyUpdateAgentRendererData();
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetAgentPrefabComponentVisibility(this.Index, componentIndex, visibility));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001158C File Offset: 0x0000F78C
		public void SetActionSet(ref AnimationSystemData animationSystemData)
		{
			MBAPI.IMBAgent.SetActionSet(this.GetPtr(), ref animationSystemData);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetAgentActionSet(this.Index, animationSystemData));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000115CC File Offset: 0x0000F7CC
		public void SetColumnwiseFollowAgent(Agent followAgent, ref Vec2 followPosition)
		{
			if (!this.IsAIControlled)
			{
				return;
			}
			int followAgentIndex = (followAgent != null) ? followAgent.Index : -1;
			MBAPI.IMBAgent.SetColumnwiseFollowAgent(this.GetPtr(), followAgentIndex, ref followPosition);
			this.SetFollowedUnit(followAgent);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00011608 File Offset: 0x0000F808
		public void SetHandInverseKinematicsFrameForMissionObjectUsage(in MatrixFrame localIKFrame, in MatrixFrame boundEntityGlobalFrame, float animationHeightDifference = 0f)
		{
			if (this.GetCurrentActionValue(1) != ActionIndexValueCache.act_none && this.GetActionChannelWeight(1) > 0f)
			{
				MBAPI.IMBAgent.SetHandInverseKinematicsFrameForMissionObjectUsage(this.GetPtr(), localIKFrame, boundEntityGlobalFrame, animationHeightDifference);
				return;
			}
			this.ClearHandInverseKinematics();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00011646 File Offset: 0x0000F846
		public void SetWantsToYell()
		{
			this._wantsToYell = true;
			this._yellTimer = MBRandom.RandomFloat * 0.3f + 0.1f;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00011668 File Offset: 0x0000F868
		public void SetCapeClothSimulator(GameEntityComponent clothSimulatorComponent)
		{
			ClothSimulatorComponent capeClothSimulator = clothSimulatorComponent as ClothSimulatorComponent;
			this._capeClothSimulator = capeClothSimulator;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00011683 File Offset: 0x0000F883
		public Vec2 GetTargetPosition()
		{
			return MBAPI.IMBAgent.GetTargetPosition(this.GetPtr());
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00011695 File Offset: 0x0000F895
		public Vec3 GetTargetDirection()
		{
			return MBAPI.IMBAgent.GetTargetDirection(this.GetPtr());
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000116A7 File Offset: 0x0000F8A7
		public float GetAimingTimer()
		{
			return MBAPI.IMBAgent.GetAimingTimer(this.GetPtr());
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000116BC File Offset: 0x0000F8BC
		public float GetInteractionDistanceToUsable(IUsable usable)
		{
			Agent agent;
			if ((agent = (usable as Agent)) != null)
			{
				if (!agent.IsMount)
				{
					return 3f;
				}
				return 1.75f;
			}
			else
			{
				SpawnedItemEntity spawnedItemEntity;
				if ((spawnedItemEntity = (usable as SpawnedItemEntity)) != null && spawnedItemEntity.IsBanner())
				{
					return 3f;
				}
				float interactionDistance = MissionGameModels.Current.AgentStatCalculateModel.GetInteractionDistance(this);
				if (!(usable is StandingPoint))
				{
					return interactionDistance;
				}
				if (!this.IsAIControlled || !this.WalkMode)
				{
					return 1f;
				}
				return 0.5f;
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00011735 File Offset: 0x0000F935
		public TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			if (this.IsMount && !userAgent.CheckSkillForMounting(this))
			{
				return GameTexts.FindText("str_ui_riding_skill_not_adequate_to_mount", null);
			}
			return TextObject.Empty;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001175C File Offset: 0x0000F95C
		public T GetController<T>() where T : AgentController
		{
			for (int i = 0; i < this._agentControllers.Count; i++)
			{
				if (this._agentControllers[i] is T)
				{
					return (T)((object)this._agentControllers[i]);
				}
			}
			return default(T);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000117AD File Offset: 0x0000F9AD
		public EquipmentIndex GetWieldedItemIndex(Agent.HandIndex index)
		{
			return MBAPI.IMBAgent.GetWieldedItemIndex(this.GetPtr(), (int)index);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public float GetTrackDistanceToMainAgent()
		{
			float result = -1f;
			if (Agent.Main != null)
			{
				result = Agent.Main.Position.Distance(this.Position);
			}
			return result;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000117F4 File Offset: 0x0000F9F4
		public string GetDescriptionText(GameEntity gameEntity = null)
		{
			return this.Name;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000117FC File Offset: 0x0000F9FC
		public GameEntity GetWeaponEntityFromEquipmentSlot(EquipmentIndex slotIndex)
		{
			return new GameEntity(MBAPI.IMBAgent.GetWeaponEntityFromEquipmentSlot(this.GetPtr(), (int)slotIndex));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00011814 File Offset: 0x0000FA14
		public WorldPosition GetRetreatPos()
		{
			return MBAPI.IMBAgent.GetRetreatPos(this.GetPtr());
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00011826 File Offset: 0x0000FA26
		public Agent.AIScriptedFrameFlags GetScriptedFlags()
		{
			return (Agent.AIScriptedFrameFlags)MBAPI.IMBAgent.GetScriptedFlags(this.GetPtr());
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00011838 File Offset: 0x0000FA38
		public Agent.AISpecialCombatModeFlags GetScriptedCombatFlags()
		{
			return (Agent.AISpecialCombatModeFlags)MBAPI.IMBAgent.GetScriptedCombatFlags(this.GetPtr());
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001184C File Offset: 0x0000FA4C
		public GameEntity GetSteppedEntity()
		{
			UIntPtr steppedEntityId = MBAPI.IMBAgent.GetSteppedEntityId(this.GetPtr());
			if (!(steppedEntityId != UIntPtr.Zero))
			{
				return null;
			}
			return new GameEntity(steppedEntityId);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001187F File Offset: 0x0000FA7F
		public AnimFlags GetCurrentAnimationFlag(int channelNo)
		{
			return (AnimFlags)MBAPI.IMBAgent.GetCurrentAnimationFlags(this.GetPtr(), channelNo);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00011892 File Offset: 0x0000FA92
		public ActionIndexCache GetCurrentAction(int channelNo)
		{
			return new ActionIndexCache(MBAPI.IMBAgent.GetCurrentAction(this.GetPtr(), channelNo));
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000118AA File Offset: 0x0000FAAA
		public ActionIndexValueCache GetCurrentActionValue(int channelNo)
		{
			return new ActionIndexValueCache(MBAPI.IMBAgent.GetCurrentAction(this.GetPtr(), channelNo));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000118C2 File Offset: 0x0000FAC2
		public Agent.ActionCodeType GetCurrentActionType(int channelNo)
		{
			return (Agent.ActionCodeType)MBAPI.IMBAgent.GetCurrentActionType(this.GetPtr(), channelNo);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000118D5 File Offset: 0x0000FAD5
		public Agent.ActionStage GetCurrentActionStage(int channelNo)
		{
			return (Agent.ActionStage)MBAPI.IMBAgent.GetCurrentActionStage(this.GetPtr(), channelNo);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000118E8 File Offset: 0x0000FAE8
		public Agent.UsageDirection GetCurrentActionDirection(int channelNo)
		{
			return (Agent.UsageDirection)MBAPI.IMBAgent.GetCurrentActionDirection(this.GetPtr(), channelNo);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000118FB File Offset: 0x0000FAFB
		public int GetCurrentActionPriority(int channelNo)
		{
			return MBAPI.IMBAgent.GetCurrentActionPriority(this.GetPtr(), channelNo);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001190E File Offset: 0x0000FB0E
		public float GetCurrentActionProgress(int channelNo)
		{
			return MBAPI.IMBAgent.GetCurrentActionProgress(this.GetPtr(), channelNo);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00011921 File Offset: 0x0000FB21
		public float GetActionChannelWeight(int channelNo)
		{
			return MBAPI.IMBAgent.GetActionChannelWeight(this.GetPtr(), channelNo);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00011934 File Offset: 0x0000FB34
		public float GetActionChannelCurrentActionWeight(int channelNo)
		{
			return MBAPI.IMBAgent.GetActionChannelCurrentActionWeight(this.GetPtr(), channelNo);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00011947 File Offset: 0x0000FB47
		public WorldFrame GetWorldFrame()
		{
			return new WorldFrame(this.LookRotation, this.GetWorldPosition());
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001195A File Offset: 0x0000FB5A
		public float GetLookDownLimit()
		{
			return MBAPI.IMBAgent.GetLookDownLimit(this.GetPtr());
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001196C File Offset: 0x0000FB6C
		public float GetEyeGlobalHeight()
		{
			return MBAPI.IMBAgent.GetEyeGlobalHeight(this.GetPtr());
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001197E File Offset: 0x0000FB7E
		public float GetMaximumSpeedLimit()
		{
			return MBAPI.IMBAgent.GetMaximumSpeedLimit(this.GetPtr());
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00011990 File Offset: 0x0000FB90
		public Vec2 GetCurrentVelocity()
		{
			return MBAPI.IMBAgent.GetCurrentVelocity(this.GetPtr());
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000119A2 File Offset: 0x0000FBA2
		public float GetTurnSpeed()
		{
			return MBAPI.IMBAgent.GetTurnSpeed(this.GetPtr());
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000119B4 File Offset: 0x0000FBB4
		public float GetCurrentSpeedLimit()
		{
			return MBAPI.IMBAgent.GetCurrentSpeedLimit(this.GetPtr());
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000119C6 File Offset: 0x0000FBC6
		public Vec2 GetMovementDirection()
		{
			return MBAPI.IMBAgent.GetMovementDirection(this.GetPtr());
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public Vec3 GetCurWeaponOffset()
		{
			return MBAPI.IMBAgent.GetCurWeaponOffset(this.GetPtr());
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000119EA File Offset: 0x0000FBEA
		public bool GetIsLeftStance()
		{
			return MBAPI.IMBAgent.GetIsLeftStance(this.GetPtr());
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000119FC File Offset: 0x0000FBFC
		public float GetPathDistanceToPoint(ref Vec3 point)
		{
			return MBAPI.IMBAgent.GetPathDistanceToPoint(this.GetPtr(), ref point);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00011A0F File Offset: 0x0000FC0F
		public int GetCurrentNavigationFaceId()
		{
			return MBAPI.IMBAgent.GetCurrentNavigationFaceId(this.GetPtr());
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00011A21 File Offset: 0x0000FC21
		public WorldPosition GetWorldPosition()
		{
			return MBAPI.IMBAgent.GetWorldPosition(this.GetPtr());
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00011A33 File Offset: 0x0000FC33
		public Agent GetLookAgent()
		{
			return this._lookAgentCache;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00011A3B File Offset: 0x0000FC3B
		public Agent GetTargetAgent()
		{
			return MBAPI.IMBAgent.GetTargetAgent(this.GetPtr());
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00011A4D File Offset: 0x0000FC4D
		public void SetTargetAgent(Agent agent)
		{
			MBAPI.IMBAgent.SetTargetAgent(this.GetPtr(), (agent != null) ? agent.Index : -1);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00011A6B File Offset: 0x0000FC6B
		public void SetAutomaticTargetSelection(bool enable)
		{
			MBAPI.IMBAgent.SetAutomaticTargetSelection(this.GetPtr(), enable);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00011A7E File Offset: 0x0000FC7E
		public AgentFlag GetAgentFlags()
		{
			return AgentHelper.GetAgentFlags(this.FlagsPointer);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00011A8B File Offset: 0x0000FC8B
		public string GetAgentFacialAnimation()
		{
			return MBAPI.IMBAgent.GetAgentFacialAnimation(this.GetPtr());
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00011A9D File Offset: 0x0000FC9D
		public string GetAgentVoiceDefinition()
		{
			return MBAPI.IMBAgent.GetAgentVoiceDefinition(this.GetPtr());
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00011AAF File Offset: 0x0000FCAF
		public Vec3 GetEyeGlobalPosition()
		{
			return MBAPI.IMBAgent.GetEyeGlobalPosition(this.GetPtr());
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00011AC1 File Offset: 0x0000FCC1
		public Vec3 GetChestGlobalPosition()
		{
			return MBAPI.IMBAgent.GetChestGlobalPosition(this.GetPtr());
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00011AD3 File Offset: 0x0000FCD3
		public Agent.MovementControlFlag GetDefendMovementFlag()
		{
			return MBAPI.IMBAgent.GetDefendMovementFlag(this.GetPtr());
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00011AE5 File Offset: 0x0000FCE5
		public Agent.UsageDirection GetAttackDirection()
		{
			return MBAPI.IMBAgent.GetAttackDirection(this.GetPtr());
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00011AF8 File Offset: 0x0000FCF8
		public WeaponInfo GetWieldedWeaponInfo(Agent.HandIndex handIndex)
		{
			bool isMeleeWeapon = false;
			bool isRangedWeapon = false;
			if (MBAPI.IMBAgent.GetWieldedWeaponInfo(this.GetPtr(), (int)handIndex, ref isMeleeWeapon, ref isRangedWeapon))
			{
				return new WeaponInfo(true, isMeleeWeapon, isRangedWeapon);
			}
			return new WeaponInfo(false, false, false);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00011B34 File Offset: 0x0000FD34
		public Vec2 GetBodyRotationConstraint(int channelIndex = 1)
		{
			return MBAPI.IMBAgent.GetBodyRotationConstraint(this.GetPtr(), channelIndex).AsVec2;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00011B5A File Offset: 0x0000FD5A
		public float GetTotalEncumbrance()
		{
			return this.AgentDrivenProperties.ArmorEncumbrance + this.AgentDrivenProperties.WeaponsEncumbrance;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00011B74 File Offset: 0x0000FD74
		public T GetComponent<T>() where T : AgentComponent
		{
			for (int i = 0; i < this._components.Count; i++)
			{
				if (this._components[i] is T)
				{
					return (T)((object)this._components[i]);
				}
			}
			return default(T);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00011BC5 File Offset: 0x0000FDC5
		public float GetAgentDrivenPropertyValue(DrivenProperty type)
		{
			return this.AgentDrivenProperties.GetStat(type);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public UsableMachine GetSteppedMachine()
		{
			GameEntity gameEntity = this.GetSteppedEntity();
			while (gameEntity != null && !gameEntity.HasScriptOfType<UsableMachine>())
			{
				gameEntity = gameEntity.Parent;
			}
			if (gameEntity != null)
			{
				return gameEntity.GetFirstScriptOfType<UsableMachine>();
			}
			return null;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00011C13 File Offset: 0x0000FE13
		public int GetAttachedWeaponsCount()
		{
			List<ValueTuple<MissionWeapon, MatrixFrame, sbyte>> attachedWeapons = this._attachedWeapons;
			if (attachedWeapons == null)
			{
				return 0;
			}
			return attachedWeapons.Count;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00011C26 File Offset: 0x0000FE26
		public MissionWeapon GetAttachedWeapon(int index)
		{
			return this._attachedWeapons[index].Item1;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00011C39 File Offset: 0x0000FE39
		public MatrixFrame GetAttachedWeaponFrame(int index)
		{
			return this._attachedWeapons[index].Item2;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00011C4C File Offset: 0x0000FE4C
		public sbyte GetAttachedWeaponBoneIndex(int index)
		{
			return this._attachedWeapons[index].Item3;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00011C5F File Offset: 0x0000FE5F
		public void DeleteAttachedWeapon(int index)
		{
			this._attachedWeapons.RemoveAt(index);
			MBAPI.IMBAgent.DeleteAttachedWeaponFromBone(this.Pointer, index);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00011C80 File Offset: 0x0000FE80
		public bool HasRangedWeapon(bool checkHasAmmo = false)
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				int num;
				bool flag;
				bool flag2;
				if (!this.Equipment[equipmentIndex].IsEmpty && this.Equipment[equipmentIndex].GetRangedUsageIndex() >= 0 && (!checkHasAmmo || this.Equipment.HasAmmo(equipmentIndex, out num, out flag, out flag2)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		public void GetFormationFileAndRankInfo(out int fileIndex, out int rankIndex)
		{
			fileIndex = ((IFormationUnit)this).FormationFileIndex;
			rankIndex = ((IFormationUnit)this).FormationRankIndex;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00011D00 File Offset: 0x0000FF00
		public void GetFormationFileAndRankInfo(out int fileIndex, out int rankIndex, out int fileCount, out int rankCount)
		{
			fileIndex = ((IFormationUnit)this).FormationFileIndex;
			rankIndex = ((IFormationUnit)this).FormationRankIndex;
			LineFormation lineFormation;
			if ((lineFormation = (((IFormationUnit)this).Formation as LineFormation)) != null)
			{
				lineFormation.GetFormationInfo(out fileCount, out rankCount);
				return;
			}
			fileCount = -1;
			rankCount = -1;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00011D3F File Offset: 0x0000FF3F
		internal Vec2 GetWallDirectionOfRelativeFormationLocation()
		{
			return this.Formation.GetWallDirectionOfRelativeFormationLocation(this);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00011D4D File Offset: 0x0000FF4D
		public void SetMortalityState(Agent.MortalityState newState)
		{
			this.CurrentMortalityState = newState;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00011D56 File Offset: 0x0000FF56
		public void ToggleInvulnerable()
		{
			if (this.CurrentMortalityState == Agent.MortalityState.Invulnerable)
			{
				this.CurrentMortalityState = Agent.MortalityState.Mortal;
				return;
			}
			this.CurrentMortalityState = Agent.MortalityState.Invulnerable;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00011D70 File Offset: 0x0000FF70
		public float GetArmLength()
		{
			return this.Monster.ArmLength * this.AgentScale;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00011D84 File Offset: 0x0000FF84
		public float GetArmWeight()
		{
			return this.Monster.ArmWeight * this.AgentScale;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00011D98 File Offset: 0x0000FF98
		public void GetRunningSimulationDataUntilMaximumSpeedReached(ref float combatAccelerationTime, ref float maxSpeed, float[] speedValues)
		{
			MBAPI.IMBAgent.GetRunningSimulationDataUntilMaximumSpeedReached(this.GetPtr(), ref combatAccelerationTime, ref maxSpeed, speedValues);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00011DAD File Offset: 0x0000FFAD
		public void SetMaximumSpeedLimit(float maximumSpeedLimit, bool isMultiplier)
		{
			MBAPI.IMBAgent.SetMaximumSpeedLimit(this.GetPtr(), maximumSpeedLimit, isMultiplier);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		public float GetBaseArmorEffectivenessForBodyPart(BoneBodyPartType bodyPart)
		{
			if (!this.IsHuman)
			{
				return this.GetAgentDrivenPropertyValue(DrivenProperty.ArmorTorso);
			}
			if (bodyPart == BoneBodyPartType.None)
			{
				return 0f;
			}
			if (bodyPart == BoneBodyPartType.Head || bodyPart == BoneBodyPartType.Neck)
			{
				return this.GetAgentDrivenPropertyValue(DrivenProperty.ArmorHead);
			}
			if (bodyPart == BoneBodyPartType.Legs)
			{
				return this.GetAgentDrivenPropertyValue(DrivenProperty.ArmorLegs);
			}
			if (bodyPart == BoneBodyPartType.ArmLeft || bodyPart == BoneBodyPartType.ArmRight)
			{
				return this.GetAgentDrivenPropertyValue(DrivenProperty.ArmorArms);
			}
			if (bodyPart == BoneBodyPartType.ShoulderLeft || bodyPart == BoneBodyPartType.ShoulderRight || bodyPart == BoneBodyPartType.Chest || bodyPart == BoneBodyPartType.Abdomen)
			{
				return this.GetAgentDrivenPropertyValue(DrivenProperty.ArmorTorso);
			}
			Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Agent.cs", "GetBaseArmorEffectivenessForBodyPart", 2826);
			return this.GetAgentDrivenPropertyValue(DrivenProperty.ArmorTorso);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00011E54 File Offset: 0x00010054
		public AITargetVisibilityState GetLastTargetVisibilityState()
		{
			return (AITargetVisibilityState)MBAPI.IMBAgent.GetLastTargetVisibilityState(this.GetPtr());
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00011E66 File Offset: 0x00010066
		public float GetMissileRange()
		{
			return MBAPI.IMBAgent.GetMissileRange(this.GetPtr());
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00011E78 File Offset: 0x00010078
		public ItemObject GetWeaponToReplaceOnQuickAction(SpawnedItemEntity spawnedItem, out EquipmentIndex possibleSlotIndex)
		{
			EquipmentIndex equipmentIndex = MissionEquipment.SelectWeaponPickUpSlot(this, spawnedItem.WeaponCopy, spawnedItem.IsStuckMissile());
			possibleSlotIndex = equipmentIndex;
			if (equipmentIndex != EquipmentIndex.None && !this.Equipment[equipmentIndex].IsEmpty && ((!spawnedItem.IsStuckMissile() && !spawnedItem.WeaponCopy.IsAnyConsumable()) || this.Equipment[equipmentIndex].Item.PrimaryWeapon.WeaponClass != spawnedItem.WeaponCopy.Item.PrimaryWeapon.WeaponClass || !this.Equipment[equipmentIndex].IsAnyConsumable() || this.Equipment[equipmentIndex].Amount == this.Equipment[equipmentIndex].ModifiedMaxAmount))
			{
				return this.Equipment[equipmentIndex].Item;
			}
			return null;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00011F64 File Offset: 0x00010164
		public Agent.Hitter GetAssistingHitter(MissionPeer killerPeer)
		{
			Agent.Hitter hitter = null;
			foreach (Agent.Hitter hitter2 in this.HitterList)
			{
				if (hitter2.HitterPeer != killerPeer && (hitter == null || hitter2.Damage > hitter.Damage))
				{
					hitter = hitter2;
				}
			}
			if (hitter != null && hitter.Damage >= 35f)
			{
				return hitter;
			}
			return null;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00011FE4 File Offset: 0x000101E4
		public bool CanReachAgent(Agent otherAgent)
		{
			float interactionDistanceToUsable = this.GetInteractionDistanceToUsable(otherAgent);
			return this.Position.DistanceSquared(otherAgent.Position) < interactionDistanceToUsable * interactionDistanceToUsable;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00012014 File Offset: 0x00010214
		public bool CanInteractWithAgent(Agent otherAgent, float userAgentCameraElevation)
		{
			bool flag = false;
			foreach (MissionBehavior missionBehavior in Mission.Current.MissionBehaviors)
			{
				flag = (flag || missionBehavior.IsThereAgentAction(this, otherAgent));
			}
			if (!flag)
			{
				return false;
			}
			bool flag2 = this.CanReachAgent(otherAgent);
			if (!otherAgent.IsMount)
			{
				return this.IsOnLand() && flag2;
			}
			if ((this.MountAgent == null && this.GetCurrentActionValue(0) != ActionIndexValueCache.act_none) || (this.MountAgent != null && !this.IsOnLand()))
			{
				return false;
			}
			if (otherAgent.RiderAgent == null)
			{
				return this.MountAgent == null && flag2 && this.CheckSkillForMounting(otherAgent) && otherAgent.GetCurrentActionType(0) != Agent.ActionCodeType.Rear;
			}
			return otherAgent == this.MountAgent && (flag2 && userAgentCameraElevation < this.GetLookDownLimit() + 0.4f && this.GetCurrentVelocity().LengthSquared < 0.25f) && otherAgent.GetCurrentActionType(0) != Agent.ActionCodeType.Rear;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00012138 File Offset: 0x00010338
		public bool CanBeAssignedForScriptedMovement()
		{
			return this.IsActive() && this.IsAIControlled && !this.IsDetachedFromFormation && !this.IsRunningAway && (this.GetScriptedFlags() & Agent.AIScriptedFrameFlags.GoToPosition) == Agent.AIScriptedFrameFlags.None && !this.InteractingWithAnyGameObject();
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001216F File Offset: 0x0001036F
		public bool CanReachAndUseObject(UsableMissionObject gameObject, float distanceSq)
		{
			return this.CanReachObject(gameObject, distanceSq) && this.CanUseObject(gameObject);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00012184 File Offset: 0x00010384
		public bool CanReachObject(UsableMissionObject gameObject, float distanceSq)
		{
			if (this.IsItemUseDisabled || this.IsUsingGameObject)
			{
				return false;
			}
			float interactionDistanceToUsable = this.GetInteractionDistanceToUsable(gameObject);
			return distanceSq <= interactionDistanceToUsable * interactionDistanceToUsable && MathF.Abs(gameObject.InteractionEntity.GlobalPosition.z - this.Position.z) <= interactionDistanceToUsable * 2f;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000121E0 File Offset: 0x000103E0
		public bool CanUseObject(UsableMissionObject gameObject)
		{
			return !gameObject.IsDisabledForAgent(this) && gameObject.IsUsableByAgent(this);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000121F4 File Offset: 0x000103F4
		public bool CanMoveDirectlyToPosition(in Vec2 position)
		{
			return MBAPI.IMBAgent.CanMoveDirectlyToPosition(this.GetPtr(), position);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00012208 File Offset: 0x00010408
		public bool CanInteractableWeaponBePickedUp(SpawnedItemEntity spawnedItem)
		{
			EquipmentIndex equipmentIndex;
			return (!spawnedItem.IsBanner() || MissionGameModels.Current.BattleBannerBearersModel.IsInteractableFormationBanner(spawnedItem, this)) && (this.GetWeaponToReplaceOnQuickAction(spawnedItem, out equipmentIndex) != null || equipmentIndex == EquipmentIndex.None);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00012243 File Offset: 0x00010443
		public bool CanQuickPickUp(SpawnedItemEntity spawnedItem)
		{
			return (!spawnedItem.IsBanner() || MissionGameModels.Current.BattleBannerBearersModel.IsInteractableFormationBanner(spawnedItem, this)) && MissionEquipment.SelectWeaponPickUpSlot(this, spawnedItem.WeaponCopy, spawnedItem.IsStuckMissile()) != EquipmentIndex.None;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001227C File Offset: 0x0001047C
		public unsafe bool CanTeleport()
		{
			return this.Mission.IsTeleportingAgents && (this.Formation == null || this.Mission.Mode != MissionMode.Deployment || this.Formation.GetReadonlyMovementOrderReference()->OrderEnum == MovementOrder.MovementOrderEnum.Move);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000122C8 File Offset: 0x000104C8
		public bool IsActive()
		{
			return this.State == AgentState.Active;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000122D3 File Offset: 0x000104D3
		public bool IsRetreating()
		{
			return MBAPI.IMBAgent.IsRetreating(this.GetPtr());
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000122E5 File Offset: 0x000104E5
		public bool IsFadingOut()
		{
			return MBAPI.IMBAgent.IsFadingOut(this.GetPtr());
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000122F7 File Offset: 0x000104F7
		public void SetAgentDrivenPropertyValueFromConsole(DrivenProperty type, float val)
		{
			this.AgentDrivenProperties.SetStat(type, val);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00012306 File Offset: 0x00010506
		public bool IsOnLand()
		{
			return MBAPI.IMBAgent.IsOnLand(this.GetPtr());
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00012318 File Offset: 0x00010518
		public bool IsSliding()
		{
			return MBAPI.IMBAgent.IsSliding(this.GetPtr());
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001232C File Offset: 0x0001052C
		public bool IsSitting()
		{
			Agent.ActionCodeType currentActionType = this.GetCurrentActionType(0);
			return currentActionType == Agent.ActionCodeType.Sit || currentActionType == Agent.ActionCodeType.SitOnTheFloor || currentActionType == Agent.ActionCodeType.SitOnAThrone;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00012354 File Offset: 0x00010554
		public bool IsReleasingChainAttack()
		{
			bool result = false;
			if (Mission.Current.CurrentTime - this._lastQuickReadyDetectedTime < 0.75f && this.GetCurrentActionStage(1) == Agent.ActionStage.AttackRelease)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00012388 File Offset: 0x00010588
		public bool IsCameraAttachable()
		{
			return !this._isDeleted && (!this._isRemoved || this._removalTime + 2.1f > this.Mission.CurrentTime) && this.IsHuman && this.AgentVisuals != null && this.AgentVisuals.IsValid() && (GameNetwork.IsSessionActive || this._agentControllerType > Agent.ControllerType.None);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000123F5 File Offset: 0x000105F5
		public bool IsSynchedPrefabComponentVisible(int componentIndex)
		{
			return this._synchedBodyComponents[componentIndex].GetVisible();
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00012408 File Offset: 0x00010608
		public bool IsEnemyOf(Agent otherAgent)
		{
			return MBAPI.IMBAgent.IsEnemy(this.GetPtr(), otherAgent.GetPtr());
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00012420 File Offset: 0x00010620
		public bool IsFriendOf(Agent otherAgent)
		{
			return MBAPI.IMBAgent.IsFriend(this.GetPtr(), otherAgent.GetPtr());
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00012438 File Offset: 0x00010638
		public void OnFocusGain(Agent userAgent)
		{
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001243A File Offset: 0x0001063A
		public void OnFocusLose(Agent userAgent)
		{
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001243C File Offset: 0x0001063C
		public void OnItemRemovedFromScene()
		{
			this.StopUsingGameObjectMT(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00012446 File Offset: 0x00010646
		public void OnUse(Agent userAgent)
		{
			this.Mission.OnAgentInteraction(userAgent, this);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00012455 File Offset: 0x00010655
		public void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex)
		{
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00012458 File Offset: 0x00010658
		public void OnWeaponDrop(EquipmentIndex equipmentSlot)
		{
			MissionWeapon droppedWeapon = this.Equipment[equipmentSlot];
			this.Equipment[equipmentSlot] = MissionWeapon.Invalid;
			this.WeaponEquipped(equipmentSlot, WeaponData.InvalidWeaponData, null, WeaponData.InvalidWeaponData, null, null, false, false);
			foreach (AgentComponent agentComponent in this._components)
			{
				agentComponent.OnWeaponDrop(droppedWeapon);
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000124E0 File Offset: 0x000106E0
		public void OnItemPickup(SpawnedItemEntity spawnedItemEntity, EquipmentIndex weaponPickUpSlotIndex, out bool removeWeapon)
		{
			removeWeapon = true;
			bool flag = true;
			MissionWeapon weaponCopy = spawnedItemEntity.WeaponCopy;
			if (weaponPickUpSlotIndex == EquipmentIndex.None)
			{
				weaponPickUpSlotIndex = MissionEquipment.SelectWeaponPickUpSlot(this, weaponCopy, spawnedItemEntity.IsStuckMissile());
			}
			bool flag2 = false;
			if (weaponPickUpSlotIndex == EquipmentIndex.ExtraWeaponSlot)
			{
				if (!GameNetwork.IsClientOrReplay)
				{
					flag2 = true;
					if (!this.Equipment[weaponPickUpSlotIndex].IsEmpty)
					{
						this.DropItem(weaponPickUpSlotIndex, this.Equipment[weaponPickUpSlotIndex].Item.PrimaryWeapon.WeaponClass);
					}
				}
			}
			else if (weaponPickUpSlotIndex != EquipmentIndex.None)
			{
				int num = 0;
				if ((spawnedItemEntity.IsStuckMissile() || spawnedItemEntity.WeaponCopy.IsAnyConsumable()) && !this.Equipment[weaponPickUpSlotIndex].IsEmpty && this.Equipment[weaponPickUpSlotIndex].IsSameType(weaponCopy) && this.Equipment[weaponPickUpSlotIndex].IsAnyConsumable())
				{
					num = (int)(this.Equipment[weaponPickUpSlotIndex].ModifiedMaxAmount - this.Equipment[weaponPickUpSlotIndex].Amount);
				}
				if (num > 0)
				{
					short num2 = (short)MathF.Min(num, (int)weaponCopy.Amount);
					if (num2 != weaponCopy.Amount)
					{
						removeWeapon = false;
						if (!GameNetwork.IsClientOrReplay)
						{
							spawnedItemEntity.ConsumeWeaponAmount(num2);
							if (GameNetwork.IsServer)
							{
								GameNetwork.BeginBroadcastModuleEvent();
								GameNetwork.WriteMessage(new ConsumeWeaponAmount(spawnedItemEntity.Id, num2));
								GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
							}
						}
					}
					if (!GameNetwork.IsClientOrReplay)
					{
						this.SetWeaponAmountInSlot(weaponPickUpSlotIndex, this.Equipment[weaponPickUpSlotIndex].Amount + num2, true);
						if (this.GetWieldedItemIndex(Agent.HandIndex.MainHand) == EquipmentIndex.None && (weaponCopy.Item.PrimaryWeapon.IsRangedWeapon || weaponCopy.Item.PrimaryWeapon.IsMeleeWeapon))
						{
							flag2 = true;
						}
					}
				}
				else if (!GameNetwork.IsClientOrReplay)
				{
					flag2 = true;
					if (!this.Equipment[weaponPickUpSlotIndex].IsEmpty)
					{
						this.DropItem(weaponPickUpSlotIndex, weaponCopy.Item.PrimaryWeapon.WeaponClass);
					}
				}
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				flag = MissionEquipment.DoesWeaponFitToSlot(weaponPickUpSlotIndex, weaponCopy);
				if (flag)
				{
					this.EquipWeaponFromSpawnedItemEntity(weaponPickUpSlotIndex, spawnedItemEntity, removeWeapon);
					if (flag2)
					{
						EquipmentIndex slotIndex = weaponPickUpSlotIndex;
						if (weaponCopy.Item.PrimaryWeapon.AmmoClass == weaponCopy.Item.PrimaryWeapon.WeaponClass)
						{
							for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < weaponPickUpSlotIndex; equipmentIndex++)
							{
								if (!this.Equipment[equipmentIndex].IsEmpty && weaponCopy.IsEqualTo(this.Equipment[equipmentIndex]))
								{
									slotIndex = equipmentIndex;
									break;
								}
							}
						}
						this.TryToWieldWeaponInSlot(slotIndex, Agent.WeaponWieldActionType.InstantAfterPickUp, false);
					}
					for (int i = 0; i < this._components.Count; i++)
					{
						this._components[i].OnItemPickup(spawnedItemEntity);
					}
					if (this.Controller == Agent.ControllerType.AI)
					{
						this.HumanAIComponent.ItemPickupDone(spawnedItemEntity);
					}
				}
			}
			if (flag)
			{
				this.Mission.TriggerOnItemPickUpEvent(this, spawnedItemEntity);
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000127CC File Offset: 0x000109CC
		public bool CheckTracked(BasicCharacterObject basicCharacter)
		{
			return this.Character == basicCharacter;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000127D7 File Offset: 0x000109D7
		public bool CheckPathToAITargetAgentPassesThroughNavigationFaceIdFromDirection(int navigationFaceId, Vec3 direction, float overridenCostForFaceId)
		{
			return MBAPI.IMBAgent.CheckPathToAITargetAgentPassesThroughNavigationFaceIdFromDirection(this.GetPtr(), navigationFaceId, ref direction, overridenCostForFaceId);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000127F0 File Offset: 0x000109F0
		public void CheckEquipmentForCapeClothSimulationStateChange()
		{
			if (this._capeClothSimulator != null)
			{
				bool flag = false;
				EquipmentIndex wieldedItemIndex = this.GetWieldedItemIndex(Agent.HandIndex.OffHand);
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
				{
					MissionWeapon missionWeapon = this.Equipment[equipmentIndex];
					if (!missionWeapon.IsEmpty && missionWeapon.IsShield() && equipmentIndex != wieldedItemIndex)
					{
						flag = true;
						break;
					}
				}
				this._capeClothSimulator.SetMaxDistanceMultiplier(flag ? 0f : 1f);
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00012864 File Offset: 0x00010A64
		public void CheckToDropFlaggedItem()
		{
			if (this.GetAgentFlags().HasAnyFlag(AgentFlag.CanWieldWeapon))
			{
				for (int i = 0; i < 2; i++)
				{
					EquipmentIndex wieldedItemIndex = this.GetWieldedItemIndex((Agent.HandIndex)i);
					if (wieldedItemIndex != EquipmentIndex.None && this.Equipment[wieldedItemIndex].Item.ItemFlags.HasAnyFlag(ItemFlags.DropOnAnyAction))
					{
						this.DropItem(wieldedItemIndex, WeaponClass.Undefined);
					}
				}
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000128C8 File Offset: 0x00010AC8
		public bool CheckSkillForMounting(Agent mountAgent)
		{
			int effectiveSkill = MissionGameModels.Current.AgentStatCalculateModel.GetEffectiveSkill(this, DefaultSkills.Riding);
			return (this.GetAgentFlags() & AgentFlag.CanRide) > AgentFlag.None && (float)effectiveSkill >= mountAgent.GetAgentDrivenPropertyValue(DrivenProperty.MountDifficulty);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001290B File Offset: 0x00010B0B
		public void InitializeSpawnEquipment(Equipment spawnEquipment)
		{
			this.SpawnEquipment = spawnEquipment;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00012914 File Offset: 0x00010B14
		public void InitializeMissionEquipment(MissionEquipment missionEquipment, Banner banner)
		{
			this.Equipment = (missionEquipment ?? new MissionEquipment(this.SpawnEquipment, banner));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00012930 File Offset: 0x00010B30
		public void InitializeAgentProperties(Equipment spawnEquipment, AgentBuildData agentBuildData)
		{
			this._propertyModifiers = default(Agent.AgentPropertiesModifiers);
			this.AgentDrivenProperties = new AgentDrivenProperties();
			float[] values = this.AgentDrivenProperties.InitializeDrivenProperties(this, spawnEquipment, agentBuildData);
			this.UpdateDrivenProperties(values);
			if (this.IsMount && this.RiderAgent == null)
			{
				Mission.Current.AddMountWithoutRider(this);
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00012985 File Offset: 0x00010B85
		public void UpdateFormationOrders()
		{
			if (this.Formation != null && !this.IsRetreating())
			{
				this.EnforceShieldUsage(ArrangementOrder.GetShieldDirectionOfUnit(this.Formation, this, this.Formation.ArrangementOrder.OrderEnum));
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000129B9 File Offset: 0x00010BB9
		public void UpdateWeapons()
		{
			MBAPI.IMBAgent.UpdateWeapons(this.GetPtr());
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000129CC File Offset: 0x00010BCC
		public void UpdateAgentProperties()
		{
			if (this.AgentDrivenProperties != null)
			{
				float[] values = this.AgentDrivenProperties.UpdateDrivenProperties(this);
				this.UpdateDrivenProperties(values);
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000129F5 File Offset: 0x00010BF5
		public void UpdateCustomDrivenProperties()
		{
			if (this.AgentDrivenProperties != null)
			{
				this.UpdateDrivenProperties(this.AgentDrivenProperties.Values);
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00012A10 File Offset: 0x00010C10
		public void UpdateBodyProperties(BodyProperties bodyProperties)
		{
			this.BodyPropertiesValue = bodyProperties;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00012A19 File Offset: 0x00010C19
		public void UpdateSyncHealthToAllClients(bool value)
		{
			this.SyncHealthToAllClients = value;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00012A24 File Offset: 0x00010C24
		public void UpdateSpawnEquipmentAndRefreshVisuals(Equipment newSpawnEquipment)
		{
			this.SpawnEquipment = newSpawnEquipment;
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SynchronizeAgentSpawnEquipment(this.Index, this.SpawnEquipment));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			this.AgentVisuals.ClearVisualComponents(false);
			this.Mission.OnEquipItemsFromSpawnEquipment(this, Agent.CreationType.FromCharacterObj);
			this.AgentVisuals.ClearAllWeaponMeshes();
			MissionEquipment equipment = this.Equipment;
			Equipment spawnEquipment = this.SpawnEquipment;
			IAgentOriginBase origin = this.Origin;
			equipment.FillFrom(spawnEquipment, (origin != null) ? origin.Banner : null);
			this.CheckEquipmentForCapeClothSimulationStateChange();
			this.EquipItemsFromSpawnEquipment(true);
			this.UpdateAgentProperties();
			if (!Mission.Current.DoesMissionRequireCivilianEquipment && !GameNetwork.IsClientOrReplay)
			{
				this.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, TaleWorlds.Core.Equipment.InitialWeaponEquipPreference.Any);
			}
			this.PreloadForRendering();
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00012AE0 File Offset: 0x00010CE0
		public void UpdateCachedAndFormationValues(bool updateOnlyMovement, bool arrangementChangeAllowed)
		{
			if (!this.IsActive())
			{
				return;
			}
			if (!updateOnlyMovement)
			{
				Agent mountAgent = this.MountAgent;
				this.WalkSpeedCached = ((mountAgent != null) ? mountAgent.WalkingSpeedLimitOfMountable : this.Monster.WalkingSpeedLimit);
				this.RunSpeedCached = this.MaximumForwardUnlimitedSpeed;
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				if (!updateOnlyMovement && !this.IsDetachedFromFormation)
				{
					Formation formation = this.Formation;
					if (formation != null)
					{
						formation.Arrangement.OnTickOccasionallyOfUnit(this, arrangementChangeAllowed);
					}
				}
				if (this.IsAIControlled)
				{
					this.HumanAIComponent.UpdateFormationMovement();
				}
				if (!updateOnlyMovement)
				{
					Formation formation2 = this.Formation;
					if (formation2 != null)
					{
						formation2.Team.DetachmentManager.TickAgent(this);
					}
				}
				if (!updateOnlyMovement && this.IsAIControlled)
				{
					this.UpdateFormationOrders();
					if (this.Formation != null)
					{
						int fileIndex;
						int rankIndex;
						int fileCount;
						int rankCount;
						this.GetFormationFileAndRankInfo(out fileIndex, out rankIndex, out fileCount, out rankCount);
						Vec2 wallDirectionOfRelativeFormationLocation = this.GetWallDirectionOfRelativeFormationLocation();
						MBAPI.IMBAgent.SetFormationInfo(this.GetPtr(), fileIndex, rankIndex, fileCount, rankCount, wallDirectionOfRelativeFormationLocation, this.Formation.UnitSpacing);
					}
				}
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00012BD6 File Offset: 0x00010DD6
		public void UpdateLastRangedAttackTimeDueToAnAttack(float newTime)
		{
			this.LastRangedAttackTime = newTime;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00012BDF File Offset: 0x00010DDF
		public void InvalidateTargetAgent()
		{
			MBAPI.IMBAgent.InvalidateTargetAgent(this.GetPtr());
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00012BF1 File Offset: 0x00010DF1
		public void InvalidateAIWeaponSelections()
		{
			MBAPI.IMBAgent.InvalidateAIWeaponSelections(this.GetPtr());
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00012C03 File Offset: 0x00010E03
		public void ResetLookAgent()
		{
			this.SetLookAgent(null);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00012C0C File Offset: 0x00010E0C
		public void ResetGuard()
		{
			MBAPI.IMBAgent.ResetGuard(this.GetPtr());
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00012C1E File Offset: 0x00010E1E
		public void ResetAgentProperties()
		{
			this.AgentDrivenProperties = null;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00012C27 File Offset: 0x00010E27
		public void ResetAiWaitBeforeShootFactor()
		{
			this._propertyModifiers.resetAiWaitBeforeShootFactor = true;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00012C35 File Offset: 0x00010E35
		public void ClearTargetFrame()
		{
			this._checkIfTargetFrameIsChanged = false;
			if (this.MovementLockedState != AgentMovementLockedState.None)
			{
				this.ClearTargetFrameAux();
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new ClearAgentTargetFrame(this.Index));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00012C70 File Offset: 0x00010E70
		public void ClearEquipment()
		{
			MBAPI.IMBAgent.ClearEquipment(this.GetPtr());
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00012C82 File Offset: 0x00010E82
		public void ClearHandInverseKinematics()
		{
			MBAPI.IMBAgent.ClearHandInverseKinematics(this.GetPtr());
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00012C94 File Offset: 0x00010E94
		public void ClearAttachedWeapons()
		{
			List<ValueTuple<MissionWeapon, MatrixFrame, sbyte>> attachedWeapons = this._attachedWeapons;
			if (attachedWeapons == null)
			{
				return;
			}
			attachedWeapons.Clear();
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00012CA8 File Offset: 0x00010EA8
		public void SetDetachableFromFormation(bool value)
		{
			bool isDetachableFromFormation = this._isDetachableFromFormation;
			if (isDetachableFromFormation != value)
			{
				if (isDetachableFromFormation)
				{
					if (this.IsDetachedFromFormation)
					{
						this._detachment.RemoveAgent(this);
						Formation formation = this._formation;
						if (formation != null)
						{
							formation.AttachUnit(this);
						}
					}
					Formation formation2 = this._formation;
					if (formation2 != null)
					{
						Team team = formation2.Team;
						if (team != null)
						{
							team.DetachmentManager.RemoveScoresOfAgentFromDetachments(this);
						}
					}
				}
				this._isDetachableFromFormation = value;
				if (!this.IsPlayerControlled)
				{
					if (isDetachableFromFormation)
					{
						Formation formation3 = this._formation;
						if (formation3 == null)
						{
							return;
						}
						formation3.OnUndetachableNonPlayerUnitAdded(this);
						return;
					}
					else
					{
						Formation formation4 = this._formation;
						if (formation4 == null)
						{
							return;
						}
						formation4.OnUndetachableNonPlayerUnitRemoved(this);
					}
				}
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00012D43 File Offset: 0x00010F43
		public void EnforceShieldUsage(Agent.UsageDirection shieldDirection)
		{
			MBAPI.IMBAgent.EnforceShieldUsage(this.GetPtr(), shieldDirection);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00012D56 File Offset: 0x00010F56
		public bool ObjectHasVacantPosition(UsableMissionObject gameObject)
		{
			return !gameObject.HasUser || gameObject.HasAIUser;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00012D68 File Offset: 0x00010F68
		public bool InteractingWithAnyGameObject()
		{
			return this.IsUsingGameObject || (this.IsAIControlled && this.AIInterestedInAnyGameObject());
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00012D84 File Offset: 0x00010F84
		private void StopUsingGameObjectAux(bool isSuccessful, Agent.StopUsingGameObjectFlags flags)
		{
			UsableMachine usableMachine = (this.Controller != Agent.ControllerType.AI || this.Formation == null) ? null : (this.Formation.GetDetachmentOrDefault(this) as UsableMachine);
			if (usableMachine == null)
			{
				flags &= ~Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject;
			}
			UsableMissionObject currentlyUsedGameObject = this.CurrentlyUsedGameObject;
			UsableMissionObject movingToOrDefendingObject = null;
			if (!this.IsUsingGameObject && this.IsAIControlled)
			{
				if (this.AIMoveToGameObjectIsEnabled())
				{
					movingToOrDefendingObject = this.HumanAIComponent.GetCurrentlyMovingGameObject();
				}
				else
				{
					movingToOrDefendingObject = this.HumanAIComponent.GetCurrentlyDefendingGameObject();
				}
			}
			if (this.IsUsingGameObject)
			{
				bool flag = this.CurrentlyUsedGameObject.LockUserFrames || this.CurrentlyUsedGameObject.LockUserPositions;
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new StopUsingObject(this.Index, isSuccessful));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				this.CurrentlyUsedGameObject.OnUseStopped(this, isSuccessful, this._usedObjectPreferenceIndex);
				this.CurrentlyUsedGameObject = null;
				if (this.IsAIControlled)
				{
					this.AIUseGameObjectDisable();
				}
				this._usedObjectPreferenceIndex = -1;
				if (flag)
				{
					this.ClearTargetFrame();
				}
			}
			else if (this.IsAIControlled)
			{
				if (this.AIDefendGameObjectIsEnabled())
				{
					this.AIDefendGameObjectDisable();
				}
				else
				{
					this.AIMoveToGameObjectDisable();
				}
			}
			if (this.State == AgentState.Active)
			{
				if (this.IsAIControlled)
				{
					this.DisableScriptedMovement();
					if (usableMachine != null)
					{
						foreach (StandingPoint standingPoint in usableMachine.StandingPoints)
						{
							standingPoint.FavoredUser = this;
						}
					}
				}
				this.AfterStoppedUsingMissionObject(usableMachine, currentlyUsedGameObject, movingToOrDefendingObject, isSuccessful, flags);
			}
			this.Mission.OnObjectStoppedBeingUsed(this, this.CurrentlyUsedGameObject);
			this._components.ForEach(delegate(AgentComponent ac)
			{
				ac.OnStopUsingGameObject();
			});
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00012F44 File Offset: 0x00011144
		public void StopUsingGameObjectMT(bool isSuccessful = true, Agent.StopUsingGameObjectFlags flags = Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject)
		{
			object stopUsingGameObjectLock = Agent._stopUsingGameObjectLock;
			lock (stopUsingGameObjectLock)
			{
				this.StopUsingGameObjectAux(isSuccessful, flags);
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00012F88 File Offset: 0x00011188
		public void StopUsingGameObject(bool isSuccessful = true, Agent.StopUsingGameObjectFlags flags = Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject)
		{
			using (new TWParallel.RecursiveSingleThreadTestBlock(TWParallel.RecursiveSingleThreadTestData.GlobalData))
			{
				this.StopUsingGameObjectAux(isSuccessful, flags);
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00012FCC File Offset: 0x000111CC
		public void HandleStopUsingAction()
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestStopUsingObject());
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			this.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00012FF2 File Offset: 0x000111F2
		public void HandleStartUsingAction(UsableMissionObject targetObject, int preferenceIndex)
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestUseObject(targetObject.Id, preferenceIndex));
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			this.UseGameObject(targetObject, preferenceIndex);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00013020 File Offset: 0x00011220
		public AgentController AddController(Type type)
		{
			AgentController agentController = null;
			if (type.IsSubclassOf(typeof(AgentController)))
			{
				agentController = (Activator.CreateInstance(type) as AgentController);
			}
			if (agentController != null)
			{
				agentController.Owner = this;
				agentController.Mission = this.Mission;
				this._agentControllers.Add(agentController);
				agentController.OnInitialize();
			}
			return agentController;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00013078 File Offset: 0x00011278
		public AgentController RemoveController(Type type)
		{
			for (int i = 0; i < this._agentControllers.Count; i++)
			{
				if (type.IsInstanceOfType(this._agentControllers[i]))
				{
					AgentController result = this._agentControllers[i];
					this._agentControllers.RemoveAt(i);
					return result;
				}
			}
			return null;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000130CC File Offset: 0x000112CC
		public bool CanThrustAttackStickToBone(BoneBodyPartType bodyPart)
		{
			if (this.IsHuman)
			{
				BoneBodyPartType[] array = new BoneBodyPartType[]
				{
					BoneBodyPartType.Abdomen,
					BoneBodyPartType.Legs,
					BoneBodyPartType.Chest,
					BoneBodyPartType.Neck,
					BoneBodyPartType.ShoulderLeft,
					BoneBodyPartType.ShoulderRight,
					BoneBodyPartType.ArmLeft,
					BoneBodyPartType.ArmRight
				};
				for (int i = 0; i < array.Length; i++)
				{
					if (bodyPart == array[i])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001310A File Offset: 0x0001130A
		public void StartSwitchingWeaponUsageIndexAsClient(EquipmentIndex equipmentIndex, int usageIndex, Agent.UsageDirection currentMovementFlagUsageDirection)
		{
			MBAPI.IMBAgent.StartSwitchingWeaponUsageIndexAsClient(this.GetPtr(), (int)equipmentIndex, usageIndex, currentMovementFlagUsageDirection);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001311F File Offset: 0x0001131F
		public void TryToWieldWeaponInSlot(EquipmentIndex slotIndex, Agent.WeaponWieldActionType type, bool isWieldedOnSpawn)
		{
			MBAPI.IMBAgent.TryToWieldWeaponInSlot(this.GetPtr(), (int)slotIndex, (int)type, isWieldedOnSpawn);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00013134 File Offset: 0x00011334
		public void PrepareWeaponForDropInEquipmentSlot(EquipmentIndex slotIndex, bool dropWithHolster)
		{
			MBAPI.IMBAgent.PrepareWeaponForDropInEquipmentSlot(this.GetPtr(), (int)slotIndex, dropWithHolster);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00013148 File Offset: 0x00011348
		public void AddHitter(MissionPeer peer, float damage, bool isFriendlyHit)
		{
			Agent.Hitter hitter = this._hitterList.Find((Agent.Hitter h) => h.HitterPeer == peer && h.IsFriendlyHit == isFriendlyHit);
			if (hitter == null)
			{
				hitter = new Agent.Hitter(peer, damage, isFriendlyHit);
				this._hitterList.Add(hitter);
				return;
			}
			hitter.IncreaseDamage(damage);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000131AB File Offset: 0x000113AB
		public void TryToSheathWeaponInHand(Agent.HandIndex handIndex, Agent.WeaponWieldActionType type)
		{
			MBAPI.IMBAgent.TryToSheathWeaponInHand(this.GetPtr(), (int)handIndex, (int)type);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000131C0 File Offset: 0x000113C0
		public void RemoveHitter(MissionPeer peer, bool isFriendlyHit)
		{
			Agent.Hitter hitter = this._hitterList.Find((Agent.Hitter h) => h.HitterPeer == peer && h.IsFriendlyHit == isFriendlyHit);
			if (hitter != null)
			{
				this._hitterList.Remove(hitter);
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00013209 File Offset: 0x00011409
		public void Retreat(WorldPosition retreatPos)
		{
			MBAPI.IMBAgent.SetRetreatMode(this.GetPtr(), retreatPos, true);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001321D File Offset: 0x0001141D
		public void StopRetreating()
		{
			MBAPI.IMBAgent.SetRetreatMode(this.GetPtr(), WorldPosition.Invalid, false);
			this.IsRunningAway = false;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001323C File Offset: 0x0001143C
		public void UseGameObject(UsableMissionObject usedObject, int preferenceIndex = -1)
		{
			if (usedObject.LockUserFrames)
			{
				WorldFrame userFrameForAgent = usedObject.GetUserFrameForAgent(this);
				this.SetTargetPositionAndDirection(userFrameForAgent.Origin.AsVec2, userFrameForAgent.Rotation.f);
				this.SetScriptedFlags(this.GetScriptedFlags() | Agent.AIScriptedFrameFlags.NoAttack);
			}
			else if (usedObject.LockUserPositions)
			{
				this.SetTargetPosition(usedObject.GetUserFrameForAgent(this).Origin.AsVec2);
				this.SetScriptedFlags(this.GetScriptedFlags() | Agent.AIScriptedFrameFlags.NoAttack);
			}
			if (this.IsActive() && this.IsAIControlled && this.AIMoveToGameObjectIsEnabled())
			{
				this.AIMoveToGameObjectDisable();
				Formation formation = this.Formation;
				if (formation != null)
				{
					formation.Team.DetachmentManager.RemoveScoresOfAgentFromDetachments(this);
				}
			}
			this.CurrentlyUsedGameObject = usedObject;
			this._usedObjectPreferenceIndex = preferenceIndex;
			if (this.IsAIControlled)
			{
				this.AIUseGameObjectEnable();
			}
			this._equipmentOnMainHandBeforeUsingObject = this.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			this._equipmentOnOffHandBeforeUsingObject = this.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			usedObject.OnUse(this);
			this.Mission.OnObjectUsed(this, usedObject);
			if (usedObject.IsInstantUse && !GameNetwork.IsClientOrReplay && this.IsActive() && this.InteractingWithAnyGameObject())
			{
				this.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00013361 File Offset: 0x00011561
		public void StartFadingOut()
		{
			MBAPI.IMBAgent.StartFadingOut(this.GetPtr());
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00013373 File Offset: 0x00011573
		public void SetRenderCheckEnabled(bool value)
		{
			MBAPI.IMBAgent.SetRenderCheckEnabled(this.GetPtr(), value);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00013386 File Offset: 0x00011586
		public bool GetRenderCheckEnabled()
		{
			return MBAPI.IMBAgent.GetRenderCheckEnabled(this.GetPtr());
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00013398 File Offset: 0x00011598
		public Vec3 ComputeAnimationDisplacement(float dt)
		{
			return MBAPI.IMBAgent.ComputeAnimationDisplacement(this.GetPtr(), dt);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000133AB File Offset: 0x000115AB
		public void TickActionChannels(float dt)
		{
			MBAPI.IMBAgent.TickActionChannels(this.GetPtr(), dt);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000133C0 File Offset: 0x000115C0
		public void LockAgentReplicationTableDataWithCurrentReliableSequenceNo(NetworkCommunicator peer)
		{
			MBDebug.Print(string.Concat(new object[]
			{
				"peer: ",
				peer.UserName,
				" index: ",
				this.Index,
				" name: ",
				this.Name
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			MBAPI.IMBAgent.LockAgentReplicationTableDataWithCurrentReliableSequenceNo(this.GetPtr(), peer.Index);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00013438 File Offset: 0x00011638
		public void TeleportToPosition(Vec3 position)
		{
			if (this.MountAgent != null)
			{
				MBAPI.IMBAgent.SetPosition(this.MountAgent.GetPtr(), ref position);
			}
			MBAPI.IMBAgent.SetPosition(this.GetPtr(), ref position);
			if (this.RiderAgent != null)
			{
				MBAPI.IMBAgent.SetPosition(this.RiderAgent.GetPtr(), ref position);
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00013495 File Offset: 0x00011695
		public void FadeOut(bool hideInstantly, bool hideMount)
		{
			MBAPI.IMBAgent.FadeOut(this.GetPtr(), hideInstantly);
			if (hideMount && this.HasMount)
			{
				this.MountAgent.FadeOut(hideMount, false);
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000134C0 File Offset: 0x000116C0
		public void FadeIn()
		{
			MBAPI.IMBAgent.FadeIn(this.GetPtr());
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000134D2 File Offset: 0x000116D2
		public void DisableScriptedMovement()
		{
			MBAPI.IMBAgent.DisableScriptedMovement(this.GetPtr());
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000134E4 File Offset: 0x000116E4
		public void DisableScriptedCombatMovement()
		{
			MBAPI.IMBAgent.DisableScriptedCombatMovement(this.GetPtr());
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000134F6 File Offset: 0x000116F6
		public void ForceAiBehaviorSelection()
		{
			MBAPI.IMBAgent.ForceAiBehaviorSelection(this.GetPtr());
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00013508 File Offset: 0x00011708
		public bool HasPathThroughNavigationFaceIdFromDirectionMT(int navigationFaceId, Vec2 direction)
		{
			object pathCheckObjectLock = Agent._pathCheckObjectLock;
			bool result;
			lock (pathCheckObjectLock)
			{
				result = MBAPI.IMBAgent.HasPathThroughNavigationFaceIdFromDirection(this.GetPtr(), navigationFaceId, ref direction);
			}
			return result;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00013558 File Offset: 0x00011758
		public bool HasPathThroughNavigationFaceIdFromDirection(int navigationFaceId, Vec2 direction)
		{
			return MBAPI.IMBAgent.HasPathThroughNavigationFaceIdFromDirection(this.GetPtr(), navigationFaceId, ref direction);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001356D File Offset: 0x0001176D
		public void DisableLookToPointOfInterest()
		{
			MBAPI.IMBAgent.DisableLookToPointOfInterest(this.GetPtr());
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001357F File Offset: 0x0001177F
		public CompositeComponent AddPrefabComponentToBone(string prefabName, sbyte boneIndex)
		{
			return MBAPI.IMBAgent.AddPrefabToAgentBone(this.GetPtr(), prefabName, boneIndex);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00013593 File Offset: 0x00011793
		public void MakeVoice(SkinVoiceManager.SkinVoiceType voiceType, SkinVoiceManager.CombatVoiceNetworkPredictionType predictionType)
		{
			MBAPI.IMBAgent.MakeVoice(this.GetPtr(), voiceType.Index, (int)predictionType);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000135AD File Offset: 0x000117AD
		public void WieldNextWeapon(Agent.HandIndex weaponIndex, Agent.WeaponWieldActionType wieldActionType = Agent.WeaponWieldActionType.WithAnimation)
		{
			MBAPI.IMBAgent.WieldNextWeapon(this.GetPtr(), (int)weaponIndex, (int)wieldActionType);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000135C1 File Offset: 0x000117C1
		public Agent.MovementControlFlag AttackDirectionToMovementFlag(Agent.UsageDirection direction)
		{
			return MBAPI.IMBAgent.AttackDirectionToMovementFlag(this.GetPtr(), direction);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000135D4 File Offset: 0x000117D4
		public Agent.MovementControlFlag DefendDirectionToMovementFlag(Agent.UsageDirection direction)
		{
			return MBAPI.IMBAgent.DefendDirectionToMovementFlag(this.GetPtr(), direction);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000135E7 File Offset: 0x000117E7
		public bool KickClear()
		{
			return MBAPI.IMBAgent.KickClear(this.GetPtr());
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000135F9 File Offset: 0x000117F9
		public Agent.UsageDirection PlayerAttackDirection()
		{
			return MBAPI.IMBAgent.PlayerAttackDirection(this.GetPtr());
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001360C File Offset: 0x0001180C
		public ValueTuple<sbyte, sbyte> GetRandomPairOfRealBloodBurstBoneIndices()
		{
			sbyte item = -1;
			sbyte item2 = -1;
			if (this.Monster.BloodBurstBoneIndices.Length != 0)
			{
				int num = MBRandom.RandomInt(this.Monster.BloodBurstBoneIndices.Length / 2);
				item = this.Monster.BloodBurstBoneIndices[num * 2];
				item2 = this.Monster.BloodBurstBoneIndices[num * 2 + 1];
			}
			return new ValueTuple<sbyte, sbyte>(item, item2);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00013669 File Offset: 0x00011869
		public void CreateBloodBurstAtLimb(sbyte realBoneIndex, float scale)
		{
			MBAPI.IMBAgent.CreateBloodBurstAtLimb(this.GetPtr(), realBoneIndex, scale);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00013680 File Offset: 0x00011880
		public void AddComponent(AgentComponent agentComponent)
		{
			this._components.Add(agentComponent);
			CommonAIComponent commonAIComponent;
			if ((commonAIComponent = (agentComponent as CommonAIComponent)) != null)
			{
				this.CommonAIComponent = commonAIComponent;
				return;
			}
			HumanAIComponent humanAIComponent;
			if ((humanAIComponent = (agentComponent as HumanAIComponent)) != null)
			{
				this.HumanAIComponent = humanAIComponent;
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000136BC File Offset: 0x000118BC
		public bool RemoveComponent(AgentComponent agentComponent)
		{
			bool flag = this._components.Remove(agentComponent);
			if (flag)
			{
				agentComponent.OnComponentRemoved();
				if (this.CommonAIComponent == agentComponent)
				{
					this.CommonAIComponent = null;
					return flag;
				}
				if (this.HumanAIComponent == agentComponent)
				{
					this.HumanAIComponent = null;
				}
			}
			return flag;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000136F4 File Offset: 0x000118F4
		public void HandleTaunt(int tauntIndex, bool isDefaultTaunt)
		{
			if (tauntIndex < 0)
			{
				return;
			}
			if (isDefaultTaunt)
			{
				ActionIndexCache actionIndexCache = Agent.DefaultTauntActions[tauntIndex];
				this.SetActionChannel(1, actionIndexCache, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
				this.MakeVoice(SkinVoiceManager.VoiceType.Victory, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				return;
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				ActionIndexCache suitableTauntAction = CosmeticsManagerHelper.GetSuitableTauntAction(this, tauntIndex);
				if (suitableTauntAction.Index >= 0)
				{
					this.SetActionChannel(1, suitableTauntAction, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
				}
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00013790 File Offset: 0x00011990
		public void HandleBark(int indexOfBark)
		{
			if (indexOfBark < SkinVoiceManager.VoiceType.MpBarks.Length && !GameNetwork.IsClientOrReplay)
			{
				this.MakeVoice(SkinVoiceManager.VoiceType.MpBarks[indexOfBark], SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				if (GameNetwork.IsMultiplayer)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new BarkAgent(this.Index, indexOfBark));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, this.MissionPeer.GetNetworkPeer());
				}
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000137F0 File Offset: 0x000119F0
		public void HandleDropWeapon(bool isDefendPressed, EquipmentIndex forcedSlotIndexToDropWeaponFrom)
		{
			Agent.ActionCodeType currentActionType = this.GetCurrentActionType(1);
			if (this.State == AgentState.Active && currentActionType != Agent.ActionCodeType.ReleaseMelee && currentActionType != Agent.ActionCodeType.ReleaseRanged && currentActionType != Agent.ActionCodeType.ReleaseThrowing && currentActionType != Agent.ActionCodeType.WeaponBash)
			{
				EquipmentIndex equipmentIndex = forcedSlotIndexToDropWeaponFrom;
				if (equipmentIndex == EquipmentIndex.None)
				{
					EquipmentIndex wieldedItemIndex = this.GetWieldedItemIndex(Agent.HandIndex.MainHand);
					EquipmentIndex wieldedItemIndex2 = this.GetWieldedItemIndex(Agent.HandIndex.OffHand);
					if (wieldedItemIndex2 >= EquipmentIndex.WeaponItemBeginSlot && isDefendPressed)
					{
						equipmentIndex = wieldedItemIndex2;
					}
					else if (wieldedItemIndex >= EquipmentIndex.WeaponItemBeginSlot)
					{
						equipmentIndex = wieldedItemIndex;
					}
					else if (wieldedItemIndex2 >= EquipmentIndex.WeaponItemBeginSlot)
					{
						equipmentIndex = wieldedItemIndex2;
					}
					else
					{
						for (EquipmentIndex equipmentIndex2 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex2 < EquipmentIndex.ExtraWeaponSlot; equipmentIndex2++)
						{
							if (!this.Equipment[equipmentIndex2].IsEmpty && this.Equipment[equipmentIndex2].Item.PrimaryWeapon.IsConsumable)
							{
								if (this.Equipment[equipmentIndex2].Item.PrimaryWeapon.IsRangedWeapon)
								{
									if (this.Equipment[equipmentIndex2].Amount == 0)
									{
										equipmentIndex = equipmentIndex2;
										break;
									}
								}
								else
								{
									bool flag = false;
									for (EquipmentIndex equipmentIndex3 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex3 < EquipmentIndex.ExtraWeaponSlot; equipmentIndex3++)
									{
										if (!this.Equipment[equipmentIndex3].IsEmpty && this.Equipment[equipmentIndex3].HasAnyUsageWithAmmoClass(this.Equipment[equipmentIndex2].Item.PrimaryWeapon.WeaponClass) && this.Equipment[equipmentIndex2].Amount > 0)
										{
											flag = true;
											break;
										}
									}
									if (!flag)
									{
										equipmentIndex = equipmentIndex2;
										break;
									}
								}
							}
						}
					}
				}
				if (equipmentIndex != EquipmentIndex.None && !this.Equipment[equipmentIndex].IsEmpty)
				{
					this.DropItem(equipmentIndex, WeaponClass.Undefined);
					this.UpdateAgentProperties();
				}
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000139C0 File Offset: 0x00011BC0
		public void DropItem(EquipmentIndex itemIndex, WeaponClass pickedUpItemType = WeaponClass.Undefined)
		{
			if (this.Equipment[itemIndex].CurrentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.AffectsArea | WeaponFlags.Burning))
			{
				MatrixFrame boneEntitialFrameWithIndex = this.AgentVisuals.GetSkeleton().GetBoneEntitialFrameWithIndex(this.Monster.MainHandItemBoneIndex);
				MatrixFrame globalFrame = this.AgentVisuals.GetGlobalFrame();
				MatrixFrame matrixFrame = globalFrame.TransformToParent(boneEntitialFrameWithIndex);
				Vec3 vec = globalFrame.origin + globalFrame.rotation.f - matrixFrame.origin;
				vec.Normalize();
				Mat3 identity = Mat3.Identity;
				identity.f = vec;
				identity.Orthonormalize();
				Mission.Current.OnAgentShootMissile(this, itemIndex, matrixFrame.origin, vec, identity, false, false, -1);
				this.RemoveEquippedWeapon(itemIndex);
				return;
			}
			MBAPI.IMBAgent.DropItem(this.GetPtr(), (int)itemIndex, (int)pickedUpItemType);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00013A9C File Offset: 0x00011C9C
		public void EquipItemsFromSpawnEquipment(bool neededBatchedItems)
		{
			this.Mission.OnEquipItemsFromSpawnEquipmentBegin(this, this._creationType);
			switch (this._creationType)
			{
			case Agent.CreationType.FromRoster:
			case Agent.CreationType.FromCharacterObj:
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
				{
					WeaponData weaponData = WeaponData.InvalidWeaponData;
					WeaponStatsData[] weaponStatsData = null;
					WeaponData weaponData2 = WeaponData.InvalidWeaponData;
					WeaponStatsData[] ammoWeaponStatsData = null;
					if (!this.Equipment[equipmentIndex].IsEmpty)
					{
						weaponData = this.Equipment[equipmentIndex].GetWeaponData(neededBatchedItems);
						weaponStatsData = this.Equipment[equipmentIndex].GetWeaponStatsData();
						weaponData2 = this.Equipment[equipmentIndex].GetAmmoWeaponData(neededBatchedItems);
						ammoWeaponStatsData = this.Equipment[equipmentIndex].GetAmmoWeaponStatsData();
					}
					this.WeaponEquipped(equipmentIndex, weaponData, weaponStatsData, weaponData2, ammoWeaponStatsData, null, true, true);
					weaponData.DeinitializeManagedPointers();
					weaponData2.DeinitializeManagedPointers();
					for (int i = 0; i < this.Equipment[equipmentIndex].GetAttachedWeaponsCount(); i++)
					{
						MatrixFrame attachedWeaponFrame = this.Equipment[equipmentIndex].GetAttachedWeaponFrame(i);
						MissionWeapon attachedWeapon = this.Equipment[equipmentIndex].GetAttachedWeapon(i);
						this.AttachWeaponToWeaponAux(equipmentIndex, ref attachedWeapon, null, ref attachedWeaponFrame);
					}
				}
				this.AddSkinMeshes(!neededBatchedItems);
				break;
			}
			this.UpdateAgentProperties();
			this.Mission.OnEquipItemsFromSpawnEquipment(this, this._creationType);
			this.CheckEquipmentForCapeClothSimulationStateChange();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00013C1C File Offset: 0x00011E1C
		public void WieldInitialWeapons(Agent.WeaponWieldActionType wieldActionType = Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference initialWeaponEquipPreference = TaleWorlds.Core.Equipment.InitialWeaponEquipPreference.Any)
		{
			EquipmentIndex wieldedItemIndex = this.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			EquipmentIndex wieldedItemIndex2 = this.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			bool flag;
			this.SpawnEquipment.GetInitialWeaponIndicesToEquip(out wieldedItemIndex, out wieldedItemIndex2, out flag, initialWeaponEquipPreference);
			if (wieldedItemIndex2 != EquipmentIndex.None)
			{
				this.TryToWieldWeaponInSlot(wieldedItemIndex2, wieldActionType, true);
			}
			if (wieldedItemIndex != EquipmentIndex.None)
			{
				this.TryToWieldWeaponInSlot(wieldedItemIndex, wieldActionType, true);
				if (this.GetWieldedItemIndex(Agent.HandIndex.MainHand) == EquipmentIndex.None)
				{
					this.WieldNextWeapon(Agent.HandIndex.MainHand, wieldActionType);
				}
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00013C78 File Offset: 0x00011E78
		public void ChangeWeaponHitPoints(EquipmentIndex slotIndex, short hitPoints)
		{
			this.Equipment.SetHitPointsOfSlot(slotIndex, hitPoints, false);
			this.SetWeaponHitPointsInSlot(slotIndex, hitPoints);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetWeaponNetworkData(this.Index, slotIndex, hitPoints));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			foreach (AgentComponent agentComponent in this._components)
			{
				agentComponent.OnWeaponHPChanged(this.Equipment[slotIndex].Item, (int)hitPoints);
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00013D1C File Offset: 0x00011F1C
		public bool HasWeapon()
		{
			for (int i = 0; i < 5; i++)
			{
				WeaponComponentData currentUsageItem = this.Equipment[i].CurrentUsageItem;
				if (currentUsageItem != null && currentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.WeaponMask))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00013D5F File Offset: 0x00011F5F
		public void AttachWeaponToWeapon(EquipmentIndex slotIndex, MissionWeapon weapon, GameEntity weaponEntity, ref MatrixFrame attachLocalFrame)
		{
			this.Equipment.AttachWeaponToWeaponInSlot(slotIndex, ref weapon, ref attachLocalFrame);
			this.AttachWeaponToWeaponAux(slotIndex, ref weapon, weaponEntity, ref attachLocalFrame);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00013D7D File Offset: 0x00011F7D
		public void AttachWeaponToBone(MissionWeapon weapon, GameEntity weaponEntity, sbyte boneIndex, ref MatrixFrame attachLocalFrame)
		{
			if (this._attachedWeapons == null)
			{
				this._attachedWeapons = new List<ValueTuple<MissionWeapon, MatrixFrame, sbyte>>();
			}
			this._attachedWeapons.Add(new ValueTuple<MissionWeapon, MatrixFrame, sbyte>(weapon, attachLocalFrame, boneIndex));
			this.AttachWeaponToBoneAux(ref weapon, weaponEntity, boneIndex, ref attachLocalFrame);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00013DB8 File Offset: 0x00011FB8
		public void RestoreShieldHitPoints()
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				if (!this.Equipment[equipmentIndex].IsEmpty && this.Equipment[equipmentIndex].CurrentUsageItem.IsShield)
				{
					this.ChangeWeaponHitPoints(equipmentIndex, this.Equipment[equipmentIndex].ModifiedMaxHitPoints);
				}
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00013E20 File Offset: 0x00012020
		public void Die(Blow b, Agent.KillInfo overrideKillInfo = Agent.KillInfo.Invalid)
		{
			if (this.Formation != null)
			{
				this.Formation.Team.QuerySystem.RegisterDeath();
				if (b.IsMissile)
				{
					this.Formation.Team.QuerySystem.RegisterDeathByRanged();
				}
			}
			this.Health = 0f;
			if (overrideKillInfo != Agent.KillInfo.TeamSwitch && (b.OwnerId == -1 || b.OwnerId == this.Index) && this.IsHuman && this._lastHitInfo.CanOverrideBlow)
			{
				b.OwnerId = this._lastHitInfo.LastBlowOwnerId;
				b.AttackType = this._lastHitInfo.LastBlowAttackType;
			}
			MBAPI.IMBAgent.Die(this.GetPtr(), ref b, (sbyte)overrideKillInfo);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00013EDB File Offset: 0x000120DB
		public void MakeDead(bool isKilled, ActionIndexValueCache actionIndex)
		{
			MBAPI.IMBAgent.MakeDead(this.GetPtr(), isKilled, actionIndex.Index);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00013EF5 File Offset: 0x000120F5
		public void RegisterBlow(Blow blow, in AttackCollisionData collisionData)
		{
			this.HandleBlow(ref blow, collisionData);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00013F00 File Offset: 0x00012100
		public void CreateBlowFromBlowAsReflection(in Blow blow, in AttackCollisionData collisionData, out Blow outBlow, out AttackCollisionData outCollisionData)
		{
			outBlow = blow;
			outBlow.InflictedDamage = blow.SelfInflictedDamage;
			outBlow.GlobalPosition = this.Position;
			outBlow.BoneIndex = 0;
			outBlow.BlowFlag = BlowFlags.None;
			outCollisionData = collisionData;
			outCollisionData.UpdateCollisionPositionAndBoneForReflect(collisionData.InflictedDamage, this.Position, 0);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00013F60 File Offset: 0x00012160
		public void Tick(float dt)
		{
			if (this.IsActive())
			{
				if (this.GetCurrentActionStage(1) == Agent.ActionStage.AttackQuickReady)
				{
					this._lastQuickReadyDetectedTime = Mission.Current.CurrentTime;
				}
				if (this._checkIfTargetFrameIsChanged)
				{
					Vec2 vec = (this.MovementLockedState != AgentMovementLockedState.None) ? this.GetTargetPosition() : this.LookFrame.origin.AsVec2;
					Vec3 vec2 = (this.MovementLockedState != AgentMovementLockedState.None) ? this.GetTargetDirection() : this.LookFrame.rotation.f;
					AgentMovementLockedState movementLockedState = this.MovementLockedState;
					if (movementLockedState != AgentMovementLockedState.PositionLocked)
					{
						if (movementLockedState == AgentMovementLockedState.FrameLocked)
						{
							this._checkIfTargetFrameIsChanged = (this._lastSynchedTargetPosition != vec || this._lastSynchedTargetDirection != vec2);
						}
					}
					else
					{
						this._checkIfTargetFrameIsChanged = (this._lastSynchedTargetPosition != vec);
					}
					if (this._checkIfTargetFrameIsChanged)
					{
						if (this.MovementLockedState == AgentMovementLockedState.FrameLocked)
						{
							this.SetTargetPositionAndDirection(MBMath.Lerp(vec, this._lastSynchedTargetPosition, 5f * dt, 0.005f), MBMath.Lerp(vec2, this._lastSynchedTargetDirection, 5f * dt, 0.005f));
						}
						else
						{
							this.SetTargetPosition(MBMath.Lerp(vec, this._lastSynchedTargetPosition, 5f * dt, 0.005f));
						}
					}
				}
				if (this.Mission.AllowAiTicking && this.IsAIControlled)
				{
					this.TickAsAI(dt);
				}
				if (this._wantsToYell)
				{
					if (this._yellTimer > 0f)
					{
						this._yellTimer -= dt;
					}
					else
					{
						this.MakeVoice((this.MountAgent != null) ? SkinVoiceManager.VoiceType.HorseRally : SkinVoiceManager.VoiceType.Yell, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
						this._wantsToYell = false;
					}
				}
				if (this.IsPlayerControlled && this.IsCheering && this.MovementInputVector != Vec2.Zero)
				{
					this.SetActionChannel(1, ActionIndexCache.act_none, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					return;
				}
			}
			else
			{
				MissionPeer missionPeer = this.MissionPeer;
				if (((missionPeer != null) ? missionPeer.ControlledAgent : null) == this && !this.IsCameraAttachable())
				{
					this.MissionPeer.ControlledAgent = null;
				}
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00014171 File Offset: 0x00012371
		[Conditional("DEBUG")]
		public void DebugMore()
		{
			MBAPI.IMBAgent.DebugMore(this.GetPtr());
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00014184 File Offset: 0x00012384
		public void Mount(Agent mountAgent)
		{
			bool flag = mountAgent.GetCurrentActionType(0) == Agent.ActionCodeType.Rear;
			if (this.MountAgent == null && mountAgent.RiderAgent == null)
			{
				if (this.CheckSkillForMounting(mountAgent) && !flag && this.GetCurrentActionValue(0) == ActionIndexValueCache.act_none)
				{
					this.EventControlFlags |= Agent.EventControlFlag.Mount;
					this.SetInteractionAgent(mountAgent);
					return;
				}
			}
			else if (this.MountAgent == mountAgent && !flag)
			{
				this.EventControlFlags |= Agent.EventControlFlag.Dismount;
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000141FC File Offset: 0x000123FC
		public void EquipWeaponToExtraSlotAndWield(ref MissionWeapon weapon)
		{
			if (!this.Equipment[EquipmentIndex.ExtraWeaponSlot].IsEmpty)
			{
				this.DropItem(EquipmentIndex.ExtraWeaponSlot, WeaponClass.Undefined);
			}
			this.EquipWeaponWithNewEntity(EquipmentIndex.ExtraWeaponSlot, ref weapon);
			this.TryToWieldWeaponInSlot(EquipmentIndex.ExtraWeaponSlot, Agent.WeaponWieldActionType.InstantAfterPickUp, false);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00014238 File Offset: 0x00012438
		public void RemoveEquippedWeapon(EquipmentIndex slotIndex)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new RemoveEquippedWeapon(this.Index, slotIndex));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			this.Equipment[slotIndex] = MissionWeapon.Invalid;
			this.WeaponEquipped(slotIndex, WeaponData.InvalidWeaponData, null, WeaponData.InvalidWeaponData, null, null, true, false);
			this.UpdateAgentProperties();
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00014298 File Offset: 0x00012498
		public void EquipWeaponWithNewEntity(EquipmentIndex slotIndex, ref MissionWeapon weapon)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new EquipWeaponWithNewEntity(this.Index, slotIndex, weapon));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			this.Equipment[slotIndex] = weapon;
			WeaponData weaponData = WeaponData.InvalidWeaponData;
			WeaponStatsData[] weaponStatsData = null;
			WeaponData weaponData2 = WeaponData.InvalidWeaponData;
			WeaponStatsData[] ammoWeaponStatsData = null;
			if (!weapon.IsEmpty)
			{
				weaponData = weapon.GetWeaponData(true);
				weaponStatsData = weapon.GetWeaponStatsData();
				weaponData2 = weapon.GetAmmoWeaponData(true);
				ammoWeaponStatsData = weapon.GetAmmoWeaponStatsData();
			}
			this.WeaponEquipped(slotIndex, weaponData, weaponStatsData, weaponData2, ammoWeaponStatsData, null, true, true);
			weaponData.DeinitializeManagedPointers();
			weaponData2.DeinitializeManagedPointers();
			for (int i = 0; i < weapon.GetAttachedWeaponsCount(); i++)
			{
				MissionWeapon attachedWeapon = weapon.GetAttachedWeapon(i);
				MatrixFrame attachedWeaponFrame = weapon.GetAttachedWeaponFrame(i);
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new AttachWeaponToWeaponInAgentEquipmentSlot(attachedWeapon, this.Index, slotIndex, attachedWeaponFrame));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				this.AttachWeaponToWeaponAux(slotIndex, ref attachedWeapon, null, ref attachedWeaponFrame);
			}
			this.UpdateAgentProperties();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001439C File Offset: 0x0001259C
		public void EquipWeaponFromSpawnedItemEntity(EquipmentIndex slotIndex, SpawnedItemEntity spawnedItemEntity, bool removeWeapon)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new EquipWeaponFromSpawnedItemEntity(this.Index, slotIndex, spawnedItemEntity.Id, removeWeapon));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			if (spawnedItemEntity.GameEntity.Parent != null && spawnedItemEntity.GameEntity.Parent.HasScriptOfType<SpawnedItemEntity>())
			{
				SpawnedItemEntity firstScriptOfType = spawnedItemEntity.GameEntity.Parent.GetFirstScriptOfType<SpawnedItemEntity>();
				int attachmentIndex = -1;
				for (int i = 0; i < firstScriptOfType.GameEntity.ChildCount; i++)
				{
					if (firstScriptOfType.GameEntity.GetChild(i) == spawnedItemEntity.GameEntity)
					{
						attachmentIndex = i;
						break;
					}
				}
				firstScriptOfType.WeaponCopy.RemoveAttachedWeapon(attachmentIndex);
			}
			if (removeWeapon)
			{
				if (!this.Equipment[slotIndex].IsEmpty)
				{
					using (new TWSharedMutexWriteLock(Scene.PhysicsAndRayCastLock))
					{
						spawnedItemEntity.GameEntity.Remove(73);
						return;
					}
				}
				GameEntity gameEntity = spawnedItemEntity.GameEntity;
				using (new TWSharedMutexWriteLock(Scene.PhysicsAndRayCastLock))
				{
					gameEntity.RemovePhysicsMT(false);
				}
				gameEntity.RemoveScriptComponent(spawnedItemEntity.ScriptComponent.Pointer, 10);
				gameEntity.SetVisibilityExcludeParents(true);
				MissionWeapon weaponCopy = spawnedItemEntity.WeaponCopy;
				this.Equipment[slotIndex] = weaponCopy;
				WeaponData weaponData = weaponCopy.GetWeaponData(true);
				WeaponStatsData[] weaponStatsData = weaponCopy.GetWeaponStatsData();
				WeaponData ammoWeaponData = weaponCopy.GetAmmoWeaponData(true);
				WeaponStatsData[] ammoWeaponStatsData = weaponCopy.GetAmmoWeaponStatsData();
				this.WeaponEquipped(slotIndex, weaponData, weaponStatsData, ammoWeaponData, ammoWeaponStatsData, gameEntity, true, false);
				weaponData.DeinitializeManagedPointers();
				for (int j = 0; j < weaponCopy.GetAttachedWeaponsCount(); j++)
				{
					MatrixFrame attachedWeaponFrame = weaponCopy.GetAttachedWeaponFrame(j);
					MissionWeapon attachedWeapon = weaponCopy.GetAttachedWeapon(j);
					this.AttachWeaponToWeaponAux(slotIndex, ref attachedWeapon, null, ref attachedWeaponFrame);
				}
				this.UpdateAgentProperties();
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00014590 File Offset: 0x00012790
		public void PreloadForRendering()
		{
			this.PreloadForRenderingAux();
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00014598 File Offset: 0x00012798
		public int AddSynchedPrefabComponentToBone(string prefabName, sbyte boneIndex)
		{
			if (this._synchedBodyComponents == null)
			{
				this._synchedBodyComponents = new List<CompositeComponent>();
			}
			if (!GameEntity.PrefabExists(prefabName))
			{
				MBDebug.ShowWarning("Missing prefab for agent logic :" + prefabName);
				prefabName = "rock_001";
			}
			CompositeComponent item = this.AddPrefabComponentToBone(prefabName, boneIndex);
			int count = this._synchedBodyComponents.Count;
			this._synchedBodyComponents.Add(item);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new AddPrefabComponentToAgentBone(this.Index, prefabName, boneIndex));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			return count;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00014620 File Offset: 0x00012820
		public bool WillDropWieldedShield(SpawnedItemEntity spawnedItem)
		{
			EquipmentIndex wieldedItemIndex = this.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (wieldedItemIndex != EquipmentIndex.None && spawnedItem.WeaponCopy.CurrentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.NotUsableWithOneHand) && spawnedItem.WeaponCopy.HasAllUsagesWithAnyWeaponFlag(WeaponFlags.NotUsableWithOneHand))
			{
				bool flag = false;
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
				{
					if (equipmentIndex != wieldedItemIndex && !this.Equipment[equipmentIndex].IsEmpty && this.Equipment[equipmentIndex].IsShield())
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000146B4 File Offset: 0x000128B4
		public bool HadSameTypeOfConsumableOrShieldOnSpawn(WeaponClass weaponClass)
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				if (!this.SpawnEquipment[equipmentIndex].IsEmpty)
				{
					foreach (WeaponComponentData weaponComponentData in this.SpawnEquipment[equipmentIndex].Item.Weapons)
					{
						if ((weaponComponentData.IsConsumable || weaponComponentData.IsShield) && weaponComponentData.WeaponClass == weaponClass)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00014758 File Offset: 0x00012958
		public bool CanAIWieldAsRangedWeapon(MissionWeapon weapon)
		{
			ItemObject item = weapon.Item;
			return !this.IsAIControlled || item == null || !item.ItemFlags.HasAnyFlag(ItemFlags.NotStackable);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001478D File Offset: 0x0001298D
		public override int GetHashCode()
		{
			return this._creationIndex;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00014795 File Offset: 0x00012995
		public bool TryGetImmediateEnemyAgentMovementData(out float maximumForwardUnlimitedSpeed, out Vec3 position)
		{
			return MBAPI.IMBAgent.TryGetImmediateEnemyAgentMovementData(this.GetPtr(), out maximumForwardUnlimitedSpeed, out position);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000147AC File Offset: 0x000129AC
		public bool HasLostShield()
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				if (this.Equipment[equipmentIndex].IsEmpty && this.SpawnEquipment[equipmentIndex].Item != null && this.SpawnEquipment[equipmentIndex].Item.PrimaryWeapon.IsShield)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00014814 File Offset: 0x00012A14
		internal void SetMountAgentBeforeBuild(Agent mount)
		{
			this.MountAgent = mount;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001481D File Offset: 0x00012A1D
		internal void SetMountInitialValues(TextObject name, string horseCreationKey)
		{
			this._name = name;
			this.HorseCreationKey = horseCreationKey;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001482D File Offset: 0x00012A2D
		internal void SetInitialAgentScale(float initialScale)
		{
			MBAPI.IMBAgent.SetAgentScale(this.GetPtr(), initialScale);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00014840 File Offset: 0x00012A40
		internal void InitializeAgentRecord()
		{
			MBAPI.IMBAgent.InitializeAgentRecord(this.GetPtr());
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00014852 File Offset: 0x00012A52
		internal void OnDelete()
		{
			this._isDeleted = true;
			this.MissionPeer = null;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00014862 File Offset: 0x00012A62
		internal void OnFleeing()
		{
			this.RelieveFromCaptaincy();
			if (this.Formation != null)
			{
				this.Formation.Team.DetachmentManager.OnAgentRemoved(this);
				this.Formation = null;
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00014890 File Offset: 0x00012A90
		internal void OnRemove()
		{
			this._isRemoved = true;
			this._removalTime = this.Mission.CurrentTime;
			IAgentOriginBase origin = this.Origin;
			if (origin != null)
			{
				origin.OnAgentRemoved(this.Health);
			}
			this.RelieveFromCaptaincy();
			Team team = this.Team;
			if (team != null)
			{
				team.OnAgentRemoved(this);
			}
			if (this.Formation != null)
			{
				this.Formation.Team.DetachmentManager.OnAgentRemoved(this);
				this.Formation = null;
			}
			if (this.IsUsingGameObject && !GameNetwork.IsClientOrReplay && this.Mission != null && !this.Mission.MissionEnded)
			{
				this.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
			foreach (AgentComponent agentComponent in this._components)
			{
				agentComponent.OnAgentRemoved();
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00014978 File Offset: 0x00012B78
		internal void InitializeComponents()
		{
			foreach (AgentComponent agentComponent in this._components)
			{
				agentComponent.Initialize();
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000149C8 File Offset: 0x00012BC8
		internal void Build(AgentBuildData agentBuildData, int creationIndex)
		{
			this.BuildAux();
			this.HasBeenBuilt = true;
			this.Controller = (this.GetAgentFlags().HasAnyFlag(AgentFlag.IsHumanoid) ? agentBuildData.AgentController : Agent.ControllerType.AI);
			this.Formation = ((!this.IsMount) ? ((agentBuildData != null) ? agentBuildData.AgentFormation : null) : null);
			MissionGameModels missionGameModels = MissionGameModels.Current;
			if (missionGameModels != null)
			{
				missionGameModels.AgentStatCalculateModel.InitializeMissionEquipment(this);
			}
			this.InitializeAgentProperties(this.SpawnEquipment, agentBuildData);
			this._creationIndex = creationIndex;
			if (GameNetwork.IsServerOrRecorder)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					if (!networkCommunicator.IsMine && networkCommunicator.IsSynchronized)
					{
						this.LockAgentReplicationTableDataWithCurrentReliableSequenceNo(networkCommunicator);
					}
				}
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00014AA8 File Offset: 0x00012CA8
		private void PreloadForRenderingAux()
		{
			MBAPI.IMBAgent.PreloadForRendering(this.GetPtr());
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00014ABA File Offset: 0x00012CBA
		internal void Clear()
		{
			this.Mission = null;
			this._pointer = UIntPtr.Zero;
			this._positionPointer = UIntPtr.Zero;
			this._flagsPointer = UIntPtr.Zero;
			this._indexPointer = UIntPtr.Zero;
			this._statePointer = UIntPtr.Zero;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00014AFA File Offset: 0x00012CFA
		public bool HasPathThroughNavigationFacesIDFromDirection(int navigationFaceID_1, int navigationFaceID_2, int navigationFaceID_3, Vec2 direction)
		{
			return MBAPI.IMBAgent.HasPathThroughNavigationFacesIDFromDirection(this.GetPtr(), navigationFaceID_1, navigationFaceID_2, navigationFaceID_3, ref direction);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00014B14 File Offset: 0x00012D14
		public bool HasPathThroughNavigationFacesIDFromDirectionMT(int navigationFaceID_1, int navigationFaceID_2, int navigationFaceID_3, Vec2 direction)
		{
			object pathCheckObjectLock = Agent._pathCheckObjectLock;
			bool result;
			lock (pathCheckObjectLock)
			{
				result = MBAPI.IMBAgent.HasPathThroughNavigationFacesIDFromDirection(this.GetPtr(), navigationFaceID_1, navigationFaceID_2, navigationFaceID_3, ref direction);
			}
			return result;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00014B64 File Offset: 0x00012D64
		private void AfterStoppedUsingMissionObject(UsableMachine usableMachine, UsableMissionObject usedObject, UsableMissionObject movingToOrDefendingObject, bool isSuccessful, Agent.StopUsingGameObjectFlags flags)
		{
			if (this.IsAIControlled)
			{
				if (flags.HasAnyFlag(Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject))
				{
					Formation formation = this.Formation;
					if (formation != null)
					{
						formation.AttachUnit(this);
					}
				}
				if (flags.HasAnyFlag(Agent.StopUsingGameObjectFlags.DefendAfterStoppingUsingGameObject))
				{
					UsableMissionObject usedObject2 = usedObject ?? movingToOrDefendingObject;
					this.AIDefendGameObjectEnable(usedObject2, usableMachine);
				}
			}
			StandingPoint standingPoint;
			if ((standingPoint = (usedObject as StandingPoint)) != null && standingPoint.AutoEquipWeaponsOnUseStopped && !flags.HasAnyFlag(Agent.StopUsingGameObjectFlags.DoNotWieldWeaponAfterStoppingUsingGameObject))
			{
				bool flag = !isSuccessful;
				bool flag2 = this._equipmentOnMainHandBeforeUsingObject != EquipmentIndex.None;
				if (this._equipmentOnOffHandBeforeUsingObject != EquipmentIndex.None)
				{
					Agent.WeaponWieldActionType type = (flag && !flag2) ? Agent.WeaponWieldActionType.WithAnimation : Agent.WeaponWieldActionType.Instant;
					this.TryToWieldWeaponInSlot(this._equipmentOnOffHandBeforeUsingObject, type, false);
				}
				if (flag2)
				{
					Agent.WeaponWieldActionType type2 = flag ? Agent.WeaponWieldActionType.WithAnimation : Agent.WeaponWieldActionType.Instant;
					this.TryToWieldWeaponInSlot(this._equipmentOnMainHandBeforeUsingObject, type2, false);
				}
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00014C1C File Offset: 0x00012E1C
		private UIntPtr GetPtr()
		{
			return this.Pointer;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00014C24 File Offset: 0x00012E24
		private void SetWeaponHitPointsInSlot(EquipmentIndex equipmentIndex, short hitPoints)
		{
			MBAPI.IMBAgent.SetWeaponHitPointsInSlot(this.GetPtr(), (int)equipmentIndex, hitPoints);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00014C38 File Offset: 0x00012E38
		private AgentMovementLockedState GetMovementLockedState()
		{
			return MBAPI.IMBAgent.GetMovementLockedState(this.GetPtr());
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00014C4C File Offset: 0x00012E4C
		private void AttachWeaponToBoneAux(ref MissionWeapon weapon, GameEntity weaponEntity, sbyte boneIndex, ref MatrixFrame attachLocalFrame)
		{
			WeaponData weaponData = weapon.GetWeaponData(true);
			MBAPI.IMBAgent.AttachWeaponToBone(this.Pointer, weaponData, weapon.GetWeaponStatsData(), weapon.WeaponsCount, (weaponEntity != null) ? weaponEntity.Pointer : UIntPtr.Zero, boneIndex, ref attachLocalFrame);
			weaponData.DeinitializeManagedPointers();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00014C99 File Offset: 0x00012E99
		private Agent GetRiderAgentAux()
		{
			return this._cachedRiderAgent;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00014CA4 File Offset: 0x00012EA4
		private void AttachWeaponToWeaponAux(EquipmentIndex slotIndex, ref MissionWeapon weapon, GameEntity weaponEntity, ref MatrixFrame attachLocalFrame)
		{
			WeaponData weaponData = weapon.GetWeaponData(true);
			MBAPI.IMBAgent.AttachWeaponToWeaponInSlot(this.Pointer, weaponData, weapon.GetWeaponStatsData(), weapon.WeaponsCount, (weaponEntity != null) ? weaponEntity.Pointer : UIntPtr.Zero, (int)slotIndex, ref attachLocalFrame);
			weaponData.DeinitializeManagedPointers();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00014CF1 File Offset: 0x00012EF1
		private Agent GetMountAgentAux()
		{
			return this._cachedMountAgent;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00014CFC File Offset: 0x00012EFC
		private void SetMountAgent(Agent mountAgent)
		{
			int mountAgentIndex = (mountAgent == null) ? -1 : mountAgent.Index;
			MBAPI.IMBAgent.SetMountAgent(this.GetPtr(), mountAgentIndex);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00014D28 File Offset: 0x00012F28
		private void RelieveFromCaptaincy()
		{
			if (this._canLeadFormationsRemotely && this.Team != null)
			{
				using (List<Formation>.Enumerator enumerator = this.Team.FormationsIncludingSpecialAndEmpty.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Formation formation = enumerator.Current;
						if (formation.Captain == this)
						{
							formation.Captain = null;
						}
					}
					return;
				}
			}
			if (this.Formation != null && this.Formation.Captain == this)
			{
				this.Formation.Captain = null;
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00014DBC File Offset: 0x00012FBC
		private void SetTeamInternal(MBTeam team)
		{
			MBAPI.IMBAgent.SetTeam(this.GetPtr(), team.Index);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00014DD4 File Offset: 0x00012FD4
		private Agent.ControllerType GetController()
		{
			return this._agentControllerType;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00014DDC File Offset: 0x00012FDC
		private void SetController(Agent.ControllerType controllerType)
		{
			if (controllerType != this._agentControllerType)
			{
				if (controllerType == Agent.ControllerType.Player && this.IsDetachedFromFormation)
				{
					this._detachment.RemoveAgent(this);
					Formation formation = this._formation;
					if (formation != null)
					{
						formation.AttachUnit(this);
					}
				}
				this._agentControllerType = controllerType;
				MBAPI.IMBAgent.SetController(this.GetPtr(), controllerType);
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00014E34 File Offset: 0x00013034
		private void WeaponEquipped(EquipmentIndex equipmentSlot, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, in WeaponData ammoWeaponData, WeaponStatsData[] ammoWeaponStatsData, GameEntity weaponEntity, bool removeOldWeaponFromScene, bool isWieldedOnSpawn)
		{
			MBAPI.IMBAgent.WeaponEquipped(this.GetPtr(), (int)equipmentSlot, weaponData, weaponStatsData, (weaponStatsData != null) ? weaponStatsData.Length : 0, ammoWeaponData, ammoWeaponStatsData, (ammoWeaponStatsData != null) ? ammoWeaponStatsData.Length : 0, (weaponEntity != null) ? weaponEntity.Pointer : UIntPtr.Zero, removeOldWeaponFromScene, isWieldedOnSpawn);
			this.CheckEquipmentForCapeClothSimulationStateChange();
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00014E88 File Offset: 0x00013088
		private Agent GetRiderAgent()
		{
			return MBAPI.IMBAgent.GetRiderAgent(this.GetPtr());
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00014E9A File Offset: 0x0001309A
		public void SetInitialFrame(in Vec3 initialPosition, in Vec2 initialDirection, bool canSpawnOutsideOfMissionBoundary = false)
		{
			MBAPI.IMBAgent.SetInitialFrame(this.GetPtr(), initialPosition, initialDirection, canSpawnOutsideOfMissionBoundary);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00014EAF File Offset: 0x000130AF
		private void UpdateDrivenProperties(float[] values)
		{
			MBAPI.IMBAgent.UpdateDrivenProperties(this.GetPtr(), values);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00014EC4 File Offset: 0x000130C4
		private void UpdateLastAttackAndHitTimes(Agent attackerAgent, bool isMissile)
		{
			float currentTime = this.Mission.CurrentTime;
			if (isMissile)
			{
				this.LastRangedHitTime = currentTime;
			}
			else
			{
				this.LastMeleeHitTime = currentTime;
			}
			if (attackerAgent != this && attackerAgent != null)
			{
				if (isMissile)
				{
					attackerAgent.LastRangedAttackTime = currentTime;
					return;
				}
				attackerAgent.LastMeleeAttackTime = currentTime;
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00014F09 File Offset: 0x00013109
		private void SetNetworkPeer(NetworkCommunicator newPeer)
		{
			MBAPI.IMBAgent.SetNetworkPeer(this.GetPtr(), (newPeer != null) ? newPeer.Index : -1);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00014F27 File Offset: 0x00013127
		private void ClearTargetFrameAux()
		{
			MBAPI.IMBAgent.ClearTargetFrame(this.GetPtr());
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00014F39 File Offset: 0x00013139
		[Conditional("_RGL_KEEP_ASSERTS")]
		private void CheckUnmanagedAgentValid()
		{
			AgentHelper.GetAgentIndex(this._indexPointer);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00014F47 File Offset: 0x00013147
		private void BuildAux()
		{
			MBAPI.IMBAgent.Build(this.GetPtr(), this.Monster.EyeOffsetWrtHead);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00014F64 File Offset: 0x00013164
		private float GetMissileRangeWithHeightDifference()
		{
			if (this.IsMount || (!this.IsRangedCached && !this.HasThrownCached) || this.Formation == null || this.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				return 0f;
			}
			return this.GetMissileRangeWithHeightDifferenceAux(this.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.GetNavMeshZ());
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00014FCC File Offset: 0x000131CC
		private void AddSkinMeshes(bool useGPUMorph)
		{
			bool prepareImmediately = this == Agent.Main;
			SkinMask skinMeshesMask = this.SpawnEquipment.GetSkinMeshesMask();
			bool isFemale = this.IsFemale && this.BodyPropertiesValue.Age >= 14f;
			SkinGenerationParams skinParams = new SkinGenerationParams((int)skinMeshesMask, this.SpawnEquipment.GetUnderwearType(isFemale), (int)this.SpawnEquipment.BodyMeshType, (int)this.SpawnEquipment.HairCoverType, (int)this.SpawnEquipment.BeardCoverType, (int)this.SpawnEquipment.BodyDeformType, prepareImmediately, this.Character.FaceDirtAmount, this.IsFemale ? 1 : 0, this.Character.Race, false, false);
			bool useFaceCache = this.Character != null && this.Character.FaceMeshCache;
			this.AgentVisuals.AddSkinMeshes(skinParams, this.BodyPropertiesValue, useGPUMorph, useFaceCache);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000150A8 File Offset: 0x000132A8
		private void HandleBlow(ref Blow b, in AttackCollisionData collisionData)
		{
			b.BaseMagnitude = MathF.Min(b.BaseMagnitude, 1000f);
			b.DamagedPercentage = (float)b.InflictedDamage / this.HealthLimit;
			Agent agent = (b.OwnerId != -1) ? this.Mission.FindAgentWithIndex(b.OwnerId) : null;
			if (!b.BlowFlag.HasAnyFlag(BlowFlags.NoSound))
			{
				bool isCriticalBlow = b.IsBlowCrit(this.Monster.HitPoints * 4);
				bool isLowBlow = b.IsBlowLow(this.Monster.HitPoints);
				bool isOwnerHumanoid = agent == null || agent.IsHuman;
				bool isNonTipThrust = b.BlowFlag.HasAnyFlag(BlowFlags.NonTipThrust);
				int hitSound = b.WeaponRecord.GetHitSound(isOwnerHumanoid, isCriticalBlow, isLowBlow, isNonTipThrust, b.AttackType, b.DamageType);
				float soundParameterForArmorType = Agent.GetSoundParameterForArmorType(this.GetProtectorArmorMaterialOfBone(b.BoneIndex));
				SoundEventParameter soundEventParameter = new SoundEventParameter("Armor Type", soundParameterForArmorType);
				this.Mission.MakeSound(hitSound, b.GlobalPosition, false, true, b.OwnerId, this.Index, ref soundEventParameter);
				if (b.IsMissile && agent != null)
				{
					int soundCodeMissionCombatPlayerhit = CombatSoundContainer.SoundCodeMissionCombatPlayerhit;
					this.Mission.MakeSoundOnlyOnRelatedPeer(soundCodeMissionCombatPlayerhit, b.GlobalPosition, agent.Index);
				}
				this.Mission.AddSoundAlarmFactorToAgents(b.OwnerId, b.GlobalPosition, 15f);
			}
			if (b.InflictedDamage <= 0)
			{
				return;
			}
			this.UpdateLastAttackAndHitTimes(agent, b.IsMissile);
			float health = this.Health;
			float num = ((float)b.InflictedDamage > health) ? health : ((float)b.InflictedDamage);
			float num2 = health - num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			if (this.CurrentMortalityState != Agent.MortalityState.Immortal && !this.Mission.DisableDying)
			{
				this.Health = num2;
			}
			if (agent != null && agent != this && this.IsHuman)
			{
				if (agent.IsMount && agent.RiderAgent != null)
				{
					this._lastHitInfo.RegisterLastBlow(agent.RiderAgent.Index, b.AttackType);
				}
				else if (agent.IsHuman)
				{
					this._lastHitInfo.RegisterLastBlow(b.OwnerId, b.AttackType);
				}
			}
			this.Mission.OnAgentHit(this, agent, b, collisionData, false, num);
			if (this.Health < 1f)
			{
				Agent.KillInfo overrideKillInfo = b.IsFallDamage ? Agent.KillInfo.Gravity : Agent.KillInfo.Invalid;
				this.Die(b, overrideKillInfo);
			}
			this.HandleBlowAux(ref b);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00015304 File Offset: 0x00013504
		private void HandleBlowAux(ref Blow b)
		{
			MBAPI.IMBAgent.HandleBlowAux(this.GetPtr(), ref b);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00015318 File Offset: 0x00013518
		private ArmorComponent.ArmorMaterialTypes GetProtectorArmorMaterialOfBone(sbyte boneIndex)
		{
			if (boneIndex >= 0)
			{
				EquipmentIndex equipmentIndex = EquipmentIndex.None;
				switch (this.AgentVisuals.GetBoneTypeData(boneIndex).BodyPartType)
				{
				case BoneBodyPartType.Head:
				case BoneBodyPartType.Neck:
					equipmentIndex = EquipmentIndex.NumAllWeaponSlots;
					break;
				case BoneBodyPartType.Chest:
				case BoneBodyPartType.Abdomen:
				case BoneBodyPartType.ShoulderLeft:
				case BoneBodyPartType.ShoulderRight:
					equipmentIndex = EquipmentIndex.Body;
					break;
				case BoneBodyPartType.ArmLeft:
				case BoneBodyPartType.ArmRight:
					equipmentIndex = EquipmentIndex.Gloves;
					break;
				case BoneBodyPartType.Legs:
					equipmentIndex = EquipmentIndex.Leg;
					break;
				}
				if (equipmentIndex != EquipmentIndex.None && this.SpawnEquipment[equipmentIndex].Item != null)
				{
					return this.SpawnEquipment[equipmentIndex].Item.ArmorComponent.MaterialType;
				}
			}
			return ArmorComponent.ArmorMaterialTypes.None;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000153B4 File Offset: 0x000135B4
		private void TickAsAI(float dt)
		{
			for (int i = 0; i < this._components.Count; i++)
			{
				this._components[i].OnTickAsAI(dt);
			}
			if (this.Formation != null && this._cachedAndFormationValuesUpdateTimer.Check(this.Mission.CurrentTime))
			{
				this.UpdateCachedAndFormationValues(false, true);
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00015414 File Offset: 0x00013614
		private void SyncHealthToClients()
		{
			if (this.SyncHealthToAllClients && (!this.IsMount || this.RiderAgent != null))
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetAgentHealth(this.Index, (int)this.Health));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				return;
			}
			NetworkCommunicator networkCommunicator;
			if (!this.IsMount)
			{
				MissionPeer missionPeer = this.MissionPeer;
				networkCommunicator = ((missionPeer != null) ? missionPeer.GetNetworkPeer() : null);
			}
			else
			{
				Agent riderAgent = this.RiderAgent;
				if (riderAgent == null)
				{
					networkCommunicator = null;
				}
				else
				{
					MissionPeer missionPeer2 = riderAgent.MissionPeer;
					networkCommunicator = ((missionPeer2 != null) ? missionPeer2.GetNetworkPeer() : null);
				}
			}
			NetworkCommunicator networkCommunicator2 = networkCommunicator;
			if (networkCommunicator2 != null && !networkCommunicator2.IsServerPeer)
			{
				GameNetwork.BeginModuleEventAsServer(networkCommunicator2);
				GameNetwork.WriteMessage(new SetAgentHealth(this.Index, (int)this.Health));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000154C8 File Offset: 0x000136C8
		public static Agent.UsageDirection MovementFlagToDirection(Agent.MovementControlFlag flag)
		{
			if (flag.HasAnyFlag(Agent.MovementControlFlag.AttackDown))
			{
				return Agent.UsageDirection.AttackDown;
			}
			if (flag.HasAnyFlag(Agent.MovementControlFlag.AttackUp))
			{
				return Agent.UsageDirection.AttackUp;
			}
			if (flag.HasAnyFlag(Agent.MovementControlFlag.AttackLeft))
			{
				return Agent.UsageDirection.AttackLeft;
			}
			if (flag.HasAnyFlag(Agent.MovementControlFlag.AttackRight))
			{
				return Agent.UsageDirection.AttackRight;
			}
			if (flag.HasAnyFlag(Agent.MovementControlFlag.DefendDown))
			{
				return Agent.UsageDirection.DefendDown;
			}
			if (flag.HasAnyFlag(Agent.MovementControlFlag.DefendUp))
			{
				return Agent.UsageDirection.AttackEnd;
			}
			if (flag.HasAnyFlag(Agent.MovementControlFlag.DefendLeft))
			{
				return Agent.UsageDirection.DefendLeft;
			}
			if (flag.HasAnyFlag(Agent.MovementControlFlag.DefendRight))
			{
				return Agent.UsageDirection.DefendRight;
			}
			return Agent.UsageDirection.None;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0001554B File Offset: 0x0001374B
		public static Agent.UsageDirection GetActionDirection(int actionIndex)
		{
			return MBAPI.IMBAgent.GetActionDirection(actionIndex);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00015558 File Offset: 0x00013758
		public static int GetMonsterUsageIndex(string monsterUsage)
		{
			return MBAPI.IMBAgent.GetMonsterUsageIndex(monsterUsage);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00015565 File Offset: 0x00013765
		private static float GetSoundParameterForArmorType(ArmorComponent.ArmorMaterialTypes armorMaterialType)
		{
			return (float)armorMaterialType * 0.1f;
		}

		// Token: 0x0400022A RID: 554
		public const float BecomeTeenagerAge = 14f;

		// Token: 0x0400022B RID: 555
		public const float MaxMountInteractionDistance = 1.75f;

		// Token: 0x0400022C RID: 556
		public const float DismountVelocityLimit = 0.5f;

		// Token: 0x0400022D RID: 557
		public const float HealthDyingThreshold = 1f;

		// Token: 0x0400022E RID: 558
		public const float CachedAndFormationValuesUpdateTime = 0.5f;

		// Token: 0x0400022F RID: 559
		public const float MaxInteractionDistance = 3f;

		// Token: 0x04000230 RID: 560
		public const float MaxFocusDistance = 10f;

		// Token: 0x04000231 RID: 561
		private const float ChainAttackDetectionTimeout = 0.75f;

		// Token: 0x04000232 RID: 562
		public static readonly ActionIndexCache[] DefaultTauntActions = new ActionIndexCache[]
		{
			ActionIndexCache.Create("act_taunt_cheer_1"),
			ActionIndexCache.Create("act_taunt_cheer_2"),
			ActionIndexCache.Create("act_taunt_cheer_3"),
			ActionIndexCache.Create("act_taunt_cheer_4")
		};

		// Token: 0x04000233 RID: 563
		private static readonly object _stopUsingGameObjectLock = new object();

		// Token: 0x04000234 RID: 564
		private static readonly object _pathCheckObjectLock = new object();

		// Token: 0x04000235 RID: 565
		public Agent.OnMainAgentWieldedItemChangeDelegate OnMainAgentWieldedItemChange;

		// Token: 0x04000236 RID: 566
		public Action OnAgentMountedStateChanged;

		// Token: 0x04000237 RID: 567
		public Action OnAgentWieldedItemChange;

		// Token: 0x0400023A RID: 570
		public float LastDetachmentTickAgentTime;

		// Token: 0x0400023B RID: 571
		public MissionPeer OwningAgentMissionPeer;

		// Token: 0x0400023C RID: 572
		public MissionRepresentativeBase MissionRepresentative;

		// Token: 0x0400023D RID: 573
		private readonly MBList<AgentComponent> _components;

		// Token: 0x0400023E RID: 574
		private readonly Agent.CreationType _creationType;

		// Token: 0x0400023F RID: 575
		private readonly List<AgentController> _agentControllers;

		// Token: 0x04000240 RID: 576
		private readonly Timer _cachedAndFormationValuesUpdateTimer;

		// Token: 0x04000241 RID: 577
		private Agent.ControllerType _agentControllerType = Agent.ControllerType.AI;

		// Token: 0x04000242 RID: 578
		private Agent _cachedMountAgent;

		// Token: 0x04000243 RID: 579
		private Agent _cachedRiderAgent;

		// Token: 0x04000244 RID: 580
		private BasicCharacterObject _character;

		// Token: 0x04000245 RID: 581
		private uint? _clothingColor1;

		// Token: 0x04000246 RID: 582
		private uint? _clothingColor2;

		// Token: 0x04000247 RID: 583
		private EquipmentIndex _equipmentOnMainHandBeforeUsingObject;

		// Token: 0x04000248 RID: 584
		private EquipmentIndex _equipmentOnOffHandBeforeUsingObject;

		// Token: 0x04000249 RID: 585
		private float _defensiveness;

		// Token: 0x0400024A RID: 586
		private UIntPtr _positionPointer;

		// Token: 0x0400024B RID: 587
		private UIntPtr _pointer;

		// Token: 0x0400024C RID: 588
		private UIntPtr _flagsPointer;

		// Token: 0x0400024D RID: 589
		private UIntPtr _indexPointer;

		// Token: 0x0400024E RID: 590
		private UIntPtr _statePointer;

		// Token: 0x0400024F RID: 591
		private float _lastQuickReadyDetectedTime;

		// Token: 0x04000250 RID: 592
		private Agent _lookAgentCache;

		// Token: 0x04000251 RID: 593
		private IDetachment _detachment;

		// Token: 0x04000252 RID: 594
		private readonly MBList<Agent.Hitter> _hitterList;

		// Token: 0x04000253 RID: 595
		private List<ValueTuple<MissionWeapon, MatrixFrame, sbyte>> _attachedWeapons;

		// Token: 0x04000254 RID: 596
		private float _health;

		// Token: 0x04000255 RID: 597
		private MissionPeer _missionPeer;

		// Token: 0x04000256 RID: 598
		private TextObject _name;

		// Token: 0x04000257 RID: 599
		private float _removalTime;

		// Token: 0x04000258 RID: 600
		private List<CompositeComponent> _synchedBodyComponents;

		// Token: 0x04000259 RID: 601
		private Formation _formation;

		// Token: 0x0400025A RID: 602
		private bool _checkIfTargetFrameIsChanged;

		// Token: 0x0400025B RID: 603
		private Agent.AgentPropertiesModifiers _propertyModifiers;

		// Token: 0x0400025C RID: 604
		private int _usedObjectPreferenceIndex = -1;

		// Token: 0x0400025D RID: 605
		private bool _isDeleted;

		// Token: 0x0400025E RID: 606
		private bool _wantsToYell;

		// Token: 0x0400025F RID: 607
		private float _yellTimer;

		// Token: 0x04000260 RID: 608
		private Vec3 _lastSynchedTargetDirection;

		// Token: 0x04000261 RID: 609
		private Vec2 _lastSynchedTargetPosition;

		// Token: 0x04000262 RID: 610
		private Agent.AgentLastHitInfo _lastHitInfo;

		// Token: 0x04000263 RID: 611
		private ClothSimulatorComponent _capeClothSimulator;

		// Token: 0x04000264 RID: 612
		private bool _isRemoved;

		// Token: 0x04000265 RID: 613
		private WeakReference<MBAgentVisuals> _visualsWeakRef = new WeakReference<MBAgentVisuals>(null);

		// Token: 0x04000266 RID: 614
		private int _creationIndex;

		// Token: 0x04000267 RID: 615
		private bool _canLeadFormationsRemotely;

		// Token: 0x04000268 RID: 616
		private bool _isDetachableFromFormation = true;

		// Token: 0x04000269 RID: 617
		private ItemObject _formationBanner;

		// Token: 0x0400028D RID: 653
		public float DetachmentWeight;

		// Token: 0x020003D7 RID: 983
		public class Hitter
		{
			// Token: 0x17000939 RID: 2361
			// (get) Token: 0x060033BE RID: 13246 RVA: 0x000D62B4 File Offset: 0x000D44B4
			// (set) Token: 0x060033BF RID: 13247 RVA: 0x000D62BC File Offset: 0x000D44BC
			public float Damage { get; private set; }

			// Token: 0x060033C0 RID: 13248 RVA: 0x000D62C5 File Offset: 0x000D44C5
			public Hitter(MissionPeer peer, float damage, bool isFriendlyHit)
			{
				this.HitterPeer = peer;
				this.Damage = damage;
				this.IsFriendlyHit = isFriendlyHit;
			}

			// Token: 0x060033C1 RID: 13249 RVA: 0x000D62E2 File Offset: 0x000D44E2
			public void IncreaseDamage(float amount)
			{
				this.Damage += amount;
			}

			// Token: 0x04001675 RID: 5749
			public const float AssistMinDamage = 35f;

			// Token: 0x04001676 RID: 5750
			public readonly MissionPeer HitterPeer;

			// Token: 0x04001677 RID: 5751
			public readonly bool IsFriendlyHit;
		}

		// Token: 0x020003D8 RID: 984
		public struct AgentLastHitInfo
		{
			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000D62F2 File Offset: 0x000D44F2
			// (set) Token: 0x060033C3 RID: 13251 RVA: 0x000D62FA File Offset: 0x000D44FA
			public int LastBlowOwnerId { get; private set; }

			// Token: 0x1700093B RID: 2363
			// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000D6303 File Offset: 0x000D4503
			// (set) Token: 0x060033C5 RID: 13253 RVA: 0x000D630B File Offset: 0x000D450B
			public AgentAttackType LastBlowAttackType { get; private set; }

			// Token: 0x1700093C RID: 2364
			// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000D6314 File Offset: 0x000D4514
			public bool CanOverrideBlow
			{
				get
				{
					return this.LastBlowOwnerId >= 0 && this._lastBlowTimer.ElapsedTime <= 5f;
				}
			}

			// Token: 0x060033C7 RID: 13255 RVA: 0x000D6336 File Offset: 0x000D4536
			public void Initialize()
			{
				this.LastBlowOwnerId = -1;
				this.LastBlowAttackType = AgentAttackType.Standard;
				this._lastBlowTimer = new BasicMissionTimer();
			}

			// Token: 0x060033C8 RID: 13256 RVA: 0x000D6351 File Offset: 0x000D4551
			public void RegisterLastBlow(int ownerId, AgentAttackType attackType)
			{
				this._lastBlowTimer.Reset();
				this.LastBlowOwnerId = ownerId;
				this.LastBlowAttackType = attackType;
			}

			// Token: 0x04001679 RID: 5753
			private BasicMissionTimer _lastBlowTimer;
		}

		// Token: 0x020003D9 RID: 985
		public struct AgentPropertiesModifiers
		{
			// Token: 0x0400167C RID: 5756
			public bool resetAiWaitBeforeShootFactor;
		}

		// Token: 0x020003DA RID: 986
		public struct StackArray8Agent
		{
			// Token: 0x1700093D RID: 2365
			public Agent this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return this._element0;
					case 1:
						return this._element1;
					case 2:
						return this._element2;
					case 3:
						return this._element3;
					case 4:
						return this._element4;
					case 5:
						return this._element5;
					case 6:
						return this._element6;
					case 7:
						return this._element7;
					default:
						return null;
					}
				}
				set
				{
					switch (index)
					{
					case 0:
						this._element0 = value;
						return;
					case 1:
						this._element1 = value;
						return;
					case 2:
						this._element2 = value;
						return;
					case 3:
						this._element3 = value;
						return;
					case 4:
						this._element4 = value;
						return;
					case 5:
						this._element5 = value;
						return;
					case 6:
						this._element6 = value;
						return;
					case 7:
						this._element7 = value;
						return;
					default:
						return;
					}
				}
			}

			// Token: 0x0400167D RID: 5757
			private Agent _element0;

			// Token: 0x0400167E RID: 5758
			private Agent _element1;

			// Token: 0x0400167F RID: 5759
			private Agent _element2;

			// Token: 0x04001680 RID: 5760
			private Agent _element3;

			// Token: 0x04001681 RID: 5761
			private Agent _element4;

			// Token: 0x04001682 RID: 5762
			private Agent _element5;

			// Token: 0x04001683 RID: 5763
			private Agent _element6;

			// Token: 0x04001684 RID: 5764
			private Agent _element7;

			// Token: 0x04001685 RID: 5765
			public const int Length = 8;
		}

		// Token: 0x020003DB RID: 987
		public enum ActionStage
		{
			// Token: 0x04001687 RID: 5767
			None = -1,
			// Token: 0x04001688 RID: 5768
			AttackReady,
			// Token: 0x04001689 RID: 5769
			AttackQuickReady,
			// Token: 0x0400168A RID: 5770
			AttackRelease,
			// Token: 0x0400168B RID: 5771
			ReloadMidPhase,
			// Token: 0x0400168C RID: 5772
			ReloadLastPhase,
			// Token: 0x0400168D RID: 5773
			Defend,
			// Token: 0x0400168E RID: 5774
			DefendParry,
			// Token: 0x0400168F RID: 5775
			NumActionStages
		}

		// Token: 0x020003DC RID: 988
		[Flags]
		public enum AIScriptedFrameFlags
		{
			// Token: 0x04001691 RID: 5777
			None = 0,
			// Token: 0x04001692 RID: 5778
			GoToPosition = 1,
			// Token: 0x04001693 RID: 5779
			NoAttack = 2,
			// Token: 0x04001694 RID: 5780
			ConsiderRotation = 4,
			// Token: 0x04001695 RID: 5781
			NeverSlowDown = 8,
			// Token: 0x04001696 RID: 5782
			DoNotRun = 16,
			// Token: 0x04001697 RID: 5783
			GoWithoutMount = 32,
			// Token: 0x04001698 RID: 5784
			RangerCanMoveForClearTarget = 128,
			// Token: 0x04001699 RID: 5785
			InConversation = 256,
			// Token: 0x0400169A RID: 5786
			Crouch = 512
		}

		// Token: 0x020003DD RID: 989
		[Flags]
		public enum AISpecialCombatModeFlags
		{
			// Token: 0x0400169C RID: 5788
			None = 0,
			// Token: 0x0400169D RID: 5789
			AttackEntity = 1,
			// Token: 0x0400169E RID: 5790
			SurroundAttackEntity = 2,
			// Token: 0x0400169F RID: 5791
			IgnoreAmmoLimitForRangeCalculation = 1024
		}

		// Token: 0x020003DE RID: 990
		[Flags]
		[EngineStruct("Ai_state_flag", false)]
		public enum AIStateFlag : uint
		{
			// Token: 0x040016A1 RID: 5793
			None = 0U,
			// Token: 0x040016A2 RID: 5794
			Cautious = 1U,
			// Token: 0x040016A3 RID: 5795
			Alarmed = 2U,
			// Token: 0x040016A4 RID: 5796
			Paused = 4U,
			// Token: 0x040016A5 RID: 5797
			UseObjectMoving = 8U,
			// Token: 0x040016A6 RID: 5798
			UseObjectUsing = 16U,
			// Token: 0x040016A7 RID: 5799
			UseObjectWaiting = 32U,
			// Token: 0x040016A8 RID: 5800
			Guard = 64U,
			// Token: 0x040016A9 RID: 5801
			ColumnwiseFollow = 128U
		}

		// Token: 0x020003DF RID: 991
		public enum WatchState
		{
			// Token: 0x040016AB RID: 5803
			Patrolling,
			// Token: 0x040016AC RID: 5804
			Cautious,
			// Token: 0x040016AD RID: 5805
			Alarmed
		}

		// Token: 0x020003E0 RID: 992
		public enum MortalityState
		{
			// Token: 0x040016AF RID: 5807
			Mortal,
			// Token: 0x040016B0 RID: 5808
			Invulnerable,
			// Token: 0x040016B1 RID: 5809
			Immortal
		}

		// Token: 0x020003E1 RID: 993
		[EngineStruct("Agent_controller_type", false)]
		public enum ControllerType
		{
			// Token: 0x040016B3 RID: 5811
			None,
			// Token: 0x040016B4 RID: 5812
			AI,
			// Token: 0x040016B5 RID: 5813
			Player,
			// Token: 0x040016B6 RID: 5814
			Count
		}

		// Token: 0x020003E2 RID: 994
		public enum CreationType
		{
			// Token: 0x040016B8 RID: 5816
			Invalid,
			// Token: 0x040016B9 RID: 5817
			FromRoster,
			// Token: 0x040016BA RID: 5818
			FromHorseObj,
			// Token: 0x040016BB RID: 5819
			FromCharacterObj
		}

		// Token: 0x020003E3 RID: 995
		[Flags]
		public enum EventControlFlag : uint
		{
			// Token: 0x040016BD RID: 5821
			Dismount = 1U,
			// Token: 0x040016BE RID: 5822
			Mount = 2U,
			// Token: 0x040016BF RID: 5823
			Rear = 4U,
			// Token: 0x040016C0 RID: 5824
			Jump = 8U,
			// Token: 0x040016C1 RID: 5825
			Wield0 = 16U,
			// Token: 0x040016C2 RID: 5826
			Wield1 = 32U,
			// Token: 0x040016C3 RID: 5827
			Wield2 = 64U,
			// Token: 0x040016C4 RID: 5828
			Wield3 = 128U,
			// Token: 0x040016C5 RID: 5829
			Sheath0 = 256U,
			// Token: 0x040016C6 RID: 5830
			Sheath1 = 512U,
			// Token: 0x040016C7 RID: 5831
			ToggleAlternativeWeapon = 1024U,
			// Token: 0x040016C8 RID: 5832
			Walk = 2048U,
			// Token: 0x040016C9 RID: 5833
			Run = 4096U,
			// Token: 0x040016CA RID: 5834
			Crouch = 8192U,
			// Token: 0x040016CB RID: 5835
			Stand = 16384U,
			// Token: 0x040016CC RID: 5836
			Kick = 32768U,
			// Token: 0x040016CD RID: 5837
			DoubleTapToDirectionUp = 65536U,
			// Token: 0x040016CE RID: 5838
			DoubleTapToDirectionDown = 131072U,
			// Token: 0x040016CF RID: 5839
			DoubleTapToDirectionLeft = 196608U,
			// Token: 0x040016D0 RID: 5840
			DoubleTapToDirectionRight = 262144U,
			// Token: 0x040016D1 RID: 5841
			DoubleTapToDirectionMask = 458752U
		}

		// Token: 0x020003E4 RID: 996
		public enum FacialAnimChannel
		{
			// Token: 0x040016D3 RID: 5843
			High,
			// Token: 0x040016D4 RID: 5844
			Mid,
			// Token: 0x040016D5 RID: 5845
			Low,
			// Token: 0x040016D6 RID: 5846
			num_facial_anim_channels
		}

		// Token: 0x020003E5 RID: 997
		[EngineStruct("Action_code_type", false)]
		public enum ActionCodeType
		{
			// Token: 0x040016D8 RID: 5848
			Other,
			// Token: 0x040016D9 RID: 5849
			DefendFist,
			// Token: 0x040016DA RID: 5850
			DefendShield,
			// Token: 0x040016DB RID: 5851
			DefendForward2h,
			// Token: 0x040016DC RID: 5852
			DefendUp2h,
			// Token: 0x040016DD RID: 5853
			DefendRight2h,
			// Token: 0x040016DE RID: 5854
			DefendLeft2h,
			// Token: 0x040016DF RID: 5855
			DefendForward1h,
			// Token: 0x040016E0 RID: 5856
			DefendUp1h,
			// Token: 0x040016E1 RID: 5857
			DefendRight1h,
			// Token: 0x040016E2 RID: 5858
			DefendLeft1h,
			// Token: 0x040016E3 RID: 5859
			DefendForwardStaff,
			// Token: 0x040016E4 RID: 5860
			DefendUpStaff,
			// Token: 0x040016E5 RID: 5861
			DefendRightStaff,
			// Token: 0x040016E6 RID: 5862
			DefendLeftStaff,
			// Token: 0x040016E7 RID: 5863
			ReadyRanged,
			// Token: 0x040016E8 RID: 5864
			ReleaseRanged,
			// Token: 0x040016E9 RID: 5865
			ReleaseThrowing,
			// Token: 0x040016EA RID: 5866
			Reload,
			// Token: 0x040016EB RID: 5867
			ReadyMelee,
			// Token: 0x040016EC RID: 5868
			ReleaseMelee,
			// Token: 0x040016ED RID: 5869
			ParriedMelee,
			// Token: 0x040016EE RID: 5870
			BlockedMelee,
			// Token: 0x040016EF RID: 5871
			Fall,
			// Token: 0x040016F0 RID: 5872
			JumpStart,
			// Token: 0x040016F1 RID: 5873
			Jump,
			// Token: 0x040016F2 RID: 5874
			JumpEnd,
			// Token: 0x040016F3 RID: 5875
			JumpEndHard,
			// Token: 0x040016F4 RID: 5876
			Kick,
			// Token: 0x040016F5 RID: 5877
			KickContinue,
			// Token: 0x040016F6 RID: 5878
			KickHit,
			// Token: 0x040016F7 RID: 5879
			WeaponBash,
			// Token: 0x040016F8 RID: 5880
			PassiveUsage,
			// Token: 0x040016F9 RID: 5881
			EquipUnequip,
			// Token: 0x040016FA RID: 5882
			Idle,
			// Token: 0x040016FB RID: 5883
			Guard,
			// Token: 0x040016FC RID: 5884
			Mount,
			// Token: 0x040016FD RID: 5885
			Dismount,
			// Token: 0x040016FE RID: 5886
			Dash,
			// Token: 0x040016FF RID: 5887
			MountQuickStop,
			// Token: 0x04001700 RID: 5888
			HitObject,
			// Token: 0x04001701 RID: 5889
			Sit,
			// Token: 0x04001702 RID: 5890
			SitOnTheFloor,
			// Token: 0x04001703 RID: 5891
			SitOnAThrone,
			// Token: 0x04001704 RID: 5892
			LadderRaise,
			// Token: 0x04001705 RID: 5893
			LadderRaiseEnd,
			// Token: 0x04001706 RID: 5894
			Rear,
			// Token: 0x04001707 RID: 5895
			StrikeLight,
			// Token: 0x04001708 RID: 5896
			StrikeMedium,
			// Token: 0x04001709 RID: 5897
			StrikeHeavy,
			// Token: 0x0400170A RID: 5898
			StrikeKnockBack,
			// Token: 0x0400170B RID: 5899
			MountStrike,
			// Token: 0x0400170C RID: 5900
			Count,
			// Token: 0x0400170D RID: 5901
			StrikeBegin = 47,
			// Token: 0x0400170E RID: 5902
			StrikeEnd = 51,
			// Token: 0x0400170F RID: 5903
			DefendAllBegin = 1,
			// Token: 0x04001710 RID: 5904
			DefendAllEnd = 15,
			// Token: 0x04001711 RID: 5905
			AttackMeleeAllBegin = 19,
			// Token: 0x04001712 RID: 5906
			AttackMeleeAllEnd = 23,
			// Token: 0x04001713 RID: 5907
			CombatAllBegin = 1,
			// Token: 0x04001714 RID: 5908
			CombatAllEnd = 23,
			// Token: 0x04001715 RID: 5909
			JumpAllBegin,
			// Token: 0x04001716 RID: 5910
			JumpAllEnd = 28
		}

		// Token: 0x020003E6 RID: 998
		[EngineStruct("Agent_guard_mode", false)]
		public enum GuardMode
		{
			// Token: 0x04001718 RID: 5912
			None = -1,
			// Token: 0x04001719 RID: 5913
			Up,
			// Token: 0x0400171A RID: 5914
			Down,
			// Token: 0x0400171B RID: 5915
			Left,
			// Token: 0x0400171C RID: 5916
			Right
		}

		// Token: 0x020003E7 RID: 999
		public enum HandIndex
		{
			// Token: 0x0400171E RID: 5918
			MainHand,
			// Token: 0x0400171F RID: 5919
			OffHand
		}

		// Token: 0x020003E8 RID: 1000
		[EngineStruct("rglInt8", false)]
		public enum KillInfo : sbyte
		{
			// Token: 0x04001721 RID: 5921
			Invalid = -1,
			// Token: 0x04001722 RID: 5922
			Headshot,
			// Token: 0x04001723 RID: 5923
			CouchedLance,
			// Token: 0x04001724 RID: 5924
			Punch,
			// Token: 0x04001725 RID: 5925
			MountHit,
			// Token: 0x04001726 RID: 5926
			Bow,
			// Token: 0x04001727 RID: 5927
			Crossbow,
			// Token: 0x04001728 RID: 5928
			ThrowingAxe,
			// Token: 0x04001729 RID: 5929
			ThrowingKnife,
			// Token: 0x0400172A RID: 5930
			Javelin,
			// Token: 0x0400172B RID: 5931
			Stone,
			// Token: 0x0400172C RID: 5932
			Pistol,
			// Token: 0x0400172D RID: 5933
			Musket,
			// Token: 0x0400172E RID: 5934
			OneHandedSword,
			// Token: 0x0400172F RID: 5935
			TwoHandedSword,
			// Token: 0x04001730 RID: 5936
			OneHandedAxe,
			// Token: 0x04001731 RID: 5937
			TwoHandedAxe,
			// Token: 0x04001732 RID: 5938
			Mace,
			// Token: 0x04001733 RID: 5939
			Spear,
			// Token: 0x04001734 RID: 5940
			Morningstar,
			// Token: 0x04001735 RID: 5941
			Maul,
			// Token: 0x04001736 RID: 5942
			Backstabbed,
			// Token: 0x04001737 RID: 5943
			Gravity,
			// Token: 0x04001738 RID: 5944
			ShieldBash,
			// Token: 0x04001739 RID: 5945
			WeaponBash,
			// Token: 0x0400173A RID: 5946
			Kick,
			// Token: 0x0400173B RID: 5947
			TeamSwitch
		}

		// Token: 0x020003E9 RID: 1001
		public enum MovementBehaviorType
		{
			// Token: 0x0400173D RID: 5949
			Engaged,
			// Token: 0x0400173E RID: 5950
			Idle,
			// Token: 0x0400173F RID: 5951
			Flee
		}

		// Token: 0x020003EA RID: 1002
		[Flags]
		public enum MovementControlFlag : uint
		{
			// Token: 0x04001741 RID: 5953
			Forward = 1U,
			// Token: 0x04001742 RID: 5954
			Backward = 2U,
			// Token: 0x04001743 RID: 5955
			StrafeRight = 4U,
			// Token: 0x04001744 RID: 5956
			StrafeLeft = 8U,
			// Token: 0x04001745 RID: 5957
			TurnRight = 16U,
			// Token: 0x04001746 RID: 5958
			TurnLeft = 32U,
			// Token: 0x04001747 RID: 5959
			AttackLeft = 64U,
			// Token: 0x04001748 RID: 5960
			AttackRight = 128U,
			// Token: 0x04001749 RID: 5961
			AttackUp = 256U,
			// Token: 0x0400174A RID: 5962
			AttackDown = 512U,
			// Token: 0x0400174B RID: 5963
			DefendLeft = 1024U,
			// Token: 0x0400174C RID: 5964
			DefendRight = 2048U,
			// Token: 0x0400174D RID: 5965
			DefendUp = 4096U,
			// Token: 0x0400174E RID: 5966
			DefendDown = 8192U,
			// Token: 0x0400174F RID: 5967
			DefendAuto = 16384U,
			// Token: 0x04001750 RID: 5968
			DefendBlock = 32768U,
			// Token: 0x04001751 RID: 5969
			Action = 65536U,
			// Token: 0x04001752 RID: 5970
			AttackMask = 960U,
			// Token: 0x04001753 RID: 5971
			DefendMask = 31744U,
			// Token: 0x04001754 RID: 5972
			DefendDirMask = 15360U,
			// Token: 0x04001755 RID: 5973
			MoveMask = 63U
		}

		// Token: 0x020003EB RID: 1003
		public enum UnderAttackType
		{
			// Token: 0x04001757 RID: 5975
			NotUnderAttack,
			// Token: 0x04001758 RID: 5976
			UnderMeleeAttack,
			// Token: 0x04001759 RID: 5977
			UnderRangedAttack
		}

		// Token: 0x020003EC RID: 1004
		[EngineStruct("Usage_direction", false)]
		public enum UsageDirection
		{
			// Token: 0x0400175B RID: 5979
			None = -1,
			// Token: 0x0400175C RID: 5980
			AttackUp,
			// Token: 0x0400175D RID: 5981
			AttackDown,
			// Token: 0x0400175E RID: 5982
			AttackLeft,
			// Token: 0x0400175F RID: 5983
			AttackRight,
			// Token: 0x04001760 RID: 5984
			AttackBegin = 0,
			// Token: 0x04001761 RID: 5985
			AttackEnd = 4,
			// Token: 0x04001762 RID: 5986
			DefendUp = 4,
			// Token: 0x04001763 RID: 5987
			DefendDown,
			// Token: 0x04001764 RID: 5988
			DefendLeft,
			// Token: 0x04001765 RID: 5989
			DefendRight,
			// Token: 0x04001766 RID: 5990
			DefendBegin = 4,
			// Token: 0x04001767 RID: 5991
			DefendAny = 8,
			// Token: 0x04001768 RID: 5992
			DefendEnd,
			// Token: 0x04001769 RID: 5993
			AttackAny = 9
		}

		// Token: 0x020003ED RID: 1005
		[EngineStruct("Weapon_wield_action_type", false)]
		public enum WeaponWieldActionType
		{
			// Token: 0x0400176B RID: 5995
			WithAnimation,
			// Token: 0x0400176C RID: 5996
			Instant,
			// Token: 0x0400176D RID: 5997
			InstantAfterPickUp,
			// Token: 0x0400176E RID: 5998
			WithAnimationUninterruptible
		}

		// Token: 0x020003EE RID: 1006
		[Flags]
		public enum StopUsingGameObjectFlags : byte
		{
			// Token: 0x04001770 RID: 6000
			None = 0,
			// Token: 0x04001771 RID: 6001
			AutoAttachAfterStoppingUsingGameObject = 1,
			// Token: 0x04001772 RID: 6002
			DoNotWieldWeaponAfterStoppingUsingGameObject = 2,
			// Token: 0x04001773 RID: 6003
			DefendAfterStoppingUsingGameObject = 4
		}

		// Token: 0x020003EF RID: 1007
		// (Invoke) Token: 0x060033CC RID: 13260
		public delegate void OnAgentHealthChangedDelegate(Agent agent, float oldHealth, float newHealth);

		// Token: 0x020003F0 RID: 1008
		// (Invoke) Token: 0x060033D0 RID: 13264
		public delegate void OnMountHealthChangedDelegate(Agent agent, Agent mount, float oldHealth, float newHealth);

		// Token: 0x020003F1 RID: 1009
		// (Invoke) Token: 0x060033D4 RID: 13268
		public delegate void OnMainAgentWieldedItemChangeDelegate();
	}
}
