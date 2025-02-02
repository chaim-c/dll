using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x0200000B RID: 11
	public class CraftedDataView
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003A07 File Offset: 0x00001C07
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00003A0F File Offset: 0x00001C0F
		public WeaponDesign CraftedData { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003A18 File Offset: 0x00001C18
		public MetaMesh WeaponMesh
		{
			get
			{
				if (!(this._weaponMesh != null) || !this._weaponMesh.HasVertexBufferOrEditDataOrPackageItem())
				{
					return this._weaponMesh = this.GenerateWeaponMesh(true);
				}
				return this._weaponMesh;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003A58 File Offset: 0x00001C58
		public MetaMesh HolsterMesh
		{
			get
			{
				MetaMesh result;
				if ((result = this._holsterMesh) == null)
				{
					result = (this._holsterMesh = this.GenerateHolsterMesh());
				}
				return result;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003A80 File Offset: 0x00001C80
		public MetaMesh HolsterMeshWithWeapon
		{
			get
			{
				if (!(this._holsterMeshWithWeapon != null) || !this._holsterMeshWithWeapon.HasVertexBufferOrEditDataOrPackageItem())
				{
					return this._holsterMeshWithWeapon = this.GenerateHolsterMeshWithWeapon(true);
				}
				return this._holsterMeshWithWeapon;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public MetaMesh NonBatchedWeaponMesh
		{
			get
			{
				MetaMesh result;
				if ((result = this._nonBatchedWeaponMesh) == null)
				{
					result = (this._nonBatchedWeaponMesh = this.GenerateWeaponMesh(false));
				}
				return result;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public MetaMesh NonBatchedHolsterMesh
		{
			get
			{
				MetaMesh result;
				if ((result = this._nonBatchedHolsterMesh) == null)
				{
					result = (this._nonBatchedHolsterMesh = this.GenerateHolsterMesh());
				}
				return result;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003B10 File Offset: 0x00001D10
		public MetaMesh NonBatchedHolsterMeshWithWeapon
		{
			get
			{
				MetaMesh result;
				if ((result = this._nonBatchedHolsterMeshWithWeapon) == null)
				{
					result = (this._nonBatchedHolsterMeshWithWeapon = this.GenerateHolsterMeshWithWeapon(false));
				}
				return result;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003B37 File Offset: 0x00001D37
		public CraftedDataView(WeaponDesign craftedData)
		{
			this.CraftedData = craftedData;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003B46 File Offset: 0x00001D46
		public void Clear()
		{
			this._weaponMesh = null;
			this._holsterMesh = null;
			this._holsterMeshWithWeapon = null;
			this._nonBatchedWeaponMesh = null;
			this._nonBatchedHolsterMesh = null;
			this._nonBatchedHolsterMeshWithWeapon = null;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003B72 File Offset: 0x00001D72
		private MetaMesh GenerateWeaponMesh(bool batchMeshes)
		{
			if (this.CraftedData.UsedPieces != null)
			{
				return CraftedDataView.BuildWeaponMesh(this.CraftedData, 0f, false, batchMeshes);
			}
			return null;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003B95 File Offset: 0x00001D95
		private MetaMesh GenerateHolsterMesh()
		{
			if (this.CraftedData.UsedPieces != null)
			{
				return CraftedDataView.BuildHolsterMesh(this.CraftedData);
			}
			return null;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003BB1 File Offset: 0x00001DB1
		private MetaMesh GenerateHolsterMeshWithWeapon(bool batchMeshes)
		{
			if (this.CraftedData.UsedPieces != null)
			{
				return CraftedDataView.BuildHolsterMeshWithWeapon(this.CraftedData, 0f, batchMeshes);
			}
			return null;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public static MetaMesh BuildWeaponMesh(WeaponDesign craftedData, float pivotDiff, bool pieceTypeHidingEnabledForHolster, bool batchAllMeshes)
		{
			CraftingTemplate template = craftedData.Template;
			MetaMesh metaMesh = MetaMesh.CreateMetaMesh(null);
			List<MetaMesh> list = new List<MetaMesh>();
			List<MetaMesh> list2 = new List<MetaMesh>();
			List<MetaMesh> list3 = new List<MetaMesh>();
			foreach (PieceData pieceData in template.BuildOrders)
			{
				if (!pieceTypeHidingEnabledForHolster || !template.IsPieceTypeHiddenOnHolster(pieceData.PieceType))
				{
					WeaponDesignElement weaponDesignElement = craftedData.UsedPieces[(int)pieceData.PieceType];
					float f = craftedData.PiecePivotDistances[(int)pieceData.PieceType];
					if (weaponDesignElement != null && weaponDesignElement.IsValid && !float.IsNaN(f))
					{
						MetaMesh copy = MetaMesh.GetCopy(weaponDesignElement.CraftingPiece.MeshName, true, false);
						if (!batchAllMeshes)
						{
							copy.ClearMeshesForOtherLods(0);
						}
						MatrixFrame frame = new MatrixFrame(Mat3.Identity, f * Vec3.Up);
						if (weaponDesignElement.IsPieceScaled)
						{
							Vec3 scalingVector = weaponDesignElement.CraftingPiece.FullScale ? (Vec3.One * weaponDesignElement.ScaleFactor) : new Vec3(1f, 1f, weaponDesignElement.ScaleFactor, -1f);
							frame.Scale(scalingVector);
						}
						copy.Frame = frame;
						if (copy.HasClothData())
						{
							list3.Add(copy);
						}
						else
						{
							list2.Add(copy);
						}
					}
				}
			}
			foreach (MetaMesh metaMesh2 in list2)
			{
				if (batchAllMeshes)
				{
					list.Add(metaMesh2);
				}
				else
				{
					metaMesh.MergeMultiMeshes(metaMesh2);
				}
			}
			if (batchAllMeshes)
			{
				metaMesh.BatchMultiMeshesMultiple(list);
			}
			foreach (MetaMesh metaMesh3 in list3)
			{
				metaMesh.MergeMultiMeshes(metaMesh3);
				metaMesh.AssignClothBodyFrom(metaMesh3);
			}
			metaMesh.SetEditDataPolicy(EditDataPolicy.Keep_until_first_render);
			if (batchAllMeshes)
			{
				metaMesh.SetLodBias(1);
			}
			MatrixFrame frame2 = metaMesh.Frame;
			frame2.Elevate(pivotDiff);
			metaMesh.Frame = frame2;
			return metaMesh;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003E00 File Offset: 0x00002000
		public static MetaMesh BuildHolsterMesh(WeaponDesign craftedData)
		{
			if (craftedData.Template.UseWeaponAsHolsterMesh)
			{
				return null;
			}
			BladeData bladeData = craftedData.UsedPieces[0].CraftingPiece.BladeData;
			if (craftedData.Template.AlwaysShowHolsterWithWeapon || string.IsNullOrEmpty(bladeData.HolsterMeshName))
			{
				return null;
			}
			float z = craftedData.PiecePivotDistances[0];
			MetaMesh copy = MetaMesh.GetCopy(bladeData.HolsterMeshName, false, false);
			MatrixFrame frame = copy.Frame;
			frame.origin += new Vec3(0f, 0f, z, -1f);
			WeaponDesignElement weaponDesignElement = craftedData.UsedPieces[0];
			if (MathF.Abs(weaponDesignElement.ScaledLength - weaponDesignElement.CraftingPiece.Length) > 1E-05f)
			{
				Vec3 scalingVector = weaponDesignElement.CraftingPiece.FullScale ? (Vec3.One * weaponDesignElement.ScaleFactor) : new Vec3(1f, 1f, weaponDesignElement.ScaleFactor, -1f);
				frame.Scale(scalingVector);
			}
			copy.Frame = frame;
			MetaMesh metaMesh = MetaMesh.CreateMetaMesh(bladeData.HolsterMeshName);
			metaMesh.MergeMultiMeshes(copy);
			return metaMesh;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003F20 File Offset: 0x00002120
		public static MetaMesh BuildHolsterMeshWithWeapon(WeaponDesign craftedData, float pivotDiff, bool batchAllMeshes)
		{
			if (craftedData.Template.UseWeaponAsHolsterMesh)
			{
				return null;
			}
			WeaponDesignElement weaponDesignElement = craftedData.UsedPieces[0];
			BladeData bladeData = weaponDesignElement.CraftingPiece.BladeData;
			if (string.IsNullOrEmpty(bladeData.HolsterMeshName))
			{
				return null;
			}
			MetaMesh metaMesh = MetaMesh.CreateMetaMesh(null);
			MetaMesh copy = MetaMesh.GetCopy(bladeData.HolsterMeshName, false, true);
			string text = bladeData.HolsterMeshName + "_skeleton";
			if (Skeleton.SkeletonModelExist(text))
			{
				MetaMesh metaMesh2 = CraftedDataView.BuildWeaponMesh(craftedData, 0f, true, batchAllMeshes);
				float num = craftedData.PiecePivotDistances[0];
				float scaledDistanceToPreviousPiece = craftedData.UsedPieces[0].ScaledDistanceToPreviousPiece;
				float num2 = num - scaledDistanceToPreviousPiece;
				List<MetaMesh> list = new List<MetaMesh>();
				Skeleton skeleton = Skeleton.CreateFromModel(text);
				for (sbyte b = 1; b < skeleton.GetBoneCount(); b += 1)
				{
					MatrixFrame boneEntitialRestFrame = skeleton.GetBoneEntitialRestFrame(b, false);
					if (craftedData.Template.RotateWeaponInHolster)
					{
						boneEntitialRestFrame.rotation.RotateAboutForward(3.1415927f);
					}
					MetaMesh metaMesh3 = metaMesh2.CreateCopy();
					MatrixFrame frame = new MatrixFrame(boneEntitialRestFrame.rotation, boneEntitialRestFrame.origin);
					frame.Elevate(-num2);
					metaMesh3.Frame = frame;
					if (batchAllMeshes)
					{
						int num3 = (int)(8 - (b - 1));
						metaMesh3.SetMaterial(Material.GetFromResource("weapon_crafting_quiver_deformer"));
						metaMesh3.SetFactor1Linear((uint)(419430400L * (long)num3));
						list.Add(metaMesh3);
					}
					else
					{
						metaMesh.MergeMultiMeshes(metaMesh3);
					}
				}
				if (list.Count > 0)
				{
					metaMesh.BatchMultiMeshesMultiple(list);
				}
				if (craftedData.Template.PieceTypeToScaleHolsterWith != CraftingPiece.PieceTypes.Invalid)
				{
					WeaponDesignElement weaponDesignElement2 = craftedData.UsedPieces[(int)craftedData.Template.PieceTypeToScaleHolsterWith];
					MatrixFrame frame2 = copy.Frame;
					int num4 = -MathF.Sign(skeleton.GetBoneEntitialRestFrame(0, false).rotation.u.z);
					float num5 = weaponDesignElement.CraftingPiece.BladeData.HolsterMeshLength * (weaponDesignElement2.ScaleFactor - 1f) * 0.5f * (float)num4;
					WeaponDesignElement weaponDesignElement3 = craftedData.UsedPieces[(int)craftedData.Template.PieceTypeToScaleHolsterWith];
					if (weaponDesignElement3.IsPieceScaled)
					{
						Vec3 scalingVector = weaponDesignElement3.CraftingPiece.FullScale ? (Vec3.One * weaponDesignElement3.ScaleFactor) : new Vec3(1f, 1f, weaponDesignElement3.ScaleFactor, -1f);
						frame2.Scale(scalingVector);
					}
					frame2.origin += new Vec3(0f, 0f, -num5, -1f);
					copy.Frame = frame2;
				}
			}
			else
			{
				if (craftedData.Template.PieceTypeToScaleHolsterWith != CraftingPiece.PieceTypes.Invalid)
				{
					MatrixFrame frame3 = copy.Frame;
					frame3.origin += new Vec3(0f, 0f, craftedData.PiecePivotDistances[(int)craftedData.Template.PieceTypeToScaleHolsterWith], -1f);
					WeaponDesignElement weaponDesignElement4 = craftedData.UsedPieces[(int)craftedData.Template.PieceTypeToScaleHolsterWith];
					if (weaponDesignElement4.IsPieceScaled)
					{
						Vec3 scalingVector2 = weaponDesignElement4.CraftingPiece.FullScale ? (Vec3.One * weaponDesignElement4.ScaleFactor) : new Vec3(1f, 1f, weaponDesignElement4.ScaleFactor, -1f);
						frame3.Scale(scalingVector2);
					}
					copy.Frame = frame3;
				}
				metaMesh.MergeMultiMeshes(CraftedDataView.BuildWeaponMesh(craftedData, 0f, true, batchAllMeshes));
			}
			metaMesh.MergeMultiMeshes(copy);
			MatrixFrame frame4 = metaMesh.Frame;
			frame4.origin += new Vec3(0f, 0f, pivotDiff, -1f);
			metaMesh.Frame = frame4;
			return metaMesh;
		}

		// Token: 0x0400000F RID: 15
		private MetaMesh _weaponMesh;

		// Token: 0x04000010 RID: 16
		private MetaMesh _holsterMesh;

		// Token: 0x04000011 RID: 17
		private MetaMesh _holsterMeshWithWeapon;

		// Token: 0x04000012 RID: 18
		private MetaMesh _nonBatchedWeaponMesh;

		// Token: 0x04000013 RID: 19
		private MetaMesh _nonBatchedHolsterMesh;

		// Token: 0x04000014 RID: 20
		private MetaMesh _nonBatchedHolsterMeshWithWeapon;
	}
}
