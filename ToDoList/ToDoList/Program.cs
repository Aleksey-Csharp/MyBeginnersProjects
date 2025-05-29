using System.ComponentModel;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace ToDoList
{
    internal class Program
    {
        class Task
        {
            public int Id { get; set; }
            public string Description { get; set; }

            public bool IsCompleted { get; set; } = false;

            public Task(int id, string desriptionTask)
            {
                Id = id;
                Description = desriptionTask;
            }
            public void Complete()
            {
                IsCompleted = true;
            }
            public void Undo()
            {
                IsCompleted = false;
            }
        }
        class TaskList
        {
            public List<Task> Tasks { get; set; } = new List<Task>();
                     
            public void AddTask(Task task)
            {
                Tasks.Add(task);   // 0 - Task abc(id,Description,IsCompleted)
            }
            public void RemoveTask(int taskId) //номер задачи пользователя от него
            {
                for (int i = 0; i < Tasks.Count; i++)
                {
                    if(Tasks[i].Id == taskId)
                    {
                        Tasks.RemoveAt(i);
                        Console.WriteLine("Задача успешно удалена.");
                        return;
                    }
                }
                Console.WriteLine("Задача с таким ID не найдена.");
            }
            public void GetAllTasks()
            {
                foreach (Task task in Tasks)
                {
                    if (task.IsCompleted == true) Console.WriteLine($"{task.Id}. {task.Description} [x]");
                    else Console.WriteLine($"{task.Id}. {task.Description} [ ]");

                }
                return;
            }
            public Task GetTaskById(int taskid)
            {           
                for (int i = 0; i < Tasks.Count; i++)
                {
                    if (taskid == Tasks[i].Id) 
                    {                       
                        return Tasks[i];                      
                    }
                }
                Console.WriteLine("Данная задача не найдена!");
                return null;
            }              
            public List<Task> GetCompleteTasks()
            {
                List<Task> taskIsCompleted = new List<Task>();

                for (int i = 0; i < Tasks.Count; i++)
                {
                    if (Tasks[i].IsCompleted == true) taskIsCompleted.Add(Tasks[i]);
                }
                return taskIsCompleted;
            }
            public List<Task> GetInCompleteTasks()
            {
                List<Task> taskInCompleted = new List<Task>();

                for (int i = 0; i < Tasks.Count; i++)
                {
                    if (Tasks[i].IsCompleted == false) taskInCompleted.Add(Tasks[i]);
                }
                return taskInCompleted;
            }
        }
        static void Main(string[] args)
        {
            string Option = "";
            TaskList taskList = new();
            while (Option != "e")
            {
                Console.WriteLine("\tTo Do List:");

                Console.WriteLine("Опции:");

                Console.WriteLine("1.Добавить задачу.");
                Console.WriteLine("2.Удалить задачу.");
                Console.WriteLine("3.Выполненные задачи.");
                Console.WriteLine("4.Невыполненные задачи.");
                Console.WriteLine("5.Отменить задачу.");
                Console.WriteLine("6.Завершить задачу.");
                Console.WriteLine("7.Найти задачу.");
                Console.WriteLine("8.Показать весь список задач.");
                Console.WriteLine("e.Выйти.");

                Console.WriteLine("Введите вариант: ");
                Option = Console.ReadLine();

                if (Option == "1")
                {
                    Console.WriteLine("Введите Id задачи: ");
                    int idTask = 0;
                    try
                    {
                        idTask = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("По всей видимости вы ввели не число.");
                        continue;
                    }

                    Console.WriteLine("Введите задачу: ");
                    string Descriptiontask = Console.ReadLine();

                    Task newtask = new Task(idTask, Descriptiontask);
                   
                    taskList.AddTask(newtask);
                    //taskList.Tasks.Sort();
                    Console.WriteLine("Задача успешно добавлена.");
                }
                else if (Option == "2")
                {
                    Console.WriteLine("Введите Id задачи: ");
                    string IdTask = Console.ReadLine();

                    if (int.TryParse(IdTask, out int idUsertask))
                    {
                        taskList.RemoveTask(idUsertask);                        
                    }
                    else Console.WriteLine("Введен неверный ID задачи: ");

                }
                else if (Option == "3")
                {
                    List<Task> CompleteTasksList = taskList.GetCompleteTasks();

                    if (CompleteTasksList.Count == 0)
                    {
                        Console.WriteLine("Нет выполненных задач.");
                    }
                    else
                    {
                        foreach (Task task in CompleteTasksList)
                        {
                            Console.WriteLine($"{task.Id}. {task.Description} [x]");
                        }
                    }
                    Console.WriteLine("Нажми любую клавишу чтобы вернуться в меню.");
                    Console.ReadKey();
                }
                else if ( Option == "4")
                {
                    List<Task> InCompleteTasksList = taskList.GetInCompleteTasks();

                    if (InCompleteTasksList.Count == 0)
                    {
                        Console.WriteLine("Нет невыполненных задач.");
                    }
                    else
                    {
                        foreach (Task task in InCompleteTasksList)
                        {
                            Console.WriteLine($"{task.Id}. {task.Description} [ ]");
                        }
                    }
                    Console.WriteLine("Нажми любую клавишу чтобы вернуться в меню.");
                    Console.ReadKey();
                    
                }
                else if ( Option == "5")
                {
                    Console.WriteLine("Введите Id выполненной задачи: ");
                    string IdTask = Console.ReadLine();
                    List<Task> CompleteTasksList = taskList.GetCompleteTasks();

                    if (int.TryParse(IdTask, out int idUsertask))
                    {
                        bool taskFound = false;
                        foreach (Task task in CompleteTasksList)
                        {
                            if (task.Id == idUsertask)
                            {
                                task.Undo();
                                Console.WriteLine("Задача успешно отмечана как невыполненная.");
                                taskFound = true;
                                break;
                            }                                                                                    
                        }
                        if (taskFound == false) Console.WriteLine("Задача по заданному Id не найдена.");
                    }
                    else Console.WriteLine("Введен неверный ID задачи: ");

                }
                else if (Option == "6")
                {
                    Console.WriteLine("Введите Id выполненной задачи: ");
                    string IdTask = Console.ReadLine();

                    List<Task> InCompleteTasksList = taskList.GetInCompleteTasks();

                    if (int.TryParse(IdTask, out int idUsertask))
                    {
                        bool taskFound = false;
                        foreach (Task task in InCompleteTasksList)
                        {
                            if (task.Id == idUsertask)
                            {
                                task.Complete();
                                Console.WriteLine("Задача успешно отмечана как выполненная.");
                                taskFound = true;
                                break;
                            }
                        }
                        if (taskFound == false) Console.WriteLine("Задача по заданному Id не найдена.");
                    }
                    else Console.WriteLine("Введен неверный ID задачи: ");
                }
                else if (Option == "7")
                {
                    Console.WriteLine("Введите Id задачи, которую хотите найти: ");
                    string IdTask = Console.ReadLine();

                    if (int.TryParse(IdTask, out int idUsertask))
                    {
                        
                        Task Searchtask = taskList.GetTaskById(idUsertask);

                        if (Searchtask != null)
                        {                            
                            if (Searchtask.IsCompleted == true)
                            {
                                Console.WriteLine($"{Searchtask.Id}. {Searchtask.Description} [x]");
                            }
                            else Console.WriteLine($"{Searchtask.Id}. {Searchtask.Description} [ ]");
                        }
                    }
                    else Console.WriteLine("Введен неверный ID задачи: ");

                }
                else if (Option == "8")
                {
                    if (taskList.Tasks.Count == 0)
                    {
                        Console.WriteLine("У вас нету никакой задачи.");
                    }
                    else taskList.GetAllTasks();

                    Console.WriteLine("Нажмите любую кнопку для возврата в меню.");
                    Console.ReadKey();
                }
                else if (Option == "e")
                {
                    Console.WriteLine("Спасибо за пользование приложением.");
                }
            }

            

        }
    }
}
