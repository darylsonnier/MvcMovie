namespace MvcMovie.Models
{
    /// <summary>
    /// The ErrorViewModel class holds data for controllers in an error state.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// The RequestID field is the id for an errored request.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// The ShowRequestId method returns true if the RequestID is not null.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
