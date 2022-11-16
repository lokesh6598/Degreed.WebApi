using Newtonsoft.Json;

namespace Degreed.WebApi.Model
{
    public class JsonResponse
    {       
        public int StatusCode { get; set; }
        public string Message { get; set; }     
        public dynamic Result { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
