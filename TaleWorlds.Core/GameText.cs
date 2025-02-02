﻿using System;
using System.Collections.Generic;
using TaleWorlds.Localization;

namespace TaleWorlds.Core
{
	// Token: 0x02000071 RID: 113
	public class GameText
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000193D0 File Offset: 0x000175D0
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x000193D8 File Offset: 0x000175D8
		public string Id { get; private set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x000193E1 File Offset: 0x000175E1
		public IEnumerable<GameText.GameTextVariation> Variations
		{
			get
			{
				return this._variationList;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x000193E9 File Offset: 0x000175E9
		public TextObject DefaultText
		{
			get
			{
				if (this._variationList != null && this._variationList.Count > 0)
				{
					return this._variationList[0].Text;
				}
				return null;
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00019414 File Offset: 0x00017614
		internal GameText()
		{
			this._variationList = new List<GameText.GameTextVariation>();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00019427 File Offset: 0x00017627
		internal GameText(string id)
		{
			this.Id = id;
			this._variationList = new List<GameText.GameTextVariation>();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00019444 File Offset: 0x00017644
		internal TextObject GetVariation(string variationId)
		{
			foreach (GameText.GameTextVariation gameTextVariation in this._variationList)
			{
				if (gameTextVariation.Id.Equals(variationId))
				{
					return gameTextVariation.Text;
				}
			}
			return null;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000194AC File Offset: 0x000176AC
		public void AddVariationWithId(string variationId, TextObject text, List<GameTextManager.ChoiceTag> choiceTags)
		{
			foreach (GameText.GameTextVariation gameTextVariation in this._variationList)
			{
				if (gameTextVariation.Id.Equals(variationId) && gameTextVariation.Text.ToString().Equals(text.ToString()))
				{
					return;
				}
			}
			this._variationList.Add(new GameText.GameTextVariation(variationId, text, choiceTags));
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00019534 File Offset: 0x00017734
		public void AddVariation(string text, params object[] propertiesAndWeights)
		{
			List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
			for (int i = 0; i < propertiesAndWeights.Length; i += 2)
			{
				string tagName = (string)propertiesAndWeights[i];
				int weight = Convert.ToInt32(propertiesAndWeights[i + 1]);
				list.Add(new GameTextManager.ChoiceTag(tagName, weight));
			}
			this.AddVariationWithId("", new TextObject(text, null), list);
		}

		// Token: 0x040003BC RID: 956
		private readonly List<GameText.GameTextVariation> _variationList;

		// Token: 0x020000FC RID: 252
		public struct GameTextVariation
		{
			// Token: 0x06000A46 RID: 2630 RVA: 0x000216EF File Offset: 0x0001F8EF
			internal GameTextVariation(string id, TextObject text, List<GameTextManager.ChoiceTag> choiceTags)
			{
				this.Id = id;
				this.Text = text;
				this.Tags = choiceTags.ToArray();
			}

			// Token: 0x040006D3 RID: 1747
			public readonly string Id;

			// Token: 0x040006D4 RID: 1748
			public readonly TextObject Text;

			// Token: 0x040006D5 RID: 1749
			public readonly GameTextManager.ChoiceTag[] Tags;
		}
	}
}
