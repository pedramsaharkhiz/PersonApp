namespace API.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public User User { get; set; }
        
    }
}