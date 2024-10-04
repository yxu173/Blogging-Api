using Domain.Entities;
using Nest;

namespace Application.Services;

public class ElasticsearchService(IElasticClient elasticClient)
{
    public async Task IndexPostAsync(Post post)
    {
        await elasticClient.IndexDocumentAsync(post);
    }

    public async Task<IEnumerable<Post>> SearchAsync(string query)
    {
        var response = await elasticClient.SearchAsync<Post>(s => s
            .Query(q => q
                .MultiMatch(m => m
                    .Fields(f => f
                        .Field(p => p.Title, 2)
                        .Field(p => p.Content)
                        .Field(p => p.PostTags.First().Tag.TagName)
                    )
                    .Query(query)
                )
            )
        );

        return response.Documents;
    }
}