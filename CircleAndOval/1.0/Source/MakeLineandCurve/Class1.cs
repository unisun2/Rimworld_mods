using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;


namespace CircleAndOval
{
    public class CAOstartpoint : Building
    {


        public CAOstartpoint()
        {
            if (Current.Game.CurrentMap != null)
            {
                foreach (Building b in Current.Game.CurrentMap.listerBuildings.allBuildingsColonist)
                {
                    if (b.def.defName.Equals("CAOstartpoint"))
                    {
                        b.Destroy(DestroyMode.Vanish);
                        Messages.Message("CAOstartpointAlreadyOnMap".Translate(), MessageTypeDefOf.NeutralEvent);
                        break;
                    }
                }
            }
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            //Messages.Message(Position.x + "  " + Position.y + " " + Position.z + " ", MessageTypeDefOf.NeutralEvent);
            
        }
    }



    public class FullCircle : Building
    {
        IntVec3 startpoint;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            foreach (Building b in Current.Game.CurrentMap.listerBuildings.allBuildingsColonist)
            {
                if (b.def.defName.Equals("CAOstartpoint"))
                {
                    startpoint = b.Position;

                    //Messages.Message((Position.x - startpoint.x) + " " + (Position.z - startpoint.z) + " ", MessageTypeDefOf.NeutralEvent);
                    MakeFC((Position.x - startpoint.x), (Position.z - startpoint.z), b.Position.x, b.Position.z, this.Stuff);

                    //this.Destroy(DestroyMode.Vanish);
                    return;
                }
            }
            // 못찾음
            Messages.Message("No_CAOstartpoint".Translate(), MessageTypeDefOf.NeutralEvent);
        }

        private void MakeFC(int XX1, int ZZ1, int bx, int bz, ThingDef stuff)
        {
            Map map = this.Map;

            //Verse.Log.Message(XX1 + " " + ZZ1);
            int Radius = (int)Math.Round(Math.Sqrt(XX1*XX1 + ZZ1*ZZ1));


            //Verse.Log.Message("Radius : " + Radius);
            int FHD = Radius * 2 + 1;//Radius * 2 + 1;
            /*
            int LLL = bx - Radius;
            int RRR = bx + Radius;
            int BBB = bz - Radius;
            int UUU = bz + Radius;

            if (LLL < 0)
            {
                LLL = 0;
            }
            if (BBB < 0)
            {
                BBB = 0;
            }
            if (RRR > map.Size.x)
            {
                RRR = map.Size.x;
            }
            if (UUU > map.Size.z)
            {
                UUU = map.Size.z;
            }
            */

            //int x = 0; int y = (int)Math.Ceiling(Radius);
            IntVec3 printVec;
            //bool[,] grid = new bool[FHD, FHD];
            int y = 0;
            int x = 0;

            for (y = 0; y < FHD; y++)
            {
                for (x = 0; x < FHD; x++)
                {
                    if((bx - Radius + x) < 0 || (bx - Radius + x) >= map.Size.x)
                    {
                        continue;
                    }
                    else if((bz - Radius + y) < 0 || bz - Radius + y >= map.Size.z)
                    {
                        continue;
                    }

                    if (is_ellipse(Radius, x - (Radius), y - (Radius))) // 체크!
                    {
                            // 블루프린트 설치
                            //grid[y, x] = true;
                            printVec = this.Position;
                            printVec.x = bx - Radius + x;
                            printVec.z = bz - Radius + y;

                            //Messages.Message(bx + "  " + x  + "  " + bz + "  "+y+ "bphere"+ "  "+ "pv" + printVec.x + " " + printVec.z + " " + Radius + " " + FHD, MessageTypeDefOf.NeutralEvent);

                            GenConstruct.PlaceBlueprintForBuild(ThingDefOf.Wall, printVec, this.Map, base.Rotation, Faction.OfPlayer, stuff);

                        }
                }
            }
            //GenConstruct.PlaceBlueprintForBuild(ThingDefOf.Wall, base.Position, this.Map, base.Rotation, Faction.OfPlayer, ThingDefOf.WoodLog);
        }

