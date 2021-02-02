namespace Assignment04
{
    interface Shape
    {
        void SetId(int id);

        int GetId();

        string GetDetails();

        string[] Update(string[] new_data);

        string[] Decorations(string[] new_data);

        string GetType();

        string Embed();
    }
}
