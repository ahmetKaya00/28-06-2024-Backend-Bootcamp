using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Models{

    public class BootcampViewModel{
        [Key]
        public int KursId { get; set; }
        public string? Baslik {get;set;}
        public int? EgitmenId {get;set;}

    }
}