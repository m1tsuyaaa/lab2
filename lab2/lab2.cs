using System;
using System.Collections.Generic;

namespace AnimalKingdom {
  public abstract class Animal {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Habitat { get; set; }
    public string DietType { get; set; }
    public double Weight { get; set; }
    public string Color { get; set; }

    protected Animal(string name, int age, string habitat, string dietType, double weight = 0.0, string color = "Unknown") {
      Name = name;
      Age = age;
      Habitat = habitat;
      DietType = dietType;
      Weight = weight;
      Color = color;
    }

    public virtual string GetInfo() {
      return $"Name: {Name}, Age: {Age}, Habitat: {Habitat}, Diet: {DietType}, Weight: {Weight:F1} kg, Color: {Color}";
    }
  }

  public class Mammal : Animal {
  public bool HasFur { get; set; }

  public Mammal(string name, int age, string habitat, string dietType, bool hasFur, double weight = 0.0, string color = "Unknown")
    : base(name, age, habitat, dietType, weight, color) {
      HasFur = hasFur;
  }

  public override string GetInfo() {
    string furStatus = HasFur ? "yes" : "no";
    return base.GetInfo() + $", Type: Mammal, Fur: {furStatus}";
    }
  }

  public class Bird : Animal {
    public double WingSpan { get; set; }

  public Bird(string name, int age, string habitat, string dietType, double wingSpan, double weight = 0.0, string color = "Unknown")
    : base(name, age, habitat, dietType, weight, color) {
      WingSpan = wingSpan;
  }

  public override string GetInfo() {
    return base.GetInfo() + $", Type: Bird, Wingspan: {WingSpan:F1} m";
    }
  }

  public class Fish : Animal {
    public string WaterType { get; set; }

    public Fish(string name, int age, string habitat, string dietType, string waterType, double weight = 0.0, string color = "Unknown")
      : base(name, age, habitat, dietType, weight, color) {
        WaterType = waterType;
    }

    public override string GetInfo() {
      return base.GetInfo() + $", Type: Fish, Water: {WaterType}";
    }
  }

  public class Reptile : Animal {
    public bool IsVenomous { get; set; }

    public Reptile(string name, int age, string habitat, string dietType, bool isVenomous, double weight = 0.0, string color = "Unknown")
      : base(name, age, habitat, dietType, weight, color) {
        IsVenomous = isVenomous;
    }

    public override string GetInfo() {
      string venomStatus = IsVenomous ? "venomous" : "non-venomous";
      return base.GetInfo() + $", Type: Reptile, Venom: {venomStatus}";
    }
  }

  public class Amphibian : Animal {
    public string SkinMoisture { get; set; }

    public Amphibian(string name, int age, string habitat, string dietType, string skinMoisture, double weight = 0.0, string color = "Unknown")
      : base(name, age, habitat, dietType, weight, color) {
        SkinMoisture = skinMoisture;
    }

    public override string GetInfo() {
      return base.GetInfo() + $", Type: Amphibian, Skin: {SkinMoisture}";
    }
  }

  public sealed class AnimalManager {
    private static readonly Lazy<AnimalManager> _instance = new Lazy<AnimalManager>(() => new AnimalManager());
    private List<Animal> _animals;

    private AnimalManager() {
      _animals = new List<Animal>();
    }

    public static AnimalManager Instance {
      get { return _instance.Value; }
    }

    public void AddAnimal(Animal animal) {
      if (animal == null) {
        Console.WriteLine("Error: Animal cannot be null");
        return;
      }
      _animals.Add(animal);
      Console.WriteLine($"Animal '{animal.Name}' added successfully");
    }

    public void ShowAllAnimals() {
      if (_animals.Count == 0) {
        Console.WriteLine("No animals in the zoo");
        return;
      }
      Console.WriteLine("\n--- All Animals ---");
      for (int index = 0; index < _animals.Count; ++index) {
        Console.WriteLine($"{index + 1}. {_animals[index].GetInfo()}");
      }
    }

    public void ShowAnimalByIndex(int index) {
      if (index < 0 || index >= _animals.Count) {
        Console.WriteLine("Error: Invalid index");
        return;
      }
      Console.WriteLine("\n" + _animals[index].GetInfo());
    }

    public void ShowAnimalByName(string name) {
      if (string.IsNullOrWhiteSpace(name)) {
        Console.WriteLine("Error: Name cannot be empty");
        return;
      }
      bool found = false;
      for (int index = 0; index < _animals.Count; ++index) {
        if (_animals[index].Name.Equals(name, StringComparison.OrdinalIgnoreCase)) {
          Console.WriteLine("\n" + _animals[index].GetInfo());
          found = true;
        }
      }
      if (!found) {
        Console.WriteLine($"Animal '{name}' not found");
      }
    }

    public int GetAnimalCount() {
      return _animals.Count;
    }
  }

