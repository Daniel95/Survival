using System;

public interface IEnemyCursorState
{
    Action onComplete { get; set; }

    float GetSpawnRate();
    void Enter();
    void Act();
}
