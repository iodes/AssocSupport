﻿namespace AssocSupport.Models
{
    public class ShellCommand
    {
        /// <summary>
        /// 커멘드 경로를 가져오거나 설정합니다.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 커멘드 인자를 가져오거나 설정합니다.
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// 커멘드 액션을 가져오거나 설정합니다.
        /// </summary>
        public string Action { get; set; }
    }
}
