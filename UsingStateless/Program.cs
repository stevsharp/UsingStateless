using UsingStateless;

try
{
    StateMachine stateMachine = new StateMachine();

    stateMachine.Process();


    var statelessStateMachine = new StatelessStateMachine();

    statelessStateMachine.Process();

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());   
}

Console.ReadLine();