using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class ProfileEntityConfiguration : EntityBaseConfiguration<ProfileEntity>
    {
        public ProfileEntityConfiguration()
        {
            HasMany(x => x.Ingredients).WithRequired(x => x.Profile);
            HasMany(x => x.Recipes).WithRequired(x => x.Profile).WillCascadeOnDelete(false);
            HasMany(x => x.Users).WithMany(x => x.Profiles);
        }
    }
}