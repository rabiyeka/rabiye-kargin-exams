using FitnessCenterOop.Equipment;
using FitnessCenterOop.Models;
using FitnessCenterOop.Plans;
using FitnessCenterOop.Promos;

namespace FitnessCenterOop;

public class GymCheckout
{
    public Member Member { get; }
    public MembershipPlanBase Plan { get; }
    public decimal RunningTotal { get; private set; }

    public GymCheckout(Member member, MembershipPlanBase plan)
    {
        Member = member;
        Plan = plan;
        RunningTotal = plan.BaseMonthlyFee;
    }

    public void AddEquipmentUsage(GymEquipment e, int minutes)
    {
        if (minutes <= 0) return;
        RunningTotal += minutes * 2m + e.EstimatedMaintenanceCost() * 0.01m;
    }

    public void ApplyPromos(IEnumerable<IFeeAdjustment> promos)
    {
        foreach (var p in promos) RunningTotal = p.ApplyToMonthly(RunningTotal);
    }

    public void CompleteCheckout()
    {
        Console.WriteLine("=== ÖDEME ÖZETİ ===");
        Member.PrintSummary();
        Console.WriteLine(
            $"Plan: {Plan.PlanName} | tür: {Plan.GetPlanKind()} | 3 aylık: {Plan.GetQuarterlyTotal():N2} | aylık (promo %10): {Plan.ApplyPromoDiscount(0.10m):N2}");
        Console.WriteLine($"Hesaplanan toplam: {RunningTotal:N2} TL");
    }
}
