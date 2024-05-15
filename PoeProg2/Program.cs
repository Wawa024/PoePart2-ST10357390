namespace PoeProg2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;




    // Delegate to notify when a recipe exceeds 300 calories
    public delegate void RecipeCal(double x);




    // Ingredient Class
    public class Ingredient

    {

        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }
        

    }


    // Step class

    public class Step

    {

        public string Description { get; set; }

    }


    // Recipe Class
    public class Recipe

    {


        public double calc(double x, double y)
        {

            x += y;
            return x;

        }


        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Step> Steps { get; set; }
        public double TotalCalories { get; private set; }


        public Recipe()

        {

            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();

        }


        // Calculates total calories and notifys if it exceeds 300
        


        // Scaling recipe by a factor

        public void ScaleRecipe(double factor)

        {

            foreach (var ingredient in Ingredients)

            {

                ingredient.Quantity *= factor;
                ingredient.Calories *= factor;

            }

        }

    }


    // Main class

    public class Menu
    {

        static void CalculateTotalCalories(double x)

        {

           // TotalCalories = Ingredients.Sum(i => i.Calories * i.Quantity);
            if (x > 300)

            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Warning: Recipe has over 300 calories!");
                Console.ResetColor();

            }

        }

        // Collection to store recipes

        private static List<Recipe> recipes = new List<Recipe>();

        //Predefined food groups
        private static readonly string[] foodGroups = { "Grains", "Vegetables", "Fruits", "Proteins", "Dairy", "Fats and Oils", "Sweets", "Nuts", "Liquids" };




        // add a new recipe

        public static void AddRecipe()

        {

            Recipe recipe = new Recipe();


            Console.Write("Enter recipe name: ");
            recipe.Name = Console.ReadLine();


            // Input ingredients

            Console.Write("Enter number of ingredients: ");
            int ingredientCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < ingredientCount; i++)

            {

                Ingredient ingredient = new Ingredient();
                Console.Write($"Ingredient {i + 1} name: ");
                ingredient.Name = Console.ReadLine();
                Console.Write("Quantity: ");
                ingredient.Quantity = double.Parse(Console.ReadLine());
                Console.Write("Unit: ");
                ingredient.Unit = Console.ReadLine();
                Console.Write("Calories: ");
                ingredient.Calories = double.Parse(Console.ReadLine());
                

                // Select food group
                Console.WriteLine("Select Food Group: ");
                for (int j = 0; j<foodGroups.Length; j++)
                {
                    Console.WriteLine($"{j + 1}.{foodGroups[j]}");
                }
                int groupIndex = int.Parse(Console.ReadLine());
                ingredient.FoodGroup = foodGroups[groupIndex - 1];
                recipe.Ingredients.Add( ingredient );
            }




            // Input steps

            Console.Write("Enter number of steps: ");
            int stepCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < stepCount; i++)

            {

                Step step = new Step();
                Console.Write($"Step {i + 1}: ");
                step.Description = Console.ReadLine();
                recipe.Steps.Add(step);

            }


            recipes.Add(recipe);




        }


        //display all recipes

        private static void DisplayRecipes()

        {

            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes.OrderBy(r => r.Name))

            {

                Console.WriteLine(recipe.Name);

            }

        }


        //display recipe details

        public static void DisplayRecipeDetails(string recipeName)

        {
            RecipeCal rep = new RecipeCal(CalculateTotalCalories); 

            Recipe recipe = recipes.FirstOrDefault(r => r.Name == recipeName);

            if (recipe != null)

            {
                double calTtl = 0;
                Console.WriteLine($"Name: {recipe.Name}");
                Console.WriteLine("Ingredients:");
                foreach (var ingredient in recipe.Ingredients)

                {
                    calTtl += ingredient.Calories;
                    Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories) - {ingredient.FoodGroup}"); 
                }

               
                Console.WriteLine("Steps:");
                foreach (var step in recipe.Steps)

                {

                    Console.WriteLine(step.Description);

                }

                //Console.WriteLine($"Total Calories: {recipe.TotalCalories}");
                Console.WriteLine($"Your total calories is:  + {calTtl}");

                if(calTtl < 150)
                {
                    Console.WriteLine("This recipe is low in calories.");
                }
                else if (calTtl >= 150 && calTtl <= 300)
                {
                    Console.WriteLine("This recipe is moderate calories.");
                }
                else
                {
                    Console.WriteLine("This recipe is high in calories.");
                    rep(calTtl);
                }


            }

            else

            {

                Console.WriteLine("Recipe not found.");

            }

        }

        //Edit a recipe
        private static void EditRecipe(string recipeName)
        {
            Recipe recipe = recipes.FirstOrDefault(r => r.Name == recipeName);
            if(recipe != null)
            {
                //Display existing details
                Console.WriteLine($"Editing Recipe: {recipe.Name}");
                Console.WriteLine("Current Ingredients: ");
                foreach (var ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} - {ingredient.Calories} calories");
                    {
                        Console.WriteLine("Current Steps: ");
                        foreach (var step in recipe.Steps)
                        {
                            Console.WriteLine(step.Description);
                        }

                        //Modify ingredients and steps
                        Console.WriteLine("Enter new ingredients and steps: ");
                        AddRecipe(); //reuse the add recipe method to update the recipe
                    }
                    
                 
                }
            }
        }


        // Method to scale a recipe
        private static void ScaleRecipe(string recipeName, double factor)

        {

            Recipe recipe = recipes.FirstOrDefault(r => r.Name == recipeName);
            if (recipe != null)

            {

                recipe.ScaleRecipe(factor);
                Console.WriteLine($"Recipe {recipeName} scaled by factor {factor}.");


            }

            else

            {

                Console.WriteLine("Recipe not found.");

            }

        }


        // Main method
        public static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)

            {

                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. Display Recipes");
                Console.WriteLine("3. Display Recipe Details");
                Console.WriteLine("4. Edit Recipe");
                Console.WriteLine("5. Scale Recipe");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());


                switch (choice)

                {

                    case 1:

                        AddRecipe();

                        break;

                    case 2:

                        DisplayRecipes();

                        break;

                    case 3:

                        Console.Write("Enter recipe name: ");
                        string recipeName = Console.ReadLine();
                        DisplayRecipeDetails(recipeName);

                        break;

                    case 4:

                        Console.Write("Enter recipe name to edit: ");
                        string recipeNameToEdit = Console.ReadLine();
                        EditRecipe(recipeNameToEdit);
                        break;


                    case 5:
                        Console.Write("Enter recipe name to scale: ");
                        string recipeNameToScale = Console.ReadLine();
                        Console.Write("Enter scaling factor: ");
                        double factor = double.Parse(Console.ReadLine());
                        ScaleRecipe(recipeNameToScale, factor);
                        break;
                        

                    case 6:

                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;

                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                }

            }

        }

    }

} 








