/*
    Copyright 2014 RISC, Inc.

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
*/
using Newtonsoft.Json.Linq;
using System;
using TinCan.Json;

namespace TinCan
{
    public class Attachment : JsonModel
    {
        public static readonly string OBJECT_TYPE = "Attachment";
        public string _usageType { get; set; }
        public string usageType
        {
            get { return _usageType; }
            set
            {
#pragma warning disable S1481 // Unused local variables should be removed  (Validation that string represents IRI)
                var uri = new Uri(value);       
                #pragma warning restore S1481 // Unused local variables should be removed
                _usageType = value;
            }
        }
        public LanguageMap display { get; set; }
        public LanguageMap description { get; set; }
        public string contentType { get; set; }
        public int length { get; set; }
        public string fileUrl { get; set; }
        public string sha2 { get; set; }

        public Attachment(StringOfJSON json) : this(json.toJObject()) { }

        public Attachment(JObject jobj)
        {
            if (jobj["usageType"] != null)
            {
                usageType = (string)jobj["usageType"];
            }

            if (jobj["contentType"] != null)
            {
                contentType = (string)jobj["contentType"];
            }

            if (jobj["length"] != null)
            {
                length = (int)jobj["length"];
            }

            if (jobj["sha2"] != null)
            {
                sha2 = (string)jobj["sha2"];
            }

            if (jobj["fileUrl"] != null)
            {
                fileUrl = (string)jobj["fileUrl"];
            }

            if (jobj["display"] != null)
            {
                display = (LanguageMap) jobj.Value<JObject>("display");
            }
            
            if (jobj["description"] != null)
            {
                description = (LanguageMap)jobj.Value<JObject>("definition");
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var attachment = new JObject
            {
                {"usageType", usageType},
                {"display", display.ToJObject(version)},
                {"contentType", contentType},
                {"length", length},
                {"sha2", sha2}
            };

            if (description != null && !description.isEmpty())
            {
                attachment.Add("description", description.ToJObject(version));
            }

            if (!string.IsNullOrWhiteSpace(fileUrl))
            {
                attachment.Add("fileUrl", fileUrl);
            }

            return attachment;
        }

        public static explicit operator Attachment(JObject jobj)
        {
            return new Attachment(jobj);
        }
    }
}
