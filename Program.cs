
using System.Linq;

Library library = new Library();

Menu:

Console.WriteLine("Salve! Vuole effettuare una ricerca(0) , registrarsi?(1) o effettuare il login(2)");
int choice = Convert.ToInt32(Console.ReadLine());

if(choice == 0)
{
    Console.WriteLine("Vuole cercare un dvd(0), un libro?(1) oppure i prestiti di un utente?(2)");
    int productChoice = Convert.ToInt32(Console.ReadLine());

    Search(productChoice);

    Console.WriteLine("Tornare al menù? Si(1) No(0)");
    if (Convert.ToInt32(Console.ReadLine()) == 1)
    {
        goto Menu;
    }
}
else if(choice == 1)
{
   User user = Registration();
   library.UserPush(user);

    Console.WriteLine($"Salve {user.Name} {user.Surname}! Vuole prenotare? Dvd(0) Libro(1) || Lista prenotazioni(2) || Logout(3)");
    int productChoice = Convert.ToInt32(Console.ReadLine());

    if (productChoice != 3)
    {
        UserList(productChoice, user);
    }
    else
    {
        goto Menu;
    }

   Console.WriteLine("Tornare al menù? Si(1) No(0)");
   if (Convert.ToInt32(Console.ReadLine()) == 1)
   {
       goto Menu;
   }
}
else
{
    User user = Login();

    Console.WriteLine($"Salve {user.Name} {user.Surname}! Vuole prenotare? Dvd(0) Libro(1) || Lista prenotazioni(2) || Logout(3)");
    int productChoice = Convert.ToInt32(Console.ReadLine());

    if(productChoice != 3)
    {
        UserList(productChoice, user);
    }
    else
    {
        goto Menu;
    }

    Console.WriteLine("Tornare al menù? Si(1) No(0)");
    if (Convert.ToInt32(Console.ReadLine()) == 1)
    {
        goto Menu;
    }
}

