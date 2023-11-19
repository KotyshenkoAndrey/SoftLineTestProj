using System.ComponentModel.DataAnnotations;

namespace SoftLineTestProj.Database.Entities
{
    public class TaskDb
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status_ID { get; set; }
        public Status Status { get; set; }
    }
}
