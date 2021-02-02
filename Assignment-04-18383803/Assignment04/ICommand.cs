namespace Assignment04
{
    /*  This is the Interface that all Command Classes implemnent    */
    interface ICommand
    {
        public void execute();  //Execute the Command 

        public void unexecute();    //Unexecute the Command
    }
}