Object Search(int productChoice)
{
    //RICERCA DVD
    if (productChoice == 0)
    {
        Console.WriteLine("Effettuare la ricerca sul titolo(0), sul codice(1) o visualizzare tutti i Dvd?(2)");
        int searchParameter = Convert.ToInt32(Console.ReadLine());

        bool dvdFounded = false;
        Dvd chosenDvd = null;

        if (searchParameter == 0)
        {
            Console.WriteLine("Inserire il titolo del dvd");
            string searchedDvd = Console.ReadLine();

            foreach (Dvd dvd in library.GetDvds())
            {
                if (dvd.Title.ToLower().Contains(searchedDvd.ToLower()))
                {
                    dvdFounded = true;
                    chosenDvd = dvd;
                }
            }
        }
        else if (searchParameter == 1)
        {
            Console.WriteLine("Inserire il codice del dvd");
            int searchedDvd = Convert.ToInt32(Console.ReadLine());

            foreach (Dvd dvd in library.GetDvds())
            {
                if (dvd.Code == searchedDvd)
                {
                    dvdFounded = true;
                    chosenDvd = dvd;
                }
            }
        }
        else
        {
            Console.WriteLine("Lista di tutti i Dvd: ");
            Console.WriteLine("-----------------------");

            foreach (Dvd dvd in library.GetDvds())
            {
                Console.WriteLine($"Titolo: {dvd.Title}");
                Console.WriteLine($"Autore: {dvd.Author.Name} {dvd.Author.Surname}");
                Console.WriteLine($"Disponibile: {(dvd.IsAvailable ? "Si" : "No")} ");
                Console.WriteLine("-----------------------");
            }
            return null;
        }

        

        if (dvdFounded)
        {
            Console.WriteLine("Dvd trovato, ecco i suoi dettagli: ");

            Console.WriteLine($"Codice: {chosenDvd.Code}");
            Console.WriteLine($"Titolo: {chosenDvd.Title}");
            Console.WriteLine($"Anno di produzione: {chosenDvd.Year}");
            Console.WriteLine($"Genere: {chosenDvd.Sector}");
            Console.WriteLine($"Disponibile: {(chosenDvd.IsAvailable ? "Si" : "No")} ");
            Console.WriteLine($"Scaffale: {chosenDvd.Shelf}");
            Console.WriteLine($"Autore: {chosenDvd.Author.Name} {chosenDvd.Author.Surname}");
            Console.WriteLine($"Durata del dvd: {chosenDvd.Time}");

            return chosenDvd;
        }
        else
        {
            Console.WriteLine("Dvd non trovato");
            return null;
        }
    }
    //RICERCA LIBRI
    else if(productChoice == 1)
    {

        Console.WriteLine("Effettuare la ricerca sul titolo(0), sul codice(1) o visualizzare tutti i Libri?(2)");
        int searchParameter = Convert.ToInt32(Console.ReadLine());

        bool bookFounded = false;
        Book chosenBook = null;

        if (searchParameter == 0)
        {
            Console.WriteLine("Inserire il titolo del libro");
            string searchedBook = Console.ReadLine();

            foreach (Book book in library.GetBooks())
            {
                if (book.Title.ToLower().Contains(searchedBook.ToLower()))
                {
                    bookFounded = true;
                    chosenBook = book;
                }
            }
        }
        else if(searchParameter == 1)
        {
            Console.WriteLine("Inserire il codice del libro");
            int searchedBook = Convert.ToInt32(Console.ReadLine());

            foreach (Book book in library.GetBooks())
            {
                if (book.Code == searchedBook)
                {
                    bookFounded = true;
                    chosenBook = book;
                }
            }
        }
        else
        {
            Console.WriteLine("Lista di tutti i Libri: ");
            Console.WriteLine("-----------------------");

            foreach (Book book in library.GetBooks())
            {
                Console.WriteLine($"Titolo: {book.Title}");
                Console.WriteLine($"Autore: {book.Author.Name} {book.Author.Surname}");
                Console.WriteLine($"Disponibile: {(book.IsAvailable? "Si" : "No")} ");
                Console.WriteLine("-----------------------");
            }
            return null;
        }

        if (bookFounded)
        {
            Console.WriteLine("Libro trovato, ecco i suoi dettagli: ");

            Console.WriteLine($"Codice: {chosenBook.Code}");
            Console.WriteLine($"Titolo: {chosenBook.Title}");
            Console.WriteLine($"Anno di produzione: {chosenBook.Year}");
            Console.WriteLine($"Genere: {chosenBook.Sector}");
            Console.WriteLine($"Disponibile: {(chosenBook.IsAvailable ? "Si" : "No")}");
            Console.WriteLine($"Scaffale: {chosenBook.Shelf}");
            Console.WriteLine($"Autore: {chosenBook.Author.Name} {chosenBook.Author.Surname}");
            Console.WriteLine($"Numero di pagine: {chosenBook.NumberOfPages}");

            return chosenBook;
        }
        else
        {
            Console.WriteLine("Libro non trovato");
            return null;
        }
    }
    //RICERCA PRENOTAZIONI
    else
    {
        Console.WriteLine("Inserire cognome utente");
        string searchedUserSurname = Console.ReadLine();

        Console.WriteLine("Inserire nome utente");
        string searchedUserName = Console.ReadLine();

        List<Loan> userLoans = new List<Loan>();
        bool loanExist = false;

        foreach (Loan loan in library.GetLoans())
        {
            if (loan.User.Name.ToLower().Contains(searchedUserName.ToLower()) &&
                loan.User.Surname.ToLower().Contains(searchedUserSurname.ToLower()) )

            {
                if (loan.Dvd != null)
                {
                    Console.WriteLine($"Dvd: {loan.Dvd.Title}");
                    loanExist = true;
                }
                else
                {
                    Console.WriteLine($"Libro: {loan.Book.Title}");
                    loanExist = true;
                }
                Console.WriteLine($"Dal: {loan.LoanStart}");
                Console.WriteLine($"al: {loan.LoanEnd}");
                Console.WriteLine("------------------------------");
            }
        }
        if(!loanExist)
        {
            Console.WriteLine("L'utente non ha nessun prestito.");
        }
        Loan newLoan2 = new Loan();
        return newLoan2;
    }
}

User Registration()
{
    int userDataAccepted = 0;
    User registedUser;
    do
    {
        Console.WriteLine("Inserire cognome");
        string surname = Console.ReadLine();

        Console.WriteLine("Inserire nome");
        string name = Console.ReadLine();

        Console.WriteLine("Inserire email");
        string email = Console.ReadLine();

        Console.WriteLine("Inserire password");
        string password = Console.ReadLine();

        Console.WriteLine("Inserire numero di telefono");
        string phone = Console.ReadLine();

        registedUser = new User(surname, name, email, password, phone);

        Console.WriteLine("Ecco il riepilogo dei dati da lei inseriti: ");

        Console.WriteLine($"Cognome: {registedUser.Surname}");
        Console.WriteLine($"Nome: {registedUser.Name}");
        Console.WriteLine($"Email: {registedUser.Email}");
        Console.WriteLine($"Password: {registedUser.Password}");
        Console.WriteLine($"Phone: {registedUser.Phone}");

        Console.WriteLine("Le vanno bene? Si(1) No(0)");
        userDataAccepted = Convert.ToInt32(Console.ReadLine());

    } while (userDataAccepted == 0);

    return registedUser;
}

