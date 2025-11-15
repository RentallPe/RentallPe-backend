namespace RentalPeAPI.Profile.Domain.Model.ValueObjects;

public sealed class QuietHours
{
    public TimeOnly Start { get; private set; }
    public TimeOnly End { get; private set; }

    private QuietHours()
    {
    } 

    public QuietHours(TimeOnly start, TimeOnly end)
    {
        if (start == end) throw new ArgumentException("Quiet hours start cannot equal end.");
        Start = start;
        End = end;
    }

    public bool IsWithin(TimeOnly t)
    {
        return Start < End
            ? t >= Start && t < End
            : t >= Start || t < End;
    }
}