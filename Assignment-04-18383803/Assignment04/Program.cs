using System;

namespace Assignment04

    /* This code was developed in Visual Studio 2019 on a Windows 10 x64 machine.
     * Student Number = 18383803
     */
{
    /*  All commands are outlined on a help screen. To View this enter the command '-h'.
     *  The Unique ID of a shape will be printed once when the shape has been created and 
     *  again every time the canvas is displayed.
     *  
     *  Creating shapes.
     *  ===============
     *  To create a shape enter the name of the shape followed by the relevent values needed 
     *  to create that shape enclosed in brackets.
     *  For example to create a circle with it's center at the point {1,2} and a radius of 3
     *  you would enter the command --circle(1,2,3). Note that all the numbers are separated 
     *  by commas and that the brackets must directly follow the word circle. The only keyword 
     *  for creating shapes that is not the exact name of the shape is for Rectangles the 
     *  keyword used is rect.
     *  
     *  Changing the Z-Index of a shape
     *  ===============================
     *  To change the z-index of a shape use the keyword shift followed by the Unique ID 
     *  of that shape and the new index of the shape. For example to move a shape with the 
     *  ID 2 to the third position in the index use the command --shift(2,2). Note that index
     *  value is zero based.
     *  
     *  Updating the Non-Style attributes of a shape
     *  ============================================
     *  For example the change the posistion, length and width of a rectangle with a Unique ID
     *  of 2 use the command --update(2,10,10,100,150). This would position this rectangle at 
     *  the point {10,10} and give it a width of 100 and a length of 150. The values that follow
     *  the Unique ID are accepted in the same order that was used to create the shape.
     *  
     *  Update the Style attributes of a shape
     *  ======================================
     *  Similarly to the update command you will identify the shape you want to update by using
     *  it's Unique ID number, followed by the new values in this order:
     *  ->  fill, stroke, stroke-dasharray, stroke-width.
     *  Note, say you didn't want to set a stroke-dasharray you may ommit this value by leaving 
     *  an empty space, example --edit(1,blue,red,,2). In this example I have ommited the 
     *  stroke-dasharray.
     *  Note, that for the line shape and the polyline shape the fill attribute is not applicable 
     *  and should be left out.
     *  
     *  Deleting a shape
     *  ================
     *  To delete a shape use the command delete. Again the Unique ID is used to specify which shape 
     *  you want to delete. For example to the delete the shape with the Unique ID 2 you would enter
     *  the command --delete(2).
     *  
     *  Changing the dimensions of the canvas
     *  =====================================
     *  The Canvas is initialised with a width of 1000 and a height of 1000. To change this you may use
     *  the command canvas at any time. For example if you wanted the change the canvas to have a width
     *  of 2000 and a height 500 you would use the command, --canvas(2000,500).
     *  
     *  Setting the default Style Attributes for a shape
     *  ================================================
     *  To set the default style attributes of any shape use the command --default. The first argument 
     *  given must be the name of the shape as it is used for creation. For example to change the default
     *  style attributes for a Rectangle to have a fill of blue, a stroke of red, a stroke-dasharray
     *  of 11 11 and a stroke-width of 5 you would use the command --default(rect,blue,red,11 11,5).
     *  All shapes are initialised with a fill of transparent, a stoke of black, a stroke-dasharray of 
     *  none and a stroke-width of 1.
     *  
     *  These commands outlined are what I refer to as Long Commands. They are the commands that take
     *  arguments and therefor take some explaining. I refer to commands that do not take arguments as
     *  Quick Commands. 
     *  Note that Quick Command are only preeceded by one dash `-`. For example the Quick Command that
     *  is used to the display the canvas is -c. I dont see it neccesary to go through all the Quick 
     *  Commands here. For a list of all Quick Commands refer to the help screen. Use the command `-h`
     *  to view the help screen.
     * 
     */



    /*  The Design Pattern used to facilitate the undo, redo functionality is the Command Design Pattern.
     *  To implement this there is a Command Object for each of the Long Commands. When the relevent command
     *  is recieved by the parser a Command Object is instansiated and stored in a list contained within the 
     *  invoker class. The in invoker will automatically call the execute function of the new Command Object
     *  when it is added. All Command Classes implement the ICommand interface. All Command classes have both
     *  a execute method and an unexecute method. The unexacute method will be very specific to the Command.
     *  The invoker will automatically remove unreachable Commands as they become unreachable.
     *  What do I mean by unreachable? 
     *  Say the user runs the program and gives three different commands, then decides to undo the last command.
     *  Immidiately after having undone this command the user then decides to give a fourth command. The the third 
     *  command has now become unreachable and is therefor removed.
     */
    class Program
    {
        //  The Ivoker Object that will maintain a list of ICommands and perform opperations with those ICommands
        static Invoker invoker = new Invoker();
        // The Canvas Object that will maintain a list of Shapes and perfom opperations on those Shapes 
        public static Canvas canvas = new Canvas(1000,1000);
        //  A vairable that will track which is the current actionable ICommand, starts at zero because we have no ICommands
        public static int index_of_current_invoke = 0;
        static void Main(string[] args)
        {
            //Set the Default style attributes for each shape
            Rectangle.SetDefaults(new string[]{ "transparent","black","","1"});
            Circle.SetDefaults(new string[] { "transparent", "black", "", "1" });
            Ellispse.SetDefaults(new string[] { "transparent", "black", "", "1" });
            Line.SetDefaults(new string[] {"black", "", "1" });
            Polyline.SetDefaults(new string[] { "black", "", "1" });
            Polygon.SetDefaults(new string[] { "transparent", "black", "", "1" });
            Path.SetDefaults(new string[] { "transparent", "black", "", "1" });
            //Notify the user that the program is now accepting input
            Console.WriteLine("Start!\n");

            while (true)
            {
                Console.WriteLine("Command: ");
                string command = Console.ReadLine();    //Accept input 

                try
                {
                    int result = Parse(command);    //Give the input to the parser, this will return an integer result
                    
                    switch (result)     //A switch to handle the result of the parse
                    {
                        case 0: // 0 returned, parser failed to understand the command
                            Console.WriteLine("Couldn't parse.");
                            break;
                        case 1: // 1 returned, Everything went ok, command parsed and executed. 
                            //Console.WriteLine("Command Parsed Successfuly.");   <-- Originally I displayed this standard message, I found this to be annoying and  to clutter up the console
                                                                                      //I never got around to removing the traces of this.
                            break;
                        case 2: // 2 returned, Everything went ok and some feed back has already been printed
                            break; 
                        case 3: // 3 returned, Progam has been closed
                            Console.WriteLine("Goodbye \x2\x3");
                            return;
                    }
                }
                catch
                {
                    Console.WriteLine("Couldn't parse.");   //The parser has failed to return any result
                }
            }
        }

        static int Parse(string command)
        {
            int dash_count = 0;

            //Return if command doesn't start with '-' or is the empty string
            if(!command.StartsWith('-') || command.Length == 0)
            {
                return 0;
            }
            //Count up the dashes at the start
            for(int i=0; i<2; i++)
            {
                if (command[i] == '-') dash_count++;
            }
            //Send the command off to be executed, their result will be returned.
            switch (dash_count) 
            {
                case 1:
                    try
                    {
                        int result = ShortCommand(command);
                        return result;
                    }
                    catch
                    {
                        return 0;
                    }
                case 2:
                    try
                    {
                        int result = LongCommand(command);
                        return result;
                    }
                    catch
                    {
                        return 0;
                    }
                default:
                    return 1;
            }
        }
    
        static int LongCommand(string command)
        {
            command = command.Substring(2, command.Length - 2); //Remove the appending dashes
            string args = command.Substring(command.IndexOf('(')+1, command.IndexOf(')') - command.IndexOf('(')-1); //Pull out the values contained within the given command
            string[] values = args.Split(',');//Put the values in an array
            command = command.Substring(0, command.IndexOf('('));   //Isolate the key word given

            switch (command)    //Handle each of the keywords
            {
                //  The block's logic will be identical for most of the keywords, for the editing of shapes somee additional steps is required, have 
                //  a look below for more information.

                /*  1. We verify that we have the correct amount of values need to execute this command
                 *  2. We try to convert the string values in the array to doubles (or sometimes integers if required).
                 *  3. We try to create the relevent Command Object from the converted values. (I say try, but really if step 2 was succesful then we should have no problem) 
                 *  4. Having created the command we give it to the invoker who will add it to it's list and execute it
                 *  5. We increment the index that tracks which command was given last.
                 *  6. We return that exarything when ok and the command has now been parsed 
                 *  
                 *  If any of this fails we print a possible reason why it failed (the most likely reason) and return that everything did not go ok.
                 */
                case "canvas":
                    if (values.Length != 2)
                    {
                        Console.WriteLine("Must give exactly 2 Numeric values in order to change the width and height of the canvas.\n");
                        return 2;
                    }
                    else
                    {
                        try
                        {
                            invoker.AddCommand(new Command_Canvas(Convert.ToDouble(values[0]), Convert.ToDouble(values[1])));
                            index_of_current_invoke++;
                            return 1;
                        }
                        catch
                        {
                            Console.WriteLine("Could not change the canvas width and height with values given. Must be Numeric.\n");
                            return 2;
                        }
                    }
                //Move a shape to a new index
                case "shift":
                    if (values.Length != 2)
                    {
                        Console.WriteLine("Must have give exactly 2 Numeric Values.\n");
                        return 2;
                    }
                    try
                    {
                        invoker.AddCommand(new Command_ZEdit(Convert.ToInt32(values[0] + ""), Convert.ToInt32(values[1] + "")));
                        index_of_current_invoke++;
                        return 1;
                    }
                    catch
                    {
                        Console.WriteLine("Values given must be Posotive Whole Numbers.\n");
                        return 2;
                    }
                //Delete based on a given ID
                case "delete":
                    if (values.Length != 1)
                    {
                        Console.WriteLine("Must give exactly one Unique ID.\n");
                        return 2;
                    }
                    try
                    {
                        invoker.AddCommand(new Command_Remove(Convert.ToInt32(values[0])));
                        index_of_current_invoke++;
                        return 1;
                    }
                    catch
                    {
                        return 0;
                    }
                //Add a rectangle
                case "rect":
                    if (values.Length != 4)
                    {
                        Console.WriteLine("Must include 4 Numeric values in order to add a Rectangle.\n");
                        return 2;
                    }
                    try
                    {
                        //Give the invoker a new Command
                        invoker.AddCommand(new Command_Add(command, values));
                        index_of_current_invoke++;
                        return 2;
                    }
                    catch
                    {
                        return 0;
                    }
                //Add a circle
                case "circle":
                    if (values.Length != 3)
                    {
                        Console.WriteLine("Must include 3 Numeric values in order to add a Circle.\n");
                        return 2;
                    }
                    try
                    {
                        //Give the invoker a new Command
                        invoker.AddCommand(new Command_Add(command, values));
                        index_of_current_invoke++;
                        return 2;
                    }
                    catch
                    {
                        return 0;
                    }
                //Add an ellipse
                case "ellipse":
                    if (values.Length != 4)
                    {
                        Console.WriteLine("Must include 4 Numeric values in order to add an Ellispe.\n");
                        return 2;
                    }
                    try
                    {
                        //Give the invoker a new Command
                        invoker.AddCommand(new Command_Add(command, values));
                        index_of_current_invoke++;
                        return 2;
                    }
                    catch
                    {
                        return 0;
                    }
                //Add a line
                case "line":
                    if (values.Length != 4)
                    {
                        Console.WriteLine("Must include 4 Numeric values in order to add a Line.\n");
                        return 2;
                    }
                    try
                    {
                        //Give the invoker a new Command
                        invoker.AddCommand(new Command_Add(command, values));
                        index_of_current_invoke++;
                        return 2;
                    }
                    catch
                    {
                        return 0;
                    }
                //Add a polyline
                case "polyline":
                    if (values.Length < 4)
                    {
                        Console.WriteLine("Must include at least 4 Numeric values in order to add a Polyline.\n");
                        return 2;
                    }
                    try
                    {
                        //Give the invoker a new Command
                        invoker.AddCommand(new Command_Add(command, values));
                        index_of_current_invoke++;
                        return 2;
                    }
                    catch
                    {
                        return 0;
                    }
                //Add a polygon
                case "polygon":
                    if (values.Length < 4)
                    {
                        Console.WriteLine("Must include at least 4 Numeric values in order to add a Polygon.\n");
                        return 2;
                    }
                    try
                    {
                        //Give the invoker a new Command
                        invoker.AddCommand(new Command_Add(command, values));
                        index_of_current_invoke++;
                        return 2;
                    }
                    catch
                    {
                        return 0;
                    }
                //Add a path
                case "path":
                    if (values.Length < 1)
                    {
                        Console.WriteLine("Must include at least 1 Numeric values in order to add a Path.\n");
                        return 2;
                    }
                    try
                    {
                        //Give the invoker a new Command
                        invoker.AddCommand(new Command_Add(command, values));
                        index_of_current_invoke++;
                        return 2;
                    }
                    catch
                    {
                        return 0;
                    }
                /*  Now for the next 3 blocks we have an additional.
                 *  
                 *  Addional Step 1. We must first find out what type of shape we are dealing with in order to verify we have the correct ammount of aruments.
                 *  Addional Step 2. We then perform another switch with this inforamtion.
                 *  
                 *  In the "default" case we have been passed the information as the first argument so we only need to perform the switch 
                 */
                case "edit":
                    string target_shape1;
                    try
                    {
                        target_shape1 = canvas.GetType(Convert.ToInt32(values[0]));
                    }
                    catch
                    {
                        return 2;
                    }
                    switch (target_shape1)
                    {
                        case "rect":
                            if(values.Length == 5)
                            {
                                invoker.AddCommand(new Command_Decorations(values[0], new string[] {values[1], values[2], values[3], values[4]}));
                                index_of_current_invoke++;
                                    return 1;
                            } else
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for a rectangle.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--edit(uid,red,black,,2).\n");
                                return 2;
                            }
                        case "circle":
                            if (values.Length == 5)
                            {
                                invoker.AddCommand(new Command_Decorations(values[0], new string[] { values[1], values[2], values[3], values[4] }));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for a circle.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--edit(uid,red,black,,2).\n");
                                return 2;
                            }
                        case "ellipse":
                            if (values.Length == 5)
                            {
                                invoker.AddCommand(new Command_Decorations(values[0], new string[] { values[1], values[2], values[3], values[4] }));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for an ellispe.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--edit(uid,red,black,,2).\n");
                                return 2;
                            }
                        case "line":
                            if (values.Length == 4)
                            {
                                invoker.AddCommand(new Command_Decorations(values[0], new string[] { values[1], values[2], values[3]}));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 3 new default decoration values for a line.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--edit(uid,red,,2).\n");
                                return 2;
                            }                        
                        case "polyline":
                            if (values.Length == 4)
                            {
                                invoker.AddCommand(new Command_Decorations(values[0], new string[] { values[1], values[2], values[3]}));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 3 new default decoration values for a polyline.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--edit(uid,red,,2).\n");
                                return 2;
                            }
                        case "polygon":
                            if (values.Length == 5)
                            {
                                invoker.AddCommand(new Command_Decorations(values[0], new string[] { values[1], values[2], values[3], values[4]}));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for a polygon.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--edit(uid,red,black,,2).\n");
                                return 2;
                            }
                        case "path":
                            if (values.Length == 5)
                            {
                                invoker.AddCommand(new Command_Decorations(values[0], new string[] { values[1], values[2], values[3], values[4] }));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for a path.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--edit(uid,red,black,,2).\n");
                                return 2;
                            }
                        default:
                            return 2;
                    }
                case "default":
                    string shape = values[0];
                    switch (shape)
                    {
                        case "rect":
                            if(values.Length != 5)
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for a rectangle.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--default(rect,red,black,,2).\n");
                                return 2;
                            }
                            invoker.AddCommand(new Command_Defaults(shape, new string[] { values[1], values[2], values[3], values[4] }));
                            index_of_current_invoke++;
                            return 1;
                        case "circle":
                            if (values.Length != 5)
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for a circle.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--default(rect,red,black,,2).\n");
                                return 2;
                            }
                            invoker.AddCommand(new Command_Defaults(shape, new string[] { values[1], values[2], values[3], values[4] }));
                            index_of_current_invoke++;
                            return 1;
                        case "ellipse":
                            if (values.Length != 5)
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for an ellipse.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--default(rect,red,black,,2).\n");
                                return 2;
                            }
                            invoker.AddCommand(new Command_Defaults(shape, new string[] { values[1], values[2], values[3], values[4] }));
                            index_of_current_invoke++;
                            return 1;
                        case "line":
                            if (values.Length != 4)
                            {
                                Console.WriteLine("Must include exactly 3 new default decoration values for a line.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--default(rect,black,,2).\n");
                                return 2;
                            }
                            invoker.AddCommand(new Command_Defaults(shape, new string[] { values[1], values[2], values[3]}));
                            index_of_current_invoke++;
                            return 1;
                        case "polyline":
                            if (values.Length != 4)
                            {
                                Console.WriteLine("Must include exactly 3 new default decoration values for polyline.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--default(rect,black,,2).\n");
                                return 2;
                            }
                            invoker.AddCommand(new Command_Defaults(shape, new string[] { values[1], values[2], values[3] }));
                            index_of_current_invoke++;
                            return 1;
                        case "polygon":
                            if (values.Length != 5)
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for polygon.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--default(rect,black,,2).\n");
                                return 2;
                            }
                            invoker.AddCommand(new Command_Defaults(shape, new string[] { values[1], values[2], values[3], values[4] }));
                            index_of_current_invoke++;
                            return 1;
                        case "path":
                            if (values.Length != 5)
                            {
                                Console.WriteLine("Must include exactly 4 new default decoration values for path.\nIf you want to omit a value you may leave it blank.\nEg. To leave strokeDash blank you could run the command:\n--default(rect,black,,2).\n");
                                return 2;
                            }
                            invoker.AddCommand(new Command_Defaults(shape, new string[] { values[1], values[2], values[3], values[4] }));
                            index_of_current_invoke++;
                            return 1;
                        default:
                            return 0;
                    }
                //Edit the dimensions of a shape. The first argument is the uid of the shape to be updated
                case "update":

                    //Query the canvas for the type of shape we need to update
                    string target_shape;
                    try
                    {
                        target_shape = canvas.GetType(Convert.ToInt32(values[0]));                        
                    }
                    catch
                    {
                        return 2;
                    }
                    
                    switch (target_shape)
                    {                       
                        case "rect":
                            if(values.Length == 5)
                            {
                                string[] subset_values = new string[values.Length - 1];
                                subset_values[0] = values[1];
                                subset_values[1] = values[2];
                                subset_values[2] = values[3];
                                subset_values[3] = values[4];
                                invoker.AddCommand(new Command_Edit(values[0], subset_values));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 4 numeric values in order to update the dimensions of a Rectangle.\n");
                                return 2;
                            }
                        case "circle":
                            if (values.Length == 4)
                            {
                                string[] subset_values = new string[values.Length - 1];
                                subset_values[0] = values[1];
                                subset_values[1] = values[2];
                                subset_values[2] = values[3];
                                invoker.AddCommand(new Command_Edit(values[0], subset_values));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 3 numeric values in order to update the dimensions of a Circle.\n");
                                return 2;
                            }
                        case "ellipse":
                            if (values.Length == 5)
                            {
                                string[] subset_values = new string[values.Length - 1];
                                subset_values[0] = values[1];
                                subset_values[1] = values[2];
                                subset_values[2] = values[3];
                                subset_values[3] = values[4];
                                invoker.AddCommand(new Command_Edit(values[0], subset_values));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 4 numeric values in order to update the dimensions of a Ellipse.\n");
                                return 2;
                            }
                        case "line":
                            if (values.Length == 5)
                            {
                                string[] subset_values = new string[values.Length - 1];
                                subset_values[0] = values[1];
                                subset_values[1] = values[2];
                                subset_values[2] = values[3];
                                subset_values[3] = values[4];
                                invoker.AddCommand(new Command_Edit(values[0], subset_values));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include exactly 4 numeric values in order to update the dimensions of a Line.\n");
                                return 2;
                            }
                        case "polyline":
                            if (values.Length > 2 && values.Length % 2 != 0)
                            {
                                string[] subset_values = new string[values.Length - 1];
                                //Create a subset array == values.Complement the uid
                                for (int i=1; i<values.Length; i++)
                                {
                                    subset_values[i-1] = values[i];
                                }
                                invoker.AddCommand(new Command_Edit(values[0], subset_values));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must have include a uid and at least one point with and x and y.");
                                Console.WriteLine("Therefor the amount of arguments given must be odd, to ensure that\nevery point has an x and a y coordinate.\n");
                                return 2;
                            }
                        case "polygon":
                            if (values.Length > 2 && values.Length % 2 != 0)
                            {
                                string[] subset_values = new string[values.Length - 1];
                                //Create a subset array == values.Complement the uid
                                for (int i = 1; i < values.Length; i++)
                                {
                                    subset_values[i - 1] = values[i];
                                }
                                invoker.AddCommand(new Command_Edit(values[0], subset_values));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must have include a uid and at least one point with and x and y.");
                                Console.WriteLine("Therefor the amount of arguments given must be odd, to ensure that\nevery point has an x and a y coordinate.\n");
                                return 2;
                            }
                        case "path":
                            if (values.Length >= 2)
                            {
                                string[] subset_values = new string[values.Length - 1];
                                //Create a subset array == values.Complement the uid
                                for (int i = 1; i < values.Length; i++)
                                {
                                    subset_values[i - 1] = values[i];
                                }
                                invoker.AddCommand(new Command_Edit(values[0], subset_values));
                                index_of_current_invoke++;
                                return 1;
                            }
                            else
                            {
                                Console.WriteLine("Must include at least 1 instruction in order to update the demensions of a path.\n");
                                return 2;
                            }
                        default:
                            return 2;
                    }
                default:
                    Console.WriteLine("Invalid command given, for help you can use the command '-h'");
                    return 0;
            }
        }

        static int ShortCommand(string command)
        {
            command = command.Substring(1, command.Length - 1);     // Remove the dash (`-`) from the command
            command = command.ToLower();
            switch (command)    // A switch to handle each command
            {
                case "q":   //Quit the program
                    return 3;
                case "h":   //Print a help message
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Quick Commands\t\x10\n");
                    Console.WriteLine("-h\t\t|\tDisplay a help screen");
                    Console.WriteLine("-c\t\t|\tDisplay a stack view of all shapes on the canvas");
                    Console.WriteLine("-e\t\t|\tExport the current canvas to an SVG file");
                    Console.WriteLine("-Q\t\t|\tClose the program");
                    Console.WriteLine("-undo\t\t|\tUndo the last command");
                    Console.WriteLine("-redo\t\t|\tRedo the last command that was undone");
                    Console.WriteLine("\nLong Commands\t\x10");
                    Console.WriteLine("\n--rect\t\t(x,y,width,height)\t\t|\tAdd a rectangle");
                    Console.WriteLine("--circle\t(cx,cy,r)\t\t\t|\tAdd a circle");
                    Console.WriteLine("--line\t\t(x1,y1,x2,y2)\t\t\t|\tAdd a line");
                    Console.WriteLine("--ellipse\t(cx,cy,rx,ry)\t\t\t|\tAdd an ellipse");
                    Console.WriteLine("--polyline\t(x1,y1,...xn,yn)\t\t|\tAdd a polyline");
                    Console.WriteLine("--polygon\t(x1,y1,...xn,yn)\t\t|\tAdd a polygone");
                    Console.WriteLine("--path\t\t(list of instructions)\t\t|\tAdd a path, for more info see -l");
                    Console.WriteLine("--shift\t\t(Unique ID,new stack_index)\t|\tMove the shapes at stack_index to the new stack_index");
                    Console.WriteLine("--update\t(Unique ID,x,y,w,h)\t\t|\tEdit a shape specified by it's ID");                    
                    Console.WriteLine("--delete\t(Unique ID)\t\t\t|\tDelete the shape specified by it's Unique ID");
                    Console.WriteLine("--canvas\t(width,height)\t\t\t|\tSet the new width and height of the canvas");
                    Console.WriteLine("--edit\t\t(Unique ID,fill,stroke,stroke-dasharray,stroke-width)\t|\tEdit a shape specified by it's ID");
                    Console.WriteLine("\nAll numerical data is of type double");
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    return 2;
                case "e":   //Export the file
                    System.IO.File.WriteAllText("./export/my_canvas.svg", canvas.GetSVG());
                    Console.WriteLine("Canvas has been saved to \"./exports/my_canvas.svg\"");
                    return 2;
                case "c":   //Display each shape on the canvas
                    Console.WriteLine($"-------------------------------\nCanvas is {canvas.width} x {canvas.height}\n");
                    int i = 0;
                    foreach(Shape obj in canvas.ReturnShapes())
                    {
                        Console.WriteLine(obj.GetDetails()+$"\nZ-Index: [{i}]\n");
                        i++;
                    }
                    Console.WriteLine("-------------------------------\n");
                    return 1;
                case "undo":    //Undo the previous command
                    if(index_of_current_invoke == 0)
                    {
                        //If we've issued no commands, cannot undo 
                        Console.WriteLine("Cannot Undo.\n");
                        return 2;
                    }
                    try
                    {
                        //Attempt to undo the last command
                        invoker.Unexecute(index_of_current_invoke);
                        index_of_current_invoke--;
                        return 1;
                    }
                    catch
                    {
                        Console.WriteLine("Failed to undo last command");
                        return 2;
                    }
                case "redo":    //Redo the last command that was undone
                    {
                        if(invoker.History.Count<=index_of_current_invoke)
                        {
                            //If we have no commands to redo, cannot redo
                           Console.WriteLine("No Commands to redo.");
                           return 2; 
                        }
                        try
                        {
                           //Attempt to redo the last undo 
                           invoker.Execute(index_of_current_invoke);
                           index_of_current_invoke++;
                           return 1;
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                            //Console.WriteLine("Failed to redo last undo.");
                            return 2;
                        }
                    }
                default:
                    return 0;
            }
        }
    }
}
