﻿using DAS.Data.Migrations;
using DAS.Model.Model.Article;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Chat;
using DAS.Model.Model.Company;
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

        public virtual void BeginTransaction()
        {
            base.Database.BeginTransaction();
        }
        public virtual void RollBack()
        {
            base.Database.BeginTransaction().Rollback();
        }


        //Dbsets

        //public LoginEntity Login { get; set; }
        //public CityEntity City { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<LoginEntity> Login { get; set; }
        public DbSet<UserAddressEntity> UserAddress { get; set; }
        public DbSet<UserPaymentEntity> UserPayment { get; set; }
        public DbSet<ImageEntity> Image { get; set; }
        public DbSet<CompanyEntity> Company { get; set; }
        public DbSet<CompanyWorkersEntity> CompanyWorkers { get; set; }
        public DbSet<WorkerRoleEntity> WorkerRole { get; set; }
        public DbSet<ChatEntity> Chat { get; set; }
        public DbSet<MessageEntity> Message { get; set; }
        public DbSet<ArticleEntity> Article { get; set; }
        public DbSet<ArticleLikeDislikeEntity> ArticleLikeDislike { get; set; }
        public DbSet<ArticleCommentEntity> ArticleComment { get; set; }
        public DbSet<ArticleCommentReplyEntity> ArticleCommentReply { get; set; }

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
