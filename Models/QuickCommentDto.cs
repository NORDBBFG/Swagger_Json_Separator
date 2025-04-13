using System;\n\nnamespace DonorApp.Models\n{\n    public class QuickCommentDto\n    {\n        public int Id { get; set; }\n        public string Comment { get; set; }\n    }\n\n    public enum QuickCommentCategory\n    {\n        ReasonsForCancellation = 1,\n        ExperienceComments = 2,\n        Other = 3\n    }\n\n    public class ContentResult\n    {\n        public string Content { get; set; }\n        public string ContentType { get; set; }\n        public int? StatusCode { get; set; }\n    }\n}