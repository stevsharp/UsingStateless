
namespace UsingStateless
{
    public class OpenCommand : ICommandStrategy
    {
        public void Execute()
        {
            OpenFile();
        }

        private void OpenFile()
        {
            Console.WriteLine(nameof(OpenFile));
        }
    }
}

