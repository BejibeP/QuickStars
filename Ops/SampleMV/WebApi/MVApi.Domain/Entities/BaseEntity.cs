﻿namespace QuickStars.MVApi.Domain.Data.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

    }
}