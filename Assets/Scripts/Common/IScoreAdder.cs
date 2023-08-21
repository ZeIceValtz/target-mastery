using System;

public interface IScoreAdder
{
    event Action<int> OnAddScore;
}
