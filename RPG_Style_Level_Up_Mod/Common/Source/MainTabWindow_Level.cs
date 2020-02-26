using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace FP_RSLUM
{
    class MainTabWindow_Level : MainTabWindow_PawnTable
    {
        public const float buttonWidth = 110f;
        public const float buttonHeight = 35f;
        public const float buttonGap = 4f;
        public const float extraTopSpace = 83f;

		private PawnTable table;

		public bool ColonistOrAnimal = true;

		// margin = 6f

		protected override PawnTableDef PawnTableDef
		{
			get
			{
				return PawnTableDefOf.FP_RSLUM_MainTable;
			}
		}

		public IEnumerable<Pawn> getPawns()
		{
			return Pawns;
		}

		protected override IEnumerable<Pawn> Pawns
		{
			get
			{
				if (ColonistOrAnimal)
				{
					return from p in Find.CurrentMap.mapPawns.PawnsInFaction(Faction.OfPlayer)
						   where p.IsColonist
						   select p;
				}
				else
				{
					return from p in Find.CurrentMap.mapPawns.PawnsInFaction(Faction.OfPlayer)
						   where p.RaceProps.Animal
						   select p;
				}
				
			}
		}

		public override void PostOpen()
		{
			if (this.table == null)
			{
				this.table = this.CreateTable();
			}
			this.SetDirty();
			Find.World.renderer.wantedMode = WorldRenderMode.None;
		}

		public override void DoWindowContents(Rect rect)
		{
			this.SetInitialSizeAndPosition();
			this.DoListShiftButton(new Rect(rect.x, rect.yMin, 200f, 30f));
			this.DoLVUPButton(new Rect(rect.x + 200f, rect.yMin, 200f, 30f));

			this.table.PawnTableOnGUI(new Vector2(rect.x, rect.y + this.ExtraTopSpace + 40f));
		}

		public void DoListShiftButton(Rect rect)
		{
			TooltipHandler.TipRegion(rect, this.ColonistOrAnimal ? Translator.Translate("LvTab_Colonist") : Translator.Translate("LvTab_Animal"));
			if (Widgets.ButtonText(rect, this.ColonistOrAnimal ? Translator.Translate("LvTab_Colonist") : Translator.Translate("LvTab_Animal")))
			{
				this.ColonistOrAnimal = !this.ColonistOrAnimal;
				Notify_ResolutionChanged();
			}
		}

		public void DoLVUPButton(Rect rect)
		{
			TooltipHandler.TipRegion(rect, Translator.Translate("LvTab_LVUPButtonDesc"));
			if (Widgets.ButtonText(rect, Translator.Translate("LvTab_LVUPButton")))
			{
				IEnumerable<Pawn> Pawns = from p in Find.CurrentMap.mapPawns.AllPawnsSpawned
										  select p;
				foreach(Pawn pawn in Pawns)
				{
					PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
					if (pawnlvcomp != null)
						pawnlvcomp.levelup();
				}

				Notify_ResolutionChanged();
			}
		}

		private PawnTable CreateTable()
		{
			return (PawnTable)Activator.CreateInstance(this.PawnTableDef.workerClass, new object[]
			{
				this.PawnTableDef,
				new Func<IEnumerable<Pawn>>(getPawns),
				UI.screenWidth - (int)(this.Margin * 2f),
				(int)((float)(UI.screenHeight - 35) - this.ExtraBottomSpace - this.ExtraTopSpace - this.Margin * 2f)
			});
		}

		public override Vector2 RequestedTabSize
		{
			get
			{
				if (this.table == null)
				{
					return Vector2.zero;
				}
				return new Vector2(this.table.Size.x + this.Margin * 2f, this.table.Size.y + this.ExtraBottomSpace + this.ExtraTopSpace + this.Margin * 2f + 40f);
			}
		}
		public void Notify_PawnsChanged()
		{
			this.SetDirty();
		}
		public override void Notify_ResolutionChanged()
		{
			this.table = this.CreateTable();
			base.Notify_ResolutionChanged();
		}

		protected void SetDirty()
		{
			this.table.SetDirty();
			this.SetInitialSizeAndPosition();
		}
	}
}
