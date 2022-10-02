using Microsoft.EntityFrameworkCore;
using MVC_TemplateApp.Models;
using System;

namespace MVC_TemplateApp.Data
{
    public class MVC_TemplateAppContext : DbContext
    {
        public MVC_TemplateAppContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
    }
}
