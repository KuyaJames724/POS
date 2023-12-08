using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using LoginModel.Models;
[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly _DbContext _context; 
    private readonly IConfiguration _configuration; 

    public SecurityController(_DbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] Login model)
    {
        var tokenService = new TokenService(_configuration, _context);
        var token = tokenService.GenerateToken(model);

        if (token == null)
        {
            return Unauthorized();
        }
        
        return Ok(new { token });
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpGet("admin-endpoint")]
    public IActionResult AdminEndpoint()
    {
        if (User.IsInRole("Admin"))
        {
            // Admin-specific logic here
            return Ok("Welcome, Admin!");
        }

         else
        {
            return Forbid(); // Return a 403 Forbidden response if the user is not authorized.
        }

    }

    [Authorize(Policy = "UserPolicy")]
    [HttpGet("user-endpoint")]
    public IActionResult UserEndpoint()
    {
        // Check if the current user is a user
        if (User.IsInRole("User"))
        {
        // User-specific logic here
            return Ok("Welcome, User!");
        }
        else
        {
            return Forbid(); // Return a 403 Forbidden response if the user is not authorized.
        }
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpDelete("products/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);

        if (product == null)
        {
            return NotFound(); // Return a 404 Not Found response if the product is not found
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        return NoContent(); // Return a 204 No Content response if the product is successfully deleted
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpDelete("users/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
        {
            return NotFound(); // Return a 404 Not Found response if the user is not found
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent(); // Return a 204 No Content response if the user is successfully deleted
    }
}