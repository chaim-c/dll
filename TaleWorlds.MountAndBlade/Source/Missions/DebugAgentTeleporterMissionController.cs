using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003B0 RID: 944
	public class DebugAgentTeleporterMissionController : MissionLogic
	{
		// Token: 0x060032BD RID: 12989 RVA: 0x000D2A00 File Offset: 0x000D0C00
		public override void AfterStart()
		{
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000D2A04 File Offset: 0x000D0C04
		public override void OnMissionTick(float dt)
		{
			Agent agent = null;
			int debugAgent = base.Mission.GetDebugAgent();
			foreach (Agent agent2 in base.Mission.Agents)
			{
				if (debugAgent == agent2.Index)
				{
					agent = agent2;
					break;
				}
			}
			if (agent == null && base.Mission.Agents.Count > 0)
			{
				int num = MBRandom.RandomInt(base.Mission.Agents.Count);
				int count = base.Mission.Agents.Count;
				int num2 = num;
				Agent agent3;
				for (;;)
				{
					agent3 = base.Mission.Agents[num2];
					if (agent3 != Agent.Main && agent3.IsActive())
					{
						break;
					}
					num2 = (num2 + 1) % count;
					if (num2 == num)
					{
						goto IL_DC;
					}
				}
				agent = agent3;
				base.Mission.SetDebugAgent(num2);
			}
			IL_DC:
			if (agent != null)
			{
				MatrixFrame lastFinalRenderCameraFrame = base.Mission.Scene.LastFinalRenderCameraFrame;
				if (Input.DebugInput.IsKeyDown(InputKey.MiddleMouseButton))
				{
					float num3;
					base.Mission.Scene.RayCastForClosestEntityOrTerrain(lastFinalRenderCameraFrame.origin, lastFinalRenderCameraFrame.origin + -lastFinalRenderCameraFrame.rotation.u * 100f, out num3, 0.01f, BodyFlags.CommonCollisionExcludeFlags);
				}
				float f;
				if (Input.DebugInput.IsKeyReleased(InputKey.MiddleMouseButton) && base.Mission.Scene.RayCastForClosestEntityOrTerrain(lastFinalRenderCameraFrame.origin, lastFinalRenderCameraFrame.origin + -lastFinalRenderCameraFrame.rotation.u * 100f, out f, 0.01f, BodyFlags.CommonCollisionExcludeFlags))
				{
					Vec3 position = lastFinalRenderCameraFrame.origin + -lastFinalRenderCameraFrame.rotation.u * f;
					if (Input.DebugInput.IsHotKeyReleased("DebugAgentTeleportMissionControllerHotkeyTeleportMainAgent"))
					{
						agent.TeleportToPosition(position);
					}
					else
					{
						Vec2 vec = -lastFinalRenderCameraFrame.rotation.u.AsVec2;
						WorldPosition worldPosition = new WorldPosition(base.Mission.Scene, UIntPtr.Zero, position, false);
						agent.SetScriptedPositionAndDirection(ref worldPosition, vec.RotationInRadians, false, Agent.AIScriptedFrameFlags.NoAttack);
					}
				}
				if (Input.DebugInput.IsHotKeyPressed("DebugAgentTeleportMissionControllerHotkeyDisableScriptedMovement"))
				{
					agent.DisableScriptedMovement();
				}
			}
		}
	}
}
