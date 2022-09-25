using Microsoft.EntityFrameworkCore;
using MVC_TemplateApp.Models;
using MVC_TemplateApp.Models.Common;

namespace MVC_TemplateApp.Data;
 
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {

    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }


    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    var datas = ChangeTracker.Entries<BaseEntity>();

    //    foreach (var item in datas)
    //    {
    //        _ = item.State switch
    //        {
    //            EntityState.Added => item.Entity.CreatedDate = DateTime.Now,
    //            EntityState.Modified => item.Entity.ModifiedDate = DateTime.Now
    //        };
    //    }
    //    return await base.SaveChangesAsync(cancellationToken);
    //}



}
