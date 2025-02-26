﻿using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x0200000D RID: 13
	public static class CraftingPieceCollectionElementViewExtensions
	{
		// Token: 0x0600005A RID: 90 RVA: 0x0000437C File Offset: 0x0000257C
		public static MatrixFrame GetCraftingPieceFrameForInventory(this CraftingPiece craftingPiece)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			Mat3 identity2 = Mat3.Identity;
			float num = 0.85f;
			Vec3 v = new Vec3(0f, 0f, 0f, -1f);
			MetaMesh copy = MetaMesh.GetCopy(craftingPiece.MeshName, true, false);
			if (copy != null)
			{
				identity2.RotateAboutSide(-1.5707964f);
				identity2.RotateAboutForward(-0.7853982f);
				Vec3 vec = new Vec3(1000000f, 1000000f, 1000000f, -1f);
				Vec3 vec2 = new Vec3(-1000000f, -1000000f, -1000000f, -1f);
				for (int num2 = 0; num2 != copy.MeshCount; num2++)
				{
					Vec3 boundingBoxMin = copy.GetMeshAtIndex(num2).GetBoundingBoxMin();
					Vec3 boundingBoxMax = copy.GetMeshAtIndex(num2).GetBoundingBoxMax();
					Vec3[] array = new Vec3[]
					{
						identity2.TransformToParent(new Vec3(boundingBoxMin.x, boundingBoxMin.y, boundingBoxMin.z, -1f)),
						identity2.TransformToParent(new Vec3(boundingBoxMin.x, boundingBoxMin.y, boundingBoxMax.z, -1f)),
						identity2.TransformToParent(new Vec3(boundingBoxMin.x, boundingBoxMax.y, boundingBoxMin.z, -1f)),
						identity2.TransformToParent(new Vec3(boundingBoxMin.x, boundingBoxMax.y, boundingBoxMax.z, -1f)),
						identity2.TransformToParent(new Vec3(boundingBoxMax.x, boundingBoxMin.y, boundingBoxMin.z, -1f)),
						identity2.TransformToParent(new Vec3(boundingBoxMax.x, boundingBoxMin.y, boundingBoxMax.z, -1f)),
						identity2.TransformToParent(new Vec3(boundingBoxMax.x, boundingBoxMax.y, boundingBoxMin.z, -1f)),
						identity2.TransformToParent(new Vec3(boundingBoxMax.x, boundingBoxMax.y, boundingBoxMax.z, -1f))
					};
					for (int i = 0; i < 8; i++)
					{
						vec = Vec3.Vec3Min(vec, array[i]);
						vec2 = Vec3.Vec3Max(vec2, array[i]);
					}
				}
				float num3 = 1f;
				Vec3 v2 = (vec + vec2) * 0.5f;
				float num4 = MathF.Max(vec2.x - vec.x, vec2.y - vec.y);
				float num5 = num * num3 / num4;
				identity.origin -= v2 * num5;
				identity.origin += v;
				identity.rotation = identity2;
				identity.rotation.ApplyScaleLocal(num5);
				identity.origin.z = identity.origin.z - 5f;
			}
			return identity;
		}
	}
}
