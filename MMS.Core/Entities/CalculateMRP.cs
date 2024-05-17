namespace MMS.Core.Entities
{
    public class CalculateMRP : BaseEntity
    {
        public long MRPCalculateID { get; set; }
        public int MRPIdToCalculate { get; set; }

        //public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public bool IsCalculated { get; set; }

        public string HostName { get; set; }
    }
}
