namespace CinemaWay.Web.Areas.Admin.Models.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models;
    using System.Collections.Generic;


    public class AdminListUsersViewModel
    {
        public IEnumerable<AdminListUsersModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
