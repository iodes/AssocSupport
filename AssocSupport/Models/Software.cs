using System.Collections.Generic;

namespace AssocSupport.Models
{
    public class Software
    {
        /// <summary>
        /// 소프트웨어의 이름을 가져오거나 설정합니다.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 소프트웨어의 아이콘을 가져오거나 설정합니다.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 소프트웨어의 제조사 이름을 가져오거나 설정합니다.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 소프트웨어의 설명을 가져오거나 설정합니다.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 연결된 식별자 컬렉션을 가져옵니다.
        /// </summary>
        public List<ProgrammaticID> Identifiers { get; } = new List<ProgrammaticID>();
    }
}
