namespace Artsofte.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = String.Empty;
        public int Department { get; set; }
        public string Language { get; set; } = String.Empty;
    }
}
