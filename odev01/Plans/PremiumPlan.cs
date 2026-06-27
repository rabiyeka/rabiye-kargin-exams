namespace FitnessCenterOop.Plans;

public class PremiumPlan : MembershipPlanBase
{
    public override string GetPlanKind() => "Premium üyelik";

    public override decimal ApplyPromoDiscount(decimal rate)
    {
        if (rate is < 0m or > 1m)
            throw new ArgumentOutOfRangeException(nameof(rate), "0–1 arası olmalı.");
        return BaseMonthlyFee * (1m - rate);
    }
}
