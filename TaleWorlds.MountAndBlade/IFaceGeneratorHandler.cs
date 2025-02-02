using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000242 RID: 578
	public interface IFaceGeneratorHandler
	{
		// Token: 0x06001F4C RID: 8012
		void ChangeToBodyCamera();

		// Token: 0x06001F4D RID: 8013
		void ChangeToEyeCamera();

		// Token: 0x06001F4E RID: 8014
		void ChangeToNoseCamera();

		// Token: 0x06001F4F RID: 8015
		void ChangeToMouthCamera();

		// Token: 0x06001F50 RID: 8016
		void ChangeToFaceCamera();

		// Token: 0x06001F51 RID: 8017
		void ChangeToHairCamera();

		// Token: 0x06001F52 RID: 8018
		void RefreshCharacterEntity();

		// Token: 0x06001F53 RID: 8019
		void MakeVoice(int voiceIndex, float pitch);

		// Token: 0x06001F54 RID: 8020
		void SetFacialAnimation(string faceAnimation, bool loop);

		// Token: 0x06001F55 RID: 8021
		void Done();

		// Token: 0x06001F56 RID: 8022
		void Cancel();

		// Token: 0x06001F57 RID: 8023
		void UndressCharacterEntity();

		// Token: 0x06001F58 RID: 8024
		void DressCharacterEntity();

		// Token: 0x06001F59 RID: 8025
		void DefaultFace();
	}
}
