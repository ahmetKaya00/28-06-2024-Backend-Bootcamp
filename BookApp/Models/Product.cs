using System.ComponentModel.DataAnnotations;

namespace BookApp.Models{

    public class Product{
        [Display(Name="Urun Id")]
        public int ProductId { get; set; }

        [Display(Name="Kitap Adı")]
        [Required(ErrorMessage ="Kitap adı zorunlu")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage ="Ücret adı zorunlu")]
        [Display(Name="Ücret")]
        [Range(0,1000)]
        public decimal Price {get;set;}

        [Display(Name="Resim")]
        public string? Image {get;set;} = string.Empty;
        public bool IsActive {get;set;}

        [Display(Name="Category")]
        [Required(ErrorMessage ="Kategori zorunlu")]
        public int CategoryId {get;set;}

    }
}