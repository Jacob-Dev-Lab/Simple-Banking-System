class Customer(string lastName, string otherNames, DateOnly dateOfBirth, string email)
{
    public Guid CustomerId { get; } = Guid.NewGuid();
    public string LastName { get; private set; } = lastName;
    public string OtherNames { get; private set; } = otherNames;
    public DateOnly DateOfBirth { get; } = dateOfBirth;
    public string Email { get; set; } = email;

    private readonly List<string> _accountNumbers = [];
    public IReadOnlyCollection<string> AccountNumbers => _accountNumbers.AsReadOnly();

    public void LinkAccountNumber(string accountNumber)
    {
        if (accountNumber == null)
            throw new ArgumentNullException(nameof(accountNumber));

        _accountNumbers.Add(accountNumber);
    }

    public void ChangeLastname(string newLastName)
    {
        LastName = newLastName;
    }
    public void ChangeEmailAddress(string newEmailAddress)
    {
        Email = newEmailAddress;
    }
}