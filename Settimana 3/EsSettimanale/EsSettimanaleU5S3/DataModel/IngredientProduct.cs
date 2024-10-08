﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsSettimanaleU5S3.DataModel
{
    public class IngredientProduct
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

}
