using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.StateSpec;

public class StateDeleteById:Specification<State>,ISingleResultSpecification<State>
{
    public StateDeleteById(Guid Id)
    {
        
    }
}