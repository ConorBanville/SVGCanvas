using System;
namespace Assignment04
{
    class Line : Shape
    {
        double x1, y1, x2, y2;
        string  stroke, strokeDash, strokeWidth;
        static string[] defaults = new string[3];
        string name;
        int id;

        public Line(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            stroke = defaults[0];
            strokeDash = defaults[1];
            strokeWidth = defaults[2];
            name = "line";
        }

        //Set the default decorations for a Line
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
            return $"Line:\n===========\nx1: {x1}\ny1: {y1}\nx2: {x2}\ny2: {y2}\nstroke: {stroke}\nstrokeDash: {strokeDash}\nstrokeWidth: {strokeWidth}\nUnique ID: {id}";
        }

        string[] Shape.Update(string[] new_data)
        {
            string[] old_data = new string[4];
            //Store the old values
            old_data[0] = x1 + "";
            old_data[1] = y1 + "";
            old_data[2] = x2 + "";
            old_data[3] = y2 + "";
            //Update the values with the new data
            try
            {
                x1 = Convert.ToDouble(new_data[0]);
                y1 = Convert.ToDouble(new_data[1]);
                x2 = Convert.ToDouble(new_data[1]);
                y2 = Convert.ToDouble(new_data[1]);
            }
            catch
            {
                Console.WriteLine("Failed to update shape, all values must be Numeric.\n");
                return null;
            }
            return old_data;
        }

        //Edit the decorations of a Line
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
            string svg = $"<line x1=\"{x1}\" y1=\"{y1}\" x2=\"{x2}\" y2=\"{y2}\"" +
                $" stroke=\"{stroke}\" stroke-width=\"{strokeWidth}\"";

            if (strokeDash.Length != 0)
            {
                svg += $" stroke-dasharray=\"{strokeDash}\"";
            }

            return svg += "/>";
        }
    }
}
