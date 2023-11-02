namespace EmailsParser.Exceptions
{
    public class PathNotFoundException : Exception
    {
        public PathNotFoundException()
        {
        }

        public override string Message => "Path cannot be null or empty!";
    }
}
