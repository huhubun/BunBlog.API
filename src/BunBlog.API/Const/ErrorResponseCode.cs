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
        /// 找不到记录
        /// </summary>
        public const string RECORD_NOT_FOUND = nameof(RECORD_NOT_FOUND);

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

        /// <summary>
        /// 服务器端发生错误
        /// </summary>
        public const string SERVER_ERROR = nameof(SERVER_ERROR);

        /// <summary>
        /// 分类不存在
        /// </summary>
        public const string CATEGORY_NOT_EXISTS = nameof(CATEGORY_NOT_EXISTS);

        /// <summary>
        /// 标签不存在
        /// </summary>
        public const string TAG_NOT_EXISTS = nameof(TAG_NOT_EXISTS);

        /// <summary>
        /// 链接名称不存在
        /// </summary>
        public const string LINK_NAME_NOT_EXISTS = nameof(LINK_NAME_NOT_EXISTS);

        // 模型验证错误（例如缺少必填字段）
        public const string MODEL_VALIDATION_ERROR = nameof(MODEL_VALIDATION_ERROR);

        /// <summary>
        /// 无效的 Code
        /// </summary>
        public const string INVALID_CODE = nameof(INVALID_CODE);

        /// <summary>
        /// 无效的值（例如 Setting 定义中要求不为 null，但 value 却为 null）
        /// </summary>
        public const string INVALID_VALUE = nameof(INVALID_VALUE);

        /// <summary>
        /// 缺少必要的 Query String
        /// </summary>
        public const string MISSING_REQUIRED_QUERYSTRING = nameof(MISSING_REQUIRED_QUERYSTRING);
    }
}
