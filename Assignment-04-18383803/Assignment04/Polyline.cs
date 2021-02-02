using System;
using System.Collections.Generic;


namespace Assignment04
{
    class Polyline : Shape
    {
        List<Point> points;
        string stroke, strokeDash, strokeWidth;
        static string[] defaults = new string[3];
        string name;
        int id;

        public Polyline(List<Point> points)
        {
            this.points = points;
            stroke = defaults[0];
            strokeDash = defaults[1];
            strokeWidth = defaults[2];
            name = "polyline";
        }

        //Set the default decorations for a Polyline
        public static string[] SetDefaults(string[] newDefaults)
        {
            string[] old_data = defaults;
            defaults = newDefaults;
            return old_data;
        }
        string Shape.GetType()
        {
            return name;
        }

        void Shape.SetId(int id)
        {
            this.id = id;
        }

        int Shape.GetId()
        {
            return id;
        }

        string Shape.GetDetails()
        {
            string s = "Polyline:\n===========\npoints = \"";
            foreach (Point obj in points)
            {
                s += obj.toString() + " ";
            }
            s = s.Substring(0, s.Length - 1);//Remove the extra space on the end
            s += $"\"\nstroke: {stroke}\nstrokeDash: {strokeDash}\nstrokeWidth: {strokeWidth}\nUnique ID: {id}";

            return s;
        }

        string[] Shape.Update(string[] new_data)
        {          
            string[] old_data = null;   //Variable to store the old list of points
            if(new_data.Length %2 == 0) //Must have an even number of new values, one for and x and one for a y
            {
                old_data = new string[points.Count*2];  //The old_data array will be twice th length of the points list 
                //Convert the current list to a string array
                for(int i=0; i<old_data.Length; i++)
                {
                    //For every old_data cell we have, store the corresponding point's x and y  
                    old_data[i] = points[i/2].GetX();   //The index of the points list must increment at half the speed of the index for the old_data
                    old_data[i+1] = points[i/2].GetY();
                    i++;    //Array needs to increment in steps of 2
                }

                //now that we have stored the old values we can
                //Update the points list
                List<Point> newPoints = new List<Point>();  //Create a new List 
                try
                {
                    //For every new values we recieved
                    for(int i=0; i<new_data.Length; i++)
                    {
                        //Try to create a point from the value pairs and add it to the new list 
                        newPoints.Add(new Point(Convert.ToDouble(new_data[i]), Convert.ToDouble(new_data[i+1])));
                        i++;
                    }

                    points = newPoints;
                    return old_data;
                }
                catch
                {
                    return null;
                }
                
            }
            return old_data;
        }

        //Edit the decorations of a Polyline
        string[] Shape.Decorations(string[] new_data)
        {
            //Store the old values
            string[] old_data = defaults;
            //Update the valuse with the new data
            stroke = new_data[0];
            strokeDash = new_data[1];
            strokeWidth = new_data[2];

            return old_data;
        }

        //Embed this shape in svg
        string Shape.Embed()
        {
            string svg = $"<polyline stroke=\"{stroke}\" stroke-width=\"{strokeWidth}\" points=\"";

            foreach(Point obj in points)
            {
                svg += obj.toString() + " ";
            }
            svg = svg.Substring(0, svg.Length - 1)+"\"";

            if(strokeDash.Length != 0)
            {
                //Only add in the stroke-dasharray attribute if it's not set to the default
                svg += $" stroke-dasharray=\"{strokeDash}\"";
            }
            return svg += "/>";
        }
    }
}
