using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

public interface ITrendyolQnAApi
{
    /// <summary>
    ///     Lists customer questions. Without dates the last week is returned; the date range is limited to two weeks.
    ///     Rate limit: 1000 requests/minute.
    /// </summary>
    [Get("/integration/qna/sellers/SELLER_ID_PARAMETER/questions/filter")]
    Task<IApiResponse<TrendyolResponseGetQuestions>> GetQuestionsAsync([Query] TrendyolFilterGetQuestions filter);

    /// <summary>Gets a single question by id. Rate limit: 1000 requests/minute.</summary>
    [Get("/integration/qna/sellers/SELLER_ID_PARAMETER/questions/{id}")]
    Task<IApiResponse<TrendyolQuestion>> GetQuestionAsync([AliasAs("id")] long id);

    /// <summary>
    ///     Answers a WAITING_FOR_ANSWER question (10 to 2000 characters, checked for banned words). Rate limit: 500
    ///     requests/minute.
    /// </summary>
    [Post("/integration/qna/sellers/SELLER_ID_PARAMETER/questions/{id}/answers")]
    Task<IApiResponse<TrendyolResponseAnswerQuestion>> AnswerQuestionAsync([AliasAs("id")] long id, [Body] TrendyolRequestAnswerQuestion request);

    /// <summary>STAGE ONLY: creates a customer question on a product so the Q&amp;A services can be tested.</summary>
    [Post("/integration/qna/sellers/SELLER_ID_PARAMETER/questions")]
    Task<IApiResponse<TrendyolResponseCreateTestQuestion>> CreateTestQuestionAsync([Body] TrendyolRequestCreateTestQuestion request);
}
