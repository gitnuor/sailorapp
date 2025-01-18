using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace EPYSLSAILORAPP.Domain.Entity.General
{
    public class NestableElement
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonIgnore]
        public int ParentId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonIgnore]
        public string PageName { get; set; }

        [JsonIgnore]
        public int SeqNo { get; set; }

        [JsonProperty("children")]
        public List<NestableElement> Children;

        public NestableElement()
        {
            Children = new List<NestableElement>();
        }
    }
}
