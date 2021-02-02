using System;
namespace Assignment04
{
    /*  This is the command used to update the Z-Index of a shape
     *  
     *  -   When an instance is create the id of the shape and the new Index is passed
     *  -   Then the invoker will execute this command which in turn will execute the 
     *      method on the canvas that is responsible for moving this shape. This method
     *      will return the old index so that command may be undone.
     * 
     */
    class Command_ZEdit : ICommand
    {
        int uid_of_shape, new_index, old_index;

        public Command_ZEdit(int uid_of_shape, int new_index)
        {
            this.uid_of_shape = uid_of_shape;
            this.new_index = new_index;
        }

        void ICommand.execute()
        {
            //Move shape method returns the index the shape was found at, and -1 if not found
            old_index = Program.canvas.MoveShape(uid_of_shape, new_index);
            if (old_index == -1) Console.WriteLine("Error: Could not find a shape with that Unique ID.");
        }

        void ICommand.unexecute()
        {
            //If the shape could not be found then old_index will be -1
            //So we return in order to do nothing.
            //Unfortunately if a user does redo this command, that action is valid but 
            //does nothing, it will appear to the user as if the system ignored the command.
            //In order to solve this I would need a way of deleting this command in the 
            //invoker. But havent thought of a way yet.
            if (old_index == -1) return;
            Program.canvas.MoveShape(uid_of_shape, old_index);
        }
    }
}
