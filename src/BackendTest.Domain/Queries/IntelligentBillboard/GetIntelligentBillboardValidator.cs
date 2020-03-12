using FluentValidation;

namespace BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardValidator : AbstractValidator<GetIntelligentBillboardRequest>
    {
        public GetIntelligentBillboardValidator()
        {
            RuleFor(x => x.BigScreens).GreaterThanOrEqualTo(0).WithMessage("Number of Big Screens must be 0 or positive");
            RuleFor(x => x.SmallScreens).GreaterThanOrEqualTo(0).WithMessage("Number of Small Screens must be 0 or positive");
            RuleFor(x => x.BigScreens).GreaterThan(0).When(x => x.SmallScreens == 0).WithMessage("Total of Screens can't be 0");
            RuleFor(x => x.PeriodOfTimeInDays).InclusiveBetween(1,365).WithMessage("Period of time in days must be between 1 and 365");
        }
    }
}