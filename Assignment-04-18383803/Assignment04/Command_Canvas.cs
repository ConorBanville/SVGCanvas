
namespace Assignment04
{
    /*  This is the class that will encapsulate the Command used to change the dimensions of the Canvas
     *  
     *  -   When an instance is created the new width and height of the canvas are passed.
     *  -   The invoker immediately calls the execute method update the data on the canvas.
     *  -   The method used to update the canvas will return and array containing the old 
     *      data of the canvas so that in the future this command can be undone.
     *  -   Unexecute simple calls the same method on the canvas except it will not store
     *      array returned.
     */
    class Command_Canvas : ICommand
    {
        double new_width, new_height;
        double[] old_data;

        public Command_Canvas(double new_width,double new_height)
        {
            this.new_width = new_width;
            this.new_height = new_height;
            old_data = new double[2];
        }

        void ICommand.execute()
        {           
            old_data = Program.canvas.SetCanvas(new_width, new_height);
        }

        void ICommand.unexecute()
        {
            Program.canvas.SetCanvas(old_data[0], old_data[1]);
        }
    }
}
