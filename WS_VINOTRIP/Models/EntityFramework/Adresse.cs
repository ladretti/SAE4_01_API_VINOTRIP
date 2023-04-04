using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS_VINOTRIP.Models.EntityFramework
{
    [Table("t_e_adresse_ads")]
    public partial class Adresse
    {
        //Property

        [Key, Column("ads_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdresseId { get; set; }

        [Required, Column("ads_rue1"), StringLength(100)]
        public string? Rue1 { get; set; }

        [Column("ads_rue2"), StringLength(100)]
        public string? Rue2 { get; set; }

        [Required, Column("ads_cp", TypeName = "char(5)")]
        public string Cp { get; set; }

        [Required, Column("ads_ville"), StringLength(30)]
        public string? Ville { get; set; }

        [Required, Column("ads_pays"), StringLength(30)]
        public string? Pays { get; set; }

        //InverseProperty

        [InverseProperty("AdresseEst_facturee")]
        public virtual ICollection<Est_facturee>? Est_factureeAdresse { get; }

        [InverseProperty("AdresseReside")]
        public virtual ICollection<Reside>? ResideAdresse { get; }
    }
}
