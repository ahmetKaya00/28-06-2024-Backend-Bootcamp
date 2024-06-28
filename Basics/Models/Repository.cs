namespace Basics.Models{

    public class Repository{

        private static readonly List<Bootcamp> _bootcamp = new();

        static Repository(){
            _bootcamp = new List<Bootcamp>(){
                new Bootcamp() {Id = 1, Title = "Backend Bootcamp",Description = "asp.net core öğreneceğiz",Image = "1.png"},
            new Bootcamp() {Id = 2, Title = "Game Bootcamp",Description = "unity öğreneceğiz",Image = "2.png"},
            new Bootcamp() {Id = 3, Title = "Full-Stack Bootcamp",Description = "Web öğreneceğiz",Image = "3.png"},
            new Bootcamp() {Id = 4, Title = "Java Bootcamp",Description = "Mobil öğreneceğiz",Image = "1.png"}
            };
        }

        public static List<Bootcamp> Bootcamps{
            get{return _bootcamp;}
        }

        public static Bootcamp? GetById(int? id){
            return _bootcamp.FirstOrDefault(b=>b.Id == id);
        }
    }
}