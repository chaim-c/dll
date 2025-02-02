using System;
using System.Diagnostics;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004B RID: 75
	public static class GameEntityPhysicsExtensions
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x00004ADA File Offset: 0x00002CDA
		[Conditional("_RGL_KEEP_ASSERTS")]
		private static void AssertSingleThreadRead()
		{
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00004ADC File Offset: 0x00002CDC
		[Conditional("_RGL_KEEP_ASSERTS")]
		private static void AssertSingleThreadWrite()
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00004ADE File Offset: 0x00002CDE
		[Conditional("_RGL_KEEP_ASSERTS")]
		private static void AssertMultiThreadRead()
		{
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00004AE0 File Offset: 0x00002CE0
		[Conditional("_RGL_KEEP_ASSERTS")]
		private static void AssertMultiThreadWrite()
		{
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00004AE2 File Offset: 0x00002CE2
		public static bool HasBody(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.HasBody(gameEntity.Pointer);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00004AF4 File Offset: 0x00002CF4
		public static void AddSphereAsBody(this GameEntity gameEntity, Vec3 sphere, float radius, BodyFlags bodyFlags)
		{
			EngineApplicationInterface.IGameEntity.AddSphereAsBody(gameEntity.Pointer, sphere, radius, (uint)bodyFlags);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00004B09 File Offset: 0x00002D09
		public static void RemovePhysics(this GameEntity gameEntity, bool clearingTheScene = false)
		{
			EngineApplicationInterface.IGameEntity.RemovePhysics(gameEntity.Pointer, clearingTheScene);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00004B1C File Offset: 0x00002D1C
		public static void RemovePhysicsMT(this GameEntity gameEntity, bool clearingTheScene = false)
		{
			EngineApplicationInterface.IGameEntity.RemovePhysics(gameEntity.Pointer, clearingTheScene);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00004B2F File Offset: 0x00002D2F
		public static bool GetPhysicsState(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetPhysicsState(gameEntity.Pointer);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00004B41 File Offset: 0x00002D41
		public static void AddDistanceJoint(this GameEntity gameEntity, GameEntity otherGameEntity, float minDistance, float maxDistance)
		{
			EngineApplicationInterface.IGameEntity.AddDistanceJoint(gameEntity.Pointer, otherGameEntity.Pointer, minDistance, maxDistance);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00004B5B File Offset: 0x00002D5B
		public static bool HasPhysicsDefinitionWithoutFlags(this GameEntity gameEntity, int excludeFlags)
		{
			return EngineApplicationInterface.IGameEntity.HasPhysicsDefinition(gameEntity.Pointer, excludeFlags);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00004B6E File Offset: 0x00002D6E
		public static void SetPhysicsState(this GameEntity gameEntity, bool isEnabled, bool setChildren)
		{
			EngineApplicationInterface.IGameEntity.SetPhysicsState(gameEntity.Pointer, isEnabled, setChildren);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00004B82 File Offset: 0x00002D82
		public static void RemoveEnginePhysics(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.RemoveEnginePhysics(gameEntity.Pointer);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00004B94 File Offset: 0x00002D94
		public static bool IsEngineBodySleeping(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsEngineBodySleeping(gameEntity.Pointer);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00004BA6 File Offset: 0x00002DA6
		public static bool IsDynamicBodyStationary(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsDynamicBodyStationary(gameEntity.Pointer);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public static bool IsDynamicBodyStationaryMT(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.IsDynamicBodyStationary(gameEntity.Pointer);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00004BCA File Offset: 0x00002DCA
		public static PhysicsShape GetBodyShape(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetBodyShape(gameEntity);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00004BD7 File Offset: 0x00002DD7
		public static void SetBodyShape(this GameEntity gameEntity, PhysicsShape shape)
		{
			EngineApplicationInterface.IGameEntity.SetBodyShape(gameEntity.Pointer, (shape == null) ? ((UIntPtr)0UL) : shape.Pointer);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00004C04 File Offset: 0x00002E04
		public static void AddPhysics(this GameEntity gameEntity, float mass, Vec3 localCenterOfMass, PhysicsShape body, Vec3 initialVelocity, Vec3 angularVelocity, PhysicsMaterial physicsMaterial, bool isStatic, int collisionGroupID)
		{
			EngineApplicationInterface.IGameEntity.AddPhysics(gameEntity.Pointer, (body != null) ? body.Pointer : UIntPtr.Zero, mass, ref localCenterOfMass, ref initialVelocity, ref angularVelocity, physicsMaterial.Index, isStatic, collisionGroupID);
			gameEntity.BodyFlag |= BodyFlags.Moveable;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00004C58 File Offset: 0x00002E58
		public static void ApplyLocalImpulseToDynamicBody(this GameEntity gameEntity, Vec3 localPosition, Vec3 impulse)
		{
			EngineApplicationInterface.IGameEntity.ApplyLocalImpulseToDynamicBody(gameEntity.Pointer, ref localPosition, ref impulse);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00004C6E File Offset: 0x00002E6E
		public static void ApplyForceToDynamicBody(this GameEntity gameEntity, Vec3 force)
		{
			EngineApplicationInterface.IGameEntity.ApplyForceToDynamicBody(gameEntity.Pointer, ref force);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00004C82 File Offset: 0x00002E82
		public static void ApplyLocalForceToDynamicBody(this GameEntity gameEntity, Vec3 localPosition, Vec3 force)
		{
			EngineApplicationInterface.IGameEntity.ApplyLocalForceToDynamicBody(gameEntity.Pointer, ref localPosition, ref force);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00004C98 File Offset: 0x00002E98
		public static void ApplyAccelerationToDynamicBody(this GameEntity gameEntity, Vec3 acceleration)
		{
			EngineApplicationInterface.IGameEntity.ApplyAccelerationToDynamicBody(gameEntity.Pointer, ref acceleration);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00004CAC File Offset: 0x00002EAC
		public static void DisableDynamicBodySimulation(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableDynamicBodySimulation(gameEntity.Pointer);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00004CBE File Offset: 0x00002EBE
		public static void DisableDynamicBodySimulationMT(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableDynamicBodySimulation(gameEntity.Pointer);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public static void EnableDynamicBody(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.EnableDynamicBody(gameEntity.Pointer);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00004CE2 File Offset: 0x00002EE2
		public static float GetMass(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetMass(gameEntity.Pointer);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public static void SetMass(this GameEntity gameEntity, float mass)
		{
			EngineApplicationInterface.IGameEntity.SetMass(gameEntity.Pointer, mass);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00004D07 File Offset: 0x00002F07
		public static void SetMassSpaceInertia(this GameEntity gameEntity, Vec3 inertia)
		{
			EngineApplicationInterface.IGameEntity.SetMassSpaceInertia(gameEntity.Pointer, ref inertia);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00004D1B File Offset: 0x00002F1B
		public static void SetDamping(this GameEntity gameEntity, float linearDamping, float angularDamping)
		{
			EngineApplicationInterface.IGameEntity.SetDamping(gameEntity.Pointer, linearDamping, angularDamping);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00004D2F File Offset: 0x00002F2F
		public static void DisableGravity(this GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.DisableGravity(gameEntity.Pointer);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00004D41 File Offset: 0x00002F41
		public static Vec3 GetLinearVelocity(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetLinearVelocity(gameEntity.Pointer);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00004D53 File Offset: 0x00002F53
		public static Vec3 GetLinearVelocityMT(this GameEntity gameEntity)
		{
			return EngineApplicationInterface.IGameEntity.GetLinearVelocity(gameEntity.Pointer);
		}
	}
}
