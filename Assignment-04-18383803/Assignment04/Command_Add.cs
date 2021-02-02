using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment04
{
    /*  This is the class that will encapsulate the command used to to add a shape.
     *  
     *  -   When an instance is created two bits of information are passed, 
     *      a string containing the name of the shape and
     *      an array containing the neccessary data to create the shape.
     *  -   Then the invoker will call this Objects execute method which will
     *      use a switch to decide what shape we are handling.
     *  -   Within the relevent switch block the method inside the Canvas Class
     *      responsible for adding new shapes will be called and passed a newly
     *      created shape.
     *  -   This method will return an integer representing the shapes Unique ID
     *      so that in the future if the Command needs to be undone we will be able
     *      to delete the shape. This integer is saved withing this Object.
     *  -   To unexecute we simple call the method inside the Canvas Class responsible
     *      for removing shapes and pass it the ID.
     */
    class Command_Add : ICommand
    {
        int id_of_shape;
        string action;
        string[] _data;
        double[] data;
        public Command_Add(string action, string[] _data)
        {
            this.action = action;
            this._data = _data;
        }
        void ICommand.execute()
        {
            try
            {
                if(action != "path")
                {
                    data = new double[_data.Length];
                    for (int i = 0; i < _data.Length; i++)
                    {
                        data[i] = Convert.ToDouble(_data[i]);
                    }
                }

                switch (action)
                {
                    case "rect":
                        id_of_shape = Program.canvas.AddShape(new Rectangle(data[0], data[1], data[2], data[3]));
                        Console.WriteLine($"Rectangle added with id: [{id_of_shape}]\n");
                        break;
                    case "circle":
                        id_of_shape = Program.canvas.AddShape(new Circle(data[0], data[1], data[2]));
                        Console.WriteLine($"Circle added with id: [{id_of_shape}]\n");
                        break;
                    case "ellipse":
                        id_of_shape = Program.canvas.AddShape(new Ellispse(data[0],data[1],data[2],data[3]));
                        Console.WriteLine($"Ellipse added with id: [{id_of_shape}]\n");
                        break;
                    case "line":
                        id_of_shape = Program.canvas.AddShape(new Line(data[0], data[1], data[2], data[3]));
                        Console.WriteLine($"Line added with id: [{id_of_shape}]\n");
                        break;
                    case "polyline":

                        if(data.Length % 2 == 0)
                        {
                            Point temp;
                            List<Point> points = new List<Point>();

                            for(int i=0; i<data.Length; i++)
                            {
                                temp = new Point(data[i],data[i+1]);
                                points.Add(temp);
                                i++;
                            }
                            id_of_shape = Program.canvas.AddShape(new Polyline(points));
                            Console.WriteLine($"Polyline added with id: [{id_of_shape}]\n");

                        } else
                        {
                            Console.WriteLine("Failed to create Polyline, must have an even number of numeric values.");
                        }
                        break;
                    case "polygon":

                        if (data.Length % 2 == 0)
                        {
                            Point temp;
                            List<Point> points = new List<Point>();

                            for (int i = 0; i < data.Length; i++)
                            {
                                temp = new Point(data[i], data[i + 1]);
                                points.Add(temp);
                                i++;
                            }
                            id_of_shape = Program.canvas.AddShape(new Polygon(points));
                            Console.WriteLine($"Polygon added with id: [{id_of_shape}]\n");

                        }
                        else
                        {
                            Console.WriteLine("Failed to create Polyline, must have an even number of numeric values.");
                        }
                        break;
                    case "path":
                        string instructions = "";
                        foreach(string obj in _data)
                        {
                            instructions += obj + ",";
                        }

                        id_of_shape = Program.canvas.AddShape(new Path(instructions));
                        Console.WriteLine($"Path added with id: [{id_of_shape}]\n");
                        break;

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void ICommand.unexecute()
        {
            Canvas.RemoveShape(id_of_shape);
        }
    }
}
