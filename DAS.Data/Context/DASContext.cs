using DAS.Data.Migrations;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Location;
using DAS.Model.Model.Media;
using DAS.Model.Model.Order;
using DAS.Model.Model.Product;
using DAS.Model.Model.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Data.Context
{
    public class DASContext:BaseDbContext<DASContext,Configuration>
    {
        public DASContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DASContext(string connectionString) :base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        //Dbsets

        //public LoginEntity Login { get; set; }
        //public CityEntity City { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        //public ImageEntity Image { get; set; }
        //public OrderDetailsEntity OrderDetails { get; set; }
        //public OrderItemsEntity OrderItems { get; set; }
        //public PaymentDetailsEntity PaymentDetails { get; set; }
        //public DiscountEntity DiscountEntity { get; set; }
        //public ProductCategoryEntity ProductCategory { get; set; }
        //public ProductEntity Product { get; set; }
        //public ProductInventoryEntity ProductInventory { get; set; }
        //public UserAddressEntity UserAddress { get; set; }
        //public UserEntity User { get; set; }
        //public UserPaymentEntity UserPayment { get; set; }

    }
}
