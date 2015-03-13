using Cookishly.Domain;

namespace Cookishly.Services.Contract
{
    public class SaveRecipeArgs
    {
        public Recipe Recipe { get; set; }
        public string Username { get; set; }
    }
}