using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS_VINOTRIP.Models.EntityFramework
{
    [Table("t_e_reservation_rsv")]
    public partial class Reservation
    {
        //Property

        [Key, Column("rsv_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        [Required, Column("sjr_id")]
        public int SejourId { get; set; }

        [Column("bcd_id")]
        public int? BonCadeauId { get; set; }

        [Required, Column("rsv_nbadultes")]
        public int NbAdultes { get; set; }

        [Required, Column("rsv_nbenfants")]
        public int NbEnfants { get; set; }

        [Required, Column("rsv_nbchambres")]
        public int NbChambres { get; set; }

        [Required, Column("rsv_prixtotal", TypeName = "numeric(9,2)")]
        public double PrixTotal { get; set; }

        [Required, Column("rsv_datedebutresa", TypeName = "date")]
        public DateTime DateDebutResa{ get; set; }

        [Required, Column("rsv_datefinresa", TypeName = "date")]
        public DateTime DateFinResa{ get; set; }

        [Required, Column("rsv_datefacture", TypeName = "date")]
        public DateTime DateFacture { get; set; }

        //InverseProperty

        [ForeignKey("SejourId")]
        [InverseProperty("ReservationsSejour")]
        public virtual Sejour? SejourReservation { get; set; } 

        [ForeignKey("BonCadeauId")]
        [InverseProperty("ReservationBonCadeau")]
        public virtual BonCadeau? BonCadeauReservation { get; set; }

        [InverseProperty("ReservationPasse")]
        public virtual ICollection<Passe>? PasseReservation { get; }

        [InverseProperty("ReservationEst_facturee")]
        public virtual ICollection<Est_facturee>? Est_factureeReservation { get; }

        [InverseProperty("ReservationPaye")]
        public virtual ICollection<Paye>? PayeReservation { get; }
    }
}
