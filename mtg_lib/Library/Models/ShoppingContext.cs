using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace mtg_lib.Library.Models
{
    public partial class ShoppingContext : DbContext
    {
        public ShoppingContext()
        {
        }

        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cartitem> Cartitems { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Shopping;Username=postgres;Password=1335");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cartitem>(entity =>
            {
                entity.HasKey(e => e.Productid)
                    .HasName("cartitems_pkey");

                entity.ToTable("cartitems");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Productimageurl)
                    .HasMaxLength(255)
                    .HasColumnName("productimageurl");

                entity.Property(e => e.Productname)
                    .HasMaxLength(255)
                    .HasColumnName("productname");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Totalprice).HasColumnName("totalprice");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_user");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("usuario_pkey");

                entity.ToTable("usuario");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Clave)
                    .HasMaxLength(500)
                    .HasColumnName("clave");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .HasColumnName("correo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
