using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x020000BD RID: 189
	public sealed class SkeletonScale : MBObjectBase
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001FC08 File Offset: 0x0001DE08
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x0001FC10 File Offset: 0x0001DE10
		public string SkeletonModel { get; private set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001FC19 File Offset: 0x0001DE19
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0001FC21 File Offset: 0x0001DE21
		public Vec3 MountSitBoneScale { get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0001FC2A File Offset: 0x0001DE2A
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0001FC32 File Offset: 0x0001DE32
		public float MountRadiusAdder { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001FC3B File Offset: 0x0001DE3B
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0001FC43 File Offset: 0x0001DE43
		public Vec3[] Scales { get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001FC4C File Offset: 0x0001DE4C
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0001FC54 File Offset: 0x0001DE54
		public List<string> BoneNames { get; private set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0001FC5D File Offset: 0x0001DE5D
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0001FC65 File Offset: 0x0001DE65
		public sbyte[] BoneIndices { get; private set; }

		// Token: 0x0600099F RID: 2463 RVA: 0x0001FC6E File Offset: 0x0001DE6E
		public SkeletonScale()
		{
			this.BoneNames = null;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001FC80 File Offset: 0x0001DE80
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			this.SkeletonModel = node.Attributes["skeleton"].InnerText;
			XmlAttribute xmlAttribute = node.Attributes["mount_sit_bone_scale"];
			Vec3 mountSitBoneScale = new Vec3(1f, 1f, 1f, -1f);
			if (xmlAttribute != null)
			{
				string[] array = xmlAttribute.Value.Split(new char[]
				{
					','
				});
				if (array.Length == 3)
				{
					float.TryParse(array[0], out mountSitBoneScale.x);
					float.TryParse(array[1], out mountSitBoneScale.y);
					float.TryParse(array[2], out mountSitBoneScale.z);
				}
			}
			this.MountSitBoneScale = mountSitBoneScale;
			XmlAttribute xmlAttribute2 = node.Attributes["mount_radius_adder"];
			if (xmlAttribute2 != null)
			{
				this.MountRadiusAdder = float.Parse(xmlAttribute2.Value);
			}
			this.BoneNames = new List<string>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = xmlNode.Name;
				if (name == "BoneScales")
				{
					List<Vec3> list = new List<Vec3>();
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Attributes != null)
						{
							name = xmlNode2.Name;
							if (name == "BoneScale")
							{
								XmlAttribute xmlAttribute3 = xmlNode2.Attributes["scale"];
								Vec3 item = default(Vec3);
								if (xmlAttribute3 != null)
								{
									string[] array2 = xmlAttribute3.Value.Split(new char[]
									{
										','
									});
									if (array2.Length == 3)
									{
										float.TryParse(array2[0], out item.x);
										float.TryParse(array2[1], out item.y);
										float.TryParse(array2[2], out item.z);
									}
								}
								this.BoneNames.Add(xmlNode2.Attributes["bone_name"].InnerText);
								list.Add(item);
							}
						}
					}
					this.Scales = list.ToArray();
				}
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001FF10 File Offset: 0x0001E110
		public void SetBoneIndices(sbyte[] boneIndices)
		{
			this.BoneIndices = boneIndices;
			this.BoneNames = null;
		}
	}
}
