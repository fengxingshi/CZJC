namespace CZOA.Tag
{
    public partial struct UserTag
    {
        public struct Domain
        {
            public const decimal 处室域 = 3011;
            public const decimal 角色域 = 3012;
        }
        public struct State
        {
            public const decimal  停用= 0;
            public const decimal  启用= 1;
        }
        public struct Type
        {
            public const decimal 系统管理 = 3030;
            public const decimal 单位管理 = 3031;
            public const decimal 业务管理 = 3032;
            public const decimal 业务办理 = 3033;
        }
    }
}
