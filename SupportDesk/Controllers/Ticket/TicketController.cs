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
    public class TicketController(TicketService ticketService, MessageService messageService) : Controller
    {
        /// <summary>
        /// Returns a view with a list of all tickets.
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TicketSummaryViewModel> ticketViewModels = (await ticketService.GetTicketsAsync())
                .Select(ticket => new TicketSummaryViewModel
                (
                    ticket.Id,
                    ticket.Title,
                    ticket.Description,
                    ticket.CreationDate
                ));

            return View(ticketViewModels);
        }

        /// <summary>
        /// Returns a view of the ticket, including its messages.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Ticket(int id)
        {
            var ticket = await ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            var messages = (await messageService.GetMessagesByTicketIdAsync(id))
                .Select(m => new MessageViewModel
                (
                    m.Id,
                    m.Content,
                    m.AuthorFullName,
                    m.Status,
                    m.PostDate
                ));

            TicketViewModel model = new(
                ticket?.Title ?? string.Empty,
                ticket?.Description ?? string.Empty,
                ticket?.CreationDate ?? DateTime.MinValue,
                messages,
                new CreateMessageViewModel { TicketId = id });

            return View(model);
        }

        /// <summary>
        /// Returns a view for creating a new ticket or editing an existing one. Only accessible to users with the "Client" role.
        /// </summary>
        [HttpGet("form")]
        [Authorize(Roles = ApplicationUserRoles.Client)]
        public async Task<IActionResult> Form(int? id)
        {
            if (id is null)
            {
                return View("Form");
            }

            var ticket = await ticketService.GetTicketByIdAsync(id.Value);

            if (ticket is null)
            {
                return NotFound();
            }

            TicketFormViewModel model = new()
            {
                Id = id.Value,
                Title = ticket.Title,
                Description = ticket.Description
            };

            return View("Form", model);
        }

        /// <summary>
        /// Handles the submission of the ticket form for creating a new ticket or updating an existing one. 
        /// Only accessible to users with the "Client" role.
        /// </summary>
        [HttpPost("form")]
        [Authorize(Roles = ApplicationUserRoles.Client)]
        public async Task<IActionResult> Form(TicketFormViewModel model)
        {
            TicketDto ticket = new()
            {
                Id = model.Id ?? 0,
                Title = model.Title,
                Description = model.Description
            };

            bool success = ticket.Id == 0 
                ? await ticketService.CreateTicketAsync(ticket) 
                : await ticketService.UpdateTicketAsync(ticket);

            if (!success)
            {
                return ticket.Id == 0 
                    ? BadRequest() 
                    : NotFound();
            }

            return RedirectToAction(nameof(Ticket), new { id = ticket.Id });
        }

        /// <summary>
        /// Deletes a ticket with the specified ID. Only accessible to users with the "Client" role.
        /// </summary>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await ticketService.DeleteTicketAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
