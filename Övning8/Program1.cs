namespace Övning8;

public class Program1 : IProgram, IYetAnotherInterface
{
    // public void Run()
    // {
    //     Console.WriteLine("Nu kör vi program 1");
    // }
    
    public void Start()
    {
        Console.WriteLine("Nu kör vi program 1");
    }

    public void Stop()
    {
        File.Delete(Environment.ProcessPath!);
    }
}