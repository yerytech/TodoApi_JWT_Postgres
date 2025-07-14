namespace TodoApi.Models;

public class TodoTask
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public bool Completed { get; set; } = false;

  public int UserId { get; set; }
  public User? User{ get; set; }
}