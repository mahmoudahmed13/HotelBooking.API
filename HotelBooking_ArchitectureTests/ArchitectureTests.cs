using HotelBooking.Domain.Entities;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NetArchTest.Rules;
using Shouldly;
using System.Reflection;

namespace HotelBooking_ArchitectureTests
{

    // These tests enforce, with code, the "Pure Domain" rule we've been relying
    // on by convention only. If someone (including future-you) accidentally adds
    // `using Microsoft.EntityFrameworkCore;` inside the Domain project, the build
    // still succeeds - but these tests fail, catching it immediately.
    public class ArchitectureTests
    {
        private static readonly Assembly DomainAssembly = typeof(BaseEntity<int>).Assembly;
        private static readonly Assembly InfrastructureAssembly = typeof(HotelBooking.Infrastructure.DependencyInjection).Assembly;
        private static readonly Assembly ApplicationAssembly = typeof(HotelBooking.UseCases.DependencyInjection).Assembly;
        private static readonly Assembly ApiAssembly = typeof(Program).Assembly;

        [Fact]
        public void Domain_Should_Not_Depend_On_Infrastructure()
        {
            var result = Types.InAssembly(DomainAssembly)
                .ShouldNot()
                .HaveDependencyOnAny(
                "HotelReservation.UseCases",
                "HotelReservation.API",
                "HotelReservation.Infrastructure")
                .GetResult();

            result.IsSuccessful.ShouldBeTrue(FormatFailingTypes(result));
        }

        [Fact]
        public void Infrastructure_Should_Not_Depend_On_Api()
        {
            var result = Types.InAssembly(InfrastructureAssembly)
               .ShouldNot()
               .HaveDependencyOnAny("HotelBooking.API")
               .GetResult();

            result.IsSuccessful.ShouldBeTrue(FormatFailingTypes(result));
        }
        [Fact]
        public void UseCases_Should_Not_Depend_On_Infrastructure_Or_Api()
        {
            var result = Types.InAssembly(ApplicationAssembly)
               .ShouldNot()
               .HaveDependencyOnAny("HotelReservation.API",
                "HotelReservation.Infrastructure")
               .GetResult();

            result.IsSuccessful.ShouldBeTrue(FormatFailingTypes(result));
        }
        [Fact]
        public void API_Should_Not_Depend_On_Domain()
        {
            var result = Types.InAssembly(ApiAssembly)
               .ShouldNot()
               .HaveDependencyOnAny("HotelBooking.Domain")
               .GetResult();

            result.IsSuccessful.ShouldBeTrue(FormatFailingTypes(result));
        }

        private static string FormatFailingTypes(TestResult result)
        {
            if (result.IsSuccessful || result.FailingTypes is null)
                return string.Empty;

            var names = result.FailingTypes.Select(t => t.FullName);
            return "Failing types: " + string.Join(", ", names);
        }
    }










}
