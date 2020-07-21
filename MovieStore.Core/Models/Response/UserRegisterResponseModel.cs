namespace MovieStore.Core.Models.Response
{
    public class UserRegisterResponseModel
    {
        /*
         * ViewModels, creating just for views/client.
         *   only have properties that are required by the view, no more no less.
         * ViewModels are also useful when you want to combine multiple models. they are called ViewModels in MVC
         * DTO (data transfer object) in API
         */
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}