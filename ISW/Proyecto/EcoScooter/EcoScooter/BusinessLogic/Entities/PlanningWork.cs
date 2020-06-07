namespace EcoScooter.Entities
{
    public partial class PlanningWork
    {
        public PlanningWork()
        {
        }

        public PlanningWork(string description, int workingTime, Maintenance maintenance, Scooter scooter)
        {
            Description = description;
            WorkingTime = workingTime;
            Maintenance = maintenance;
            Scooter = scooter;
        }
    }
}
