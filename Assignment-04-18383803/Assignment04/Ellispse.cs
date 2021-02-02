using System;

namespace Assignment04
{
    class Ellispse : Shape
    {
        double x, y, rad_x, rad_y;
        string fill, stroke, strokeDash, strokeWidth;
        static string[] defaults = new string[4];
        string name;
        int id;

        public Ellispse(double x, double y, double rad_x, double rad_y)
        {
            this.x = x;
            this.y = y;
            this.rad_x = rad_x;
            this.rad_y = rad_y;
            fill = defaults[0];
            stroke = defaults[1];
            strokeDash = defaults[2];
            strokeWidth = defaults[3];
            name = "ellipse";
        }

        //Set the default decorations for an Ellipse
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
            return $"Ellipse:\n===========\ncx: {x}\ncy: {y}\nrad_x: {rad_x}\nrad_y: {rad_y}\nfill: {fill}\nstroke: {stroke}\nstrokeDash: {strokeDash}\nstrokeWidth: {strokeWidth}\nUnique ID: {id}";
        }

        string[] Shape.Update(string[] new_data)
        {
            string[] old_data = new string[4];
            //Store the old values
            old_data[0] = x + "";
            old_data[1] = y + "";
            old_data[2] = rad_x + "";
            old_data[3] = rad_y + "";
            //Update the values with the new data
            try
            {
                x = Convert.ToDouble(new_data[0]);
                y = Convert.ToDouble(new_data[1]);
                rad_x = Convert.ToDouble(new_data[1]);
                rad_y = Convert.ToDouble(new_data[1]);
            }
            catch
            {
                Console.WriteLine("Failed to update shape, all values must be Numeric.\n");
                return null;
            }
            return old_data;
        }

        //Edit the decorations of an Ellispe
        string[] Shape.Decorations(string[] new_data)
        {
            //Store the old values
            string[] old_data = defaults;
            //Update the valuse with the new data
            fill = new_data[0];
            stroke = new_data[1];
            strokeDash = new_data[2];
            strokeWidth = new_data[3];

            return old_data;
        }

        //Embed this shape in svg
        string Shape.Embed()
        {
            string svg = $"<ellipse cx=\"{x}\" cy=\"{x}\" rx=\"{rad_x}\" ry=\"{rad_y}\"" +
                $" stroke=\"{stroke}\" fill=\"{fill}\" stroke-width=\"{strokeWidth}\"";

            if (strokeDash.Length != 0)
            {
                svg += $" stroke-dasharray=\"{strokeDash}\"";
            }

            return svg += "/>";
        }
    }
}
