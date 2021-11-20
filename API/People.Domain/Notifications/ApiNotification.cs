using FluentValidation.Results;
using People.Domain.Interfaces;

namespace People.Domain.Notifications
{
    public class ApiNotification : IApiNotification
    {
        private IList<ValidationFailure> failures;
        public ApiNotification()
        {
            failures = new List<ValidationFailure>();
        }

        public int StatusCode { get; set; }

        public void AddFailure(ValidationFailure failure)
        {
            failures.Add(failure);
        }

        public IList<ValidationFailure> GetFailures()
        {
            return failures;
        }

        public bool HasNotifications()
        {
            return (failures != null && failures.Any());
        }
    }
}
