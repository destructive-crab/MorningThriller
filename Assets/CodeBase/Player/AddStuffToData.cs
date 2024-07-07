using MothDIed;
using MothDIed.ExtensionSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MorningThriller.PlayerLogic
{
    [DisallowMultipleExtensions]
    public sealed class AddStuffToData : Extension
    {
        public override void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Game.RunData.PlayerData.stuff.Add(Random.Range(0, 999));
            }
        }
    }
}