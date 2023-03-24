using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WS_VINOTRIP.Models.EntityFramework
{
    [Table("t_j_concerne_ccr")]
    public partial class Concerne
    {
        //Property

        [Required, Column("etp_id")]
        public int EtapeId { get; set; }

        [Required, Column("ele_id")]
        public int ElementId { get; set; }
        
        [Required, Column("ccr_horaire", TypeName = "time")]
        public TimeOnly? Horaire { get; set; }

        //InverseProperty

        [ForeignKey("EtapeId")]
        [InverseProperty("ConcerneEtape")]
        public virtual Etape? EtapeConcerne { get; set; }

        [ForeignKey("ElementId")]
        [InverseProperty("ConcerneElementEtape")]
        public virtual ElementEtape? ElementEtapeConcerne { get; set; }
    }
}