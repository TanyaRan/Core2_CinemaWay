namespace CinemaWay.Tests.Web.Areas.Admin.Controllers
{
    using CinemaWay.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ThemesControllerTests
    {
        public ThemesControllerTests()
        {
            Tests.Initialize();
        }


        private CinemaWayDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<CinemaWayDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CinemaWayDbContext(dbOptions);
        }
    }
}
