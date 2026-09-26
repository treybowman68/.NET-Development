using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;
[ApiController]
[Route("[controller]")]
public class ContosoPizzacontroller : ControllerBase
{


    private readonly ILogger<ContosoPizzacontroller> _logger;

    public ContosoPizzacontroller(ILogger<ContosoPizzacontroller> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetPizza")]
    public IEnumerable<Pizza> Get()
    {
        // return Enumerable.Range(1, 5).Select(index => new Pizza
        // {
        //     Id = index,
        //     Name = $"Pizza {index}",
        //     Description = $"Description for Pizza {index}",
        //     Price = Random.Shared.Next(5, 20)
        // })
        // .ToArray();
        List<Pizza> pizzas = [new Pizza { Id = 1, Name = "Cheese", Description = "Classic pizza with tomato sauce and mozzarella cheese", Price = 8.99m },
                              new Pizza { Id = 2, Name = "Pepperoni", Description = "Pizza topped with pepperoni slices", Price = 9.99m },
                              new Pizza { Id = 3, Name = "Chefs choice", Description = "Chefs pizza choice of the day", Price = 10.99m },
                              new Pizza { Id = 4, Name = "Hawaiian", Description = "Pizza with ham and pineapple", Price = 11.99m },
                              new Pizza { Id = 5, Name = "BBQ Chicken", Description = "This pizza features barbecue sauce and grilled chicken", Price = 12.99m }];
        return pizzas;
    }
}
