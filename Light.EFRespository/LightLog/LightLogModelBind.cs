using Light.Model.TableModel.LightLog;
using Microsoft.EntityFrameworkCore;

namespace Light.EFRespository.LightLog
{
    public class LightLogModelBind
    {
        public static void ConfigModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(m =>
            {
                m.Property(t => t.Message)
                        .IsRequired()
                        .HasColumnType("text");

                m.Property(t => t.Level)
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");
            });
        }
    }
}
