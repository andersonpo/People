using FluentValidation.Results;

namespace People.Domain.Interfaces
{
    public interface IApiNotification
    {
        int StatusCode {  get; set; }
        bool HasNotifications();
        IList<ValidationFailure> GetFailures();
        void AddFailure(ValidationFailure failure);
    }
}
