using Light.Model.TableModel.LightAuthority;
using Microsoft.EntityFrameworkCore;

namespace Light.EFRespository.LightAuthority
{
    public class LightAuthorityModelBind
    {
        public static void ConfigModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(m =>
            {
                m.Property(t => t.UserName)
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                m.Property(t => t.Email)
                        .HasColumnType("nvarchar(50)");

                m.Property(t => t.Password)
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<Role>(m =>
            {
                m.Property(t => t.RoleName)
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<ModuleSystem>(m =>
            {
                m.Property(t => t.ModuleName)
                        .IsRequired()
                       .HasColumnType("nvarchar(50)");

                m.Property(t => t.RouteUrl)
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");
            });

            modelBuilder.Entity<UserRole>(m =>
            {
                m.Property(t => t.UserId)
                        .IsRequired();

                m.Property(t => t.RoleId)
                        .IsRequired();
            });

            modelBuilder.Entity<RoleModule>(m =>
            {
                m.Property(t => t.ModuleId)
                        .IsRequired();

                m.Property(t => t.RoleId)
                        .IsRequired();
            });
        }
    }
}
