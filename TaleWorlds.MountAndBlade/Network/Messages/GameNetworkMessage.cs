using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade.Network.Messages
{
	// Token: 0x0200039E RID: 926
	public abstract class GameNetworkMessage
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x000CF2B6 File Offset: 0x000CD4B6
		// (set) Token: 0x06003213 RID: 12819 RVA: 0x000CF2BE File Offset: 0x000CD4BE
		public int MessageId { get; set; }

		// Token: 0x06003214 RID: 12820 RVA: 0x000CF2C8 File Offset: 0x000CD4C8
		internal void Write()
		{
			DebugNetworkEventStatistics.StartEvent(base.GetType().Name, this.MessageId);
			GameNetworkMessage.WriteIntToPacket(this.MessageId, GameNetwork.IsClientOrReplay ? CompressionBasic.NetworkComponentEventTypeFromClientCompressionInfo : CompressionBasic.NetworkComponentEventTypeFromServerCompressionInfo);
			this.OnWrite();
			GameNetworkMessage.WriteIntToPacket(5, GameNetworkMessage.TestValueCompressionInfo);
			DebugNetworkEventStatistics.EndEvent();
		}

		// Token: 0x06003215 RID: 12821
		protected abstract void OnWrite();

		// Token: 0x06003216 RID: 12822 RVA: 0x000CF320 File Offset: 0x000CD520
		internal bool Read()
		{
			bool result = this.OnRead();
			bool flag = true;
			if (GameNetworkMessage.ReadIntFromPacket(GameNetworkMessage.TestValueCompressionInfo, ref flag) != 5)
			{
				throw new MBNetworkBitException(base.GetType().Name);
			}
			return result;
		}

		// Token: 0x06003217 RID: 12823
		protected abstract bool OnRead();

		// Token: 0x06003218 RID: 12824 RVA: 0x000CF355 File Offset: 0x000CD555
		internal MultiplayerMessageFilter GetLogFilter()
		{
			return this.OnGetLogFilter();
		}

		// Token: 0x06003219 RID: 12825
		protected abstract MultiplayerMessageFilter OnGetLogFilter();

		// Token: 0x0600321A RID: 12826 RVA: 0x000CF35D File Offset: 0x000CD55D
		internal string GetLogFormat()
		{
			return this.OnGetLogFormat();
		}

		// Token: 0x0600321B RID: 12827
		protected abstract string OnGetLogFormat();

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000CF365 File Offset: 0x000CD565
		public static bool IsClientMissionOver
		{
			get
			{
				return GameNetwork.IsClient && !NetworkMain.GameClient.IsInGame && !NetworkMain.CommunityClient.IsInGame;
			}
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000CF38C File Offset: 0x000CD58C
		public static bool ReadBoolFromPacket(ref bool bufferReadValid)
		{
			CompressionInfo.Integer integer = new CompressionInfo.Integer(0, 1);
			int num = 0;
			bufferReadValid = (bufferReadValid && MBAPI.IMBNetwork.ReadIntFromPacket(ref integer, out num));
			return num != 0;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000CF3C0 File Offset: 0x000CD5C0
		public static void WriteBoolToPacket(bool value)
		{
			CompressionInfo.Integer integer = new CompressionInfo.Integer(0, 1);
			MBAPI.IMBNetwork.WriteIntToPacket(value ? 1 : 0, ref integer);
			DebugNetworkEventStatistics.AddDataToStatistic(integer.GetNumBits());
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000CF3F8 File Offset: 0x000CD5F8
		public static int ReadIntFromPacket(CompressionInfo.Integer compressionInfo, ref bool bufferReadValid)
		{
			int result = 0;
			bufferReadValid = (bufferReadValid && MBAPI.IMBNetwork.ReadIntFromPacket(ref compressionInfo, out result));
			return result;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000CF41F File Offset: 0x000CD61F
		public static void WriteIntToPacket(int value, CompressionInfo.Integer compressionInfo)
		{
			MBAPI.IMBNetwork.WriteIntToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000CF43C File Offset: 0x000CD63C
		public static uint ReadUintFromPacket(CompressionInfo.UnsignedInteger compressionInfo, ref bool bufferReadValid)
		{
			uint result = 0U;
			bufferReadValid = (bufferReadValid && MBAPI.IMBNetwork.ReadUintFromPacket(ref compressionInfo, out result));
			return result;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000CF463 File Offset: 0x000CD663
		public static void WriteUintToPacket(uint value, CompressionInfo.UnsignedInteger compressionInfo)
		{
			MBAPI.IMBNetwork.WriteUintToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000CF480 File Offset: 0x000CD680
		public static long ReadLongFromPacket(CompressionInfo.LongInteger compressionInfo, ref bool bufferReadValid)
		{
			long result = 0L;
			bufferReadValid = (bufferReadValid && MBAPI.IMBNetwork.ReadLongFromPacket(ref compressionInfo, out result));
			return result;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000CF4A8 File Offset: 0x000CD6A8
		public static void WriteLongToPacket(long value, CompressionInfo.LongInteger compressionInfo)
		{
			MBAPI.IMBNetwork.WriteLongToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000CF4C4 File Offset: 0x000CD6C4
		public static ulong ReadUlongFromPacket(CompressionInfo.UnsignedLongInteger compressionInfo, ref bool bufferReadValid)
		{
			ulong result = 0UL;
			bufferReadValid = (bufferReadValid && MBAPI.IMBNetwork.ReadUlongFromPacket(ref compressionInfo, out result));
			return result;
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000CF4EC File Offset: 0x000CD6EC
		public static void WriteUlongToPacket(ulong value, CompressionInfo.UnsignedLongInteger compressionInfo)
		{
			MBAPI.IMBNetwork.WriteUlongToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000CF508 File Offset: 0x000CD708
		public static float ReadFloatFromPacket(CompressionInfo.Float compressionInfo, ref bool bufferReadValid)
		{
			float result = 0f;
			bufferReadValid = (bufferReadValid && MBAPI.IMBNetwork.ReadFloatFromPacket(ref compressionInfo, out result));
			return result;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000CF533 File Offset: 0x000CD733
		public static void WriteFloatToPacket(float value, CompressionInfo.Float compressionInfo)
		{
			MBAPI.IMBNetwork.WriteFloatToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000CF550 File Offset: 0x000CD750
		public static string ReadStringFromPacket(ref bool bufferReadValid)
		{
			byte[] array = new byte[1024];
			int count = GameNetworkMessage.ReadByteArrayFromPacket(array, 0, 1024, ref bufferReadValid);
			return GameNetworkMessage.StringEncoding.GetString(array, 0, count);
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000CF584 File Offset: 0x000CD784
		public static void WriteStringToPacket(string value)
		{
			byte[] array = string.IsNullOrEmpty(value) ? new byte[0] : GameNetworkMessage.StringEncoding.GetBytes(value);
			GameNetworkMessage.WriteByteArrayToPacket(array, 0, array.Length);
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000CF5B7 File Offset: 0x000CD7B7
		public static int ReadByteArrayFromPacket(byte[] buffer, int offset, int bufferCapacity, ref bool bufferReadValid)
		{
			return MBAPI.IMBNetwork.ReadByteArrayFromPacket(buffer, offset, bufferCapacity, ref bufferReadValid);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000CF5C8 File Offset: 0x000CD7C8
		public static void WriteBannerCodeToPacket(string bannerCode)
		{
			List<BannerData> bannerDataFromBannerCode = Banner.GetBannerDataFromBannerCode(bannerCode);
			GameNetworkMessage.WriteIntToPacket(bannerDataFromBannerCode.Count, CompressionBasic.BannerDataCountCompressionInfo);
			for (int i = 0; i < bannerDataFromBannerCode.Count; i++)
			{
				BannerData bannerData = bannerDataFromBannerCode[i];
				GameNetworkMessage.WriteIntToPacket(bannerData.MeshId, CompressionBasic.BannerDataMeshIdCompressionInfo);
				GameNetworkMessage.WriteIntToPacket(bannerData.ColorId, CompressionBasic.BannerDataColorIndexCompressionInfo);
				GameNetworkMessage.WriteIntToPacket(bannerData.ColorId2, CompressionBasic.BannerDataColorIndexCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Size.X, CompressionBasic.BannerDataSizeCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Size.Y, CompressionBasic.BannerDataSizeCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Position.X, CompressionBasic.BannerDataSizeCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Position.Y, CompressionBasic.BannerDataSizeCompressionInfo);
				GameNetworkMessage.WriteBoolToPacket(bannerData.DrawStroke);
				GameNetworkMessage.WriteBoolToPacket(bannerData.Mirror);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Rotation, CompressionBasic.BannerDataRotationCompressionInfo);
			}
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000CF6C4 File Offset: 0x000CD8C4
		public static string ReadBannerCodeFromPacket(ref bool bufferReadValid)
		{
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataCountCompressionInfo, ref bufferReadValid);
			MBList<BannerData> mblist = new MBList<BannerData>(num);
			for (int i = 0; i < num; i++)
			{
				BannerData item = new BannerData(GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataMeshIdCompressionInfo, ref bufferReadValid), GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataColorIndexCompressionInfo, ref bufferReadValid), GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataColorIndexCompressionInfo, ref bufferReadValid), new Vec2((float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataSizeCompressionInfo, ref bufferReadValid), (float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataSizeCompressionInfo, ref bufferReadValid)), new Vec2((float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataSizeCompressionInfo, ref bufferReadValid), (float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataSizeCompressionInfo, ref bufferReadValid)), GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid), GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid), (float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataRotationCompressionInfo, ref bufferReadValid) * 0.00278f);
				mblist.Add(item);
			}
			return Banner.GetBannerCodeFromBannerDataList(mblist);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000CF782 File Offset: 0x000CD982
		public static void WriteByteArrayToPacket(byte[] value, int offset, int size)
		{
			MBAPI.IMBNetwork.WriteByteArrayToPacket(value, offset, size);
			DebugNetworkEventStatistics.AddDataToStatistic(MathF.Min(size, 1024) + 10);
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000CF7A4 File Offset: 0x000CD9A4
		public static MBActionSet ReadActionSetReferenceFromPacket(CompressionInfo.Integer compressionInfo, ref bool bufferReadValid)
		{
			if (bufferReadValid)
			{
				int i;
				bufferReadValid = MBAPI.IMBNetwork.ReadIntFromPacket(ref compressionInfo, out i);
				return new MBActionSet(i);
			}
			return MBActionSet.InvalidActionSet;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000CF7D1 File Offset: 0x000CD9D1
		public static void WriteActionSetReferenceToPacket(MBActionSet actionSet, CompressionInfo.Integer compressionInfo)
		{
			MBAPI.IMBNetwork.WriteIntToPacket(actionSet.Index, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000CF7F4 File Offset: 0x000CD9F4
		public static int ReadAgentIndexFromPacket(ref bool bufferReadValid)
		{
			CompressionInfo.Integer agentCompressionInfo = CompressionMission.AgentCompressionInfo;
			int result = -1;
			bufferReadValid = (bufferReadValid && MBAPI.IMBNetwork.ReadIntFromPacket(ref agentCompressionInfo, out result));
			return result;
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000CF824 File Offset: 0x000CDA24
		public static void WriteAgentIndexToPacket(int agentIndex)
		{
			CompressionInfo.Integer agentCompressionInfo = CompressionMission.AgentCompressionInfo;
			MBAPI.IMBNetwork.WriteIntToPacket(agentIndex, ref agentCompressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(agentCompressionInfo.GetNumBits());
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000CF850 File Offset: 0x000CDA50
		public static MBObjectBase ReadObjectReferenceFromPacket(MBObjectManager objectManager, CompressionInfo.UnsignedInteger compressionInfo, ref bool bufferReadValid)
		{
			uint num = GameNetworkMessage.ReadUintFromPacket(compressionInfo, ref bufferReadValid);
			if (bufferReadValid && num > 0U)
			{
				MBGUID objectId = new MBGUID(num);
				return objectManager.GetObject(objectId);
			}
			return null;
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000CF880 File Offset: 0x000CDA80
		public static void WriteObjectReferenceToPacket(MBObjectBase value, CompressionInfo.UnsignedInteger compressionInfo)
		{
			MBAPI.IMBNetwork.WriteUintToPacket((value != null) ? value.Id.InternalValue : 0U, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000CF8BC File Offset: 0x000CDABC
		public static VirtualPlayer ReadVirtualPlayerReferenceToPacket(ref bool bufferReadValid, bool canReturnNull = false)
		{
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref bufferReadValid);
			bool flag = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			if ((num >= 0 && !GameNetworkMessage.IsClientMissionOver) & bufferReadValid)
			{
				VirtualPlayer result;
				if (!flag)
				{
					result = GameNetwork.VirtualPlayers[num];
				}
				else
				{
					result = GameNetwork.DisconnectedNetworkPeers[num].VirtualPlayer;
				}
				return result;
			}
			return null;
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000CF911 File Offset: 0x000CDB11
		public static NetworkCommunicator ReadNetworkPeerReferenceFromPacket(ref bool bufferReadValid, bool canReturnNull = false)
		{
			VirtualPlayer virtualPlayer = GameNetworkMessage.ReadVirtualPlayerReferenceToPacket(ref bufferReadValid, canReturnNull);
			return ((virtualPlayer != null) ? virtualPlayer.Communicator : null) as NetworkCommunicator;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000CF92C File Offset: 0x000CDB2C
		public static void WriteVirtualPlayerReferenceToPacket(VirtualPlayer virtualPlayer)
		{
			bool value = false;
			int num = (virtualPlayer != null) ? virtualPlayer.Index : -1;
			if (num >= 0 && GameNetwork.VirtualPlayers[num] != virtualPlayer)
			{
				for (int i = 0; i < GameNetwork.DisconnectedNetworkPeers.Count; i++)
				{
					if (GameNetwork.DisconnectedNetworkPeers[i].VirtualPlayer == virtualPlayer)
					{
						num = i;
						value = true;
						break;
					}
				}
			}
			GameNetworkMessage.WriteIntToPacket(num, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(value);
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000CF995 File Offset: 0x000CDB95
		public static void WriteNetworkPeerReferenceToPacket(NetworkCommunicator networkCommunicator)
		{
			GameNetworkMessage.WriteVirtualPlayerReferenceToPacket((networkCommunicator != null) ? networkCommunicator.VirtualPlayer : null);
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000CF9A8 File Offset: 0x000CDBA8
		public static int ReadTeamIndexFromPacket(ref bool bufferReadValid)
		{
			return GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamCompressionInfo, ref bufferReadValid);
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000CF9B5 File Offset: 0x000CDBB5
		public static void WriteTeamIndexToPacket(int teamIndex)
		{
			GameNetworkMessage.WriteIntToPacket(teamIndex, CompressionMission.TeamCompressionInfo);
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000CF9C4 File Offset: 0x000CDBC4
		public static MissionObjectId ReadMissionObjectIdFromPacket(ref bool bufferReadValid)
		{
			bool createdAtRuntime = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref bufferReadValid);
			if (!bufferReadValid || num == -1 || GameNetworkMessage.IsClientMissionOver)
			{
				if (num != -1)
				{
					MBDebug.Print(string.Concat(new object[]
					{
						"Reading null MissionObject because IsClientMissionOver: ",
						GameNetworkMessage.IsClientMissionOver.ToString(),
						" valid read: ",
						bufferReadValid.ToString(),
						" MissionObject ID: ",
						num,
						" runtime: ",
						createdAtRuntime.ToString()
					}), 0, Debug.DebugColor.White, 17592186044416UL);
				}
				return new MissionObjectId(-1, false);
			}
			return new MissionObjectId(num, createdAtRuntime);
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000CFA6E File Offset: 0x000CDC6E
		public static void WriteMissionObjectIdToPacket(MissionObjectId value)
		{
			GameNetworkMessage.WriteBoolToPacket(value.CreatedAtRuntime);
			GameNetworkMessage.WriteIntToPacket(value.Id, CompressionBasic.MissionObjectIDCompressionInfo);
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000CFA8C File Offset: 0x000CDC8C
		public static Vec3 ReadVec3FromPacket(CompressionInfo.Float compressionInfo, ref bool bufferReadValid)
		{
			float x = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			float y = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			float z = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			return new Vec3(x, y, z, -1f);
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000CFABC File Offset: 0x000CDCBC
		public static void WriteVec3ToPacket(Vec3 value, CompressionInfo.Float compressionInfo)
		{
			GameNetworkMessage.WriteFloatToPacket(value.x, compressionInfo);
			GameNetworkMessage.WriteFloatToPacket(value.y, compressionInfo);
			GameNetworkMessage.WriteFloatToPacket(value.z, compressionInfo);
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000CFAE4 File Offset: 0x000CDCE4
		public static Vec2 ReadVec2FromPacket(CompressionInfo.Float compressionInfo, ref bool bufferReadValid)
		{
			float a = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			float b = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			return new Vec2(a, b);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000CFB06 File Offset: 0x000CDD06
		public static void WriteVec2ToPacket(Vec2 value, CompressionInfo.Float compressionInfo)
		{
			GameNetworkMessage.WriteFloatToPacket(value.x, compressionInfo);
			GameNetworkMessage.WriteFloatToPacket(value.y, compressionInfo);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000CFB20 File Offset: 0x000CDD20
		public static Mat3 ReadRotationMatrixFromPacket(ref bool bufferReadValid)
		{
			Vec3 s = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref bufferReadValid);
			Vec3 f = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref bufferReadValid);
			Vec3 u = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref bufferReadValid);
			return new Mat3(s, f, u);
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000CFB57 File Offset: 0x000CDD57
		public static void WriteRotationMatrixToPacket(Mat3 value)
		{
			GameNetworkMessage.WriteVec3ToPacket(value.s, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(value.f, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(value.u, CompressionBasic.UnitVectorCompressionInfo);
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000CFB8C File Offset: 0x000CDD8C
		public static MatrixFrame ReadMatrixFrameFromPacket(ref bool bufferReadValid)
		{
			Vec3 o = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.PositionCompressionInfo, ref bufferReadValid);
			Vec3 scalingVector = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.ScaleCompressionInfo, ref bufferReadValid);
			Mat3 rot = GameNetworkMessage.ReadRotationMatrixFromPacket(ref bufferReadValid);
			MatrixFrame result = new MatrixFrame(rot, o);
			result.Scale(scalingVector);
			return result;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000CFBCC File Offset: 0x000CDDCC
		public static void WriteMatrixFrameToPacket(MatrixFrame frame)
		{
			Vec3 scaleVector = frame.rotation.GetScaleVector();
			MatrixFrame matrixFrame = frame;
			matrixFrame.Scale(new Vec3(1f / scaleVector.x, 1f / scaleVector.y, 1f / scaleVector.z, -1f));
			GameNetworkMessage.WriteVec3ToPacket(matrixFrame.origin, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(scaleVector, CompressionBasic.ScaleCompressionInfo);
			GameNetworkMessage.WriteRotationMatrixToPacket(matrixFrame.rotation);
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000CFC44 File Offset: 0x000CDE44
		public static MatrixFrame ReadNonUniformTransformFromPacket(CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo, ref bool bufferReadValid)
		{
			MatrixFrame result = GameNetworkMessage.ReadUnitTransformFromPacket(positionCompressionInfo, quaternionCompressionInfo, ref bufferReadValid);
			Vec3 scaleAmountXYZ = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.ScaleCompressionInfo, ref bufferReadValid);
			result.rotation.ApplyScaleLocal(scaleAmountXYZ);
			return result;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000CFC74 File Offset: 0x000CDE74
		public static void WriteNonUniformTransformToPacket(MatrixFrame frame, CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo)
		{
			MatrixFrame frame2 = frame;
			Vec3 value = frame2.rotation.MakeUnit();
			GameNetworkMessage.WriteUnitTransformToPacket(frame2, positionCompressionInfo, quaternionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(value, CompressionBasic.ScaleCompressionInfo);
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000CFCA4 File Offset: 0x000CDEA4
		public static MatrixFrame ReadTransformFromPacket(CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo, ref bool bufferReadValid)
		{
			MatrixFrame result = GameNetworkMessage.ReadUnitTransformFromPacket(positionCompressionInfo, quaternionCompressionInfo, ref bufferReadValid);
			if (GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid))
			{
				float scaleAmount = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.ScaleCompressionInfo, ref bufferReadValid);
				result.rotation.ApplyScaleLocal(scaleAmount);
			}
			return result;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000CFCDC File Offset: 0x000CDEDC
		public static void WriteTransformToPacket(MatrixFrame frame, CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo)
		{
			MatrixFrame frame2 = frame;
			Vec3 vec = frame2.rotation.MakeUnit();
			GameNetworkMessage.WriteUnitTransformToPacket(frame2, positionCompressionInfo, quaternionCompressionInfo);
			bool flag = !vec.x.ApproximatelyEqualsTo(1f, CompressionBasic.ScaleCompressionInfo.GetPrecision());
			GameNetworkMessage.WriteBoolToPacket(flag);
			if (flag)
			{
				GameNetworkMessage.WriteFloatToPacket(vec.x, CompressionBasic.ScaleCompressionInfo);
			}
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000CFD38 File Offset: 0x000CDF38
		public static MatrixFrame ReadUnitTransformFromPacket(CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo, ref bool bufferReadValid)
		{
			return new MatrixFrame
			{
				origin = GameNetworkMessage.ReadVec3FromPacket(positionCompressionInfo, ref bufferReadValid),
				rotation = GameNetworkMessage.ReadQuaternionFromPacket(quaternionCompressionInfo, ref bufferReadValid).ToMat3
			};
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000CFD72 File Offset: 0x000CDF72
		public static void WriteUnitTransformToPacket(MatrixFrame frame, CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo)
		{
			GameNetworkMessage.WriteVec3ToPacket(frame.origin, positionCompressionInfo);
			GameNetworkMessage.WriteQuaternionToPacket(frame.rotation.ToQuaternion(), quaternionCompressionInfo);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000CFD94 File Offset: 0x000CDF94
		public static Quaternion ReadQuaternionFromPacket(CompressionInfo.Float compressionInfo, ref bool bufferReadValid)
		{
			Quaternion result = default(Quaternion);
			float num = 0f;
			int num2 = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.OmittedQuaternionComponentIndexCompressionInfo, ref bufferReadValid);
			for (int i = 0; i < 4; i++)
			{
				if (i != num2)
				{
					result[i] = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
					num += result[i] * result[i];
				}
			}
			result[num2] = MathF.Sqrt(1f - num);
			result.SafeNormalize();
			return result;
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000CFE0C File Offset: 0x000CE00C
		public static void WriteQuaternionToPacket(Quaternion q, CompressionInfo.Float compressionInfo)
		{
			int num = -1;
			float num2 = 0f;
			Quaternion quaternion = q;
			quaternion.SafeNormalize();
			for (int i = 0; i < 4; i++)
			{
				float num3 = MathF.Abs(quaternion[i]);
				if (num3 > num2)
				{
					num2 = num3;
					num = i;
				}
			}
			if (quaternion[num] < 0f)
			{
				quaternion.Flip();
			}
			GameNetworkMessage.WriteIntToPacket(num, CompressionBasic.OmittedQuaternionComponentIndexCompressionInfo);
			for (int j = 0; j < 4; j++)
			{
				if (j != num)
				{
					GameNetworkMessage.WriteFloatToPacket(quaternion[j], compressionInfo);
				}
			}
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000CFE98 File Offset: 0x000CE098
		public static void WriteBodyPropertiesToPacket(BodyProperties bodyProperties)
		{
			GameNetworkMessage.WriteFloatToPacket(bodyProperties.Age, CompressionBasic.AgentAgeCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(bodyProperties.Weight, CompressionBasic.FaceKeyDataCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(bodyProperties.Build, CompressionBasic.FaceKeyDataCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart1, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart2, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart3, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart4, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart5, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart6, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart7, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart8, CompressionBasic.DebugULongNonCompressionInfo);
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000CFF60 File Offset: 0x000CE160
		public static BodyProperties ReadBodyPropertiesFromPacket(ref bool bufferReadValid)
		{
			float age = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AgentAgeCompressionInfo, ref bufferReadValid);
			float weight = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.FaceKeyDataCompressionInfo, ref bufferReadValid);
			float build = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.FaceKeyDataCompressionInfo, ref bufferReadValid);
			ulong keyPart = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong keyPart2 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong keyPart3 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong keyPart4 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong keyPart5 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong keyPart6 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong keyPart7 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong keyPart8 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			if (bufferReadValid)
			{
				return new BodyProperties(new DynamicBodyProperties(age, weight, build), new StaticBodyProperties(keyPart, keyPart2, keyPart3, keyPart4, keyPart5, keyPart6, keyPart7, keyPart8));
			}
			return default(BodyProperties);
		}

		// Token: 0x040015AB RID: 5547
		private static readonly Encoding StringEncoding = new UTF8Encoding();

		// Token: 0x040015AC RID: 5548
		private static CompressionInfo.Integer TestValueCompressionInfo = new CompressionInfo.Integer(0, 3);

		// Token: 0x040015AD RID: 5549
		private const int ConstTestValue = 5;

		// Token: 0x02000651 RID: 1617
		// (Invoke) Token: 0x06003D4A RID: 15690
		public delegate bool ClientMessageHandlerDelegate<T>(NetworkCommunicator peer, T message) where T : GameNetworkMessage;

		// Token: 0x02000652 RID: 1618
		// (Invoke) Token: 0x06003D4E RID: 15694
		public delegate void ServerMessageHandlerDelegate<T>(T message) where T : GameNetworkMessage;
	}
}
