namespace ProjectManagement.API.Contracts.Common
{
    public class ResponseModel<T>
    {
        public ResponseModel(Result<T> resultDto)
        {
            Status = resultDto.Status;
            Payload = resultDto.Data;
        }
        public ResponseModel(StatusCode status, T payload = default(T), string statusMessage = null)
        {
            Status = status;
            StatusMessage = statusMessage;
            Payload = payload;
        }

        public bool IsSuccess => ((int)Status) > 0;
        public StatusCode Status { get; set; }
        public string StatusMessage { get; set; }
        public T Payload { get; set; }
    }
}
