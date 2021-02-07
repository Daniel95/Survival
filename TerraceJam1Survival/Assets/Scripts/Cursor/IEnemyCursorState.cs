using System;

public interface IEnemyCursorState
{
    Action OnComplete { get; set; }

    void Enter();
    void Act();
}
