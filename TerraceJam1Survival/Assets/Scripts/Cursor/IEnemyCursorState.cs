using System;

public interface IEnemyCursorState
{
    Action OnComplete { get; set; }

    float GetSpawnRate();
    void Enter();
    void Act();
}
