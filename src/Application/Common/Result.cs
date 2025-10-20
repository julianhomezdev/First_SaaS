namespace SaaS.src.Application.Common
{

    // Generic wrapper to manage success and failure responses


    // T represents the type of data being returned

    public class Result<T>
    {

        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        
        // Collection of errors to return in case of failure
        public List<string> Errors { get; set; } = new();


        // Create succcesful result 
        public static Result<T> Success (T data, string message = "Successful operation")
        {

            return new Result<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message

            };

        }

        // Create a failed result
        public static Result<T> Failure(string message, List<string> errors = null)
        {

            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                // If 'errors' is null, return and empty list
                Errors = errors ?? new List<string>()


            };

        }


    }
}
