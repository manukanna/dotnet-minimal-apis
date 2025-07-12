using AutoMapper;
using DOTNETPROJECT.DTOs;
using DOTNETPROJECT.Entities;
using DOTNETPROJECT.Repositiries;
using Microsoft.AspNetCore.OutputCaching;

namespace DOTNETPROJECT.EndPoints
{
    public static class UserExpEndpoints
    {
        public static RouteGroupBuilder EndPointsExpUsers(this RouteGroupBuilder userExpEndPoints)
        {
            userExpEndPoints.MapPost("/", AddUserExp);
            return userExpEndPoints;
        }

        static async Task<IResult> AddUserExp(UserDtosExpEntity userDtosExpEntity, IUserCompanyexp userCompanyexp, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var userExpEntity = mapper.Map<UserExpEntity>(userDtosExpEntity);
            await userCompanyexp.CreateUserExp(userExpEntity);
             await outputCacheStore.EvictByTagAsync("Update-Users-Exp-List", default);
            return TypedResults.Created($"/userEntity/{userExpEntity.Id}", userExpEntity);
        }
    }
}