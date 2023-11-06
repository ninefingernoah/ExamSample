
using System.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

class Solution {
    
    public static void q1Solution(HotelContext db)
    {
     // Exercise 1: Give the booking detail of given guest booking details (for GuestID 10).  
     // The result should include booking date, room number, and number of nights.
      	var bookingDetailsGuest10 = db.bookings.Where(b => b.GuestID == 10);

    }

    public static void q2Solution(HotelContext db, DateOnly date)
    {
     // Exercise: 2:  List down all the guest names, and room number, 
     // having booking on specific date (2022 - 01 - 31) 	
        var guestBookings = db.bookings
                            .Where(b => b.BookingDate.CompareTo(new DateOnly(2022, 1, 31)) == 0)
                            .Include(b => b.guest)
                            .Select(b => new {b.guest.Name, b.RoomNumber});
    }

    public static void q3Solution(HotelContext db)
    {
     // Exercise 3: List down number of bookings per day where there are more than 1 bookings
        var bookingsPerDay = db.bookings
                            .GroupBy(b => b.BookingDate)
                            .Where(g => g.Count() > 1)
                            .Select(g => new{ g.Key, Count = g.Count()});
    }

    public static void q4Solution(HotelContext db, DateOnly date)
    {
     // Exercise 4. List the rooms that are free on '2022-01-13'.
        var bookedRooms = db.bookings
                            .Where(b => b.BookingDate == new DateOnly(2022,1,13))
                            .Include(b => b.room)
                            .Select(b => b.room);
        var freeRooms = db.rooms
                        .Except(bookedRooms);
    }

    public static void q5Solution(HotelContext db) 
    {
     // Exercise: 5:  List down top 5 valued customers, with their id and spending 
     // HINT: a valued customer is the on with max amount spent, 
     // amount = Nights * Price for each booking of a customer
        var topFive = db.bookings
                    .Include(b => new {b.guest, b.room})
                    .ThenInclude(b => b.room.roomType)
                    .GroupBy(b => b.guest)
                    .Select(g => new { g.Key, spent = g.Sum(g => g.Nights * g.room.roomType.Price) })
                    .OrderByDescending(g => g.spent)
                    .Take(5);
    }

    public static void q6Solution(HotelContext db, DateOnly date)
    {
     // Exercise 6: 

    }
   
    //     ****  Ex7 in Model.cs  **** 

    public static void q8aSolution(HotelContext db)
    {
     // Exercise 8a: 
        var alice = new Employee(1, "Alice");
        var bob = new Employee(2, "Bob");
        var claudia = new Employee(3, "Claudia");
        var diana = new Employee(4, "Diana");
        var faris = new Employee(5, "Faris");
        bob.Subordinates.Add(diana);
        alice.Subordinates.AddRange(
            new List<Employee>() {
                claudia,
                bob
            }
        );
        db.employee.Add(alice);
        db.employee.Add(faris);
        db.SaveChanges();
    
    }

    public static void q8bSolution(HotelContext db)
    {
     // Exercise 8b: 
        
    }

}