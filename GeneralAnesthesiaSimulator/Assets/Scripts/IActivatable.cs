using System;

public interface IActivatable
{
    bool GetActivationStatus();
    void Activate();
    void DeActivate();
}
