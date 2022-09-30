using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Xml.Linq;

string stringaDiConnessione = "Data Source=localhost;Initial Catalog=db-biblioteca;Integrated Security=True";
SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione);

Init();

void Init()
{
    Console.WriteLine("Salve! Vuole effettuare una ricerca(0), registrarsi(1) o effettuare il login?(2)");

    switch (Convert.ToInt32(Console.ReadLine()))
    {
        case 0:
            Console.WriteLine($"Vuole cercare Dvd(0) o Libri(1)?");
            SearchProduct(Convert.ToInt32(Console.ReadLine()), 0);
            Init();
            break;
        case 1:
            Registration();
            break;
        default:
            Console.WriteLine("Inserire email");
            string email = Console.ReadLine();

            Console.WriteLine("Inserire password");
            string password = Console.ReadLine();

            UserMenu(SearchUser(email, password, false));
            break;
    }
}

void Registration()
{
    int userDataAccepted = 0;
    long currentUserId = 0;
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
        long phone = Convert.ToInt64(Console.ReadLine());

        Console.WriteLine();
        Console.WriteLine("Ecco il riepilogo dei dati da lei inseriti: ");

        Console.WriteLine($"Cognome: {surname}");
        Console.WriteLine($"Nome: {name}");
        Console.WriteLine($"Email: {email}");
        Console.WriteLine($"Password: {password}");
        Console.WriteLine($"Phone: {phone}");

        Console.WriteLine();
        Console.WriteLine("Le vanno bene? Si(1) No(0)");
        userDataAccepted = Convert.ToInt32(Console.ReadLine());

        if (userDataAccepted == 1)
        {
            currentUserId = UserToDb(surname, name, email, password, phone);
        }

    } while (userDataAccepted == 0);

    Console.WriteLine();
    UserMenu(currentUserId);
}

