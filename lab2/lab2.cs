using System;
using System.Collections.Generic;

namespace AnimalKingdom
{
  public abstract class Animal
  {
    public string name { get; set; }
    public int age { get; set; }
    public string habitat { get; set; }
    public string dietType { get; set; }
    public double weight { get; set; }
    public string color { get; set; }

    protected Animal(string animalName, int animalAge, string animalHabitat, string animalDietType, double animalWeight = 0.0, string animalColor = "Unknown")
    {
      name = animalName;
      age = animalAge;
      habitat = animalHabitat;
      dietType = animalDietType;
      weight = animalWeight;
      color = animalColor;
    }

    public virtual string GetInfo()
    {
      return $"Name: {name}, Age: {age}, Habitat: {habitat}, Diet: {dietType}, Weight: {weight:F1} kg, Color: {Color}";
    }
  }

  public class Mammal : Animal
  {
    public bool hasFur { get; set; }

    public Mammal(string name, int age, string habitat, string dietType, bool animalHasFur, double weight = 0.0, string color = "Unknown")
      : base(name, age, habitat, dietType, weight, color)
    {
      hasFur = animalHasFur;
    }

    string furStatus;

    public override string GetInfo()
    {
      furStatus = hasFur ? "yes" : "no";
      return base.GetInfo() + $", Type: Mammal, Fur: {furStatus}";
    }
  }

  public class Bird : Animal
  {
    public double wingSpan { get; set; }

    public Bird(string name, int age, string habitat, string dietType, double animalWingSpan, double weight = 0.0, string color = "Unknown")
      : base(name, age, habitat, dietType, weight, color)
    {
      wingSpan = animalWingSpan;
    }

    public override string GetInfo()
    {
      return base.GetInfo() + $", Type: Bird, Wingspan: {wingSpan:F1} m";
    }
  }

  public class Fish : Animal
  {
    public string waterType { get; set; }

    public Fish(string name, int age, string habitat, string dietType, string animalWaterType, double weight = 0.0, string color = "Unknown")
      : base(name, age, habitat, dietType, weight, color)
    {
      waterType = animalWaterType;
    }

    public override string GetInfo()
    {
      return base.GetInfo() + $", Type: Fish, Water: {waterType}";
    }
  }

  public class Reptile : Animal
  {
    public bool isVenomous { get; set; }

    public Reptile(string name, int age, string habitat, string dietType, bool animalIsVenomous, double weight = 0.0, string color = "Unknown")
      : base(name, age, habitat, dietType, weight, color)
    {
      isVenomous = animalIsVenomous;
    }

    string venomStatus;

    public override string GetInfo()
    {
      venomStatus = isVenomous ? "venomous" : "non-venomous";
      return base.GetInfo() + $", Type: Reptile, Venom: {venomStatus}";
    }
  }

  public class Amphibian : Animal
  {
    public string skinMoisture { get; set; }

    public Amphibian(string name, int age, string habitat, string dietType, string animalSkinMoisture, double weight = 0.0, string color = "Unknown")
      : base(name, age, habitat, dietType, weight, color)
    {
      skinMoisture = animalSkinMoisture;
    }

    public override string GetInfo()
    {
      return base.GetInfo() + $", Type: Amphibian, Skin: {skinMoisture}";
    }
  }

  public sealed class AnimalManager
  {
    private static readonly Lazy<AnimalManager> s_instance = new Lazy<AnimalManager>(() => new AnimalManager());
    private List<Animal> s_animals;

    private AnimalManager()
    {
      s_animals = new List<Animal>();
    }

    public static AnimalManager Instance
    {
      get { return s_instance.Value; }
    }

    public void AddAnimal(Animal animal)
    {
      if (animal == null)
      {
        Console.WriteLine("Error: Animal cannot be null");
        return;
      }
      s_animals.Add(animal);
      Console.WriteLine($"Animal '{animal.name}' added successfully");
    }

    public void ShowAllAnimals()
    {
      if (s_animals.Count == 0)
      {
        Console.WriteLine("No animals in the zoo");
        return;
      }
      Console.WriteLine("\n--- All Animals ---");
      for (int animalIndex = 0; animalIndex < s_animals.Count; ++animalIndex)
      {
        Console.WriteLine($"{animalIndex + 1}. {s_animals[animalIndex].GetInfo()}");
      }
    }

