using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportDesk.Data;
using SupportDesk.Data.Dtos;
using SupportDesk.Data.ViewModels;
using SupportDesk.Services;

namespace SupportDesk.Controllers.Ticket
{
    [Route("ticket")]
    [Authorize]
    public class TicketController(TicketService ticketService) : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TicketDto> tickets = await ticketService.GetTicketsAsync();

            IEnumerable<TicketOverviewViewModel> ticketViewModels = tickets.Select(ticket => new TicketOverviewViewModel
            (
                ticket.Id,
                ticket.Title,
                ticket.Description,
                ticket.CreationDate
            ));

            return View(ticketViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Ticket(int id)
        {
            var ticket = await ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            TicketViewModel model = new(
                ticket?.Title ?? string.Empty,
                ticket?.Description ?? string.Empty,
                ticket?.CreationDate ?? DateTime.MinValue);

            return View(model);
        }

        [HttpGet("create")]
        [Authorize(Roles = ApplicationUserRoles.Client)]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("create")]
        [Authorize(Roles = ApplicationUserRoles.Client)]
        public async Task<IActionResult> Create(CreateTicketViewModel model)
        {
            TicketDto newTicket = new()
            {
                Title = model.Title,
                Description = model.Description
            };

            bool isCreated = await ticketService.CreateTicketAsync(newTicket);

            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await ticketService.DeleteTicketAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
