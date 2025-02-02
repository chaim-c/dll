using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200006F RID: 111
	[EngineClass("rglParticle_system_instanced")]
	public sealed class ParticleSystem : GameEntityComponent
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x00008A47 File Offset: 0x00006C47
		internal ParticleSystem(UIntPtr pointer) : base(pointer)
		{
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00008A50 File Offset: 0x00006C50
		public static ParticleSystem CreateParticleSystemAttachedToBone(string systemName, Skeleton skeleton, sbyte boneIndex, ref MatrixFrame boneLocalFrame)
		{
			return ParticleSystem.CreateParticleSystemAttachedToBone(ParticleSystemManager.GetRuntimeIdByName(systemName), skeleton, boneIndex, ref boneLocalFrame);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00008A60 File Offset: 0x00006C60
		public static ParticleSystem CreateParticleSystemAttachedToBone(int systemRuntimeId, Skeleton skeleton, sbyte boneIndex, ref MatrixFrame boneLocalFrame)
		{
			return EngineApplicationInterface.IParticleSystem.CreateParticleSystemAttachedToBone(systemRuntimeId, skeleton.Pointer, boneIndex, ref boneLocalFrame);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00008A75 File Offset: 0x00006C75
		public static ParticleSystem CreateParticleSystemAttachedToEntity(string systemName, GameEntity parentEntity, ref MatrixFrame boneLocalFrame)
		{
			return ParticleSystem.CreateParticleSystemAttachedToEntity(ParticleSystemManager.GetRuntimeIdByName(systemName), parentEntity, ref boneLocalFrame);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00008A84 File Offset: 0x00006C84
		public static ParticleSystem CreateParticleSystemAttachedToEntity(int systemRuntimeId, GameEntity parentEntity, ref MatrixFrame boneLocalFrame)
		{
			return EngineApplicationInterface.IParticleSystem.CreateParticleSystemAttachedToEntity(systemRuntimeId, parentEntity.Pointer, ref boneLocalFrame);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00008A98 File Offset: 0x00006C98
		public void AddMesh(Mesh mesh)
		{
			EngineApplicationInterface.IMetaMesh.AddMesh(base.Pointer, mesh.Pointer, 0U);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00008AB1 File Offset: 0x00006CB1
		public void SetEnable(bool enable)
		{
			EngineApplicationInterface.IParticleSystem.SetEnable(base.Pointer, enable);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00008AC4 File Offset: 0x00006CC4
		public void SetRuntimeEmissionRateMultiplier(float multiplier)
		{
			EngineApplicationInterface.IParticleSystem.SetRuntimeEmissionRateMultiplier(base.Pointer, multiplier);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00008AD7 File Offset: 0x00006CD7
		public void Restart()
		{
			EngineApplicationInterface.IParticleSystem.Restart(base.Pointer);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00008AE9 File Offset: 0x00006CE9
		public void SetLocalFrame(ref MatrixFrame newLocalFrame)
		{
			EngineApplicationInterface.IParticleSystem.SetLocalFrame(base.Pointer, ref newLocalFrame);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00008AFC File Offset: 0x00006CFC
		public MatrixFrame GetLocalFrame()
		{
			MatrixFrame identity = MatrixFrame.Identity;
			EngineApplicationInterface.IParticleSystem.GetLocalFrame(base.Pointer, ref identity);
			return identity;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00008B22 File Offset: 0x00006D22
		public void SetParticleEffectByName(string effectName)
		{
			EngineApplicationInterface.IParticleSystem.SetParticleEffectByName(base.Pointer, effectName);
		}
	}
}
