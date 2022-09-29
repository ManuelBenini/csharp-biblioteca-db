public class Library
{
    //Attributes
    public List<User> users = new List<User>();
    List<Book> books = new List<Book>();
    List<Dvd> dvds = new List<Dvd>();
    List<Loan> loans = new List<Loan>();

    //Constructor
    public Library()
    {
        StaticUserPush();
        StaticBookPush();
        StaticDvdPush();
        StaticLoanPush();
    }

    //Riempiemento delle liste con dati statici
    public void StaticUserPush()
    {
        users.Add(new User("Benini", "Manuel", "manuelbenini1905@gmail.com", "ciao19", "3913325333"));
        users.Add(new User("John", "Snow", "cicciolino@gmail.com", "beppe19", "3913325333"));
        users.Add(new User("artoris", "eleggis", "ercole@gmail.com", "nonloso", "3913325333"));
        users.Add(new User("peppe", "Gianluca", "elementare@gmail.com", "wattson", "3913325333"));
    }
    public void StaticBookPush()
    {
        books.Add(new Book(5534, "Lo Hobbit", 2019, "Fantasy", true, "AB", new Author("J.R.R", "Tolkien"), 540));
        books.Add(new Book(2305, "Psicologo", 1980, "Drammatic", true, "CB", new Author("Frank", "John"), 230));
        books.Add(new Book(2312, "Chthulu", 1970, "Fantasy", false, "CS", new Author("Howard", "Lovecraft"), 999));
        books.Add(new Book(4321, "Arthur", 2019, "Mitologia", false, "ERB", new Author("Il", "Re"), 250));
    }
    public void StaticDvdPush()
    {
        dvds.Add(new Dvd(1234, "Do You Breath?", 2019, "Rock", true, "as", new Author("Frank", "Conley"), 20));
        dvds.Add(new Dvd(5321, "Summer Vibes", 1980, "Lo-Fi", true, "CB", new Author("Cinegar", "John"), 60));
        dvds.Add(new Dvd(6742, "Worms Carves Your Mind", 1970, "Metal", false, "CS", new Author("Loving", "Lovecraft"), 50));
        dvds.Add(new Dvd(1111, "Jordie", 2019, "Storytelling", false, "ERB", new Author("De", "Andrè"), 110));
    }
    public void StaticLoanPush()
    {
        loans.Add(new Loan(users[0], "20/09/2022", "27/09/2022"));
        loans.Add(new Loan(users[0], "23/09/2022", "31/10/2022"));
        loans.Add(new Loan(users[1], "20/09/2022", "27/09/2022"));
        loans.Add(new Loan(users[2], "23/09/2022", "31/10/2022"));
        loans.Add(new Loan(users[3], "23/09/2022", "31/10/2022"));

        loans[0].Book = books[0];
        loans[1].Dvd = dvds[1];
        loans[2].Dvd = dvds[3];
        loans[3].Book = books[2];
        loans[4].Book = books[1];
    }

    //Getters
    public List<User> GetUsers()
    {
        return users;
    }
    public List<Book> GetBooks()
    {
        return books;
    }
    public List<Dvd> GetDvds()
    {
        return dvds;
    }
    public List<Loan> GetLoans()
    {
        return loans;
    }

    //Setters ( List.Add() )
    public void UserPush(User user)
    {
        users.Add(user);
    }
    public void BookPush(Book book)
    {
        books.Add(book);
    }
    public void DvdPush(Dvd dvd)
    {
        dvds.Add(dvd);
    }
    public void LoanPush(Loan loan)
    {
        loans.Add(loan);
    }
}

