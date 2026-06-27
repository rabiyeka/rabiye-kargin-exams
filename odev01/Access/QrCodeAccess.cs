namespace FitnessCenterOop.Access;

public class QrCodeAccess : IAccessControl
{
    public bool GrantAccess(int memberId) => memberId % 2 == 0;
}
