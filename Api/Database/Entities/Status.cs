using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftLineTestProj.Database.Entities
{
    public class Status
    {
        [Key]
        public int Status_ID {  get; set; }
        public string Status_name {  get; set; }
    }
}
