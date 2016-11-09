/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2015/8/21
* Description: Automated building by service@jucheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace JuCheap.Web
{
    public class ParseMaster
    {
        // used to determine nesting levels  
        Regex GROUPS = new Regex("\\("),
            SUB_REPLACE = new Regex("\\$"),
            INDEXED = new Regex("^\\$\\d+$"),
            ESCAPE = new Regex("\\\\."),
            QUOTE = new Regex("'"),
            DELETED = new Regex("\\x01[^\\x01]*\\x01");

        /// <summary>
        /// Delegate to call when a regular expression is found.  
        /// Use match.Groups[offset + <group number=""/>].Value to get  
        /// the correct subexpression 
        /// </summary>
        /// <param name="match"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public delegate string MatchGroupEvaluator(Match match, int offset);

        private string DELETE(Match match, int offset)
        {
            return "\x01" + match.Groups[offset].Value + "\x01";
        }

        private bool ignoreCase;
        private char escapeChar = '\0';

        ///   
        /// Ignore Case?  
        ///   
        public bool IgnoreCase
        {
            get { return ignoreCase; }
            set { ignoreCase = value; }
        }

        ///   
        /// Escape Character to use  
        ///   
        public char EscapeChar
        {
            get { return escapeChar; }
            set { escapeChar = value; }
        }

        ///   
        /// Add an expression to be deleted  
        ///   
        /// Regular Expression String  
        public void Add(string expression)
        {
            Add(expression, string.Empty);
        }

        ///   
        /// Add an expression to be replaced with the replacement string  
        ///   
        /// Regular Expression String  
        /// Replacement String. Use $1, $2, etc. for groups  
        public void Add(string expression, string replacement)
        {
            if (replacement == string.Empty)
                add(expression, new MatchGroupEvaluator(DELETE));

            add(expression, replacement);
        }

        ///   
        /// Add an expression to be replaced using a callback function  
        ///   
        /// Regular expression string  
        /// Callback function  
        public void Add(string expression, MatchGroupEvaluator replacement)
        {
            add(expression, replacement);
        }

        ///   
        /// Executes the parser  
        ///   
        /// input string  
        /// parsed string  
        public string Exec(string input)
        {
            return DELETED.Replace(unescape(getPatterns().Replace(escape(input), replacement)), string.Empty);
            //long way for debugging  
            /*input = escape(input); 
            Regex patterns = getPatterns(); 
            input = patterns.Replace(input, new MatchEvaluator(replacement)); 
            input = DELETED.Replace(input, string.Empty); 
            return input;*/
        }

        ArrayList patterns = new ArrayList();
        private void add(string expression, object replacement)
        {
            Pattern pattern = new Pattern();
            pattern.expression = expression;
            pattern.replacement = replacement;
            //count the number of sub-expressions  
            // - add 1 because each group is itself a sub-expression  
            pattern.length = GROUPS.Matches(internalEscape(expression)).Count + 1;

            //does the pattern deal with sup-expressions?  
            if (replacement is string && SUB_REPLACE.IsMatch((string)replacement))
            {
                string sreplacement = (string)replacement;
                // a simple lookup (e.g. $2)  
                if (INDEXED.IsMatch(sreplacement))
                {
                    pattern.replacement = int.Parse(sreplacement.Substring(1)) - 1;
                }
            }

            patterns.Add(pattern);
        }

        ///   
        /// builds the patterns into a single regular expression  
        ///   
        ///   
        private Regex getPatterns()
        {
            StringBuilder rtrn = new StringBuilder(string.Empty);
            foreach (object pattern in patterns)
            {
                rtrn.Append(((Pattern)pattern) + "|");
            }
            rtrn.Remove(rtrn.Length - 1, 1);
            return new Regex(rtrn.ToString(), ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        ///   
        /// Global replacement function. Called once for each match found  
        ///   
        /// Match found  
        private string replacement(Match match)
        {
            int i = 1, j = 0;
            Pattern pattern;
            //loop through the patterns  
            while (!((pattern = (Pattern)patterns[j++]) == null))
            {
                //do we have a result?  
                if (match.Groups[i].Value != string.Empty)
                {
                    object replacement = pattern.replacement;
                    if (replacement is MatchGroupEvaluator)
                    {
                        return ((MatchGroupEvaluator)replacement)(match, i);
                    }
                    else if (replacement is int)
                    {
                        return match.Groups[(int)replacement + i].Value;
                    }
                    else
                    {
                        //string, send to interpreter  
                        return replacementString(match, i, (string)replacement, pattern.length);
                    }
                }
                else //skip over references to sub-expressions  
                    i += pattern.length;
            }
            return match.Value; //should never be hit, but you never know  
        }

        ///   
        /// Replacement function for complicated lookups (e.g. Hello $3 $2)  
        ///   
        private string replacementString(Match match, int offset, string replacement, int length)
        {
            while (length > 0)
            {
                replacement = replacement.Replace("$" + length--, match.Groups[offset + length].Value);
            }
            return replacement;
        }

        private StringCollection escaped = new StringCollection();

        //encode escaped characters  
        private string escape(string str)
        {
            if (escapeChar == '\0')
                return str;
            Regex escaping = new Regex("\\\\(.)");
            return escaping.Replace(str, escapeMatch);
        }

        private string escapeMatch(Match match)
        {
            escaped.Add(match.Groups[1].Value);
            return "\\";
        }

        //decode escaped characters  
        private int unescapeIndex;
        private string unescape(string str)
        {
            if (escapeChar == '\0')
                return str;
            Regex unescaping = new Regex("\\" + escapeChar);
            return unescaping.Replace(str, unescapeMatch);
        }

        private string unescapeMatch(Match match)
        {
            return "\\" + escaped[unescapeIndex++];
        }

        private string internalEscape(string str)
        {
            return ESCAPE.Replace(str, "");
        }

        //subclass for each pattern  
        private class Pattern
        {
            public string expression;
            public object replacement;
            public int length;

            public override string ToString()
            {
                return "(" + expression + ")";
            }
        }
    }
}