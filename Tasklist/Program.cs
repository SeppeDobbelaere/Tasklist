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
            Console.Write("Actions: [T]oevoegen / [V]oltooien / [D]efinitief verwijderen / [S]toppen: ");

            string choice = Console.ReadLine().ToLower();

            if (choice == "t")
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
            else if (choice == "v")
            {
                // ask which task to mark as done
                Console.Write("Enter the number of the task: ");
                string numInput = Console.ReadLine();

                if (int.TryParse(numInput, out int index))
                {
                    if (index >= 0 && index < taskCount && !isDone[index])
                    {
                        // mark as done with todays date
                        isDone[index] = true;
                        doneDates[index] = DateTime.Today.ToString("yyyy-MM-dd");

                        // save open tasks to file
                        using (StreamWriter sw = new StreamWriter(takenBestand, false))
                        {
                            for (int i = 0; i < taskCount; i++)
                            {
                                if (!isDone[i])
                                    sw.WriteLine($"{taskNames[i]};{deadlines[i] ?? ""}");
                            }
                        }

                        // save completed tasks to file
                        using (StreamWriter sw = new StreamWriter(voltooidBestand, false))
                        {
                            for (int i = 0; i < taskCount; i++)
                            {
                                if (isDone[i])
                                    sw.WriteLine($"{taskNames[i]};{deadlines[i] ?? ""};{doneDates[i]}");
                            }
                        }

                        Console.WriteLine("Task marked as done! Press Enter to continue.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid number. Press Enter to continue.");
                    }
                }
                else
                {
                    Console.WriteLine("That is not a number. Press Enter to continue.");
                }

                Console.ReadLine();
            }
            else if (choice == "d")
            {
                Console.WriteLine("todo");
                Console.ReadLine();
            }
            else if (choice == "s")
                break;
            else
            {
                Console.WriteLine("Wrong input! Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }
}