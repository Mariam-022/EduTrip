using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduTrip.Models; // تأكدي إن ده اسم البروجكت بتاعك
using System;
using System.Linq;

public class BookingController : Controller
{
    // 1. تعريف الداتا بيز
    private readonly EduTripContext _context;

    // 2. حقن الاعتمادية (Dependency Injection)
    public BookingController(EduTripContext context)
    {
        _context = context;
    }

    // 3. عرض المرشدين للطالب
    [HttpGet]
    public IActionResult Index()
    {
        var mentors = _context.MentorProfiles.Include(m => m.User).ToList();
        return View(mentors);
    }

    // 4. استلام طلب الحجز وتنفيذ البزنس لوجيك
    [HttpPost]
    public IActionResult Confirm(Guid mentorId)
    {
        // نجيب بيانات المرشد عشان نعرف سعر الساعة
        var mentor = _context.MentorProfiles.Find(mentorId);

        // نجيب أي طالب من الداتا بيز للمناقشة
        var student = _context.Users.FirstOrDefault(u => u.Role == "Student");

        // أ. إنشاء طلب مالي (Order)
        var newOrder = new Order
        {
            Id = Guid.NewGuid(),
            UserId = student.Id,
            OrderType = "Session",
            Amount = mentor.HourlyRate,
            Currency = "EGP",
            Status = "Paid",
            CreatedAt = DateTime.Now
        };
        _context.Orders.Add(newOrder);

        // ب. توزيع الأرباح (70% للمرشد و 30% للمنصة)
        var earnings = new MentorEarning
        {
            Id = Guid.NewGuid(),
            MentorId = mentorId,
            OrderId = newOrder.Id,
            GrossAmount = mentor.HourlyRate,
            MentorShare = mentor.HourlyRate * 0.70m,
            PlatformShare = mentor.HourlyRate * 0.30m,
            CommissionRate = 70.00m,
            Status = "Pending",
            CreatedAt = DateTime.Now
        };
        _context.MentorEarnings.Add(earnings);

        // ج. إنشاء الحجز (Booking)
        var newBooking = new Booking
        {
            Id = Guid.NewGuid(),
            OrderId = newOrder.Id,
            StudentId = student.Id,
            MentorId = mentorId,
            AvailabilityId = Guid.NewGuid(), // رقم وهمي عشان نعدي الـ Validation
            DurationMinutes = 60,
            Status = "Confirmed",
            ScheduledAt = DateTime.Now.AddDays(1),
            CreatedAt = DateTime.Now
        };
        _context.Bookings.Add(newBooking);

        // د. حفظ التغييرات في الداتا بيز
        _context.SaveChanges();

        return Content("تم الحجز بنجاح! راجعي جدول MentorEarnings هتلاقي الأرباح اتقسمت 70/30");
    }
}