using Newtonsoft.Json;
using System.Collections.Generic;

namespace GggYoutubeDataApi
{
    public class VideoResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<Item> items { get; set; }
        public static VideoResponse FromString(string result)
        {
            VideoResponse deserializeObject = JsonConvert.DeserializeObject<VideoResponse>(result);
            return deserializeObject;
        }

        public string ToJsonString()
        {
            string serializeObject = JsonConvert.SerializeObject(this);
            return serializeObject;
        }


    }
}