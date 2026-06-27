namespace FitnessCenterOop.Models;

public class MembershipContract
{
    public Guid ContractId { get; }
    public MembershipContract() => ContractId = Guid.NewGuid();
}
