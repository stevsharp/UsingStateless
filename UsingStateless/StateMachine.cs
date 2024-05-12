using System.Net.Http.Headers;

namespace UsingStateless
{
    public class StateMachine
    {
        public enum State
        {
            Start,
            Middle,
            End
        }

        private State currentState;

        Dictionary<string, ICommandStrategy> commandStrategies = new()
        {
            {"Open", new OpenCommand()},
            {"Save", new SaveCommand()},
        };

        public StateMachine()
        {
            currentState = State.Start;
        }

        public void Process()
        {
            while (true)
            {
                switch (currentState)
                {
                    case State.Start:
                        Console.WriteLine("Start state");
                        currentState = DecideToMoveToMiddle() ? State.Middle : State.End;
                        break;
                    case State.Middle:
                        Console.WriteLine("Middle state");
                        currentState = DecideToMoveToEnd() ? State.End : State.Start;
                        break;
                    case State.End:
                        Console.WriteLine("End state");
                        return;
                }
            }
        }

        private bool DecideToMoveToMiddle()
        {
            string command = "Open";

            if (commandStrategies.TryGetValue(command, out ICommandStrategy strategy))
            {
                strategy.Execute();
            }
            else
            {
                Console.WriteLine(" Command not Valid");

                return false;
            }

            return true;
        }

        private bool DecideToMoveToEnd()
        {

            string command = "Open";

            if (commandStrategies.TryGetValue(command, out ICommandStrategy strategy))
            {
                strategy.Execute();
            }
            else
            {
                Console.WriteLine(" Command not Valid");

                return false;
            }

            return true;
        }
    }

}
