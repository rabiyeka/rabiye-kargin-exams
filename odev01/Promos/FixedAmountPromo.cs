namespace FitnessCenterOop.Promos;

public class FixedAmountPromo : IFeeAdjustment
{
    private readonly decimal _d;
    public FixedAmountPromo(decimal d) => _d = d >= 0 ? d
        : throw new ArgumentOutOfRangeException(nameof(d));
    public decimal ApplyToMonthly(decimal a) => Math.Max(0, a - _d);
}
