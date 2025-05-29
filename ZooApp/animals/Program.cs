using System.Runtime.Serialization;
using System.Xml.Linq;

namespace animals;

class Program
{
    public interface IAnimal
    {      
        public string Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int EnergyAnimal { get; set; }
        public void GetSound() { }

        public void EatFood() { }
                
    }
    class Animal : IAnimal 
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int EnergyAnimal { get; set; }

        public Animal(int id, string name, int energyAnimal)
        {
            
            Id = id;
            Name = name;
            EnergyAnimal = energyAnimal;
        }

        public void GetSound() { }
        public void EatFood() { }
        
    }
    class AnimalList
    {
        public List<IAnimal> Animals { get; set; } = new List<IAnimal>();

        public void AddAnimal(IAnimal animal) => Animals.Add(animal);

        public void GetAnimalList()
        {
            foreach (var animal in Animals)
            {
                Console.WriteLine($" - {animal.Type} {animal.Name}. ");
            }
        }
        
        public void FeedTheAnimal(int idAnimal)
        {
            foreach (var animal in Animals)
            {
                if (animal.Id == idAnimal)
                {
                    animal.GetSound();
                    animal.EatFood();                    
                    WriteFileFeeding();
                    return;
                }

            }
            Console.WriteLine("Животное с данным Id не было найдено.");
        }

        public void WriteFileFeeding()
        {           
            string path = @"C:\Users\sibir\Desktop\FeedingStatistics.txt";

            DateTime localTime = DateTime.Now;

            //using (FileStream file = new FileStream(path, FileMode.Append))

            using (StreamWriter stream = new StreamWriter(path, true))
                stream.WriteLine($"Животное покормлено. Время {localTime}");
        }

    }
    class Lion : Animal,IAnimal
    {
        public string Type { get; set; } = "Лев";

        public Lion(int id, string name, int energyAnimal) : base (id, name, energyAnimal) 
        {           
            Id = id;
            Name = name;
            EnergyAnimal = energyAnimal;
        }
        public void GetSound() => Console.WriteLine("Рррр!!!");       
        public void EatFood() // Функция кормления льва с отображением энергии, 
        {
            Console.WriteLine($"Кормление льва {Name} ...");
            Console.WriteLine($"Лев {Name} накормлен!");                        
        }            
    }
    class Monkey : Animal,IAnimal
    {
        public string Type { get; set; } = "Обезьяна";
        public Monkey(int id, string name, int energyAnimal) : base(id, name, energyAnimal)
        {           
            Id = id;
            Name = name;
            EnergyAnimal = energyAnimal;
        }
    }
    static void Main(string[] args)
    {
        AnimalList listAnimal = new AnimalList();
        int animalId = 0;
        int newAnimalEnergy = 50;

        string OptionUser = "";
        while (OptionUser != "e")
        {
            Console.WriteLine("1.Добавить льва.");
            Console.WriteLine("2.Покормить животное.");
            Console.WriteLine("3.Посмотреть список животных.");
            Console.WriteLine("e.Выход.");
            OptionUser = Console.ReadLine();

            if(OptionUser == "1")
            {
                Console.WriteLine("Введите имя животного : ");
                string animalName = Console.ReadLine();
                animalId++;

                IAnimal animal = new Lion(animalId, animalName, newAnimalEnergy);
                
                listAnimal.AddAnimal(animal);
                Console.WriteLine("Животное успешно добавлено");
            }
            else if (OptionUser == "2")
            {
                Console.WriteLine("Выберите номер животного которое хотите покормить: ");
                
                listAnimal.GetAnimalList();

                
                string OptionFeedAnimal = Console.ReadLine();
                if (int.TryParse(OptionFeedAnimal, out animalId))
                {
                    listAnimal.FeedTheAnimal(animalId);
                    
                }
                else Console.WriteLine("Введен неверный номер животного.");
                    

            }
            else if (OptionUser == "3")
            {
                listAnimal.GetAnimalList();
                Console.WriteLine("Нажмите любую клавишу чтобы вернуться в меню...");
                Console.ReadLine();
            }
            else if (OptionUser == "e")
            {
                Console.WriteLine("Спасибо за пользование приложением.");
            }
        }
        

    }
}
