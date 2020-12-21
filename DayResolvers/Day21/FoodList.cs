using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day21
{
    public class FoodList
    {
        private List<Food> foods = new List<Food>();
        private Dictionary<string,List<string>> foodAllergens = new Dictionary<string, List<string>>();
        private Dictionary<string, string> foodAllergensEvaluated = new Dictionary<string, string>();
        private List<string> allIngredients = new List<string>();

        public FoodList(StreamReader input)
        {
            string line = input.ReadLine();
            while (line != null)
            {
                var currentFood = new Food(line);
                foods.Add(currentFood);
                allIngredients.AddRange(currentFood.Foods);

                if (currentFood.Allergens.Any())
                {
                    currentFood.Allergens.ForEach(a =>
                    {
                        if (foodAllergens.ContainsKey(a))
                        {
                            foodAllergens[a] = foodAllergens[a].Intersect(currentFood.Foods).ToList();
                        }
                        else
                        {
                            foodAllergens.Add(a, currentFood.Foods);
                        }
                    });
                }
                
                line = input.ReadLine();
            }

            EvaluateAllergens();
        }

        public long NumberOfSafeIngredients()
        {
            long sum = 0;
            var safeIngredients = allIngredients.Except(foodAllergensEvaluated.Values).ToList();
            foreach (var allIngredient in allIngredients)
            {
                sum += safeIngredients.Count(si => si == allIngredient);
            }

            return sum;
        }

        public string CanonicalIngredientList()
        {
            var allergens = foodAllergensEvaluated.Keys.ToList();
            allergens.Sort();
            string result = "";
            foreach (var allergen in allergens)
            {
                result += foodAllergensEvaluated[allergen] + ",";
            }

            return result.Substring(0, result.Length - 1);

        }


        private void EvaluateAllergens()
        {
            var filtered = foodAllergens.Where(fa => fa.Value.Count == 1).ToDictionary(x => x.Key, x => x.Value);
            foreach (var filter in filtered)
            {
                foodAllergensEvaluated.Add(filter.Key, filter.Value.Single());
            }


            var unfiltered = foodAllergens.Where(fa => fa.Value.Count != 1).ToDictionary(x => x.Key, x => x.Value);

            while (foodAllergensEvaluated.Count != foodAllergens.Count)
            {
                foreach (var unfilter in unfiltered)
                {
                    foodAllergensEvaluated.Values.ToList().ForEach(v => unfilter.Value.Remove(v));
                    if (unfilter.Value.Count == 1)
                    {
                        foodAllergensEvaluated.Add(unfilter.Key, unfilter.Value.Single());
                    }
                }
            }

            
        }
    }
}
