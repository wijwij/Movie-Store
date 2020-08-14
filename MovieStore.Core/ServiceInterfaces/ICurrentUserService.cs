namespace MovieStore.Core.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        public int? Id { get; }
        public bool IsAuthenticated { get; }
        public string Email { get; }
        public string RemoteIpAddress { get; }
    }
}