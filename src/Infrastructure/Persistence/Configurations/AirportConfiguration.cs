using LowCostFligtsBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCostFligtsBrowser.Infrastructure.Persistence.Configurations
{
    public class AirportConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airport");
            builder.Property(e => e.Id).HasColumnName("Id").UseIdentityColumn(1, 1);
            builder.Property(e => e.GMT).HasColumnName("gmt");
            builder.Property(e => e.IATACode).HasColumnName("iata_code").IsUnicode();
            builder.Property(e => e.CityIATACode).HasColumnName("city_iata_code");
            builder.Property(e => e.ICAOCode).HasColumnName("icao_code");
            builder.Property(e => e.CountryISO2).HasColumnName("country_iso2");
            builder.Property(e => e.GeonameId).HasColumnName("geoname_id");
            builder.Property(e => e.Latitude).HasColumnName("latitude");
            builder.Property(e => e.Longitude).HasColumnName("longitude");
            builder.Property(e => e.AirportName).HasColumnName("airport_name");
            builder.Property(e => e.CountryName).HasColumnName("country_name");
            builder.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            builder.Property(e => e.TimeZone).HasColumnName("timezone");
        }
    }
}
