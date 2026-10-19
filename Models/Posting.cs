using System;
using System.ComponentModel.DataAnnotations;

namespace PostService.Models
{
    /// <summary>
    /// Модель поштового відправлення в системі.
    /// </summary>
    public class Posting
    {
        /// <summary>
        /// Унікальний ідентифікатор поштового відправлення.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Пункт відправлення (місто або поштове відділення).
        /// </summary>
        /// <example>Київ</example>
        [Required]
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// Пункт призначення (місто або поштове відділення одержувача).
        /// </summary>
        /// <example>Львів</example>
        [Required]
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// Опис вмісту посилки (найменування вантажу).
        /// </summary>
        /// <example>Документи та підручники</example>
        [Required]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Тип доставки (0 - стандартна у відділення, 1 - кур'єрська до дверей).
        /// </summary>
        /// <example>0</example>
        public DeliveryType DeliveryType { get; set; }

        /// <summary>
        /// Фактична вага відправлення у кілограмах.
        /// </summary>
        /// <example>1.5</example>
        [Range(0.01, 1000)]
        public float Weight { get; set; }

        /// <summary>
        /// Ширина коробки у сантиметрах.
        /// </summary>
        /// <example>25</example>
        public int Width { get; set; }

        /// <summary>
        /// Висота коробки у сантиметрах.
        /// </summary>
        /// <example>15</example>
        public int Height { get; set; }

        /// <summary>
        /// Довжина (глибина) коробки у сантиметрах.
        /// </summary>
        /// <example>20</example>
        public int Depth { get; set; }

        /// <summary>
        /// Оголошена грошова вартість відправлення в гривнях.
        /// </summary>
        /// <example>350.0</example>
        public float Value { get; set; }

        /// <summary>
        /// Вартість послуги доставки у гривнях.
        /// </summary>
        /// <example>85.0</example>
        public float Price { get; set; }

        /// <summary>
        /// Дата та час створення запису про відправлення.
        /// </summary>
        /// <example>2026-10-19T12:00:00Z</example>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
