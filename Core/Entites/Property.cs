using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entites
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // İlan başlığı
        public string Description { get; set; } = string.Empty;

        public string RoomCount { get; set; } = "2+1"; // Oda sayısı (1+1, 2+1 vb.)
        public int SquareMeter { get; set; } // Metrekare

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Fiyat

        public ListingType Status { get; set; } // Satılık mı? (True: Satılık, False: Kiralık)
        public string Address { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
