using BookStore.Core.Enum;

namespace BookStore.Core.FilterModel
{
	public class BookFilterModel
	{
        public string KeyWord { get; set; }
        public int? IdCategory { get; set; }
        public int? IdAuthor { get; set; }
        public TypeOrderBy? OrderBy { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? Skip { get; set; }
        public int? Offset { get; set; }
    }
}
