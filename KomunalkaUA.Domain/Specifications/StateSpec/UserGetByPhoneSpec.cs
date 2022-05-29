using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.StateSpec;

public class UserGetByPhoneSpec:Specification<User>,ISingleResultSpecification<User>
{
    public UserGetByPhoneSpec(string phone)
    {
        Query
            .Where(x => x.PhoneNumber == phone);
    }
}