using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace FP_RSLUM
{
    /*
    class FP_RSLUM_level_tab : ITab
    {
		private Pawn PawnToShowInfoAbout
		{
			get
			{
				Pawn pawn = null;
				bool flag = base.SelPawn != null;
				bool flag2 = flag;
				if (flag2)
				{
					pawn = base.SelPawn;
				}
				else
				{
					Corpse corpse = base.SelThing as Corpse;
					bool flag3 = corpse != null;
					bool flag4 = flag3;
					if (flag4)
					{
						pawn = corpse.InnerPawn;
					}
				}
				bool flag5 = pawn == null;
				bool flag6 = flag5;
				Pawn result;
				if (flag6)
				{
					Log.Error("Character tab found no selected pawn to display.", false);
					result = null;
				}
				else
				{
					result = pawn;
				}
				return result;
			}
		}
		public override bool IsVisible
		{
			get
			{
				//bool flag = base.SelPawn.story != null && base.SelPawn.IsColonist;
				bool flag = base.SelPawn.IsColonist;

				return flag;
			}
		}
		public FP_RSLUM_level_tab()
		{
			this.size = new Vector2(17f, 17f) * 2f;
			this.labelKey = "FP_level_tab";
		}
		protected override void FillTab()
		{
		
		}
	}
     * */
}
