using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaWeb.Models
{
    public class VillaNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }
        [ForeignKey("villa")]
        public int VillaId { get; set; }
        public Villa villa { get; set; }
        public string specialdetils { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Updatedate { get; set;}
    }
}
