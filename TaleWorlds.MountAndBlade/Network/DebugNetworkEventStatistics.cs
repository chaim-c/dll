using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Network
{
	// Token: 0x0200039B RID: 923
	public static class DebugNetworkEventStatistics
	{
		// Token: 0x1400009A RID: 154
		// (add) Token: 0x060031D4 RID: 12756 RVA: 0x000CDB70 File Offset: 0x000CBD70
		// (remove) Token: 0x060031D5 RID: 12757 RVA: 0x000CDBA4 File Offset: 0x000CBDA4
		public static event Action<IEnumerable<DebugNetworkEventStatistics.TotalEventData>> OnEventDataUpdated;

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x060031D6 RID: 12758 RVA: 0x000CDBD8 File Offset: 0x000CBDD8
		// (remove) Token: 0x060031D7 RID: 12759 RVA: 0x000CDC0C File Offset: 0x000CBE0C
		public static event Action<DebugNetworkEventStatistics.PerSecondEventData> OnPerSecondEventDataUpdated;

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x060031D8 RID: 12760 RVA: 0x000CDC40 File Offset: 0x000CBE40
		// (remove) Token: 0x060031D9 RID: 12761 RVA: 0x000CDC74 File Offset: 0x000CBE74
		public static event Action<IEnumerable<float>> OnFPSEventUpdated;

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x060031DA RID: 12762 RVA: 0x000CDCA8 File Offset: 0x000CBEA8
		// (remove) Token: 0x060031DB RID: 12763 RVA: 0x000CDCDC File Offset: 0x000CBEDC
		public static event Action OnOpenExternalMonitor;

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000CDD0F File Offset: 0x000CBF0F
		// (set) Token: 0x060031DD RID: 12765 RVA: 0x000CDD16 File Offset: 0x000CBF16
		public static int SamplesPerSecond
		{
			get
			{
				return DebugNetworkEventStatistics._samplesPerSecond;
			}
			set
			{
				DebugNetworkEventStatistics._samplesPerSecond = value;
				DebugNetworkEventStatistics.MaxGraphPointCount = value * 5;
			}
		} = 10;

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000CDD26 File Offset: 0x000CBF26
		// (set) Token: 0x060031DF RID: 12767 RVA: 0x000CDD2D File Offset: 0x000CBF2D
		public static bool IsActive { get; private set; }

		// Token: 0x060031E1 RID: 12769 RVA: 0x000CDE00 File Offset: 0x000CC000
		internal static void StartEvent(string eventName, int eventType)
		{
			if (!DebugNetworkEventStatistics.IsActive)
			{
				return;
			}
			DebugNetworkEventStatistics._curEventType = eventType;
			if (!DebugNetworkEventStatistics._statistics.ContainsKey(DebugNetworkEventStatistics._curEventType))
			{
				DebugNetworkEventStatistics._statistics.Add(DebugNetworkEventStatistics._curEventType, new DebugNetworkEventStatistics.PerEventData
				{
					Name = eventName
				});
			}
			DebugNetworkEventStatistics._statistics[DebugNetworkEventStatistics._curEventType].Count++;
			DebugNetworkEventStatistics._totalData.TotalCount++;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000CDE78 File Offset: 0x000CC078
		internal static void EndEvent()
		{
			if (!DebugNetworkEventStatistics.IsActive)
			{
				return;
			}
			DebugNetworkEventStatistics.PerEventData perEventData = DebugNetworkEventStatistics._statistics[DebugNetworkEventStatistics._curEventType];
			perEventData.DataSize = perEventData.TotalDataSize / perEventData.Count;
			DebugNetworkEventStatistics._curEventType = -1;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000CDEB6 File Offset: 0x000CC0B6
		internal static void AddDataToStatistic(int bitCount)
		{
			if (!DebugNetworkEventStatistics.IsActive)
			{
				return;
			}
			DebugNetworkEventStatistics._statistics[DebugNetworkEventStatistics._curEventType].TotalDataSize += bitCount;
			DebugNetworkEventStatistics._totalData.TotalDataSize += bitCount;
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000CDEEE File Offset: 0x000CC0EE
		public static void OpenExternalMonitor()
		{
			if (DebugNetworkEventStatistics.OnOpenExternalMonitor != null)
			{
				DebugNetworkEventStatistics.OnOpenExternalMonitor();
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000CDF01 File Offset: 0x000CC101
		public static void ControlActivate()
		{
			DebugNetworkEventStatistics.IsActive = true;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000CDF09 File Offset: 0x000CC109
		public static void ControlDeactivate()
		{
			DebugNetworkEventStatistics.IsActive = false;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000CDF11 File Offset: 0x000CC111
		public static void ControlJustDump()
		{
			DebugNetworkEventStatistics.DumpData();
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000CDF18 File Offset: 0x000CC118
		public static void ControlDumpAll()
		{
			DebugNetworkEventStatistics.DumpData();
			DebugNetworkEventStatistics.DumpReplicationData();
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000CDF24 File Offset: 0x000CC124
		public static void ControlClear()
		{
			DebugNetworkEventStatistics.Clear();
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000CDF2B File Offset: 0x000CC12B
		public static void ClearNetGraphs()
		{
			DebugNetworkEventStatistics._eventSamples.Clear();
			DebugNetworkEventStatistics._lossSamples.Clear();
			DebugNetworkEventStatistics._prevEventData = new DebugNetworkEventStatistics.TotalEventData();
			DebugNetworkEventStatistics._currEventData = new DebugNetworkEventStatistics.TotalEventData();
			DebugNetworkEventStatistics._collectSampleCheck = 0f;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000CDF5F File Offset: 0x000CC15F
		public static void ClearFpsGraph()
		{
			DebugNetworkEventStatistics._fpsSamplesUntilNextSampling.Clear();
			DebugNetworkEventStatistics._fpsSamples.Clear();
			DebugNetworkEventStatistics._collectFpsSampleCheck = 0f;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000CDF7F File Offset: 0x000CC17F
		public static void ControlClearAll()
		{
			DebugNetworkEventStatistics.Clear();
			DebugNetworkEventStatistics.ClearFpsGraph();
			DebugNetworkEventStatistics.ClearNetGraphs();
			DebugNetworkEventStatistics.ClearReplicationData();
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000CDF95 File Offset: 0x000CC195
		public static void ControlDumpReplicationData()
		{
			DebugNetworkEventStatistics.DumpReplicationData();
			DebugNetworkEventStatistics.ClearReplicationData();
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000CDFA4 File Offset: 0x000CC1A4
		public static void EndTick(float dt)
		{
			if (DebugNetworkEventStatistics._useImgui && Input.DebugInput.IsHotKeyPressed("DebugNetworkEventStatisticsHotkeyToggleActive"))
			{
				DebugNetworkEventStatistics.ToggleActive();
				if (DebugNetworkEventStatistics.IsActive)
				{
					Imgui.NewFrame();
				}
			}
			if (!DebugNetworkEventStatistics.IsActive)
			{
				return;
			}
			DebugNetworkEventStatistics._totalData.TotalTime += dt;
			DebugNetworkEventStatistics._totalData.TotalFrameCount++;
			if (DebugNetworkEventStatistics._useImgui)
			{
				Imgui.BeginMainThreadScope();
				Imgui.Begin("Network panel");
				if (Imgui.Button("Disable Network Panel"))
				{
					DebugNetworkEventStatistics.ToggleActive();
				}
				Imgui.Separator();
				if (Imgui.Button("Show Upload Data (screen)"))
				{
					DebugNetworkEventStatistics._showUploadDataText = !DebugNetworkEventStatistics._showUploadDataText;
				}
				Imgui.Separator();
				if (Imgui.Button("Clear Data"))
				{
					DebugNetworkEventStatistics.Clear();
				}
				if (Imgui.Button("Dump Data (console)"))
				{
					DebugNetworkEventStatistics.DumpData();
				}
				Imgui.Separator();
				if (Imgui.Button("Clear Replication Data"))
				{
					DebugNetworkEventStatistics.ClearReplicationData();
				}
				if (Imgui.Button("Dump Replication Data (console)"))
				{
					DebugNetworkEventStatistics.DumpReplicationData();
				}
				if (Imgui.Button("Dump & Clear Replication Data (console)"))
				{
					DebugNetworkEventStatistics.DumpReplicationData();
					DebugNetworkEventStatistics.ClearReplicationData();
				}
				if (DebugNetworkEventStatistics._showUploadDataText)
				{
					Imgui.Separator();
					DebugNetworkEventStatistics.ShowUploadData();
				}
				Imgui.End();
			}
			if (!DebugNetworkEventStatistics.IsActive)
			{
				return;
			}
			DebugNetworkEventStatistics.CollectFpsSample(dt);
			DebugNetworkEventStatistics._collectSampleCheck += dt;
			if (DebugNetworkEventStatistics._collectSampleCheck >= 1f / (float)DebugNetworkEventStatistics.SamplesPerSecond)
			{
				DebugNetworkEventStatistics._currEventData = DebugNetworkEventStatistics.GetCurrentEventData();
				if (DebugNetworkEventStatistics._currEventData.HasData && DebugNetworkEventStatistics._prevEventData.HasData && DebugNetworkEventStatistics._currEventData != DebugNetworkEventStatistics._prevEventData)
				{
					DebugNetworkEventStatistics._lossSamples.Enqueue(GameNetwork.GetAveragePacketLossRatio());
					DebugNetworkEventStatistics._eventSamples.Enqueue(DebugNetworkEventStatistics._currEventData - DebugNetworkEventStatistics._prevEventData);
					DebugNetworkEventStatistics._prevEventData = DebugNetworkEventStatistics._currEventData;
					if (DebugNetworkEventStatistics._eventSamples.Count > DebugNetworkEventStatistics.MaxGraphPointCount)
					{
						DebugNetworkEventStatistics._eventSamples.Dequeue();
						DebugNetworkEventStatistics._lossSamples.Dequeue();
					}
					if (DebugNetworkEventStatistics._eventSamples.Count >= DebugNetworkEventStatistics.SamplesPerSecond)
					{
						List<DebugNetworkEventStatistics.TotalEventData> range = DebugNetworkEventStatistics._eventSamples.ToList<DebugNetworkEventStatistics.TotalEventData>().GetRange(DebugNetworkEventStatistics._eventSamples.Count - DebugNetworkEventStatistics.SamplesPerSecond, DebugNetworkEventStatistics.SamplesPerSecond);
						DebugNetworkEventStatistics.UploadPerSecondEventData = new DebugNetworkEventStatistics.PerSecondEventData(range.Sum((DebugNetworkEventStatistics.TotalEventData x) => x.TotalUpload), range.Sum((DebugNetworkEventStatistics.TotalEventData x) => x.TotalConstantsUpload), range.Sum((DebugNetworkEventStatistics.TotalEventData x) => x.TotalReliableUpload), range.Sum((DebugNetworkEventStatistics.TotalEventData x) => x.TotalReplicationUpload), range.Sum((DebugNetworkEventStatistics.TotalEventData x) => x.TotalUnreliableUpload), range.Sum((DebugNetworkEventStatistics.TotalEventData x) => x.TotalOtherUpload));
						if (DebugNetworkEventStatistics.OnPerSecondEventDataUpdated != null)
						{
							DebugNetworkEventStatistics.OnPerSecondEventDataUpdated(DebugNetworkEventStatistics.UploadPerSecondEventData);
						}
					}
					if (DebugNetworkEventStatistics.OnEventDataUpdated != null)
					{
						DebugNetworkEventStatistics.OnEventDataUpdated(DebugNetworkEventStatistics._eventSamples.ToList<DebugNetworkEventStatistics.TotalEventData>());
					}
					DebugNetworkEventStatistics._collectSampleCheck -= 1f / (float)DebugNetworkEventStatistics.SamplesPerSecond;
				}
			}
			if (DebugNetworkEventStatistics._useImgui)
			{
				Imgui.Begin("Network Graph panel");
				float[] array = (from x in DebugNetworkEventStatistics._eventSamples
				select (float)x.TotalUpload / 8192f).ToArray<float>();
				float num = (array.Length != 0) ? array.Max() : 0f;
				DebugNetworkEventStatistics._targetMaxGraphHeight = (DebugNetworkEventStatistics._useAbsoluteMaximum ? MathF.Max(num, DebugNetworkEventStatistics._targetMaxGraphHeight) : num);
				float amount = MBMath.ClampFloat(3f * dt, 0f, 1f);
				DebugNetworkEventStatistics._curMaxGraphHeight = MBMath.Lerp(DebugNetworkEventStatistics._curMaxGraphHeight, DebugNetworkEventStatistics._targetMaxGraphHeight, amount, 1E-05f);
				if (DebugNetworkEventStatistics.UploadPerSecondEventData != null)
				{
					Imgui.Text(string.Concat(new object[]
					{
						"Taking ",
						DebugNetworkEventStatistics.SamplesPerSecond,
						" samples per second. Total KiB per second:",
						(float)DebugNetworkEventStatistics.UploadPerSecondEventData.TotalUploadPerSecond / 8192f
					}));
				}
				float[] values = (from x in DebugNetworkEventStatistics._eventSamples
				select (float)x.TotalConstantsUpload / 8192f).ToArray<float>();
				Imgui.PlotLines("", values, DebugNetworkEventStatistics._eventSamples.Count, 0, "Constants upload (in KiB)", 0f, DebugNetworkEventStatistics._curMaxGraphHeight, 400f, 45f, 4);
				Imgui.SameLine(0f, 0f);
				Imgui.Text("Y-range: " + DebugNetworkEventStatistics._curMaxGraphHeight);
				float[] values2 = (from x in DebugNetworkEventStatistics._eventSamples
				select (float)x.TotalReliableUpload / 8192f).ToArray<float>();
				Imgui.PlotLines("", values2, DebugNetworkEventStatistics._eventSamples.Count, 0, "Reliable upload (in KiB)", 0f, DebugNetworkEventStatistics._curMaxGraphHeight, 400f, 45f, 4);
				Imgui.SameLine(0f, 0f);
				Imgui.Text("Y-range: " + DebugNetworkEventStatistics._curMaxGraphHeight);
				float[] values3 = (from x in DebugNetworkEventStatistics._eventSamples
				select (float)x.TotalReplicationUpload / 8192f).ToArray<float>();
				Imgui.PlotLines("", values3, DebugNetworkEventStatistics._eventSamples.Count, 0, "Replication upload (in KiB)", 0f, DebugNetworkEventStatistics._curMaxGraphHeight, 400f, 45f, 4);
				Imgui.SameLine(0f, 0f);
				Imgui.Text("Y-range: " + DebugNetworkEventStatistics._curMaxGraphHeight);
				float[] values4 = (from x in DebugNetworkEventStatistics._eventSamples
				select (float)x.TotalUnreliableUpload / 8192f).ToArray<float>();
				Imgui.PlotLines("", values4, DebugNetworkEventStatistics._eventSamples.Count, 0, "Unreliable upload (in KiB)", 0f, DebugNetworkEventStatistics._curMaxGraphHeight, 400f, 45f, 4);
				Imgui.SameLine(0f, 0f);
				Imgui.Text("Y-range: " + DebugNetworkEventStatistics._curMaxGraphHeight);
				float[] values5 = (from x in DebugNetworkEventStatistics._eventSamples
				select (float)x.TotalOtherUpload / 8192f).ToArray<float>();
				Imgui.PlotLines("", values5, DebugNetworkEventStatistics._eventSamples.Count, 0, "Other upload (in KiB)", 0f, DebugNetworkEventStatistics._curMaxGraphHeight, 400f, 45f, 4);
				Imgui.SameLine(0f, 0f);
				Imgui.Text("Y-range: " + DebugNetworkEventStatistics._curMaxGraphHeight);
				Imgui.Separator();
				float[] values6 = (from x in DebugNetworkEventStatistics._eventSamples
				select (float)x.TotalUpload / (float)x.TotalPackets / 8f).ToArray<float>();
				Imgui.PlotLines("", values6, DebugNetworkEventStatistics._eventSamples.Count, 0, "Data per package (in B)", 0f, 1400f, 400f, 45f, 4);
				Imgui.SameLine(0f, 0f);
				Imgui.Text("Y-range: " + 1400);
				Imgui.Separator();
				float num2 = (DebugNetworkEventStatistics._lossSamples.Count > 0) ? DebugNetworkEventStatistics._lossSamples.Max() : 0f;
				DebugNetworkEventStatistics._targetMaxLossGraphHeight = (DebugNetworkEventStatistics._useAbsoluteMaximum ? MathF.Max(num2, DebugNetworkEventStatistics._targetMaxLossGraphHeight) : num2);
				float amount2 = MBMath.ClampFloat(3f * dt, 0f, 1f);
				DebugNetworkEventStatistics._currMaxLossGraphHeight = MBMath.Lerp(DebugNetworkEventStatistics._currMaxLossGraphHeight, DebugNetworkEventStatistics._targetMaxLossGraphHeight, amount2, 1E-05f);
				Imgui.PlotLines("", DebugNetworkEventStatistics._lossSamples.ToArray(), DebugNetworkEventStatistics._lossSamples.Count, 0, "Averaged loss ratio", 0f, DebugNetworkEventStatistics._currMaxLossGraphHeight, 400f, 45f, 4);
				Imgui.SameLine(0f, 0f);
				Imgui.Text("Y-range: " + DebugNetworkEventStatistics._currMaxLossGraphHeight);
				Imgui.Checkbox("Use absolute Maximum", ref DebugNetworkEventStatistics._useAbsoluteMaximum);
				Imgui.End();
			}
			Imgui.EndMainThreadScope();
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000CE810 File Offset: 0x000CCA10
		private static void CollectFpsSample(float dt)
		{
			if (DebugNetworkEventStatistics.TrackFps)
			{
				float fps = Utilities.GetFps();
				if (!float.IsInfinity(fps) && !float.IsNegativeInfinity(fps) && !float.IsNaN(fps))
				{
					DebugNetworkEventStatistics._fpsSamplesUntilNextSampling.Add(fps);
				}
				DebugNetworkEventStatistics._collectFpsSampleCheck += dt;
				if (DebugNetworkEventStatistics._collectFpsSampleCheck >= 1f / (float)DebugNetworkEventStatistics.SamplesPerSecond)
				{
					if (DebugNetworkEventStatistics._fpsSamplesUntilNextSampling.Count > 0)
					{
						DebugNetworkEventStatistics._fpsSamples.Enqueue(DebugNetworkEventStatistics._fpsSamplesUntilNextSampling.Min());
						DebugNetworkEventStatistics._fpsSamplesUntilNextSampling.Clear();
						if (DebugNetworkEventStatistics._fpsSamples.Count > DebugNetworkEventStatistics.MaxGraphPointCount)
						{
							DebugNetworkEventStatistics._fpsSamples.Dequeue();
						}
						Action<IEnumerable<float>> onFPSEventUpdated = DebugNetworkEventStatistics.OnFPSEventUpdated;
						if (onFPSEventUpdated != null)
						{
							onFPSEventUpdated(DebugNetworkEventStatistics._fpsSamples.ToList<float>());
						}
					}
					DebugNetworkEventStatistics._collectFpsSampleCheck -= 1f / (float)DebugNetworkEventStatistics.SamplesPerSecond;
				}
			}
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000CE8E7 File Offset: 0x000CCAE7
		private static void ToggleActive()
		{
			DebugNetworkEventStatistics.IsActive = !DebugNetworkEventStatistics.IsActive;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000CE8F6 File Offset: 0x000CCAF6
		private static void Clear()
		{
			DebugNetworkEventStatistics._totalData = new DebugNetworkEventStatistics.TotalData();
			DebugNetworkEventStatistics._statistics = new Dictionary<int, DebugNetworkEventStatistics.PerEventData>();
			GameNetwork.ResetDebugUploads();
			DebugNetworkEventStatistics._curEventType = -1;
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000CE918 File Offset: 0x000CCB18
		private static void DumpData()
		{
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "DumpData");
			mbstringBuilder.AppendLine();
			mbstringBuilder.AppendLine<string>("///GENERAL DATA///");
			mbstringBuilder.AppendLine<string>("Total elapsed time: " + DebugNetworkEventStatistics._totalData.TotalTime + " seconds.");
			mbstringBuilder.AppendLine<string>("Total frame count: " + DebugNetworkEventStatistics._totalData.TotalFrameCount);
			mbstringBuilder.AppendLine<string>("Total avg packet count: " + (int)(DebugNetworkEventStatistics._totalData.TotalTime / 60f));
			mbstringBuilder.AppendLine<string>("Total event data size: " + DebugNetworkEventStatistics._totalData.TotalDataSize + " bits.");
			mbstringBuilder.AppendLine<string>("Total event count: " + DebugNetworkEventStatistics._totalData.TotalCount);
			mbstringBuilder.AppendLine();
			mbstringBuilder.AppendLine<string>("///ALL DATA///");
			List<DebugNetworkEventStatistics.PerEventData> list = new List<DebugNetworkEventStatistics.PerEventData>();
			list.AddRange(DebugNetworkEventStatistics._statistics.Values);
			list.Sort();
			foreach (DebugNetworkEventStatistics.PerEventData perEventData in list)
			{
				mbstringBuilder.AppendLine<string>("Event name: " + perEventData.Name);
				mbstringBuilder.AppendLine<string>("\tEvent size (for one event): " + perEventData.DataSize + " bits.");
				mbstringBuilder.AppendLine<string>("\tTotal count: " + perEventData.Count);
				mbstringBuilder.AppendLine<string>(string.Concat(new object[]
				{
					"\tTotal size: ",
					perEventData.TotalDataSize,
					"bits | ~",
					perEventData.TotalDataSize / 8 + ((perEventData.TotalDataSize % 8 == 0) ? 0 : 1),
					" bytes."
				}));
				mbstringBuilder.AppendLine<string>("\tTotal count per frame: " + (float)perEventData.Count / (float)DebugNetworkEventStatistics._totalData.TotalFrameCount);
				mbstringBuilder.AppendLine<string>("\tTotal size per frame: " + (float)perEventData.TotalDataSize / (float)DebugNetworkEventStatistics._totalData.TotalFrameCount + " bits per frame.");
				mbstringBuilder.AppendLine();
			}
			DebugNetworkEventStatistics.GetFormattedDebugUploadDataOutput(ref mbstringBuilder);
			mbstringBuilder.AppendLine<string>("NetworkEventStaticticsLogLength: " + mbstringBuilder.Length + "\n");
			MBDebug.Print(mbstringBuilder.ToStringAndRelease(), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000CEBD4 File Offset: 0x000CCDD4
		private static void GetFormattedDebugUploadDataOutput(ref MBStringBuilder outStr)
		{
			GameNetwork.DebugNetworkPacketStatisticsStruct debugNetworkPacketStatisticsStruct = default(GameNetwork.DebugNetworkPacketStatisticsStruct);
			GameNetwork.DebugNetworkPositionCompressionStatisticsStruct debugNetworkPositionCompressionStatisticsStruct = default(GameNetwork.DebugNetworkPositionCompressionStatisticsStruct);
			GameNetwork.GetDebugUploadsInBits(ref debugNetworkPacketStatisticsStruct, ref debugNetworkPositionCompressionStatisticsStruct);
			outStr.AppendLine<string>("REAL NETWORK UPLOAD PERCENTS");
			if (debugNetworkPacketStatisticsStruct.TotalUpload == 0)
			{
				outStr.AppendLine<string>("Total Upload is ZERO");
				return;
			}
			int num = debugNetworkPacketStatisticsStruct.TotalUpload - (debugNetworkPacketStatisticsStruct.TotalConstantsUpload + debugNetworkPacketStatisticsStruct.TotalReliableEventUpload + debugNetworkPacketStatisticsStruct.TotalReplicationUpload + debugNetworkPacketStatisticsStruct.TotalUnreliableEventUpload);
			if (num == debugNetworkPacketStatisticsStruct.TotalUpload)
			{
				outStr.AppendLine<string>("USE_DEBUG_NETWORK_PACKET_PERCENTS not defined!");
			}
			else
			{
				outStr.AppendLine<string>("\tAverage Ping: " + debugNetworkPacketStatisticsStruct.AveragePingTime);
				outStr.AppendLine<string>("\tTime out period: " + debugNetworkPacketStatisticsStruct.TimeOutPeriod);
				outStr.AppendLine<string>("\tLost Percent: " + debugNetworkPacketStatisticsStruct.LostPercent);
				outStr.AppendLine<string>("\tlost_count: " + debugNetworkPacketStatisticsStruct.LostCount);
				outStr.AppendLine<string>("\ttotal_count_on_lost_check: " + debugNetworkPacketStatisticsStruct.TotalCountOnLostCheck);
				outStr.AppendLine<string>("\tround_trip_time: " + debugNetworkPacketStatisticsStruct.RoundTripTime);
				float num2 = (float)debugNetworkPacketStatisticsStruct.TotalUpload;
				float num3 = 1f / (float)debugNetworkPacketStatisticsStruct.TotalPackets;
				outStr.AppendLine<string>(string.Concat(new object[]
				{
					"\tConstants Upload: percent: ",
					(float)debugNetworkPacketStatisticsStruct.TotalConstantsUpload / num2 * 100f,
					"; size in bits: ",
					(float)debugNetworkPacketStatisticsStruct.TotalConstantsUpload * num3,
					";"
				}));
				outStr.AppendLine<string>(string.Concat(new object[]
				{
					"\tReliable Upload: percent: ",
					(float)debugNetworkPacketStatisticsStruct.TotalReliableEventUpload / num2 * 100f,
					"; size in bits: ",
					(float)debugNetworkPacketStatisticsStruct.TotalReliableEventUpload * num3,
					";"
				}));
				outStr.AppendLine<string>(string.Concat(new object[]
				{
					"\tReplication Upload: percent: ",
					(float)debugNetworkPacketStatisticsStruct.TotalReplicationUpload / num2 * 100f,
					"; size in bits: ",
					(float)debugNetworkPacketStatisticsStruct.TotalReplicationUpload * num3,
					";"
				}));
				outStr.AppendLine<string>(string.Concat(new object[]
				{
					"\tUnreliable Upload: percent: ",
					(float)debugNetworkPacketStatisticsStruct.TotalUnreliableEventUpload / num2 * 100f,
					"; size in bits: ",
					(float)debugNetworkPacketStatisticsStruct.TotalUnreliableEventUpload * num3,
					";"
				}));
				outStr.AppendLine<string>(string.Concat(new object[]
				{
					"\tOthers (headers, ack etc.) Upload: percent: ",
					(float)num / num2 * 100f,
					"; size in bits: ",
					(float)num * num3,
					";"
				}));
				int num4 = debugNetworkPositionCompressionStatisticsStruct.totalPositionCoarseBitCountX + debugNetworkPositionCompressionStatisticsStruct.totalPositionCoarseBitCountY + debugNetworkPositionCompressionStatisticsStruct.totalPositionCoarseBitCountZ;
				float num5 = 1f / (float)debugNetworkPacketStatisticsStruct.TotalCellPriorityChecks;
				outStr.AppendLine<string>(string.Concat(new object[]
				{
					"\n\tTotal PPS: ",
					(float)debugNetworkPacketStatisticsStruct.TotalPackets / DebugNetworkEventStatistics._totalData.TotalTime,
					"; bps: ",
					(float)debugNetworkPacketStatisticsStruct.TotalUpload / DebugNetworkEventStatistics._totalData.TotalTime,
					";"
				}));
			}
			outStr.AppendLine<string>(string.Concat(new object[]
			{
				"\n\tTotal packets: ",
				debugNetworkPacketStatisticsStruct.TotalPackets,
				"; bits per packet: ",
				(float)debugNetworkPacketStatisticsStruct.TotalUpload / (float)debugNetworkPacketStatisticsStruct.TotalPackets,
				";"
			}));
			outStr.AppendLine<string>("Total Upload: " + debugNetworkPacketStatisticsStruct.TotalUpload + " in bits");
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000CEFA0 File Offset: 0x000CD1A0
		private static void ShowUploadData()
		{
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "ShowUploadData");
			DebugNetworkEventStatistics.GetFormattedDebugUploadDataOutput(ref mbstringBuilder);
			string[] array = mbstringBuilder.ToStringAndRelease().Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				Imgui.Text(array[i]);
			}
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000CEFF8 File Offset: 0x000CD1F8
		private static DebugNetworkEventStatistics.TotalEventData GetCurrentEventData()
		{
			GameNetwork.DebugNetworkPacketStatisticsStruct debugNetworkPacketStatisticsStruct = default(GameNetwork.DebugNetworkPacketStatisticsStruct);
			GameNetwork.DebugNetworkPositionCompressionStatisticsStruct debugNetworkPositionCompressionStatisticsStruct = default(GameNetwork.DebugNetworkPositionCompressionStatisticsStruct);
			GameNetwork.GetDebugUploadsInBits(ref debugNetworkPacketStatisticsStruct, ref debugNetworkPositionCompressionStatisticsStruct);
			return new DebugNetworkEventStatistics.TotalEventData(debugNetworkPacketStatisticsStruct.TotalPackets, debugNetworkPacketStatisticsStruct.TotalUpload, debugNetworkPacketStatisticsStruct.TotalConstantsUpload, debugNetworkPacketStatisticsStruct.TotalReliableEventUpload, debugNetworkPacketStatisticsStruct.TotalReplicationUpload, debugNetworkPacketStatisticsStruct.TotalUnreliableEventUpload);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000CF047 File Offset: 0x000CD247
		private static void DumpReplicationData()
		{
			GameNetwork.PrintReplicationTableStatistics();
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000CF04E File Offset: 0x000CD24E
		private static void ClearReplicationData()
		{
			GameNetwork.ClearReplicationTableStatistics();
		}

		// Token: 0x0400158D RID: 5517
		private static DebugNetworkEventStatistics.TotalData _totalData = new DebugNetworkEventStatistics.TotalData();

		// Token: 0x0400158E RID: 5518
		private static int _curEventType = -1;

		// Token: 0x0400158F RID: 5519
		private static Dictionary<int, DebugNetworkEventStatistics.PerEventData> _statistics = new Dictionary<int, DebugNetworkEventStatistics.PerEventData>();

		// Token: 0x04001590 RID: 5520
		private static int _samplesPerSecond;

		// Token: 0x04001591 RID: 5521
		public static int MaxGraphPointCount;

		// Token: 0x04001592 RID: 5522
		private static bool _showUploadDataText = false;

		// Token: 0x04001593 RID: 5523
		private static bool _useAbsoluteMaximum = false;

		// Token: 0x04001594 RID: 5524
		private static float _collectSampleCheck = 0f;

		// Token: 0x04001595 RID: 5525
		private static float _collectFpsSampleCheck = 0f;

		// Token: 0x04001596 RID: 5526
		private static float _curMaxGraphHeight = 0f;

		// Token: 0x04001597 RID: 5527
		private static float _targetMaxGraphHeight = 0f;

		// Token: 0x04001598 RID: 5528
		private static float _currMaxLossGraphHeight = 0f;

		// Token: 0x04001599 RID: 5529
		private static float _targetMaxLossGraphHeight = 0f;

		// Token: 0x0400159A RID: 5530
		private static DebugNetworkEventStatistics.PerSecondEventData UploadPerSecondEventData;

		// Token: 0x0400159B RID: 5531
		private static readonly Queue<DebugNetworkEventStatistics.TotalEventData> _eventSamples = new Queue<DebugNetworkEventStatistics.TotalEventData>();

		// Token: 0x0400159C RID: 5532
		private static readonly Queue<float> _lossSamples = new Queue<float>();

		// Token: 0x0400159D RID: 5533
		private static DebugNetworkEventStatistics.TotalEventData _prevEventData = new DebugNetworkEventStatistics.TotalEventData();

		// Token: 0x0400159E RID: 5534
		private static DebugNetworkEventStatistics.TotalEventData _currEventData = new DebugNetworkEventStatistics.TotalEventData();

		// Token: 0x0400159F RID: 5535
		private static readonly List<float> _fpsSamplesUntilNextSampling = new List<float>();

		// Token: 0x040015A0 RID: 5536
		private static readonly Queue<float> _fpsSamples = new Queue<float>();

		// Token: 0x040015A1 RID: 5537
		private static bool _useImgui = !GameNetwork.IsDedicatedServer;

		// Token: 0x040015A2 RID: 5538
		public static bool TrackFps = false;

		// Token: 0x0200064C RID: 1612
		public class TotalEventData
		{
			// Token: 0x06003D2D RID: 15661 RVA: 0x000ECD64 File Offset: 0x000EAF64
			protected bool Equals(DebugNetworkEventStatistics.TotalEventData other)
			{
				return this.TotalPackets == other.TotalPackets && this.TotalUpload == other.TotalUpload && this.TotalConstantsUpload == other.TotalConstantsUpload && this.TotalReliableUpload == other.TotalReliableUpload && this.TotalReplicationUpload == other.TotalReplicationUpload && this.TotalUnreliableUpload == other.TotalUnreliableUpload && this.TotalOtherUpload == other.TotalOtherUpload;
			}

			// Token: 0x06003D2E RID: 15662 RVA: 0x000ECDD5 File Offset: 0x000EAFD5
			public override bool Equals(object obj)
			{
				return obj != null && (this == obj || (obj.GetType() == base.GetType() && this.Equals((DebugNetworkEventStatistics.TotalEventData)obj)));
			}

			// Token: 0x06003D2F RID: 15663 RVA: 0x000ECE04 File Offset: 0x000EB004
			public override int GetHashCode()
			{
				return (((((this.TotalPackets * 397 ^ this.TotalUpload) * 397 ^ this.TotalConstantsUpload) * 397 ^ this.TotalReliableUpload) * 397 ^ this.TotalReplicationUpload) * 397 ^ this.TotalUnreliableUpload) * 397 ^ this.TotalOtherUpload;
			}

			// Token: 0x06003D30 RID: 15664 RVA: 0x000ECE65 File Offset: 0x000EB065
			public TotalEventData()
			{
			}

			// Token: 0x06003D31 RID: 15665 RVA: 0x000ECE70 File Offset: 0x000EB070
			public TotalEventData(int totalPackets, int totalUpload, int totalConstants, int totalReliable, int totalReplication, int totalUnreliable)
			{
				this.TotalPackets = totalPackets;
				this.TotalUpload = totalUpload;
				this.TotalConstantsUpload = totalConstants;
				this.TotalReliableUpload = totalReliable;
				this.TotalReplicationUpload = totalReplication;
				this.TotalUnreliableUpload = totalUnreliable;
				this.TotalOtherUpload = totalUpload - (totalConstants + totalReliable + totalReplication + totalUnreliable);
			}

			// Token: 0x17000A29 RID: 2601
			// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000ECEC2 File Offset: 0x000EB0C2
			public bool HasData
			{
				get
				{
					return this.TotalUpload > 0;
				}
			}

			// Token: 0x06003D33 RID: 15667 RVA: 0x000ECED0 File Offset: 0x000EB0D0
			public static DebugNetworkEventStatistics.TotalEventData operator -(DebugNetworkEventStatistics.TotalEventData d1, DebugNetworkEventStatistics.TotalEventData d2)
			{
				return new DebugNetworkEventStatistics.TotalEventData(d1.TotalPackets - d2.TotalPackets, d1.TotalUpload - d2.TotalUpload, d1.TotalConstantsUpload - d2.TotalConstantsUpload, d1.TotalReliableUpload - d2.TotalReliableUpload, d1.TotalReplicationUpload - d2.TotalReplicationUpload, d1.TotalUnreliableUpload - d2.TotalUnreliableUpload);
			}

			// Token: 0x06003D34 RID: 15668 RVA: 0x000ECF30 File Offset: 0x000EB130
			public static bool operator ==(DebugNetworkEventStatistics.TotalEventData d1, DebugNetworkEventStatistics.TotalEventData d2)
			{
				return d1.TotalPackets == d2.TotalPackets && d1.TotalUpload == d2.TotalUpload && d1.TotalConstantsUpload == d2.TotalConstantsUpload && d1.TotalReliableUpload == d2.TotalReliableUpload && d1.TotalReplicationUpload == d2.TotalReplicationUpload && d1.TotalUnreliableUpload == d2.TotalUnreliableUpload;
			}

			// Token: 0x06003D35 RID: 15669 RVA: 0x000ECF93 File Offset: 0x000EB193
			public static bool operator !=(DebugNetworkEventStatistics.TotalEventData d1, DebugNetworkEventStatistics.TotalEventData d2)
			{
				return !(d1 == d2);
			}

			// Token: 0x040020DF RID: 8415
			public readonly int TotalPackets;

			// Token: 0x040020E0 RID: 8416
			public readonly int TotalUpload;

			// Token: 0x040020E1 RID: 8417
			public readonly int TotalConstantsUpload;

			// Token: 0x040020E2 RID: 8418
			public readonly int TotalReliableUpload;

			// Token: 0x040020E3 RID: 8419
			public readonly int TotalReplicationUpload;

			// Token: 0x040020E4 RID: 8420
			public readonly int TotalUnreliableUpload;

			// Token: 0x040020E5 RID: 8421
			public readonly int TotalOtherUpload;
		}

		// Token: 0x0200064D RID: 1613
		private class PerEventData : IComparable<DebugNetworkEventStatistics.PerEventData>
		{
			// Token: 0x06003D36 RID: 15670 RVA: 0x000ECF9F File Offset: 0x000EB19F
			public int CompareTo(DebugNetworkEventStatistics.PerEventData other)
			{
				return other.TotalDataSize - this.TotalDataSize;
			}

			// Token: 0x040020E6 RID: 8422
			public string Name;

			// Token: 0x040020E7 RID: 8423
			public int DataSize;

			// Token: 0x040020E8 RID: 8424
			public int TotalDataSize;

			// Token: 0x040020E9 RID: 8425
			public int Count;
		}

		// Token: 0x0200064E RID: 1614
		public class PerSecondEventData
		{
			// Token: 0x06003D38 RID: 15672 RVA: 0x000ECFB6 File Offset: 0x000EB1B6
			public PerSecondEventData(int totalUploadPerSecond, int constantsUploadPerSecond, int reliableUploadPerSecond, int replicationUploadPerSecond, int unreliableUploadPerSecond, int otherUploadPerSecond)
			{
				this.TotalUploadPerSecond = totalUploadPerSecond;
				this.ConstantsUploadPerSecond = constantsUploadPerSecond;
				this.ReliableUploadPerSecond = reliableUploadPerSecond;
				this.ReplicationUploadPerSecond = replicationUploadPerSecond;
				this.UnreliableUploadPerSecond = unreliableUploadPerSecond;
				this.OtherUploadPerSecond = otherUploadPerSecond;
			}

			// Token: 0x040020EA RID: 8426
			public readonly int TotalUploadPerSecond;

			// Token: 0x040020EB RID: 8427
			public readonly int ConstantsUploadPerSecond;

			// Token: 0x040020EC RID: 8428
			public readonly int ReliableUploadPerSecond;

			// Token: 0x040020ED RID: 8429
			public readonly int ReplicationUploadPerSecond;

			// Token: 0x040020EE RID: 8430
			public readonly int UnreliableUploadPerSecond;

			// Token: 0x040020EF RID: 8431
			public readonly int OtherUploadPerSecond;
		}

		// Token: 0x0200064F RID: 1615
		private class TotalData
		{
			// Token: 0x040020F0 RID: 8432
			public float TotalTime;

			// Token: 0x040020F1 RID: 8433
			public int TotalFrameCount;

			// Token: 0x040020F2 RID: 8434
			public int TotalCount;

			// Token: 0x040020F3 RID: 8435
			public int TotalDataSize;
		}
	}
}
