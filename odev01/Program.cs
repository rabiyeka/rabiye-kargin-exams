using System.Collections.Generic;
using FitnessCenterOop;
using FitnessCenterOop.Access;
using FitnessCenterOop.Equipment;
using FitnessCenterOop.Models;
using FitnessCenterOop.Plans;
using FitnessCenterOop.Promos;

Console.OutputEncoding = System.Text.Encoding.UTF8;

try { _ = new Member { FullName = "   ", Email = "a@b.com" }; }
catch (Exception ex) { Console.WriteLine("Beklenen hata: " + ex.Message); }

var m1 = new Member("Ayşe Yılmaz", 800m, "ayse@ornek.com") { Id = 1 };
var m2 = new Member { Id = 2, FullName = "Mehmet", Email = "m@x.com", MonthlyFee = 600m, JoinedOn = new DateTime(2024, 1, 5) };
m1.PrintSummary();
m2.PrintSummary();

var ekip = new List<GymEquipment>
{
    new CardioEquipment(1, "Koşu bandı", 2022, 1001, 18),
    new StrengthEquipment(2, "Squat rack", 2021, 1002, 200m)
};
foreach (var e in ekip) Console.WriteLine(e.Describe());

foreach (var g in new IAccessControl[] { new KeycardAccess(), new QrCodeAccess() })
    Console.WriteLine($"{g.GetType().Name} → {g.GrantAccess(1)}");

var profil = new MemberProfile(m1);
Console.WriteLine(profil.GetDisplayText());
profil.SendReminder("Ders: 19:00");

MembershipPlanBase plan = new PremiumPlan { Id = 1, PlanName = "Premium", BaseMonthlyFee = 800m };
var kasa = new GymCheckout(m1, plan);
kasa.AddEquipmentUsage((CardioEquipment)ekip[0], 30);
kasa.ApplyPromos(new IFeeAdjustment[] { new PercentagePromo(0.10m), new FixedAmountPromo(20m) });
kasa.CompleteCheckout();
