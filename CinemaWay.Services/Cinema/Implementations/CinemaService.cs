namespace CinemaWay.Services.Cinema.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServicesConstants;

    public class CinemaService : ICinemaService
    {
        private readonly CinemaWayDbContext db;
        private readonly IPdfGenerator pdfGenerator;

        public CinemaService(CinemaWayDbContext db,
            IPdfGenerator pdfGenerator)
        {
            this.db = db;
            this.pdfGenerator = pdfGenerator;
        }

        public async Task<IEnumerable<ListProjectionWithMovieModel>> Active(int page = 1)
            => await this.db
                .Projections
                .OrderBy(p => p.Date)
                .Where(p => p.Date >= DateTime.UtcNow)
                .Skip((page - 1) * ProjectionsPageSize)
                .Take(ProjectionsPageSize)
                .ProjectTo<ListProjectionWithMovieModel>()
                .ToListAsync();

        public async Task<int> TotalActive()
            => await this.db.Projections
                .Where(p => p.Date >= DateTime.UtcNow)
                .CountAsync();

        public async Task<IEnumerable<ListProjectionWithMovieModel>> All(int page = 1)
            => await this.db
                .Projections
                .OrderByDescending(p => p.Date)
                .Skip((page - 1) * AllProjectionsPageSize)
                .Take(AllProjectionsPageSize)
                .ProjectTo<ListProjectionWithMovieModel>()
                .ToListAsync();

        public async Task<int> Total()
            => await this.db.Projections.CountAsync();

        public async Task<IEnumerable<ListProjectionWithMovieModel>> FindAsyncMovie(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Projections
                .OrderBy(p => p.Date)
                .Where(p => p.Movie.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<ListProjectionWithMovieModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<ListProjectionWithMovieModel>> FindAsyncTheme(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Projections
                .OrderBy(p => p.Date)
                .Where(p => p.Theme.Title.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<ListProjectionWithMovieModel>()
                .ToListAsync();
        }

        public async Task<TModel> ByIdAsync<TModel>(int id) where TModel : class
            => await this.db
                .Projections
                .Where(p => p.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> UserBookedATicket(int projectionId, string userId)
            => await this.db
            .Projections
            .AnyAsync(p => (p.Id == projectionId)
                && p.Visitors.Any(v => v.VisitorId == userId));

        public async Task<bool> BookTicket(int projectionId, string userId)
        {
            var projectionInfo = await this.GetProjectionInfo(projectionId, userId);

            if (projectionInfo == null
                || projectionInfo.Date < DateTime.UtcNow
                || projectionInfo.UserHasATicket
                || projectionInfo.VisitorsCount >= 50)
            {
                return false;
            }

            var ticket = new UserProjections
            {
                ProjectionId = projectionId,
                VisitorId = userId
            };

            this.db.Add(ticket);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RefuseTicket(int projectionId, string userId)
        {
            var projectionInfo = await this.GetProjectionInfo(projectionId, userId);

            if (projectionInfo == null
                || projectionInfo.Date < DateTime.UtcNow
                || !projectionInfo.UserHasATicket)
            {
                return false;
            }

            var bookedTicket = await this.db
                .FindAsync<UserProjections>(projectionId, userId);

            this.db.Remove(bookedTicket);

            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<ProjectionWithUserInfo> GetProjectionInfo(int projectionId, string userId)
            => await this.db
                .Projections
                .Where(p => p.Id == projectionId)
                .Select(p => new ProjectionWithUserInfo
                {
                    Date = p.Date,
                    UserHasATicket = p.Visitors.Any(v => v.VisitorId == userId),
                    VisitorsCount = p.Visitors.Count
                })
                .FirstOrDefaultAsync();

        public async Task<byte[]> GetPdfTicket(int projectionId, string userId)
        {
            var bookedTicket = await this.db
                .FindAsync<UserProjections>(projectionId, userId);

            if (bookedTicket == null)
            {
                return null;
            }

            var ticket = await this.db
                .Projections
                .Where(p => p.Id == projectionId)
                .Select(p => new
                {
                    Movie = p.Movie.Name,
                    Theme = p.Theme.Title,
                    Date = p.Date,
                    StartTime = p.StartTime,
                    FirstName = p.Visitors
                        .Where(v => v.Visitor.Id == userId)
                        .Select(v => v.Visitor.FirstName)
                        .FirstOrDefault(),
                    LastName = p.Visitors
                        .Where(v => v.Visitor.Id == userId)
                        .Select(v => v.Visitor.LastName)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            return this.pdfGenerator.GeneratePdfFromHtml(
                string.Format(PdfTicketFormat,
                ticket.Movie,
                ticket.Theme,
                ticket.Date.ToShortDateString(),
                ticket.StartTime,
                ticket.FirstName,
                ticket.LastName,
                DateTime.UtcNow.ToShortDateString()));
        }
    }
}
