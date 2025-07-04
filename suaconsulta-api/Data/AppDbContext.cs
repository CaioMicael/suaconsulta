﻿using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Model;
using System.Collections.Generic;

namespace suaconsulta_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ModelDoctor> Doctor { get; set; }

        public DbSet<ModelPatient> Patient { get; set; }

        public DbSet<ModelConsultation> Consultation { get; set; }

        public DbSet<ModelDoctorSchedule> DoctorSchedule { get; set; }

        public DbSet<ModelUsers> Users { get; set; }
    }
}