using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class ProfileEntityConfiguration : EntityBaseConfiguration<ProfileEntity>
    {
        public ProfileEntityConfiguration()
        {
            ToTable("Profiles");
            HasMany(x => x.Ingredients).WithOptional(x => x.Profile);
            HasMany(x => x.Recipes).WithRequired(x => x.Profile).WillCascadeOnDelete(false);
            HasMany(x => x.Users).WithRequired(x => x.Profile);
        }
    }
}