namespace AluraFlixChallenge.API.Responses
{
    public class InternalResponses
    { 
        public int Code { get; set; }
        public object Data { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
