using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Entities;
using Stump.Server.WorldServer.Global.Maps;

namespace Stump.Server.WorldServer.Actions
{
    public abstract class ActionBase
    {
        public static void ExecuteAction(ActionBase action, InteractiveObject interactiveObject, Character executer)
        {
            if (action is CharacterAction)
                ( action as CharacterAction ).Execute(executer);
            else if (action is InteractiveAction)
                (action as InteractiveAction).Execute(interactiveObject, executer);
        }

        public static void ExecuteAction(ActionBase action, NpcSpawn npc, Character executer)
        {
            if (action is CharacterAction)
                ( action as CharacterAction ).Execute(executer);
            else if (action is NpcAction)
                (action as NpcAction).Execute(npc, executer);
        }

        public static void ExecuteAction(ActionBase action, CellData cell, Character executer)
        {
            if (action is CharacterAction)
                ( action as CharacterAction ).Execute(executer);
            else if (action is CellAction)
                ( action as CellAction ).Execute(cell, executer);
        }
    }
}