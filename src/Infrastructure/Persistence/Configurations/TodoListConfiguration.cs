using LowCostFligtsBrowser.Domain.Common;
using LowCostFligtsBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCostFligtsBrowser.Infrastructure.Persistence.Configurations
{
    public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .OwnsOne(b => b.Colour);
            builder.Property(t => t.Status)
                     .HasDefaultValue<SoftDeleteStatus>(SoftDeleteStatus.Active)
                     .HasConversion<int>();
        }
    }
}