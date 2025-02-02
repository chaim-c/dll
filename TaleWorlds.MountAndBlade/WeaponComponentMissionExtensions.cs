using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000245 RID: 581
	public static class WeaponComponentMissionExtensions
	{
		// Token: 0x06001F60 RID: 8032 RVA: 0x0006F1DD File Offset: 0x0006D3DD
		public static int GetItemUsageIndex(this WeaponComponentData weaponComponentData)
		{
			return MBItem.GetItemUsageIndex(weaponComponentData.ItemUsage);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0006F1EA File Offset: 0x0006D3EA
		public static Vec3 GetWeaponCenterOfMass(this PhysicsShape body)
		{
			return WeaponComponentMissionExtensions.CalculateCenterOfMass(body);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0006F1F4 File Offset: 0x0006D3F4
		[MBCallback]
		internal static Vec3 CalculateCenterOfMass(PhysicsShape body)
		{
			if (body == null)
			{
				Debug.FailedAssert("Item has no body! Check this!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\ItemCollectionElementMissionExtensions.cs", "CalculateCenterOfMass", 46);
				return Vec3.Zero;
			}
			Vec3 vec = Vec3.Zero;
			float num = 0f;
			int num2 = body.CapsuleCount();
			for (int i = 0; i < num2; i++)
			{
				CapsuleData capsuleData = default(CapsuleData);
				body.GetCapsule(ref capsuleData, i);
				Vec3 v = (capsuleData.P1 + capsuleData.P2) * 0.5f;
				float num3 = capsuleData.P1.Distance(capsuleData.P2);
				float num4 = capsuleData.Radius * capsuleData.Radius * 3.1415927f * (1.3333334f * capsuleData.Radius + num3);
				num += num4;
				vec += v * num4;
			}
			int num5 = body.SphereCount();
			for (int j = 0; j < num5; j++)
			{
				SphereData sphereData = default(SphereData);
				body.GetSphere(ref sphereData, j);
				float num6 = 4.1887903f * sphereData.Radius * sphereData.Radius * sphereData.Radius;
				num += num6;
				vec += sphereData.Origin * num6;
			}
			if (num > 0f)
			{
				vec /= num;
				if (MathF.Abs(vec.x) < 0.01f)
				{
					vec.x = 0f;
				}
				if (MathF.Abs(vec.y) < 0.01f)
				{
					vec.y = 0f;
				}
				if (MathF.Abs(vec.z) < 0.01f)
				{
					vec.z = 0f;
				}
			}
			else
			{
				vec = body.GetBoundingBoxCenter();
			}
			return vec;
		}
	}
}
