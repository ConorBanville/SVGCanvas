using System;
namespace Assignment04
{
    class Path : Shape
    {
        string instructions;
        string fill, stroke, strokeDash, strokeWidth;
        static string[] defaults = new string[4];
        string name;
        int id;

        public Path(string instructions)
        {
            this.instructions = instructions.Substring(0,instructions.Length-1);
            fill = defaults[0];
            stroke = defaults[1];
            strokeDash = defaults[2];
            strokeWidth = defaults[3];
            name = "path";
        }

        //Set the default decorations for a Rectangle
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
            return $"Path:\n===========\nInstructions:\n[{instructions}]\nfill: {fill}\nstroke: {stroke}\nstrokeDash: {strokeDash}\nstrokeWidth: {strokeWidth}\nUnique ID: {id}";
        }

        public string[] Update(string[] new_data)
        {
            //Convert the current values to an array
            string[] old_data = instructions.Split(',');

            //Update the current values with the new ones 
            string new_instructions = "";

            foreach(String obj in new_data)
            {
                new_instructions += obj + ",";
            }
            //Remove extra comma at the end 
            new_instructions = new_instructions.Substring(0, new_instructions.Length - 1);
            //Update the instructions
            instructions = new_instructions;
            //Return the old values
            return old_data;
        }

        //Edit the decorations of a Path
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

        //Embed this Path into svg 
        string Shape.Embed()
        {
            String svg = $"<path fill=\"{fill}\" stroke=\"{stroke}\" stroke-width=\"{strokeWidth}\" \n" +
                $"d=\"{instructions}\"";

            if (strokeDash.Length != 0)
            {
                //Only add in the stroke-dasharray attribute if it's not set to the default
                svg += $" stroke-dasharray=\"{strokeDash}\"";
            }

            return svg + "/>";//Return the embedded rectangle
        }
    }
}
