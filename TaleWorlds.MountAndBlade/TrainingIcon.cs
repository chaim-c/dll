using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000357 RID: 855
	public class TrainingIcon : UsableMachine
	{
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x000BFA21 File Offset: 0x000BDC21
		// (set) Token: 0x06002EDD RID: 11997 RVA: 0x000BFA29 File Offset: 0x000BDC29
		public bool Focused { get; private set; }

		// Token: 0x06002EDE RID: 11998 RVA: 0x000BFA34 File Offset: 0x000BDC34
		protected internal override void OnInit()
		{
			base.OnInit();
			this._markerBeam = base.GameEntity.GetFirstChildEntityWithTag(TrainingIcon.HighlightBeamTag);
			this._weaponIcons = (from x in base.GameEntity.GetChildren()
			where !x.GetScriptComponents().Any<ScriptComponentBehavior>() && x != this._markerBeam
			select x).ToList<GameEntity>();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000BFA90 File Offset: 0x000BDC90
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000BFA9C File Offset: 0x000BDC9C
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._markerBeam != null)
			{
				if (MathF.Abs(this._markerAlpha - this._targetMarkerAlpha) > dt * this._markerAlphaChangeAmount)
				{
					this._markerAlpha += dt * this._markerAlphaChangeAmount * (float)MathF.Sign(this._targetMarkerAlpha - this._markerAlpha);
					this._markerBeam.GetChild(0).GetFirstMesh().SetVectorArgument(this._markerAlpha, 1f, 0.49f, 11.65f);
				}
				else
				{
					this._markerAlpha = this._targetMarkerAlpha;
					if (this._targetMarkerAlpha == 0f)
					{
						GameEntity markerBeam = this._markerBeam;
						if (markerBeam != null)
						{
							markerBeam.SetVisibilityExcludeParents(false);
						}
					}
				}
			}
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				if (standingPoint.HasUser)
				{
					Agent userAgent = standingPoint.UserAgent;
					ActionIndexValueCache currentActionValue = userAgent.GetCurrentActionValue(0);
					ActionIndexValueCache currentActionValue2 = userAgent.GetCurrentActionValue(1);
					if (!(currentActionValue2 == ActionIndexValueCache.act_none) || (!(currentActionValue == TrainingIcon.act_pickup_middle_begin) && !(currentActionValue == TrainingIcon.act_pickup_middle_begin_left_stance)))
					{
						if (currentActionValue2 == ActionIndexValueCache.act_none && (currentActionValue == TrainingIcon.act_pickup_middle_end || currentActionValue == TrainingIcon.act_pickup_middle_end_left_stance))
						{
							this._activated = true;
							userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						}
						else if (currentActionValue2 != ActionIndexValueCache.act_none || !userAgent.SetActionChannel(0, userAgent.GetIsLeftStance() ? TrainingIcon.act_pickup_middle_begin_left_stance : TrainingIcon.act_pickup_middle_begin, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true))
						{
							userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						}
					}
				}
			}
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000BFC84 File Offset: 0x000BDE84
		public void SetMarked(bool highlight)
		{
			if (!highlight)
			{
				this._targetMarkerAlpha = 0f;
				return;
			}
			this._targetMarkerAlpha = 75f;
			this._markerBeam.GetChild(0).GetFirstMesh().SetVectorArgument(this._markerAlpha, 1f, 0.49f, 11.65f);
			GameEntity markerBeam = this._markerBeam;
			if (markerBeam == null)
			{
				return;
			}
			markerBeam.SetVisibilityExcludeParents(true);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000BFCE7 File Offset: 0x000BDEE7
		public bool GetIsActivated()
		{
			bool activated = this._activated;
			this._activated = false;
			return activated;
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000BFCF6 File Offset: 0x000BDEF6
		public string GetTrainingSubTypeTag()
		{
			return this._trainingSubTypeTag;
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000BFD00 File Offset: 0x000BDF00
		public void DisableIcon()
		{
			foreach (GameEntity gameEntity in this._weaponIcons)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000BFD54 File Offset: 0x000BDF54
		public void EnableIcon()
		{
			foreach (GameEntity gameEntity in this._weaponIcons)
			{
				gameEntity.SetVisibilityExcludeParents(true);
			}
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000BFDA8 File Offset: 0x000BDFA8
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			TextObject textObject = new TextObject("{=!}{TRAINING_TYPE}", null);
			textObject.SetTextVariable("TRAINING_TYPE", GameTexts.FindText("str_tutorial_" + this._descriptionTextOfIcon, null));
			return textObject.ToString();
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000BFDDC File Offset: 0x000BDFDC
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject = null)
		{
			TextObject textObject = new TextObject("{=wY1qP2qj}{KEY} Select", null);
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000BFE06 File Offset: 0x000BE006
		public override void OnFocusGain(Agent userAgent)
		{
			base.OnFocusGain(userAgent);
			this.Focused = true;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000BFE16 File Offset: 0x000BE016
		public override void OnFocusLose(Agent userAgent)
		{
			base.OnFocusLose(userAgent);
			this.Focused = false;
		}

		// Token: 0x040013CB RID: 5067
		private static readonly ActionIndexCache act_pickup_middle_begin = ActionIndexCache.Create("act_pickup_middle_begin");

		// Token: 0x040013CC RID: 5068
		private static readonly ActionIndexCache act_pickup_middle_begin_left_stance = ActionIndexCache.Create("act_pickup_middle_begin_left_stance");

		// Token: 0x040013CD RID: 5069
		private static readonly ActionIndexCache act_pickup_middle_end = ActionIndexCache.Create("act_pickup_middle_end");

		// Token: 0x040013CE RID: 5070
		private static readonly ActionIndexCache act_pickup_middle_end_left_stance = ActionIndexCache.Create("act_pickup_middle_end_left_stance");

		// Token: 0x040013CF RID: 5071
		private static readonly string HighlightBeamTag = "highlight_beam";

		// Token: 0x040013D1 RID: 5073
		private bool _activated;

		// Token: 0x040013D2 RID: 5074
		private float _markerAlpha;

		// Token: 0x040013D3 RID: 5075
		private float _targetMarkerAlpha;

		// Token: 0x040013D4 RID: 5076
		private float _markerAlphaChangeAmount = 110f;

		// Token: 0x040013D5 RID: 5077
		private List<GameEntity> _weaponIcons = new List<GameEntity>();

		// Token: 0x040013D6 RID: 5078
		private GameEntity _markerBeam;

		// Token: 0x040013D7 RID: 5079
		[EditableScriptComponentVariable(true)]
		private string _descriptionTextOfIcon = "";

		// Token: 0x040013D8 RID: 5080
		[EditableScriptComponentVariable(true)]
		private string _trainingSubTypeTag = "";
	}
}
