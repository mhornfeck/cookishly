namespace Cookishly.Services.Args
{
    public class PagingArgs
    {
        public PagingArgs()
        {
            Offset = 0;
            Limit = 10;
        }

        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}