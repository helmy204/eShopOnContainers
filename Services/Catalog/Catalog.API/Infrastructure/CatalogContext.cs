using Catalog.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }
    }
}
