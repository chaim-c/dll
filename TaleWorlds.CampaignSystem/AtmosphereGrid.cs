﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000020 RID: 32
	public class AtmosphereGrid
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000E94A File Offset: 0x0000CB4A
		public void Initialize()
		{
			this.states = Campaign.Current.MapSceneWrapper.GetAtmosphereStates().ToList<AtmosphereState>();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000E968 File Offset: 0x0000CB68
		public AtmosphereState GetInterpolatedStateInfo(Vec3 pos)
		{
			AtmosphereGrid.<>c__DisplayClass3_0 CS$<>8__locals1 = new AtmosphereGrid.<>c__DisplayClass3_0();
			CS$<>8__locals1.pos = pos;
			List<AtmosphereGrid.AtmosphereStateSortData> list = new List<AtmosphereGrid.AtmosphereStateSortData>();
			int num = 0;
			foreach (AtmosphereState atmosphereState in this.states)
			{
				list.Add(new AtmosphereGrid.AtmosphereStateSortData
				{
					Position = atmosphereState.Position,
					InitialIndex = num++
				});
			}
			AtmosphereGrid.<>c__DisplayClass3_0 CS$<>8__locals2 = CS$<>8__locals1;
			CS$<>8__locals2.pos.z = CS$<>8__locals2.pos.z * 0.3f;
			list.Sort((AtmosphereGrid.AtmosphereStateSortData x, AtmosphereGrid.AtmosphereStateSortData y) => x.Position.Distance(CS$<>8__locals1.pos).CompareTo(y.Position.Distance(CS$<>8__locals1.pos)));
			AtmosphereState atmosphereState2 = new AtmosphereState();
			float num2 = 0f;
			bool flag = true;
			string colorGradeTexture = "color_grade_empire_harsh";
			atmosphereState2.ColorGradeTexture = colorGradeTexture;
			foreach (AtmosphereGrid.AtmosphereStateSortData atmosphereStateSortData in list)
			{
				AtmosphereState atmosphereState3 = this.states[atmosphereStateSortData.InitialIndex];
				float value = atmosphereState3.Position.Distance(CS$<>8__locals1.pos);
				float num3 = 1f - MBMath.SmoothStep(atmosphereState3.distanceForMaxWeight, atmosphereState3.distanceForMinWeight, value);
				if ((double)num3 >= 0.001)
				{
					if (flag)
					{
						colorGradeTexture = atmosphereState3.ColorGradeTexture;
					}
					atmosphereState2.HumidityAverage += atmosphereState3.HumidityAverage * num3;
					atmosphereState2.HumidityVariance += atmosphereState3.HumidityVariance * num3;
					atmosphereState2.TemperatureAverage += atmosphereState3.TemperatureAverage * num3;
					atmosphereState2.TemperatureVariance += atmosphereState3.TemperatureVariance * num3;
					num2 += num3;
					flag = false;
				}
			}
			if (num2 > 0f)
			{
				atmosphereState2.ColorGradeTexture = colorGradeTexture;
				atmosphereState2.HumidityAverage /= num2;
				atmosphereState2.HumidityVariance /= num2;
				atmosphereState2.TemperatureAverage /= num2;
				atmosphereState2.TemperatureVariance /= num2;
			}
			return atmosphereState2;
		}

		// Token: 0x04000027 RID: 39
		private List<AtmosphereState> states = new List<AtmosphereState>();

		// Token: 0x02000479 RID: 1145
		private struct AtmosphereStateSortData
		{
			// Token: 0x04001373 RID: 4979
			public Vec3 Position;

			// Token: 0x04001374 RID: 4980
			public int InitialIndex;
		}
	}
}