    bool isFound;

    public void ShowAnimalByIndex(int index)
    {
      if (index < 0 || index >= s_animals.Count)
      {
        Console.WriteLine("Error: Invalid index");
        return;
      }
      Console.WriteLine("\n" + s_animals[index].GetInfo());
    }

    public void ShowAnimalByName(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        Console.WriteLine("Error: Name cannot be empty");
        return;
      }
      isFound = false;
      for (int animalIndex = 0; animalIndex < s_animals.Count; ++animalIndex)
      {
        if (s_animals[animalIndex].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
          Console.WriteLine("\n" + s_animals[animalIndex].GetInfo());
          isFound = true;
        }
      }
      if (!isFound)
      {
        Console.WriteLine($"Animal '{name}' not found");
      }
    }

    public int GetAnimalCount()
    {
      return s_animals.Count;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      AnimalManager animalManager = AnimalManager.Instance;

      animalManager.AddAnimal(new Mammal("Simba", 5, "Savanna", "Predator", true, 180.5, "Golden"));
      animalManager.AddAnimal(new Bird("Zephyr", 3, "Mountains", "Predator", 2.3, 6.2, "Brown"));
      animalManager.AddAnimal(new Fish("Finn", 2, "Ocean", "Omnivore", "Salt", 3.5, "Silver"));
      animalManager.AddAnimal(new Reptile("Viper", 4, "Desert", "Predator", true, 2.8, "Green"));
      animalManager.AddAnimal(new Amphibian("Kermit", 2, "Pond", "Insectivore", "Moist", 0.5, "Green"));

      bool isRunning = true;
      while (isRunning)
      {
        Console.WriteLine("\n=== ZOO MANAGER ===");
        Console.WriteLine("1. Show all animals");
        Console.WriteLine("2. Find animal by index");
        Console.WriteLine("3. Find animal by name");
        Console.WriteLine("4. Add new animal");
        Console.WriteLine("5. Exit");
        Console.Write("Select option (1-5): ");

        string userInput;
        int menuChoice;

        userInput = Console.ReadLine()?.Trim() ?? "";
        if (!int.TryParse(userInput, out menuChoice) || menuChoice < 1 || menuChoice > 5)
        {
          Console.WriteLine("Invalid input. Please enter a number from 1 to 5");
          continue;
        }

        switch (menuChoice)
        {
          case 1:
            {
              animalManager.ShowAllAnimals();
              break;
            }

          case 2:
            {
              if (animalManager.GetAnimalCount() == 0)
              {
                Console.WriteLine("No animals available");
                break;
              }
              Console.Write($"Enter index (0-{animalManager.GetAnimalCount() - 1}): ");

              string indexInput;
              int animalIndex;

              indexInput = Console.ReadLine()?.Trim() ?? "";
              if (int.TryParse(indexInput, out animalIndex))
              {
                animalManager.ShowAnimalByIndex(animalIndex);
              }
              else
              {
                Console.WriteLine("Invalid index format");
              }
              break;
            }

          case 3:
            {
              if (animalManager.GetAnimalCount() == 0)
              {
                Console.WriteLine("No animals available");
                break;
              }
              Console.Write("Enter animal name: ");

              string animalName;

              animalName = Console.ReadLine()?.Trim() ?? "";
              animalManager.ShowAnimalByName(animalName);
              break;
            }

          case 4:
            {
              AddNewAnimal(animalManager);
              break;
            }

          case 5:
            {
              isRunning = false;
              Console.WriteLine("Goodbye!");
              break;
            }
        }
      }
    }

