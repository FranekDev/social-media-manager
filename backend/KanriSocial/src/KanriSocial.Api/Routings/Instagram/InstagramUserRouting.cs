// using KanriSocial.Application.Features.Instagram.User.Commands.CreateInstagramUser;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
//
// namespace KanriSocial.Api.Routings.Instagram;
//
// public static class InstagramUserRouting
// {
//     public static void MapInstagramUserEndpoints(this IEndpointRouteBuilder app)
//     {
//         app.MapPost("/instagram/user", async ([FromBody] CreateInstagramUserCommand command, ISender sender) =>
//         {
//             var result = await sender.Send(command);
//             if (result is null)
//             {
//                 Results.BadRequest("Could not create Instagram user");
//             }
//             
//             return Results.Ok(new { UserId = result });
//         });
//     }
// }