User Login()
{
    Console.WriteLine("Inserire la password");
    string searchedPassword = Console.ReadLine();

    bool userFounded = false;
    User chosenUser = null;

    foreach (User user in library.GetUsers())
    {
        if (user.Password.ToLower().Contains(searchedPassword.ToLower()))
        {
            userFounded = true;
            chosenUser = user;
        }
    }
    if (userFounded)
    {
        Console.WriteLine("Utente trovato, ecco i suoi dettagli: ");

        Console.WriteLine($"Cognome: {chosenUser.Surname}");
        Console.WriteLine($"Nome: {chosenUser.Name}");
        Console.WriteLine($"Email: {chosenUser.Email}");
        Console.WriteLine($"Password: {chosenUser.Password}");
        Console.WriteLine($"Phone: {chosenUser.Phone}");

        return chosenUser;
    }
    else
    {
        Console.WriteLine("Utente non trovato");
        return null;
    }
}

void UserList(int productChoice, User user)
{
    if (productChoice == 0)
    {
        Dvd chosenDvd = (Dvd)Search(productChoice);

        if(chosenDvd != null)
        {
            if (chosenDvd.IsAvailable == true)
            {
                Console.WriteLine("Il dvd è prenotabile, procedere? Si(1) No(0)");
                int isUserBooking = Convert.ToInt32(Console.ReadLine());

                if (isUserBooking == 1)
                {
                    chosenDvd.IsAvailable = false;
                    library.LoanPush(new Loan(user, DateTime.Now.ToString(), DateTime.Now.AddDays(7).ToString()));
                    library.GetLoans()[library.GetLoans().Count - 1].Dvd = chosenDvd;

                    Console.WriteLine($"la prenotazione del Dvd {chosenDvd.Title} è avvenuta con successo! Visualizzare la lista dei propri Dvd? Si(1) No(0)");
                    int toList = Convert.ToInt32(Console.ReadLine());

                    if (toList == 1)
                    {
                        GetLoans(user, "dvd");
                    }
                }
            }
            else
            {
                Console.WriteLine("Il Dvd non è prenotabile.");
            }
        }
    }
    else if(productChoice == 1)
    {
        Book chosenBook = (Book)Search(productChoice);

        if(chosenBook != null)
        {
            if (chosenBook.IsAvailable == true)
            {
                Console.WriteLine("Il libro è prenotabile, procedere? Si(1) No(0)");
                int isUserBooking = Convert.ToInt32(Console.ReadLine());

                if (isUserBooking == 1)
                {
                    chosenBook.IsAvailable = false;
                    library.LoanPush(new Loan(user, DateTime.Now.ToString(), DateTime.Now.AddDays(7).ToString()));
                    library.GetLoans()[library.GetLoans().Count - 1].Book = chosenBook;

                    Console.WriteLine($"la prenotazione del libro {chosenBook.Title} è avvenuta con successo! Visualizzare la lista dei propri libri? Si(1) No(0)");
                    int toList = Convert.ToInt32(Console.ReadLine());

                    if (toList == 1)
                    {
                        GetLoans(user, "libri");
                    }
                }
            }
            else
            {
                Console.WriteLine("Il libro non è prenotabile.");
            }
        }

    }
    else
    {
        Console.WriteLine("Di cosa vuole mostrare le prenotazioni? Dvd(0) Libri(1)");
        int searchType = Convert.ToInt32(Console.ReadLine());

        if(searchType == 0)
        {
            GetLoans(user, "dvd");
        }
        else
        {
            GetLoans(user, "libri");
        }
    }
}

void GetLoans(User user, string testo)
{
    Console.WriteLine($"Ecco la lista dei {testo} da lei prenotati: ");
    bool LoanExist = false;
    foreach (Loan loan in library.GetLoans())
    {
        if (loan.User.Name == user.Name)
        {
            if(testo == "dvd")
            {
                if (loan.Dvd != null)
                {
                    Console.WriteLine(loan.Dvd.Title);
                    Console.WriteLine($"Dal: {loan.LoanStart}");
                    Console.WriteLine($"al: {loan.LoanEnd}");
                    Console.WriteLine("------------------------------");
                    LoanExist = true;
                }
            }
            else
            {
                if (loan.Book != null)
                {
                    Console.WriteLine(loan.Book.Title);
                    Console.WriteLine($"Dal: {loan.LoanStart}");
                    Console.WriteLine($"al: {loan.LoanEnd}");
                    Console.WriteLine("------------------------------");
                    LoanExist = true;
                }
            }
            
        }
    }
    if (!LoanExist)
    {
        Console.WriteLine($"Non vi sono prenotazioni di {testo} a suo nome.");
    }
}

