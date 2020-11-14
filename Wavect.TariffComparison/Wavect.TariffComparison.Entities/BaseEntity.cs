using System;
using System.Collections.Generic;
using System.Text;
using Wavect.TariffComparison.Interfaces.Entities;

namespace Wavect.TariffComparison.Entities
{
    public class BaseEntity : IBaseEntity
    {
        #region properties
        public Guid Id { get; set; }
        #endregion
    }
}
