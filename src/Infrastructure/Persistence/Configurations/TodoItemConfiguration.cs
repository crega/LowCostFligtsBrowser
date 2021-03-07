using LowCostFligtsBrowser.Domain.Common;
using LowCostFligtsBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCostFligtsBrowser.Infrastructure.Persistence.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Status)
                        .HasDefaultValue<SoftDeleteStatus>(SoftDeleteStatus.Active)
                        .HasConversion<int>();
        }
    }
}