void UserMenu(long userId)
{
    long currentUserId = userId;

    Console.WriteLine();
    Console.WriteLine("Vuole prenotare? Dvd(0) Libro(1) || Lista prenotazioni(2) || Logout(3)");
    int productChoice = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();
    try
    {
        switch (productChoice)
        {
            case 0:

                if (SearchProduct(productChoice, currentUserId) == 1)
                {
                    UserMenu(currentUserId);
                }
                else
                {
                    Init();
                }
                break;

            case 1:
                if (SearchProduct(productChoice, currentUserId) == 1)
                {
                    UserMenu(currentUserId);
                }
                else
                {
                    Init();
                }
                break;

            case 2:
                GetUserLoans(currentUserId);
                Console.WriteLine("Vuole tornare al menù utente? Si(1) No(0)");

                if(Convert.ToInt32(Console.ReadLine()) == 1)
                {
                    UserMenu(currentUserId);
                }
                else
                {
                    Init();
                }
                break;

            default:
                Init();
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    
}

void GetUserLoans(long currentUserId)
{
    try
    {
        connessioneSql.Open();

        string query = "SELECT documents.title, loan_start, loan_end " +
                        "FROM document_user " +
                        "INNER JOIN documents ON documents.id = document_user.document_id " +
                        "WHERE user_id = @dato1";

        SqlCommand cmd = new SqlCommand(query, connessioneSql);
        cmd.Parameters.Add(new SqlParameter("@dato1", currentUserId));

        SqlDataReader reader = cmd.ExecuteReader();
        {
            while (reader.Read())
            {
                Console.WriteLine($"Titolo: {reader.GetString(0)}");
                Console.WriteLine($"Prenotato il: {reader.GetDateTime(1)}");
                Console.WriteLine($"Fino al: {reader.GetDateTime(2)}");
            }
        }

        Console.WriteLine("Fine lista prenotazioni!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        connessioneSql.Close();
    }
}

void ChangeDocumentAvailability(long documentId)
{
    try
    {
        connessioneSql.Open();

        string query = "UPDATE documents SET available = 0 WHERE id = @dato1";

        SqlCommand cmd = new SqlCommand(query, connessioneSql);
        cmd.Parameters.Add(new SqlParameter("@dato1", documentId));

        int affectedRows = cmd.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        connessioneSql.Close();
    }
}

void NewLoan(long documentId, long currentUserId)
{
    try
    {
        Console.WriteLine("Inserire data della restituzione (dd/mm/yyyy)");
        string loan_end = Console.ReadLine();

        connessioneSql.Open();
        string query = "INSERT INTO document_user (user_id, document_id, loan_start, loan_end) VALUES (@dato1, @dato2, @dato3, @dato4)";

        SqlCommand cmd = new SqlCommand(query, connessioneSql);
        cmd.Parameters.Add(new SqlParameter("@dato1", currentUserId));
        cmd.Parameters.Add(new SqlParameter("@dato2", documentId));
        cmd.Parameters.Add(new SqlParameter("@dato3", DateTime.Now.ToString("d")));
        cmd.Parameters.Add(new SqlParameter("@dato4", loan_end));
        int affectedRows = cmd.ExecuteNonQuery();

        Console.WriteLine("Prenotazione effettuata!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        connessioneSql.Close();
    }

    ChangeDocumentAvailability(documentId);
}

void ProductInfo(long documentId, int productType)
{
    Console.WriteLine();
    try
    {
        string query = "";

        connessioneSql.Open();

        switch (productType)
        {

            case 0:
                query = "SELECT title, year, sector, author, time, available, code " +
                        "FROM documents " +
                        "WHERE id = @dato1";
                break;

            default:
                query = "SELECT title, year, sector, author, number_of_pages, available, code " +
                        "FROM documents " +
                        "WHERE id = @dato1";
                break;
        }
        

        SqlCommand cmd = new SqlCommand(query, connessioneSql);
        cmd.Parameters.Add(new SqlParameter("@dato1", documentId));

        SqlDataReader reader = cmd.ExecuteReader();
        {
            while (reader.Read())
            {
                Console.WriteLine($"Titolo: {reader.GetString(0)}");
                Console.WriteLine($"Anno di uscita: {reader.GetInt32(1)}");
                Console.WriteLine($"Genere: {reader.GetString(2)}");
                Console.WriteLine($"Autore: {reader.GetString(3)}");

                switch (productType)
                {
                    case 0:
                        Console.WriteLine($"Durata dvd: {reader.GetInt32(4)}");
                        break;

                    default:
                        Console.WriteLine($"Numero di pagine: {reader.GetInt32(4)}");
                        break;
                }
                
                Console.WriteLine($"Disponibile: {(reader.GetByte(5) == 1 ? "Si" : "No")}");
                Console.WriteLine($"code: {reader.GetString(6)}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        connessioneSql.Close();
    }
}

int SearchProduct(int productType, long currentUserId)
{

    Console.WriteLine("Inserire titolo");
    string title = Console.ReadLine();
    Console.WriteLine();

    string query = "";
    long documentId = 0;
    int documentAvailabe = 0;

    switch (productType)
    {
        case 0:
            query = "SELECT id, available FROM documents WHERE title = @dato1 AND type = 'dvd'";
            break;
        default:
            query = "SELECT id, available FROM documents WHERE title = @dato1 AND type = 'book'";
            break;
    }

    try
    {
        connessioneSql.Open();

        SqlCommand cmd = new SqlCommand(query, connessioneSql);
        cmd.Parameters.Add(new SqlParameter("@dato1", title));

        SqlDataReader reader = cmd.ExecuteReader();
        {
            while (reader.Read())
            {
                documentId = reader.GetInt64(0);
                documentAvailabe = reader.GetByte(1);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        connessioneSql.Close();
    }

    switch (productType)
    {
        case 0:
            Console.WriteLine("Dvd trovato con successo!");
            ProductInfo(documentId, productType);
            break;

        default:
            Console.WriteLine("Libro trovato con successo!");
            ProductInfo(documentId, productType);
            break;
    }

    if (currentUserId != 0)
    {
        switch (documentAvailabe)
        {
            case 0:
                Console.WriteLine();
                Console.WriteLine("Ma al momento non è disponibile. Tornare al menù utente? Si(1) No(0)");
                return Convert.ToInt32(Console.ReadLine());

            default:
                Console.WriteLine();
                Console.WriteLine("Proseguire con la prenotazione? Si(1) No(0)");

                if (Convert.ToInt32(Console.ReadLine()) == 1)
                {
                    NewLoan(documentId, currentUserId);
                    Console.WriteLine("Tornare al menù utente? Si(1) No(0)");
                    return Convert.ToInt32(Console.ReadLine());
                }
                else
                {
                    return 1;
                }
        }
    }
    else
    {
        Console.WriteLine("Ritorno al menù principale...");
        Console.WriteLine();
        return 0;
    }
}

long SearchUser(string email, string password, bool isUserLogged)
{
    long currentUserId = 0;

    try
    {
        connessioneSql.Open();

        string query = "SELECT id FROM users WHERE email = @dato1 AND password = @dato2";

        SqlCommand cmd = new SqlCommand(query, connessioneSql);
        cmd.Parameters.Add(new SqlParameter("@dato1", email));
        cmd.Parameters.Add(new SqlParameter("@dato2", password));

        SqlDataReader reader = cmd.ExecuteReader();
        {
            while (reader.Read())
            {
                currentUserId = reader.GetInt64(0);
            }
        }

        Console.WriteLine();
        if (!isUserLogged)
        {
            Console.WriteLine("utente trovato con successo!");
        }
        
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        connessioneSql.Close();
    }

    return currentUserId;
}

long UserToDb(string surname, string name, string email, string password, long phone)
{
    long currentUserId = 0;

    try
    {
        connessioneSql.Open();

        string query = "INSERT INTO users (name, surname, email, password, phone) VALUES (@dato1, @dato2, @dato3, @dato4, @dato5)";

        SqlCommand cmd = new SqlCommand(query, connessioneSql);
        cmd.Parameters.Add(new SqlParameter("@dato1", name));
        cmd.Parameters.Add(new SqlParameter("@dato2", surname));
        cmd.Parameters.Add(new SqlParameter("@dato3", email));
        cmd.Parameters.Add(new SqlParameter("@dato4", password));
        cmd.Parameters.Add(new SqlParameter("@dato5", phone));

        int affectedRows = cmd.ExecuteNonQuery();

        Console.WriteLine("utente inserito nel database");
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        connessioneSql.Close();
    }
    

    try
    {
        currentUserId = SearchUser(email, password, true);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    return currentUserId;
}