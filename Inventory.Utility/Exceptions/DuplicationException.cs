namespace Inventory.Utility.Exceptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string name) : base($"{name} already Exists")
        {

        }
    }
}
