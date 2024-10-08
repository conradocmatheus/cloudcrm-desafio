using back_end.CustomActionFilters;
using back_end.DTOs.UserDTOs;
using back_end.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Cria um novo usuário.
    /// </summary>
    /// <param name="createUserDto">Dados do usuário a ser criado. (atente-se ao formato de data ano-mes-dia)</param>
    /// <returns>Um objeto de usuário criado.</returns>
    /// <response code="201">Usuário criado com sucesso.</response>
    /// <response code="400">Dados inválidos fornecidos.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpPost]
    [ValidadeModel]
    [Route("")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var userDto = await userService.CreateUserAsync(createUserDto);
        
        return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
    }

    /// <summary>
    /// Apaga um usuário por ID.
    /// </summary>
    /// <param name="id">ID do usuário a ser apagado.</param>
    /// <returns>O usuário apagado.</returns>
    /// <response code="200">Usuário apagado com sucesso.</response>
    /// <response code="404">Usuário não encontrado.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
    {
        var deletedUser = await userService.DeleteUserByIdAsync(id);

        if (deletedUser == null)
        {
            return NotFound($"Usuário com ID {id} não encontrado.");
        }

        return Ok(deletedUser);
    }

    /// <summary>
    /// Retorna os usuários em ordem de criação.
    /// </summary>
    /// <returns>Lista de usuários ordenados pela data de criação.</returns>
    /// <response code="200">Lista de usuários retornada com sucesso.</response>
    /// <response code="404">Nenhum usuário encontrado.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpGet]
    [Route("by-creation-date")]
    public async Task<IActionResult> GetUsersByCreatedAt()
    {
        var userCreatedAtDto = await userService.GetUsersByCreatedAtAsync();
        
        if (userCreatedAtDto.Count == 0)
        {
            return NotFound("Nenhum usuário foi encontrado");
        }

        return Ok(userCreatedAtDto);
    }

    /// <summary>
    /// Retorna um usuário por ID.
    /// </summary>
    /// <param name="id">ID do usuário a ser buscado.</param>
    /// <returns>O usuário correspondente ao ID fornecido.</returns>
    /// <response code="200">Usuário encontrado e retornado com sucesso.</response>
    /// <response code="404">Usuário não encontrado.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var selectedUser = await userService.GetUserByIdAsync(id);
        
        if (selectedUser == null)
        {
            return NotFound($"Usuário com ID {id} não foi encontrado");
        }

        return Ok(selectedUser);
    }
}