namespace dotnet_sort
{
    public class Apply
    {
        public Apply()
        {
            Path = ".";
            Sort = SortEnum.ALPHABETICALLY_ASCENDING;
            To = ApplyEnum.REFERENCES_IMPORTS;
        }
        public string Path { get; set; }
        public bool PathIsFile { get; set; }
        public ApplyEnum To { get; set; }
        public SortEnum Sort { get; set; }

        public override string ToString()
        {
            return $"Apply [Path={Path}, To={To}, Sort={Sort}]";
        }
    }
}