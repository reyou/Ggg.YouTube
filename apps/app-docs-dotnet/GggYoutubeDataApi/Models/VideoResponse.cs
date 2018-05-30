using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace GggYoutubeDataApi.Models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
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