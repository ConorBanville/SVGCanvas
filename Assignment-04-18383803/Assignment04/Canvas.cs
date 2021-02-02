using System.Collections.Generic;

namespace Assignment04
{
    class Canvas
    {
        public static List<Shape> shapes;
        public double height, width;
        static int id_counter;
        static int counter;

        //Constructor for Canvas
        public Canvas(double height, double width)
        {
            this.height = height;
            this.width = width;
            id_counter = 0;
            counter = 0;
            shapes = new List<Shape>();
        }

        //Add a new shape Return a Unique ID
        public int AddShape(Shape newShape)
        {
            shapes.Add(newShape);
            id_counter++;
            counter++;
            shapes[counter - 1].SetId(id_counter);
            return id_counter;
        }

        //Remove a shape based on a given Unique ID
        public static void RemoveShape(int id)
        {
            for(int i=0; i<shapes.Count; i++)
            {
                if(shapes[i].GetId() == id)
                {
                    shapes.RemoveAt(i);
                    counter--;

                }
            }
        }

        public static CustomObject ReturnShape(int id)
        {
            int shape_id = -1;

            for(int i=0; i<shapes.Count; i++)
            {
                if(shapes[i].GetId() == id)
                {
                    shape_id = i;
                }
            }

            if (shape_id == -1)
            {
                return new CustomObject(null,-1);
            }
            else
            {
                return new CustomObject(shapes[shape_id], shape_id);
            }
        }

        public List<Shape> ReturnShapes()
        {
            return shapes;
        }

        public void InsertShape(Shape newShape, int index)
        {
            List<Shape> tempShapes = new List<Shape>();

            //Add in all the shapes before the index
            for(int i=0; i<index; i++)
            {
                tempShapes.Add(shapes[i]);
            }

            //Insert the new Shape
            tempShapes.Add(newShape);

            //Add in the shapes after the index
            for(int i=index; i<shapes.Count; i++)
            {
                tempShapes.Add(shapes[i]);
            }

            shapes = tempShapes;
        }
        //Wrap a shape in an Object also containing the index the shape was found at

        //Moves a shape specified by a uid to a new index. Returns the index the shape was initially found at
        public int MoveShape(int uid_of_shape, int new_index)
        {
            for(int i=0; i<shapes.Count; i++)
            {
                if(shapes[i].GetId() == uid_of_shape)
                {
                    Shape target = shapes[i];   //Store the shape to be moved
                    shapes.RemoveAt(i);         //Remove it 

                    List<Shape> tempShapes = new List<Shape>(); //A new list that will be reordered
                    //Add in all the shapes before the new_index
                    for(int j=0; j<new_index; j++)
                    {
                        tempShapes.Add(shapes[j]);
                    }
                    tempShapes.Add(target);
                    //Add in all the shapes after the new_index
                    for(int j=new_index; j<shapes.Count; j++)
                    {
                        tempShapes.Add(shapes[j]);
                    }
                    //Override the current list of shapes
                    shapes = tempShapes;
                    return i;
                }
            }
            return -1;
        }

        //Update the dimensions of a shape
        public string[] Update(int id_of_shape, string[] new_data)
        {
            string[] old_data = null;
            for(int i=0; i<shapes.Count; i++)
            {
                if(id_of_shape == shapes[i].GetId())
                {
                    old_data = shapes[i].Update(new_data);
                }
            }

            return old_data;
        }

        //Update the decorations of a shape
        public string[] Decorations(int id_of_shape, string[] new_data)
        {
            string[] old_data = null;
            for(int i=0; i<shapes.Count; i++)
            {
                if(id_of_shape == shapes[i].GetId())
                {
                    old_data = shapes[i].Decorations(new_data);
                }
            }

            return old_data;
        }

        //Return a svg string that has all the shapes
        public string GetSVG()
        {
            string svg = $"<?xml version=\"1.0\" standalone=\"no\"?>\n" +
                $"<svg width=\"{width}\" height=\"{height}\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\">\n";

            foreach(Shape obj in shapes)
            {
                svg += obj.Embed()+"\n";
            }

            return svg += "</svg>";
        }

        //Set a new height and width for the canvas
        public double[] SetCanvas(double width, double height)
        {
            double[] old_data = new double[] { this.width, this.height };
            this.height = height;
            this.width = width;
            return old_data;
        }
        
        public string GetType(int id_of_shape)
        {
            foreach(Shape obj in shapes)
            {
                if(obj.GetId() == id_of_shape)
                {
                    return obj.GetType();
                }
            }
            return null;
        }
        //This is a struct that holds both the shape and its uid, I couldn't think of a better name
        public struct CustomObject
        {
            Shape shape;
            int index;
            public CustomObject(Shape shape, int index)
            {
                this.shape = shape;
                this.index = index;
            }

            public Shape GetShape()
            {
                return shape;
            }

            public int GetIndex()
            {
                return index;
            }
        }
    }
}
