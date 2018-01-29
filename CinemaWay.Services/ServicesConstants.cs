namespace CinemaWay.Services
{
    public class ServicesConstants
    {
        public const int StoriesPageSize = 4;

        public const int ProjectionsPageSize = 4;
        public const int AllProjectionsPageSize = 6;

        public const int MainPageSize = 5;

        public const string PdfTicketFormat = @"
<div>
    <p style='color: #73C2FB; text-align: center;'>
        ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
    </p>
    <h1 style='text-align: center;'>{0}</h1>
    <h4 style='color: #B90E0A; text-align: center;'>Start: {3}:00 - {2}, CinemaWay - Sofia</h4>
    <h2 style='text-align: center; font-style: italic; color: #999999'>Week theme: {1}</h2>
    <p style='color: #73C2FB; text-align: center;'>
        * * *
    </p>
    <h3 style='text-align: center;'>Welcome to our CinemaWay, {4} {5}! Have a nice time! Have fun!</h3>
    <p style='font-size: 0.85em; font-style: italic; color: #777777;'>Ticket created on: {6}</p>
    <p style='color: #73C2FB; text-align: center;'>
        ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
    </p>
</div>
";
    }
}
