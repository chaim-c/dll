using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade.View.Screens;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000057 RID: 87
	public abstract class MissionView : MissionBehavior
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00020AFC File Offset: 0x0001ECFC
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00020B04 File Offset: 0x0001ED04
		public MissionScreen MissionScreen { get; internal set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00020B0D File Offset: 0x0001ED0D
		public IInputContext Input
		{
			get
			{
				return this.MissionScreen.SceneLayer.Input;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00020B1F File Offset: 0x0001ED1F
		public override MissionBehaviorType BehaviorType
		{
			get
			{
				return MissionBehaviorType.Other;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00020B22 File Offset: 0x0001ED22
		public virtual void OnMissionScreenTick(float dt)
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00020B24 File Offset: 0x0001ED24
		public virtual bool OnEscape()
		{
			return false;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00020B27 File Offset: 0x0001ED27
		public virtual bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			return true;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00020B2A File Offset: 0x0001ED2A
		public virtual void OnFocusChangeOnGameWindow(bool focusGained)
		{
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00020B2C File Offset: 0x0001ED2C
		public virtual void OnSceneRenderingStarted()
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00020B2E File Offset: 0x0001ED2E
		public virtual void OnMissionScreenInitialize()
		{
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00020B30 File Offset: 0x0001ED30
		public virtual void OnMissionScreenFinalize()
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00020B32 File Offset: 0x0001ED32
		public virtual void OnMissionScreenActivate()
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00020B34 File Offset: 0x0001ED34
		public virtual void OnMissionScreenDeactivate()
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00020B36 File Offset: 0x0001ED36
		public virtual bool UpdateOverridenCamera(float dt)
		{
			return false;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00020B39 File Offset: 0x0001ED39
		public virtual bool IsReady()
		{
			return true;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00020B3C File Offset: 0x0001ED3C
		public virtual void OnPhotoModeActivated()
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00020B3E File Offset: 0x0001ED3E
		public virtual void OnPhotoModeDeactivated()
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00020B40 File Offset: 0x0001ED40
		public virtual void OnConversationBegin()
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00020B42 File Offset: 0x0001ED42
		public virtual void OnConversationEnd()
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00020B44 File Offset: 0x0001ED44
		public virtual void OnInitialDeploymentPlanMadeForSide(BattleSideEnum side, bool isFirstPlan)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00020B46 File Offset: 0x0001ED46
		public sealed override void OnEndMissionInternal()
		{
			this.OnEndMission();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00020B4E File Offset: 0x0001ED4E
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
		}

		// Token: 0x04000287 RID: 647
		public int ViewOrderPriority;
	}
}
