using Microsoft.Extensions.Caching.Memory;
using QandA.Data.Models;

namespace QandA.Data
{
    public class QuestionCache : IQuestionCache
    {
        private MemoryCache _cache {  get; set; }

        private static string GetCacheKey(int questionId) => $"Question-{questionId}";

        public QuestionCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 100
            });
        }

        public QuestionGetSingleResponse Get(int questionId)
        {
            QuestionGetSingleResponse question;
            _cache.TryGetValue(questionId, out question);
            return question;
        }

        public void Set(QuestionGetSingleResponse question)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSize(1);
            _cache.Set(
                GetCacheKey(question.QuestionId),
                question,
                cacheEntryOptions);
        }

        public void Remove(int questionId)
        {
            _cache.Remove(GetCacheKey(questionId));
        }
    }
}
