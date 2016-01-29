namespace StackTraceTool
{
    public class ParsedExceptionJson
    {
        public ErrorDetails ErrorDetails { get; set; }
    }

    public class ErrorDetails
    {
        public string ErrorMessage { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionDetail { get; set; }
        public bool UnexpectedException { get; set; }
    }
}