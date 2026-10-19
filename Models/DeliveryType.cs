namespace PostService.Models
{
    /// <summary>
    /// Спосіб доставки поштового відправлення.
    /// </summary>
    public enum DeliveryType
    {
        /// <summary>
        /// Стандартна доставка у відділення пошти.
        /// </summary>
        Standard = 0,

        /// <summary>
        /// Кур'єрська доставка за адресою одержувача.
        /// </summary>
        Courier = 1
    }
}
