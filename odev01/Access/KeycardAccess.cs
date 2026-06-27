namespace FitnessCenterOop.Access;

public class KeycardAccess : IAccessControl
{
    public bool GrantAccess(int memberId) => memberId > 0;
}
