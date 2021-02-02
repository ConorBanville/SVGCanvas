
namespace Assignment04
{
    /*  This is the command used to update the default style attributes of a shape
     *  
     *  -   When an instance is create the name of the shape and an array containing
     *      the new data is passed
     *  -   Then the invoker will execute this command which in turn will execute the 
     *      method on the canvas that is responsible for setting defaults. This method
     *      will return an array containing the old values so that this command may be 
     *      undone.
     *  -   A switch is used to handle each of the diffent shaped we may be dealing with.
     * 
     */
    class Command_Defaults : ICommand
    {
        string shape;
        string[] data;
        string[] old_data;

        public Command_Defaults(string shape, string[] data)
        {
            this.shape = shape;
            this.data = data;
        }

        void ICommand.execute()
        {
            switch (shape)
            {
                case "rect":
                    old_data = Rectangle.SetDefaults(data);
                    return;
                case "circle":
                    old_data = Circle.SetDefaults(data);
                    return;
                case "ellipse":
                    old_data = Ellispse.SetDefaults(data);
                    return;
                case "line":
                    old_data = Line.SetDefaults(data);
                    return;
                case "polyline":
                    old_data = Polyline.SetDefaults(data);
                    return;
                case "polygon":
                    old_data = Polygon.SetDefaults(data);
                    return;
                case "path":
                    old_data = Path.SetDefaults(data);
                    return;
                default:
                    return;
            }
        }

        void ICommand.unexecute()
        {
            if(old_data == null)
            {
                return;
            }
            switch (shape)
            {
                case "rect":
                    Rectangle.SetDefaults(old_data);
                    return;
                case "circle":
                    Circle.SetDefaults(old_data);
                    return;
                case "ellipse":
                    Ellispse.SetDefaults(old_data);
                    return;
                case "line":
                    Line.SetDefaults(old_data);
                    return;
                case "polyline":
                    Polyline.SetDefaults(old_data);
                    return;
                case "polygon":
                    Polygon.SetDefaults(old_data);
                    return;
                case "path":
                    Path.SetDefaults(old_data);
                    return;
                default:
                    return;
            }
        }
    }
}
