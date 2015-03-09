using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class EntityBaseConfiguration<T> : EntityTypeConfiguration<T> where T : EntityBase
    {
        public EntityBaseConfiguration()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}