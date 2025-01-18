﻿using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.Interface;

namespace EPYSLSAILORAPP.Domain.General
{
    public abstract class DapperBaseEntity : IDapperBaseEntity
    {
        /// <summary>
        /// EntityState
        /// </summary>
        [Write(false)]
        public EntityState EntityState { get; set; }

        [Write(false)]
        public int TotalRows { get; set; }

        [Write(false)]
        public abstract bool IsModified { get; }

        [Write(false)]
        public virtual bool IsNew => EntityState == EntityState.Added;

        /// <summary>
        /// Ctor
        /// </summary>
        public DapperBaseEntity()
        {
            EntityState = EntityState.Added;
        }
    }
}
