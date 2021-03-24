/*
    Copyright 2014 Rustici Software

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
    public abstract class StatementBase : JsonModel
    {
        private const string ISODateTimeFormat = "o";

        public Agent actor { get; set; }
        public Verb verb { get; set; }
        public StatementTarget target { get; set; }
        public Result result { get; set; }
        public Context context { get; set; }
        public Nullable<DateTime> timestamp { get; set; }

        public StatementBase() { }
        public StatementBase(StringOfJSON json) : this(json.toJObject()) { }

        public StatementBase(JObject jobj)
        {
            if (jobj["actor"] != null)
            {
                if (jobj["actor"]["objectType"] != null && (string)jobj["actor"]["objectType"] == Group.OBJECT_TYPE)
                {
                    actor = (Group)jobj.Value<JObject>("actor");
                }
                else
                {
                    actor = (Agent)jobj.Value<JObject>("actor");
                }
            }
            if (jobj["verb"] != null)
            {
                verb = (Verb)jobj.Value<JObject>("verb");
            }
            if (jobj["object"] != null)
            {
                if (jobj["object"]["objectType"] != null)
                {
                    if ((string)jobj["object"]["objectType"] == Group.OBJECT_TYPE)
                    {
                        target = (Group)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == Agent.OBJECT_TYPE)
                    {
                        target = (Agent)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == Activity.OBJECT_TYPE)
                    {
                        target = (Activity)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == StatementRef.OBJECT_TYPE)
                    {
                        target = (StatementRef)jobj.Value<JObject>("object");
                    }
                }
                else
                {
                    target = (Activity)jobj.Value<JObject>("object");
                }
            }
            if (jobj["result"] != null)
            {
                result = (Result)jobj.Value<JObject>("result");
            }
            if (jobj["context"] != null)
            {
                context = (Context)jobj.Value<JObject>("context");
            }

            if (jobj["timestamp"] != null)
            {
                timestamp = jobj.Value<DateTime>("timestamp");
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result_ = new JObject();

            if (actor != null)
            {
                result_.Add("actor", actor.ToJObject(version));
            }

            if (verb != null)
            {
                result_.Add("verb", verb.ToJObject(version));
            }

            if (target != null)
            {
                result_.Add("object", target.ToJObject(version));
            }
            if (this.result != null)
            {
                result_.Add("result", this.result.ToJObject(version));
            }
            if (this.context != null)
            {
                result_.Add("context", context.ToJObject(version));
            }
            if (timestamp != null)
            {
                result_.Add("timestamp", timestamp.Value.ToString(ISODateTimeFormat));
            }

            return result_;
        }
    }
}
