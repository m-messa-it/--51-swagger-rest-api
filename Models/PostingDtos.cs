using System.ComponentModel.DataAnnotations;

namespace PostService.Models
{
    /// <summary>
    /// Модель передачі даних (DTO) для створення нового відправлення.
    /// </summary>
    public class PostingCreateDto
    {
        /// <summary>
        /// Місто або відділення відправника.
        /// </summary>
        /// <example>Київ</example>
        [Required(ErrorMessage = "Пункт відправлення є обов'язковим.")]
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// Місто або відділення одержувача.
        /// </summary>
        /// <example>Рівне</example>
        [Required(ErrorMessage = "Пункт призначення є обов'язковим.")]
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// Опис вмісту посилки.
        /// </summary>
        /// <example>Одяг та взуття</example>
        [Required(ErrorMessage = "Опис вмісту обов'язковий.")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Тип доставки: 0 - Standard (відділення), 1 - Courier (кур'єр).
        /// </summary>
        /// <example>0</example>
        public DeliveryType DeliveryType { get; set; } = DeliveryType.Standard;

        /// <summary>
        /// Вага посилки в кілограмах (має бути більше 0).
        /// </summary>
        /// <example>2.5</example>
        [Range(0.01, 1000, ErrorMessage = "Вага повинна бути більшою за 0 кг.")]
        public float Weight { get; set; }

        /// <summary>
        /// Ширина в сантиметрах.
        /// </summary>
        /// <example>30</example>
        public int Width { get; set; }

        /// <summary>
        /// Висота в сантиметрах.
        /// </summary>
        /// <example>20</example>
        public int Height { get; set; }

        /// <summary>
        /// Довжина в сантиметрах.
        /// </summary>
        /// <example>15</example>
        public int Depth { get; set; }

        /// <summary>
        /// Оголошена вартість у гривнях.
        /// </summary>
        /// <example>500.0</example>
        public float Value { get; set; }

        /// <summary>
        /// Вартість доставки у гривнях.
        /// </summary>
        /// <example>95.0</example>
        public float Price { get; set; }
    }

    /// <summary>
    /// Модель передачі даних (DTO) для оновлення існуючого відправлення.
    /// </summary>
    public class PostingUpdateDto : PostingCreateDto
    {
    }
}
