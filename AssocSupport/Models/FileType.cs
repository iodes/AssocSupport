namespace AssocSupport.Models
{
    public class FileType
    {
        /// <summary>
        /// 확장자 이름을 가져오거나 설정합니다.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 콘텐츠 형식을 가져오거나 설정합니다.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 파일 인식 유형을 가져오거나 설정합니다.
        /// 본 속성은 Vista 이상에서만 유효합니다.
        /// </summary>
        public PerceivedTypes PerceivedType { get; set; }
    }
}
