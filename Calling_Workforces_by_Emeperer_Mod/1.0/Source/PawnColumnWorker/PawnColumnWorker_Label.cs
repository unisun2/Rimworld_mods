using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace FP_RSLUM
{
    class PawnColumnWorker_Label : RimWorld.PawnColumnWorker_Label
	{
		public override void DoHeader(Rect rect, PawnTable table)
		{
			bool flag = Mouse.IsOver(rect);
			if (flag && Event.current.type == null && (Event.current.button == 0 || Event.current.button == 1))
			{
				this.Sort(rect, table, Event.current.control);
				return;
			}
			base.DoHeader(rect, table);
			if (flag)
			{
				TooltipHandler.TipRegion(rect, this.GetHeaderTip(table));
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005790 File Offset: 0x00003990
		public void Sort(Rect rect, PawnTable table, bool byKind)
		{
			if (byKind)
			{
				PawnColumnWorker_Label.sortMode = PawnColumnWorker_Label.SortMode.PawnKind;
			}
			else
			{
				PawnColumnWorker_Label.sortMode = PawnColumnWorker_Label.SortMode.Name;
			}
			this.HeaderClicked(rect, table);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000057AC File Offset: 0x000039AC
		public override int Compare(Pawn a, Pawn b)
		{
			PawnColumnWorker_Label.SortMode sortMode = PawnColumnWorker_Label.sortMode;
			if (sortMode != PawnColumnWorker_Label.SortMode.PawnKind)
			{
				if (sortMode != PawnColumnWorker_Label.SortMode.Name)
				{
				}
				return string.Compare(a.Name.ToStringShort, b.Name.ToStringShort, StringComparison.CurrentCultureIgnoreCase);
			}
			return string.Compare(a.KindLabel, b.KindLabel, StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000057F8 File Offset: 0x000039F8
		public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
		{
			bool flag = Mouse.IsOver(rect) && !pawn.Name.Numerical;
			if (flag && Event.current.control && Event.current.type == null && Event.current.button == 0)
			{
				//Find.WindowStack.Add(new Dialog_RenameAnimal(pawn));
				return;
			}
			base.DoCell(rect, pawn, table);
			if (flag)
			{
				TooltipHandler.ClearTooltipsFrom(rect);
				TooltipHandler.TipRegion(rect, this.GetToolTip(pawn));
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000587C File Offset: 0x00003A7C
		private string GetToolTip(Pawn pawn)
		{
			return string.Concat(new string[]
			{
				Translator.Translate("ClickToJumpTo"),
				"\n\n",
				pawn.GetTooltip().text
			});
		}

		protected override string GetHeaderTip(PawnTable table)
		{
			return Translator.Translate("FP_RSLUM.LabelHeaderTip");
		}

		private static PawnColumnWorker_Label.SortMode sortMode = PawnColumnWorker_Label.SortMode.Name;

		private enum SortMode
		{
			PawnKind,
			Name
		}
	}
}