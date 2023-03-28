using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WS_VINOTRIP.Models.EntityFramework
{
    [Table("t_e_panier_pnr")]
    public partial class Panier
    {
        //Property

        [Required, Column("prs_id")]
        public int PersonneId { get; set; }

        [Required, Column("sjr_id")]
        public int SejourId { get; set; }

        [Required, Column("pnr_nbadultes")]
        public int NbAdultes { get; set; }

        [Required, Column("pnr_nbenfants")]
        public int NbEnfants{ get; set; }

        [Required, Column("pnr_nbchambres")]
        public int NbChambres { get; set; }

        [Required, Column("pnr_offert"), Key]
        public bool Offert { get; set; }

        [Required, Column("pnr_prixtotal", TypeName = "numeric(9,2)")]
        public double PrixTotal { get; set; }

        [Required, Column("pnr_datesejour", TypeName = "date")]
        public DateTime DateSejour { get; set; }

        //InverseProperty

        [ForeignKey("SejourId")]
        [InverseProperty("PanierSejour")]
        public virtual Sejour? SejourPanier { get; set; }

        [ForeignKey("PersonneId")]
        [InverseProperty("PanierUser")]
        public virtual User? UserPanier { get; set; }
    }
}
