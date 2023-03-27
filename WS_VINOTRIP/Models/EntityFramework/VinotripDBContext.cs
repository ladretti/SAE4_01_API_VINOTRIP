using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.EntityFramework
{
    public partial class VinotripDBContext : DbContext
    {
        public VinotripDBContext()
        { }

        public VinotripDBContext(DbContextOptions<VinotripDBContext> options)
        : base(options)
        { }

        public virtual DbSet<Sejour>? Sejours { get; set; }
        public virtual DbSet<CatParticipant>? CatsParticipant { get; set; }
        public virtual DbSet<Comporte>? Comportes { get; set; }
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Avis>? Aviss { get; set; }
        public virtual DbSet<RouteDesVins>? RoutesDesVins { get; set; }
        public virtual DbSet<Vignoble>? Vignobles { get; set; }
        public virtual DbSet<CatSejour>? CatsSejour { get; set; }
        public virtual DbSet<CatVignoble>? CatsVignoble { get; set; }
        public virtual DbSet<Lien>? Liens { get; set; }
        public virtual DbSet<LienSejour>? LienSejours { get; set; }
        public virtual DbSet<LienRouteDesVins>? LienRouteDesVinss { get; set; }
        public virtual DbSet<Etape>? Etapes { get; set; }
        public virtual DbSet<ElementVignoble>? ElementsVignoble { get; set; }
        public virtual DbSet<LienElementVignoble>? LiensElementVignoble { get; set; }
        public virtual DbSet<Personne>? Personnes { get; set; }
        public virtual DbSet<ElementEtape>? ElementEtapes { get; set; }
        public virtual DbSet<Concerne>? Concernes { get; set; }
        public virtual DbSet<Panier>? Paniers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=51.83.36.122;port=5432;Database=vinotrique;SearchPath=vinotrip; uid=s222; password=8F1ASd"); //à changer

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasKey(e => new { e.AdresseId })
                    .HasName("pk_ads");

                entity.HasCheckConstraint("ck_ads_rue1_rue2", "ads_rue1 <> ads_rue2");
            });

            modelBuilder.Entity<Avis>(entity =>
            {
                entity.HasKey(e => new { e.AvisId })
                    .HasName("pk_avi");

                entity.HasOne(d => d.PersonneAvis).WithMany(p => p.AvisPersonne)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_avi_prs");

                entity.HasOne(d => d.SejourAvis).WithMany(p => p.AvisSejour)
                    .HasForeignKey(d => d.SejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_avi_sjr");

                entity.HasCheckConstraint("ck_avi_note", "avi_note between 1 and 5");
            });

            modelBuilder.Entity<BonCadeau>(entity =>
            {
                entity.HasKey(e => new { e.BonCadeauId })
                    .HasName("pk_bcd");

                entity.HasAlternateKey(u => u.CodeReduction)
                    .HasName("uq_bcd_codereduction");
            });

            modelBuilder.Entity<CatParticipant>(entity =>
            {
                entity.HasKey(e => new { e.CatParticipantId })
                    .HasName("pk_cpp");

                entity.HasOne(d => d.LienCatParticipant).WithMany(p => p.CatParticipantLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cpp_len");
            });

            modelBuilder.Entity<CatSejour>(entity =>
            {
                entity.HasKey(e => new { e.CatSejourId })
                    .HasName("pk_csj");

                entity.HasOne(d => d.LienCatSejour).WithMany(p => p.CatSejourLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_csj_len");
            });

            modelBuilder.Entity<CatVignoble>(entity =>
            {
                entity.HasKey(e => new { e.CatVignobleId })
                    .HasName("pk_cvg");
            });

            modelBuilder.Entity<ChequeCadeau>(entity =>
            {
                entity.HasKey(e => new { e.BonCadeauId })
                    .HasName("pk_cqc");

                entity.HasOne(d => d.BonCadeauChequeCadeau).WithMany(p => p.ChequeCadeauBonCadeau)
                    .HasForeignKey(d => d.BonCadeauId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cqc_bcd");
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => new { e.CommandeId })
                    .HasName("pk_cmd");

                entity.HasOne(d => d.UserCommande).WithMany(p => p.CommandeUser)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cmd_cmp");
            });

            modelBuilder.Entity<Comporte>(entity =>
            {
                entity.HasKey(e => new { e.SejourId, e.CatParticipantId })
                    .HasName("pk_cpt");

                entity.HasOne(d => d.SejourComporte).WithMany(p => p.ComporteSejour)
                    .HasForeignKey(d => d.SejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cpt_sjr");

                entity.HasOne(d => d.CatParticipantComporte).WithMany(p => p.ComporteCatParticipant)
                    .HasForeignKey(d => d.CatParticipantId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cpt_cpp");
            });

            modelBuilder.Entity<CompteCarte>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId, e.CarteId })
                    .HasName("pk_cpc");

                entity.HasOne(d => d.UserCompteCarte).WithMany(p => p.CompteCarteUser)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cpc_cmp");

                entity.HasOne(d => d.RefCarteBancaireCompteCarte).WithMany(p => p.CompteCarteRefCarteBancaire)
                    .HasForeignKey(d => d.CarteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cpc_rcb");
            });

            modelBuilder.Entity<Concerne>(entity =>
            {
                entity.HasKey(e => new { e.EtapeId, e.ElementId })
                    .HasName("pk_ccr");

                entity.HasOne(d => d.EtapeConcerne).WithMany(p => p.ConcerneEtape)
                    .HasForeignKey(d => d.EtapeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ccr_etp");

                entity.HasOne(d => d.ElementEtapeConcerne).WithMany(p => p.ConcerneElementEtape)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ccr_ele");
            });

            modelBuilder.Entity<Contient>(entity =>
            {
                entity.HasKey(e => new { e.LienId, e.ElementId })
                    .HasName("pk_ctn");

                entity.HasOne(d => d.LienContient).WithMany(p => p.ContientLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ctn_len");

                entity.HasOne(d => d.ElementEtapeContient).WithMany(p => p.ContientElementEtape)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ctn_ele");
            });

            modelBuilder.Entity<ElementEtape>(entity =>
            {
                entity.HasKey(e => new { e.ElementId })
                    .HasName("pk_ele");

                entity.HasOne(d => d.PartenaireElementEtape).WithMany(p => p.ElementsEtapePartenaire)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ele_ptn");

                entity.HasOne(d => d.TypeElementEtapeElementEtape).WithMany(p => p.ElementEtapeTypeElementEtape)
                    .HasForeignKey(d => d.TypeElementId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ele_tpe");
            });

            modelBuilder.Entity<ElementVignoble>(entity =>
            {
                entity.HasKey(e => new { e.ElementVignobleId })
                    .HasName("pk_evg");

                entity.HasOne(d => d.VignobleElementVignoble).WithMany(p => p.ElementVignobleVignoble)
                    .HasForeignKey(d => d.VignobleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_evg_vgb");
            });

            modelBuilder.Entity<Est_facturee>(entity =>
            {
                entity.HasKey(e => new { e.ReservationId, e.AdresseId })
                    .HasName("pk_efc");

                entity.HasOne(d => d.ReservationEst_facturee).WithMany(p => p.Est_factureeReservation)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_efc_rsv");

                entity.HasOne(d => d.AdresseEst_facturee).WithMany(p => p.Est_factureeAdresse)
                    .HasForeignKey(d => d.AdresseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_efc_ads");
            });

            modelBuilder.Entity<Etape>(entity =>
            {
                entity.HasKey(e => new { e.EtapeId })
                    .HasName("pk_etp");

                entity.HasOne(d => d.SejourEtape).WithMany(p => p.EtapesSejour)
                    .HasForeignKey(d => d.SejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_etp_sjr");
            });

            modelBuilder.Entity<Favori>(entity =>
            {
                entity.HasKey(e => new { e.SejourId, e.PersonneId })
                    .HasName("pk_fav");

                entity.HasOne(d => d.SejourFavori).WithMany(p => p.FavoriSejour)
                    .HasForeignKey(d => d.SejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_fav_sjr");

                entity.HasOne(d => d.UserFavori).WithMany(p => p.FavoriUser)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_fav_clt");
            });

            modelBuilder.Entity<HistoriqueCadeau>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId, e.BonCadeauId })
                    .HasName("pk_htc");

                entity.HasOne(d => d.UserHistoriqueCadeau).WithMany(p => p.HistoriqueCadeauUser)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_htc_clt");

                entity.HasOne(d => d.BonCadeauHistoriqueCadeau).WithMany(p => p.HistoriqueCadeauBonCadeau)
                    .HasForeignKey(d => d.BonCadeauId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_htc_bcd");
            });

            modelBuilder.Entity<Lien>(entity =>
            {
                entity.HasKey(e => new { e.LienId })
                    .HasName("pk_len");
            });

            modelBuilder.Entity<LienAvis>(entity =>
            {
                entity.HasKey(e => new { e.LienId, e.AvisId })
                    .HasName("pk_lav");

                entity.HasOne(d => d.LienLienAvis).WithMany(p => p.LiensAvisLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lav_len");

                entity.HasOne(d => d.AvisLienAvis).WithMany(p => p.LiensAvisAvis)
                    .HasForeignKey(d => d.AvisId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lav_avi");
            });

            modelBuilder.Entity<LienElementVignoble>(entity =>
            {
                entity.HasKey(e => new { e.LienId, e.ElementVignobleId })
                    .HasName("pk_lev");

                entity.HasOne(d => d.LienLienElementVignoble).WithMany(p => p.LienElementVignobleLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lev_len");

                entity.HasOne(d => d.ElementVignobleLienElementVignoble).WithMany(p => p.LienElementVignobleElementVignoble)
                    .HasForeignKey(d => d.ElementVignobleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lev_evg");
            });

            modelBuilder.Entity<LienEtape>(entity =>
            {
                entity.HasKey(e => new { e.LienId, e.EtapeId })
                    .HasName("pk_lep");

                entity.HasOne(d => d.LienLienEtape).WithMany(p => p.LienEtapeLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lep_len");

                entity.HasOne(d => d.EtapeLienEtape).WithMany(p => p.LienEtapeEtape)
                    .HasForeignKey(d => d.EtapeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lep_etp");
            });

            modelBuilder.Entity<LienRouteDesVins>(entity =>
            {
                entity.HasKey(e => new { e.LienId, e.RouteDesVinsId })
                    .HasName("pk_lrv");

                entity.HasOne(d => d.LienLienRouteDesVins).WithMany(p => p.LienRouteDesVinsLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lrv_len");

                entity.HasOne(d => d.RouteDesVinsLienRouteDesVins).WithMany(p => p.LienRouteDesVinsRouteDesVins)
                    .HasForeignKey(d => d.RouteDesVinsId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lrv_rdv");
            });

            modelBuilder.Entity<LienSejour>(entity =>
            {
                entity.HasKey(e => new { e.LienId, e.SejourId })
                    .HasName("pk_lsj");

                entity.HasOne(d => d.LienLienSejour).WithMany(p => p.LienSejourLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lsj_len");

                entity.HasOne(d => d.SejourLienSejour).WithMany(p => p.LienSejourSejour)
                    .HasForeignKey(d => d.SejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_lsj_sjr");
            });

            modelBuilder.Entity<Panier>(entity =>
            {
                entity.HasKey(e => new { e.SejourId, e.PersonneId })
                    .HasName("pk_pnr");

                entity.HasOne(d => d.SejourPanier).WithMany(p => p.PanierSejour)
                    .HasForeignKey(d => d.SejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pnr_sjr");

                entity.HasOne(d => d.UserPanier).WithMany(p => p.PanierUser)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pnr_cmp");
            });

            modelBuilder.Entity<Partenaire>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId })
                    .HasName("pk_ptn");

                entity.HasOne(d => d.PersonnePartenaire).WithMany(p => p.PartenairePersonne)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ptn_prs");
            });

            modelBuilder.Entity<PartenaireActivite>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId })
                    .HasName("pk_pta");

                entity.HasOne(d => d.PartenairePartenaireActivite).WithMany(p => p.PartenairesActivitePartenaire)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pta_ptn");
            });

            modelBuilder.Entity<PartenaireCave>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId })
                    .HasName("pk_ptc");

                entity.HasOne(d => d.PartenairePartenaireCave).WithMany(p => p.PartenairesCavePartenaire)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ptc_ptn");
            });

            modelBuilder.Entity<PartenaireHebergement>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId })
                    .HasName("pk_pth");

                entity.HasOne(d => d.PartenairePartenaireHebergement).WithMany(p => p.PartenairesHebergementPartenaire)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pth_ptn");

                entity.HasCheckConstraint("ck_pth_etoiles", "pth_etoiles between 0 and 5");
            });

            modelBuilder.Entity<PartenaireRestaurant>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId })
                    .HasName("pk_ptr");

                entity.HasOne(d => d.PartenairePartenaireRestaurant).WithMany(p => p.PartenairesRestaurantPartenaire)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ptr_ptn");

                entity.HasCheckConstraint("ck_ptr_etoiles", "ptr_etoiles between 0 and 5");
            });

            modelBuilder.Entity<Passe>(entity =>
            {
                entity.HasKey(e => new { e.ReservationId, e.CommandeId })
                    .HasName("pk_pss");

                entity.HasOne(d => d.ReservationPasse).WithMany(p => p.PasseReservation)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pss_rsv");

                entity.HasOne(d => d.CommandePasse).WithMany(p => p.PasseCommande)
                    .HasForeignKey(d => d.CommandeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pss_cmd");
            });

            modelBuilder.Entity<Paye>(entity =>
            {
                entity.HasKey(e => new { e.ReservationId, e.TypePayementId })
                    .HasName("pk_pay");

                entity.HasOne(d => d.ReservationPaye).WithMany(p => p.PayeReservation)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pay_rsv");

                entity.HasOne(d => d.TypePayementPaye).WithMany(p => p.PayeTypePayement)
                    .HasForeignKey(d => d.TypePayementId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pay_tpa");
            });

            modelBuilder.Entity<Personne>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId })
                    .HasName("pk_prs");

                entity.HasCheckConstraint("ck_prs_mail", "prs_mail like '%@%.%'");

                entity.HasAlternateKey(u => u.Mail)
                    .HasName("uq_prs_mail");
            });

            modelBuilder.Entity<RefCarteBancaire>(entity =>
            {
                entity.HasKey(e => new { e.CarteId })
                    .HasName("pk_rcb");
            });

            modelBuilder.Entity<Reported>(entity =>
            {
                entity.HasKey(e => new { e.AvisId, e.PersonneId })
                    .HasName("pk_rep");

                entity.HasOne(d => d.AvisReported).WithMany(p => p.ReportedAvis)
                    .HasForeignKey(d => d.AvisId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rep_avi");

                entity.HasOne(d => d.UserReported).WithMany(p => p.ReportedUser)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rep_cmp");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => new { e.ReservationId })
                    .HasName("pk_rsv");

                entity.HasOne(d => d.SejourReservation).WithMany(p => p.ReservationsSejour)
                    .HasForeignKey(d => d.SejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rsv_sjr");

                entity.HasOne(d => d.BonCadeauReservation).WithMany(p => p.ReservationBonCadeau)
                    .HasForeignKey(d => d.BonCadeauId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rsv_bcd");
            });

            modelBuilder.Entity<Reside>(entity =>
            {
                entity.HasKey(e => new { e.AdresseId, e.PersonneId })
                    .HasName("pk_rsd");

                entity.HasOne(d => d.AdresseReside).WithMany(p => p.ResideAdresse)
                    .HasForeignKey(d => d.AdresseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rsd_ads");

                entity.HasOne(d => d.UserReside).WithMany(p => p.ResideUser)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rsd_cmp");
            });

                modelBuilder.Entity<RouteDesVins>(entity =>
            {
                entity.HasKey(e => new { e.RouteDesVinsId })
                    .HasName("pk_rdv");

                entity.HasOne(d => d.VignobleRouteDesVins).WithMany(p => p.RouteDesVinsVignoble)
                    .HasForeignKey(d => d.VignobleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rdv_vgb");
            });

            modelBuilder.Entity<Sejour>(entity =>
            {
                entity.HasKey(e => new { e.SejourId })
                    .HasName("pk_sjr");

                entity.HasOne(d => d.RouteDesVinsSejour).WithMany(p => p.SejourRouteDesVins)
                    .HasForeignKey(d => d.RouteVinId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_sjr_rdv");

                entity.HasOne(d => d.SejourCatSejour).WithMany(p => p.CatSejoursSejour)
                    .HasForeignKey(d => d.CatSejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_sjr_csj");

                entity.HasOne(d => d.SejourCatVignoble).WithMany(p => p.CatVignobleSejour)
                    .HasForeignKey(d => d.CatVignobleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_sjr_cvg");
            });

            modelBuilder.Entity<SejourCadeau>(entity =>
            {
                entity.HasKey(e => new { e.BonCadeauId })
                    .HasName("pk_sjc");

                entity.HasOne(d => d.BonCadeauSejourCadeau).WithMany(p => p.SejourCadeauBonCadeau)
                    .HasForeignKey(d => d.BonCadeauId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_sjc_bcd");

                entity.HasOne(d => d.SejourSejourCadeau).WithMany(p => p.SejourCadeauSejour)
                    .HasForeignKey(d => d.SejourId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_sjc_sjr");
            });

            modelBuilder.Entity<TypeElementEtape>(entity =>
            {
                entity.HasKey(e => new { e.TypeElementEtapeId })
                    .HasName("pk_tpe");
            });

            modelBuilder.Entity<TypePayement>(entity =>
            {
                entity.HasKey(e => new { e.TypePayementId })
                    .HasName("pk_tpa");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.PersonneId })
                    .HasName("pk_usr");

                entity.HasOne(d => d.PersonneUser).WithMany(p => p.UserPersonne)
                    .HasForeignKey(d => d.PersonneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_usr_prs");

                entity.HasCheckConstraint("ck_usr_datenaissance", "now() - usr_datenaissance > INTERVAL '6570 days'");
                entity.HasCheckConstraint("ck_usr_tel", "usr_tel like '06%' or usr_tel like '07%'");

                entity.HasAlternateKey(u => u.Pseudo)
                    .HasName("uq_usr_pseudo");
            });

            modelBuilder.Entity<Vignoble>(entity =>
            {
                entity.HasKey(e => new { e.VignobleId })
                    .HasName("pk_vgb");

                entity.HasOne(d => d.LienVignoble).WithMany(p => p.VignobleLien)
                    .HasForeignKey(d => d.LienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_vgb_len");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        
    }
}
