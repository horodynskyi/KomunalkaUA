using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.StateSpec;

public sealed class StateGetByUserId:Specification<State>,ISingleResultSpecification<State>
{
    public StateGetByUserId(long id)
    {
        Query.Where(x => x.UserId == id);
    }
}