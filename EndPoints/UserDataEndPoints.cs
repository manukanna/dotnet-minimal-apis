using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DOTNETPROJECT.DTOs;
using DOTNETPROJECT.Entities;
using DOTNETPROJECT.Repositiries;
using Microsoft.AspNetCore.OutputCaching;

namespace DOTNETPROJECT.EndPoints
{
    public static class UserDataEndPoints
    {
        public static RouteGroupBuilder EndPointsUserData(this RouteGroupBuilder userDataEndPoints)
        {
            userDataEndPoints.MapPost("/", AddUserEntity);
            userDataEndPoints.MapGet("/", GetAllUserEntity).CacheOutput((item) => item.Expire(TimeSpan.FromSeconds(60)).Tag("Update-Users-List"));
            userDataEndPoints.MapGet("/{id:int}", GetUserEntity);
            userDataEndPoints.MapPut("/{id:int}", UpdateUserEntity);
            userDataEndPoints.MapDelete("/{id:int}", DeleteUserData);
            return userDataEndPoints;
        }

        static async Task<IResult> AddUserEntity(UserDataDtos userDataDtos, IUserData userData, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var userEntity = mapper.Map<UserEntity>(userDataDtos);
            await userData.Create(userEntity);
            await outputCacheStore.EvictByTagAsync("Update-Users-List", default);
            return TypedResults.Created($"/userEntity/{userEntity.Id}", userEntity);
        }
        
        static async Task<IResult> GetAllUserEntity(IUserData userData, IMapper mapper)
        {
            var allEntities = await userData.GetAll();
            var allUserEntites = mapper.Map<List<UserDataDtosId>>(allEntities);
            return Results.Ok(allUserEntites);
        }

        static async Task<IResult> GetUserEntity(int id, IUserData userData, IMapper mapper)
        {
            var userEntity = await userData.GetById(id);

            if (userEntity is null)
            {
                return Results.NotFound(new { message = "User not found." });
            }

            var userEntityData = mapper.Map<UserDataDtos>(userEntity);
            return Results.Ok(userEntityData);
        }

        static async Task<IResult> DeleteUserData(int id, IUserData userData, IOutputCacheStore outputCacheStore)
        {
            var exists = await userData.Exists(id);
            if (!exists)
            {
                return Results.NotFound(new { message = "User not found" });
            }

            await userData.Delete(id);
            await outputCacheStore.EvictByTagAsync("Update-Users-List", default);
            return Results.Ok(new { message = "Deleted Successfully" });
        }

        static async Task<IResult> UpdateUserEntity(int id, UserDataDtosId userDataDtos, IUserData userData, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var exists = await userData.Exists(id);
            if (!exists)
            {
                return Results.NotFound(new { message = "User not found." });
            }

            var userEntity = mapper.Map<UserEntity>(userDataDtos);
            userEntity.Id = id;
            await userData.Update(userEntity);
            await outputCacheStore.EvictByTagAsync("Update-Users-List", default);
            return Results.Ok(new { message = "Upaded Successfully" });
        }
    }
}