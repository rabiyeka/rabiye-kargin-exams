namespace FitnessCenterOop.Equipment;

public class GymEquipment
{
    public int Id { get; }
    public string Name { get; }
    public int PurchaseYear { get; }
    public bool IsOperational { get; }
    protected int _assetTag;

    public GymEquipment(int id, string name, int purchaseYear, bool isOperational, int assetTag)
    {
        Id = id;
        ValidateName(name);
        Name = name;
        PurchaseYear = purchaseYear;
        IsOperational = isOperational;
        _assetTag = assetTag;
    }

    protected void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ekipman adı gerekir.", nameof(name));
    }

    public virtual string Describe() =>
        $"Ekipman #{Id} {Name} — etiket: {_assetTag} ({PurchaseYear}, çalışır: {IsOperational})";

    public virtual decimal EstimatedMaintenanceCost() =>
        IsOperational ? 50m + (DateTime.UtcNow.Year - PurchaseYear) * 10m : 0m;
}
