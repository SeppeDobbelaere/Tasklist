class Program
{
    static void Main()
    {
        string[] taskNames = new string[100];
        string[] deadlines = new string[100];
        bool[] isDone = new bool[100];
        string[] doneDates = new string[100];
        int taskCount = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== TASK MANAGER ===");
            Console.WriteLine();
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. Mark task as done");
            Console.WriteLine("3. Delete task");
            Console.WriteLine("4. Exit");
            Console.Write("\nChoice: ");

            string input = Console.ReadLine();

            if (input == "1")
                Console.WriteLine("todo: add task");
            else if (input == "2")
                Console.WriteLine("todo: mark done");
            else if (input == "3")
                Console.WriteLine("todo: delete task");
            else if (input == "4")
                return;
            else
                Console.WriteLine("Wrong input, try again.");

            Console.ReadLine();
        }

    }
}