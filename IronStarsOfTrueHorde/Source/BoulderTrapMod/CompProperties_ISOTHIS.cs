using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;


namespace ISOTH
{
    /*
    [StaticConstructorOnStartup]
    public class ISOTHIS_SHELL
    {
        public static readonly Material ballimage1;
        public static readonly Material ProjectileMat;

        static ISOTHIS_SHELL()
        {
            ISOTHIS_SHELL.ProjectileMat = MaterialPool.MatFrom("Things/ISOTH_0");

            //ISOTHIS_SHELL.ProjectileMat = MaterialPool.MatFrom("Things/ISOTH_0", ShaderDatabase.MetaOverlay);

        }
        public static void SHELLdraw(Thing t)
        {
            Graphics.DrawMesh(MeshPool.plane05, t.DrawPos, Quaternion.identity, ISOTHIS_SHELL.ProjectileMat, 0);
        }
    }

    */
        public class CompProperties_ISOTHIS : CompProperties
    {
        public CompProperties_ISOTHIS()
        {
            this.compClass = typeof(ISOTHIS_comp);
        }
    }


    public class ISOTHIS_comp : ThingComp
    {
        public CompProperties_ISOTHIS Props
        {
            get
            {
                return (CompProperties_ISOTHIS)this.props;
            }
        }


        

        protected CompChangeableProjectile compChangeableProjectile;

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            
            this.compChangeableProjectile = this.parent.GetComp<CompChangeableProjectile>();


        }

        //Vector2 picsize = 2;
        




        public override void PostDraw()
        {
            base.PostDraw();

            
            //if (compChangeableProjectile.Loaded)
            //{

                //Verse.Log.Message("loaded", false);

                //ISOTHIS_SHELL.SHELLdraw(parent);

                //Mesh mesh = MeshPool.GridPlane(parent.def.graphicData.drawSize);

                //Vector3 b = new Vector3(parent.Position.x, 0f, parent.Position.y);


                

                //Graphics.DrawMesh(mesh, b, Quaternion.identity, ISOTHIS_SHELL.ProjectileMat, 0);


                
                //float turretTopDrawSize = 2f;
                //Matrix4x4 matrix = default(Matrix4x4);
                
                //matrix.SetTRS(parent.DrawPos + Altitudes.AltIncVect + b, parent.Rotation.AsQuat, new Vector3(turretTopDrawSize, 1f, turretTopDrawSize));
                //Graphics.DrawMesh(MeshPool.plane10, matrix, parent.def.building., 0);




                
                //Graphics.DrawMesh(mesh, this.DrawPos, this.ExactRotation, this.def.DrawMatSingle, 0);
                //base.Comps_PostDraw();

                //Graphics.DrawMesh(mesh, parent.DrawPos, parent., ballimage1, 0);
            //}
        }

       

    }
}