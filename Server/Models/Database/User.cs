using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TodoApp.Models.Database;

[Table("user")]
public sealed class User : IdentityUser;