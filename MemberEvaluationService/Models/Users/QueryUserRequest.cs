using MemberEvaluationService.Models.Pagination;

namespace MemberEvaluationService.Models.Users
{
    public class QueryUserRequest : PaginationParameters
    {
        public string FullName { get; set; }

    }
}
