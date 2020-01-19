using Swashbuckle.AspNetCore.Swagger;

namespace REST.API.SeedWork
{
    /// <summary>
    /// Class represent API description in swagger
    /// </summary>
    public class APIDescription
    {
        /// <summary>
        /// Title of API documentation
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Version of API
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// API description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Contact to creators
        /// </summary>
        public Contact Contact { get; set; }
    }
}