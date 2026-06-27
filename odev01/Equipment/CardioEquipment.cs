namespace FitnessCenterOop.Equipment;

public class CardioEquipment : GymEquipment
{
    public double MaxSpeedKmh { get; }
    public CardioEquipment(int id, string n, int y, int t, double max) : base(id, n, y, true, t) => MaxSpeedKmh = max;
    public override string Describe() => $"{base.Describe()} | kardiyo, max {MaxSpeedKmh:0.#} km/s";
    public override decimal EstimatedMaintenanceCost() => base.EstimatedMaintenanceCost() + 25m;
}
