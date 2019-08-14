namespace BunBlog.API.Const
{
    public static class ErrorResponseCode
    {
        /// <summary>
        /// 错误的用户名或密码
        /// </summary>
        public const string WRONG_USERNAME_OR_PASSWORD = nameof(WRONG_USERNAME_OR_PASSWORD);

        /// <summary>
        /// 根据 ID 无法找到记录
        /// </summary>
        public const string ID_NOT_FOUND = nameof(ID_NOT_FOUND);

        /// <summary>
        /// 链接名称已经存在（用于带有 LinkName 的配置，如分类、标签等）
        /// </summary>
        public const string LINK_NAME_ALREADY_EXISTS = nameof(LINK_NAME_ALREADY_EXISTS);

        /// <summary>
        /// 显示名称已经存在（用于带有 DisplayName 的配置，如分类、标签等）
        /// </summary>
        public const string DISPLAY_NAME_ALREADY_EXISTS = nameof(DISPLAY_NAME_ALREADY_EXISTS);

        /// <summary>
        /// 使用中（因为有其它引用导致的操作失败）
        /// </summary>
        public const string IN_USE = nameof(IN_USE);

        /// <summary>
        /// 无效的 grant_type
        /// </summary>
        public const string INVALID_GRANT_TYPE = nameof(INVALID_GRANT_TYPE);

    }
}
