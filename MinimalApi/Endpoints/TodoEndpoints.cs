﻿using TodoLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MinimalApi.Endpoints;

public static class TodoEndpoints
{
    public static void AddTodoEndpoints(this WebApplication app)
    {
        app.MapGet("/api/Todos", GetAllTodos);

        app.MapPost("/api/Todos", CreateTodo).RequireAuthorization();

        app.MapDelete("/api/Todos/{id}", DeleteTodo).RequireAuthorization();
    }

    [Authorize]
    private async static Task<IResult> GetAllTodos(ITodoData data)
    {
        var output = await data.GetAllAssigned(1);
        return Results.Ok(output);
    }

    private async static Task<IResult> CreateTodo(ITodoData data, [FromBody] string task)
    {
        var output = await data.Create(1, task);
        return Results.Ok(output);
    }

    private async static Task<IResult> DeleteTodo(ITodoData data, int id)
    {
        await data.Delete(1, id);
        return Results.Ok();
    }
}
