﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlowerPower.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_A3D6D6_FlowerPowerLuukEntities2 : DbContext
    {
        public DB_A3D6D6_FlowerPowerLuukEntities2()
            : base("name=DB_A3D6D6_FlowerPowerLuukEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<artikel> artikels { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<bestelling> bestellings { get; set; }
        public virtual DbSet<bestelregel> bestelregels { get; set; }
        public virtual DbSet<klant> klants { get; set; }
        public virtual DbSet<medewerker> medewerkers { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<vestiging> vestigings { get; set; }
    }
}
