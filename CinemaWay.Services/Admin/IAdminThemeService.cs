namespace CinemaWay.Services.Admin
{
    using Models.Themes;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminThemeService
    {
        Task Create(
            string title,
            DateTime startDate,
            DateTime endDate);

        Task<IEnumerable<AdminListThemesModel>> All(int page = 1);

        Task<int> Total();

        Task<IEnumerable<ShortThemeModel>> AllTitles();
    }
}
