﻿using Barber.Colocho.Domain.Entity.Columns;

namespace Barber.Colocho.Domain.Entity.Version
{
    public class VersionDomainDto : DefaultColumns
    {
        public Guid Id { get; set; }
        public double VersionApi { get; set; }
    }
}
