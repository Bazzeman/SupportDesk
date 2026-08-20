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
        /// Handles the creation of a new ticket. Only accessible to users with the "Client" role.
        /// </summary>
        [HttpPost("")]
        [Authorize(Roles = ApplicationUserRoles.Client)]
        public async Task<IActionResult> Create(TicketFormViewModel model)
        {
            TicketDto newTicket = new()
            {
                Title = model.Title,
                Description = model.Description
            };

            bool isCreated = await ticketService.CreateTicketAsync(newTicket);

            if (isCreated)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        /// <summary>
        /// Returns a view with the details of a specific ticket, including its messages.
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
        /// Handles the update of an existing ticket. Only accessible to users with the "Client" role.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, TicketFormViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            TicketDto ticket = new()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description
            };

            bool isUpdated = await ticketService.UpdateTicketAsync(ticket);

            if (!isUpdated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Handles the deletion of a ticket by its ID. Only accessible to users with the "Client" role.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await ticketService.DeleteTicketAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Returns a view for creating a new ticket. Only accessible to users with the "Client" role.
        /// </summary>
        [HttpGet("create")]
        [Authorize(Roles = ApplicationUserRoles.Client)]
        public async Task<IActionResult> Create()
        {
            return View("Form");
        }

        /// <summary>
        /// Returns a view for editing an existing ticket. Only accessible to users with the "Client" role.
        /// </summary>
        [HttpGet("{id}/edit")]
        [Authorize(Roles = ApplicationUserRoles.Client)]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            TicketFormViewModel model = new()
            {
                Id = id,
                Title = ticket.Title,
                Description = ticket.Description
            };

            return View("Form", model);
        }
    }
}
