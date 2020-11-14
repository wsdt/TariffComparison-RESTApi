using System;
using System.Collections.Generic;
using System.Text;

namespace Wavect.TariffComparison.Interfaces.Entities
{
    public interface IBaseEntity : IModel
    {
        public Guid Id { get; set; }
    }
}
