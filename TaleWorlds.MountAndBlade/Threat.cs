using System;
using System.Diagnostics;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000188 RID: 392
	public class Threat
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x0004CD8C File Offset: 0x0004AF8C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0004CD94 File Offset: 0x0004AF94
		public string Name
		{
			get
			{
				if (this.WeaponEntity != null)
				{
					return this.WeaponEntity.Entity().Name;
				}
				if (this.Agent != null)
				{
					return this.Agent.Name.ToString();
				}
				if (this.Formation != null)
				{
					return this.Formation.ToString();
				}
				Debug.FailedAssert("Invalid threat", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Threat.cs", "Name", 38);
				return "Invalid";
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0004CE04 File Offset: 0x0004B004
		public Vec3 Position
		{
			get
			{
				if (this.WeaponEntity != null)
				{
					return (this.WeaponEntity.GetTargetEntity().PhysicsGlobalBoxMax + this.WeaponEntity.GetTargetEntity().PhysicsGlobalBoxMin) * 0.5f + this.WeaponEntity.GetTargetingOffset();
				}
				if (this.Agent != null)
				{
					return this.Agent.CollisionCapsuleCenter;
				}
				if (this.Formation != null)
				{
					return this.Formation.GetMedianAgent(false, false, this.Formation.GetAveragePositionOfUnits(false, false)).Position;
				}
				Debug.FailedAssert("Invalid threat", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Threat.cs", "Position", 62);
				return Vec3.Invalid;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0004CEB0 File Offset: 0x0004B0B0
		public Vec3 BoundingBoxMin
		{
			get
			{
				if (this.WeaponEntity != null)
				{
					return this.WeaponEntity.GetTargetEntity().PhysicsGlobalBoxMin + this.WeaponEntity.GetTargetingOffset();
				}
				if (this.Agent != null)
				{
					return this.Agent.CollisionCapsule.GetBoxMin();
				}
				if (this.Formation != null)
				{
					Debug.FailedAssert("Nobody should be requesting a bounding box for a formation", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Threat.cs", "BoundingBoxMin", 82);
					return Vec3.Invalid;
				}
				return Vec3.Invalid;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0004CF2C File Offset: 0x0004B12C
		public Vec3 BoundingBoxMax
		{
			get
			{
				if (this.WeaponEntity != null)
				{
					return this.WeaponEntity.GetTargetEntity().PhysicsGlobalBoxMax + this.WeaponEntity.GetTargetingOffset();
				}
				if (this.Agent != null)
				{
					return this.Agent.CollisionCapsule.GetBoxMax();
				}
				if (this.Formation != null)
				{
					Debug.FailedAssert("Nobody should be requesting a bounding box for a formation", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Threat.cs", "BoundingBoxMax", 106);
					return Vec3.Invalid;
				}
				return Vec3.Invalid;
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
		public Vec3 GetVelocity()
		{
			if (this.WeaponEntity != null)
			{
				Vec3 zero = Vec3.Zero;
				IMoveableSiegeWeapon moveableSiegeWeapon = this.WeaponEntity as IMoveableSiegeWeapon;
				if (moveableSiegeWeapon != null)
				{
					return moveableSiegeWeapon.MovementComponent.Velocity;
				}
			}
			return Vec3.Zero;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0004CFE4 File Offset: 0x0004B1E4
		public override bool Equals(object obj)
		{
			Threat threat;
			return (threat = (obj as Threat)) != null && this.WeaponEntity == threat.WeaponEntity && this.Formation == threat.Formation;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0004D01B File Offset: 0x0004B21B
		[Conditional("DEBUG")]
		public void DisplayDebugInfo()
		{
		}

		// Token: 0x040005B4 RID: 1460
		public ITargetable WeaponEntity;

		// Token: 0x040005B5 RID: 1461
		public Formation Formation;

		// Token: 0x040005B6 RID: 1462
		public Agent Agent;

		// Token: 0x040005B7 RID: 1463
		public float ThreatValue;
	}
}
