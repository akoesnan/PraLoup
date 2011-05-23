/*
 * Copyright 2010 Facebook, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Web.Script.Serialization;

namespace PraLoup.Utilities
{
    /// <summary>
    /// Represents an object encoded in JSON. Can be either a dictionary 
    /// mapping strings to other objects, an array of objects, or a single 
    /// object, which represents a scalar.
    /// </summary>
    public class DynamicJsonObject : DynamicObject
    {
        private IDictionary<string, object> Dictionary { get; set; }

        public DynamicJsonObject() { }

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public dynamic GetMember(string memberName) {
            if (this.Dictionary.ContainsKey(memberName))
            {
                return GetValue(this.Dictionary[memberName]);
            }
            else {
                return null;
            }

        }

        public override bool TryGetMember(GetMemberBinder binder, out dynamic item)
        {
            if (this.Dictionary.ContainsKey(binder.Name))
            {
                item = this.Dictionary[binder.Name];
                item = GetValue(item);
                return true;
            }
            else
            {
                item = null;
                return true;
            }
        }

        public static dynamic GetValue(dynamic item)
        {
            if (item is IDictionary<string, dynamic>)
            {
                item = new DynamicJsonObject(item as IDictionary<string, dynamic>);
            }
            else if (item is ArrayList && (item as ArrayList).Count > 0 && (item as ArrayList)[0] is IDictionary<string, dynamic>)
            {
                item = new DynamicJsonArray(item);
            }
            else if (item is ArrayList)
            {
                item = new List<dynamic>((item as ArrayList).ToArray());
            }
            return item;
        }
    }

    internal class DynamicJsonConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(object))
            {
                return new DynamicJsonObject(dictionary);
            }

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
        }
    }


}
