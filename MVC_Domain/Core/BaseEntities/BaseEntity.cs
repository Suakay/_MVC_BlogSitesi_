using MVC_Domain.Core.Interfaces;
using MVC_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Domain.Core.BaseEntities
{
    public class BaseEntity : IUpdatebleEntity
    {
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Status Status { get; set; }
    }
}
