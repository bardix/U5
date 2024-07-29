namespace EsSettimanaleU5S3.DataModel
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<IngredientProduct> IngredientProducts { get; set; }
    }

}
