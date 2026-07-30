using Microsoft.AspNetCore.Mvc;
using SupportDesk.Models.Dtos;
using SupportDesk.Models.ViewModels;
using SupportDesk.Services;

namespace SupportDesk.Controllers.Ticket
{
    [Route("tickets")]
    public class TicketController(TicketService ticketService) : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TicketDto> tickets = await ticketService.GetTicketsAsync();

            IEnumerable<TicketViewModel> ticketViewModels = tickets.Select(ticket => new TicketViewModel
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                CreationDate = ticket.CreationDate
            });

            return View(ticketViewModels);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            TicketDto newTicket = new()
            {
                Title = "New Ticket",
                Description = "This is a new ticket."
            };

            bool isCreated = await ticketService.CreateTicketAsync(newTicket);

            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }
    }
}
