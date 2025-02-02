using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade.View.Scripts
{
	// Token: 0x02000041 RID: 65
	public class MapColorGradeManager : ScriptComponentBehavior
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0001A894 File Offset: 0x00018A94
		private void Init()
		{
			if (base.Scene.ContainsTerrain)
			{
				Vec2i vec2i;
				float num;
				int num2;
				int num3;
				base.Scene.GetTerrainData(out vec2i, out num, out num2, out num3);
				this.terrainSize.x = (float)vec2i.X * num;
				this.terrainSize.y = (float)vec2i.Y * num;
			}
			this.colorGradeGridMapping.Add(1, this.defaultColorGradeTextureName);
			this.colorGradeGridMapping.Add(2, "worldmap_colorgrade_night");
			this.ReadColorGradesXml();
			MBMapScene.GetColorGradeGridData(base.Scene, this.colorGradeGrid, this.colorGradeGridName);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001A929 File Offset: 0x00018B29
		protected override void OnInit()
		{
			base.OnInit();
			this.Init();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001A937 File Offset: 0x00018B37
		protected override void OnEditorInit()
		{
			base.OnEditorInit();
			this.Init();
			this.TimeOfDay = base.Scene.TimeOfDay;
			this.lastSceneTimeOfDay = this.TimeOfDay;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001A962 File Offset: 0x00018B62
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001A965 File Offset: 0x00018B65
		protected override void OnTick(float dt)
		{
			this.TimeOfDay = base.Scene.TimeOfDay;
			this.SeasonTimeFactor = MBMapScene.GetSeasonTimeFactor(base.Scene);
			this.ApplyAtmosphere(false);
			this.ApplyColorGrade(dt);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001A998 File Offset: 0x00018B98
		protected override void OnEditorTick(float dt)
		{
			if (base.Scene.TimeOfDay != this.lastSceneTimeOfDay)
			{
				this.TimeOfDay = base.Scene.TimeOfDay;
				this.lastSceneTimeOfDay = this.TimeOfDay;
			}
			if (base.Scene.ContainsTerrain)
			{
				Vec2i vec2i;
				float num;
				int num2;
				int num3;
				base.Scene.GetTerrainData(out vec2i, out num, out num2, out num3);
				this.terrainSize.x = (float)vec2i.X * num;
				this.terrainSize.y = (float)vec2i.Y * num;
			}
			else
			{
				this.terrainSize.x = 1f;
				this.terrainSize.y = 1f;
			}
			if (this.AtmosphereSimulationEnabled)
			{
				this.TimeOfDay += dt;
				if (this.TimeOfDay >= 24f)
				{
					this.TimeOfDay -= 24f;
				}
				this.ApplyAtmosphere(false);
			}
			if (this.ColorGradeEnabled)
			{
				this.ApplyColorGrade(dt);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001AA8C File Offset: 0x00018C8C
		protected override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "ColorGradeEnabled")
			{
				if (!this.ColorGradeEnabled)
				{
					base.Scene.SetColorGradeBlend("", "", -1f);
					this.lastColorGrade = 0;
					return;
				}
			}
			else
			{
				if (variableName == "TimeOfDay")
				{
					this.ApplyAtmosphere(false);
					return;
				}
				if (variableName == "SeasonTimeFactor")
				{
					this.ApplyAtmosphere(false);
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001AB00 File Offset: 0x00018D00
		private void ReadColorGradesXml()
		{
			List<string> list;
			XmlDocument mergedXmlForNative = MBObjectManager.GetMergedXmlForNative("soln_worldmap_color_grades", out list);
			if (mergedXmlForNative == null)
			{
				return;
			}
			XmlNode xmlNode = mergedXmlForNative.SelectSingleNode("worldmap_color_grades");
			if (xmlNode == null)
			{
				return;
			}
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("color_grade_grid");
			if (xmlNode2 != null && xmlNode2.Attributes["name"] != null)
			{
				this.colorGradeGridName = xmlNode2.Attributes["name"].Value;
			}
			XmlNode xmlNode3 = xmlNode.SelectSingleNode("color_grade_default");
			if (xmlNode3 != null && xmlNode3.Attributes["name"] != null)
			{
				this.defaultColorGradeTextureName = xmlNode3.Attributes["name"].Value;
				this.colorGradeGridMapping[1] = this.defaultColorGradeTextureName;
			}
			XmlNode xmlNode4 = xmlNode.SelectSingleNode("color_grade_night");
			if (xmlNode4 != null && xmlNode4.Attributes["name"] != null)
			{
				this.colorGradeGridMapping[2] = xmlNode4.Attributes["name"].Value;
			}
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("color_grade");
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode5 = (XmlNode)obj;
					byte key;
					if (xmlNode5.Attributes["name"] != null && xmlNode5.Attributes["value"] != null && byte.TryParse(xmlNode5.Attributes["value"].Value, out key))
					{
						this.colorGradeGridMapping[key] = xmlNode5.Attributes["name"].Value;
					}
				}
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001ACC8 File Offset: 0x00018EC8
		public void ApplyAtmosphere(bool forceLoadTextures)
		{
			this.TimeOfDay = MBMath.ClampFloat(this.TimeOfDay, 0f, 23.99f);
			this.SeasonTimeFactor = MBMath.ClampFloat(this.SeasonTimeFactor, 0f, 1f);
			MBMapScene.SetFrameForAtmosphere(base.Scene, this.TimeOfDay * 10f, base.Scene.LastFinalRenderCameraFrame.origin.z, forceLoadTextures);
			float valueFrom = 0.55f;
			float valueTo = -0.1f;
			float seasonTimeFactor = this.SeasonTimeFactor;
			Vec3 dynamic_params = new Vec3(0f, 0.65f, 0f, -1f);
			dynamic_params.x = MBMath.Lerp(valueFrom, valueTo, seasonTimeFactor, 1E-05f);
			MBMapScene.SetTerrainDynamicParams(base.Scene, dynamic_params);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001AD88 File Offset: 0x00018F88
		public void ApplyColorGrade(float dt)
		{
			Vec3 origin = base.Scene.LastFinalRenderCameraFrame.origin;
			float num = 1f;
			int num2 = MathF.Floor(origin.x / this.terrainSize.X * 512f);
			int num3 = MathF.Floor(origin.y / this.terrainSize.Y * 512f);
			num2 = MBMath.ClampIndex(num2, 0, 512);
			num3 = MBMath.ClampIndex(num3, 0, 512);
			byte b = this.colorGradeGrid[num3 * 512 + num2];
			if (origin.z > 400f)
			{
				b = 1;
			}
			if (this.TimeOfDay > 22f || this.TimeOfDay < 2f)
			{
				b = 2;
			}
			if (MBMapScene.GetApplyRainColorGrade() && origin.z < 50f)
			{
				b = 160;
				num = 0.2f;
			}
			if (this.lastColorGrade != b)
			{
				string color = "";
				string color2 = "";
				if (!this.colorGradeGridMapping.TryGetValue(this.lastColorGrade, out color))
				{
					color = this.defaultColorGradeTextureName;
				}
				if (!this.colorGradeGridMapping.TryGetValue(b, out color2))
				{
					color2 = this.defaultColorGradeTextureName;
				}
				if (this.primaryTransitionRecord == null)
				{
					this.primaryTransitionRecord = new MapColorGradeManager.ColorGradeBlendRecord
					{
						color1 = color,
						color2 = color2,
						alpha = 0f
					};
				}
				else
				{
					this.secondaryTransitionRecord = new MapColorGradeManager.ColorGradeBlendRecord
					{
						color1 = this.primaryTransitionRecord.color2,
						color2 = color2,
						alpha = 0f
					};
				}
				this.lastColorGrade = b;
			}
			if (this.primaryTransitionRecord != null)
			{
				if (this.primaryTransitionRecord.alpha < 1f)
				{
					this.primaryTransitionRecord.alpha = MathF.Min(this.primaryTransitionRecord.alpha + dt * (1f / num), 1f);
					base.Scene.SetColorGradeBlend(this.primaryTransitionRecord.color1, this.primaryTransitionRecord.color2, this.primaryTransitionRecord.alpha);
					return;
				}
				this.primaryTransitionRecord = null;
				if (this.secondaryTransitionRecord != null)
				{
					this.primaryTransitionRecord = new MapColorGradeManager.ColorGradeBlendRecord(this.secondaryTransitionRecord);
					this.secondaryTransitionRecord = null;
				}
			}
		}

		// Token: 0x0400021D RID: 541
		public bool ColorGradeEnabled;

		// Token: 0x0400021E RID: 542
		public bool AtmosphereSimulationEnabled;

		// Token: 0x0400021F RID: 543
		public float TimeOfDay;

		// Token: 0x04000220 RID: 544
		public float SeasonTimeFactor;

		// Token: 0x04000221 RID: 545
		private string colorGradeGridName = "worldmap_colorgrade_grid";

		// Token: 0x04000222 RID: 546
		private const int colorGradeGridSize = 262144;

		// Token: 0x04000223 RID: 547
		private byte[] colorGradeGrid = new byte[262144];

		// Token: 0x04000224 RID: 548
		private Dictionary<byte, string> colorGradeGridMapping = new Dictionary<byte, string>();

		// Token: 0x04000225 RID: 549
		private MapColorGradeManager.ColorGradeBlendRecord primaryTransitionRecord;

		// Token: 0x04000226 RID: 550
		private MapColorGradeManager.ColorGradeBlendRecord secondaryTransitionRecord;

		// Token: 0x04000227 RID: 551
		private byte lastColorGrade;

		// Token: 0x04000228 RID: 552
		private Vec2 terrainSize = new Vec2(1f, 1f);

		// Token: 0x04000229 RID: 553
		private string defaultColorGradeTextureName = "worldmap_colorgrade_stratosphere";

		// Token: 0x0400022A RID: 554
		private const float transitionSpeedFactor = 1f;

		// Token: 0x0400022B RID: 555
		private float lastSceneTimeOfDay;

		// Token: 0x020000A6 RID: 166
		private class ColorGradeBlendRecord
		{
			// Token: 0x060004F1 RID: 1265 RVA: 0x00026698 File Offset: 0x00024898
			public ColorGradeBlendRecord()
			{
				this.color1 = "";
				this.color2 = "";
				this.alpha = 0f;
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x000266C1 File Offset: 0x000248C1
			public ColorGradeBlendRecord(MapColorGradeManager.ColorGradeBlendRecord other)
			{
				this.color1 = other.color1;
				this.color2 = other.color2;
				this.alpha = other.alpha;
			}

			// Token: 0x04000373 RID: 883
			public string color1;

			// Token: 0x04000374 RID: 884
			public string color2;

			// Token: 0x04000375 RID: 885
			public float alpha;
		}
	}
}
