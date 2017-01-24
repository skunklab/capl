/*
Claims Authorization Policy Langugage SDK ver. 1.0
 
Copyright (c) Matt Long labskunk@gmail.com
 
All rights reserved.
 
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the ""Software""), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace Capl.Authorization.Matching
{
    using System.Collections;
    using System.Collections.Generic;

    public class MatchExpressionDictionary : IDictionary<string, MatchExpression>
    {
        public MatchExpressionDictionary()
        {
            this.expressions = new Dictionary<string, MatchExpression>();
        }

        private Dictionary<string, MatchExpression> expressions;


        #region IDictionary<string,MatchExpression> Members

        /// <summary>
        /// Adds a new match expression.
        /// </summary>
        /// <param name="key">The key that identifies the match expression.</param>
        /// <param name="value">The match expression instance.</param>
        public void Add(string key, MatchExpression value)
        {
            expressions.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return expressions.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return expressions.Keys; }
        }

        public bool Remove(string key)
        {
            return expressions.Remove(key);
        }

        public bool TryGetValue(string key, out MatchExpression value)
        {
            return expressions.TryGetValue(key, out value);
        }

        public ICollection<MatchExpression> Values
        {
            get { return expressions.Values; }
        }

        public MatchExpression this[string key]
        {
            get { return expressions[key]; }
            set { expressions[key] = value; }
        }

        #endregion

        #region ICollection<KeyValuePair<string,MatchExpression>> Members

        public void Add(KeyValuePair<string, MatchExpression> item)
        {
            ((ICollection<KeyValuePair<string, MatchExpression>>)expressions).Add(item);
        }

        public void Clear()
        {
            expressions.Clear();
        }

        public bool Contains(KeyValuePair<string, MatchExpression> item)
        {
            return ((ICollection<KeyValuePair<string, MatchExpression>>)expressions).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, MatchExpression>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, MatchExpression>>)expressions).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return expressions.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, MatchExpression> item)
        {
            return ((ICollection<KeyValuePair<string, MatchExpression>>)expressions).Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,MatchExpression>> Members

        public IEnumerator<KeyValuePair<string, MatchExpression>> GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<string, MatchExpression>>)expressions.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return expressions.GetEnumerator();
        }

        #endregion
    }
}
