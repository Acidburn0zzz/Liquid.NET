﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liquid.NET.Constants;

namespace Liquid.NET.Filters.Strings
{
    public class TruncateWordsFilter : FilterExpression<StringValue, StringValue>
    {
        private readonly NumericValue _length;
        private readonly StringValue _truncateString;

        public TruncateWordsFilter(NumericValue length, StringValue truncateString)
        {
            _length = length;
            _truncateString = truncateString.IsUndefined ? new StringValue("...") : truncateString;
        }
        public override StringValue ApplyTo(StringValue objectExpression)
        {
            return StringUtils.Eval(objectExpression, TruncateWords);
            
        }

        private string TruncateWords(string s)
        {
            if (s == null)
            {
                return "";
            }
            var words = s.Split(new [] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length <= _length.IntValue)
            {
                return s;
            }
            // this removes rendundant spaces.
            return String.Join(" ", words.Take(_length.IntValue)) + _truncateString.StringVal;
        }
    }
}
