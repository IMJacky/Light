using Light.Model.TableModel;
using Light.Model.TableModel.LightAuthority;
using Light.Model.TableModel.LightBlog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Light.EFRespository.LightBlog
{
    public class LightBlogModelBind
    {
        public static void ConfigModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(m =>
            {
                m.Property(t => t.Content)
                        .IsRequired()
                        .HasColumnType("nvarchar(4000)");

                m.Property(t => t.Title)
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                m.Property(t => t.SubTitle)
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");
            });
        }
    }
}
