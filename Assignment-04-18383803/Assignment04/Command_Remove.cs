using System;

namespace Assignment04
{
    /*  This is the command used to delete a shape
     *  
     *  -   When an instance is create the id of the shape is passed
     *  -   Then the invoker will execute this command which in turn will execute the 
     *      method on the canvas that is responsible for removing the shape. This method
     *      will return a CustomObject containing the removed shape and its Z-Index
     *      so that this command may be undone and the exact same shape reinserted in
     *      the correct position.
     * 
     */
    class Command_Remove : ICommand
    {
        Canvas.CustomObject removed_shape;
        int uid;

        public Command_Remove(int uid)
        {
            this.uid = uid;
        }

        void ICommand.execute()
        {
            //Store the shape and it's index so it can be re-instantiated
            removed_shape = Canvas.ReturnShape(uid);
            //If no shape was found corresponding to the uid given the return 
            if(removed_shape.GetIndex() == -1)
            {
                return;
            }
            //Remove the shape
            Canvas.RemoveShape(uid);
            Console.WriteLine($"Shape removed with id: [{uid}]");
        }

        void ICommand.unexecute()
        {
            Program.canvas.InsertShape(removed_shape.GetShape(), removed_shape.GetIndex());
        }
    }
}
