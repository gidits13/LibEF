namespace LibEF
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }
        public string Address { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();

    }
}