        private bool is_ellipse(int Radius, double x, double y)
        {
            //EPSILON : change errors.
            const float EPSILON = 0.5f;
            double tmp1, tmp2;

            if (Radius < x) return false;
            tmp1 = Math.Sqrt(Radius * Radius - x * x);
            tmp2 = Math.Sqrt(Radius * Radius - x * x) * -1;
            if (Math.Abs(tmp1 - y) < EPSILON) return true;
            else if (Math.Abs(tmp2 - y) < EPSILON) return true;

            if (Radius < y) return false;
            tmp1 = Math.Sqrt(Radius * Radius - y * y);
            tmp2 = Math.Sqrt(Radius * Radius - y * y) * -1;
            if (Math.Abs(tmp1 - x) < EPSILON) return true;
            else if (Math.Abs(tmp2 - x) < EPSILON) return true;
            return false;
        }
    }


    public class FullOval : Building
    {
        IntVec3 startpoint;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            foreach (Building b in Current.Game.CurrentMap.listerBuildings.allBuildingsColonist)
            {
                if (b.def.defName.Equals("CAOstartpoint"))
                {
                    
                    startpoint = b.Position;
                    MakeFO(Math.Abs(Position.x - startpoint.x), Math.Abs(Position.z - startpoint.z), b.Position.x, b.Position.z, this.Stuff);
                    return;
                }
            }
            // 못찾음
            Messages.Message("No_CAOstartpoint".Translate(), MessageTypeDefOf.NeutralEvent);

        }

        private void MakeFO(int XX1, int ZZ1, int bx, int bz, ThingDef stuff)
        {
            Map map = this.Map;

            //Verse.Log.Message(XX1 + " " + ZZ1);
            //int Radius = (int)Math.Round(Math.Sqrt(XX1 * XX1 + ZZ1 * ZZ1));


            //Verse.Log.Message("Radius : " + Radius);
            //int FHD = Radius * 2 + 1;//Radius * 2 + 1;
            

            //int x = 0; int y = (int)Math.Ceiling(Radius);
            IntVec3 printVec;
            //bool[,] grid = new bool[XX1 * 2 + 1, ZZ1 * 2 + 1];
            int y = 0;
            int x = 0;

            for (y = 0; y < ZZ1 * 2 + 1; y++)
            {
                for (x = 0; x < XX1 * 2 + 1; x++)
                {
                    if ((bx - XX1 + x) < 0 || (bx - XX1 + x) >= map.Size.x)
                    {
                       // Messages.Message("xxx", MessageTypeDefOf.NeutralEvent);
                        continue;
                    }
                    else if ((bz - ZZ1 + y) < 0 || bz - ZZ1 + y >= map.Size.z)
                    {
                        //Messages.Message("zzz", MessageTypeDefOf.NeutralEvent);
                        continue;
                    }

                    if (is_ellipse(XX1, ZZ1, x - XX1, y - ZZ1)) // 체크!
                    {
                        // 블루프린트 설치
                        //grid[y, x] = true;
                        printVec = this.Position;
                        printVec.x = bx - XX1 + x;
                        printVec.z = bz - ZZ1 + y;

                        //Messages.Message(bx + "  " + x  + "  " + bz + "  "+y+ "bphere"+ "  "+ "pv" + printVec.x + " " + printVec.z + " " + XX1 + " " + ZZ1, MessageTypeDefOf.NeutralEvent);

                        GenConstruct.PlaceBlueprintForBuild(ThingDefOf.Wall, printVec, this.Map, base.Rotation, Faction.OfPlayer, stuff);

                    }
                }
            }
            //GenConstruct.PlaceBlueprintForBuild(ThingDefOf.Wall, base.Position, this.Map, base.Rotation, Faction.OfPlayer, ThingDefOf.WoodLog);
        }

        private bool is_ellipse(double a, double b, double x, double y)
        {
            //EPSILON : change errors.
            const float EPSILON = 0.5f;
            double tmp1, tmp2;

            if (a < x) return false;
            tmp1 = (b / a) * Math.Sqrt(a * a - x * x);
            tmp2 = (b / a) * Math.Sqrt(a * a - x * x) * -1;
            if (Math.Abs(tmp1 - y) < EPSILON) return true;
            else if (Math.Abs(tmp2 - y) < EPSILON) return true;

            if (b < y) return false;
            tmp1 = (a / b) * Math.Sqrt(b * b - y * y);
            tmp2 = (a / b) * Math.Sqrt(b * b - y * y) * -1;
            if (Math.Abs(tmp1 - x) < EPSILON) return true;
            else if (Math.Abs(tmp2 - x) < EPSILON) return true;
            return false;
        }
    }


    public class Diagonalff : Building
    {
        IntVec3 startpoint;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            foreach (Building b in Current.Game.CurrentMap.listerBuildings.allBuildingsColonist)
            {
                if (b.def.defName.Equals("CAOstartpoint"))
                {

                    startpoint = b.Position;
                    if(startpoint.x == Position.x || startpoint.z == Position.z)
                    {
                        Messages.Message("There is a problem with coordinates. Z axis or X axis is duplicated.".Translate(), MessageTypeDefOf.NeutralEvent);
                        return;
                    }

                    MakeD((Position.x - startpoint.x), (Position.z - startpoint.z), Position.x, Position.z, this.Stuff);
                    return;
                }
            }
            // 못찾음
            Messages.Message("No_CAOstartpoint".Translate(), MessageTypeDefOf.NeutralEvent);

        }

        private void MakeD(int XX1, int ZZ1, int bx, int bz, ThingDef stuff)
        {
            Map map = this.Map;
            int xp, yp;

            if (XX1 > 0)
                xp = 1;
            else xp = -1;
            if (ZZ1 > 0)
                yp = 1;
            else yp = -1;

            IntVec3 printVec = startpoint;

            while (true)
            {
                printVec.x += xp;
                printVec.z += yp;

                GenConstruct.PlaceBlueprintForBuild(ThingDefOf.Wall, printVec, this.Map, base.Rotation, Faction.OfPlayer, stuff);

                if(printVec.x == bx || printVec.z == bz)
                {
                    return;
                }
            }
            //GenConstruct.PlaceBlueprintForBuild(ThingDefOf.Wall, base.Position, this.Map, base.Rotation, Faction.OfPlayer, ThingDefOf.WoodLog);
        }
    }



    /*


    public class CurveendpointR : Building
    {
        IntVec3 startpoint;
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            foreach (Building b in Current.Game.CurrentMap.listerBuildings.allBuildingsColonist)
            {
                if (b.def.defName.Equals("MLCstartpoint"))
                {

                    startpoint = b.Position;
                    MakeCR((Position.x - startpoint.x), (Position.z - startpoint.z), b.Position.x, b.Position.z);
                    return;
                }
            }
        }

        private void MakeCR(int XX1, int ZZ1, int bx, int bz)
        {
            Map map = this.Map;

            int Rec;
            if(XX1 < 0)
            {
                if (ZZ1 < 0)
                {
                    Rec = 3;
                }
                else Rec = 4;
            }
            else
            {
                if (ZZ1 < 0)
                {
                    Rec = 2;
                }
                else Rec = 1;
            }

            IntVec3 printVec;
            bool[,] grid = new bool[XX1 + 1, ZZ1 + 1];
            int y = 0;
            int x = 0;

            for (y = 0; y < ZZ1 +1; y++)
            {
                for (x = 0; x < XX1 + 1; x++)
                {

                    if (is_ellipse(XX1, ZZ1, x - XX1, y - ZZ1)) // 체크!
                    {
                        // 블루프린트 설치
                        //grid[y, x] = true;
                        printVec = this.Position;
                        printVec.x = bx + x;
                        printVec.z = bz + y;

                        //Messages.Message(bx + "  " + x + "  " + bz + "  " + y + "bphere" + "  " + "pv" + printVec.x + " " + printVec.z + " " + XX1 + " " + ZZ1, MessageTypeDefOf.NeutralEvent);

                        GenConstruct.PlaceBlueprintForBuild(ThingDefOf.Wall, printVec, this.Map, base.Rotation, Faction.OfPlayer, ThingDefOf.WoodLog);

                    }
                }
            }
            //GenConstruct.PlaceBlueprintForBuild(ThingDefOf.Wall, base.Position, this.Map, base.Rotation, Faction.OfPlayer, ThingDefOf.WoodLog);
        }

        private bool is_ellipse(double a, double b, double x, double y)
        {
            //EPSILON : change errors.
            const float EPSILON = 0.5f;
            double tmp1, tmp2;

            if (a < x) return false;
            tmp1 = (b / a) * Math.Sqrt(a * a - x * x);
            tmp2 = (b / a) * Math.Sqrt(a * a - x * x) * -1;
            if (Math.Abs(tmp1 - y) < EPSILON) return true;
            else if (Math.Abs(tmp2 - y) < EPSILON) return true;

            if (b < y) return false;
            tmp1 = (a / b) * Math.Sqrt(b * b - y * y);
            tmp2 = (a / b) * Math.Sqrt(b * b - y * y) * -1;
            if (Math.Abs(tmp1 - x) < EPSILON) return true;
            else if (Math.Abs(tmp2 - x) < EPSILON) return true;
            return false;
        }
    }




    public class CurveendpointL : Building
    {
        IntVec3 startpoint;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            foreach (Building b in Current.Game.CurrentMap.listerBuildings.allBuildingsColonist)
            {
                if (b.def.defName.Equals("MLCstartpoint"))
                {
                    startpoint = b.Position;
                    break;
                }
            }
        }


    }
    
    */


}
