using Ardalis.Specification;
using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.StateSpec;

public sealed class StateGetByUserIdAndStateTypeNotNone:Specification<State>,ISingleResultSpecification<State>
{
    public StateGetByUserIdAndStateTypeNotNone(long id)
    {
        Query.Where(x => x.UserId == id && x.StateType != StateType.None);
    }
}