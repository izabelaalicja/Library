using System;
using System.Threading.Tasks;
using Library.Models.DTO;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rollbar;

namespace Library.Controllers
{
    [Route("api/book-borrows")]
    //[Authorize(Roles="Reader")]
    public class BookBorrowsController : Controller
    {
        private readonly IBookBorrowRepository _bookBorrowRepository;
        private readonly ILogger<BookBorrowsController> _logger;

        public BookBorrowsController(IBookBorrowRepository bookBorrowRepository, ILogger<BookBorrowsController> logger)
        {
            _bookBorrowRepository = bookBorrowRepository;
            _logger = logger;
        }


        [HttpPost(Name = nameof(AddBookBorrow))]
        public async Task<IActionResult> AddBookBorrow([FromBody] BookBorrowDto borrow)
        {
            RollbarLocator.RollbarInstance.Error(new Exception("Błąd z Rollbar add bookborrow"));
            _logger.LogError("Wystąpił błąd przy dodaniu bookborrowsa");
            var res = await _bookBorrowRepository.AddBookBorrow(borrow);
            return CreatedAtRoute(nameof(AddBookBorrow), res);
        }

        [HttpPut("{idBookBorrow}")]
        public async Task<IActionResult> UpdateBookBorrow([FromBody] UpdateBookBorrowDto borrow)
        {
            RollbarLocator.RollbarInstance.Error(new Exception("Błąd z Rollbar update bookborrow"));
            _logger.LogError("Wystąpił błąd przy updacie bookborrowsa");
            await _bookBorrowRepository.ChangeBookBorrow(borrow);
            return NoContent();
        }

        
    }
}