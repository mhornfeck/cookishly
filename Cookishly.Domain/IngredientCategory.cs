using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cookishly.Domain
{
    public enum IngredientCategory
    {
        Beverages,
        [Display(Name = "Bread/Bakery")]
        BreadBakery,
        [Display(Name = "Canned/Jarred")]
        CannedJarred,
        Dairy,
        [Display(Name = "Dry/Grains")]
        DryGrains,
        Baking,
        Frozen,
        Meat,
        Produce,
        Seasonings,
        Oils,
        Other
    }
}