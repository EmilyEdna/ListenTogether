﻿namespace ListenTogether.Model.Api.Response
{
    public class MusicResponse : MusicBase
    {
        /// <summary>
        /// 平台
        /// </summary>
        public int Platform { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName { get; set; } = null!;
        /// <summary>
        /// 对应平台的ID
        /// </summary>
        public string PlatformInnerId { get; set; } = null!;

        /// <summary>
        /// 扩展数据
        /// </summary>
        public string ExtendData { get; set; } = null!;
    }
}
