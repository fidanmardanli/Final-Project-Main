using System;

namespace AASA_Back_End.Models
{
    public abstract class BaseEntity
    {

        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
