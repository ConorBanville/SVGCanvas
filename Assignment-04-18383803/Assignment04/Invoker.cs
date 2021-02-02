using System.Collections.Generic;

namespace Assignment04
{
    class Invoker
    {
        //history of commands

        public List<ICommand> History;

        //Constructor, initialises a list to store Commands
        public Invoker()
        {
            History = new List<ICommand>();
        }

        //Add a new command to the list
        public void AddCommand(ICommand newCommand)
        {
            //Account for the case where we hit undo a couple times and add a new command before hitting redo the same amount of times,
            //Need to delete the unreachable history
            if (Program.index_of_current_invoke!=History.Count)
            {
                //Clear all history after this index
                for(int i=Program.index_of_current_invoke; i<History.Count; i++)
                {
                    History.RemoveAt(i);
                }
            }
            History.Add(newCommand);
            Execute(History.Count-1);   //When adding a command we automatically execute it. We could get fancier here and put the commands in a queue, maybe to later execute them
                                        //or maybe if we were really scaling this up we could execute them in batches so handle resources more efficiently.
        }

        //Execute the Command
        public void Execute(int id)
        {
            History[id].execute();
        }
        //Unexecute the Command
        public void Unexecute(int id)
        {
            History[id-1].unexecute();
        }
    }
}
