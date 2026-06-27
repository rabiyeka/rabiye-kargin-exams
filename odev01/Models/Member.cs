namespace FitnessCenterOop.Models;

public class Member
{
    public int Id { get; set; }
    public DateTime JoinedOn { get; set; } = DateTime.Today;
    public MembershipStatus Status { get; set; } = MembershipStatus.Active;

    private string _fullName = string.Empty;
    private string _email = string.Empty;
    private decimal _monthlyFee;

    public string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ad soyad zorunludur.", nameof(value));
            _fullName = value.Trim();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                throw new ArgumentException("Geçerli e-posta gerekir.", nameof(value));
            _email = value.Trim();
        }
    }

    public decimal MonthlyFee
    {
        get => _monthlyFee;
        set
        {
            if (value < 0m)
                throw new ArgumentOutOfRangeException(nameof(value), "Aylık ücret negatif olamaz.");
            _monthlyFee = value;
        }
    }

    public decimal YearlyEstimate => MonthlyFee * 12m;
    public bool IsFeeValid => MonthlyFee > 0m;

    public Member() : this("Yeni üye", 0m, "yeni@ornek.com") { }

    public Member(string fullName, decimal monthlyFee, string email)
    {
        FullName = fullName;
        MonthlyFee = monthlyFee;
        Email = email;
    }

    public void PrintSummary() =>
        Console.WriteLine($"ID: {Id} | {FullName} | {Email} | Aylık: {MonthlyFee:N2} TL | {JoinedOn:yyyy-MM-dd} | {StatusLabel(Status)} | Yıllık: {YearlyEstimate:N2}");

    public static string StatusLabel(MembershipStatus s) => s switch
    {
        MembershipStatus.Active => "Aktif",
        MembershipStatus.Frozen => "Donduruldu",
        MembershipStatus.Cancelled => "İptal",
        _ => s.ToString()
    };

    public decimal CalculateQuarterlyCost() => MonthlyFee * 3m;

    public decimal CalculateQuarterlyCost(decimal discountRate) =>
        discountRate is < 0m or > 1m
            ? throw new ArgumentOutOfRangeException(nameof(discountRate))
            : CalculateQuarterlyCost() * (1m - discountRate);
}
