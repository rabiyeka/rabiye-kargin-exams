namespace FitnessCenterOop.Plans;

public abstract class MembershipPlanBase
{
    public int Id { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public decimal BaseMonthlyFee { get; set; }

    public decimal GetQuarterlyTotal() => BaseMonthlyFee * 3m;

    public abstract string GetPlanKind();
    public abstract decimal ApplyPromoDiscount(decimal rate);
}
