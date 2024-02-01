﻿namespace CityBookMVCOnionDomain.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateAt { get; set; }

        public string CreatedBy { get; set; } = null!;

    }
}
