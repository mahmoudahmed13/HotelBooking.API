using Ardalis.Specification.EntityFrameworkCore;
using HotelBooking.Domain.Entities;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.Repositories;
using HotelBooking.UseCases.Rooms.Specifications;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HotelBooking_ArchitectureTests
{
    public class RoomsPaginationTests
    {
        [Fact]
        public void Spec_Should_Set_Skip_And_Take()
        {
            var spec = new RoomsWithHotelSpec(new RoomQueryParams { PageIndex = 1, PageSize = 2 });
            spec.Skip.ShouldBe(0);
            spec.Take.ShouldBe(2);
        }

        [Fact]
        public async Task Repository_Should_Not_Return_More_Than_PageSize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=.;Database=Hotel_BookingDB;Trusted_Connection=true;TrustServerCertificate=true;")
                .Options;

            await using var db = new AppDbContext(options);
            var spec = new RoomsWithHotelSpec(new RoomQueryParams { PageIndex = 1, PageSize = 2 });
            var sql = SpecificationEvaluator.Default.GetQuery(db.Set<Room>(), spec).ToQueryString();
            var rooms = await new Repository<Room, int>(db).GetAllAsync(spec);

            rooms.Count.ShouldBeLessThanOrEqualTo(2, sql);
        }
    }
}
