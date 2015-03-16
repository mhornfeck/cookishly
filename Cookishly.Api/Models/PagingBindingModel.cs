namespace Cookishly.Api.Models
{
    public class PagingBindingModel
    {
        public PagingBindingModel()
        {
            Offset = 0;
            Limit = 10;
        }

        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}