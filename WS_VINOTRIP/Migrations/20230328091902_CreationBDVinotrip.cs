using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WS_VINOTRIP.Migrations
{
    public partial class CreationBDVinotrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_adresse_ads",
                columns: table => new
                {
                    ads_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ads_rue1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ads_rue2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ads_cp = table.Column<int>(type: "char(5)", nullable: false),
                    ads_ville = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ads_pays = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ads", x => x.ads_id);
                    table.CheckConstraint("ck_ads_rue1_rue2", "ads_rue1 <> ads_rue2");
                });

            migrationBuilder.CreateTable(
                name: "t_e_boncadeau_bcd",
                columns: table => new
                {
                    bcd_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bcd_dateexpirationbon = table.Column<DateTime>(type: "date", nullable: false),
                    bcd_codereduction = table.Column<string>(type: "char(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bcd", x => x.bcd_id);
                    table.UniqueConstraint("uq_bcd_codereduction", x => x.bcd_codereduction);
                });

            migrationBuilder.CreateTable(
                name: "t_e_catvignoble_cvg",
                columns: table => new
                {
                    cvg_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cvg_libelle = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    cvg_description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cvg", x => x.cvg_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_lien_len",
                columns: table => new
                {
                    len_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    len_url = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    len_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_len", x => x.len_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_personne_prs",
                columns: table => new
                {
                    prs_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prs_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    prs_mail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prs", x => x.prs_id);
                    table.UniqueConstraint("uq_prs_mail", x => x.prs_mail);
                    table.CheckConstraint("ck_prs_mail", "prs_mail like '%@%.%'");
                });

            migrationBuilder.CreateTable(
                name: "t_e_refcartebancaire_rcb",
                columns: table => new
                {
                    rcb_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rcb_numcarte = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    rcb_dateexpirationcarte = table.Column<DateTime>(type: "date", nullable: false),
                    rcb_nomcarte = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rcb", x => x.rcb_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeelementetape_tpe",
                columns: table => new
                {
                    tpe_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpe_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tpe", x => x.tpe_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typepayement_tpa",
                columns: table => new
                {
                    tpa_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpa_libelle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tpa", x => x.tpa_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_chequecadeau_cqc",
                columns: table => new
                {
                    bcd_id = table.Column<int>(type: "integer", nullable: false),
                    cqc_montant = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cqc", x => x.bcd_id);
                    table.ForeignKey(
                        name: "fk_cqc_bcd",
                        column: x => x.bcd_id,
                        principalTable: "t_e_boncadeau_bcd",
                        principalColumn: "bcd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_catparticipant_cpp",
                columns: table => new
                {
                    cpp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vgb_titre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    len_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cpp", x => x.cpp_id);
                    table.ForeignKey(
                        name: "fk_cpp_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_catsejour_csj",
                columns: table => new
                {
                    csj_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    csj_libelle = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    len_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_csj", x => x.csj_id);
                    table.ForeignKey(
                        name: "fk_csj_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_vignoble_vgb",
                columns: table => new
                {
                    vgb_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vgb_titre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    vgb_description = table.Column<string>(type: "text", nullable: false),
                    len_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vgb", x => x.vgb_id);
                    table.ForeignKey(
                        name: "fk_vgb_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_partenaire_ptn",
                columns: table => new
                {
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    ptn_tel = table.Column<string>(type: "char(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ptn", x => x.prs_id);
                    table.ForeignKey(
                        name: "fk_ptn_prs",
                        column: x => x.prs_id,
                        principalTable: "t_e_personne_prs",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_user_usr",
                columns: table => new
                {
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    usr_titre = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    usr_prenom = table.Column<string>(type: "text", nullable: false),
                    usr_pseudo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    usr_datenaissance = table.Column<DateTime>(type: "date", nullable: false),
                    usr_tel = table.Column<string>(type: "char(10)", nullable: true),
                    usr_mdp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usr_newsletter = table.Column<bool>(type: "boolean", nullable: false),
                    usr_dateconnexion = table.Column<DateTime>(type: "date", nullable: false),
                    usr_role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usr", x => x.prs_id);
                    table.UniqueConstraint("uq_usr_pseudo", x => x.usr_pseudo);
                    table.CheckConstraint("ck_usr_datenaissance", "now() - usr_datenaissance > INTERVAL '6570 days'");
                    table.CheckConstraint("ck_usr_tel", "usr_tel like '06%' or usr_tel like '07%'");
                    table.ForeignKey(
                        name: "fk_usr_prs",
                        column: x => x.prs_id,
                        principalTable: "t_e_personne_prs",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_elementvignoble_evg",
                columns: table => new
                {
                    evg_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vgb_id = table.Column<int>(type: "integer", nullable: false),
                    evg_titre = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    evg_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_evg", x => x.evg_id);
                    table.ForeignKey(
                        name: "fk_evg_vgb",
                        column: x => x.vgb_id,
                        principalTable: "t_e_vignoble_vgb",
                        principalColumn: "vgb_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_routedesvins_rdv",
                columns: table => new
                {
                    rdv_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vgb_id = table.Column<int>(type: "integer", nullable: true),
                    rdv_titre = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    rdv_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rdv", x => x.rdv_id);
                    table.ForeignKey(
                        name: "fk_rdv_vgb",
                        column: x => x.vgb_id,
                        principalTable: "t_e_vignoble_vgb",
                        principalColumn: "vgb_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_elementetape_ele",
                columns: table => new
                {
                    ele_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    tpe_id = table.Column<int>(type: "integer", nullable: true),
                    ele_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ele_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ele", x => x.ele_id);
                    table.ForeignKey(
                        name: "fk_ele_ptn",
                        column: x => x.prs_id,
                        principalTable: "t_e_partenaire_ptn",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ele_tpe",
                        column: x => x.tpe_id,
                        principalTable: "t_e_typeelementetape_tpe",
                        principalColumn: "tpe_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_partenaireactivite_pta",
                columns: table => new
                {
                    ptn_id = table.Column<int>(type: "integer", nullable: false),
                    pta_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pta_typeactivite = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    pta_lieurdv = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pta", x => x.ptn_id);
                    table.ForeignKey(
                        name: "fk_pta_ptn",
                        column: x => x.ptn_id,
                        principalTable: "t_e_partenaire_ptn",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_partenairecave_ptc",
                columns: table => new
                {
                    ptn_id = table.Column<int>(type: "integer", nullable: false),
                    ptc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ptc_typedegustation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ptc", x => x.ptn_id);
                    table.ForeignKey(
                        name: "fk_ptc_ptn",
                        column: x => x.ptn_id,
                        principalTable: "t_e_partenaire_ptn",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_partenairehebergement_pth",
                columns: table => new
                {
                    ptn_id = table.Column<int>(type: "integer", nullable: false),
                    pth_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pth_typehebergement = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    pth_nbchambre = table.Column<int>(type: "integer", nullable: true),
                    pth_etoiles = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pth", x => x.ptn_id);
                    table.CheckConstraint("ck_pth_etoiles", "pth_etoiles between 0 and 5");
                    table.ForeignKey(
                        name: "fk_pth_ptn",
                        column: x => x.ptn_id,
                        principalTable: "t_e_partenaire_ptn",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_partenairerestaurant_ptr",
                columns: table => new
                {
                    ptn_id = table.Column<int>(type: "integer", nullable: false),
                    ptr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ptr_typecuisine = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ptr_specialite = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    ptr_etoiles = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ptr", x => x.ptn_id);
                    table.CheckConstraint("ck_ptr_etoiles", "ptr_etoiles between 0 and 5");
                    table.ForeignKey(
                        name: "fk_ptr_ptn",
                        column: x => x.ptn_id,
                        principalTable: "t_e_partenaire_ptn",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_commande_cmd",
                columns: table => new
                {
                    cmd_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    cmd_datefacture = table.Column<DateTime>(type: "date", nullable: false),
                    cmd_montantreduction = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmd", x => x.cmd_id);
                    table.ForeignKey(
                        name: "fk_cmd_cmp",
                        column: x => x.prs_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_comptecarte_cpc",
                columns: table => new
                {
                    ctl_id = table.Column<int>(type: "integer", nullable: false),
                    rcb_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cpc", x => new { x.ctl_id, x.rcb_id });
                    table.ForeignKey(
                        name: "fk_cpc_cmp",
                        column: x => x.ctl_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cpc_rcb",
                        column: x => x.rcb_id,
                        principalTable: "t_e_refcartebancaire_rcb",
                        principalColumn: "rcb_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_historiquecadeau_htc",
                columns: table => new
                {
                    ctl_id = table.Column<int>(type: "integer", nullable: false),
                    bcd_id = table.Column<int>(type: "integer", nullable: false),
                    htc_typecadeau = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_htc", x => new { x.ctl_id, x.bcd_id });
                    table.ForeignKey(
                        name: "fk_htc_bcd",
                        column: x => x.bcd_id,
                        principalTable: "t_e_boncadeau_bcd",
                        principalColumn: "bcd_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_htc_clt",
                        column: x => x.ctl_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_reside_rsd",
                columns: table => new
                {
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    ads_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rsd", x => new { x.ads_id, x.prs_id });
                    table.ForeignKey(
                        name: "fk_rsd_ads",
                        column: x => x.ads_id,
                        principalTable: "t_e_adresse_ads",
                        principalColumn: "ads_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_rsd_cmp",
                        column: x => x.prs_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_lienelementvignoble_lev",
                columns: table => new
                {
                    evg_id = table.Column<int>(type: "integer", nullable: false),
                    len_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lev", x => new { x.len_id, x.evg_id });
                    table.ForeignKey(
                        name: "fk_lev_evg",
                        column: x => x.evg_id,
                        principalTable: "t_e_elementvignoble_evg",
                        principalColumn: "evg_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_lev_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_sejour_sjr",
                columns: table => new
                {
                    sjr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rdv_id = table.Column<int>(type: "integer", nullable: true),
                    csj_id = table.Column<int>(type: "integer", nullable: false),
                    cvg_id = table.Column<int>(type: "integer", nullable: true),
                    sjr_titre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    sjr_description = table.Column<string>(type: "text", nullable: false),
                    sjr_prix = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    sjr_nbjour = table.Column<decimal>(type: "numeric(3,2)", nullable: false),
                    sjr_nbnuit = table.Column<int>(type: "integer", nullable: false),
                    sjr_promotion = table.Column<int>(type: "integer", nullable: true),
                    sjr_est_valide = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sjr", x => x.sjr_id);
                    table.ForeignKey(
                        name: "fk_sjr_csj",
                        column: x => x.csj_id,
                        principalTable: "t_e_catsejour_csj",
                        principalColumn: "csj_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sjr_cvg",
                        column: x => x.cvg_id,
                        principalTable: "t_e_catvignoble_cvg",
                        principalColumn: "cvg_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sjr_rdv",
                        column: x => x.rdv_id,
                        principalTable: "t_e_routedesvins_rdv",
                        principalColumn: "rdv_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_lienroutedesvins_lrv",
                columns: table => new
                {
                    rdv_id = table.Column<int>(type: "integer", nullable: false),
                    len_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lrv", x => new { x.len_id, x.rdv_id });
                    table.ForeignKey(
                        name: "fk_lrv_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_lrv_rdv",
                        column: x => x.rdv_id,
                        principalTable: "t_e_routedesvins_rdv",
                        principalColumn: "rdv_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_contient_ctn",
                columns: table => new
                {
                    ele_id = table.Column<int>(type: "integer", nullable: false),
                    len_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ctn", x => new { x.len_id, x.ele_id });
                    table.ForeignKey(
                        name: "fk_ctn_ele",
                        column: x => x.ele_id,
                        principalTable: "t_e_elementetape_ele",
                        principalColumn: "ele_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ctn_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_avis_avi",
                columns: table => new
                {
                    avi_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    sjr_id = table.Column<int>(type: "integer", nullable: false),
                    avi_titre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    avi_note = table.Column<int>(type: "integer", nullable: false),
                    avi_description = table.Column<string>(type: "text", nullable: false),
                    avi_dateavis = table.Column<DateTime>(type: "date", nullable: false),
                    avi_reponse = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avi", x => x.avi_id);
                    table.CheckConstraint("ck_avi_note", "avi_note between 1 and 5");
                    table.ForeignKey(
                        name: "fk_avi_prs",
                        column: x => x.prs_id,
                        principalTable: "t_e_personne_prs",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_avi_sjr",
                        column: x => x.sjr_id,
                        principalTable: "t_e_sejour_sjr",
                        principalColumn: "sjr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_etape_etp",
                columns: table => new
                {
                    etp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sjr_id = table.Column<int>(type: "integer", nullable: true),
                    etp_titre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    etp_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_etp", x => x.etp_id);
                    table.ForeignKey(
                        name: "fk_etp_sjr",
                        column: x => x.sjr_id,
                        principalTable: "t_e_sejour_sjr",
                        principalColumn: "sjr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_panier_pnr",
                columns: table => new
                {
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    sjr_id = table.Column<int>(type: "integer", nullable: false),
                    pnr_offert = table.Column<bool>(type: "boolean", nullable: false),
                    pnr_nbadultes = table.Column<int>(type: "integer", nullable: false),
                    pnr_nbenfants = table.Column<int>(type: "integer", nullable: false),
                    pnr_nbchambres = table.Column<int>(type: "integer", nullable: false),
                    pnr_prixtotal = table.Column<decimal>(type: "numeric(9,2)", nullable: false),
                    pnr_datesejour = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pnr", x => new { x.sjr_id, x.prs_id, x.pnr_offert });
                    table.ForeignKey(
                        name: "fk_pnr_cmp",
                        column: x => x.prs_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pnr_sjr",
                        column: x => x.sjr_id,
                        principalTable: "t_e_sejour_sjr",
                        principalColumn: "sjr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reservation_rsv",
                columns: table => new
                {
                    rsv_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sjr_id = table.Column<int>(type: "integer", nullable: false),
                    bcd_id = table.Column<int>(type: "integer", nullable: false),
                    rsv_nbadultes = table.Column<int>(type: "integer", nullable: false),
                    rsv_nbenfants = table.Column<int>(type: "integer", nullable: false),
                    rsv_nbchambres = table.Column<int>(type: "integer", nullable: false),
                    rsv_prixtotal = table.Column<decimal>(type: "numeric(9,2)", nullable: false),
                    rsv_datedebutresa = table.Column<DateTime>(type: "date", nullable: false),
                    rsv_datefinresa = table.Column<DateTime>(type: "date", nullable: false),
                    rsv_datefacture = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rsv", x => x.rsv_id);
                    table.ForeignKey(
                        name: "fk_rsv_bcd",
                        column: x => x.bcd_id,
                        principalTable: "t_e_boncadeau_bcd",
                        principalColumn: "bcd_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_rsv_sjr",
                        column: x => x.sjr_id,
                        principalTable: "t_e_sejour_sjr",
                        principalColumn: "sjr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_sejourcadeau_sjc",
                columns: table => new
                {
                    bcd_id = table.Column<int>(type: "integer", nullable: false),
                    sjr_id = table.Column<int>(type: "integer", nullable: false),
                    sjc_choixdestination = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sjc", x => x.bcd_id);
                    table.ForeignKey(
                        name: "fk_sjc_bcd",
                        column: x => x.bcd_id,
                        principalTable: "t_e_boncadeau_bcd",
                        principalColumn: "bcd_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sjc_sjr",
                        column: x => x.sjr_id,
                        principalTable: "t_e_sejour_sjr",
                        principalColumn: "sjr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_comporte_cpt",
                columns: table => new
                {
                    sjr_id = table.Column<int>(type: "integer", nullable: false),
                    cpp_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cpt", x => new { x.sjr_id, x.cpp_id });
                    table.ForeignKey(
                        name: "fk_cpt_cpp",
                        column: x => x.cpp_id,
                        principalTable: "t_e_catparticipant_cpp",
                        principalColumn: "cpp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cpt_sjr",
                        column: x => x.sjr_id,
                        principalTable: "t_e_sejour_sjr",
                        principalColumn: "sjr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_favori_fav",
                columns: table => new
                {
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    sjr_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fav", x => new { x.sjr_id, x.prs_id });
                    table.ForeignKey(
                        name: "fk_fav_clt",
                        column: x => x.prs_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_fav_sjr",
                        column: x => x.sjr_id,
                        principalTable: "t_e_sejour_sjr",
                        principalColumn: "sjr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_liensejour_lsj",
                columns: table => new
                {
                    sjr_id = table.Column<int>(type: "integer", nullable: false),
                    len_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lsj", x => new { x.len_id, x.sjr_id });
                    table.ForeignKey(
                        name: "fk_lsj_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_lsj_sjr",
                        column: x => x.sjr_id,
                        principalTable: "t_e_sejour_sjr",
                        principalColumn: "sjr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_lienavis_lav",
                columns: table => new
                {
                    len_id = table.Column<int>(type: "integer", nullable: false),
                    avi_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lav", x => new { x.len_id, x.avi_id });
                    table.ForeignKey(
                        name: "fk_lav_avi",
                        column: x => x.avi_id,
                        principalTable: "t_e_avis_avi",
                        principalColumn: "avi_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_lav_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_reported_rep",
                columns: table => new
                {
                    prs_id = table.Column<int>(type: "integer", nullable: false),
                    avi_id = table.Column<int>(type: "integer", nullable: false),
                    rep_a_signale = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rep", x => new { x.avi_id, x.prs_id });
                    table.ForeignKey(
                        name: "fk_rep_avi",
                        column: x => x.avi_id,
                        principalTable: "t_e_avis_avi",
                        principalColumn: "avi_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_rep_cmp",
                        column: x => x.prs_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "prs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_concerne_ccr",
                columns: table => new
                {
                    etp_id = table.Column<int>(type: "integer", nullable: false),
                    ele_id = table.Column<int>(type: "integer", nullable: false),
                    ccr_horaire = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ccr", x => new { x.etp_id, x.ele_id });
                    table.ForeignKey(
                        name: "fk_ccr_ele",
                        column: x => x.ele_id,
                        principalTable: "t_e_elementetape_ele",
                        principalColumn: "ele_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ccr_etp",
                        column: x => x.etp_id,
                        principalTable: "t_e_etape_etp",
                        principalColumn: "etp_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_lienetape_lep",
                columns: table => new
                {
                    etp_id = table.Column<int>(type: "integer", nullable: false),
                    len_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lep", x => new { x.len_id, x.etp_id });
                    table.ForeignKey(
                        name: "fk_lep_etp",
                        column: x => x.etp_id,
                        principalTable: "t_e_etape_etp",
                        principalColumn: "etp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_lep_len",
                        column: x => x.len_id,
                        principalTable: "t_e_lien_len",
                        principalColumn: "len_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_est_facturee_efc",
                columns: table => new
                {
                    rsv_id = table.Column<int>(type: "integer", nullable: false),
                    ads_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_efc", x => new { x.rsv_id, x.ads_id });
                    table.ForeignKey(
                        name: "fk_efc_ads",
                        column: x => x.ads_id,
                        principalTable: "t_e_adresse_ads",
                        principalColumn: "ads_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_efc_rsv",
                        column: x => x.rsv_id,
                        principalTable: "t_e_reservation_rsv",
                        principalColumn: "rsv_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_passe_pss",
                columns: table => new
                {
                    cmd_id = table.Column<int>(type: "integer", nullable: false),
                    rsv_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pss", x => new { x.rsv_id, x.cmd_id });
                    table.ForeignKey(
                        name: "fk_pss_cmd",
                        column: x => x.cmd_id,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pss_rsv",
                        column: x => x.rsv_id,
                        principalTable: "t_e_reservation_rsv",
                        principalColumn: "rsv_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_paye_pay",
                columns: table => new
                {
                    rsv_id = table.Column<int>(type: "integer", nullable: false),
                    tpa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pay", x => new { x.rsv_id, x.tpa_id });
                    table.ForeignKey(
                        name: "fk_pay_rsv",
                        column: x => x.rsv_id,
                        principalTable: "t_e_reservation_rsv",
                        principalColumn: "rsv_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pay_tpa",
                        column: x => x.tpa_id,
                        principalTable: "t_e_typepayement_tpa",
                        principalColumn: "tpa_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avi_prs_id",
                table: "t_e_avis_avi",
                column: "prs_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avi_sjr_id",
                table: "t_e_avis_avi",
                column: "sjr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_catparticipant_cpp_len_id",
                table: "t_e_catparticipant_cpp",
                column: "len_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_catsejour_csj_len_id",
                table: "t_e_catsejour_csj",
                column: "len_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_prs_id",
                table: "t_e_commande_cmd",
                column: "prs_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_comptecarte_cpc_rcb_id",
                table: "t_e_comptecarte_cpc",
                column: "rcb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_elementetape_ele_prs_id",
                table: "t_e_elementetape_ele",
                column: "prs_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_elementetape_ele_tpe_id",
                table: "t_e_elementetape_ele",
                column: "tpe_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_elementvignoble_evg_vgb_id",
                table: "t_e_elementvignoble_evg",
                column: "vgb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_etape_etp_sjr_id",
                table: "t_e_etape_etp",
                column: "sjr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_historiquecadeau_htc_bcd_id",
                table: "t_e_historiquecadeau_htc",
                column: "bcd_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_panier_pnr_prs_id",
                table: "t_e_panier_pnr",
                column: "prs_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reservation_rsv_bcd_id",
                table: "t_e_reservation_rsv",
                column: "bcd_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reservation_rsv_sjr_id",
                table: "t_e_reservation_rsv",
                column: "sjr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_routedesvins_rdv_vgb_id",
                table: "t_e_routedesvins_rdv",
                column: "vgb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_sejour_sjr_csj_id",
                table: "t_e_sejour_sjr",
                column: "csj_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_sejour_sjr_cvg_id",
                table: "t_e_sejour_sjr",
                column: "cvg_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_sejour_sjr_rdv_id",
                table: "t_e_sejour_sjr",
                column: "rdv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_sejourcadeau_sjc_sjr_id",
                table: "t_e_sejourcadeau_sjc",
                column: "sjr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_vignoble_vgb_len_id",
                table: "t_e_vignoble_vgb",
                column: "len_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_comporte_cpt_cpp_id",
                table: "t_j_comporte_cpt",
                column: "cpp_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_concerne_ccr_ele_id",
                table: "t_j_concerne_ccr",
                column: "ele_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_contient_ctn_ele_id",
                table: "t_j_contient_ctn",
                column: "ele_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_est_facturee_efc_ads_id",
                table: "t_j_est_facturee_efc",
                column: "ads_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_favori_fav_prs_id",
                table: "t_j_favori_fav",
                column: "prs_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_lienavis_lav_avi_id",
                table: "t_j_lienavis_lav",
                column: "avi_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_lienelementvignoble_lev_evg_id",
                table: "t_j_lienelementvignoble_lev",
                column: "evg_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_lienetape_lep_etp_id",
                table: "t_j_lienetape_lep",
                column: "etp_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_lienroutedesvins_lrv_rdv_id",
                table: "t_j_lienroutedesvins_lrv",
                column: "rdv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_liensejour_lsj_sjr_id",
                table: "t_j_liensejour_lsj",
                column: "sjr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_passe_pss_cmd_id",
                table: "t_j_passe_pss",
                column: "cmd_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_paye_pay_tpa_id",
                table: "t_j_paye_pay",
                column: "tpa_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_reported_rep_prs_id",
                table: "t_j_reported_rep",
                column: "prs_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_reside_rsd_prs_id",
                table: "t_j_reside_rsd",
                column: "prs_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_chequecadeau_cqc");

            migrationBuilder.DropTable(
                name: "t_e_comptecarte_cpc");

            migrationBuilder.DropTable(
                name: "t_e_historiquecadeau_htc");

            migrationBuilder.DropTable(
                name: "t_e_panier_pnr");

            migrationBuilder.DropTable(
                name: "t_e_partenaireactivite_pta");

            migrationBuilder.DropTable(
                name: "t_e_partenairecave_ptc");

            migrationBuilder.DropTable(
                name: "t_e_partenairehebergement_pth");

            migrationBuilder.DropTable(
                name: "t_e_partenairerestaurant_ptr");

            migrationBuilder.DropTable(
                name: "t_e_sejourcadeau_sjc");

            migrationBuilder.DropTable(
                name: "t_j_comporte_cpt");

            migrationBuilder.DropTable(
                name: "t_j_concerne_ccr");

            migrationBuilder.DropTable(
                name: "t_j_contient_ctn");

            migrationBuilder.DropTable(
                name: "t_j_est_facturee_efc");

            migrationBuilder.DropTable(
                name: "t_j_favori_fav");

            migrationBuilder.DropTable(
                name: "t_j_lienavis_lav");

            migrationBuilder.DropTable(
                name: "t_j_lienelementvignoble_lev");

            migrationBuilder.DropTable(
                name: "t_j_lienetape_lep");

            migrationBuilder.DropTable(
                name: "t_j_lienroutedesvins_lrv");

            migrationBuilder.DropTable(
                name: "t_j_liensejour_lsj");

            migrationBuilder.DropTable(
                name: "t_j_passe_pss");

            migrationBuilder.DropTable(
                name: "t_j_paye_pay");

            migrationBuilder.DropTable(
                name: "t_j_reported_rep");

            migrationBuilder.DropTable(
                name: "t_j_reside_rsd");

            migrationBuilder.DropTable(
                name: "t_e_refcartebancaire_rcb");

            migrationBuilder.DropTable(
                name: "t_e_catparticipant_cpp");

            migrationBuilder.DropTable(
                name: "t_e_elementetape_ele");

            migrationBuilder.DropTable(
                name: "t_e_elementvignoble_evg");

            migrationBuilder.DropTable(
                name: "t_e_etape_etp");

            migrationBuilder.DropTable(
                name: "t_e_commande_cmd");

            migrationBuilder.DropTable(
                name: "t_e_reservation_rsv");

            migrationBuilder.DropTable(
                name: "t_e_typepayement_tpa");

            migrationBuilder.DropTable(
                name: "t_e_avis_avi");

            migrationBuilder.DropTable(
                name: "t_e_adresse_ads");

            migrationBuilder.DropTable(
                name: "t_e_partenaire_ptn");

            migrationBuilder.DropTable(
                name: "t_e_typeelementetape_tpe");

            migrationBuilder.DropTable(
                name: "t_e_user_usr");

            migrationBuilder.DropTable(
                name: "t_e_boncadeau_bcd");

            migrationBuilder.DropTable(
                name: "t_e_sejour_sjr");

            migrationBuilder.DropTable(
                name: "t_e_personne_prs");

            migrationBuilder.DropTable(
                name: "t_e_catsejour_csj");

            migrationBuilder.DropTable(
                name: "t_e_catvignoble_cvg");

            migrationBuilder.DropTable(
                name: "t_e_routedesvins_rdv");

            migrationBuilder.DropTable(
                name: "t_e_vignoble_vgb");

            migrationBuilder.DropTable(
                name: "t_e_lien_len");
        }
    }
}
