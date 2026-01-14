class Customer(string lastName, string otherNames, DateOnly dateOfBirth)
    //string address, string meansOfIdentification, string nationality)
{
    public Guid CustomerId {get;} = Guid.NewGuid();
    public string Lastname {get; private set;} = lastName;
    public string OtherNames {get; private set;} = otherNames;
    public DateOnly DateOfBirth {get;} = dateOfBirth;

    //public string Address {get; set;} = address;
    //public string MeansOfIdentification {get; private set;} = meansOfIdentification;
    //public string Nationality {get; private set;} = nationality;

    private readonly List<string> _accountNumbers = [];
    public IReadOnlyCollection<string> AccountNumbers => _accountNumbers.AsReadOnly();

    public void ChangeLastname(string lastName)
    {
        Lastname = lastName;
    }
    public void ChangeAddress(string lastName)
    {
        Lastname = lastName;
    }
}