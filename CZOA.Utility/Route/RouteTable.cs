namespace CZOA._路由表
{
    //public struct _Route
    //{
    //    public const string _GlobalPrefix = "api/";
    //}
    public struct Test
    {
        public const string _Prefix = "api/test";

        public const string GetNewid = "newid";
        public const string GuidToDecimal = "{guid1}-guid2d19-{guid}";
        public const string GetDynamic = "dynamic";
        public const string GetTable = "table";
        public const string Get = "";
        public const string GetById = "{id}";

        public const string Post = "";
        public const string PostList = "addlist";
        public const string PostDynamic = "add";

        public const string Put = "edit";

        public const string Delete = "del/{id}";
    }

    public struct Tag
    {
        public const string _Prefix = "api/tag";

        public const string GetAllCategory = "category";
        public const string GetCategory = "category/{categoryid}";
        public const string GetTagByCategory = "~/api/tags/{categoryid}";
        public const string GetTag = "{tagid}";
        public const string GetTagItemByTag = "items/{tagid}";
        public const string GetTagItem = "item/{tagid}";

        public const string PostCategory = "category/add";
        public const string PostTag = "add";
        public const string PostTagItem = "item/add";

        public const string PutCategory = "category/edit";
        public const string PutTag = "edit";
        public const string PutTagItem = "item/edit";

        public const string DeleteCategory = "category/del/{categoryid}";
        public const string DeleteTag = "del/{tagid}";
        public const string DeleteTagItem = "item/del/{itemid}";
    }

    public struct Step
    {
        public const string _Prefix = "api/step";

        public const string SaveNewStep = "add";
        public const string SaveEditStep = "edit";
    }
}