    static void AddNewAnimal(AnimalManager animalManager)
    {
      Console.WriteLine("\n--- Add New Animal ---");
      Console.WriteLine("Select animal type:");
      Console.WriteLine("1. Mammal");
      Console.WriteLine("2. Bird");
      Console.WriteLine("3. Fish");
      Console.WriteLine("4. Reptile");
      Console.WriteLine("5. Amphibian");
      Console.Write("Choice (1-5): ");

      string typeInput;
      int typeChoice;

      typeInput = Console.ReadLine()?.Trim() ?? "";
      if (!int.TryParse(typeInput, out typeChoice) || typeChoice < 1 || typeChoice > 5)
      {
        Console.WriteLine("Invalid type selection");
        return;
      }

      Console.Write("Enter name: ");
      string animalName;

      animalName = Console.ReadLine()?.Trim() ?? "";
      if (string.IsNullOrWhiteSpace(animalName))
      {
        Console.WriteLine("Name cannot be empty");
        return;
      }

      Console.Write("Enter age: ");

      string ageInput;
      int animalAge;

      ageInput = Console.ReadLine()?.Trim() ?? "";
      if (!int.TryParse(ageInput, out animalAge) || animalAge < 0)
      {
        Console.WriteLine("Invalid age");
        return;
      }

      Console.Write("Enter habitat: ");
      string animalHabitat;

      animalHabitat = Console.ReadLine()?.Trim() ?? "";
      if (string.IsNullOrWhiteSpace(animalHabitat))
      {
        Console.WriteLine("Habitat cannot be empty");
        return;
      }

      Console.Write("Enter diet type: ");
      string animalDiet;

      animalDiet = Console.ReadLine()?.Trim() ?? "";
      if (string.IsNullOrWhiteSpace(animalDiet))
      {
        Console.WriteLine("Diet type cannot be empty");
        return;
      }

      Console.Write("Enter weight (kg): ");

      string weightInput;
      double animalWeight;

      weightInput = Console.ReadLine()?.Trim() ?? "";
      if (!double.TryParse(weightInput, out animalWeight) || animalWeight < 0.0)
      {
        Console.WriteLine("Invalid weight");
        return;
      }

      Console.Write("Enter color: ");
      string animalColor;

      animalColor = Console.ReadLine()?.Trim() ?? "Unknown";

      switch (typeChoice)
      {
        case 1:
          {
            Console.Write("Has fur? (yes/no): ");

            string furInput;
            bool hasFur;

            furInput = Console.ReadLine()?.Trim().ToLower() ?? "";
            hasFur = furInput == "yes" || furInput == "y";
            animalManager.AddAnimal(new Mammal(animalName, animalAge, animalHabitat, animalDiet, hasFur, animalWeight, animalColor));
            break;
          }

        case 2:
          {
            Console.Write("Enter wingspan (m): ");

            string wingInput;
            double wingspan;

            wingInput = Console.ReadLine()?.Trim() ?? "";
            if (!double.TryParse(wingInput, out wingspan) || wingspan < 0.0)
            {
              Console.WriteLine("Invalid wingspan");
              return;
            }
            animalManager.AddAnimal(new Bird(animalName, animalAge, animalHabitat, animalDiet, wingspan, animalWeight, animalColor));
            break;
          }

        case 3:
          {
            Console.Write("Enter water type (fresh/salt): ");

            string waterType;

            waterType = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(waterType))
            {
              Console.WriteLine("Water type cannot be empty");
              return;
            }
            animalManager.AddAnimal(new Fish(animalName, animalAge, animalHabitat, animalDiet, waterType, animalWeight, animalColor));
            break;
          }

        case 4:
          {
            Console.Write("Is venomous? (yes/no): ");

            string venomInput;
            bool isVenomous;

            venomInput = Console.ReadLine()?.Trim().ToLower() ?? "";
            isVenomous = venomInput == "yes" || venomInput == "y";
            animalManager.AddAnimal(new Reptile(animalName, animalAge, animalHabitat, animalDiet, isVenomous, animalWeight, animalColor));
            break;
          }

        case 5:
          {
            Console.Write("Enter skin moisture: ");

            string skinMoisture;

            skinMoisture = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(skinMoisture))
            {
              Console.WriteLine("Skin moisture cannot be empty");
              return;
            }
            animalManager.AddAnimal(new Amphibian(animalName, animalAge, animalHabitat, animalDiet, skinMoisture, animalWeight, animalColor));
            break;
          }
      }
    }
  }
}