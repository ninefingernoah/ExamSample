

class Program {

    public static void openTest(HotelContext context){

        BulkDataSeeding();

        Console.WriteLine("\n Q1 Solution: \n");
        Solution.q1Solution(context);
        Console.WriteLine("\n Q2 Solution: \n");
        Solution.q2Solution(context, new DateOnly(2022,01,31));
        Console.WriteLine("\n Q3 Solution: \n");
        Solution.q3Solution(context);
        Console.WriteLine("\n Q4 Solution: \n");
        Solution.q4Solution(context, new DateOnly(2022,01,31));
        Console.WriteLine("\n Q5 Solution: \n");
        Solution.q5Solution(context);
        Console.WriteLine("\n Q6 Solution: \n");
        Solution.q6Solution(context, new DateOnly(2022,01,13));
        //Test.q7atest(context);
        //Test.q7btest(context);
        Console.WriteLine("\n Q8 Solution: \n");
        Solution.q8aSolution(context);
        Solution.q8bSolution(context);
    
    }
    
    public static void Main(string[] args)
    {
        BulkDataSeeding(false);    
     
        using (var context = new HotelContext())
        {         
            switch (args[1])
            {
                case "Q1": Solution.q1Solution(context); return;
                case "Q2": Solution.q2Solution(context, new DateOnly(2022,01,31)); return;
                case "Q3": Solution.q3Solution(context); return;
                case "Q4": Solution.q4Solution(context, new DateOnly(2022,01,13)); return;
                case "Q5": Solution.q5Solution(context); return;
                case "Q6": Solution.q6Solution(context, new DateOnly(2022, 1, 13)); return;
                case "Q7A": Test.q7atest(context); return;
                case "Q7B": Test.q7btest(context); return;
                case "Q8A": Solution.q8aSolution(context); return;
                case "Q8B": Test.q8aSolution(context);
                            Solution.q8bSolution(context); 
                            return;
                case "ALL": openTest(context); return;
                case "RESET" : Test.ResetSchema(context); return;
                default: throw new ArgumentException();
            }
        }
    }
        
    static void BulkDataSeeding(bool flag = true){
        using (var dbContext = new HotelContext()) {
            if (dbContext == null)
            {
                Console.WriteLine("No DbContext avaialable");
                return;
            }
              
            Test.ResetTables(dbContext);
            
            dbContext.roomType.AddRange(SeedData.SeedRoomType());
            dbContext.rooms.AddRange(SeedData.SeedRoom());
            dbContext.guests.AddRange(SeedData.SeedGuest());
            dbContext.bookings.AddRange(SeedData.SeedBookings(flag));
            Console.WriteLine("{0} Records Created.", dbContext.SaveChanges());
        };
    }
}