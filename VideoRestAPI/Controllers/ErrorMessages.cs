namespace VideoRestAPI.Controllers
{
    public static class ErrorMessages
    {
        public static string IdWasNotFoundMessage(int id) => 
            $"Id: {id} - doesn't exist";
        public static string IdDoesNotMatchMessage(int id) =>
            $"Id: {id} in URL - doesn't match the Id of the entity";

        public static string InvalidJSON => "JSON is not valid";
    }
}