using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data{

    public class Bootcamp{
        [Key]
        public int KursId { get; set; }
        public string? Baslik {get;set;}
    }
}