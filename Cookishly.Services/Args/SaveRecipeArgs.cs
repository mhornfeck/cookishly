using Cookishly.Domain;

namespace Cookishly.Services.Args
{
    public class SaveRecipeArgs
    {
        public Recipe Recipe { get; set; }
        public string Username { get; set; }
    }
}