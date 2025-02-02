using System;
using TaleWorlds.CampaignSystem.Map;

namespace SandBox
{
	// Token: 0x0200001C RID: 28
	public class MapSceneCreator : IMapSceneCreator
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00004BFC File Offset: 0x00002DFC
		IMapScene IMapSceneCreator.CreateMapScene()
		{
			return new MapScene();
		}
	}
}
