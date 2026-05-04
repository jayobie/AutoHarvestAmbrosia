using RimWorld;
using Verse;

namespace AutoHarvestAmbrosia
{
    // links the XML to the ThingComp
    public class CompProperties_AutoHarvest : CompProperties
    {
        public CompProperties_AutoHarvest()
        {
            this.compClass = typeof(CompAutoHarvest);
        }
    }

    public class CompAutoHarvest : ThingComp
    {
        bool designatedForHarvest = false;

        public override void CompTickLong()
        {
            base.CompTickLong();

            if (this.parent is Plant plant && plant.Spawned)
            {
                // Check if the plant is fully grown 
                if (!designatedForHarvest && plant.HarvestableNow && plant.Growth >= 1f)
                {
                    designatedForHarvest = true; // Make sure we only do the check once,
                    //so we don't check every tick after its already been desginated for harvest
                    //and also allows player to cancel the harvest if they want to,
                    //and it won't be re-designated on the next tick

                    // Check if it's already marked for harvest to prevent duplicate designations
                    if (plant.Map.designationManager.DesignationOn(plant, DesignationDefOf.HarvestPlant) == null)
                    {
                        // Add the harvest designation
                        plant.Map.designationManager.AddDesignation(new Designation(plant, DesignationDefOf.HarvestPlant));

                    }
                }
            }
        }
    }
}
