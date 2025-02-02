using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200020D RID: 525
	public class FormationDeploymentPlan : IFormationDeploymentPlan
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00065E6E File Offset: 0x0006406E
		public FormationClass Class
		{
			get
			{
				return this._class;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x00065E76 File Offset: 0x00064076
		public FormationClass SpawnClass
		{
			get
			{
				return this._spawnClass;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00065E7E File Offset: 0x0006407E
		public float PlannedWidth
		{
			get
			{
				return this._plannedWidth;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x00065E86 File Offset: 0x00064086
		public float PlannedDepth
		{
			get
			{
				return this._plannedDepth;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00065E8E File Offset: 0x0006408E
		public int PlannedTroopCount
		{
			get
			{
				return this._plannedFootTroopCount + this._plannedMountedTroopCount;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x00065E9D File Offset: 0x0006409D
		public int PlannedFootTroopCount
		{
			get
			{
				return this._plannedFootTroopCount;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00065EA5 File Offset: 0x000640A5
		public int PlannedMountedTroopCount
		{
			get
			{
				return this._plannedMountedTroopCount;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x00065EAD File Offset: 0x000640AD
		public bool HasDimensions
		{
			get
			{
				return this._plannedWidth >= 1E-05f && this._plannedDepth >= 1E-05f;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00065ECE File Offset: 0x000640CE
		public bool HasSignificantMountedTroops
		{
			get
			{
				return MissionDeploymentPlan.HasSignificantMountedTroops(this._plannedFootTroopCount, this._plannedMountedTroopCount);
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00065EE1 File Offset: 0x000640E1
		public FormationDeploymentPlan(FormationClass fClass)
		{
			this._class = fClass;
			this._spawnClass = fClass;
			this.Clear();
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00065EFD File Offset: 0x000640FD
		public bool HasFrame()
		{
			return this._spawnFrame.IsValid;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00065F0C File Offset: 0x0006410C
		public FormationDeploymentFlank GetDefaultFlank(bool spawnWithHorses, int formationTroopCount, int infantryCount)
		{
			FormationDeploymentFlank result;
			if (!this._class.IsMounted() && formationTroopCount == 0)
			{
				result = FormationDeploymentFlank.Rear;
			}
			else if (this.HasSignificantMountedTroops && (!spawnWithHorses || infantryCount == 0))
			{
				if (formationTroopCount == 0 || this._class == FormationClass.LightCavalry || this._class == FormationClass.HorseArcher)
				{
					result = FormationDeploymentFlank.Rear;
				}
				else
				{
					result = FormationDeploymentFlank.Front;
				}
			}
			else
			{
				switch (this._class)
				{
				case FormationClass.Ranged:
				case FormationClass.NumberOfRegularFormations:
				case FormationClass.Bodyguard:
				case FormationClass.NumberOfAllFormations:
					return FormationDeploymentFlank.Rear;
				case FormationClass.Cavalry:
				case FormationClass.HeavyCavalry:
					return FormationDeploymentFlank.Left;
				case FormationClass.HorseArcher:
				case FormationClass.LightCavalry:
					return FormationDeploymentFlank.Right;
				}
				result = FormationDeploymentFlank.Front;
			}
			return result;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00065FA2 File Offset: 0x000641A2
		public FormationDeploymentOrder GetFlankDeploymentOrder(int offset = 0)
		{
			return FormationDeploymentOrder.GetDeploymentOrder(this._class, offset);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00065FB0 File Offset: 0x000641B0
		public MatrixFrame GetGroundFrame()
		{
			return this._spawnFrame.ToGroundMatrixFrame();
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00065FBD File Offset: 0x000641BD
		public Vec3 GetGroundPosition()
		{
			return this._spawnFrame.Origin.GetGroundVec3();
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00065FD0 File Offset: 0x000641D0
		public Vec2 GetDirection()
		{
			return this._spawnFrame.Rotation.f.AsVec2.Normalized();
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00065FFC File Offset: 0x000641FC
		public WorldPosition CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache worldPositionEnforcedCache)
		{
			if (worldPositionEnforcedCache == WorldPosition.WorldPositionEnforcedCache.NavMeshVec3)
			{
				return new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this._spawnFrame.Origin.GetNavMeshVec3(), false);
			}
			if (worldPositionEnforcedCache != WorldPosition.WorldPositionEnforcedCache.GroundVec3)
			{
				return this._spawnFrame.Origin;
			}
			return new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this._spawnFrame.Origin.GetGroundVec3(), false);
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0006606A File Offset: 0x0006426A
		public void Clear()
		{
			this._plannedWidth = 0f;
			this._plannedDepth = 0f;
			this._plannedFootTroopCount = 0;
			this._plannedMountedTroopCount = 0;
			this._spawnFrame = WorldFrame.Invalid;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0006609B File Offset: 0x0006429B
		public void SetPlannedTroopCount(int footTroopCount, int mountedTroopCount)
		{
			this._plannedFootTroopCount = footTroopCount;
			this._plannedMountedTroopCount = mountedTroopCount;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000660AB File Offset: 0x000642AB
		public void SetPlannedDimensions(float width, float depth)
		{
			this._plannedWidth = MathF.Max(0f, width);
			this._plannedDepth = MathF.Max(0f, depth);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000660CF File Offset: 0x000642CF
		public void SetFrame(WorldFrame frame)
		{
			this._spawnFrame = frame;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000660D8 File Offset: 0x000642D8
		public void SetSpawnClass(FormationClass spawnClass)
		{
			this._spawnClass = spawnClass;
		}

		// Token: 0x04000961 RID: 2401
		private WorldFrame _spawnFrame;

		// Token: 0x04000962 RID: 2402
		private FormationClass _spawnClass;

		// Token: 0x04000963 RID: 2403
		private readonly FormationClass _class;

		// Token: 0x04000964 RID: 2404
		private float _plannedWidth;

		// Token: 0x04000965 RID: 2405
		private float _plannedDepth;

		// Token: 0x04000966 RID: 2406
		private int _plannedFootTroopCount;

		// Token: 0x04000967 RID: 2407
		private int _plannedMountedTroopCount;
	}
}
