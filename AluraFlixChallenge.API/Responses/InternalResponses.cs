namespace AluraFlixChallenge.API.Responses
{
    public class InternalResponses<T>
    { 
        public int Code { get; set; }
        public List<T>? Data { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