  class Program {
    static void Main(string[] args) {
      AnimalManager manager = AnimalManager.Instance;

      manager.AddAnimal(new Mammal("Simba", 5, "Savanna", "Predator", true, 180.5, "Golden"));
      manager.AddAnimal(new Bird("Zephyr", 3, "Mountains", "Predator", 2.3, 6.2, "Brown"));
      manager.AddAnimal(new Fish("Finn", 2, "Ocean", "Omnivore", "Salt", 3.5, "Silver"));
      manager.AddAnimal(new Reptile("Viper", 4, "Desert", "Predator", true, 2.8, "Green"));
      manager.AddAnimal(new Amphibian("Kermit", 2, "Pond", "Insectivore", "Moist", 0.5, "Green"));

      bool running = true;
      while (running) {
        Console.WriteLine("\n=== ZOO MANAGER ===");
        Console.WriteLine("1. Show all animals");
        Console.WriteLine("2. Find animal by index");
        Console.WriteLine("3. Find animal by name");
        Console.WriteLine("4. Add new animal");
        Console.WriteLine("5. Exit");
        Console.Write("Select option (1-5): ");

        string input = Console.ReadLine()?.Trim() ?? "";
          int choice;

          if (!int.TryParse(input, out choice) || choice < 1 || choice > 5) {
            Console.WriteLine("Invalid input. Please enter a number from 1 to 5");
            continue;
          }

          switch (choice) {
            case 1:
              manager.ShowAllAnimals();
              break;

            case 2:
              if (manager.GetAnimalCount() == 0) {
                Console.WriteLine("No animals available");
                break;
              }
              Console.Write($"Enter index (0-{manager.GetAnimalCount() - 1}): ");
              string indexInput = Console.ReadLine()?.Trim() ?? "";
              int index;
              if (int.TryParse(indexInput, out index)) {
                manager.ShowAnimalByIndex(index);
              }
              else {
                Console.WriteLine("Invalid index format");
              }
              break;

            case 3:
              if (manager.GetAnimalCount() == 0) {
                Console.WriteLine("No animals available");
                break;
              }
              Console.Write("Enter animal name: ");
              string name = Console.ReadLine()?.Trim() ?? "";
              manager.ShowAnimalByName(name);
              break;

            case 4:
              AddNewAnimal(manager);
              break;

            case 5:
              running = false;
              Console.WriteLine("Goodbye!");
              break;
            }
          }
      }

      static void AddNewAnimal(AnimalManager manager) {
        Console.WriteLine("\n--- Add New Animal ---");
        Console.WriteLine("Select animal type:");
        Console.WriteLine("1. Mammal");
        Console.WriteLine("2. Bird");
        Console.WriteLine("3. Fish");
        Console.WriteLine("4. Reptile");
        Console.WriteLine("5. Amphibian");
        Console.Write("Choice (1-5): ");

        string typeInput = Console.ReadLine()?.Trim() ?? "";
        int typeChoice;

        if (!int.TryParse(typeInput, out typeChoice) || typeChoice < 1 || typeChoice > 5) {
          Console.WriteLine("Invalid type selection");
          return;
        }

        Console.Write("Enter name: ");
        string name = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(name)) {
          Console.WriteLine("Name cannot be empty");
          return;
        }

        Console.Write("Enter age: ");
        string ageInput = Console.ReadLine()?.Trim() ?? "";
        int age;
        if (!int.TryParse(ageInput, out age) || age < 0) {
          Console.WriteLine("Invalid age");
          return;
        }

        Console.Write("Enter habitat: ");
        string habitat = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(habitat)) {
          Console.WriteLine("Habitat cannot be empty");
          return;
        }

        Console.Write("Enter diet type: ");
        string diet = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(diet)) {
          Console.WriteLine("Diet type cannot be empty");
          return;
        }

        Console.Write("Enter weight (kg): ");
        string weightInput = Console.ReadLine()?.Trim() ?? "";
        double weight;
        if (!double.TryParse(weightInput, out weight) || weight < 0) {
          Console.WriteLine("Invalid weight");
          return;
        }

        Console.Write("Enter color: ");
        string color = Console.ReadLine()?.Trim() ?? "Unknown";

        switch (typeChoice) {
          case 1:
            Console.Write("Has fur? (yes/no): ");
            string furInput = Console.ReadLine()?.Trim().ToLower() ?? "";
            bool hasFur = furInput == "yes" || furInput == "y";
            manager.AddAnimal(new Mammal(name, age, habitat, diet, hasFur, weight, color));
            break;

          case 2:
            Console.Write("Enter wingspan (m): ");
            string wingInput = Console.ReadLine()?.Trim() ?? "";
            double wingspan;
            if (!double.TryParse(wingInput, out wingspan) || wingspan < 0) {
              Console.WriteLine("Invalid wingspan");
              return;
            }
            manager.AddAnimal(new Bird(name, age, habitat, diet, wingspan, weight, color));
            break;

          case 3:
            Console.Write("Enter water type (fresh/salt): ");
            string waterType = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(waterType)) {
              Console.WriteLine("Water type cannot be empty");
              return;
            }
            manager.AddAnimal(new Fish(name, age, habitat, diet, waterType, weight, color));
            break;

          case 4:
            Console.Write("Is venomous? (yes/no): ");
            string venomInput = Console.ReadLine()?.Trim().ToLower() ?? "";
            bool isVenomous = venomInput == "yes" || venomInput == "y";
            manager.AddAnimal(new Reptile(name, age, habitat, diet, isVenomous, weight, color));
            break;

          case 5:
            Console.Write("Enter skin moisture: ");
            string moisture = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(moisture)) {
              Console.WriteLine("Skin moisture cannot be empty");
              return;
            }
            manager.AddAnimal(new Amphibian(name, age, habitat, diet, moisture, weight, color));
            break;
        }
      }
  }
}