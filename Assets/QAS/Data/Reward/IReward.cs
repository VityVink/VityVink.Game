using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IReward
{
    void Apply();   // выдать награду игроку
    string GetDescription(); // для UI (например, "+100 опыта")
}





