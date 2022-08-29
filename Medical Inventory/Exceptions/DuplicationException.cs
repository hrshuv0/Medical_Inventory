namespace Medical_Inventory.Exceptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string name) : base($"{name} already Exists")
        {

        }
    }
}
