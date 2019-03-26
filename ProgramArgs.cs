using System.Collections.Generic;

namespace dotnet_sort
{
    public class ProgramArgs
    {
        public string[] Keys { get; set; }
        public int Count { get; set; }

        public string Value { get; set; }

        public ProgramArgs(string[] keys)
        {
            if (keys == null || keys.Length == 0)
                throw new System.ArgumentException("keys can't be null or empty");
            Keys = keys;
        }
    }
}