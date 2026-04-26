
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
    }
}