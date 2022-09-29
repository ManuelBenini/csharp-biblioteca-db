public class User
{
    //Attributes
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public List<Dvd> dvds = new List<Dvd>();
    public List<Book> books = new List<Book>();

    //Constructor
    public User(string surname, string name, string email, string password, string phone)
    {
        Surname = surname;
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
    }

    public User()
    {

    }

    public List<Dvd> getDvds()
    {
        return dvds;
    }

    public void DvdPush(Dvd dvd)
    {
        dvds.Add(dvd);
    }

    public List<Book> getBooks()
    {
        return books;
    }

    public void BookPush(Book book)
    {
        books.Add(book);
    }
}
