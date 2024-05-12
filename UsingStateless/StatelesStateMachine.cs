using Stateless;

namespace UsingStateless;

public class StatelessStateMachine
{
    private enum State
    {
        Idle,
        Moving,
        Attacking,
        Fleeing,
        Dead
    }

    private enum Trigger
    {
        Move,
        StopMoving,
        Attack,
        StopAttacking,
        Flee,
        StopFleeing,
        Die
    }

    private readonly StateMachine<State, Trigger> stateMachine;

    public StatelessStateMachine()
    {
        stateMachine = new StateMachine<State, Trigger>(State.Idle);

        // Configure transitions
        stateMachine.Configure(State.Idle)
            .Permit(Trigger.Move, State.Moving) 
            .Permit(Trigger.Attack, State.Attacking)
            .Permit(Trigger.Flee, State.Fleeing)
            .Permit(Trigger.Die, State.Dead);

        stateMachine.Configure(State.Moving)
            .OnEntry(() => Console.WriteLine("Entering Moving state"))
            .OnExit(() => Console.WriteLine("Exiting Moving state"))
            .Permit(Trigger.StopMoving, State.Idle)
            .Permit(Trigger.Attack, State.Attacking)
            .Permit(Trigger.Flee, State.Fleeing)
            .Permit(Trigger.Die, State.Dead);

        stateMachine.Configure(State.Attacking)
            .Permit(Trigger.StopAttacking, State.Idle)
            .Permit(Trigger.Flee, State.Fleeing)
            .Permit(Trigger.Die, State.Dead);

        stateMachine.Configure(State.Fleeing)
            .Permit(Trigger.StopFleeing, State.Idle)
            .Permit(Trigger.Die, State.Dead);

        // Initial state
        stateMachine.Configure(State.Dead)
            .OnEntry(() => Console.WriteLine("Entering Dead state"))
            .Permit(Trigger.Die, State.Dead);
    }

    public void Process()
    {
        while (true)
        {
            // Perform actions based on current state
            switch (stateMachine.State)
            {
                case State.Idle:
                    // Decide whether to move, attack, flee, or die
                    if (ShouldMove())
                    {
                        Move();
                    }
                    else if (ShouldAttack())
                    {
                        Attack();
                    }
                    else if (ShouldFlee())
                    {
                        Flee();
                    }
                    else
                    {
                        Die();
                    }
                    break;
                case State.Moving:
                    // Decide whether to stop moving, attack, flee, or die
                    if (ShouldStopMoving())
                    {
                        StopMoving();
                    }
                    else if (ShouldAttack())
                    {
                        Attack();
                    }
                    else if (ShouldFlee())
                    {
                        Flee();
                    }
                    else
                    {
                        Die();
                    }
                    break;
                case State.Attacking:
                    // Decide whether to stop attacking, flee, or die
                    if (ShouldStopAttacking())
                    {
                        StopAttacking();
                    }
                    else if (ShouldFlee())
                    {
                        Flee();
                    }
                    else
                    {
                        Die();
                    }
                    break;
                case State.Fleeing:
                    // Decide whether to stop fleeing or die
                    if (ShouldStopFleeing())
                    {
                        StopFleeing();
                    }
                    else
                    {
                        Die();
                    }
                    break;
                case State.Dead:
                    return;
            }
        }
    }

    private bool ShouldAttack()
    {
        throw new NotImplementedException();
    }

    private bool ShouldStopMoving()
    {
        throw new NotImplementedException();
    }

    private bool ShouldFlee()
    {
        throw new NotImplementedException();
    }

    private bool ShouldStopAttacking()
    {
        throw new NotImplementedException();
    }

    private bool ShouldStopFleeing()
    {
        throw new NotImplementedException();
    }

    private bool ShouldMove()
    {
        throw new NotImplementedException();
    }

    // Example methods for triggering transitions
    public void Move() => stateMachine.Fire(Trigger.Move);
    public void StopMoving() => stateMachine.Fire(Trigger.StopMoving);
    public void Attack() => stateMachine.Fire(Trigger.Attack);
    public void StopAttacking() => stateMachine.Fire(Trigger.StopAttacking);
    public void Flee() => stateMachine.Fire(Trigger.Flee);
    public void StopFleeing() => stateMachine.Fire(Trigger.StopFleeing);
    public void Die() => stateMachine.Fire(Trigger.Die);
}

