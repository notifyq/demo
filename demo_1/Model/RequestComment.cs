using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class RequestComment
    {
        public int RequestCommentId { get; set; }
        public string CommentContent { get; set; } = null!;
        public int RequestId { get; set; }

        public virtual Request Request { get; set; } = null!;
    }
}
