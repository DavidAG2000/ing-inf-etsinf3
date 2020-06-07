using System;

namespace EcoScooter.Entities
{
    public partial class Incident
    {
        public Incident()
        {
        }

        public Incident(String description, DateTime timeStamp)
        {
            Description = description;
            TimeStamp = timeStamp;
        }
    }
}
