namespace LMSApi.Models
{
    public class RootObject<T>
    {

        public string OPERATION { get; set; }
        public string USER_ID { get; set; }
        public List<T> VALUES { get; set; }
    }
}
