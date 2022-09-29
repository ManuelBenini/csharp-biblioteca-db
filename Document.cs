public partial class Document
{
    //Attributes
    public int Code { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Sector { get; set; }
    public bool IsAvailable { get; set; }
    public string Shelf { get; set; }
    public Author Author { get; set; }

    //Constructor
    public Document(int code, string title, int year, string sector, bool isAvailable, string shelf, Author author)
    {
        Code = code;
        Title = title;
        Year = year;
        Sector = sector;
        IsAvailable = isAvailable;
        Shelf = shelf;
        Author = author;
    }

}
