namespace FitnessCenterOop.Equipment;

public class StrengthEquipment : GymEquipment
{
    public decimal MaxWeightKg { get; }
    public StrengthEquipment(int id, string n, int y, int t, decimal kg) : base(id, n, y, true, t) => MaxWeightKg = kg;
    public override string Describe() => $"{base.Describe()} | ağırlık, max {MaxWeightKg:N0} kg";
    public override decimal EstimatedMaintenanceCost() => base.EstimatedMaintenanceCost() + 40m;
}
