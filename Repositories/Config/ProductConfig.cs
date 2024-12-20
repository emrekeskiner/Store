using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p=>p.ProductName).IsRequired();
            builder.Property(p=>p.Price).IsRequired();

            builder.HasData(
                 new Product(){ProductId=1, CategoryId=2, ImageUrl="/images/1.jpg", ProductName="Bilgisayar", Price= 17_000 , ShowCase=true},
                new Product(){ProductId=2, CategoryId=3, ImageUrl="/images/2.jpg", ProductName="Mouse", Price= 1_000 , ShowCase=false},
                new Product(){ProductId=3, CategoryId=3, ImageUrl="/images/3.jpg", ProductName="Monitor", Price= 11_500 , ShowCase=false},
                new Product(){ProductId=4, CategoryId=3, ImageUrl="/images/4.jpg", ProductName="Klavye", Price= 2_000 , ShowCase=false},
                new Product(){ProductId=5, CategoryId=2, ImageUrl="/images/5.jpg", ProductName="Notebook", Price= 35_000 , ShowCase=true},
                new Product(){ProductId=6, CategoryId=2, ImageUrl="/images/5.jpg", ProductName="Asus Zenbook", Price= 39_000 , ShowCase=true},
                new Product(){ProductId=7, CategoryId=2, ImageUrl="/images/5.jpg", ProductName="Macbook Air", Price= 32_000 , ShowCase=true}
            );
        }
    }
}