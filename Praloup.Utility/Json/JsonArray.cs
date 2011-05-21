using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Collections;

namespace PraLoup.Utilities
{
    public class DynamicJsonArray : DynamicObject, IEnumerable<DynamicJsonObject>
    {
        IList<DynamicJsonObject> array;

        public DynamicJsonArray(ArrayList item)
        {
            this.array = new List<DynamicJsonObject>((item as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, dynamic>)));
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var i = (int)indexes[0];
            if (i < 0 || i > array.Count())
                result = null;
            else
            {
                result = DynamicJsonObject.GetValue(array[i]);                
            }
            return true;
        }

        public IEnumerator<DynamicJsonObject> GetEnumerator()
        {
            return new DynamicJsonArrayEnumerator(array.GetEnumerator());
        }

        public class DynamicJsonArrayEnumerator : IEnumerator<DynamicJsonObject>
        {
            IEnumerator<DynamicJsonObject> enumerator;

            public DynamicJsonArrayEnumerator(IEnumerator<DynamicJsonObject> enumerator)
            {
                this.enumerator = enumerator;
            }

            public DynamicJsonObject Current
            {
                get { return DynamicJsonObject.GetValue(enumerator.Current); }
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public void Reset()
            {
                enumerator.Reset();
            }

            public void Dispose()
            {
                this.enumerator.Dispose();
            }

            object IEnumerator.Current
            {
                get { return DynamicJsonObject.GetValue(enumerator.Current); }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DynamicJsonArrayEnumerator(array.GetEnumerator());
        }
    }
}
