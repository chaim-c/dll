using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000022 RID: 34
	public interface ITwoDimensionPlatform
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000140 RID: 320
		float Width { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000141 RID: 321
		float Height { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000142 RID: 322
		float ReferenceWidth { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000143 RID: 323
		float ReferenceHeight { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000144 RID: 324
		float ApplicationTime { get; }

		// Token: 0x06000145 RID: 325
		void Draw(float x, float y, Material material, DrawObject2D drawObject2D, int layer);

		// Token: 0x06000146 RID: 326
		void SetScissor(ScissorTestInfo scissorTestInfo);

		// Token: 0x06000147 RID: 327
		void ResetScissor();

		// Token: 0x06000148 RID: 328
		void PlaySound(string soundName);

		// Token: 0x06000149 RID: 329
		void CreateSoundEvent(string soundName);

		// Token: 0x0600014A RID: 330
		void PlaySoundEvent(string soundName);

		// Token: 0x0600014B RID: 331
		void StopAndRemoveSoundEvent(string soundName);

		// Token: 0x0600014C RID: 332
		void OpenOnScreenKeyboard(string initialText, string descriptionText, int maxLength, int keyboardTypeEnum);

		// Token: 0x0600014D RID: 333
		void BeginDebugPanel(string panelTitle);

		// Token: 0x0600014E RID: 334
		void EndDebugPanel();

		// Token: 0x0600014F RID: 335
		void DrawDebugText(string text);

		// Token: 0x06000150 RID: 336
		bool DrawDebugTreeNode(string text);

		// Token: 0x06000151 RID: 337
		void PopDebugTreeNode();

		// Token: 0x06000152 RID: 338
		void DrawCheckbox(string label, ref bool isChecked);

		// Token: 0x06000153 RID: 339
		bool IsDebugItemHovered();

		// Token: 0x06000154 RID: 340
		bool IsDebugModeEnabled();
	}
}
