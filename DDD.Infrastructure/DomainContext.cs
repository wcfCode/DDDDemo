﻿using System;
using System.Collections.Generic;
using System.Text;
using DDD.Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DDD.Domain.OrderAggregate;
using DDD.Domain.UserAggregate;
using DDD.Infrastructure.EntityConfigurations;
using DotNetCore.CAP;

namespace DDD.Infrastructure
{
    public class DomainContext : EFContext
    {
        public DomainContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
