public class Loan
{
    public Dvd Dvd { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
    public string LoanStart { get; set; }
    public string LoanEnd { get; set; }

    public Loan(User user, string loanStart, string loanEnd)
    {
        User = user;
        LoanStart = loanStart;
        LoanEnd = loanEnd;
    }

    public Loan()
    {

    }
}

