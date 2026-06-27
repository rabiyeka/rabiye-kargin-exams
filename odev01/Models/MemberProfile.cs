namespace FitnessCenterOop.Models;

public class MemberProfile : IDisplayable, INotifiable
{
    public Member Member { get; }
    public MemberProfile(Member m) => Member = m;
    public string GetDisplayText() => $"{Member.FullName} <{Member.Email}>";
    public void SendReminder(string message) => Console.WriteLine($"[Bildirim → {Member.Email}] {message}");
}
