using System;
namespace Assignment04
{
    /*  This is the command used to update the dimenstion attributes of a shape
     *  
     *  -   When an instance is create the id of the shape and an array containing
     *      the new data is passed
     *  -   Then the invoker will execute this command which in turn will execute the 
     *      method on the canvas that is responsible for update these values. This method
     *      will return an array containing the old valuse so that this command may be undone.
     * 
     */
    class Command_Edit : ICommand
    {
        string id_of_shape;
        string[] data;
        string[] old_data;

        public Command_Edit(string id_of_shape, string[] data)
        {
            this.id_of_shape = id_of_shape;
            this.data = data;
        }

        void ICommand.execute()
        {
            try
            {
                old_data = Program.canvas.Update(Convert.ToInt32(id_of_shape), data);
            }
            catch
            {
                old_data = null;
            }
        }

        void ICommand.unexecute()
        {
            if (old_data == null)
            {
                return;
            }
            else
            {
                try
                {
                    Program.canvas.Update(Convert.ToInt32(id_of_shape), old_data);
                }
                catch
                {
                    return;
                }
                
            }

        }
    }
}
