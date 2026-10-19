using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Models;

namespace PostService.Controllers
{
    /// <summary>
    /// Контролер для управління поштовими відправленнями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PostingsController : ControllerBase
    {
        private static readonly List<Posting> _postings = new()
        {
            new Posting
            {
                Id = 1,
                From = "Київ",
                To = "Львів",
                Content = "Документи та книги",
                DeliveryType = DeliveryType.Courier,
                Weight = 1.5f,
                Width = 25,
                Height = 15,
                Depth = 20,
                Value = 350.0f,
                Price = 85.0f,
                CreatedAt = DateTime.UtcNow
            },
            new Posting
            {
                Id = 2,
                From = "Рівне",
                To = "Луцьк",
                Content = "Одяг та аксесуари",
                DeliveryType = DeliveryType.Standard,
                Weight = 2.0f,
                Width = 30,
                Height = 20,
                Depth = 10,
                Value = 500.0f,
                Price = 90.0f,
                CreatedAt = DateTime.UtcNow
            }
        };

        /// <summary>
        /// Отримати список усіх зареєстрованих поштових відправлень.
        /// </summary>
        /// <remarks>
        /// Повертає повний список активних поштових відправлень, які зараз є в системі.
        /// </remarks>
        /// <returns>Список об'єктів поштових відправлень.</returns>
        /// <response code="200">Успішно повернуто список відправлень.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Posting>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Posting>> GetAll()
        {
            return Ok(_postings);
        }

        /// <summary>
        /// Отримати конкретне відправлення за його унікальним ідентифікатором.
        /// </summary>
        /// <param name="id">Числовий ідентифікатор поштового відправлення.</param>
        /// <returns>Дані знайденого відправлення.</returns>
        /// <response code="200">Відправлення успішно знайдено.</response>
        /// <response code="404">Відправлення з вказаним ID не знайдено.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Posting), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Posting> GetById(int id)
        {
            var item = _postings.FirstOrDefault(p => p.Id == id);
            if (item == null)
            {
                return NotFound(new { message = $"Відправлення з ID {id} не знайдено." });
            }
            return Ok(item);
        }

        /// <summary>
        /// Створити та зареєструвати нове поштове відправлення.
        /// </summary>
        /// <remarks>
        /// Приймає дані відправлення, перевіряє обов'язкові поля (місто відправника, одержувача, вміст) та повертає створений запис з новим ID.
        /// </remarks>
        /// <param name="dto">Модель даних для створення нового відправлення (PostingCreateDto).</param>
        /// <returns>Створене відправлення з присвоєним унікальним номером.</returns>
        /// <response code="201">Відправлення успішно зареєстровано.</response>
        /// <response code="400">Помилка валідації: некоректно заповнені поля форми.</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Posting), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Posting> Create([FromBody] PostingCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posting = new Posting
            {
                Id = _postings.Count > 0 ? _postings.Max(p => p.Id) + 1 : 1,
                From = dto.From,
                To = dto.To,
                Content = dto.Content,
                DeliveryType = dto.DeliveryType,
                Weight = dto.Weight,
                Width = dto.Width,
                Height = dto.Height,
                Depth = dto.Depth,
                Value = dto.Value,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow
            };

            _postings.Add(posting);
            return CreatedAtAction(nameof(GetById), new { id = posting.Id }, posting);
        }

        /// <summary>
        /// Оновити дані існуючого відправлення за його ідентифікатором.
        /// </summary>
        /// <param name="id">Ідентифікатор відправлення, яке потрібно відредагувати.</param>
        /// <param name="dto">Оновлені дані відправлення (PostingUpdateDto).</param>
        /// <returns>Оновлений запис відправлення.</returns>
        /// <response code="200">Дані відправлення успішно оновлено.</response>
        /// <response code="400">Помилка валідації введених значень.</response>
        /// <response code="404">Відправлення з вказаним ID не знайдено.</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Posting), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] PostingUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = _postings.FirstOrDefault(p => p.Id == id);
            if (existing == null)
            {
                return NotFound(new { message = $"Відправлення з ID {id} для оновлення не знайдено." });
            }

            existing.From = dto.From;
            existing.To = dto.To;
            existing.Content = dto.Content;
            existing.DeliveryType = dto.DeliveryType;
            existing.Weight = dto.Weight;
            existing.Width = dto.Width;
            existing.Height = dto.Height;
            existing.Depth = dto.Depth;
            existing.Value = dto.Value;
            existing.Price = dto.Price;

            return Ok(existing);
        }

        /// <summary>
        /// Видалити поштове відправлення із системи.
        /// </summary>
        /// <param name="id">Ідентифікатор відправлення для видалення.</param>
        /// <response code="204">Відправлення успішно видалено (тіло відповіді відсутнє).</response>
        /// <response code="404">Відправлення з вказаним ID не знайдено або вже було видалене.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var item = _postings.FirstOrDefault(p => p.Id == id);
            if (item == null)
            {
                return NotFound(new { message = $"Відправлення з ID {id} не знайдено." });
            }

            _postings.Remove(item);
            return NoContent();
        }
    }
}
