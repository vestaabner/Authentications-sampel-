namespace BackOffice.Application.CustomeException
{
    public class CustomException : Exception
    {
        public CustomException() : base("My error occured")
        {

        }
        public CustomException(string message) : base(message)
        {

        }
        public CustomException(Exception exception) : this(exception.Message)
        {

        }


    }
}

