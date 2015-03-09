using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class RecipeStepEntityConfiguration : EntityBaseConfiguration<RecipeStepEntity>
    {
        public RecipeStepEntityConfiguration()
        {
            HasRequired(x => x.Recipe).WithMany(x => x.Steps);
        }
    }
}