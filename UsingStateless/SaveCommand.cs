
namespace UsingStateless
{
    public class SaveCommand : ICommandStrategy
    {
        public void Execute()
        {
            SaveFile();
        }

        private void SaveFile()
        {
            Console.WriteLine(nameof(SaveFile));
        }
    }
}

