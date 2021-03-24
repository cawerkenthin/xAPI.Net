using Newtonsoft.Json.Linq;
using TinCan.Json;

//
// CAW: 8/10/2017 This entire file created.
//
namespace TinCan
{
    public class InteractionComponent : JsonModel
    {
        public string id;
        public LanguageMap description { get; set; }

        public InteractionComponent()
        {
        }

        public InteractionComponent(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                id = jobj.Value<string>("id");
            }
            if (jobj["description"] != null)
            {
                description = (LanguageMap)jobj.Value<JObject>("description");
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (id != null)
            {
                result.Add("id", id);
            }
            if (description != null && !description.isEmpty())
            {
                result.Add("description", description.ToJObject(version));
            }

            return result;
        }

        public static explicit operator InteractionComponent(JObject jobj)
        {
            return new InteractionComponent(jobj);
        }
    }
}
