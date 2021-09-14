namespace BoBo.Decorating
{
    public class TextOf : IBody
    {
        private readonly IFootprint footprint;

        public TextOf(IFootprint footprint)
        {
            this.footprint = footprint;
        }

        public string Text()
        {
            return footprint
                .MakeFootprint()
                .ToString();
        }
    }
}
