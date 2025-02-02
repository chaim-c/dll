using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000058 RID: 88
	public class ReplayCaptureLogic : MissionView
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x00020B60 File Offset: 0x0001ED60
		private void CheckFixedDeltaTimeMode()
		{
			if (this.RenderActive && this.SaveScreenshots)
			{
				base.Mission.FixedDeltaTime = 0.016666668f;
				base.Mission.FixedDeltaTimeMode = true;
				return;
			}
			base.Mission.FixedDeltaTime = 0f;
			base.Mission.FixedDeltaTimeMode = false;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00020BB6 File Offset: 0x0001EDB6
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00020BBE File Offset: 0x0001EDBE
		private bool RenderActive
		{
			get
			{
				return this._renderActive;
			}
			set
			{
				this._renderActive = value;
				this.CheckFixedDeltaTimeMode();
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00020BCD File Offset: 0x0001EDCD
		private Camera MissionCamera
		{
			get
			{
				if (base.MissionScreen == null || !(base.MissionScreen.CombatCamera != null))
				{
					return null;
				}
				return base.MissionScreen.CombatCamera;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00020BF7 File Offset: 0x0001EDF7
		private float ReplayTime
		{
			get
			{
				return base.Mission.CurrentTime - this._replayTimeDiff;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00020C0B File Offset: 0x0001EE0B
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00020C13 File Offset: 0x0001EE13
		private bool SaveScreenshots
		{
			get
			{
				return this._saveScreenshots;
			}
			set
			{
				this._saveScreenshots = value;
				this.CheckFixedDeltaTimeMode();
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00020C22 File Offset: 0x0001EE22
		private KeyValuePair<float, MatrixFrame> PreviousKey
		{
			get
			{
				return this.GetPreviousKey();
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00020C2A File Offset: 0x0001EE2A
		private KeyValuePair<float, MatrixFrame> NextKey
		{
			get
			{
				return this.GetNextKey();
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00020C34 File Offset: 0x0001EE34
		private KeyValuePair<float, MatrixFrame> GetPreviousKey()
		{
			KeyValuePair<float, MatrixFrame> result = this._invalid;
			if (!this._cameraKeys.Any<KeyValuePair<float, SortedDictionary<int, MatrixFrame>>>())
			{
				return result;
			}
			foreach (KeyValuePair<float, SortedDictionary<int, MatrixFrame>> keyValuePair in this._cameraKeys)
			{
				if (keyValuePair.Key <= this.ReplayTime)
				{
					result = new KeyValuePair<float, MatrixFrame>(keyValuePair.Key, keyValuePair.Value[keyValuePair.Value.Count - 1]);
				}
			}
			return result;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00020CD0 File Offset: 0x0001EED0
		private KeyValuePair<float, MatrixFrame> GetNextKey()
		{
			KeyValuePair<float, MatrixFrame> result = this._invalid;
			if (!this._cameraKeys.Any<KeyValuePair<float, SortedDictionary<int, MatrixFrame>>>())
			{
				return result;
			}
			foreach (KeyValuePair<float, SortedDictionary<int, MatrixFrame>> keyValuePair in this._cameraKeys)
			{
				if (keyValuePair.Key > this.ReplayTime)
				{
					result = new KeyValuePair<float, MatrixFrame>(keyValuePair.Key, keyValuePair.Value[0]);
					break;
				}
			}
			return result;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00020D60 File Offset: 0x0001EF60
		public ReplayCaptureLogic()
		{
			this._cameraKeys = new SortedDictionary<float, SortedDictionary<int, MatrixFrame>>();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00020D97 File Offset: 0x0001EF97
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._replayLogic = base.Mission.GetMissionBehavior<ReplayMissionView>();
			this._replayLogic.OverrideInput(true);
			if (!MBCommon.IsPaused)
			{
				this._replayLogic.Pause();
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00020DD0 File Offset: 0x0001EFD0
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._frameSkip && !MBCommon.IsPaused)
			{
				if (!this._isRendered)
				{
					this._isRendered = true;
					return;
				}
				this._replayLogic.Pause();
				this._frameSkip = false;
			}
			if (this.RenderActive)
			{
				this.SaveScreenshot();
				if (!base.Mission.Recorder.IsEndOfRecord())
				{
					KeyValuePair<float, MatrixFrame> previousKey = this.PreviousKey;
					KeyValuePair<float, MatrixFrame> nextKey = this.NextKey;
					this._replayLogic.Resume();
					if (nextKey.Key >= 0f)
					{
						for (int i = 0; i < this._cameraKeys.Count; i++)
						{
							if (previousKey.Key == this._cameraKeys.ElementAt(i).Key)
							{
								float num = nextKey.Key - previousKey.Key;
								float num2 = (this.ReplayTime - previousKey.Key) / num;
								int count = this._cameraKeys[previousKey.Key].Count;
								MatrixFrame frame;
								if (this._lastUsedIndex != i && count > 1)
								{
									frame = this._cameraKeys[previousKey.Key][count - 1];
								}
								else
								{
									frame = new MatrixFrame
									{
										origin = this._path.GetHermiteFrameForDt(num2, i).origin
									};
									Vec3 s = previousKey.Value.rotation.s * (1f - num2) + nextKey.Value.rotation.s * num2;
									Vec3 u = previousKey.Value.rotation.u * (1f - num2) + nextKey.Value.rotation.u * num2;
									Vec3 f = previousKey.Value.rotation.f * (1f - num2) + nextKey.Value.rotation.f * num2;
									frame.rotation.s = s;
									frame.rotation.u = u;
									frame.rotation.f = f;
								}
								frame.rotation.s.Normalize();
								frame.rotation.u.Normalize();
								frame.rotation.f.Normalize();
								frame.rotation.Orthonormalize();
								base.MissionScreen.CustomCamera.Frame = frame;
								this._lastUsedIndex = i;
								return;
							}
						}
						return;
					}
					if (previousKey.Key >= 0f)
					{
						int count2 = this._cameraKeys[previousKey.Key].Count;
						if (count2 > 1)
						{
							MatrixFrame frame2 = this._cameraKeys[previousKey.Key][count2 - 1];
							frame2.rotation.s.Normalize();
							frame2.rotation.u.Normalize();
							frame2.rotation.f.Normalize();
							frame2.rotation.Orthonormalize();
							base.MissionScreen.CustomCamera.Frame = frame2;
							return;
						}
					}
				}
				else
				{
					MBDebug.Print("All images are saved.", 0, Debug.DebugColor.DarkCyan, 64UL);
					this.RenderActive = false;
					this._replayLogic.ResetReplay();
					this._replayTimeDiff = base.Mission.CurrentTime;
					base.MissionScreen.CustomCamera = null;
					this._replayLogic.Pause();
					this.SaveScreenshots = false;
					this._ssNum = 0;
				}
				return;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00021170 File Offset: 0x0001F370
		private void InsertCamKey()
		{
			float replayTime = this.ReplayTime;
			MatrixFrame frame = this.MissionCamera.Frame;
			int num = 0;
			if (this._cameraKeys.ContainsKey(replayTime))
			{
				num = this._cameraKeys[replayTime].Count;
				this._cameraKeys[replayTime].Add(num, frame);
			}
			else
			{
				this._cameraKeys.Add(replayTime, new SortedDictionary<int, MatrixFrame>
				{
					{
						num,
						frame
					}
				});
			}
			MBDebug.Print(string.Concat(new object[]
			{
				"Keyframe to \"",
				replayTime,
				"\" has been inserted with the index: ",
				num,
				".\n"
			}), 0, Debug.DebugColor.Green, 64UL);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0002121F File Offset: 0x0001F41F
		private void MoveToNextFrame()
		{
			this._replayLogic.FastForward(0.016666668f);
			this._replayLogic.Resume();
			this._frameSkip = true;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00021244 File Offset: 0x0001F444
		private void GoToKey(float keyTime)
		{
			if (keyTime < 0f || !this._cameraKeys.ContainsKey(keyTime) || keyTime == this.ReplayTime)
			{
				return;
			}
			MatrixFrame frame;
			if (keyTime < this.ReplayTime)
			{
				frame = this._cameraKeys[keyTime][this._cameraKeys[keyTime].Count - 1];
				this._replayLogic.Rewind(this.ReplayTime - keyTime);
				this._replayTimeDiff = base.Mission.CurrentTime;
			}
			else
			{
				frame = this._cameraKeys[keyTime][0];
				this._replayLogic.FastForward(keyTime - this.ReplayTime);
			}
			this.MissionCamera.Frame = frame;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000212F8 File Offset: 0x0001F4F8
		private void SetPath()
		{
			if (base.Mission.Scene.GetPathWithName("CameraPath") != null)
			{
				base.Mission.Scene.DeletePathWithName("CameraPath");
			}
			base.Mission.Scene.AddPath("CameraPath");
			foreach (KeyValuePair<float, SortedDictionary<int, MatrixFrame>> keyValuePair in this._cameraKeys)
			{
				base.Mission.Scene.AddPathPoint("CameraPath", keyValuePair.Value[0]);
			}
			this._path = base.Mission.Scene.GetPathWithName("CameraPath");
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000213C8 File Offset: 0x0001F5C8
		private void Render(bool saveScreenshots = false)
		{
			if (!this._cameraKeys.ContainsKey(0f))
			{
				this._cameraKeys.Add(0f, new SortedDictionary<int, MatrixFrame>
				{
					{
						0,
						this.MissionCamera.Frame
					}
				});
			}
			else
			{
				this._cameraKeys[0f] = new SortedDictionary<int, MatrixFrame>
				{
					{
						0,
						this.MissionCamera.Frame
					}
				};
			}
			this._replayLogic.ResetReplay();
			this._replayLogic.Pause();
			this._replayTimeDiff = base.Mission.CurrentTime;
			this.SetPath();
			this.SaveScreenshots = saveScreenshots;
			this.RenderActive = true;
			this._lastUsedIndex = 0;
			base.MissionScreen.CustomCamera = base.MissionScreen.CombatCamera;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00021490 File Offset: 0x0001F690
		private void SaveScreenshot()
		{
			if (!this.SaveScreenshots)
			{
				return;
			}
			if (string.IsNullOrEmpty(this._directoryPath.Path))
			{
				PlatformDirectoryPath path = new PlatformDirectoryPath(PlatformFileType.User, "Captures");
				string str = "Cap_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
				this._directoryPath = path + str;
			}
			Utilities.TakeScreenshot(new PlatformFilePath(this._directoryPath, "time_" + string.Format("{0:000000}", this._ssNum) + ".bmp"));
			this._ssNum++;
		}

		// Token: 0x04000289 RID: 649
		private ReplayMissionView _replayLogic;

		// Token: 0x0400028A RID: 650
		private bool _renderActive;

		// Token: 0x0400028B RID: 651
		public const float CaptureFrameRate = 60f;

		// Token: 0x0400028C RID: 652
		private float _replayTimeDiff;

		// Token: 0x0400028D RID: 653
		private bool _frameSkip;

		// Token: 0x0400028E RID: 654
		private Path _path;

		// Token: 0x0400028F RID: 655
		private PlatformDirectoryPath _directoryPath;

		// Token: 0x04000290 RID: 656
		private bool _saveScreenshots;

		// Token: 0x04000291 RID: 657
		private readonly KeyValuePair<float, MatrixFrame> _invalid = new KeyValuePair<float, MatrixFrame>(-1f, default(MatrixFrame));

		// Token: 0x04000292 RID: 658
		private SortedDictionary<float, SortedDictionary<int, MatrixFrame>> _cameraKeys;

		// Token: 0x04000293 RID: 659
		private bool _isRendered;

		// Token: 0x04000294 RID: 660
		private int _lastUsedIndex;

		// Token: 0x04000295 RID: 661
		private int _ssNum;
	}
}
