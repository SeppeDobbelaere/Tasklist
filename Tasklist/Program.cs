internal class Program
{
    static void Main(string[] args)
    {
        const string takenBestand = "taken.txt";
        const string voltooidBestand = "voltooid.txt";

        string[] taskNames = new string[100];
        string[] deadlines = new string[100];
        string[] doneDates = new string[100];
        bool[] isDone = new bool[100];
        int taskCount = 0;
        int doneCount = 0;

        // load open tasks from file
        if (File.Exists(takenBestand))
        {
            string[] lines = File.ReadAllLines(takenBestand);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                taskNames[taskCount] = parts[0];
                deadlines[taskCount] = parts[1] == "" ? null : parts[1];
                taskCount++;
            }
        }

        // load completed tasks from file
        if (File.Exists(voltooidBestand))
        {
            string[] lines = File.ReadAllLines(voltooidBestand);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                taskNames[taskCount] = parts[0];
                deadlines[taskCount] = parts[1] == "" ? null : parts[1];
                doneDates[taskCount] = parts[2];
                isDone[taskCount] = true;
                taskCount++;
            }
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== TASK MANAGER ===");
            Console.WriteLine();

            // show open tasks
            Console.WriteLine("Open tasks:");
            for (int i = 0; i < taskCount; i++)
            {
                if (!isDone[i])
                {
                    if (deadlines[i] == null)
                        Console.WriteLine($"  [{i}] {taskNames[i]}");
                    else
                        Console.WriteLine($"  [{i}] {taskNames[i]} | deadline: {deadlines[i]}");
                }
            }

            Console.WriteLine();

            // show completed tasks
            Console.WriteLine("Completed tasks:");
            for (int i = 0; i < taskCount; i++)
            {
                if (isDone[i])
                    Console.WriteLine($"  {taskNames[i]} | done on: {doneDates[i]}");
            }

            Console.WriteLine();
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. Mark task as done");
            Console.WriteLine("3. Delete task");
            Console.WriteLine("4. Exit");
            Console.Write("\nChoice: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // ask for task name
                string name = "";
                while (true)
                {
                    Console.Write("Task description (max 30 chars): ");
                    name = Console.ReadLine();
                    if (name.Length == 0 || name.Length > 30)
                        Console.WriteLine("Invalid! Must be between 1 and 30 characters.");
                    else
                        break;
                }

                // ask for deadline
                Console.Write("Deadline (dd/mm/yyyy, leave empty = no deadline): ");
                string dlInput = Console.ReadLine().Trim();
                string dl = null;
                if (dlInput != "")
                    dl = dlInput;

                // save the task in the arrays
                taskNames[taskCount] = name;
                deadlines[taskCount] = dl;
                isDone[taskCount] = false;
                taskCount++;

                // save to file
                using (StreamWriter sw = new StreamWriter(takenBestand, false))
                {
                    for (int i = 0; i < taskCount; i++)
                    {
                        if (!isDone[i])
                            sw.WriteLine($"{taskNames[i]};{deadlines[i] ?? ""}");
                    }
                }

                Console.WriteLine("Task added! Press Enter to continue.");
                Console.ReadLine();
            }
            else if (choice == "2")
                Console.WriteLine("todo");
            else if (choice == "3")
                Console.WriteLine("todo");
            else if (choice == "4")
                break;
            else
                Console.WriteLine("Wrong input!");

            Console.ReadLine();
        }
    }
}