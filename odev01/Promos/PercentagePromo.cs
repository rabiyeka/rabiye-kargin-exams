namespace FitnessCenterOop.Promos;

public class PercentagePromo : IFeeAdjustment
{
    private readonly decimal _r;
    public PercentagePromo(decimal r) => _r = r is >= 0 and <= 1 ? r
        : throw new ArgumentOutOfRangeException(nameof(r));
    public decimal ApplyToMonthly(decimal a) => a * (1m - _r);
}
