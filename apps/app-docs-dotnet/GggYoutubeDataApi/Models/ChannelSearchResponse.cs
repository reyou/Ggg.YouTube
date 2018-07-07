using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GggYoutubeDataApi.Models
{
    public class ChannelSearchResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string regionCode { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<ChannelItem> items { get; set; }

        public static ChannelSearchResponse FromString(string result)
        {
            ChannelSearchResponse channelSearchResponse = JsonConvert.DeserializeObject<ChannelSearchResponse>(result);
            return channelSearchResponse;
        }

        public string Serialize()
        {
            string serializeObject = JsonConvert.SerializeObject(this, Formatting.Indented);
            return serializeObject;
        }
    }
}
