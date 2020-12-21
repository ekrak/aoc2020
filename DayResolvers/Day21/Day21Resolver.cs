using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day21
{
    public class Day21Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            var squareImage = new FoodList(input);
            if (task == 1) return Resolve1Internal(squareImage);

            return Resolve2Internal(squareImage);
        }

        private string Resolve2Internal(FoodList input)
        {
            return input.CanonicalIngredientList();
        }

        private string Resolve1Internal(FoodList input)
        {
            return input.NumberOfSafeIngredients().ToString();
        }
    }
}