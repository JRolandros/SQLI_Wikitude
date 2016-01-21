using SQLite;

namespace SQLI_CrossAR.CrossAR.DataAccess.DB.Tables
{
    [Table("PI_DB")]
    public class PI_DB
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [MaxLength(50), Column("_nom")]
        public string Nom { get; set; }

        [MaxLength(200), Column("_image")]
        public string ImageUrl { get; set; }

        [MaxLength(20), Column("_categorie")]
        public string Categorie { get; set; }

        [MaxLength (200), Column("_adresse")]
        public string Adresse { get; set; }

        [Column("_gps_lon")]
        public double Longitude { get; set; }

        [Column("_gps_lat")]
        public double Latitude { get; set; }

        
        

    }
}
