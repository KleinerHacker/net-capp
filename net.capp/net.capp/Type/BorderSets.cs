namespace net.capp.Type
{
    public static class BorderSets
    {
        public static BorderSet SingleLineBorder { get; } = new BorderSet('\xC4', '\xB3', '\xDA', '\xBF', '\xC0', '\xD9', 1);
        public static BorderSet DoubleLineBorder { get; } = new BorderSet('\xCD', '\xBA', '\xC9', '\xBB', '\xC8', '\xBC', 1);
        public static BorderSet SimpleBorder { get; } = new BorderSet('-', '|', '+', 1);
        public static BorderSet StarBorder { get; } = new BorderSet('*', 1);
        public static BorderSet EmptyBorder { get; } = new BorderSet(' ', 0);
    }
}